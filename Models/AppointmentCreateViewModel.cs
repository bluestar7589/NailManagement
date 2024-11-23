

using System.ComponentModel.DataAnnotations;

namespace NailManagement.Models
{
    public class AppointmentCreateViewModel
    {
        public int AppointmentId { get; set; }
        //public int CustomerId { get; set; }
        //public int TechnicianId { get; set; }
        //public int ServiceId { get; set; }

        [Required(ErrorMessage = "Appointment Date is required.")]
        [DataType(DataType.DateTime)] 
        public DateTime? AppointmentDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

        public List<Technician>? Technicians { get; set; } = null;

        public List<Service>? Services { get; set; } = null;

        public Customer? Customer { get; set; } = null;
        public Service? Service { get; set; } = null;
        public Technician? Technician { get; set; } = null;
    }
}
