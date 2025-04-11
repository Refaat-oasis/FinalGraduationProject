using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;
public partial class ReturnsOrder
{
    [Display(Name = "الرقم التعريفي لامر المرتجع")]
    public int ReturnId { get; set; }

    [Display(Name = "تاريخ المرتجع")]
    public DateOnly? ReturnDate { get; set; }

    [Display(Name = "رقم امر التصنيع")]
    public int? JobOrderId { get; set; }

    [Display(Name = "رقم امر الشراء")]
    public int? PurchaseId { get; set; }

    [Display(Name = "مرتجع داخلي او خارجي")]
    public bool ReturnInOut { get; set; }

    [Display(Name = "الرقم التعريفي الخاص بالموظف")]
    public string EmployeeId { get; set; } = null!;

    [Display(Name = "ملاحظات")]
    public string? ReturnsNotes { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual JobOrder? JobOrder { get; set; }

    public virtual PurchaseOrder? Purchase { get; set; }

    public virtual ICollection<QuantityBridge> QuantityBridges { get; set; } = new List<QuantityBridge>();
}
