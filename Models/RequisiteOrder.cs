using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class RequisiteOrder
{
    [Display(Name = "الرقم التعريفي لازن الصرف")]
    public int RequisiteId { get; set; }
    [Display(Name = "تاريخ اذن الصرف")]
    public DateOnly? RequisiteDate { get; set; }
    [Display(Name = "الرقم القومي للموظف")]
    public string EmployeeId { get; set; } = null!;
    [Display(Name = "الرقم التعريفي للتشغيلة")]
    public int JobOrderId { get; set; }
    [Display(Name = "ملاحظات عن اذن الصرف")]
    public string? RequisiteNotes { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual JobOrder JobOrder { get; set; } = null!;
}
