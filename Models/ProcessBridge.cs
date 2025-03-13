using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public partial class ProcessBridge
{
    public int? ProcessBridgeID { get; set; }

    [Display(Name = "الرقم التعريفي للتشغيلة")]
    public int? JobOrderId { get; set; }
    
    [Display(Name = "الرقم التعريفي للألة")]
    public int? MachineId { get; set; }
    
    [Display(Name = "الرقم التعريفي للعمال")]
    public int? LabourId { get; set; }
    
    [Display(Name = "جملة مصاريف الالات")]
    public decimal TotalMachinePrice { get; set; }
    
    [Display(Name = "جملة اجور العمال")]
    public decimal TotalLabourPrice { get; set; }
    
    [Display(Name = "عدد ساعات العمل و التشغيل")]
    public decimal NumberOfHours { get; set; }
    
    [Display(Name = "الرقم القومي للموظف")]
    public string? EmployeeId { get; set; }


    public virtual Employee? Employee { get; set; }

    public virtual JobOrder? JobOrder { get; set; }

    public virtual Labour? Labour { get; set; }

    public virtual Machine? Machine { get; set; }
}
