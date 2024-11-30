using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NailManagement.Models;

/// <summary>
/// Represents a customer in the nail management system.
/// </summary>
public partial class Customer : Person
{
    /// <summary>
    /// Gets or sets the unique identifier for the customer.
    /// </summary>
    [Key]
    public int CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the date the customer joined.
    /// </summary>
    [Display(Name = "Join Date")]
    [DataType(DataType.Date)]
    public DateOnly? JoinDate { get; set; }

    /// <summary>
    /// Gets or sets the loyalty points of the customer.
    /// </summary>
    [Display(Name = "Loyalty Points")]
    public int? LoyaltyPoints { get; set; }

    /// <summary>
    /// Gets or sets the appointments associated with the customer.
    /// </summary>
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    /// <summary>
    /// Gets or sets the loyalty points navigation property.
    /// </summary>
    public virtual ICollection<LoyaltyPoint> LoyaltyPointsNavigation { get; set; } = new List<LoyaltyPoint>();
}
