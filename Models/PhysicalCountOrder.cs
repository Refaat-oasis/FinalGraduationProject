using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;
public partial class PhysicalCountOrder
{
    [Display(Name = "رقم امر المرتجع")]
    public int PhysicalCountId { get; set; }

    [Display(Name = "الرقما لتعريفي الخاص بالموظف")]
    public string? EmployeeId { get; set; }

    [Display(Name = "تاريخ الجرد")]
    public DateOnly? PhysicalCountDate { get; set; }

    [Display(Name = "ملاحظات")]
    public string? PhysicalCountNotes { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<QuantityBridge> QuantityBridges { get; set; } = new List<QuantityBridge>();
}
