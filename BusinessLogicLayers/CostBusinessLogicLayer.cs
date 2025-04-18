using System.Reflection.PortableExecutable;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.InterfaceServices;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.ViewModels;
using Machine = ThothSystemVersion1.Models.Machine;

namespace ThothSystemVersion1.BusinessLogicLayers
{
    public class CostBusinessLogicLayer : CostService
    {

        private readonly ThothContext _context;


        public CostBusinessLogicLayer(ThothContext context)
        {
            _context = context;
        }

        public bool addMachineAndLabourExpense(MachineLabourDTO machlabdto)
        {
            List<ProcessBridge> proBrid = new List<ProcessBridge>();

            proBrid = machlabdto.processBridges;

            for (int i = 0; i < proBrid.Count; i++)
            {

                if (proBrid[i].MachineId != null)
                {
                    Machine machine = _context.Machines.FirstOrDefault(m => m.MachineId == proBrid[i].MachineId);

                    proBrid[i].MachineHourPrice = machine.Price;
                    proBrid[i].TotalMachineValue = machine.Price * proBrid[i].NumberOfHours;
                    proBrid[i].JobOrderId = machlabdto.JobOrderId;
                    proBrid[i].OldMachinePrice = machine.Price;
                    proBrid[i].EmployeeId = machlabdto.EmployeeId;

                    _context.ProcessBridges.Add(proBrid[i]);
                    _context.SaveChanges();

                }
                else if (proBrid[i].LabourId != null)
                {
                    Labour labour = _context.Labours.FirstOrDefault(m => m.LabourId == proBrid[i].LabourId);
                    proBrid[i].LabourHourPrice = labour.Price;
                    proBrid[i].TotalLabourValue = labour.Price * proBrid[i].NumberOfHours;
                    proBrid[i].JobOrderId = machlabdto.JobOrderId;
                    proBrid[i].OldlabourPrice = labour.Price;
                    proBrid[i].EmployeeId = machlabdto.EmployeeId;

                    _context.ProcessBridges.Add(proBrid[i]);
                    _context.SaveChanges();

                }


                else
                {
                    return false;

                }


            }

            return true;
        }

        public bool addMiscelleneousExpense(int jobOrderId)
        {
            throw new NotImplementedException();
        }

        public (bool Success, string Message) EditLabour(int LabourID, Labour lab)
        {
            try
            {
                if (lab == null)
                {
                    return (false, "بيانات العملية المطلوبة غير متوفرة.");
                }
                Labour exsistingLabour = _context.Labours.FirstOrDefault(a => a.LabourId == lab.LabourId);
                if (exsistingLabour == null)
                {
                    return (false, "العملية غير موجودة.");
                }
                exsistingLabour.LabourProcessName = lab.LabourProcessName;
                exsistingLabour.Price = lab.Price;
                exsistingLabour.Activated = lab.Activated;

                _context.Labours.Update(exsistingLabour); // Mark the entity as modified
                _context.SaveChanges(); // Save changes to the database
                return (true, $" تم تعديل العملية {exsistingLabour.LabourProcessName}"); // Success

            }
            catch (Exception ex)
            {
                return (false, $"حدث خطأ: {ex.Message}");
            }
        }

        public (bool Success, string Message) EditMachine(int MachineID, Machine machine)
        {
            try
            {
                if (machine == null)
                {
                    return (false, "بيانات الالة المطلوبة غير متوفرة.");
                }
                Machine exsistingMachine = _context.Machines.FirstOrDefault(a => a.MachineId == machine.MachineId);
                if (exsistingMachine == null)
                {
                    return (false, "الالة غير موجود.");
                }
                exsistingMachine.MachineProcessName = machine.MachineProcessName;
                exsistingMachine.Price = machine.Price;
                exsistingMachine.Activated = machine.Activated;

                _context.Machines.Update(exsistingMachine); // Mark the entity as modified
                _context.SaveChanges(); // Save changes to the database
                return (true, $" تم تعديل الالة  {exsistingMachine.MachineProcessName}"); // Success

            }
            catch (Exception ex)
            {
                return (false, $"حدث خطأ: {ex.Message}");
            }
        }
        public bool newLabour(Labour labour)
        {
            try
            {
                if (labour == null)
                {
                    throw new ArgumentNullException(nameof(labour));
                }
                Labour addedLabour = new Labour();
                addedLabour.LabourProcessName = labour.LabourProcessName;
                addedLabour.Price = labour.Price;
                addedLabour.Activated = true;
                _context.Labours.Add(addedLabour);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new ApplicationException("An error occurred while adding the labour.", ex);
            }
        }

