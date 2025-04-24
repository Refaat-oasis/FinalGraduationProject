using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class JobOrder
{
    [Display(Name = "رقم امر التصنيع")]
    public int JobOrderId { get; set; }

    [Display(Name = "المبلغ المتبقي")]
    public decimal? RemainingAmount { get; set; }

    [Display(Name = "المبلغ الغير مستحق")]
    public decimal? UnearnedRevenue { get; set; }

    [Display(Name = "ملاحظات")]
    public string? JobOrdernotes { get; set; }

    [Display(Name = "مبلغ مستحق")]
    public decimal? EarnedRevenue { get; set; }

    [Display(Name = "مرحلة التقدم")]
    public string? OrderProgress { get; set; }

    [Display(Name = "العميل")]
    public int? CustomerId { get; set; }

    [Display(Name = "تاريخ البدء")]
    public DateOnly? StartDate { get; set; }

    [Display(Name = "تاريخ الانتهاء")]
    public DateOnly EndDate { get; set; }

    [Display(Name = "الرقم التعريفي الخاص بالموظف")]
    public string? EmployeeId { get; set; }

    [Display(Name ="مصدر امر الطلبية")]
    public string? JobOrderSource { get; set; }


    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<MiscellaneousExpense> MiscellaneousExpenses { get; set; } = new List<MiscellaneousExpense>();

    public virtual ICollection<ProcessBridge> ProcessBridges { get; set; } = new List<ProcessBridge>();

    public virtual ICollection<RecieptsOrder> RecieptsOrders { get; set; } = new List<RecieptsOrder>();

    public virtual ICollection<RequisiteOrder> RequisiteOrders { get; set; } = new List<RequisiteOrder>();

    public virtual ICollection<ReturnsOrder> ReturnsOrders { get; set; } = new List<ReturnsOrder>();
}
