using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.DataTransfereObject
{
    public class VendorAddDTO
    {

        [Display(Name = "اسم التاجر")]
        [Required(ErrorMessage = "يجب عليك إدخال اسم المورد")]
        [RegularExpression(@"^[\u0600-\u06FF\s]{2,}$", ErrorMessage = "يجب أن يحتوي اسم المورد على حروف عربية ومسافات فقط")]
        public string VendorName { get; set; } = null!;


        [Required(ErrorMessage = "يجب عليك ادخال عنوان المورد")]
        [Display(Name = "عنوان التاجر")]
        [RegularExpression(@"^[\u0600-\u06FF0-9\u0660-\u0669\s\-.,#،]+$", ErrorMessage = "يجب كتابة العنوان باللغة العربية مع السماح بالأرقام وعلامات الترقيم")]
        public string? VendorAddress { get; set; }



        [Display(Name = "الايميل الخاص للتاجر")]
        [Required(ErrorMessage = "يجب عليك إدخال البريد الإلكتروني الخاص بالمورد")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة")]
        public string? VendorEmail { get; set; }

        [Display(Name = "ملاحظات عن التاجر")]
        public string? VendorNotes { get; set; }

        [Display(Name = "الرقم الهاتف الخاص بالتاجر")]
        [Required(ErrorMessage = "يجب عليك إدخال رقم هاتف المورد")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "رقم الهاتف يجب أن يكون 11 رقمًا")]
        [RegularExpression(@"^(?:\+20|0020|0)?1[0125][0-9]{8}$", ErrorMessage = "يجب إدخال رقم هاتف صحيح مثل 010xxxxxxxx أو +2010xxxxxxxx")]
        public string VendorPhone { get; set; } = null!;
    }
}
