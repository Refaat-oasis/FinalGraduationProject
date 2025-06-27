using ThothSystemVersion1.Models;
using ThothSystemVersion1.ModifiedModels;

namespace ThothSystemVersion1.ViewModels
{
    public class InventoryReportViewModel
    {
        // list of the types of the orders that will be displayed in the inventory report

        public List<PurchaseOrder> purchaseOrderList { get; set; }

        public List<RequisiteOrder> requisiteOrderList { get; set; }

        public List<ReturnsOrder> returnOrderList { get; set; }

        public List<PhysicalCountOrder> physicalCountList { get; set; }

        public List<PerpetualRequisiteOrder> perpetualOrderList { get; set; }

        // list of the items in the quantity bridge table

        public List<QuantityBridge> quantityBridgeList { get; set; }

        public List<ModifiedPhysicalCountOrder> modifiedPhysicalCountOrderList { get; set; }
        public List<ModifiedPurchaseOrder> modifiedPurchaseOrderList { get; set; }
        public List<ModifiedQuantityBridge> modifiedQuantityBridges { get; set; }
        public List<ModifiedRequisiteOrder> modifiedRequisiteOrderList { get; set; }
        public List<ModifiedReturnsOrder> modifiedReturnsOrderList { get; set; }
        public List<ModifiedPerpetualRequisiteOrder> modifiedPerpetualRequisiteOrdersList { get; set; }


        // item data for inventory item 

        public string itemName { get; set; }
        public string itemType { get; set; }
        public int itemQuantity { get; set; }
        public decimal itemPrice { get; set; }
        public decimal itemTotalBalance { get; set; }


        public List<(Vendor Vendor, int PurchaseCount, decimal TotalOldBalance)> VendorReport { get; set; }

        public List<(Customer Customer, int OrderCount, decimal TotalBalance, decimal unearnedBalance, decimal RemainingBalance)> CustomerReport { get; set; } = new();


    }
}
