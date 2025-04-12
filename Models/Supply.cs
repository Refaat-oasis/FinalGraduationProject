using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;
public partial class Supply
{
    [Display(Name = "الرقم التعريفي للمستلزمات")]
    public int SuppliesId { get; set; }

    [Display(Name = "اسم المستلزم")]
    [Required(ErrorMessage = "يجب ادخال اسم المستلزم")]
    public string Name { get; set; } = null!;

    [Display(Name = "القيمة المتاحة")]
    public decimal? TotalBalance { get; set; }

    [Display(Name = "السعر")]
    public decimal Price { get; set; }

    [Display(Name = "الكمية")]
    public int Quantity { get; set; }

    [Display(Name = "نقطة اعادة الطلب")]
    [Required(ErrorMessage = "يجب ادخال قيمة نقطة اعادة الشراء")]
    public decimal? ReorderPoint { get; set; }

    [Display(Name = "التفعيل")]
    public bool Activated { get; set; }

    public virtual ICollection<QuantityBridge> QuantityBridges { get; set; } = new List<QuantityBridge>();
}
