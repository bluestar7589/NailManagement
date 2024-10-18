using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NailManagement.Models;

public partial class Technician : Person
{
    [Key]
    public int TechnicianId { get; set; }

    [Display(Name = "Specialties")]
    [StringLength(100, ErrorMessage = "Specialties cannot be longer than 100 characters.")]
    public string? Specialties { get; set; }

    [Display(Name = "Rating")]
    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
    public decimal? Rating { get; set; }

    [Display(Name = "Profile Picture")]
    [Url(ErrorMessage = "Invalid URL.")]
    [StringLength(255, ErrorMessage = "Profile picture URL cannot be longer than 255 characters.")]
    public string? ProfilePicture { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<StaffSchedule> StaffSchedules { get; set; } = new List<StaffSchedule>();
}
