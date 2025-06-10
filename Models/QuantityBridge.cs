using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;
public partial class QuantityBridge
{
    [Display(Name = "الرقم التعريفي")]
    public int? QuantityBridgeId { get; set; }

    [Display(Name = "السعر")]
    public decimal Price { get; set; }

    [Display(Name = "رقم امر المرتجع")]
    public int? ReturnId { get; set; }

    [Display(Name = "رقم امر الشراء")]
    public int? PurchaseId { get; set; }

    [Display(Name = "الكمية")]
    public int Quantity { get; set; }

    [Display(Name ="الوحدات")]
    public int NumberOfUnits { get; set; }

    [Display(Name = "القيمة")]
    public decimal? TotalBalance { get; set; }

    [Display(Name = "القيمة القديمة")]
    public int? OldQuantity { get; set; }

    [Display(Name = "السعر القديم")]
    public decimal? OldPrice { get; set; }

    [Display(Name = "القيمة القديمة")]
    public decimal? OldTotalBalance { get; set; }

    [Display(Name = "رقم امر الصرف")]
    public int? RequisiteId { get; set; }

    [Display(Name = "الرقم التعريفي الخاص بالورق")]
    public int? PaperId { get; set; }

    [Display(Name = "الرقم التعريفي الخاص بالحبر")]
    public int? InkId { get; set; }

    [Display(Name = "الرقم التعريفي الخاص بالمستلزمات")]
    public int? SuppliesId { get; set; }

    [Display(Name = "رقم امر الجرد")]
    public int? PhysicalCountId { get; set; }

    public virtual Ink? Ink { get; set; }

    public virtual Paper? Paper { get; set; }

    public virtual PhysicalCountOrder? PhysicalCount { get; set; }

    public virtual PurchaseOrder? Purchase { get; set; }

    public virtual RequisiteOrder? Requisite { get; set; }

    public virtual ReturnsOrder? Return { get; set; }

    public virtual Supply? Supplies { get; set; }
}
