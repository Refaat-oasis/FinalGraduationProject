using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class Machine
{
    [Display(Name = "رقم الماكينة")]
    public int MachineId { get; set; }

    [Display(Name = "اسم الماكينة")]
    public string MachineProcessName { get; set; } = null!;

    [Display(Name = "سعر")]
    public decimal Price { get; set; }

    [Display(Name = "التفعيل")]
    public bool Activated { get; set; }


    public virtual ICollection<ProcessBridge> ProcessBridges { get; set; } = new List<ProcessBridge>();
}
