using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ThothSystemVersion1.Models;

public partial class Paper
{
    [Display(Name = "رقم الورق")]
    public int PaperId { get; set; }

    [Display(Name = "اسم الورق")]
    [Required(ErrorMessage = "يجب ادخال اسم الورق")]
    [RegularExpression("^[\\u0600-\\u06FF ]+$", ErrorMessage = "اسم الورق يجب أن يحتوي على حروف عربية ومسافات فقط")]
    public string Name { get; set; } = null!;

    [Display(Name = "الحجم")]
    [Required(ErrorMessage = "يجب ادخال حجم الورق")]
    public string? Size { get; set; }


    [Display(Name = "الوزن")]
    [Required(ErrorMessage = "يجب ادخال قيمة الوزن")]
    [Range(0.00001, double.MaxValue, ErrorMessage = "يجب إدخال رقم أكبر من الصفر")]
    public decimal? Weight { get; set; }

    [Display(Name = "القيمة المتاحة")]
    public decimal? TotalBalance { get; set; }

    [Display(Name = "اللون")]
    [Required(ErrorMessage = "يجب ادخال قيمة اللون")]
    public string Colored { get; set; } = null!;

    [Display(Name = "الكمية")]
    public int Quantity { get; set; }

    [Display(Name = "السعر")]
    public decimal Price { get; set; }

    [Display(Name = "نقطة اعادة الطلب")]
    [Range(0.00001, double.MaxValue, ErrorMessage = "يجب إدخال رقم أكبر من الصفر")]
    public decimal? ReorderPoint { get; set; }

    [Display(Name = "التفعيل")]
    public bool Activated { get; set; }

    public virtual ICollection<QuantityBridge> QuantityBridges { get; set; } = new List<QuantityBridge>();
}