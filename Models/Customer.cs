using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string? CustomerAddress { get; set; }

    public string? CustomerEmail { get; set; }

    public string? CustomerNotes { get; set; }

    public string CustomerPhone { get; set; } = null!;

    public virtual ICollection<JobOrder> JobOrders { get; set; } = new List<JobOrder>();
}
