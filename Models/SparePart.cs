using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;

public partial class SparePart
{
    public int SparePartsId { get; set; }

    public string Name { get; set; } = null!;

    public decimal? TotalBalance { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal? ReorderPoint { get; set; }

    public bool? Activated { get; set; }

    public virtual ICollection<QuantityBridge> QuantityBridges { get; set; } = new List<QuantityBridge>();
}
