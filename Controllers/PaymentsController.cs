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
    [Authorize(Roles = IdentityHelper.Admin)]
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 
        /// <summary>
        /// GET method: Get the report for the lastest day
        /// </summary>
        /// <returns>Return the list of payment report on the lastest date</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DateOnly? latestPaymentDate = await _context.Payments
                .OrderByDescending(p => p.PaymentDate)
                .Select(p => p.PaymentDate)
                .FirstOrDefaultAsync();
            // If there is no payment, set the latest payment date to the current date
            if (latestPaymentDate == null) {
                latestPaymentDate = DateOnly.FromDateTime(DateTime.Now);
            }
            // Get the list of payment report on the lastest date
            var viewModel = new PaymentIndexViewModel
            {
                DateFrom = (DateOnly)latestPaymentDate,
                DateTo = (DateOnly)latestPaymentDate,
                Payments = PaymentDB.GetAllPaymentFromRange(_context, (DateOnly)latestPaymentDate, (DateOnly)latestPaymentDate)
            };
            return View(viewModel);
        }

        /// <summary>
        /// POST method: To get the report from the range of date
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>Return the list of payment report on the range</returns>
        [HttpPost]
        public async Task<IActionResult> Index(PaymentIndexViewModel viewModel)
        {
            // Get the list of payment report on the range
            viewModel.Payments = PaymentDB.GetAllPaymentFromRange(_context, viewModel.DateFrom, viewModel.DateTo);
            return View(viewModel);
        }
    }
}
