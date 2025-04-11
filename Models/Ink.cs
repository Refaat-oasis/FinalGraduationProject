using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;
public partial class Ink
{
    [Display(Name = "الرقم التعريفي للحبر")]
    public int InkId { get; set; }

    [Display(Name = "اسم الحبر")]
    public string Name { get; set; } = null!;

    [Display(Name = "القيمة المتاحة")]
    public decimal? TotalBalance { get; set; }

    [Display(Name = "اللوم")]
    public string Colored { get; set; } = null!;

    [Display(Name = "السعر")]
    public decimal Price { get; set; }

    [Display(Name = "الكمية")]
    public int Quantity { get; set; }

    [Display(Name = "نقطة اعادة الطلب")]
    public decimal? ReorderPoint { get; set; }

    [Display(Name = "التفعيل")]
    public bool Activated { get; set; }

    public virtual ICollection<QuantityBridge> QuantityBridges { get; set; } = new List<QuantityBridge>();
}
