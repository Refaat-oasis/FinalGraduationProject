using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;

public partial class JobOrder
{
    public int JobOrderId { get; set; }

    public decimal? RemainingAmount { get; set; }

    public decimal? UnearnedRevenue { get; set; }

    public string? JobOrdernotes { get; set; }

    public decimal? EarnedRevenue { get; set; }

    public string? OrderProgress { get; set; }

    public int? CustomerId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string? EmployeeId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<RequisiteOrder> RequisiteOrders { get; set; } = new List<RequisiteOrder>();

    public virtual ICollection<ReturnsOrder> ReturnsOrders { get; set; } = new List<ReturnsOrder>();
}
