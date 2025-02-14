using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;

public partial class MiscellaneousExpense
{
    public int? JobOrderId { get; set; }

    public string? EmployeeId { get; set; }

    public decimal? MaterialProcessingExpense { get; set; }

    public decimal? FilmsProcessingExpense { get; set; }

    public decimal? MaterialsTotal { get; set; }

    public decimal TotalAfterMaterials { get; set; }

    public decimal? AdminstrativeExpense { get; set; }

    public decimal TotalExpenses { get; set; }

    public decimal Percentage { get; set; }

    public decimal TotalAfterPercentage { get; set; }

    public decimal MinistryOfFinance { get; set; }

    public decimal EmployeeImprovmentBox { get; set; }

    public decimal ValueAddedTax { get; set; }

    public decimal FinalTotal { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual JobOrder? JobOrder { get; set; }
}
