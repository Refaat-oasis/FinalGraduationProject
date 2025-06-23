using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class Labour
{
    [Display(Name = "الرقم التعرفي للعملية ")]
    public int LabourId { get; set; }

    [Display(Name = "اسم العملية ")]
    [Required(ErrorMessage = "يجب ادخال اسم العملية ")]
    [RegularExpression("^[\\u0600-\\u06FF ]+$", ErrorMessage = "اسم العملية يجب أن يحتوي على حروف عربية ومسافات فقط")]
    public string LabourProcessName { get; set; } = null!;

    [Display(Name = "اجر الساعة")]
    [Required(ErrorMessage = "يجب ادخال السعر")]
    [Range(0.00001, double.MaxValue, ErrorMessage = "يجب إدخال رقم أكبر من الصفر")]
    public decimal Price { get; set; }

    [Display(Name = "التفعيل")]
    public bool Activated { get; set; }

    public virtual ICollection<ProcessBridge> ProcessBridges { get; set; } = new List<ProcessBridge>();
}
