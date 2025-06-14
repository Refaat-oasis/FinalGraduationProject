using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models;

public enum JobRole
{
    // 0 big man
    [Display(Name = "المدير")] Admin,

    // 1 edit and view and add
    [Display(Name = "مدير المخازن")] InventoryManager,
    // 2 add and view
    [Display(Name = "موظف المخازن")] InventoryClerk,

    // 3 view and add and edit
    [Display(Name = "مدير الفني")] TechnicalManager,
    // 4 view and add   
    [Display(Name = "الموظف الفني")] TechnicalClerk,

    // 5 view and add and edit
    [Display(Name = "مدير التكاليف")] CostManager,
    // 6 view and add
    [Display(Name = "موظف التكاليف")] CostClerk,

    // 7 view and add and edit
    [Display(Name = "موظف التكاليف")] AcoountingManager,
    // 8 view and add
    [Display(Name = "موظف التكاليف")] AccountingClerk,
}
public partial class Employee
{
    [Display(Name = "الرقم التعريفي الخاص بالموظف")]
    public string EmployeeId { get; set; } = null!;

    [Display(Name = "اسم المستخدم")]
    public string EmployeeUserName { get; set; } = null!;

    [Display(Name = "كلمة المرور")]
    public string EmployeePassword { get; set; } = null!;

    [Display(Name = "اسم الموظف")]
    public string EmployeeName { get; set; } = null!;

    [Display(Name = "المسمي الوظيفي")]
    public JobRole JobRole { get; set; }

    [Display(Name = "التفعيل")]
    public bool Activated { get; set; }

    
    public bool? Forgetpassword { get; set; }


    public virtual ICollection<JobOrder> JobOrders { get; set; } = new List<JobOrder>();

    public virtual ICollection<MiscellaneousExpense> MiscellaneousExpenses { get; set; } = new List<MiscellaneousExpense>();

    public virtual ICollection<PaymentOrder> PaymentOrders { get; set; } = new List<PaymentOrder>();

    public virtual ICollection<PhysicalCountOrder> PhysicalCountOrders { get; set; } = new List<PhysicalCountOrder>();

    public virtual ICollection<ProcessBridge> ProcessBridges { get; set; } = new List<ProcessBridge>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<RecieptsOrder> RecieptsOrders { get; set; } = new List<RecieptsOrder>();

    public virtual ICollection<RequisiteOrder> RequisiteOrders { get; set; } = new List<RequisiteOrder>();

    public virtual ICollection<ReturnsOrder> ReturnsOrders { get; set; } = new List<ReturnsOrder>();

    public virtual ICollection<PerpetualRequisiteOrder> PerpetualRequisiteOrders { get; set; } = new List<PerpetualRequisiteOrder>();

}
