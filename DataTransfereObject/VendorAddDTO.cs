using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.DataTransfereObject
{
    public class VendorAddDTO
    {

        [Display(Name = "اسم التاجر")]
        [Required(ErrorMessage = "يجد عليك ادخال اسم المورد")]
        public string VendorName { get; set; } = null!;
        [Display(Name = "عنوان التاجر")]
        [Required(ErrorMessage = "يجد عليك ادخال عنوان المورد")]
        public string? VendorAddress { get; set; }
        [Display(Name = "الايميل الخاص للتاجر")]
        [EmailAddress]
        [Required(ErrorMessage = "يجد عليك ادخال عنوان المورد")]
        public string? VendorEmail { get; set; }
        [Display(Name = "ملاحظات عن التاجر")]
        public string? VendorNotes { get; set; }
        [Display(Name = "الرقم الهاتف الخاص بالتاجر")]
        [Required(ErrorMessage = "يجد عليك ادخال رقم هاتف المورد")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "رقم الهاتف يجب ان تكون بين 10 ارقام و 14 رقم")]
        public string VendorPhone { get; set; } = null!;
    }
}
