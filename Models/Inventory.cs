using System;
using System.Collections.Generic;

namespace NailManagement.Models;

public partial class Inventory
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public int? Quantity { get; set; }

    public string? Supplier { get; set; }

    public int? ReorderLevel { get; set; }

    public DateTime? LastUpdated { get; set; }
}
