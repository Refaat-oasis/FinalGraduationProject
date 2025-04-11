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

        public bool EditLabour(int id, Labour labour)
        {
            throw new NotImplementedException();
        }

        public bool EditMachine(int id, Machine machine)
        {
            throw new NotImplementedException();
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

    }
}
