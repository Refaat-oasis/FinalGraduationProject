using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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

        //public bool AddVendor(VendorEditDTO newVendor)
        //{
        //    try
        //    {
        //        Vendor foundVendorByPhone = _context.Vendors.FirstOrDefault(v => v.VendorPhone == newVendor.VendorPhone);
        //        Vendor foundVendorByEmail = _context.Vendors.FirstOrDefault(v => v.VendorEmail == newVendor.VendorEmail);
        //        if (foundVendorByEmail != null || foundVendorByPhone != null)
        //        {

        //            return false;
        //        }

        //        if (newVendor == null)
        //        {
        //            throw new ArgumentNullException(nameof(newVendor));
        //        }

        //        Vendor addedVendor = new Vendor();

        //        addedVendor.VendorName = newVendor.VendorName;
        //        addedVendor.VendorEmail = newVendor.VendorEmail;
        //        addedVendor.VendorPhone = newVendor.VendorPhone;
        //        addedVendor.VendorNotes = newVendor.VendorNotes;
        //        addedVendor.VendorAddress = newVendor.VendorAddress;


        //        _context.Vendors.Add(addedVendor);
        //        _context.SaveChanges();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception (ex) here
        //        throw new ApplicationException("An error occurred while adding the vendor.", ex);
        //    }
        //}
        public bool AddVendor(VendorAddDTO newVendor)
        {
            try
            {
                Vendor foundVendorByPhone = _context.Vendors.FirstOrDefault(v => v.VendorPhone == newVendor.VendorPhone);
                //Vendor foundVendorByName = _context.Vendors.FirstOrDefault(v => v.VendorName == newVendor.VendorName);
                Vendor foundVendorByEmail = _context.Vendors.FirstOrDefault(v => v.VendorEmail == newVendor.VendorEmail);
                if (foundVendorByEmail != null || foundVendorByPhone != null)
                {

                    return false;
                }

                if (newVendor == null)
                {
                    throw new ArgumentNullException(nameof(newVendor));
                }

                Vendor addedVendor = new Vendor();

                addedVendor.VendorName = newVendor.VendorName;
                addedVendor.VendorEmail = newVendor.VendorEmail;
                addedVendor.VendorPhone = newVendor.VendorPhone;
                addedVendor.VendorNotes = newVendor.VendorNotes;
                addedVendor.VendorAddress = newVendor.VendorAddress;
                //addedVendor.Activated = true;


                _context.Vendors.Add(addedVendor);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new ApplicationException("An error occurred while adding the vendor.", ex);
            }
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

        public bool EditVendor(int vendorID, Vendor newVendor)
        {
            try
            {

                Vendor foundVendor = _context.Vendors.FirstOrDefault(v => v.VendorId == vendorID);
                Vendor foundVandorEmail = _context.Vendors.FirstOrDefault(v => v.VendorEmail == newVendor.VendorEmail);
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

            return inkList;
        }



        public async Task<List<Paper>> ViewAllPaper()
        {
            List<Paper> papersList = _context.Papers.ToList();

            return papersList;
        }

        public async Task<List<Supply>> ViewAllSupply()
        {
            List<Supply> supplyList = _context.Supplies.ToList();
           
            return supplyList;
        }

        public bool AddVendor(VendorEditDTO newVendor)
        {
            throw new NotImplementedException();
        }

        public void purchaseNewPaper(purchaseOrderDTO purchaseOrdDTO)
        {

            List<Paper> existingPaper = _context.Papers.ToList();
            List<QuantityBridge> quantityBridgeList = purchaseOrdDTO.BridgeList;
            PurchaseOrder purchaseOrder = new PurchaseOrder();

            purchaseOrder.EmployeeId = purchaseOrdDTO.EmployeeId;
            purchaseOrder.VendorId = purchaseOrdDTO.VendorId;
            purchaseOrder.PurchaseNotes = purchaseOrdDTO.PurchaseNotes;

            _context.PurchaseOrders.Add(purchaseOrder);
            _context.SaveChanges();

            int lastone = _context.PurchaseOrders
                      .OrderByDescending(po => po.PurchaseId)
                      .Select(po => po.PurchaseId)
                      .FirstOrDefault();

            foreach (QuantityBridge bridge in purchaseOrdDTO.BridgeList)
            {
                bridge.PurchaseId = lastone;
                bridge.QuantityBridgeID = null;
            }

            for (int i = 0; i < existingPaper.Count; i++) {

                for (int j = 0; j < quantityBridgeList.Count; j++) {

                    if (existingPaper[i].PaperId == quantityBridgeList[j].PaperId) {

                        var existingPapers = _context.Papers.ToList();
                        foreach (var bridge in purchaseOrdDTO.BridgeList)
                        {
                            var paper = existingPapers.FirstOrDefault(p => p.PaperId == bridge.PaperId);
                            if (paper != null)
                            {
                                // Calculate new quantity and average price
                                double totalQuantity = paper.Quantity + bridge.Quantity;
                                decimal totalValue = (decimal)paper.Quantity * paper.Price
                                                   + (decimal)bridge.Quantity * bridge.Price;
                                decimal averagePrice = totalValue / (decimal)totalQuantity;

                                // Update paper properties
                                paper.Quantity = (int)totalQuantity;
                                paper.Price = averagePrice;

                                _context.Papers.Update(paper);
                            }
                        }

                        // Add QuantityBridges to context and save all changes
                        _context.QuantityBridges.AddRange(purchaseOrdDTO.BridgeList);
                        _context.SaveChanges();
                    }

                }

                }

            }

        }

    }
//}
