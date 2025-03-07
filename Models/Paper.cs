using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class Paper
{
    [Display(Name = "الرقم التعريفي للورق")]
    public int PaperId { get; set; }
    
    [Display(Name = "اسم خام الورق")]
    public string Name { get; set; } = null!;
    
    [Display(Name = "نوع الورق")]
    public string? Type { get; set; }
    
    [Display(Name = "وزن الورق")]
    public decimal? Weight { get; set; }
    
    [Display(Name = "القيمة المتوفرة")]
    public decimal? TotalBalance { get; set; }
    
    [Display(Name = "اللون")]
    public string Colored { get; set; } = null!;
    
    [Display(Name = "الكمية")]
    public int Quantity { get; set; }
    
    [Display(Name = "السعر")]
    public decimal Price { get; set; }
    
    [Display(Name = "نقطة اعادة الشراء")]
    public decimal? ReorderPoint { get; set; }
}
