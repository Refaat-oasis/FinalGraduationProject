using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;

public partial class Paper
{
    public int PaperId { get; set; }

    public string Name { get; set; } = null!;

    public string? Type { get; set; }

    public decimal? Weight { get; set; }

    public decimal? TotalBalance { get; set; }

    public string Colored { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal? ReorderPoint { get; set; }
}
