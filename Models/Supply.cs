using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;

public partial class Supply
{
    public int SuppliesId { get; set; }

    public string Name { get; set; } = null!;

    public decimal? TotalBalance { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal? ReorderPoint { get; set; }
}
