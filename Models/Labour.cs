using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class Labour
{
    [Display(Name = "الاسم التعرفي للألة للعامل")]
    public int LabourId { get; set; }
    
    [Display(Name = "اسم الألة للعامل")]
    public string LabourProcessName { get; set; } = null!;
    
    [Display(Name = "سعر ساعة العمل للعامل")]
    public decimal Price { get; set; }

    [Display(Name = "حالة الاستخدام")]
    public bool Activated { get; set; }
}
