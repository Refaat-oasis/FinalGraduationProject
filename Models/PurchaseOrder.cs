using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class PurchaseOrder
{
    [Display(Name = "الرقم التعريفي لامر الشراء")]
    public int PurchaseId { get; set; }
    
    [Display(Name = "تاريخ الشراء")]
    public DateOnly? PurchaseDate { get; set; }
    
    [Display(Name = "الرقم القومي للموظف")]
    public string EmployeeId { get; set; } = null!;
    
    [Display(Name = "الرقم التعريفي للتاجر")]
    public int VendorId { get; set; }
    
    [Display(Name = "ملاحظات عن شراء البضاعة")]
    public string? PurchaseNotes { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Vendor Vendor { get; set; } = null!;
}
