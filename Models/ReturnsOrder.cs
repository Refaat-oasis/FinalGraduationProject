using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class ReturnsOrder
{
    [Display(Name = "الرقم التعريفي لاذن المردودات")]
    public int ReturnId { get; set; }
    [Display(Name = "تاريخ اذن المردودات")]
    public DateOnly? ReturnDate { get; set; }
    [Display(Name = "الرقم التعريفي للتشغيلة ")]
    public int JobOrderId { get; set; }
    [Display(Name = "الرقم القومي للموظف")]
    public string EmployeeId { get; set; } = null!;
    [Display(Name = "ملاحظات عن اذن المردودات")]
    public string? ReturnsNotes { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual JobOrder JobOrder { get; set; } = null!;
}
