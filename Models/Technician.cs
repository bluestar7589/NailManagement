using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NailManagement.Models;

/// <summary>
/// Represents a technician in the nail management system.
/// </summary>
public partial class Technician : Person
{
    /// <summary>
    /// Gets or sets the unique identifier for the technician.
    /// </summary>
    [Key]
    public int TechnicianId { get; set; }

    /// <summary>
    /// Gets or sets the specialties of the technician.
    /// Specialties are optional and cannot exceed 100 characters.
    /// </summary>
    [Display(Name = "Specialties")]
    [StringLength(100, ErrorMessage = "Specialties cannot be longer than 100 characters.")]
    public string? Specialties { get; set; }

    /// <summary>
    /// Gets or sets the rating of the technician.
    /// The rating must be between 0 and 5.
    /// </summary>
    [Display(Name = "Rating")]
    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
    public decimal? Rating { get; set; }

    /// <summary>
    /// Gets or sets the profile picture URL of the technician.
    /// The URL must be valid and cannot exceed 255 characters.
    /// </summary>
    [Display(Name = "Profile Picture")]
    [Url(ErrorMessage = "Invalid URL.")]
    [StringLength(255, ErrorMessage = "Profile picture URL cannot be longer than 255 characters.")]
    public string? ProfilePicture { get; set; }

    /// <summary>
    /// Gets or sets the collection of appointments associated with the technician.
    /// A technician can have multiple appointments.
    /// </summary>
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    /// <summary>
    /// Gets or sets the collection of staff schedules associated with the technician.
    /// A technician can have multiple schedules.
    /// </summary>
    public virtual ICollection<StaffSchedule> StaffSchedules { get; set; } = new List<StaffSchedule>();
}
