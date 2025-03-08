using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.InterfaceServices
{
    public interface InventoryService
    {
        // vendor section
        public List<Vendor> ViewAllVendor();
         public Task <List<Paper>> ViewAllPaper();
         public Task <List<Ink>> ViewAllInk();
         public Task<List<Supply>> ViewAllSupply();
        public bool AddVendor(VendorEditDTO newVendor);
        public bool EditVendor(int vendorID ,Vendor newVendor);

        // inventory ink section
        public void addInk(Ink newInk);
        public void EditInk(int inkID, Ink newInk);
        
        // inventory paper section
        public void addPaper (Paper newPaper);
        public void editPaper(int paperID , Paper newPaper);
        
        // inventory supply section
        public void addSupply(Supply newSupply);
        public void editSupply(int supplyID , Supply newSupply);






    }
}
