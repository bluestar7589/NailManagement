using System;
using System.Collections.Generic;

namespace NailManagement.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? AppointmentId { get; set; }

    public decimal? Amount { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public string? PaymentMethod { get; set; }

    public decimal? Tip { get; set; }

    public virtual Appointment? Appointment { get; set; }
}
