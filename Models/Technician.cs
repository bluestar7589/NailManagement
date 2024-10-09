using System;
using System.Collections.Generic;

namespace NailManagement.Models;

public partial class Technician
{
    public int TechnicianId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Specialties { get; set; }

    public decimal? Rating { get; set; }

    public string? ProfilePicture { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<StaffSchedule> StaffSchedules { get; set; } = new List<StaffSchedule>();
}
