using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;

public partial class Vendor
{
    public int VendorId { get; set; }

    public string VendorName { get; set; } = null!;

    public string? VendorAddress { get; set; }

    public string? VendorEmail { get; set; }

    public string? VendorNotes { get; set; }

    public string VendorPhone { get; set; } = null!;

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
