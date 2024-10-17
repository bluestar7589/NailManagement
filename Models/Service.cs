using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NailManagement.Models;

public partial class Service
{
    [Key]
    public int ServiceId { get; set; }

    [Required(ErrorMessage = "Service name is required.")]
    [StringLength(50)]
    [Display(Name = "Service Name")]
    public string ServiceName { get; set; }

    [StringLength(500)]
    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, 1000.00, ErrorMessage = "Price must be between $0.01 and $1000.")]
    [DataType(DataType.Currency)]
    [Display(Name = "Price")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Duration is required.")]
    [Range(1, 240, ErrorMessage = "Duration must be between 1 and 240 minutes.")]
    [Display(Name = "Duration (minutes)")]
    public int DurationMinutes { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
