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
    /// Controller for managing technicians.
    /// </summary>
    [Authorize]
    public class TechniciansController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechniciansController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public TechniciansController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the list of technicians.
        /// </summary>
        /// <returns>A view of the list of technicians.</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Technicians.ToListAsync());
        }

        /// <summary>
        /// Gets the details of a specific technician.
        /// </summary>
        /// <param name="id">The technician ID.</param>
        /// <returns>A view of the technician details, or a 404 error if not found.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technician = await _context.Technicians
                .FirstOrDefaultAsync(m => m.TechnicianId == id);
            if (technician == null)
            {
                return NotFound();
            }

            return View(technician);
        }

        /// <summary>
        /// Displays the create technician form.
        /// </summary>
        /// <returns>A view of the create technician form.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a new technician.
        /// </summary>
        /// <param name="technician">The technician to create.</param>
        /// <returns>A redirect to the index view if successful; otherwise, the create view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TechnicianId,Specialties,Rating,ProfilePicture,FirstName,LastName,Email,PhoneNumber,DateOfBirth")] Technician technician)
        {
            if (ModelState.IsValid)
            {
                _context.Add(technician);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(technician);
        }

        /// <summary>
        /// Displays the edit technician form.
        /// </summary>
        /// <param name="id">The technician ID.</param>
        /// <returns>A view of the edit technician form, or a 404 error if not found.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technician = await _context.Technicians.FindAsync(id);
            if (technician == null)
            {
                return NotFound();
            }
            return View(technician);
        }

        /// <summary>
        /// Edits an existing technician.
        /// </summary>
        /// <param name="id">The technician ID.</param>
        /// <param name="technician">The technician to edit.</param>
        /// <returns>A redirect to the index view if successful; otherwise, the edit view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TechnicianId,Specialties,Rating,ProfilePicture,FirstName,LastName,Email,PhoneNumber,DateOfBirth")] Technician technician)
        {
            if (id != technician.TechnicianId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(technician);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechnicianExists(technician.TechnicianId))
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
            return View(technician);
        }

        /// <summary>
        /// Displays the delete technician confirmation form.
        /// </summary>
        /// <param name="id">The technician ID.</param>
        /// <returns>A view of the delete technician confirmation form.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technician = await _context.Technicians
                .FirstOrDefaultAsync(m => m.TechnicianId == id);
            if (technician == null)
            {
                return NotFound();
            }

            return View(technician);
        }

        /// <summary>
        /// Deletes a technician.
        /// </summary>
        /// <param name="id">The technician ID.</param>
        /// <returns>A redirect to the index view.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var technician = await _context.Technicians.FindAsync(id);
            if (technician != null)
            {
                _context.Technicians.Remove(technician);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks if a technician exists.
        /// </summary>
        /// <param name="id">The technician ID.</param>
        /// <returns>True if the technician exists; otherwise, false.</returns>
        private bool TechnicianExists(int id)
        {
            return _context.Technicians.Any(e => e.TechnicianId == id);
        }
    }
}
