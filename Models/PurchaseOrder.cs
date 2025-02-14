using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;

public partial class PurchaseOrder
{
    public int PurchaseId { get; set; }

    public DateOnly? PurchaseDate { get; set; }

    public string EmployeeId { get; set; } = null!;

    public int VendorId { get; set; }

    public string? PurchaseNotes { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Vendor Vendor { get; set; } = null!;
}
