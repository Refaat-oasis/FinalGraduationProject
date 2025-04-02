using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.Hubs;
using ThothSystemVersion1.InterfaceServices;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.ViewModels;

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


        public Ink GetInkByID(int inkID)
        {
            try
            {
                Ink ink = _context.Inks.FirstOrDefault(i => i.InkId == inkID);
                if (ink == null)
                {
                    throw new ArgumentException("Ink not found.");
                }
                return ink;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new ApplicationException("An error occurred while fetching the ink.", ex);
            }

        }
        public bool EditInk(int inkID, Ink newInk)
        {
            try
            {
                Ink foundInk = _context.Inks.FirstOrDefault(i => i.InkId == inkID);
                if (newInk.ReorderPoint < 0)
                {
                    return false;
                }
                if (foundInk == null)
                {

                    throw new ArgumentException("Ink not found.");
                }

                foundInk.Name = newInk.Name;
                foundInk.Colored = newInk.Colored;
                foundInk.Activated = newInk.Activated;
                foundInk.ReorderPoint = newInk.ReorderPoint;
                _context.Inks.Update(foundInk);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the ink.", ex);
            }
        }

        public Paper GetPaperByID(int paperID)
        {
            try
            {
                Paper foundPaper = _context.Papers.FirstOrDefault(p => p.PaperId == paperID);
                if (foundPaper == null)
                {
                    throw new ArgumentException("Paper not found.");
                }
                return foundPaper;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new ApplicationException("An error occurred while fetching the paper.", ex);
            }
        }
        public bool editPaper(int paperID, Paper newPaper)
        {
            try
            {
                Paper foundPaper = _context.Papers.FirstOrDefault(p => p.PaperId == paperID);

                if (newPaper.ReorderPoint < 0)
                {
                    return false;
                }
                if (foundPaper == null)
                {

                    throw new ArgumentException("Paper not found.");
                }
                foundPaper.Name = newPaper.Name;
                foundPaper.Type = newPaper.Type;
                foundPaper.Weight = newPaper.Weight;
                foundPaper.Colored = newPaper.Colored;
                foundPaper.Activated = newPaper.Activated;
                foundPaper.ReorderPoint = newPaper.ReorderPoint;
                _context.Papers.Update(foundPaper);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the ink.", ex);
            }
        }

        public Supply GetSupplyByID(int suppliesId)
        {
            try
            {
                Supply supply = _context.Supplies.FirstOrDefault(s => s.SuppliesId == suppliesId);
                if (supply == null)
                {
                    throw new ArgumentException("Supply not found.");
                }
                return supply;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new ApplicationException("An error occurred while fetching the supply.", ex);
            }

        }

        public bool editSupply(int suppliesId, Supply newSupply)
        {
            try
            {
                Supply foundSupply = _context.Supplies.FirstOrDefault(s => s.SuppliesId == suppliesId);
                if (newSupply.ReorderPoint < 0)
                {
                    return false;
                }
                if (foundSupply == null)
                {

                    throw new ArgumentException("Supply not found.");
                }
                foundSupply.Name = newSupply.Name;
                foundSupply.Activated = newSupply.Activated;
                foundSupply.ReorderPoint = newSupply.ReorderPoint;
                _context.Supplies.Update(foundSupply);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while updating the supply", ex);
            }
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

        public Vendor GetVendorByID(int vendorID)
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


            for (int i = 0; i < quantityBridgeList.Count; i++)
            {
                quantityBridgeList[i].PurchaseId = lastone;
                Paper pap = _context.Papers.FirstOrDefault(p => p.PaperId == quantityBridgeList[i].PaperId);
                if (pap != null)
                {
                    // Calculate new quantity and average price
                    double totalQuantity = pap.Quantity + quantityBridgeList[i].Quantity;
                    decimal totalValue = (decimal)pap.Quantity * pap.Price +
                                         (decimal)quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;
                    decimal averagePrice = totalValue / (decimal)totalQuantity;

                    // Update paper properties
                    pap.Quantity = (int)totalQuantity;
                    pap.Price = averagePrice;
                    pap.TotalBalance = (decimal)totalQuantity * averagePrice;

                    _context.Papers.Update(pap);
                }
            }

            // Add QuantityBridges to context and save all changes
            _context.QuantityBridges.AddRange(purchaseOrdDTO.BridgeList);
            _context.SaveChanges();


        }


        public void purchaseNewInk(purchaseOrderDTO purchaseOrdDTO)
        {
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


            for (int i = 0; i < quantityBridgeList.Count; i++)
            {
                quantityBridgeList[i].PurchaseId = lastone;
                Ink ink = _context.Inks.FirstOrDefault(p => p.InkId == quantityBridgeList[i].InkId);
                if (ink != null)
                {
                    // Calculate new quantity and average price
                    double totalQuantity = ink.Quantity + quantityBridgeList[i].Quantity;
                    decimal totalValue = (decimal)ink.Quantity * ink.Price +
                                         (decimal)quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;
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
        public void purchaseNewSupply(purchaseOrderDTO purchaseOrdDTO)
        {

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


            for (int i = 0; i < quantityBridgeList.Count; i++)
            {
                quantityBridgeList[i].PurchaseId = lastone;
                Supply supply = _context.Supplies.FirstOrDefault(p => p.SuppliesId == quantityBridgeList[i].SuppliesId);
                if (supply != null)
                {
                    // Calculate new quantity and average price
                    double totalQuantity = supply.Quantity + quantityBridgeList[i].Quantity;
                    decimal totalValue = (decimal)supply.Quantity * supply.Price +
                                         (decimal)quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;
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
        //public (bool Success, string Message) UpdateQuantity(string itemType, int itemId, int newQuantity, string notes, string employeeId)
        //{
        //    try
        //    {
        //        int oldQuantity = 0;
        //        string itemName = "";
        //        QuantityBridge quantityBridge = null;

        //        switch (itemType)
        //        {
        //            case "Paper":
        //                var paper = _context.Papers.Find(itemId);
        //                if (paper == null) return (false, "الورق غير موجود");
        //                oldQuantity = paper.Quantity;
        //                itemName = paper.Name;
        //                paper.Quantity = newQuantity;

        //                quantityBridge = _context.QuantityBridges.FirstOrDefault(q => q.PaperId == itemId);
        //                break;

        //            case "Ink":
        //                var ink = _context.Inks.Find(itemId);
        //                if (ink == null) return (false, "الحبر غير موجود");
        //                oldQuantity = ink.Quantity;
        //                itemName = ink.Name;
        //                ink.Quantity = newQuantity;

        //                quantityBridge = _context.QuantityBridges.FirstOrDefault(q => q.InkId == itemId);
        //                break;

        //            case "Supply":
        //                var supply = _context.Supplies.Find(itemId);
        //                if (supply == null) return (false, "المستلزمات غير موجودة");
        //                oldQuantity = supply.Quantity;
        //                itemName = supply.Name;
        //                supply.Quantity = newQuantity;

        //                quantityBridge = _context.QuantityBridges.FirstOrDefault(q => q.SuppliesId == itemId);
        //                break;

        //            default:
        //                return (false, "نوع العنصر غير صحيح");
        //        }

        //        if (quantityBridge == null)
        //        {
        //            return (false, "لا يوجد سجل مطابق في QuantityBridge");
        //        }

        //        // إنشاء سجل جديد في PhysicalCountOrder
        //        var physicalCount = new PhysicalCountOrder
        //        {
        //            EmployeeId = employeeId,
        //            PhysicalCountDate = DateOnly.FromDateTime(DateTime.Now),
        //            PhysicalCountNotes = notes
        //        };

        //        _context.PhysicalCountOrders.Add(physicalCount);
        //        _context.SaveChanges();

        //        // ربط الكمية بالجرد
        //        quantityBridge.PhysicalCountId = physicalCount.PhysicalCountId;
        //        _context.SaveChanges();

        //        return (true, $"تم تعديل كمية {itemName} من {oldQuantity} إلى {newQuantity}");
        //    }
        //    catch (Exception ex)
        //    {
        //        return (false, $"حدث خطأ: {ex.Message}");
        //    }
        //}
        public (bool Success, string Message) UpdateQuantity(PhysicalCountDTO phdto)
        {
            try
            {
                int oldQuantity = 0;
                string itemName = "";
                QuantityBridge quantityBridge = new QuantityBridge();

                switch (phdto.itemType)
                {
                    case "Paper":
                        var paper = _context.Papers.Find(phdto.itemId);
                        if (paper == null) return (false, "الورق غير موجود");
                        oldQuantity = paper.Quantity;
                        itemName = paper.Name;
                        paper.Quantity = phdto.newQuantity;

                        quantityBridge.PaperId = phdto.itemId;
                        quantityBridge.Quantity = phdto.newQuantity;
                        break;

                    case "Ink":
                        var ink = _context.Inks.Find(phdto.itemId);
                        if (ink == null) return (false, "الحبر غير موجود");
                        oldQuantity = ink.Quantity;
                        itemName = ink.Name;
                        ink.Quantity = phdto.newQuantity;

                        quantityBridge.PaperId = phdto.itemId;
                        quantityBridge.Quantity = phdto.newQuantity;

                        break;

                    case "Supply":
                        var supply = _context.Supplies.Find(phdto.itemId);
                        if (supply == null) return (false, "المستلزمات غير موجودة");
                        oldQuantity = supply.Quantity;
                        itemName = supply.Name;
                        supply.Quantity = phdto.newQuantity;

                        quantityBridge.PaperId = phdto.itemId;
                        quantityBridge.Quantity = phdto.newQuantity;

                        break;

                    default:
                        return (false, "نوع العنصر غير صحيح");
                }

                var physicalCount = new PhysicalCountOrder
                {
                    EmployeeId = phdto.employeeId,
                    PhysicalCountNotes = phdto.notes
                };

                _context.PhysicalCountOrders.Add(physicalCount);
                _context.SaveChanges();

                int lastone = _context.PhysicalCountOrders
          .OrderByDescending(po => po.PhysicalCountId)
          .Select(po => po.PhysicalCountId)
          .FirstOrDefault();

                quantityBridge.PhysicalCountId = lastone;
                _context.QuantityBridges.Add(quantityBridge);
                _context.SaveChanges();


                return (true, $"تم تعديل كمية {itemName} من {oldQuantity} إلى {phdto.newQuantity}");
            }
            catch (Exception ex)
            {
                return (false, $"حدث خطأ: {ex.Message}");
            }
        }

        public bool editInk(int inkID, Ink newInk)
        {
            throw new NotImplementedException();
        }

        bool InventoryService.editPaper(int paperID, Paper newPaper)
        {
            throw new NotImplementedException();
        }

        bool InventoryService.editSupply(int suppliesID, Supply newSupply)
        {
            throw new NotImplementedException();
        }

        public InventoryReportViewModel invetoryReports(string itemType, int itemId, DateOnly beginingDate, DateOnly endingDate)
        {
            List<QuantityBridge> quantityBridgeList = new List<QuantityBridge>();
            List<PurchaseOrder> purchaseOrderList = new List<PurchaseOrder>();
            List<RequisiteOrder> requisiteOrderList = new List<RequisiteOrder>();
            List<ReturnsOrder> returnOrderList = new List<ReturnsOrder>();
            List<PhysicalCountOrder> physicalCountList = new List<PhysicalCountOrder>();
            switch (itemType)
            {
                case "Paper":
                    quantityBridgeList = _context.QuantityBridges.Where(q => q.PaperId == itemId).ToList();

                    foreach (QuantityBridge qb in quantityBridgeList)
                    {
                        List<PurchaseOrder> purorders = _context.PurchaseOrders.Where(p =>
                                    p.PurchaseId == qb.PurchaseId
                                    &&
                                    p.PurchaseDate >= beginingDate &&
                                    p.PurchaseDate <= endingDate
                                    )
                        .ToList();
                        
                        List<PhysicalCountOrder> phyOrders = _context.PhysicalCountOrders.Where(p =>
                                   p.PhysicalCountId == qb.PhysicalCountId).ToList();
                        
                        List<RequisiteOrder> reqOrders = _context.RequisiteOrders.Where(req =>
                                   req.RequisiteId == qb.RequisiteId).ToList();

                        //List<ReturnsOrder> retOrders = _context.ReturnsOrders.Where(ret =>
                        //           ret.ReturnId == qb.ReturnId).ToList();


                        purchaseOrderList.AddRange(purorders);
                        physicalCountList.AddRange(phyOrders);
                        //returnOrderList.AddRange(retOrders);
                        requisiteOrderList.AddRange(reqOrders);
                    }
                    break;


                    //purchaseOrderList = _context.PurchaseOrders
                    //    .Where(p => p.PurchaseId == qua
                    //                p.PurchaseDate.HasValue &&
                    //                p.PurchaseDate.Value >= DateOnly.FromDateTime(beginingDate) &&
                    //                p.PurchaseDate.Value <= DateOnly.FromDateTime(endDate))
                    //    .ToList();

                    //requisiteOrderList = _context.RequisiteOrders
                    //    .Where(r => r.RequisiteDate.HasValue &&
                    //                r.RequisiteDate.Value >= DateOnly.FromDateTime(beginingDate) &&
                    //                r.RequisiteDate.Value <= DateOnly.FromDateTime(endDate))
                    //    .ToList();

                    //returnOrderList = _context.ReturnsOrders
                    //    .Where(r => r.ReturnDate.HasValue &&
                    //                r.ReturnDate.Value >= DateOnly.FromDateTime(beginingDate) &&
                    //                r.ReturnDate.Value <= DateOnly.FromDateTime(endDate))
                    //    .ToList();
                    break;

                //case "Ink":
                //    quantityBridgeList = _context.QuantityBridges.Where(q => q.InkId == itemId).ToList();
                //    purchaseOrderList = _context.PurchaseOrders
                //        .Where(p => p.PurchaseDate.HasValue &&
                //                    p.PurchaseDate.Value >= DateOnly.FromDateTime(beginingDate) &&
                //                    p.PurchaseDate.Value <= DateOnly.FromDateTime(endDate))
                //        .ToList();
                //    requisiteOrderList = _context.RequisiteOrders
                //        .Where(r => r.RequisiteDate.HasValue &&
                //                    r.RequisiteDate.Value >= DateOnly.FromDateTime(beginingDate) &&
                //                    r.RequisiteDate.Value <= DateOnly.FromDateTime(endDate))
                //        .ToList();
                //    returnOrderList = _context.ReturnsOrders
                //        .Where(r => r.ReturnDate.HasValue &&
                //                    r.ReturnDate.Value >= DateOnly.FromDateTime(beginingDate) &&
                //                    r.ReturnDate.Value <= DateOnly.FromDateTime(endDate))
                //        .ToList();
                //    break;

                //case "Supply":
                //    quantityBridgeList = _context.QuantityBridges.Where(q => q.SuppliesId == itemId).ToList();
                //    purchaseOrderList = _context.PurchaseOrders
                //        .Where(p => p.PurchaseDate.HasValue &&
                //                    p.PurchaseDate.Value >= DateOnly.FromDateTime(beginingDate) &&
                //                    p.PurchaseDate.Value <= DateOnly.FromDateTime(endDate))
                //        .ToList();
                //    requisiteOrderList = _context.RequisiteOrders
                //        .Where(r => r.RequisiteDate.HasValue &&
                //                    r.RequisiteDate.Value >= DateOnly.FromDateTime(beginingDate) &&
                //                    r.RequisiteDate.Value <= DateOnly.FromDateTime(endDate))
                //        .ToList();
                //    returnOrderList = _context.ReturnsOrders
                //        .Where(r => r.ReturnDate.HasValue &&
                //                    r.ReturnDate.Value >= DateOnly.FromDateTime(beginingDate) &&
                //                    r.ReturnDate.Value <= DateOnly.FromDateTime(endDate))
                //        .ToList();
                //    break;

                default:
                    break;
            }

            InventoryReportViewModel invViewModel = new InventoryReportViewModel();
            invViewModel.purchaseOrderList = purchaseOrderList;
            invViewModel.requisiteOrderList = requisiteOrderList;
            invViewModel.quantityBridgeList = quantityBridgeList;
            invViewModel.returnOrderList = returnOrderList;

            return (invViewModel);

        }

    }

}
