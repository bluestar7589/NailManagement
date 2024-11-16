using NailManagement.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NailManagement.Data
{
    public static class ServiceDB
    {
        /// <summary>
        /// This function to get all the services from database
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Return the list all of services</returns>
        public static List<Service> GetAllServices(ApplicationDbContext context)
        {
            return context.Services.ToList();
        }

        // Write the function to get the top service in the salon relate to appointment
        public static List<ServiceTopDTO> GetTopService(ApplicationDbContext context, DateOnly dateFrom, DateOnly dateTo)
        {
            // Get the top service in the salon
            var topService = from s in context.Services
                             join a in context.Appointments on s.ServiceId equals a.ServiceId
                             join p in context.Payments on a.AppointmentId equals p.AppointmentId
                             where p.PaymentDate >= dateFrom && p.PaymentDate <= dateTo
                             group s by s.ServiceName into g
                             orderby g.Count() descending
                             select new ServiceTopDTO
                             {
                                 ServiceName = g.Key,
                                 Count = g.Count()
                             };
            return topService.ToList();
        }
    }
}
