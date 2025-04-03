using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;
public enum JobRole { 
    // 0 big man
    [Display(Name="المدير")]Admin,
    
    // 1 edit and view and add
    [Display(Name = "مدير المخازن")] InventoryManager,
    // 2 add and view
    [Display(Name="موظف المخازن")]InventoryClerk,

    // 3 view and add and edit
    [Display(Name = "مدير الفني")] TechnicalManager,
    // 4 view and add   
    [Display(Name = "الموظف الفني")] TechnicalClerk,
    
    // 5 view and add and edit
    [Display(Name = "مدير التكاليف")] CostManager,
    // 6 view and add
    [Display(Name = "موظف التكاليف")] CostClerk
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
