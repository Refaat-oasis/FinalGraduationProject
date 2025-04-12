using System.Reflection.PortableExecutable;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.InterfaceServices;
using ThothSystemVersion1.Models;
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
            throw new NotImplementedException();
        }

        public bool newMachine(Machine machine)
        {
            throw new NotImplementedException();
        }

        public List<Labour> ViewAllLabour()
        {
            throw new NotImplementedException();
        }

        public List<Machine> ViewAllMachines()
        {
            throw new NotImplementedException();
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

        
    }
}
