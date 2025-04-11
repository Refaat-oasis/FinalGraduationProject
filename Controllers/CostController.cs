using Microsoft.AspNetCore.Mvc;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.ViewModels;

namespace ThothSystemVersion1.Controllers
{
    public class CostController : Controller
    {
        private readonly CostBusinessLogicLayer _costbusinessLogicL;
        private readonly TechnicalBusinessLogicLayer _techBusinessLogiclayer;
        private readonly InventoryBussinesLogicLayer _inventoryBusinessLogicLayer;

        public CostController(CostBusinessLogicLayer businessLogicLayer,
            TechnicalBusinessLogicLayer businessforTechnical,
            InventoryBussinesLogicLayer inventoryBusinessLogicLayer)
        {
            _techBusinessLogiclayer = businessforTechnical;
            _costbusinessLogicL = businessLogicLayer;
            _inventoryBusinessLogicLayer = inventoryBusinessLogicLayer;
        }


        // Refaat section 

        public IActionResult viewAlljobOrder()
        {

            List<JobOrderCustEmpVM> jobOrderList = _techBusinessLogiclayer.GetJobOrdersWithoutProcessBridge();

            return View("~/views/Cost/viewAlljobOrder.cshtml", jobOrderList);
        }
        [HttpGet]
        public IActionResult addMachineAndLabourExpense(int JobOrderId)
        {
            // Get the job order details
            JobOrder jobOrder = _techBusinessLogiclayer.GetJobOrderByID(JobOrderId);

            MachineLabourDTO machLabDto = new MachineLabourDTO();
            machLabDto.JobOrderId = jobOrder.JobOrderId;
            machLabDto.CustomerId = jobOrder.CustomerId;
            machLabDto.StartDate = jobOrder.StartDate;
            machLabDto.EndDate = jobOrder.EndDate;
            machLabDto.JobOrdernotes = jobOrder.JobOrdernotes;
            machLabDto.RemainingAmount = jobOrder.RemainingAmount;
            machLabDto.UnearnedRevenue = jobOrder.UnearnedRevenue;
            machLabDto.EarnedRevenue = jobOrder.EarnedRevenue;



            ViewBag.labourList = _costbusinessLogicL.getActiveLabour();
            ViewBag.machineList = _costbusinessLogicL.getActiveMachine();

            return View("~/views/Cost/addMachineAndLabourExpense.cshtml", machLabDto);

        }
        [HttpPost]
        public IActionResult addMachineAndLabourExpense(MachineLabourDTO machLabDto)
        {

            machLabDto.EmployeeId = HttpContext.Session.GetString("EmployeeID");

            bool result = _costbusinessLogicL.addMachineAndLabourExpense(machLabDto);

            string messageSuccess = "تم اضافة عناصر المقايسة";
            string messageError = "هناك خظأ في اضافة عناصر المقايسة ";

            if (result)
            {
                TempData["Success"] = messageSuccess;
                return View(machLabDto);
            }
            else
            {
                TempData["Error"] = messageError;
                return View(machLabDto);

            }


        }



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Mariam section



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Sandra section




        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // sherwet section





    }
}
