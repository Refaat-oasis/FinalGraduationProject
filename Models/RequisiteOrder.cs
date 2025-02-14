using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;

public partial class RequisiteOrder
{
    public int RequisiteId { get; set; }

    public DateOnly? RequisiteDate { get; set; }

    public string EmployeeId { get; set; } = null!;

    public int JobOrderId { get; set; }

    public string? RequisiteNotes { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual JobOrder JobOrder { get; set; } = null!;
}
