using System;
using System.Collections.Generic;

namespace NailManagement.Models;

public partial class StaffSchedule
{
    public int ScheduleId { get; set; }

    public int? TechnicianId { get; set; }

    public DateOnly? ShiftDate { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public virtual Technician? Technician { get; set; }
}