        public bool newMachine(Machine machine)
        {
            try
            {
                if (machine == null)
                {
                    throw new ArgumentNullException(nameof(machine));
                }
                Machine addedMachine = new Machine();
                addedMachine.MachineProcessName = machine.MachineProcessName;
                addedMachine.Price = machine.Price;
                addedMachine.Activated = true;
                _context.Machines.Add(addedMachine);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new ApplicationException("An error occurred while adding the machine.", ex);
            }
        }

        public List<Labour> ViewAllLabour()
        {
            List<Labour> labourList = _context.Labours.ToList();
            return labourList;
        }

        public List<Machine> ViewAllMachines()
        {
            List<Machine> machineList = _context.Machines.ToList();
            return machineList;
        }

        public List<Machine> getActiveMachine()
        {

            List<Machine> machineList = _context.Machines.Where(mach => mach.Activated == true).ToList();
            return machineList;
        }
        public List<Labour> getActiveLabour()
        {

            List<Labour> labourList = _context.Labours.Where(mach => mach.Activated == true).ToList();
            return labourList;
        }
        public Labour GetLabourById(int id)
        {
            try
            {
                Labour labour = _context.Labours.Find(id); // Synchronous Find
                if (labour == null)
                {
                    throw new ArgumentException("Labour not found.");
                }
                return labour;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new ApplicationException("An error occurred while fetching the labour.", ex);
            }
        }

        
        public Machine GetMachineById(int id)
        {
            try
            {
                Machine machine = _context.Machines.Find(id); // Synchronous Find
                if (machine == null)
                {
                    throw new ArgumentException("Machine not found.");
                }
                return machine;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new ApplicationException("An error occurred while fetching the machine.", ex);
            }
        }

        public List<JobOrderCustEmpVM> GetJobOrdersWithoutProcessBridge()
        {
            // Get job orders without process bridges
            var jobOrderIdsWithBridges = _context.ProcessBridges
                .Where(pb => pb.JobOrderId != null)
                .Select(pb => pb.JobOrderId.Value)
                .Distinct()
                .ToList();

            var jobOrdersWithoutBridges = _context.JobOrders
                .Where(jo => !jobOrderIdsWithBridges.Contains(jo.JobOrderId))
                .ToList();

            // Handle customer IDs (assuming CustomerId is int)
            var customerIds = jobOrdersWithoutBridges
                .Where(jo => jo.CustomerId.HasValue)
                .Select(jo => jo.CustomerId.Value)
                .Distinct()
                .ToList();

            // Handle employee IDs (now as string)
            var employeeIds = jobOrdersWithoutBridges
                .Where(jo => !string.IsNullOrEmpty(jo.EmployeeId))
                .Select(jo => jo.EmployeeId)
                .Distinct()
                .ToList();

            // Get related data
            var customers = _context.Customers
                .Where(c => customerIds.Contains(c.CustomerId))
                .ToDictionary(c => c.CustomerId);

            var employees = _context.Employees
                .Where(e => employeeIds.Contains(e.EmployeeId))
                .ToDictionary(e => e.EmployeeId);

            // Create view models with proper null checking
            return jobOrdersWithoutBridges
                .Where(jo => jo.CustomerId.HasValue &&
                            !string.IsNullOrEmpty(jo.EmployeeId) &&
                            customers.ContainsKey(jo.CustomerId.Value) &&
                            employees.ContainsKey(jo.EmployeeId))
                .Select(jo => new JobOrderCustEmpVM
                {
                    // Customer data
                    CustomerId = jo.CustomerId.Value,
                    CustomerAddress = customers[jo.CustomerId.Value].CustomerAddress,
                    CustomerPhone = customers[jo.CustomerId.Value].CustomerPhone,
                    CustomerName = customers[jo.CustomerId.Value].CustomerName,
                    CustomerEmail = customers[jo.CustomerId.Value].CustomerEmail,

                    // Job Order data
                    JobOrderId = jo.JobOrderId,
                    OrderProgress = jo.OrderProgress,
                    RemainingAmount = jo.RemainingAmount,
                    EarnedRevenue = jo.EarnedRevenue,
                    UnearnedRevenue = jo.UnearnedRevenue,
                    StartDate = jo.StartDate,
                    EndDate = jo.EndDate,
                    JobOrdernotes = jo.JobOrdernotes,

                    // Employee data (now handling string ID)
                    EmployeeId = jo.EmployeeId,
                    EmployeeName = employees[jo.EmployeeId].EmployeeName
                })
                .ToList();
        }


    }
}
