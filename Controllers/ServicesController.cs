using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NailManagement.Data;
using NailManagement.Models;

namespace NailManagement.Controllers
{
    /// <summary>
    /// Controller for managing services.
    /// </summary>
    [Authorize]
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicesController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the list of all services.
        /// </summary>
        /// <returns>The view with the list of services.</returns>
        public IActionResult Index()
        {
            return View(ServiceDB.GetAllServices(_context));
        }

        /// <summary>
        /// Generates a report of services.
        /// </summary>
        /// <returns>The view with the service report.</returns>
        public async Task<IActionResult> ReportAsync()
        {
            DateOnly? latestPaymentDate = await _context.Payments
                .OrderByDescending(p => p.PaymentDate)
                .Select(p => p.PaymentDate)
                .FirstOrDefaultAsync();
            // If there is no payment, set the latest payment date to the current date
            if (latestPaymentDate == null)
            {
                latestPaymentDate = DateOnly.FromDateTime(DateTime.Now);
            }
            var viewModel = new ServiceReportViewModel
            {
                DateFrom = (DateOnly)latestPaymentDate,
                DateTo = (DateOnly)latestPaymentDate,
                Services = ServiceDB.GetTopService(_context, (DateOnly)latestPaymentDate, (DateOnly)latestPaymentDate)
            };
            return View(viewModel);
        }

        /// <summary>
        /// Gets the details of a specific service.
        /// </summary>
        /// <param name="id">The ID of the service.</param>
        /// <returns>The view with the service details.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        /// <summary>
        /// Displays the create service form.
        /// </summary>
        /// <returns>The view with the create service form.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a new service.
        /// </summary>
        /// <param name="service">The service to create.</param>
        /// <returns>The view with the created service.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,ServiceName,Description,Price,DurationMinutes")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        /// <summary>
        /// Displays the edit service form.
        /// </summary>
        /// <param name="id">The ID of the service to edit.</param>
        /// <returns>The view with the edit service form.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        /// <summary>
        /// Edits an existing service.
        /// </summary>
        /// <param name="id">The ID of the service to edit.</param>
        /// <param name="service">The service to edit.</param>
        /// <returns>The view with the edited service.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceId,ServiceName,Description,Price,DurationMinutes")] Service service)
        {
            if (id != service.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.ServiceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        /// <summary>
        /// Displays the delete service confirmation form.
        /// </summary>
        /// <param name="id">The ID of the service to delete.</param>
        /// <returns>The view with the delete service confirmation form.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        /// <summary>
        /// Deletes a service.
        /// </summary>
        /// <param name="id">The ID of the service to delete.</param>
        /// <returns>The view with the deleted service.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks if a service exists.
        /// </summary>
        /// <param name="id">The ID of the service.</param>
        /// <returns>True if the service exists, otherwise false.</returns>
        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.ServiceId == id);
        }
    }
}
