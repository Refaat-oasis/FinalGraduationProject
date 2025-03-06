using Microsoft.AspNetCore.SignalR;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.Hubs;
using ThothSystemVersion1.InterfaceServices;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.BusinessLogicLayers
{
    public class InventoryBussinesLogicLayer : InventoryService
    {
        private readonly ThothContext _context;
        private readonly IHubContext<ProductHub> _hubContext;


        public InventoryBussinesLogicLayer(ThothContext context, IHubContext<ProductHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;

        }

        public void addInk(Ink newInk)
        {
            if (newInk != null)
            {
                _context.Add(newInk);
                _context.SaveChanges();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void addPaper(Paper newPaper)
        {
            if (newPaper != null)
            {
                _context.Add(newPaper);
                _context.SaveChanges();
             }
            else { 
                throw new NotImplementedException();
            }
        }

        public void addSupply(Supply newSupply)
        {
            if (newSupply != null)
            {
                _context.Add(newSupply);
                _context.SaveChanges();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void AddVendor(Vendor newVendor)
        {
            throw new NotImplementedException();
        }

        public void EditInk(int inkID, Ink newInk)
        {
            throw new NotImplementedException();
        }

        public void editPaper(int paperID, Paper newPaper)
        {
            throw new NotImplementedException();
        }

        public void editSupply(int supplyID, Supply newSupply)
        {
            throw new NotImplementedException();
        }

        public bool EditVendor(int vendorID, VendorDTO newVendor)
        {
            try
            {

                Vendor foundVendor = _context.Vendors.FirstOrDefault(v => v.VendorId == vendorID);
                Vendor foundVandorEmail = _context.Vendors.Find(foundVendor.VendorEmail);
                Vendor foundVendorPhone = _context.Vendors.FirstOrDefault(v => v.VendorPhone == newVendor.VendorPhone);

                if (foundVandorEmail != null & foundVendorPhone != null)
                {
                    return false;
                }
                if (foundVendor == null)
                {

                    throw new ArgumentException("Vendor not found.");
                }
                foundVendor.VendorName = newVendor.VendorName;
                foundVendor.VendorEmail = newVendor.VendorEmail;
                foundVendor.VendorPhone = newVendor.VendorPhone;
                foundVendor.VendorAddress = newVendor.VendorAddress;
                foundVendor.VendorNotes = newVendor.VendorNotes;
                _context.Vendors.Update(foundVendor);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the vendor.", ex);
            }
        }

        public Vendor GetVendorByID (int vendorID)
        {
            Vendor foundVendor = _context.Vendors.FirstOrDefault(v => v.VendorId == vendorID);
            if (foundVendor == null)
            {
                throw new ArgumentException("Vendor not found.");
            }
            return foundVendor;
        }

        public List<Vendor> ViewAllVendor()
        {
            List<Vendor> vendorsList = _context.Vendors.ToList();
            return vendorsList;
        }

        public async Task<List<Ink>> ViewAllInk()
        {
            List<Ink> inkList = _context.Inks.ToList();
            Console.WriteLine("Calling CheckInkReorderPoint on the hub...");
           await _hubContext.Clients.All.SendAsync("CheckInkReorderPoint");
            return inkList;
        }

        public async Task<List<Paper>> ViewAllPaper()
        {
            List<Paper> papersList = _context.Papers.ToList();
            Console.WriteLine("Calling CheckPaperReorderPoint on the hub...");
            await _hubContext.Clients.All.SendAsync("CheckPaperReorderPoint");
            return papersList;
        }

       public async Task<List<Supply>> ViewAllSupply()
        {
            List<Supply> supplyList = _context.Supplies.ToList();
            Console.WriteLine("Calling ChecksupplyReorderPoint on the hub...");
            await _hubContext.Clients.All.SendAsync("CheckSupplyReorderPoint");
            return supplyList;
        }
    }
}
