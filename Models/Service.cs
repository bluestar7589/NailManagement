using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NailManagement.Models;

/// <summary>
/// Represents a service that can be provided by the salon.
/// </summary>
public partial class Service
{
    /// <summary>
    /// Gets or sets the unique identifier for the service.
    /// </summary>
    [Key]
    public int ServiceId { get; set; }

    /// <summary>
    /// Gets or sets the name of the service.
    /// </summary>
    [Required(ErrorMessage = "Service name is required.")]
    [StringLength(50)]
    [Display(Name = "Service Name")]
    public string ServiceName { get; set; }

    /// <summary>
    /// Gets or sets the description of the service.
    /// </summary>
    [StringLength(500)]
    [Display(Name = "Description")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the price of the service.
    /// </summary>
    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, 1000.00, ErrorMessage = "Price must be between $0.01 and $1000.")]
    [DataType(DataType.Currency)]
    [Display(Name = "Price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the duration of the service in minutes.
    /// </summary>
    [Required(ErrorMessage = "Duration is required.")]
    [Range(1, 240, ErrorMessage = "Duration must be between 1 and 240 minutes.")]
    [Display(Name = "Duration (minutes)")]
    public int DurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets the appointments that are associated with the service.
    /// </summary>
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}

/// <summary>
/// This class to present the top service in the salon
/// </summary>
public class ServiceTopDTO
{
    public String? ServiceName { get; set; }

    public int Count { get; set; }
}
