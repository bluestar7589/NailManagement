using System;
using System.Collections.Generic;

namespace NailManagement.Models;

public partial class LoyaltyPoint
{
    public int LoyaltyId { get; set; }

    public int? CustomerId { get; set; }

    public int? PointsEarned { get; set; }

    public int? PointsRedeemed { get; set; }

    public DateTime? TransactionDate { get; set; }

    public virtual Customer? Customer { get; set; }
}
