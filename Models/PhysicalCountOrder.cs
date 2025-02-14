using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;

public partial class PhysicalCountOrder
{
    public int PhysicalCountId { get; set; }

    public string? EmployeeId { get; set; }

    public DateOnly? PhysicalCountDate { get; set; }

    public string? PhysicalCountNotes { get; set; }

    public virtual Employee? Employee { get; set; }
}
