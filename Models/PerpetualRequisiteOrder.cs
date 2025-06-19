using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;


public partial class PerpetualRequisiteOrder
{
    public int PerpetualRequisiteId { get; set; }

    public DateOnly? PerpetualRequisiteDate { get; set; }

    public string EmployeeId { get; set; } = null!;

    public string? RequisiteNotes { get; set; }

    public int MachineStoreId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual MachineStore MachineStore { get; set; } = null!;


    public virtual ICollection<QuantityBridge> QuantityBridges { get; set; } = new List<QuantityBridge>();
}
