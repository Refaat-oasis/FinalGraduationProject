using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.DataTransfereObject
{
    public class SparepartsDTO
    {

        public int SparePartsId { get; set; }

        [Display(Name = "اسم قطعة الغيار")]
        [Required(ErrorMessage = "يجب ادخال اسم قطعة الغيار")]
        public string Name { get; set; } = null!;

        [Display(Name = "نقطة اعادة الطلب")]
        [Required]
        public decimal? ReorderPoint { get; set; }

        [Display(Name = "التفعيل")]
        [Required]
        public bool? Activated { get; set; }


    }
}
