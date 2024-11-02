using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NailManagement.Models;

public partial class Payment
{
    /// <summary>
    /// The payment id
    /// </summary>
    [Key]
    public int PaymentId { get; set; }

    /// <summary>
    /// The appointment id for Appointment table
    /// </summary>
    public int? AppointmentId { get; set; }

    /// <summary>
    /// The amount for the service
    /// </summary>
    public decimal? Amount { get; set; }

    /// <summary>
    /// The payment date when paid
    /// </summary>
    public DateOnly? PaymentDate { get; set; }

    /// <summary>
    /// The payment method when paid
    /// </summary>
    public string? PaymentMethod { get; set; }

    /// <summary>
    /// The tip when paid
    /// </summary>
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
