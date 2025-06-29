using System.ComponentModel.DataAnnotations;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.DataTransfereObject
{
    public class EmployeeEditDTO
    {

        public string EmployeeId { get; set; } = null!;


        [Display(Name = "اسم المستخدم")]
        [Required(ErrorMessage = "يجب عليك ادخال اسم المستخدم للموظف")]
        [RegularExpression("^[\\u0600-\\u06FF0-9 ]+$", ErrorMessage = "يجب أن يحتوي اسم المستخدم على حروف عربية وأرقام ومسافات فقط")]
        public string EmployeeUserName { get; set; } = null!;

        [Display(Name = "اسم الموظف")]
        [Required(ErrorMessage = "يجب عليك ادخال اسم الموظف")]
        [RegularExpression(@"^[\u0621-\u064A\s]+$", ErrorMessage = "يجب أن يحتوي اسم الموظف على حروف لغة عربية ومسافات فقط")]
        public string EmployeeName { get; set; } = null!;

        [Display(Name = "حالة الحساب للموظف")]
        public bool Activated { get; set; }

        [Display(Name = "كلمة المرور")]
        public string? EmployeePassword { get; set; } = null!;
    }
}