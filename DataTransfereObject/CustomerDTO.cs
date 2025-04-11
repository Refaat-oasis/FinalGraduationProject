using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.DataTransfereObject
{
    public class CustomerDTO
    {
      
        [Display(Name = "اسم العميل")]
        [Required(ErrorMessage = "يجد عليك ادخال اسم العميل")]
        public string CustomerName { get; set; } = null!;
        
        [Display(Name = "عنوان العميل")]
        [Required(ErrorMessage = "يجد عليك ادخال عنوان العميل")]
        public string? CustomerAddress { get; set; }
        
        [Display(Name = "الايميل الخاص بالعميل")]
        [EmailAddress]
        [Required(ErrorMessage = "يجد عليك ادخال عنوان العميل")]
        public string? CustomerEmail { get; set; }
        
        [Display(Name = "ملاحظات عن العميل")]
        public string? CustomerNotes { get; set; }

        [Display(Name = "رقم الهاتف الخاص بالعميل")]
        [Required(ErrorMessage = "يجد عليك ادخال رقم هاتف العميل ")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "رقم الهاتف يجب ان تكون بين 10 ارقام و 14 رقم")]
        public string CustomerPhone { get; set; } = null!;
        [Display(Name = "حالة الحساب للعميل")]
        public bool Activated { get; set; }

    }
}
