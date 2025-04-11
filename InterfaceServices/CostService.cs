using System.Reflection.PortableExecutable;
using ThothSystemVersion1.Models;
using Machine = ThothSystemVersion1.Models.Machine;

namespace ThothSystemVersion1.InterfaceServices
{
    public interface CostService
    {

        public bool newMachine (Machine machine);

        public bool newLabour(Labour labour);

        public List<Labour> ViewAllLabour();
        public List<Machine> ViewAllMachines();

        public bool EditLabour(int id, Labour labour);
        public bool EditMachine(int id, Machine machine);

        public bool addMachineAndLabourExpense(int jobOrderId);

        public bool addMiscelleneousExpense(int jobOrderId);




    }
}
