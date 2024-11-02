using static NailManagement.Data.AppointmentDB;

namespace NailManagement.Models
{
    public class HomeIndexViewModel
    {
        /// <summary>
        /// To get the appointment information
        /// </summary>
        public List<AppointmentDTO> Appointments { get; set; } = null;

        /// <summary>
        /// To get the list of technician
        /// </summary>
        public List<Technician> Technicians { get; set; } = null;
        // Add other models as needed
    }
}
