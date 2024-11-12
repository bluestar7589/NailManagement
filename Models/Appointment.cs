using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

public class AppointmentCreateViewDTO
{
    // Customer information
    [Required]
    [Phone]
    public string? PhoneNumber { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? DateOfBirth { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    // Appointment details
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime Date { get; set; } // Date of the appointment

    public string Notes { get; set; } // Additional notes for the appointment

    // Dropdown selection IDs
    [Required]
    public int TechnicianId { get; set; } // Selected technician ID

    [Required]
    public int ServiceId { get; set; } // Selected service ID

    // Lists of available options
    public List<Technician> Technicians { get; set; } = new List<Technician>(); // Available technicians
    public List<Service> Services { get; set; } = new List<Service>(); // Available services
    public List<Customer> Customers { get; set; } = new List<Customer>(); // For reference only
}
