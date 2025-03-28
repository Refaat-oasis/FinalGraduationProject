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


        public List<Paper> getAllActivePaper()
        {

            List<Paper> paperList = _context.Papers.Where(p => p.Activated == true).ToList();
            return paperList;
        }
        public List<Ink> getAllActiveInk()
        {

            List<Ink> inkList = _context.Inks.Where(p => p.Activated == true).ToList();
            return inkList;
        }
        public List<Supply> getAllActiveSupply()
        {

            List<Supply> supplyList = _context.Supplies.Where(p => p.Activated == true).ToList();
            return supplyList;
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
                                paper.TotalBalance = (decimal)totalQuantity * averagePrice;

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

        public void purchaseNewInk(purchaseOrderDTO purchaseOrdDTO)
        {

            List<Ink> existingInk = _context.Inks.ToList();
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

            for (int i = 0; i < existingInk.Count; i++)
            {

                for (int j = 0; j < quantityBridgeList.Count; j++)
                {

                    if (existingInk[i].InkId == quantityBridgeList[j].InkId)
                    {

                        var existinginks = _context.Inks.ToList();
                        foreach (var bridge in purchaseOrdDTO.BridgeList)
                        {
                            var ink = existinginks.FirstOrDefault(p => p.InkId == bridge.InkId);
                            if (ink != null)
                            {
                                // Calculate new quantity and average price
                                double totalQuantity = ink.Quantity + bridge.Quantity;
                                decimal totalValue = (decimal)ink.Quantity * ink.Price
                                                   + (decimal)bridge.Quantity * bridge.Price;
                                decimal averagePrice = totalValue / (decimal)totalQuantity;

                                // Update paper properties
                                ink.Quantity = (int)totalQuantity;
                                ink.Price = averagePrice;
                                ink.TotalBalance = (decimal)totalQuantity * averagePrice;

                                _context.Inks.Update(ink);
                            }
                        }

                        // Add QuantityBridges to context and save all changes
                        _context.QuantityBridges.AddRange(purchaseOrdDTO.BridgeList);
                        _context.SaveChanges();
                    }

                }

            }

        }
        public void purchaseNewSupply(purchaseOrderDTO purchaseOrdDTO)
        {

            List<Supply> existingSupply = _context.Supplies.ToList();
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

            for (int i = 0; i < existingSupply.Count; i++)
            {

                for (int j = 0; j < quantityBridgeList.Count; j++)
                {

                    if (existingSupply[i].SuppliesId == quantityBridgeList[j].SuppliesId)
                    {

                        var existingSupplies = _context.Supplies.ToList();
                        foreach (var bridge in purchaseOrdDTO.BridgeList)
                        {
                            var supply = existingSupplies.FirstOrDefault(p => p.SuppliesId == bridge.SuppliesId);
                            if (supply != null)
                            {
                                // Calculate new quantity and average price
                                double totalQuantity = supply.Quantity + bridge.Quantity;
                                decimal totalValue = (decimal)supply.Quantity * supply.Price
                                                   + (decimal)bridge.Quantity * bridge.Price;
                                decimal averagePrice = totalValue / (decimal)totalQuantity;

                                // Update paper properties
                                supply.Quantity = (int)totalQuantity;
                                supply.Price = averagePrice;
                                supply.TotalBalance = (decimal)totalQuantity * averagePrice;

                                _context.Supplies.Update(supply);
                            }
                        }

                        // Add QuantityBridges to context and save all changes
                        _context.QuantityBridges.AddRange(purchaseOrdDTO.BridgeList);
                        _context.SaveChanges();
                    }

                }

            }

        }
        // الحصول على العناصر النشطة
        public List<Paper> GetActivePapers() => _context.Papers.Where(p => p.Activated).ToList();
        public List<Ink> GetActiveInks() => _context.Inks.Where(i => i.Activated).ToList();
        public List<Supply> GetActiveSupplies() => _context.Supplies.Where(s => s.Activated).ToList();

        // الحصول على الكمية الحالية
        public int GetCurrentQuantity(string itemType, int itemId)
        {
            return itemType switch
            {
                "Paper" => _context.Papers.FirstOrDefault(p => p.PaperId == itemId)?.Quantity ?? 0,
                "Ink" => _context.Inks.FirstOrDefault(i => i.InkId == itemId)?.Quantity ?? 0,
                "Supply" => _context.Supplies.FirstOrDefault(s => s.SuppliesId == itemId)?.Quantity ?? 0,
                _ => 0 // Default case
            };
        }

        // تحديث الكمية مع تخزين الجرد
        public (bool Success, string Message) UpdateQuantity(string itemType, int itemId, int newQuantity, string notes, string employeeId)
        {
            try
            {
                int oldQuantity = 0;
                string itemName = "";
                QuantityBridge quantityBridge = null;

                switch (itemType)
                {
                    case "Paper":
                        var paper = _context.Papers.Find(itemId);
                        if (paper == null) return (false, "الورق غير موجود");
                        oldQuantity = paper.Quantity;
                        itemName = paper.Name;
                        paper.Quantity = newQuantity;

                        quantityBridge = _context.QuantityBridges.FirstOrDefault(q => q.PaperId == itemId);
                        break;

                    case "Ink":
                        var ink = _context.Inks.Find(itemId);
                        if (ink == null) return (false, "الحبر غير موجود");
                        oldQuantity = ink.Quantity;
                        itemName = ink.Name;
                        ink.Quantity = newQuantity;

                        quantityBridge = _context.QuantityBridges.FirstOrDefault(q => q.InkId == itemId);
                        break;

                    case "Supply":
                        var supply = _context.Supplies.Find(itemId);
                        if (supply == null) return (false, "المستلزمات غير موجودة");
                        oldQuantity = supply.Quantity;
                        itemName = supply.Name;
                        supply.Quantity = newQuantity;

                        quantityBridge = _context.QuantityBridges.FirstOrDefault(q => q.SuppliesId == itemId);
                        break;

                    default:
                        return (false, "نوع العنصر غير صحيح");
                }

                if (quantityBridge == null)
                {
                    return (false, "لا يوجد سجل مطابق في QuantityBridge");
                }

                // إنشاء سجل جديد في PhysicalCountOrder
                var physicalCount = new PhysicalCountOrder
                {
                    EmployeeId = employeeId,
                    PhysicalCountDate = DateOnly.FromDateTime(DateTime.Now),
                    PhysicalCountNotes = notes
                };

                _context.PhysicalCountOrders.Add(physicalCount);
                _context.SaveChanges();

                // ربط الكمية بالجرد
                quantityBridge.PhysicalCountId = physicalCount.PhysicalCountId;
                _context.SaveChanges();

                return (true, $"تم تعديل كمية {itemName} من {oldQuantity} إلى {newQuantity}");
            }
            catch (Exception ex)
            {
                return (false, $"حدث خطأ: {ex.Message}");
            }
        }
    }

    }
//}
