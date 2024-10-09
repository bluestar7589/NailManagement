using System;
using System.Collections.Generic;

namespace NailManagement.Models;

public partial class Customer : Person
{
    public int CustomerId { get; set; }

    public DateOnly? JoinDate { get; set; }

    public int? LoyaltyPoints { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<LoyaltyPoint> LoyaltyPointsNavigation { get; set; } = new List<LoyaltyPoint>();
}
