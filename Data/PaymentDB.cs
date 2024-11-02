using NailManagement.Models;

namespace NailManagement.Data
{
    public static class PaymentDB
    {
        /// <summary>
        ///  This function to get all the payments from the range of date
        /// </summary>
        /// <param name="context">The ApplicationDbContext to connect to DB</param>
        /// <param name="dateFrom">The begin date for the range</param>
        /// <param name="dateTo">the end date for the range</param>
        /// <returns>Return the list of all the payments from the range of date</returns>
        public static List<PaymentDTO> GetAllPaymentFromRange(ApplicationDbContext context, DateOnly dateFrom, DateOnly dateTo)
        {
            var payments = from p in context.Payments
                           join a in context.Appointments on p.AppointmentId equals a.AppointmentId
                           join c in context.Customers on a.CustomerId equals c.CustomerId
                           join t in context.Technicians on a.TechnicianId equals t.TechnicianId
                           join s in context.Services on a.ServiceId equals s.ServiceId
                           where p.PaymentDate.HasValue && p.PaymentDate.Value >= dateFrom && p.PaymentDate.Value <= dateTo
                           select new PaymentDTO
                           {
                               PaymentId = p.PaymentId,
                               CustomerName = c.FirstName + " " + c.LastName,
                               TechnicianName = t.FirstName + " " + t.LastName,
                               Service = s.ServiceName,
                               Amount = (double)s.Price,
                               PaymentDate = (DateOnly)p.PaymentDate,
                               PaymentMethod = p.PaymentMethod,
                               Tip = (double)p.Tip
                           };
            return payments.ToList();
        }
    }
}
