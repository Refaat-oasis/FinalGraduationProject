using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;
public partial class ProcessBridge
{
    [Display(Name = "رقم التعريفي")]
    public int ProcessBridgeId { get; set; }

    [Display(Name = "رقم امر التصنيع")]
    public int? JobOrderId { get; set; }

    [Display(Name = "رقم الماكينة")]
    public int? MachineId { get; set; }

    [Display(Name = "رقم العملية العمالية")]
    public int? LabourId { get; set; }

    [Display(Name = "تكلفة صاعة الماكينة")]
    public decimal? MachineHourPrice { get; set; }

    [Display(Name = "قيمة عدد ساعات المكينة")]
    public decimal TotalMachineValue { get; set; }

    [Display(Name = "سعر ساعة العملية العمالية")]
    public decimal? LabourHourPrice { get; set; }

    [Display(Name = "قيمة عدد الساعات العمالية")]
    public decimal TotalLabourValue { get; set; }

    [Display(Name = "عدد الساعات")]
    public decimal NumberOfHours { get; set; }

    [Display(Name = "رقم التعريفي الخاص بالموظف")]
    public string? EmployeeId { get; set; }


    public virtual Employee? Employee { get; set; }

    public virtual JobOrder? JobOrder { get; set; }

    public virtual Labour? Labour { get; set; }

    public virtual Machine? Machine { get; set; }
}
