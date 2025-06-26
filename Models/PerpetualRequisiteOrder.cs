using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;


public partial class PerpetualRequisiteOrder
{
    [Display(Name = "رقم امر الصرف الدائم")]
    public int PerpetualRequisiteId { get; set; }
    [Display(Name = "تاريخ امر الصرف الدائم")]
    public DateOnly? PerpetualRequisiteDate { get; set; }
    [Display(Name = "رقم القومي للموظف")]
    public string EmployeeId { get; set; } = null!;
    [Display(Name = "ملاحظات")]
    public string? RequisiteNotes { get; set; }
    [Display(Name = "رقم الالة")]
    public int MachineStoreId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual MachineStore MachineStore { get; set; } = null!;



    public virtual ICollection<QuantityBridge> QuantityBridges { get; set; } = new List<QuantityBridge>();
}
