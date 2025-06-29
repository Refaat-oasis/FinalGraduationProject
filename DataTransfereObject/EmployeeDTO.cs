using System.ComponentModel.DataAnnotations;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.DataTransfereObject
{
    public class EmployeeDTO

    {
        [Required(ErrorMessage = "يجب عليك ادخال الرقم القومي للموظف")]
        [StringLength(15, MinimumLength = 13, ErrorMessage = "الرقم القومي للموظف مكون من 14 رقم")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "يجب أن يحتوي الرقم القومي على أرقام فقط")]
        [Display(Name = "الرقم القومي للموظف")]
        public string EmployeeId { get; set; } = null!;


        [Display(Name = "اسم المستخدم")]
        [Required(ErrorMessage = "يجب عليك ادخال اسم المستخدم للموظف")]
        [RegularExpression("^[\\u0600-\\u06FF0-9 ]+$", ErrorMessage = "يجب أن يحتوي اسم المستخدم على حروف عربية وأرقام ومسافات فقط")]
        public string EmployeeUserName { get; set; } = null!;


        [Required(ErrorMessage = "يجب عليك ادخال كلمة السر")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "كلمة المرور يجب ان تكون بين 5 حروف او اكثر")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]+$",
        ErrorMessage = "يجب أن تحتوي كلمة المرور على حرف صغير وحرف كبير ورقم وحرف خاص (@$!%*?&)")]
        [Display(Name = "كلمة السر الخاصة")]
        public string EmployeePassword { get; set; } = null!;



        [Display(Name = "اسم الموظف")]
        [Required(ErrorMessage = "يجب عليك ادخال اسم الموظف")]
        [RegularExpression(@"^[\u0621-\u064A\s]+$", ErrorMessage = "يجب أن يحتوي اسم الموظف على حروف لغة عربية ومسافات فقط")]
        public string EmployeeName { get; set; } = null!;


        [Required(ErrorMessage = "يجب عليك اختيار الدور الوظيفي للموظف")]
        [Display(Name = "الدور الوظيفي")]
        public JobRole JobRole { get; set; }

        [Display(Name = "حالة الحساب للموظف")]
        public bool Activated { get; set; }

    }
}
