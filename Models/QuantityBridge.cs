using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class QuantityBridge
{
    //[Display(Name = "الرقم التعريفي لامر الشراء")]
    public int? QuantityBridgeID { get; set; }

    [Display(Name = "السعر")]
    public decimal Price { get; set; }
    
    [Display(Name = "الرقم التعريفي لاذن المردودات")]
    public int? ReturnId { get; set; }
    
    [Display(Name = "الرقم التعريفي لامر الشراء")]
    public int? PurchaseId { get; set; }
    
    [Display(Name = "الكمية")]
    public int Quantity { get; set; }
    
    [Display(Name = "اذن الصرف")]
    public int? RequisiteId { get; set; }
    
    [Display(Name = "الرقم التعريفي للورق")]
    public int? PaperId { get; set; }
    
    [Display(Name = "الرقم التريفي للحبر")]
    public int? InkId { get; set; }
    
    [Display(Name = "الرقم التعريفي للمستلزمات")]
    public int? SuppliesId { get; set; }
    
    [Display(Name = "الرقم التعريفي للجرد")]
    public int? PhysicalCountId { get; set; }

    [Display(Name ="الكمية القديمة")]
    public int? OldQuantity { get; set; }

    [Display(Name = "السعر القديم")]
    public decimal? OldPrice { get; set; }

    [Display(Name = "القيمة القديمة")]
    public decimal? OldTotalBalance { get; set; }

    [Display(Name = "القيمة الحالية")]
    public decimal? TotalBalance { get; set; }


    public virtual Ink? Ink { get; set; }

    public virtual Paper? Paper { get; set; }

    public virtual PhysicalCountOrder? PhysicalCount { get; set; }

    public virtual PurchaseOrder? Purchase { get; set; }

    public virtual RequisiteOrder? Requisite { get; set; }

    public virtual ReturnsOrder? Return { get; set; }

    public virtual Supply? Supplies { get; set; }
}
