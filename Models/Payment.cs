using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NailManagement.Models;

public partial class Payment
{
    [Key]
    public int PaymentId { get; set; }

    public int? AppointmentId { get; set; }

    public decimal? Amount { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public string? PaymentMethod { get; set; }

    public decimal? Tip { get; set; }

    public virtual Appointment? Appointment { get; set; }
}

public class PaymentDTO
{
    [Key]
    public int PaymentId { get; set; } = 0;

    public String? CustomerName { get; set; }

    public String? TechnicianName { get; set; }

    public double Amount { get; set; }

    public String? Service { get; set; }

    public DateOnly PaymentDate { get; set; }

    public String? PaymentMethod { get; set; }

    public double Tip { get; set; }
}
