using Microsoft.AspNetCore.Mvc;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.ViewModels;

namespace ThothSystemVersion1.Controllers
{
    public class CostController : Controller
    {
        private readonly CostBusinessLogicLayer _businessLogicL;
        private readonly TechnicalBusinessLogicLayer _techBusinessLogiclayer;

        public CostController(CostBusinessLogicLayer businessLogicLayer,
            TechnicalBusinessLogicLayer businessforTechnical)
        {
            _techBusinessLogiclayer = businessforTechnical;
            _businessLogicL = businessLogicLayer;
        }


        // Refaat section 

        public IActionResult viewAlljobOrder()
        {
            List<JobOrderCustEmpVM> ListJobOrder = _techBusinessLogiclayer.ViewAllJobOrder();

            return View("~/views/Cost/viewAlljobOrder.cshtml" , ListJobOrder);
        }

        public IActionResult addMachineAndLabourExpense(int JobOrderId) {

            return View("~/views/Cost/addMachineAndLabourExpense.cshtml");


        }



/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Mariam section



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Sandra section




        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // sherwet section





    }
}
