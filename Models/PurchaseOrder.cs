using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;
public partial class PurchaseOrder
{
    [Display(Name = "رقم امر الشراء")]
    public int PurchaseId { get; set; }

    [Display(Name = "تاريخ الشراء")]
    public DateOnly? PurchaseDate { get; set; }

    [Display(Name = "رقم التعريفي الخاص بالموظف")]
    public string EmployeeId { get; set; } = null!;

    [Display(Name = "المبلغ المتبقي")]
    public decimal? RemainingAmount { get; set; }

    [Display(Name = "المبلغ المدفوع")]
    public decimal? PaidAmount { get; set; }

    [Display(Name = "رقم التعريفي الخاص بالتاجر")]
    public int VendorId { get; set; }

    [Display(Name = "ملاحظات")]
    public string? PurchaseNotes { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<PaymentOrder> PaymentOrders { get; set; } = new List<PaymentOrder>();

    public virtual ICollection<QuantityBridge> QuantityBridges { get; set; } = new List<QuantityBridge>();

    public virtual ICollection<ReturnsOrder> ReturnsOrders { get; set; } = new List<ReturnsOrder>();

    public virtual Vendor Vendor { get; set; } = null!;
}
