﻿using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.ModifiedModels
{
    public class ModifiedPhysicalCountOrder
    {

        [Display(Name = "رقم امر المرتجع")]
        public int PhysicalCountId { get; set; }

        [Display(Name = "الرقم التعريفي الخاص بالموظف")]
        public string? EmployeeId { get; set; }

        [Display(Name = "اسم الموظف")]
        public string? EmployeeName { get; set; }

        [Display(Name = "تاريخ الجرد")]
        public DateOnly? PhysicalCountDate { get; set; }

        [Display(Name = "ملاحظات")]
        public string? PhysicalCountNotes { get; set; }

        [Display(Name = "كمية العنصر")]
        public int balance { get; set; }

        [Display(Name = "السعر")]
        public decimal price { get; set; }

        [Display(Name = "كمية قديمة للعنصر")]
        public int oldBalance { get; set; }



    }
}
