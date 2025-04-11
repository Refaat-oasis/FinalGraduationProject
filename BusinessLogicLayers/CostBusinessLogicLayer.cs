using ThothSystemVersion1.Database;
using ThothSystemVersion1.InterfaceServices;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.BusinessLogicLayers
{
    public class CostBusinessLogicLayer : CostService
    {

        private readonly ThothContext _context;


        public CostBusinessLogicLayer(ThothContext context)
        {
            _context = context;
        }

        public bool addMachineAndLabourExpense(int jobOrderId)
        {
            throw new NotImplementedException();
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
    }
}
