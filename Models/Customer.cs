using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NailManagement.Models;

public partial class Customer : Person
{
    [Key]
    public int CustomerId { get; set; }

    [Display(Name = "Join Date")]
    [DataType(DataType.Date)] 
    public DateOnly? JoinDate { get; set; }

    
    [Display(Name = "Loyalty Points")]
    public int? LoyaltyPoints { get; set; }

    
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    
    public virtual ICollection<LoyaltyPoint> LoyaltyPointsNavigation { get; set; } = new List<LoyaltyPoint>();
}
