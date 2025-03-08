using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class Ink
{
    [Display(Name = "الرقم الميز للحبر")]
    public int InkId { get; set; }
    
    [Display(Name = "اسم الحبر")]
    public string Name { get; set; } = null!;
    
    [Display(Name = "القيمة المتوفرة")]
    public decimal? TotalBalance { get; set; }
    
    [Display(Name = "اللون")]
    public string Colored { get; set; } = null!;
    
    [Display(Name = "السعر")]
    public decimal Price { get; set; }
    
    [Display(Name = "الكمية")]
    public int Quantity { get; set; }
    
    [Display(Name = "نقطة اعادة الشراء")]
    public decimal? ReorderPoint { get; set; }

    [Display(Name = "حالة الاستخدام")]
    public bool Activated { get; set; }
}
