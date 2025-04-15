using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.ViewModels;

namespace ThothSystemVersion1.InterfaceServices
{
    public interface AccountingService
    {
        public List<JobOrderCustEmpVM> getJObOrderWithRemainingAmount();
        public List<PaymentPurchaseOrderVM> getPurchaseOrdersWithRemainingAmount();

        public bool makeRecipt( RecieptOrderDTO recOrd);
        public bool makePayment(PaymentOrderDTO payment);

        public bool editJobOrder(int orderid ,JobOrder jobOrder);
        public bool editPurchaseOrder(int orderid, PurchaseOrder purchaseOrder);




    }
}
