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
                CustomerId = appointment.CustomerId ?? 0,
                TechnicianId = appointment.TechnicianId ?? 0,
                ServiceId = appointment.ServiceId ?? 0,
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
                CustomerId = appointment.CustomerId ?? 0,
                TechnicianId = appointment.TechnicianId ?? 0,
                ServiceId = appointment.ServiceId ?? 0,
                Customer = appointment.Customer, // Link the Customer entity
                Technician = appointment.Technician, // Link the Technician entity
                Service = appointment.Service // Link the Service entity
            };

            return View(viewModel); // Pass the ViewModel to the view
        }

        [AllowAnonymous]
        // GET: AppointmentController/Create
        public async Task<ActionResult> Create()
        {
            // Create a new instance of the AppointmentCreateViewDTO
            var viewDTO = new AppointmentCreateViewDTO
            {
                Technicians = await _context.Technicians.ToListAsync(),
                Services = await _context.Services.ToListAsync(),
                Customers = await _context.Customers.ToListAsync()
            };
            

            return View(viewDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentCreateViewDTO viewDTO)
        {
            if (ModelState.IsValid)
            {
                // Check if the phone number exists in the database
                Customer customer = await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == viewDTO.PhoneNumber);
                if (customer == null)
                {
                    // Redirect to Customer creation page with the phone number
                    return RedirectToAction("Create", "Customer", new { phoneNumber = viewDTO.PhoneNumber });
                    bool insertSuccess = false;
                    try { 
                        _context.Customers.Add(new Customer
                        {
                            PhoneNumber = viewDTO.PhoneNumber,
                            Email = viewDTO.Email,
                            FirstName = viewDTO.FirstName,
                            LastName = viewDTO.LastName,
                            DateOfBirth = viewDTO.DateOfBirth
                        });
                        insertSuccess = true;
                    }
                    catch (Exception e)
                    {
                        insertSuccess = false;
                    }

                }

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

            // Repopulate ViewBag in case of an error
            ViewBag.CustomerId = new SelectList(await _context.Customers.ToListAsync(), "CustomerId", "Email", viewDTO.PhoneNumber);
            ViewBag.TechnicianId = new SelectList(await _context.Technicians.ToListAsync(), "TechnicianId", "Email", viewDTO.Technicians);
            ViewBag.ServiceId = new SelectList(await _context.Services.ToListAsync(), "ServiceId", "ServiceName", viewDTO.Services);

            return View(viewDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null)
            {
                return NotFound();
            }
            else {
                var appointment = await _context.Appointments.FindAsync(id);
                if (appointment == null) {
                    return NotFound();
                }

                Customer cus = await _context.Customers.FindAsync(appointment.CustomerId);
                var appointmentCreateViewModelDTO = new AppointmentCreateViewDTO()
                {
                    PhoneNumber = cus.PhoneNumber,
                    FirstName = cus.FirstName,
                    LastName = cus.LastName,
                    DateOfBirth = cus.DateOfBirth,
                    Email = cus.Email,
                    Notes = appointment.Notes,
                    Date = (DateTime)appointment.AppointmentDate,
                    Technicians = _context.Technicians.ToList(),
                    Services = _context.Services.ToList()
                };
                return View(appointmentCreateViewModelDTO);
            }
        }

        // POST: AppointmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppointmentCreateViewDTO viewModel)
        {
            // Remove the PhoneNumber validation error if it exists
            ModelState.Remove(nameof(viewModel.PhoneNumber));

            if (ModelState.IsValid)
            {
                var appointment = await _context.Appointments.FindAsync(id);
                if (appointment == null)
                {
                    return NotFound();
                }

                // Check if the phone number exists in the database
                Customer customer = await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == viewModel.PhoneNumber);
                if (customer == null)
                {
                    // Redirect to Customer creation page with the phone number
                    return RedirectToAction("Create", "Customer", new { phoneNumber = viewModel.PhoneNumber });
                }

                // Update appointment details
                appointment.CustomerId = customer.CustomerId;
                appointment.TechnicianId = viewModel.TechnicianId;
                appointment.ServiceId = viewModel.ServiceId;
                appointment.AppointmentDate = viewModel.Date;
                appointment.Status = viewModel.Status;
                appointment.Notes = viewModel.Notes;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
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
