using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;
public partial class RequisiteOrder
{
    [Display(Name = "رقم امر الصرف")]
    public int RequisiteId { get; set; }

    [Display(Name = "تاريخ امر الصرف")]
    public DateOnly? RequisiteDate { get; set; }

    [Display(Name = "الرقم التعريفي الخاص بالموظف")]
    public string EmployeeId { get; set; } = null!;

    [Display(Name = "رقم امر التصنيع")]
    public int JobOrderId { get; set; }

    [Display(Name = "ملاحظات")]
    public string? RequisiteNotes { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual JobOrder JobOrder { get; set; } = null!;

    public virtual ICollection<QuantityBridge> QuantityBridges { get; set; } = new List<QuantityBridge>();
}
