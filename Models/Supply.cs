using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class Supply
{
    [Display(Name = "الرقم التعريفي للمستلزمات")]
    public int SuppliesId { get; set; }
    
    [Display(Name = "اسم المستلزمات")]
    public string Name { get; set; } = null!;
    
    [Display(Name = "القيمة المتوفرة")]
    public decimal? TotalBalance { get; set; }
    
    [Display(Name = "السعر")]
    public decimal Price { get; set; }
    
    [Display(Name = "الكمية")]
    public int Quantity { get; set; }
    
    [Display(Name = "نقطة اعادة الشراء")]
    public decimal? ReorderPoint { get; set; }
}
