using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class Vendor
{
    [Display(Name = "الرقم التعريفي للتاجر")]
    public int VendorId { get; set; }

    [Display(Name = "اسم التاجر")]
    public string VendorName { get; set; } = null!;
    
    [Display(Name = "عنوان التاجر")]
    public string? VendorAddress { get; set; }
    
    [Display(Name = "الايميل الخاص للتاجر")]
    public string? VendorEmail { get; set; }
    
    [Display(Name = "ملاحظات عن التاجر")]
    public string? VendorNotes { get; set; }
    
    [Display(Name = "الرقم الهاتف الخاص بالتاجر")]
    public string VendorPhone { get; set; } = null!;

    [Display(Name = "حالة الحساب للتاجر")]
    public bool Activated { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
