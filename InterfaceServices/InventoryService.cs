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
        public bool AddVendor(VendorAddDTO newVendor);
        public bool EditVendor(int vendorID, VendorEditDTO newVendor);

        // inventory ink section
        public bool addInk(Ink newInk);
        public bool EditInk(int inkID, Ink newInk);
        
        // inventory paper section
        public bool addPaper (Paper newPaper);
        public bool editPaper(int paperID , Paper newPaper);
        
        // inventory supply section
        public bool addSupply(Supply newSupply);
        public bool editSupply(int suppliesId , Supply newSupply);






    }
}
