using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class JobOrder
{
    [Display(Name = "الرقم التعريفي للتشغيلة")]
    public int JobOrderId { get; set; }
    
    [Display(Name = "المبلغ المتبقي")]
    public decimal? RemainingAmount { get; set; }
    
    [Display(Name = "المبلغ المدفوع مقدما")]
    public decimal? UnearnedRevenue { get; set; }
    
    [Display(Name = "ملاحظات عن التشغيلة")]
    public string? JobOrdernotes { get; set; }
    
    [Display(Name = "المبلغ المستحق")]
    public decimal? EarnedRevenue { get; set; }
    
    [Display(Name = "مدي التقدم في التشغيلة")]
    public string? OrderProgress { get; set; }
    
    [Display(Name = "الرقم المميز للعميل")]
    public int? CustomerId { get; set; }
    
    [Display(Name = "تاريخ البدأ في التشغيلة")]
    public DateOnly? StartDate { get; set; }
    
    [Display(Name = "تاريخ انتهاء التشغيلة")]
    public DateOnly EndDate { get; set; }
    
    [Display(Name = "الرقم القومي للموظف")]
    public string? EmployeeId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<RequisiteOrder> RequisiteOrders { get; set; } = new List<RequisiteOrder>();

    public virtual ICollection<ReturnsOrder> ReturnsOrders { get; set; } = new List<ReturnsOrder>();
}
