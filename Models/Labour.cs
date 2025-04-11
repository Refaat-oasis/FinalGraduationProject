using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class Labour
{
    [Display(Name = "الرقم التعرفي للعملية العمالية")]
    public int LabourId { get; set; }

    [Display(Name = "اسم العملية العمالية")]
    public string LabourProcessName { get; set; } = null!;

    [Display(Name = "السعر")]
    public decimal Price { get; set; }

    [Display(Name = "التفعيل")]
    public bool Activated { get; set; }

    public virtual ICollection<ProcessBridge> ProcessBridges { get; set; } = new List<ProcessBridge>();
}
