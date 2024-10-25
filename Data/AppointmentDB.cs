
using Microsoft.EntityFrameworkCore;
using NailManagement.Models;

namespace NailManagement.Data
{
    public static class AppointmentDB
    {
        /// <summary>
        /// Get all the appointment(s) from database
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static List<AppointmentDTO> GetAllAppointments(ApplicationDbContext context)
        {
            // Get the currently date today
            DateTime today = DateTime.Today;
            // get all the appointment from the database with join condition
            var appointments = from a in context.Appointments
                               join c in context.Customers on a.CustomerId equals c.CustomerId
                               join t in context.Technicians on a.TechnicianId equals t.TechnicianId
                               join s in context.Services on a.ServiceId equals s.ServiceId
                               where a.AppointmentDate.HasValue && a.AppointmentDate.Value.Date >= today
                               select new AppointmentDTO
                                {
                                    AppointmentID = a.AppointmentId,
                                    CustomerName = c.FirstName + " " + c.LastName,
                                    TechnicianName = t.FirstName + " " + t.LastName,
                                    AppointmentDate = (DateTime)a.AppointmentDate,
                                    Service = s.ServiceName,
                                    Price = (double)s.Price,
                                    Status = a.Status,
                                    Notes = a.Notes
                                };

            return appointments.ToList();
        }

        /// <summary>
        /// Add new appointment
        /// </summary>
        /// <param name="appt">Appointment information from customer</param>
        public static void AddAppointment(ApplicationDbContext context, Appointment appointment)
        {
            {
                
                context.Appointments.Add(appointment);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Delete the appointment from database
        /// </summary>
        /// <param name="appointment"></param>
        public static void DeleteAppointment(ApplicationDbContext context, Appointment appointment)
        {
            {
                context.Appointments.Remove(appointment);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Update the appointment information
        /// </summary>
        /// <param name="appointment"></param>
        public static void UpdateAppointment(ApplicationDbContext context, Appointment appointment)
        {
            
            {

                var existingAppointment = context.Appointments.Find(appointment.AppointmentId);
                if (existingAppointment != null)
                {
                    existingAppointment.TechnicianId = appointment.TechnicianId;
                    existingAppointment.ServiceId = appointment.ServiceId;
                    existingAppointment.AppointmentDate = appointment.AppointmentDate;
                    context.SaveChanges();
                }

            }
        }

        /// <summary>
        /// Get information of the appointment by appointment ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Appointment?> GetAppointmentByIDAsync(ApplicationDbContext context, int id)
        {
                // Get the appointment information by appointment ID
                return await context.Appointments.FirstOrDefaultAsync(m => m.AppointmentId == id);
        }
    }
}
