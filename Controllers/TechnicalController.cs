using Microsoft.AspNetCore.Mvc;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.ViewModels;

namespace ThothSystemVersion1.Controllers
{
    public class TechnicalController : Controller
    {

        private readonly TechnicalBusinessLogicLayer _businessLogicLayer;

        public TechnicalController(TechnicalBusinessLogicLayer _technicalBussinessLogicLayer) {

            _businessLogicLayer = _technicalBussinessLogicLayer; 
        
        }

        [HttpGet]
        public IActionResult ViewAllJobOrder()
        {
            List<JobOrderCustEmpVM> jobOrderCustomerViewModelsList = _businessLogicLayer.ViewAllJobOrder();
            return View("~/Views/technical/ViewAlljobOrder.cshtml", jobOrderCustomerViewModelsList);

        }
    }
}
