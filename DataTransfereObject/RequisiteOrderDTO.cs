// DataTransfereObject/RequisiteOrderDTO.cs
using System.ComponentModel.DataAnnotations;

namespace ThothSystemVersion1.DataTransfereObject
{
    public class RequisiteOrderDTO
    {
        [Required(ErrorMessage = "Employee ID is required")]
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "Job order is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid job order")]
        public int JobOrderId { get; set; }

        public string? RequisiteNotes { get; set; }

        [Required(ErrorMessage = "At least one item is required")]
        [MinLength(1, ErrorMessage = "Add at least one item")]
        public List<RequisitionItem> Items { get; set; } = new List<RequisitionItem>();

        public class RequisitionItem
        {
            [Required(ErrorMessage = "Item type is required")]
            public string Type { get; set; } // "Paper", "Ink", "Supply"

            [Required(ErrorMessage = "Item is required")]
            [Range(1, int.MaxValue, ErrorMessage = "Invalid item")]
            public int ItemId { get; set; }

            [Required(ErrorMessage = "Quantity is required")]
            [Range(1, int.MaxValue, ErrorMessage = "Quantity must be positive")]
            public int Quantity { get; set; }
        }
    }
}





