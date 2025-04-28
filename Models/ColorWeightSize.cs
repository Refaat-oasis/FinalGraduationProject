using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.Models
{
        public partial class ColorWeightSize
        {

        [Display(Name = "الرقم التعريفي ")]

        public int ColorWeightSizeId { get; set; }

        [Display(Name = "النوع")]

        public int? Type { get; set; }

        [Display(Name = "القياس")]
        public string? Size { get; set; }

        [Display(Name = "الوزن")]
        public decimal? Weight { get; set; }
        [Display(Name = "اللون")]
        public string? Colored { get; set; }
        }
  
}
