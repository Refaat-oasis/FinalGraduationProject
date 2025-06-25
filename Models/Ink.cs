using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;
public partial class Ink
{
    [Display(Name = "الرقم التعريفي للحبر")]
    public int InkId { get; set; }

    [Display(Name = "اسم الحبر")]
    [Required(ErrorMessage = "يجب ادخال اسم الحبر")]
    [RegularExpression("^[\\u0600-\\u06FF ]+$", ErrorMessage = "اسم الحبر يجب أن يحتوي على حروف عربية ومسافات فقط")]
    public string Name { get; set; } = null!;


    [Display(Name = "القيمة المتاحة")]
    public decimal? TotalBalance { get; set; }

    [Display(Name = "اللون")]
    [Required(ErrorMessage = "يجب ادخال قيمة اللون")]
    public string Colored { get; set; } = null!;

    [Display(Name = "السعر")]
    public decimal Price { get; set; }

    [Display(Name = "السحبات")]
    public int Quantity { get; set; }

    [Display(Name = "الوحدات")]
    public int NumberOfUnits { get; set; }
    [Display(Name = "متوسط السحبات")]
    public int AverageQuantity { get; set; }

    [Display(Name = "نقطة اعادة الطلب")]
    [Range(0, double.MaxValue, ErrorMessage = "يجب إدخال رقم أكبر من او يساوي الصفر")]
    public decimal? ReorderPoint { get; set; }


    public decimal UnitPrice { get; set; }

    [Display(Name = "التفعيل")]
    public bool Activated { get; set; }

    public virtual ICollection<QuantityBridge> QuantityBridges { get; set; } = new List<QuantityBridge>();
}
