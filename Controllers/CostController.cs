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


            JobOrderCost jobOrder = _costbusinessLogicL.GetJobOrderForCost(JobOrderId);

            MachineLabourDTO machLabDto = new MachineLabourDTO();
            machLabDto.JobOrderId = jobOrder.JobOrderId;
            machLabDto.CustomerId = jobOrder.CustomerId;
            machLabDto.CustomerName = jobOrder.CustomerName;
            machLabDto.EmployeeId = jobOrder.EmployeeId;
            machLabDto.EmployeeName = jobOrder.EmployeeName;
            machLabDto.OrderProgress = jobOrder.OrderProgress;
            machLabDto.paperBalance = null;
            machLabDto.StartDate = jobOrder.StartDate;
            machLabDto.EndDate = jobOrder.EndDate;
            machLabDto.JobOrdernotes = jobOrder.JobOrdernotes;
            machLabDto.RemainingAmount = jobOrder.RemainingAmount;
            machLabDto.UnearnedRevenue = jobOrder.UnearnedRevenue;
            machLabDto.EarnedRevenue = jobOrder.EarnedRevenue;
            machLabDto.paperBalance = jobOrder.paperBalance;
            machLabDto.inkBalance = jobOrder.inkBalance;
            machLabDto.supplyBalance = jobOrder.supplyBalance;
            //machLabDto


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
        [HttpGet]
        public IActionResult ViewAllLabour()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 5 || jobRole ==6)
            {

                List<Labour> labourList = _costbusinessLogicL.ViewAllLabour();
                return View("~/Views/Cost/ViewAllLabour.cshtml", labourList);
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }

        }


        [HttpGet]
        public IActionResult ViewAllMachines()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 5 || jobRole == 6)
            {

                List<Machine> machineList = _costbusinessLogicL.ViewAllMachines();
                return View("~/Views/Cost/ViewAllMachines.cshtml", machineList);
            }
            else
            {

                return RedirectToAction("UnauthorizedAccess", "employee");
            }

        }




        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Sandra section

        [HttpGet]
        public IActionResult EditLabour(int LabourID)
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 5)
            {
                try
                {
                    var labour = _costbusinessLogicL.GetLabourById(LabourID);
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
        public IActionResult EditLabour(int LabourID, Labour updatedLabour)
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
                var result = _costbusinessLogicL.EditLabour(LabourID, updatedLabour); // Update the labour
                if (result.Success)
                {
                    TempData["Success"] = result.Message;
                    return RedirectToAction("EditLabour", "cost", new { LabourID });
                }
                else
                {

                    TempData["Error"] = result.Message;
                    return RedirectToAction("EditLabour", "cost", new { LabourID });
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
                    return RedirectToAction("EditMachine", "cost", new { MachineID });
                }
                else
                {

                    TempData["Error"] = result.Message;
                    return RedirectToAction("EditMachine", "cost",new { MachineID });
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

        [HttpGet]
        public IActionResult AddMachine()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 5 || jobRole == 6)
            {
                try
                {
                    Machine model = new Machine();
                    return View("~/Views/Cost/AddMachine.cshtml",model);
                }
                catch (ApplicationException ex)
                {
                    return StatusCode(500, ex.Message);
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
        public IActionResult AddMachine(Machine machine)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("~/Views/Cost/AddMachine.cshtml", machine);
                }

                bool isCustomerAdded = _costbusinessLogicL.newMachine(machine);

                TempData["Success"] = "تم إضافة الالة بنجاح";
                return RedirectToAction("AddMachine", "Cost");
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = "حدث خطأ أثناء إضافة الالة";
                return View("~/Views/Cost/AddMachine.cshtml", machine);
            }
        }

        [HttpGet]
        public IActionResult AddLabour()
        {
            int? jobRole = HttpContext.Session.GetInt32("JobRole");
            if (jobRole == 0 || jobRole == 5 || jobRole == 6)
            {
                try
                {
                    Labour model = new Labour();
                    return View("~/Views/Cost/AddLabour.cshtml",model);
                }
                catch (ApplicationException ex)
                {
                    return StatusCode(500, ex.Message);
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
        public IActionResult AddLabour(Labour labour)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("~/Views/Cost/AddLabour.cshtml", labour);
                }

                bool isCustomerAdded = _costbusinessLogicL.newLabour(labour);

                TempData["Success"] = "تم إضافة العامل بنجاح";
                return RedirectToAction("AddLabour", "Cost");
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = "حدث خطأ أثناء إضافة العامل";
                return View("~/Views/Cost/AddLabour.cshtml", labour);
            }
        }



    }
}
