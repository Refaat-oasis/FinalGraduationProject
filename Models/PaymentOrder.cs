using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;
public partial class PaymentOrder
{
    [Display(Name = "رقم امر الدفع")]
    public int PaymentId { get; set; }

    [Display(Name = "رقم امر الشراء")]
    public int? PurchaseId { get; set; }

    [Display(Name = "المبلغ")]
    public decimal? Amount { get; set; }

    [Display(Name = "تاريخ امر الدفع")]
    public DateOnly? PaymentDate { get; set; }

    [Display(Name = "الرقما لتعريفي الخاص بالموظف")]
    public string? EmployeeId { get; set; }

    [Display(Name = "ملاحظات")]
    public string? PaymentNotes { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual PurchaseOrder? Purchase { get; set; }
}
