using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ThothSystemVersion1.Models;

public partial class Paper
{
    [Display(Name = "رقم الورق")]
    public int PaperId { get; set; }

    [Display(Name = "اسم الورق")]
    public string Name { get; set; } = null!;

    [Display(Name = "النوع")]
    public string? Type { get; set; }

    [Display(Name = "الوزن")]
    public decimal? Weight { get; set; }

    [Display(Name = "القيمة المتاحة")]
    public decimal? TotalBalance { get; set; }

    [Display(Name = "اللون")]
    public string Colored { get; set; } = null!;

    [Display(Name = "الكمية")]
    public int Quantity { get; set; }

    [Display(Name = "السعر")]
    public decimal Price { get; set; }

    [Display(Name = "نقطة اعادة الطلب")]
    public decimal? ReorderPoint { get; set; }

    [Display(Name = "التفعيل")]
    public bool Activated { get; set; }

    public virtual ICollection<QuantityBridge> QuantityBridges { get; set; } = new List<QuantityBridge>();
}
