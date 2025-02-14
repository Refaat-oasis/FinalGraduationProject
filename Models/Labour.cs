using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;

public partial class Labour
{
    public int LabourId { get; set; }

    public string LabourProcessName { get; set; } = null!;

    public decimal Price { get; set; }
}
