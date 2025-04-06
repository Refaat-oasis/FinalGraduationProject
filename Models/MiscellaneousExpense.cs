using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class MiscellaneousExpense
{
    public int? MiscellaneousExpensesID { get; set; }

    [Display(Name = "الرقم التعريفي الخاص بالتشغيلة")]
    public int? JobOrderId { get; set; }

    [Display(Name = "الرقم التعرفي الخاص بالعميل")]
    public string? EmployeeId { get; set; }

    [Display(Name = "مصاريف تشغيل الالات")]
    public decimal? MaterialProcessingExpense { get; set; }

    [Display(Name = "مصاريف تشغيل الافلام")]
    public decimal? FilmsProcessingExpense { get; set; }

    [Display(Name = "جملة الخامات")]
    public decimal? MaterialsTotal { get; set; }

    [Display(Name = "الاجمالي")]
    public decimal TotalAfterMaterials { get; set; }

    [Display(Name = "مصاريف ادارية")]
    public decimal? AdminstrativeExpense { get; set; }

    [Display(Name = "احمالي المصروفات")]
    public decimal TotalExpenses { get; set; }

    [Display(Name = "النسبة")]
    public decimal Percentage { get; set; }

    [Display(Name = "الجملة")]
    public decimal TotalAfterPercentage { get; set; }

    [Display(Name = "وزارة المالية")]
    public decimal MinistryOfFinance { get; set; }

    [Display(Name = "صندوق تحسين العاملين")]
    public decimal EmployeeImprovmentBox { get; set; }

    [Display(Name = "ضريبة القيمة المضافة")]
    public decimal ValueAddedTax { get; set; }

    [Display(Name = "القيمة الاجمالية")]
    public decimal FinalTotal { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual JobOrder? JobOrder { get; set; }
}
