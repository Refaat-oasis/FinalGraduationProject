using System.Reflection.PortableExecutable;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.Models;
using Machine = ThothSystemVersion1.Models.Machine;

namespace ThothSystemVersion1.InterfaceServices
{
    public interface CostService
    {

        public bool newMachine(Machine machine);

        public bool newLabour(Labour labour);

        public List<Labour> ViewAllLabour();
        public List<Machine> ViewAllMachines();

        public (bool Success, string Message) EditLabour(int LabourID, Labour lab);
        public (bool Success, string Message) EditMachine(int MachineID, Machine machine);

        public bool addMachineAndLabourExpense(MachineLabourDTO maclabdto);

        public bool addMiscelleneousExpense(int jobOrderId);




    }
}
