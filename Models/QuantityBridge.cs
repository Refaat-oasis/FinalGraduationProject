using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;

public partial class QuantityBridge
{
    public decimal Price { get; set; }

    public int? ReturnId { get; set; }

    public int? PurchaseId { get; set; }

    public int Quantity { get; set; }

    public int? RequisiteId { get; set; }

    public int? PaperId { get; set; }

    public int? InkId { get; set; }

    public int? SuppliesId { get; set; }

    public int? PhysicalCountId { get; set; }

    public virtual Ink? Ink { get; set; }

    public virtual Paper? Paper { get; set; }

    public virtual PhysicalCountOrder? PhysicalCount { get; set; }

    public virtual PurchaseOrder? Purchase { get; set; }

    public virtual RequisiteOrder? Requisite { get; set; }

    public virtual ReturnsOrder? Return { get; set; }

    public virtual Supply? Supplies { get; set; }
}
