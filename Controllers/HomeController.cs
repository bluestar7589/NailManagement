using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NailManagement.Data;
using NailManagement.Models;
using System.Diagnostics;

namespace NailManagement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// To keep the connection to the database
        /// </summary>
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize(Roles = IdentityHelper.Admin)]
        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                // Get all appointments and technicians
                Appointments = AppointmentDB.GetAllAppointments(_context),
                Technicians = TechnicianDB.GetAllTechnicians(_context)
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
