using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;

public partial class MachineStore
{
    public int MachineStoreId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Activated { get; set; }

    public virtual ICollection<QuantityBridge> QuantityBridges { get; set; } = new List<QuantityBridge>();
}
