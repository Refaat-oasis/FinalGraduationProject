using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;

public partial class ReturnsOrder
{
    public int ReturnId { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public int JobOrderId { get; set; }

    public string EmployeeId { get; set; } = null!;

    public string? ReturnsNotes { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual JobOrder JobOrder { get; set; } = null!;
}
