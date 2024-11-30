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
    /// Controller for managing customers.
    /// </summary>
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersController"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays a list of customers.
        /// </summary>
        /// <returns>The view with the list of customers.</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        /// <summary>
        /// Displays the details of a specific customer.
        /// </summary>
        /// <param name="id">The ID of the customer.</param>
        /// <returns>The view with the customer details.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        /// <summary>
        /// Displays the form to create a new customer.
        /// </summary>
        /// <param name="phoneNumber">The phone number to pre-fill in the form.</param>
        /// <param name="newCus">Indicates if the customer is new.</param>
        /// <returns>The view with the create customer form.</returns>
        public IActionResult Create(string phoneNumber, bool newCus = false)
        {
            Customer customer = new Customer();

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                customer.PhoneNumber = phoneNumber;
            }

            return View(customer);
        }

        /// <summary>
        /// Handles the form submission to create a new customer.
        /// </summary>
        /// <param name="customer">The customer to create.</param>
        /// <returns>The view with the create customer form or redirects to the appointment creation page.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,FirstName,LastName,Email,PhoneNumber,DateOfBirth,JoinDate,LoyaltyPoints")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Appointment", new { phoneNumber = customer.PhoneNumber });
            }

            return View(customer);
        }

        /// <summary>
        /// Displays the form to edit an existing customer.
        /// </summary>
        /// <param name="id">The ID of the customer to edit.</param>
        /// <returns>The view with the edit customer form.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        /// <summary>
        /// Handles the form submission to edit an existing customer.
        /// </summary>
        /// <param name="id">The ID of the customer to edit.</param>
        /// <param name="customer">The customer to edit.</param>
        /// <returns>The view with the edit customer form or redirects to the customer list.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,FirstName,LastName,Email,PhoneNumber,DateOfBirth,JoinDate,LoyaltyPoints")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            return View(customer);
        }

        /// <summary>
        /// Displays the form to delete an existing customer.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>The view with the delete customer form.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        /// <summary>
        /// Handles the form submission to delete an existing customer.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>Redirects to the customer list.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks if a customer exists.
        /// </summary>
        /// <param name="id">The ID of the customer.</param>
        /// <returns>True if the customer exists, otherwise false.</returns>
        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
