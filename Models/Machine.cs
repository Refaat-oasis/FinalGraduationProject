using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class Machine
{
    [Display(Name = "الاسم التعريفي للألة")]
    public int MachineId { get; set; }
    
    [Display(Name = "اسم الالة")]
    public string MachineProcessName { get; set; } = null!;
    
    [Display(Name = "سعر ساعة العمل للالة")]
    public decimal Price { get; set; }

    [Display(Name = "حالة الاستخدام")]
    public bool Activated { get; set; }
}
