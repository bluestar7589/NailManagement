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
        /// Retrieves and displays a list of all appointments.
        /// </summary>
        /// <returns>The index view populated with a list of appointments.</returns>
        public async Task<IActionResult> Index()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Technician)
                .Include(a => a.Service)
                .ToListAsync();

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

            return View(viewModels);
        }

        /// <summary>
        /// Retrieves the details of a specific appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to retrieve.</param>
        /// <returns>The details view of the specified appointment.</returns>
        public async Task<ActionResult> Details(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Service)
                .Include(a => a.Technician)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
            {
                return NotFound();
            }

            var viewModel = new AppointmentCreateViewModel
            {
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status,
                Notes = appointment.Notes,
                Customer = appointment.Customer,
                Technician = appointment.Technician,
                Service = appointment.Service
            };

            return View(viewModel);
        }

        /// <summary>
        /// Checks if a customer exists based on their phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number of the customer to check.</param>
        /// <returns>A JSON result indicating whether the customer exists and their details if found.</returns>
        [HttpGet]
        public async Task<IActionResult> CheckCustomer(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return Json(new { exists = false });
            }

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

            if (customer != null)
            {
                return Json(new { exists = true, customer });
            }
            else
            {
                return Json(new { exists = false });
            }
        }

        /// <summary>
        /// Displays the appointment creation form.
        /// </summary>
        /// <param name="phoneNumber">Optional phone number to pre-fill customer information.</param>
        /// <returns>The create view with necessary data for creating an appointment.</returns>
        [AllowAnonymous]
        public async Task<ActionResult> Create(string phoneNumber = null)
        {
            var viewDTO = new AppointmentCreateViewDTO
            {
                Technicians = await _context.Technicians.ToListAsync(),
                Services = await _context.Services.ToListAsync(),
                Customers = await _context.Customers.ToListAsync(),
                Date = DateTime.Now
            };

            return View(viewDTO);
        }

        /// <summary>
        /// Handles the submission of the appointment creation form.
        /// </summary>
        /// <param name="viewDTO">The data transfer object containing appointment details.</param>
        /// <returns>Redirects to the index view if successful, otherwise redisplays the form with validation errors.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(AppointmentCreateViewDTO viewDTO)
        {
            if (ModelState.IsValid)
            {
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.PhoneNumber == viewDTO.PhoneNumber);

                if (customer == null)
                {
                    return RedirectToAction("Create", "Customers", new { phoneNumber = viewDTO.PhoneNumber, newCus = true });
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

            viewDTO.Technicians = await _context.Technicians.ToListAsync();
            viewDTO.Services = await _context.Services.ToListAsync();
            return View(viewDTO);
        }

        /// <summary>
        /// Displays the appointment edit form for a specific appointment.
        /// </summary>
        /// <param name="id">The ID of the appointment to edit.</param>
        /// <returns>The edit view populated with the appointment details.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Customer)
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

        /// <summary>
        /// Handles the submission of the appointment edit form.
        /// </summary>
        /// <param name="id">The ID of the appointment to edit.</param>
        /// <param name="viewModel">The data transfer object containing updated appointment details.</param>
        /// <returns>Redirects to the index view if successful, otherwise redisplays the form with validation errors.</returns>
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

            appointment.TechnicianId = viewModel.TechnicianId;
            appointment.ServiceId = viewModel.ServiceId;
            appointment.AppointmentDate = viewModel.Date;
            appointment.Status = viewModel.Status;
            appointment.Notes = viewModel.Notes;

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

        /// <summary>
        /// Displays the confirmation view for deleting a specific appointment.
        /// </summary>
        /// <param name="id">The ID of the appointment to delete.</param>
        /// <returns>The delete view populated with the appointment details.</returns>
        public async Task<ActionResult> Delete(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Technician)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
            {
                return NotFound();
            }

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

        /// <summary>
        /// Handles the deletion of a specific appointment.
        /// </summary>
        /// <param name="id">The ID of the appointment to delete.</param>
        /// <param name="collection">The form collection containing additional data.</param>
        /// <returns>Redirects to the index view if successful, otherwise redisplays the delete view.</returns>
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
