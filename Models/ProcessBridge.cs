using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;

public partial class ProcessBridge
{
    public int? JobOrderId { get; set; }

    public int? MachineId { get; set; }

    public int? LabourId { get; set; }

    public decimal TotalMachinePrice { get; set; }

    public decimal TotalLabourPrice { get; set; }

    public decimal NumberOfHours { get; set; }

    public string? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual JobOrder? JobOrder { get; set; }

    public virtual Labour? Labour { get; set; }

    public virtual Machine? Machine { get; set; }
}
