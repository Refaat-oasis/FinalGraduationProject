using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.ViewModels
{
    public class InventoryReportViewModel
    {
        // list of the types of the orders that will be displayed in the inventory report

        public List<PurchaseOrder> purchaseOrderList {get; set;}

        public List<RequisiteOrder> requisiteOrderList {get; set;}

        public List<ReturnsOrder>  returnOrderList { get; set;}

        public List<PhysicalCountOrder> physicalCountlist { get; set; }

        // list of the items in the quantity bridge table

        public List<QuantityBridge> quantityBridgeList {get; set;}

        public List<(Vendor Vendor, int PurchaseCount, decimal TotalOldBalance)> VendorReport { get; set; }

        public List<(Customer Customer, int OrderCount, decimal TotalBalance , decimal unearnedBalance , decimal RemainingBalance)> CustomerReport { get; set; } = new();




        // every item in the purchase order must have one elemt in the quantity bridge that look similar
        // to the item in the purchase order
        // we loop the purchase order according to the condition and then the quantity bridge


    }
}
