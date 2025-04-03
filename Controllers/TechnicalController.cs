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
        // refaat section
        [HttpGet]
        public IActionResult ViewAllJobOrder()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 3 || jobRole == 4)
            {

            List<JobOrderCustEmpVM> jobOrderCustomerViewModelsList = _businessLogicLayer.ViewAllJobOrder();
            return View("~/Views/technical/ViewAlljobOrder.cshtml", jobOrderCustomerViewModelsList);

            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // sherwet section


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // sandra section

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // mariam section


    }
}
