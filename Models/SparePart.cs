using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class SparePart
{

    [Display(Name = "الرقم التعريفي لقطعة الغيار")]

    public int SparePartsId { get; set; }

    [Display(Name = "اسم قطعة الغيار")]
    [Required(ErrorMessage = "يجب ادخال اسم قطعة الغيار")]

    public string Name { get; set; } = null!;

    [Display(Name = "القيمة المتاحة")]

    public decimal? TotalBalance { get; set; }

    [Display(Name = "الكمية")]
    //[Range(0, double.MaxValue, ErrorMessage = "يجب إدخال رقم أكبر من او يساوي الصفر")]

    public int Quantity { get; set; }

    [Display(Name = "السعر")]
    //[Range(0.000001, double.MaxValue, ErrorMessage = "يجب إدخال رقم أكبر من الصفر")]

    public decimal Price { get; set; }

    [Display(Name = "نقطة اعادة الطلب")]
    [Range(0, double.MaxValue, ErrorMessage = "يجب إدخال رقم أكبر من او يساوي الصفر")]

    public decimal? ReorderPoint { get; set; }

    [Display(Name = "التفعيل")]

    public bool? Activated { get; set; }

    public virtual ICollection<QuantityBridge> QuantityBridges { get; set; } = new List<QuantityBridge>();
}
