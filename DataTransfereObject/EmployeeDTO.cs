using System.ComponentModel.DataAnnotations;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.DataTransfereObject
{
    public class EmployeeDTO
        
    {
        [Required(ErrorMessage ="يجب عليك ادخال الرقم القومي للموظف")]
        [StringLength(15,MinimumLength =13, ErrorMessage ="الرقم القومي للموظف مكون من ")]
        [Display(Name = "الرقم القومي للموظف")]
        public string EmployeeId { get; set; } = null!;


        [Display(Name = "اسم المستخدم")]
        [Required(ErrorMessage ="يجب عليك ادخال اسم المستخد للموظف")]
        public string EmployeeUserName { get; set; } = null!;

        [Required(ErrorMessage ="يجب عليك ادخال كلمة السر")]
        [StringLength(50,MinimumLength =5 , ErrorMessage ="كلمة المرور يجب ان تكون بين 5 حروف او اكثر")]
        [Display(Name = "كلمة السر الخاصة")]
        public string EmployeePassword { get; set; } = null!;


        [Display(Name = "اسم الموظف")]
        [Required (ErrorMessage ="يجد عليك ادخال اسم الموظف")]
        public string EmployeeName { get; set; } = null!;


        [Required(ErrorMessage ="يجب عليك اختيار الدور الوظيفي للموظف")]
        [Display(Name = "الدور الوظيفي")]
        public JobRole JobRole { get; set; }

        [Required (ErrorMessage ="يجب عليك اختيار حالة الحساب للموظف")]
        [Display(Name = "حالة الحساب للموظف")]
        public bool Activated { get; set; }

    }
}
