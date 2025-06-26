using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.ModifiedModels
{
    public class ModifiedQuantityBridge
    {
        [Display(Name = "الرقم التعريفي")]
        public int? QuantityBridgeId { get; set; }

        [Display(Name = "رقم امر المرتجع")]
        public int? ReturnId { get; set; }

        [Display(Name = "رقم امر الشراء")]
        public int? PurchaseId { get; set; }


        [Display(Name = "رقم امر الجرد")]
        public int? PhysicalCountId { get; set; }

        [Display(Name = "رقم امر الصرف الدائم")]
        public int? PerpetualRequisiteId { get; set; }



        [Display(Name = "رقم امر الصرف")]
        public int? RequisiteId { get; set; }

        [Display(Name = "الرقم التعريفي الخاص بالورق")]
        public int? PaperId { get; set; }
        public string PaperName { get; set; }

        [Display(Name = "الرقم التعريفي الخاص بالحبر")]
        public int? InkId { get; set; }
        public string InkName { get; set; }

        [Display(Name = "الرقم التعريفي الخاص بالمستلزمات")]
        public int? SuppliesId { get; set; }
        public string SuppliesName { get; set; }

        [Display(Name = "الرقم التعريفي الخاص بقطع الغيار")]
        public int? SparePartsId { get; set; }


        [Display(Name = "اسم العنصر")]
        public string Name { get; set; } = null!;


        [Display(Name = "السعر")]
        public decimal Price { get; set; }

        [Display(Name = "الكمية")]
        public int Quantity { get; set; }

        [Display(Name = "الوحدات")]
        public int NumberOfUnits { get; set; }

        [Display(Name = "سعر الوحدة")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "عدد الوحدات القديمة")]
        public int? OldNumberOfUnits { get; set; }

        [Display(Name = "القيمة")]
        public decimal? TotalBalance { get; set; }

        [Display(Name = "القيمة القديمة")]
        public int? OldQuantity { get; set; }

        [Display(Name = "السعر القديم")]
        public decimal? OldPrice { get; set; }

        [Display(Name = "القيمة القديمة")]
        public decimal? OldTotalBalance { get; set; }




    }
}
