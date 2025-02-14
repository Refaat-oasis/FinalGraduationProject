using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;

public partial class Machine
{
    public int MachineId { get; set; }

    public string MachineProcessName { get; set; } = null!;

    public decimal Price { get; set; }
}
