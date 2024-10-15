using System;
using System.Collections.Generic;

namespace NailManagement.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int? CustomerId { get; set; }

    public int? TechnicianId { get; set; }

    public int? ServiceId { get; set; }

    public DateTime? AppointmentDate { get; set; }

    public string? Status { get; set; }

    public string? Notes { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Service? Service { get; set; }

    public virtual Technician? Technician { get; set; }
}

/// <summary>
/// Create the ProductDTO class to store the appointment information with join condition
/// </summary>
public class AppointmentDTO
{

    public int AppointmentID { get; set; }

    public String CustomerName { get; set; }

    public String TechnicianName { get; set; }

    public string Service { get; set; }

    public double Price { get; set; }

    public DateTime AppointmentDate { get; set; }

    public String Status { get; set; }

    public String Notes { get; set; }

}
