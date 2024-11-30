

using System.ComponentModel.DataAnnotations;

namespace NailManagement.Models
{
    /// <summary>
    /// ViewModel for creating an appointment.
    /// </summary>
    public class AppointmentCreateViewModel
    {
        /// <summary>
        /// Gets or sets the appointment ID.
        /// </summary>
        public int AppointmentId { get; set; }

        /// <summary>
        /// Gets or sets the customer ID.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the technician ID.
        /// </summary>
        public int TechnicianId { get; set; }

        /// <summary>
        /// Gets or sets the service ID.
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the appointment date.
        /// </summary>
        [Required(ErrorMessage = "Appointment Date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime? AppointmentDate { get; set; }

        /// <summary>
        /// Gets or sets the status of the appointment.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets any additional notes for the appointment.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the list of available technicians.
        /// </summary>
        public List<Technician>? Technicians { get; set; } = null;

        /// <summary>
        /// Gets or sets the list of available services.
        /// </summary>
        public List<Service>? Services { get; set; } = null;

        /// <summary>
        /// Gets or sets the customer associated with the appointment.
        /// </summary>
        public Customer? Customer { get; set; } = null;

        /// <summary>
        /// Gets or sets the service associated with the appointment.
        /// </summary>
        public Service? Service { get; set; } = null;

        /// <summary>
        /// Gets or sets the technician associated with the appointment.
        /// </summary>
        public Technician? Technician { get; set; } = null;
    }
}
