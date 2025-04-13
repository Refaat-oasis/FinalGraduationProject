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
            List<JobOrderCustEmpVM> jobOrderList = _costbusinessLogicL.GetJobOrdersWithoutProcessBridge();

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

        [HttpGet]
        public IActionResult EditLabour(int LabID)
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 5)
            {
                try
                {
                    var labour = _costbusinessLogicL.GetLabourById(LabID);
                    return View("~/Views/Cost/EditLabour.cshtml", labour);
                }
                catch (ApplicationException ex)
                {
                    return StatusCode(500, ex.Message); // Internal server error
                }
                catch (ArgumentException ex)
                {
                    return NotFound(ex.Message);
                }
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }
        [HttpPost]
        public IActionResult EditLabour(int LabID, Labour updatedLabour)
        {
            if (updatedLabour == null)
            {
                ModelState.AddModelError("", "بيانات العامل غير صالحة.");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                // Return to the view with validation errors
                return View(updatedLabour);
            }

            try
            {
                var result = _costbusinessLogicL.EditLabour(LabID, updatedLabour); // Update the labour
                if (result.Success)
                {
                    TempData["Success"] = result.Message;
                    return RedirectToAction("EditLabour", "cost", LabID);
                }
                else
                {

                    TempData["Error"] = result.Message;
                    return RedirectToAction("EditLabour", "cost", LabID);
                }



            }

            catch (Exception ex)
            {
                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات الموظف";
                return View(updatedLabour);
            }

        }
        [HttpGet]
        public IActionResult EditMachine(int MachineID)
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 5)
            {
                try
                {
                    var machine = _costbusinessLogicL.GetMachineById(MachineID);
                    return View("~/Views/Cost/EditMachine.cshtml", machine);
                }
                catch (ApplicationException ex)
                {
                    return StatusCode(500, ex.Message); // Internal server error
                }
                catch (ArgumentException ex)
                {
                    return NotFound(ex.Message);
                }
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }
        }
        [HttpPost]
        public IActionResult EditMachine(int MachineID, Machine updatedMachine)
        {
            if (updatedMachine == null)
            {
                ModelState.AddModelError("", "بيانات الالة غير صالحة.");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                // Return to the view with validation errors
                return View(updatedMachine);
            }
            try
            {
                var result = _costbusinessLogicL.EditMachine(MachineID, updatedMachine); // Update the labour
                if (result.Success)
                {
                    TempData["Success"] = result.Message;
                    return RedirectToAction("EditMachine", "cost", MachineID);
                }
                else
                {

                    TempData["Error"] = result.Message;
                    return RedirectToAction("EditMachine", "cost", MachineID);
                }

            }

            catch (Exception ex)
            {
                TempData["Error"] = "حدث خطأ أثناء تعديل بيانات الموظف";
                return View(updatedMachine);
            }

        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // sherwet section





    }
}
