using NailManagement.Models;

namespace NailManagement.Data
{
    public static class TechnicianDB
    {
        /// <summary>
        /// This function to get all the technicians from database
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Return the list all of technicians</returns>
        public static List<Technician> GetAllTechnicians(ApplicationDbContext context)
        {
            return context.Technicians.ToList();
        }
    }
}
