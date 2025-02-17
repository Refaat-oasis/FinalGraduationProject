using System;
using System.Collections.Generic;

namespace ThothSystemVersion1.Models;
public enum JobRole { 
    Admin,Inventory, Technical, Cost
}
public partial class Employee
{
    public string EmployeeId { get; set; } = null!;

    public string EmployeeUserName { get; set; } = null!;

    public string EmployeePassword { get; set; } = null!;

    public string EmployeeName { get; set; } = null!;

    public JobRole JobRole { get; set; } 

    public virtual ICollection<JobOrder> JobOrders { get; set; } = new List<JobOrder>();

    public virtual ICollection<PhysicalCountOrder> PhysicalCountOrders { get; set; } = new List<PhysicalCountOrder>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<RequisiteOrder> RequisiteOrders { get; set; } = new List<RequisiteOrder>();

    public virtual ICollection<ReturnsOrder> ReturnsOrders { get; set; } = new List<ReturnsOrder>();
}
