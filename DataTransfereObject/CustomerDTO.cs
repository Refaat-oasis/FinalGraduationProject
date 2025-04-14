using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.DataTransfereObject
{
    public class CustomerDTO
    {

        [Display(Name = "الرقم التعريفي للعميل")]
        public int CustomerId { get; set; }

        [Display(Name = "اسم العميل")]
        [Required(ErrorMessage = "يجد عليك ادخال اسم العميل")]
        [RegularExpression(@"^[\u0600-\u06FF\s]{2,}$", ErrorMessage = "يجب أن يحتوي اسم المورد على حروف عربية ومسافات فقط")]
        public string? CustomerName { get; set; }

        [Display(Name = "عنوان العميل")]
        [Required(ErrorMessage = "يجب عليك ادخال عنوان العميل")]
        [RegularExpression(@"^[\u0621-\u064A0-9\s]+$",
    ErrorMessage = "يجب كتابة العنوان باستخدام الحروف العربية والأرقام والمسافات فقط، مثل: المحافظة المدينة العنوان بالتفصيل")]
        public string? CustomerAddress { get; set; }

        [Display(Name = "الايميل الخاص بالعميل")]
        [Required(ErrorMessage = "يجب عليك إدخال البريد الإلكتروني الخاص بالعميل")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة")]
        public string? CustomerEmail { get; set; }

        [Display(Name = "ملاحظات عن العميل")]
        public string? CustomerNotes { get; set; }

        [Display(Name = "الرقم الهاتف الخاص بالتاجر")]
        [Required(ErrorMessage = "يجب عليك إدخال رقم هاتف المورد")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "رقم الهاتف يجب أن يكون 11 رقمًا")]
        [RegularExpression(@"^(?:\+20|0020|0)?1[0125][0-9]{8}$", ErrorMessage = "يجب إدخال رقم هاتف صحيح مثل 010xxxxxxxx أو +2010xxxxxxxx")]
        public string CustomerPhone { get; set; } = null!;
        [Display(Name = "التفعيل")]
        public bool? Activated { get; set; }
    }
}
