using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;
public partial class RecieptsOrder
{
    [Display(Name = "الرقم الخاص باستلام الاموال")]
    public int RecieptId { get; set; }

    [Display(Name = "الرقم الخاص بعملية التصنيع")]
    public int? JobOrderId { get; set; }

    [Display(Name = "المبلغ")]
    public decimal? Amount { get; set; }

    [Display(Name = "تاريخ الاستلام")]
    public DateOnly? ReceiptDate { get; set; }

    [Display(Name = "الرقما لتعريفي الخاص بالموظف")]
    public string? EmployeeId { get; set; }

    [Display(Name = "ملاحظات")]
    public string? ReceiptNotes { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual JobOrder? JobOrder { get; set; }
}
