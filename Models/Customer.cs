using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class Customer
{
    [Display ( Name ="الرقم التعريفي للعميل")]
    public int CustomerId { get; set; }
    
    [Display(Name ="اسم العميل")]
    public string CustomerName { get; set; } = null!;

    [Display(Name = "عنوان العميل")]
    public string? CustomerAddress { get; set; }

    [Display(Name = "الايميل الخاص بالعميل")]
    public string? CustomerEmail { get; set; }

    [Display(Name = "ملاحظات")]
    public string? CustomerNotes { get; set; }

    [Display(Name = "رقم العميل")]
    public string CustomerPhone { get; set; } = null!;

    [Display(Name = "التفعيل")]
    public bool Activated { get; set; }

    public virtual ICollection<JobOrder> JobOrders { get; set; } = new List<JobOrder>();
}
