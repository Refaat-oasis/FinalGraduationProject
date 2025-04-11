using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;
public partial class Vendor
{
    [Display(Name = "اسم المعرف للمورد")]
    public int VendorId { get; set; }

    [Display(Name = "اسم المورد")]
    public string VendorName { get; set; } = null!;

    [Display(Name = "عنوان المورد")]
    public string? VendorAddress { get; set; }

    [Display(Name = "الايميل الخاص للمورد")]
    public string? VendorEmail { get; set; }

    [Display(Name = "ملاحظات")]
    public string? VendorNotes { get; set; }

    [Display(Name = "رقم المورد")]
    public string VendorPhone { get; set; } = null!;

    [Display(Name = "التفعيل")]
    public bool Activated { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
