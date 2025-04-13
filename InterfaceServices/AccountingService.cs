using ThothSystemVersion1.Models;
using ThothSystemVersion1.ViewModels;

namespace ThothSystemVersion1.InterfaceServices
{
    public interface AccountingService
    {
        public List<JobOrderCustEmpVM> getJObOrderWithRemainingAmount();
        public List<PurchaseOrderEmpVendVm> getPurchaseOrdersWithRemainingAmount();

        public bool makeRecipt();
        public bool makePayment();

        public bool editJobOrder(int orderid ,JobOrder jobOrder);
        public bool editPurchaseOrder(int orderid, PurchaseOrder purchaseOrder);




    }
}
