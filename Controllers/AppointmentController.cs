using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NailManagement.Data;
using NailManagement.Models;

namespace NailManagement.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // Retrieve all appointments from the database
            var appointments = _context.Appointments.ToList();

            // Pass the list of appointments to the view
            return View(appointments);
        }

        /// <summary>
        /// This function to get the detail for the appointment by AppointmentID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Details(int id)
        {
            await AppointmentDB.GetAppointmentByIDAsync(_context, id);
            return View();
        }

        // GET: AppointmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppointmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppointmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AppointmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppointmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppointmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
