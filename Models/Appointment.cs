using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NailManagement.Models;

/// <summary>
/// Represents an appointment in the nail management system.
/// </summary>
public partial class Appointment
{
    /// <summary>
    /// Gets or sets the unique identifier for the appointment.
    /// </summary>
    public int AppointmentId { get; set; }

    /// <summary>
    /// Gets or sets the customer ID associated with the appointment.
    /// </summary>
    public int? CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the technician ID associated with the appointment.
    /// </summary>
    public int? TechnicianId { get; set; }

    /// <summary>
    /// Gets or sets the service ID associated with the appointment.
    /// </summary>
    public int? ServiceId { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the appointment.
    /// </summary>
    public DateTime? AppointmentDate { get; set; }

    /// <summary>
    /// Gets or sets the status of the appointment.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets any additional notes for the appointment.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the customer associated with the appointment.
    /// </summary>
    public virtual Customer? Customer { get; set; }

    /// <summary>
    /// Gets or sets the feedbacks associated with the appointment.
    /// </summary>
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    /// <summary>
    /// Gets or sets the payments associated with the appointment.
    /// </summary>
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    /// <summary>
    /// Gets or sets the service associated with the appointment.
    /// </summary>
    public virtual Service? Service { get; set; }

    /// <summary>
    /// Gets or sets the technician associated with the appointment.
    /// </summary>
    public virtual Technician? Technician { get; set; }
}

/// <summary>
/// Represents a data transfer object for appointment information with join conditions.
/// </summary>
public class AppointmentDTO
{
    /// <summary>
    /// Gets or sets the unique identifier for the appointment.
    /// </summary>
    public int AppointmentID { get; set; }

    /// <summary>
    /// Gets or sets the name of the customer.
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// Gets or sets the name of the technician.
    /// </summary>
    public string TechnicianName { get; set; }

    /// <summary>
    /// Gets or sets the name of the service.
    /// </summary>
    public string Service { get; set; }

    /// <summary>
    /// Gets or sets the price of the service.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the appointment.
    /// </summary>
    public DateTime AppointmentDate { get; set; }

    /// <summary>
    /// Gets or sets the status of the appointment.
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets any additional notes for the appointment.
    /// </summary>
    public string Notes { get; set; }
}

/// <summary>
/// Represents a data transfer object for creating an appointment with customer and appointment details.
/// </summary>
public class AppointmentCreateViewDTO
{
    /// <summary>
    /// Gets or sets the customer ID associated with the appointment.
    /// </summary>
    public int? CustomerId { get; set; }

    // Customer information

    /// <summary>
    /// Gets or sets the phone number of the customer.
    /// </summary>
    [Required]
    [Phone]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the first name of the customer.
    /// </summary>
    [StringLength(50)]
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the customer.
    /// </summary>
    [StringLength(50)]
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the date of birth of the customer.
    /// </summary>
    [DataType(DataType.Date)]
    public DateOnly? DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the email address of the customer.
    /// </summary>
    [EmailAddress]
    public string? Email { get; set; }

    // Appointment details

    /// <summary>
    /// Gets or sets the date and time of the appointment.
    /// </summary>
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime Date { get; set; } = DateTime.Today; // Date of the appointment, default to current date.

    /// <summary>
    /// Gets or sets the status of the appointment.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets any additional notes for the appointment.
    /// </summary>
    public string? Notes { get; set; } // Additional notes for the appointment

    // Dropdown selection IDs

    /// <summary>
    /// Gets or sets the selected technician ID.
    /// </summary>
    [Required]
    public int TechnicianId { get; set; } // Selected technician ID

    /// <summary>
    /// Gets or sets the selected service ID.
    /// </summary>
    [Required]
    public int ServiceId { get; set; } // Selected service ID

    // Lists of available options

    /// <summary>
    /// Gets or sets the list of available technicians.
    /// </summary>
    public List<Technician> Technicians { get; set; } = new List<Technician>(); // Available technicians

    /// <summary>
    /// Gets or sets the list of available services.
    /// </summary>
    public List<Service> Services { get; set; } = new List<Service>(); // Available services

    /// <summary>
    /// Gets or sets the list of available customers for reference only.
    /// </summary>
    public List<Customer> Customers { get; set; } = new List<Customer>(); // For reference only
}
