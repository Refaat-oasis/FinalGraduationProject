using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class PhysicalCountOrder
{
    [Display(Name = "الرقم التعريفي للجرد")]
    public int PhysicalCountId { get; set; }
    
    [Display(Name = "الرقم القومي للموظف")]
    public string? EmployeeId { get; set; }
    
    [Display(Name = "تاريخ الجرد")]
    public DateOnly? PhysicalCountDate { get; set; }
    
    [Display(Name = "ملاحظات عن الجرد")]
    public string? PhysicalCountNotes { get; set; }

    public virtual Employee? Employee { get; set; }
}
