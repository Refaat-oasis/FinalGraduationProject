using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;

public partial class Ink
{
    public int InkId { get; set; }

    public string Name { get; set; } = null!;

    public decimal? TotalBalance { get; set; }

    public string Colored { get; set; } = null!;

    public decimal Price { get; set; }

    public DateOnly ExpireDate { get; set; }

    public int Quantity { get; set; }

    public decimal? ReorderPoint { get; set; }
}
