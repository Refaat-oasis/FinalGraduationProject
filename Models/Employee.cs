using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;
public enum JobRole { 

    [Display(Name="المدير")]Admin,
    [Display(Name="موظف المخازن")]Inventory,
    [Display(Name="الموظف الفني")]Technical,
    [Display(Name="موظف التكاليف")]Cost
}
public partial class Employee
{
    [Display(Name = "الرقم القومي للموظف")]
    public string EmployeeId { get; set; } = null!;
    [Display(Name = "اسم المستخدم")]
    public string EmployeeUserName { get; set; } = null!;
    [Display(Name = "كلمة السر الخاصة")]
    public string EmployeePassword { get; set; } = null!;
    [Display(Name = "اسم الموظف")]
    public string EmployeeName { get; set; } = null!;
    [Display(Name = "الدور الوظيفي")]
    public JobRole JobRole { get; set; }
    [Display(Name = "حالة الحساب للموظف")]
    public bool Activated { get; set; }


    public virtual ICollection<JobOrder> JobOrders { get; set; } = new List<JobOrder>();

    public virtual ICollection<PhysicalCountOrder> PhysicalCountOrders { get; set; } = new List<PhysicalCountOrder>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<RequisiteOrder> RequisiteOrders { get; set; } = new List<RequisiteOrder>();

    public virtual ICollection<ReturnsOrder> ReturnsOrders { get; set; } = new List<ReturnsOrder>();
}
