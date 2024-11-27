using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NailManagement.Data;
using NailManagement.Models;

namespace NailManagement.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Display a list of appointments
        /// </summary>
        /// <returns>The index view with a list of appointments.</returns>
        public async Task<IActionResult> Index()
        {
            // Fetch appointments from the database
            var appointments = await _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Technician)
                .Include(a => a.Service)
                .ToListAsync();

            // Map appointments to the AppointmentCreateViewModel
            var viewModels = appointments.Select(appointment => new AppointmentCreateViewModel
            {
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status,
                Notes = appointment.Notes,
                Customer = appointment.Customer,
                Technician = appointment.Technician,
                Service = appointment.Service 
            }).ToList();

            // Return the view with the view models
            return View(viewModels);
        }

        /// <summary>
        /// This function to get the detail for the appointment by AppointmentID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Details(int id)
        {
            // Retrieve the appointment by ID
            var appointment = await _context.Appointments
                .Include(a => a.Customer)    
                .Include(a => a.Service)
                .Include(a => a.Technician)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
            {
                return NotFound(); // Handle case when appointment is not found
            }

            // Map to ViewModel
            var viewModel = new AppointmentCreateViewModel
            {
                AppointmentId = appointment.AppointmentId, 
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status,
                Notes = appointment.Notes,
                Customer = appointment.Customer, // Link the Customer entity
                Technician = appointment.Technician, // Link the Technician entity
                Service = appointment.Service // Link the Service entity
            };

            return View(viewModel); // Pass the ViewModel to the view
        }

        [HttpGet]
        public async Task<IActionResult> CheckCustomer(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return Json(new { exists = false });
            }

            // Attempt to find a customer by phone number
            var customer = await _context.Customers
                .Where(c => c.PhoneNumber == phoneNumber)
                .Select(c => new
                {
                    c.FirstName,
                    c.LastName,
                    c.Email,
                    c.DateOfBirth
                })
                .FirstOrDefaultAsync();

            // If customer is found, return their details
            if (customer != null)
            {
                return Json(new { exists = true, customer });
            }
            else
            {
                return Json(new { exists = false });
            }
        }


        [AllowAnonymous]
        // GET: Appointments/Create
        public async Task<ActionResult> Create(string phoneNumber = null)
        {
            var viewDTO = new AppointmentCreateViewDTO
            {
                Technicians = await _context.Technicians.ToListAsync(),
                Services = await _context.Services.ToListAsync(),
                Customers = await _context.Customers.ToListAsync(),
                Date = DateTime.Now // Default date
            };

            return View(viewDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppointmentCreateViewDTO viewDTO)
        {
            if (ModelState.IsValid)
            {
                // Check if the customer exists by phone number
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.PhoneNumber == viewDTO.PhoneNumber);

                // If customer does not exist, redirect to Customer/Create
                if (customer == null)
                {
                    return RedirectToAction("Create", "Customers", new { phoneNumber = viewDTO.PhoneNumber, newCus = true });
                }

                // Create a new appointment
                var appointment = new Appointment
                {
                    CustomerId = customer.CustomerId,
                    TechnicianId = viewDTO.TechnicianId,
                    ServiceId = viewDTO.ServiceId,
                    AppointmentDate = viewDTO.Date,
                    Status = viewDTO.Status,
                    Notes = viewDTO.Notes
                };

                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate the dropdown lists if ModelState is invalid
            viewDTO.Technicians = await _context.Technicians.ToListAsync();
            viewDTO.Services = await _context.Services.ToListAsync();
            return View(viewDTO);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Customer) // Include Customer entity
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
            {
                return NotFound();
            }

            var appointmentCreateViewModelDTO = new AppointmentCreateViewDTO
            {
                PhoneNumber = appointment.Customer.PhoneNumber,
                FirstName = appointment.Customer.FirstName,
                LastName = appointment.Customer.LastName,
                DateOfBirth = appointment.Customer.DateOfBirth,
                Email = appointment.Customer.Email,
                Notes = appointment.Notes,
                Date = (DateTime)appointment.AppointmentDate,
                Technicians = _context.Technicians.ToList(),
                Services = _context.Services.ToList()
            };

            return View(appointmentCreateViewModelDTO);
        }



        // POST: AppointmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppointmentCreateViewDTO viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            // Update appointment properties directly
            appointment.TechnicianId = viewModel.TechnicianId;
            appointment.ServiceId = viewModel.ServiceId;
            appointment.AppointmentDate = viewModel.Date;
            appointment.Status = viewModel.Status;
            appointment.Notes = viewModel.Notes;

            // Save changes to the database
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Appointments.Any(a => a.AppointmentId == id))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToAction(nameof(Index));
        }





        // GET: AppointmentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Customer)    // Include the related Customer
                .Include(a => a.Technician)  // Include the related Technician
                .Include(a => a.Service)      // Include the related Service
                .FirstOrDefaultAsync(a => a.AppointmentId == id); // Fetch the appointment by ID

            if (appointment == null)
            {
                return NotFound();
            }

            // Create the ViewModel and populate it with the appointment data
            var viewModel = new AppointmentCreateViewModel
            {
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate ?? DateTime.Now,
                Status = appointment.Status,
                Notes = appointment.Notes,
                Customer = appointment.Customer,
                Service = appointment.Service,
                Technician = appointment.Technician
            };

            return View(viewModel);
        }

        // POST: AppointmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var appointment = await _context.Appointments.FindAsync(id);
                if (appointment != null)
                {
                    _context.Appointments.Remove(appointment);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
