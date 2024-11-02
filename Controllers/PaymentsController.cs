using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NailManagement.Data;
using NailManagement.Models;

namespace NailManagement.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new PaymentIndexViewModel
            {
                Payments = PaymentDB.GetAllPaymentFromRange(_context, DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now))
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PaymentIndexViewModel viewModel)
        {
            viewModel.Payments = PaymentDB.GetAllPaymentFromRange(_context, viewModel.DateFrom, viewModel.DateTo);
            return View(viewModel);
        }
    }
}
