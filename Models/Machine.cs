using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class Machine
{
    [Display(Name = "رقم الماكينة")]
    public int MachineId { get; set; }

    [Display(Name = "اسم الماكينة")]
    [Required(ErrorMessage = "يجب ادخال اسم الماكينة")]
    [RegularExpression("^[\\u0600-\\u06FF ]+$", ErrorMessage = "اسم الورق يجب أن يحتوي على حروف عربية ومسافات فقط")]
    public string MachineProcessName { get; set; } = null!;

    [Display(Name = "سعر الساعة")]
    [Required(ErrorMessage = "يجب ادخال سعر الماكينة")]
    [Range(0.00001, double.MaxValue, ErrorMessage = "يجب إدخال رقم أكبر من الصفر")]
    public decimal Price { get; set; }

    [Display(Name = "التفعيل")]
    public bool Activated { get; set; }


    public virtual ICollection<ProcessBridge> ProcessBridges { get; set; } = new List<ProcessBridge>();
}
