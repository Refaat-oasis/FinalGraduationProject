using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.Hubs;
using ThothSystemVersion1.InterfaceServices;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.ModifiedModels;
using ThothSystemVersion1.Utilities;
using ThothSystemVersion1.ViewModels;
using static System.Net.Mime.MediaTypeNames;

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


        public bool addInk(Ink newInk)
        {
            try
            {
                if (newInk != null)
                {
                    newInk.Activated = true;
                    newInk.TotalBalance = newInk.UnitPrice * newInk.NumberOfUnits;
                    newInk.Quantity = newInk.NumberOfUnits * newInk.AverageQuantity;
                    newInk.Price = (decimal)newInk.TotalBalance / newInk.Quantity;
                    _context.Add(newInk);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (ArgumentException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
            }
        }

        public bool addPaper(Paper newPaper)
        {
            try
            {
                if (newPaper != null)
                {
                    newPaper.Activated = true;
                    newPaper.TotalBalance = newPaper.Price * newPaper.Quantity;
                    _context.Add(newPaper);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (ArgumentException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
            }
        }

        public bool addSupply(Supply newSupply)
        {
            try
            {
                if (newSupply != null)
                {
                    newSupply.Activated = true;
                    newSupply.TotalBalance = newSupply.Price * newSupply.Quantity;
                    _context.Add(newSupply);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (ArgumentException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
            }
        }

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
                addedVendor.Activated = true;


                _context.Vendors.Add(addedVendor);
                _context.SaveChanges();

                return true;
            }
            catch (ArgumentException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
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
            catch (ArgumentException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }

        }

        public bool EditInk(int inkID, Ink newInk)
        {
            try
            {
                Ink foundInk = _context.Inks.FirstOrDefault(i => i.InkId == inkID);
                if (foundInk == null)
                {

                    throw new ArgumentException("Ink not found.");
                }

                foundInk.Name = newInk.Name;
                foundInk.Colored = newInk.Colored;
                foundInk.Activated = newInk.Activated;
                foundInk.ReorderPoint = newInk.ReorderPoint;
                foundInk.AverageQuantity = newInk.AverageQuantity;
                _context.Inks.Update(foundInk);
                _context.SaveChanges();
                return true;
            }
            catch (ArgumentException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
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
            catch (ArgumentException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        public bool editPaper(int paperID, Paper newPaper)
        {
            try
            {
                Paper foundPaper = _context.Papers.FirstOrDefault(p => p.PaperId == paperID);
                if (foundPaper == null)
                {

                    throw new ArgumentException("Paper not found.");
                }
                foundPaper.Name = newPaper.Name;
                foundPaper.Size = newPaper.Size;
                foundPaper.Weight = newPaper.Weight;
                foundPaper.Colored = newPaper.Colored;
                foundPaper.Activated = newPaper.Activated;
                foundPaper.ReorderPoint = newPaper.ReorderPoint;
                _context.Papers.Update(foundPaper);
                _context.SaveChanges();
                return true;
            }
            catch (ArgumentException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
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
            catch (ArgumentException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }

        }

        public bool editSupply(int suppliesId, Supply newSupply)
        {
            try
            {
                Supply foundSupply = _context.Supplies.FirstOrDefault(s => s.SuppliesId == suppliesId);
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
            catch (ArgumentException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
            }
        }

        public bool EditVendor(int vendorID, VendorEditDTO newVendor)
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

                    //throw new ArgumentException("Vendor not found.");
                    return false;
                }
                foundVendor.VendorName = newVendor.VendorName;
                foundVendor.VendorEmail = newVendor.VendorEmail;
                foundVendor.VendorPhone = newVendor.VendorPhone;
                foundVendor.VendorAddress = newVendor.VendorAddress;
                foundVendor.VendorNotes = newVendor.VendorNotes;
                foundVendor.Activated = newVendor.Activated;
                _context.Vendors.Update(foundVendor);
                _context.SaveChanges();
                return true;
            }
            catch (ArgumentException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
            }
        }

        public VendorEditDTO GetVendorByID(int vendorID)
        {
            try
            {
                Vendor foundVendor = _context.Vendors.FirstOrDefault(v => v.VendorId == vendorID);
                if (foundVendor == null)
                {
                    throw new ArgumentException("Vendor not found.");
                }
                VendorEditDTO vendor = new VendorEditDTO();
                vendor.VendorId = foundVendor.VendorId;
                vendor.VendorName = foundVendor.VendorName;
                vendor.VendorEmail = foundVendor.VendorEmail;
                vendor.VendorPhone = foundVendor.VendorPhone;
                vendor.VendorAddress = foundVendor.VendorAddress;


                return vendor;
            }
            catch (ArgumentException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        public List<Vendor> ViewAllVendor()
        {
            try
            {
                List<Vendor> vendorsList = _context.Vendors.ToList();
                return vendorsList;
            }
            catch (ArgumentException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        public async Task<List<Ink>> ViewAllInk()
        {
            try
            {
                List<Ink> inkList = _context.Inks.ToList();

                return inkList;
            }
            catch (ArgumentException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        public async Task<List<Paper>> ViewAllPaper()
        {
            try
            {
                List<Paper> papersList = _context.Papers.ToList();

                return papersList;
            }
            catch (ArgumentException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        public async Task<List<Supply>> ViewAllSupply()
        {
            try
            {
                List<Supply> supplyList = _context.Supplies.ToList();

                return supplyList;
            }
            catch (ArgumentException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        public bool DeleteColorWeightSize(int ColorWeightSizeId)
        {
            try
            {

                ColorWeightSize cwz = _context.ColorWeightSizes.FirstOrDefault(c => c.ColorWeightSizeId == ColorWeightSizeId);
                if (cwz == null)
                {
                    return false;
                }
                else
                {

                    _context.ColorWeightSizes.Remove(cwz);
                    _context.SaveChanges();

                    return true;
                }
            }
            catch (ArgumentException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
            }
        }

        public List<ColorWeightSize> getAllColorWeightSize()
        {
            try
            {
                List<ColorWeightSize> colorWeightSizeList = _context.ColorWeightSizes.ToList();
                return colorWeightSizeList;
            }
            catch (ArgumentException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // view bags lists

        public List<Paper> GetActivePapers() => _context.Papers.Where(p => p.Activated).ToList();

        public List<Ink> GetActiveInks() => _context.Inks.Where(i => i.Activated).ToList();

        public List<Supply> GetActiveSupplies() => _context.Supplies.Where(s => s.Activated).ToList();


        public List<SparePart> getActiveSpareParts()
        {

            List<SparePart> sparepartslist = _context.SpareParts.Where(sp => sp.Activated == true).ToList();

            return sparepartslist;
        }


        //public List<RequisiteOrder> getLast15RequisiteORder() => _context.RequisiteOrders
        //    .OrderByDescending(r => r.RequisiteDate)
        //    .Take(15)
        //    .ToList();

        //public List<PurchaseOrder> getLast15PurchaseOrder() => _context.PurchaseOrders
        //    .OrderByDescending(p => p.PurchaseDate)
        //    .Take(15)
        //    .ToList();

        //public List<JobOrder> getLast15JObOrder() => _context.JobOrders
        //    .OrderByDescending(p => p.StartDate)
        //    .Take(15)
        //    .ToList();

        public List<Paper> getAllActivePaper()
        {
            try
            {
                List<Paper> paperList = _context.Papers.Where(p => p.Activated == true).ToList();
                return paperList;
            }
            catch (ArgumentException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        public List<Ink> getAllActiveInk()
        {
            try
            {
                List<Ink> inkList = _context.Inks.Where(p => p.Activated == true).ToList();
                return inkList;
            }
            catch (ArgumentException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        public List<Supply> getAllActiveSupply()
        {
            try
            {
                List<Supply> supplyList = _context.Supplies.Where(p => p.Activated == true).ToList();
                return supplyList;
            }
            catch (ArgumentException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // الحصول على الكمية الحالية

        public int GetCurrentQuantity(string itemType, int itemId)
        {

            return itemType switch
            {
                "Paper" => _context.Papers.FirstOrDefault(p => p.PaperId == itemId)?.Quantity ?? 0,
                "Ink" => _context.Inks.FirstOrDefault(i => i.InkId == itemId)?.Quantity ?? 0,
                "Supply" => _context.Supplies.FirstOrDefault(s => s.SuppliesId == itemId)?.Quantity ?? 0,
                "Spare Parts" => _context.SpareParts.FirstOrDefault(sp => sp.SparePartsId == itemId)?.Quantity ?? 0,
                _ => 0 // Default case
            };
        }

        public int GetCurrentNumberOfUnits(string itemType, int itemId)
        {
            if (itemType == "Ink")
            {
                return _context.Inks.FirstOrDefault(i => i.InkId == itemId)?.NumberOfUnits ?? 0;
            }
            return 0;
        }

        public (bool Success, string Message) UpdateQuantity(PhysicalCountDTO phdto)
        {
            try
            {
                int oldQuantity = 0;
                int oldNumberOfUnits = 0;
                string itemName = "";
                QuantityBridge quantityBridge = new QuantityBridge();
                bool quantityChanged = false;
                bool unitsChanged = false;

                switch (phdto.itemType)
                {
                    case "Paper":
                        var paper = _context.Papers.Find(phdto.itemId);
                        if (paper == null) return (false, "الورق غير موجود");
                        oldQuantity = paper.Quantity;
                        itemName = paper.Name;

                        quantityChanged = (paper.Quantity != phdto.newQuantity);
                        paper.Quantity = phdto.newQuantity;

                        quantityBridge.PaperId = phdto.itemId;
                        quantityBridge.Quantity = phdto.newQuantity;
                        quantityBridge.Price = paper.Price;
                        quantityBridge.TotalBalance = phdto.newQuantity * paper.Price;
                        quantityBridge.OldQuantity = oldQuantity;
                        quantityBridge.OldPrice = paper.Price;
                        quantityBridge.OldTotalBalance = oldQuantity * paper.Price;

                        paper.TotalBalance = phdto.newQuantity * paper.Price;
                        break;

                    case "Ink":
                        var ink = _context.Inks.Find(phdto.itemId);
                        if (ink == null) return (false, "الحبر غير موجود");
                        oldQuantity = ink.Quantity;
                        oldNumberOfUnits = ink.NumberOfUnits;
                        itemName = ink.Name;

                        quantityChanged = (ink.Quantity != phdto.newQuantity);
                        unitsChanged = (ink.NumberOfUnits != phdto.newNumberOfUnits);
                        ink.Quantity = phdto.newQuantity;
                        ink.NumberOfUnits = phdto.newNumberOfUnits;

                        quantityBridge.InkId = phdto.itemId;
                        quantityBridge.Quantity = phdto.newQuantity;
                        quantityBridge.NumberOfUnits = phdto.newNumberOfUnits;
                        quantityBridge.UnitPrice = ink.UnitPrice;
                        quantityBridge.Price = ink.Price;
                        quantityBridge.TotalBalance = phdto.newNumberOfUnits * ink.UnitPrice;
                        quantityBridge.OldQuantity = oldQuantity;
                        quantityBridge.OldNumberOfUnits = oldNumberOfUnits;
                        quantityBridge.OldPrice = ink.Price;
                        quantityBridge.OldTotalBalance = oldNumberOfUnits * ink.UnitPrice;

                        if (phdto.newNumberOfUnits != null)
                            ink.TotalBalance = phdto.newNumberOfUnits * ink.UnitPrice;
                        break;

                    case "Supply":
                        var supply = _context.Supplies.Find(phdto.itemId);
                        if (supply == null) return (false, "المستلزمات غير موجودة");
                        oldQuantity = supply.Quantity;
                        itemName = supply.Name;

                        quantityChanged = (supply.Quantity != phdto.newQuantity);
                        supply.Quantity = phdto.newQuantity;

                        quantityBridge.SuppliesId = phdto.itemId;
                        quantityBridge.Quantity = phdto.newQuantity;
                        quantityBridge.Price = supply.Price;
                        quantityBridge.TotalBalance = phdto.newQuantity * supply.Price;
                        quantityBridge.OldQuantity = oldQuantity;
                        quantityBridge.OldPrice = supply.Price;
                        quantityBridge.OldTotalBalance = oldQuantity * supply.Price;

                        supply.TotalBalance = phdto.newQuantity * supply.Price;
                        break;

                    case "Spare Parts":
                        var spareParts = _context.SpareParts.Find(phdto.itemId);
                        if (spareParts == null) return (false, " قطع الصيانة غير موجودة");
                        oldQuantity = spareParts.Quantity;
                        itemName = spareParts.Name;

                        quantityChanged = (spareParts.Quantity != phdto.newQuantity);
                        spareParts.Quantity = phdto.newQuantity;

                        quantityBridge.SparePartsId = phdto.itemId;
                        quantityBridge.Quantity = phdto.newQuantity;
                        quantityBridge.Price = spareParts.Price;
                        quantityBridge.TotalBalance = phdto.newQuantity * spareParts.Price;
                        quantityBridge.OldQuantity = oldQuantity;
                        quantityBridge.OldPrice = spareParts.Price;
                        quantityBridge.OldTotalBalance = oldQuantity * spareParts.Price;

                        spareParts.TotalBalance = phdto.newQuantity * spareParts.Price;
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

                //string message = $"تم تعديل كمية {itemName} من {oldQuantity} إلى {phdto.newQuantity}";
                //if (phdto.itemType == "Ink")
                //{
                //    message += $" وعدد السحبات من {oldNumberOfUnits} إلى {phdto.newNumberOfUnits}";
                //}
                string message = $"تم تعديل {itemName}: ";

                if (quantityChanged)
                {
                    message += $"الكمية من {oldQuantity} إلى {phdto.newQuantity}";
                }

                if (phdto.itemType == "Ink" && unitsChanged)
                {
                    if (quantityChanged) message += " و ";
                    message += $"عدد الوحدات من {oldNumberOfUnits} إلى {phdto.newNumberOfUnits}";
                }

                if (!quantityChanged && !unitsChanged)
                {
                    message = "لم يتم إجراء أي تعديلات على القيم";
                }

                return (true, message);

                //return (true, $"تم تعديل كمية {itemName} من {oldQuantity} إلى {phdto.newQuantity}");
            }
            catch (ArgumentException ex)
            {
                return (false, $"حدث خطأ: {ex.ToString()}");
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return (false, $"حدث خطأ: {ex.ToString()}");
            }
        }

        public InventoryReportViewModel invetoryReports(string itemType, int itemId, DateOnly beginingDate, DateOnly endingDate)
        {
            try
            {
                List<QuantityBridge> quantityBridgeList = new List<QuantityBridge>();

                List<PurchaseOrder> purchaseOrderList = new List<PurchaseOrder>();
                List<RequisiteOrder> requisiteOrderList = new List<RequisiteOrder>();
                List<ReturnsOrder> returnOrderList = new List<ReturnsOrder>();
                List<PhysicalCountOrder> physicalCountList = new List<PhysicalCountOrder>();
                List<PerpetualRequisiteOrder> perpetualOrderList = new List<PerpetualRequisiteOrder>();

                List<ModifiedPurchaseOrder> modifiedPurchaseList = new List<ModifiedPurchaseOrder>();
                List<ModifiedRequisiteOrder> modifiedRequisiteList = new List<ModifiedRequisiteOrder>();
                List<ModifiedReturnsOrder> modifiedReturnList = new List<ModifiedReturnsOrder>();
                List<ModifiedPhysicalCountOrder> modifiedPhysicalList = new List<ModifiedPhysicalCountOrder>();
                List<ModifiedPerpetualRequisiteOrder> modifiedPerpetualList = new List<ModifiedPerpetualRequisiteOrder>();




                switch (itemType)
                {
                    case "Paper":
                        quantityBridgeList = _context.QuantityBridges.Where(q => q.PaperId == itemId).ToList();

                        foreach (QuantityBridge qb in quantityBridgeList)
                        {
                            List<PurchaseOrder> purorders = _context.PurchaseOrders.Where(p =>
                                        p.PurchaseId == qb.PurchaseId
                                        && p.PurchaseDate >= beginingDate
                                        && p.PurchaseDate <= endingDate
                                        )
                            .ToList();

                            List<PhysicalCountOrder> phyOrders = _context.PhysicalCountOrders.Where(p =>
                                       p.PhysicalCountId == qb.PhysicalCountId
                                       && p.PhysicalCountDate >= beginingDate
                                        && p.PhysicalCountDate <= endingDate
                                        ).ToList();

                            List<RequisiteOrder> reqOrders = _context.RequisiteOrders.Where(req =>
                                       req.RequisiteId == qb.RequisiteId
                                       && req.RequisiteDate >= beginingDate
                                        && req.RequisiteDate <= endingDate
                                       ).ToList();

                            List<ReturnsOrder> retOrders = _context.ReturnsOrders.Where(ret =>
                                       ret.ReturnId == qb.ReturnId
                                       && ret.ReturnDate >= beginingDate
                                        && ret.ReturnDate <= endingDate
                                       ).ToList();


                            purchaseOrderList.AddRange(purorders);
                            physicalCountList.AddRange(phyOrders);
                            returnOrderList.AddRange(retOrders);
                            requisiteOrderList.AddRange(reqOrders);
                        }
                        break;

                    case "Ink":
                        quantityBridgeList = _context.QuantityBridges.Where(q => q.InkId == itemId).ToList();
                        foreach (QuantityBridge qb in quantityBridgeList)
                        {
                            List<PurchaseOrder> purorders = _context.PurchaseOrders.Where(p =>
                                        p.PurchaseId == qb.PurchaseId
                                        && p.PurchaseDate >= beginingDate
                                        && p.PurchaseDate <= endingDate
                                        )
                            .ToList();

                            List<PhysicalCountOrder> phyOrders = _context.PhysicalCountOrders.Where(p =>
                                       p.PhysicalCountId == qb.PhysicalCountId
                                       && p.PhysicalCountDate >= beginingDate
                                        && p.PhysicalCountDate <= endingDate
                                        ).ToList();

                            List<RequisiteOrder> reqOrders = _context.RequisiteOrders.Where(req =>
                                       req.RequisiteId == qb.RequisiteId
                                       && req.RequisiteDate >= beginingDate
                                        && req.RequisiteDate <= endingDate
                                       ).ToList();

                            List<ReturnsOrder> retOrders = _context.ReturnsOrders.Where(ret =>
                                       ret.ReturnId == qb.ReturnId
                                       && ret.ReturnDate >= beginingDate
                                        && ret.ReturnDate <= endingDate
                                       ).ToList();
                            List<PerpetualRequisiteOrder> perpOrders = _context.PerpetualRequisiteOrders.Where(
                                perp => perp.PerpetualRequisiteId == qb.PerpetualRequisiteId
                                && perp.PerpetualRequisiteDate >= beginingDate
                                && perp.PerpetualRequisiteDate <= endingDate
                                ).ToList();

                            purchaseOrderList.AddRange(purorders);
                            physicalCountList.AddRange(phyOrders);
                            returnOrderList.AddRange(retOrders);
                            requisiteOrderList.AddRange(reqOrders);
                            perpetualOrderList.AddRange(perpOrders);
                        }
                        break;

                    case "Supply":

                        quantityBridgeList = _context.QuantityBridges.Where(q => q.SuppliesId == itemId).ToList();
                        foreach (QuantityBridge qb in quantityBridgeList)
                        {
                            List<PurchaseOrder> purorders = _context.PurchaseOrders.Where(p =>
                                        p.PurchaseId == qb.PurchaseId
                                        && p.PurchaseDate >= beginingDate
                                        && p.PurchaseDate <= endingDate
                                        )
                            .ToList();

                            List<PhysicalCountOrder> phyOrders = _context.PhysicalCountOrders.Where(p =>
                                       p.PhysicalCountId == qb.PhysicalCountId
                                       && p.PhysicalCountDate >= beginingDate
                                        && p.PhysicalCountDate <= endingDate
                                        ).ToList();

                            List<RequisiteOrder> reqOrders = _context.RequisiteOrders.Where(req =>
                                       req.RequisiteId == qb.RequisiteId
                                       && req.RequisiteDate >= beginingDate
                                        && req.RequisiteDate <= endingDate
                                       ).ToList();

                            List<ReturnsOrder> retOrders = _context.ReturnsOrders.Where(ret =>
                                       ret.ReturnId == qb.ReturnId
                                       && ret.ReturnDate >= beginingDate
                                        && ret.ReturnDate <= endingDate
                                       ).ToList();


                            purchaseOrderList.AddRange(purorders);
                            physicalCountList.AddRange(phyOrders);
                            returnOrderList.AddRange(retOrders);
                            requisiteOrderList.AddRange(reqOrders);
                        }


                        break;

                    case "Spare":

                        quantityBridgeList = _context.QuantityBridges.Where(qb => qb.SparePartsId == itemId).ToList();

                        foreach (QuantityBridge qb in quantityBridgeList)
                        {
                            List<PurchaseOrder> purorders = _context.PurchaseOrders.Where(p =>
                                        p.PurchaseId == qb.PurchaseId
                                        && p.PurchaseDate >= beginingDate
                                        && p.PurchaseDate <= endingDate
                                        )
                            .ToList();

                            List<PhysicalCountOrder> phyOrders = _context.PhysicalCountOrders.Where(p =>
                                       p.PhysicalCountId == qb.PhysicalCountId
                                       && p.PhysicalCountDate >= beginingDate
                                        && p.PhysicalCountDate <= endingDate
                                        ).ToList();

                            List<RequisiteOrder> reqOrders = _context.RequisiteOrders.Where(req =>
                                       req.RequisiteId == qb.RequisiteId
                                       && req.RequisiteDate >= beginingDate
                                        && req.RequisiteDate <= endingDate
                                       ).ToList();

                            List<ReturnsOrder> retOrders = _context.ReturnsOrders.Where(ret =>
                                       ret.ReturnId == qb.ReturnId
                                       && ret.ReturnDate >= beginingDate
                                        && ret.ReturnDate <= endingDate
                                       ).ToList();

                            List<PerpetualRequisiteOrder> perpOrders = _context.PerpetualRequisiteOrders.Where(
                                perp => perp.PerpetualRequisiteId == qb.PerpetualRequisiteId
                                && perp.PerpetualRequisiteDate >= beginingDate
                                && perp.PerpetualRequisiteDate <= endingDate
                                ).ToList();

                            purchaseOrderList.AddRange(purorders);
                            physicalCountList.AddRange(phyOrders);
                            returnOrderList.AddRange(retOrders);
                            requisiteOrderList.AddRange(reqOrders);
                            perpetualOrderList.AddRange(perpOrders);
                        }

                        break;


                    default:
                        break;
                }

                InventoryReportViewModel invViewModel = new InventoryReportViewModel();
                invViewModel.purchaseOrderList = purchaseOrderList;
                invViewModel.requisiteOrderList = requisiteOrderList;
                invViewModel.returnOrderList = returnOrderList;
                invViewModel.physicalCountList = physicalCountList;
                invViewModel.perpetualOrderList = perpetualOrderList;

                switch (itemType)
                {
                    case "Spare":
                        SparePart spare = _context.SpareParts.FirstOrDefault(s => s.SparePartsId == itemId);
                        invViewModel.itemName = spare.Name;
                        invViewModel.itemType = "قطع غيار";
                        invViewModel.itemQuantity = spare.Quantity;
                        invViewModel.itemPrice = spare.Price;
                        invViewModel.itemTotalBalance = spare.TotalBalance ?? 0;
                        break;
                    case "Supply":
                        Supply supply = _context.Supplies.FirstOrDefault(s => s.SuppliesId == itemId);
                        invViewModel.itemName = supply.Name;
                        invViewModel.itemType = "مسلتزمات";
                        invViewModel.itemQuantity = supply.Quantity;
                        invViewModel.itemPrice = supply.Price;
                        invViewModel.itemTotalBalance = supply.TotalBalance ?? 0;
                        break;
                    case "Ink":
                        Ink ink = _context.Inks.FirstOrDefault(s => s.InkId == itemId);
                        invViewModel.itemName = ink.Name;
                        invViewModel.itemType = "حبر";
                        invViewModel.itemQuantity = ink.Quantity;
                        invViewModel.itemPrice = ink.Price;
                        invViewModel.itemTotalBalance = ink.TotalBalance ?? 0;
                        break;
                    case "Paper":
                        Paper paper = _context.Papers.FirstOrDefault(s => s.PaperId == itemId);
                        invViewModel.itemName = paper.Name;
                        invViewModel.itemType = "ورق";
                        invViewModel.itemQuantity = paper.Quantity;
                        invViewModel.itemPrice = paper.Price;
                        invViewModel.itemTotalBalance = paper.TotalBalance ?? 0;
                        break;

                }

                invViewModel.quantityBridgeList = quantityBridgeList;

                invViewModel.modifiedPurchaseOrderList = modifiedPurchaseList;
                invViewModel.modifiedRequisiteOrderList = modifiedRequisiteList;
                invViewModel.modifiedPerpetualRequisiteOrdersList = modifiedPerpetualList;
                invViewModel.modifiedReturnsOrderList = modifiedReturnList;
                invViewModel.modifiedPhysicalCountOrderList = modifiedPhysicalList;


                switch (itemType)
                {
                    case "Ink":
                        foreach (PurchaseOrder po in purchaseOrderList)
                        {

                            ModifiedPurchaseOrder modifiedPurchase = new ModifiedPurchaseOrder();
                            modifiedPurchase.PurchaseId = po.PurchaseId;
                            modifiedPurchase.PurchaseDate = po.PurchaseDate;
                            modifiedPurchase.RemainingAmount = po.RemainingAmount;
                            modifiedPurchase.PurchaseNotes = po.PurchaseNotes;
                            modifiedPurchase.PaidAmount = po.PaidAmount;
                            modifiedPurchase.VendorId = po.VendorId;
                            modifiedPurchase.Vendorname = _context.Vendors.FirstOrDefault(v => v.VendorId == po.VendorId).VendorName;
                            modifiedPurchase.EmployeeId = po.EmployeeId;
                            modifiedPurchase.EmployeeName = _context.Employees.FirstOrDefault(e => e.EmployeeId == po.EmployeeId).EmployeeName;
                            modifiedPurchase.balance = quantityBridgeList.FirstOrDefault(qb => qb.PurchaseId == po.PurchaseId).NumberOfUnits;
                            modifiedPurchase.price = quantityBridgeList.FirstOrDefault(qb => qb.PurchaseId == po.PurchaseId).UnitPrice ?? 0;
                            modifiedPurchaseList.Add(modifiedPurchase);
                        }
                        foreach (RequisiteOrder ro in requisiteOrderList)
                        {

                            ModifiedRequisiteOrder modifiedRequisite = new ModifiedRequisiteOrder();
                            modifiedRequisite.RequisiteId = ro.RequisiteId;
                            modifiedRequisite.RequisiteDate = ro.RequisiteDate;
                            modifiedRequisite.EmployeeId = ro.EmployeeId;
                            modifiedRequisite.JobOrderId = ro.JobOrderId;
                            modifiedRequisite.RequisiteNotes = ro.RequisiteNotes;
                            modifiedRequisite.EmployeeName = _context.Employees.FirstOrDefault(e => e.EmployeeId == ro.EmployeeId).EmployeeName;
                            modifiedRequisite.balance = quantityBridgeList.FirstOrDefault(qb => qb.RequisiteId == ro.RequisiteId).Quantity;
                            modifiedRequisite.price = quantityBridgeList.FirstOrDefault(qb => qb.RequisiteId == ro.RequisiteId).Price;

                            modifiedRequisiteList.Add(modifiedRequisite);
                        }
                        foreach (ReturnsOrder ro in returnOrderList)
                        {

                            ModifiedReturnsOrder modifiedReturn = new ModifiedReturnsOrder();
                            modifiedReturn.ReturnId = ro.ReturnId;
                            modifiedReturn.ReturnDate = ro.ReturnDate;
                            modifiedReturn.EmployeeId = ro.EmployeeId;
                            modifiedReturn.EmployeeName = _context.Employees.FirstOrDefault(e => e.EmployeeId == ro.EmployeeId).EmployeeName;
                            modifiedReturn.ReturnInOut = ro.ReturnInOut;
                            modifiedReturn.ReturnsNotes = ro.ReturnsNotes;
                            if (modifiedReturn.ReturnInOut == true)
                            {
                                modifiedReturn.balance = quantityBridgeList.FirstOrDefault(qb => qb.ReturnId == ro.ReturnId).Quantity;
                                modifiedReturn.price = quantityBridgeList.FirstOrDefault(qb => qb.ReturnId == ro.ReturnId).Price;
                            }
                            else
                            {
                                modifiedReturn.balance = quantityBridgeList.FirstOrDefault(qb => qb.ReturnId == ro.ReturnId).NumberOfUnits;
                                modifiedReturn.price = quantityBridgeList.FirstOrDefault(qb => qb.ReturnId == ro.ReturnId).UnitPrice ?? 0;

                            }
                            modifiedReturnList.Add(modifiedReturn);
                        }
                        foreach (PhysicalCountOrder ph in physicalCountList)
                        {
                            ModifiedPhysicalCountOrder modifiedPhysical = new ModifiedPhysicalCountOrder();
                            modifiedPhysical.PhysicalCountId = ph.PhysicalCountId;
                            modifiedPhysical.PhysicalCountDate = ph.PhysicalCountDate;
                            modifiedPhysical.EmployeeId = ph.EmployeeId;
                            modifiedPhysical.EmployeeName = _context.Employees.FirstOrDefault(e => e.EmployeeId == ph.EmployeeId).EmployeeName;
                            modifiedPhysical.oldBalance = quantityBridgeList.FirstOrDefault(qb => qb.PhysicalCountId == ph.PhysicalCountId).OldNumberOfUnits ?? 0;
                            modifiedPhysical.balance = quantityBridgeList.FirstOrDefault(qb => qb.PhysicalCountId == ph.PhysicalCountId).NumberOfUnits;

                            modifiedPhysicalList.Add(modifiedPhysical);
                        }
                        foreach (PerpetualRequisiteOrder pr in perpetualOrderList)
                        {

                            ModifiedPerpetualRequisiteOrder modifiedPerpetual = new ModifiedPerpetualRequisiteOrder();
                            modifiedPerpetual.PerpetualRequisiteId = pr.PerpetualRequisiteId;
                            modifiedPerpetual.PerpetualRequisiteDate = pr.PerpetualRequisiteDate;
                            modifiedPerpetual.EmployeeId = pr.EmployeeId;
                            modifiedPerpetual.EmployeeName = _context.Employees.FirstOrDefault(e => e.EmployeeId == pr.EmployeeId).EmployeeName;
                            modifiedPerpetual.price = quantityBridgeList.FirstOrDefault(qb => qb.PerpetualRequisiteId == pr.PerpetualRequisiteId).UnitPrice ?? 0;
                            modifiedPerpetual.balance = quantityBridgeList.FirstOrDefault(qb => qb.PerpetualRequisiteId == pr.PerpetualRequisiteId).NumberOfUnits;

                            modifiedPerpetualList.Add(modifiedPerpetual);
                        }
                        break;
                    default:
                        foreach (PurchaseOrder po in purchaseOrderList)
                        {

                            ModifiedPurchaseOrder modifiedPurchase = new ModifiedPurchaseOrder();
                            modifiedPurchase.PurchaseId = po.PurchaseId;
                            modifiedPurchase.PurchaseDate = po.PurchaseDate;
                            modifiedPurchase.RemainingAmount = po.RemainingAmount;
                            modifiedPurchase.PurchaseNotes = po.PurchaseNotes;
                            modifiedPurchase.PaidAmount = po.PaidAmount;
                            modifiedPurchase.VendorId = po.VendorId;
                            modifiedPurchase.Vendorname = _context.Vendors.FirstOrDefault(v => v.VendorId == po.VendorId).VendorName;
                            modifiedPurchase.EmployeeId = po.EmployeeId;
                            modifiedPurchase.EmployeeName = _context.Employees.FirstOrDefault(e => e.EmployeeId == po.EmployeeId).EmployeeName;
                            modifiedPurchase.balance = quantityBridgeList.FirstOrDefault(qb => qb.PurchaseId == po.PurchaseId).Quantity;
                            modifiedPurchase.price = quantityBridgeList.FirstOrDefault(qb => qb.PurchaseId == po.PurchaseId).Price;
                            modifiedPurchaseList.Add(modifiedPurchase);
                        }
                        foreach (RequisiteOrder ro in requisiteOrderList)
                        {

                            ModifiedRequisiteOrder modifiedRequisite = new ModifiedRequisiteOrder();
                            modifiedRequisite.RequisiteId = ro.RequisiteId;
                            modifiedRequisite.RequisiteDate = ro.RequisiteDate;
                            modifiedRequisite.EmployeeId = ro.EmployeeId;
                            modifiedRequisite.JobOrderId = ro.JobOrderId;
                            modifiedRequisite.RequisiteNotes = ro.RequisiteNotes;
                            modifiedRequisite.EmployeeName = _context.Employees.FirstOrDefault(e => e.EmployeeId == ro.EmployeeId).EmployeeName;
                            modifiedRequisite.balance = quantityBridgeList.FirstOrDefault(qb => qb.RequisiteId == ro.RequisiteId).Quantity;
                            modifiedRequisite.price = quantityBridgeList.FirstOrDefault(qb => qb.RequisiteId == ro.RequisiteId).Price;

                            modifiedRequisiteList.Add(modifiedRequisite);
                        }
                        foreach (ReturnsOrder ro in returnOrderList)
                        {

                            ModifiedReturnsOrder modifiedReturn = new ModifiedReturnsOrder();
                            modifiedReturn.ReturnId = ro.ReturnId;
                            modifiedReturn.ReturnDate = ro.ReturnDate;
                            modifiedReturn.EmployeeId = ro.EmployeeId;
                            modifiedReturn.EmployeeName = _context.Employees.FirstOrDefault(e => e.EmployeeId == ro.EmployeeId).EmployeeName;
                            modifiedReturn.ReturnInOut = ro.ReturnInOut;
                            modifiedReturn.ReturnsNotes = ro.ReturnsNotes;
                            modifiedReturn.balance = quantityBridgeList.FirstOrDefault(qb => qb.ReturnId == ro.ReturnId).Quantity;
                            modifiedReturn.price = quantityBridgeList.FirstOrDefault(qb => qb.ReturnId == ro.ReturnId).Price;
                            modifiedReturnList.Add(modifiedReturn);
                        }
                        foreach (PhysicalCountOrder ph in physicalCountList)
                        {

                            ModifiedPhysicalCountOrder modifiedPhysical = new ModifiedPhysicalCountOrder();
                            modifiedPhysical.PhysicalCountId = ph.PhysicalCountId;
                            modifiedPhysical.PhysicalCountDate = ph.PhysicalCountDate;
                            modifiedPhysical.EmployeeId = ph.EmployeeId;
                            modifiedPhysical.EmployeeName = _context.Employees.FirstOrDefault(e => e.EmployeeId == ph.EmployeeId).EmployeeName;
                            modifiedPhysical.oldBalance = quantityBridgeList.FirstOrDefault(qb => qb.PhysicalCountId == ph.PhysicalCountId).OldQuantity ?? 0;
                            modifiedPhysical.balance = quantityBridgeList.FirstOrDefault(qb => qb.PhysicalCountId == ph.PhysicalCountId).Quantity;

                            modifiedPhysicalList.Add(modifiedPhysical);
                        }
                        foreach (PerpetualRequisiteOrder pr in perpetualOrderList)
                        {

                            ModifiedPerpetualRequisiteOrder modifiedPerpetual = new ModifiedPerpetualRequisiteOrder();
                            modifiedPerpetual.PerpetualRequisiteId = pr.PerpetualRequisiteId;
                            modifiedPerpetual.PerpetualRequisiteDate = pr.PerpetualRequisiteDate;
                            modifiedPerpetual.EmployeeId = pr.EmployeeId;
                            modifiedPerpetual.EmployeeName = _context.Employees.FirstOrDefault(e => e.EmployeeId == pr.EmployeeId).EmployeeName;
                            modifiedPerpetual.price = quantityBridgeList.FirstOrDefault(qb => qb.PerpetualRequisiteId == pr.PerpetualRequisiteId).Price;
                            modifiedPerpetual.balance = quantityBridgeList.FirstOrDefault(qb => qb.PerpetualRequisiteId == pr.PerpetualRequisiteId).Quantity;

                            modifiedPerpetualList.Add(modifiedPerpetual);
                        }
                        break;
                }

                return (invViewModel);
            }
            catch (ArgumentException ex)
            {
                return new InventoryReportViewModel();
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return new InventoryReportViewModel();
            }
        }


        public InventoryReportViewModel VendorReportRanking(DateOnly beginningDate, DateOnly endDate)
        {
            try
            {
                // Retrieve purchase orders within the specified date range
                var purchaseOrdersInRange = _context.PurchaseOrders
                    .Where(po => po.PurchaseDate >= beginningDate && po.PurchaseDate <= endDate)
                    .ToList();


                var vendorPurchases = (from po in purchaseOrdersInRange
                                       join qb in _context.QuantityBridges on po.PurchaseId equals qb.PurchaseId
                                       group qb by po.VendorId into g
                                       select new
                                       {
                                           VendorId = g.Key,
                                           PurchaseCount = g.Select(q => q.PurchaseId).Distinct().Count(),
                                           TotalOldBalance = g.Sum(q => (q.Price) * (q.Quantity))
                                           //TotalOldBalance = g.Sum(q => q.TotalBalance)

                                       })
                             .ToList();

                // Join with vendor details and apply ranking based on both criteria
                var vendorReport = (from vp in vendorPurchases
                                    join vendor in _context.Vendors on vp.VendorId equals vendor.VendorId
                                    orderby vp.PurchaseCount descending, vp.TotalOldBalance descending
                                    select new
                                    {
                                        Vendor = vendor,
                                        PurchaseCount = vp.PurchaseCount,
                                        TotalOldBalance = vp.TotalOldBalance
                                    })
                         .ToList();


                InventoryReportViewModel invModel = new InventoryReportViewModel
                {
                    VendorReport = vendorReport.Select(v => (v.Vendor, v.PurchaseCount, v.TotalOldBalance)).ToList()
                };

                return invModel;
            }
            catch (ArgumentException ex)
            {
                return new InventoryReportViewModel();
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return new InventoryReportViewModel();
            }

        }

        public (bool success, string message) PurchaseAll(purchaseOrderDTO purchaseDTO)
        {

            try
            {
                decimal purchaseOrderRemainingBalance = 0;
                List<QuantityBridge> quantityBridgeList = purchaseDTO.BridgeList;

                PurchaseOrder purchaseOrder = new PurchaseOrder();
                purchaseOrder.EmployeeId = purchaseDTO.EmployeeId;
                purchaseOrder.VendorId = purchaseDTO.VendorId;
                purchaseOrder.PurchaseNotes = purchaseDTO.PurchaseNotes;
                _context.PurchaseOrders.Add(purchaseOrder);
                _context.SaveChanges();

                int purchaseOrderNumber = _context.PurchaseOrders
                          .OrderByDescending(po => po.PurchaseId)
                          .Select(po => po.PurchaseId)
                          .FirstOrDefault();

                for (int i = 0; i < quantityBridgeList.Count; i++)
                {
                    quantityBridgeList[i].PurchaseId = purchaseOrderNumber;
                    quantityBridgeList[i].QuantityBridgeId = null;

                    if (quantityBridgeList[i].InkId != null)
                    {
                        Ink ink = _context.Inks.FirstOrDefault(p => p.InkId == quantityBridgeList[i].InkId);

                        quantityBridgeList[i].UnitPrice = quantityBridgeList[i].Price;
                        // quantity bridge calculations 

                        quantityBridgeList[i].TotalBalance = quantityBridgeList[i].UnitPrice * quantityBridgeList[i].NumberOfUnits;
                        decimal totalValue = (decimal)quantityBridgeList[i].TotalBalance + (decimal)ink.TotalBalance;
                        decimal purchaseValue = (decimal)quantityBridgeList[i].NumberOfUnits * (decimal)quantityBridgeList[i].UnitPrice;
                        purchaseOrderRemainingBalance += purchaseValue;
                        decimal averageUnitPrice = totalValue / (ink.NumberOfUnits + quantityBridgeList[i].NumberOfUnits);
                        decimal averagequantityPrice = totalValue / (ink.Quantity + (quantityBridgeList[i].NumberOfUnits * ink.AverageQuantity));

                        int totalNumberOfUnits = quantityBridgeList[i].NumberOfUnits + ink.NumberOfUnits;

                        int newInkQuantity = quantityBridgeList[i].NumberOfUnits * ink.AverageQuantity;

                        // update to the old data in the bridge
                        quantityBridgeList[i].OldPrice = ink.Price;
                        quantityBridgeList[i].OldQuantity = ink.Quantity;
                        quantityBridgeList[i].OldTotalBalance = ink.TotalBalance;

                        // Update paper properties
                        ink.Quantity += newInkQuantity;
                        ink.Price = averagequantityPrice;
                        ink.UnitPrice = averageUnitPrice;
                        ink.NumberOfUnits = totalNumberOfUnits;
                        ink.TotalBalance = totalNumberOfUnits * averageUnitPrice;

                        _context.Inks.Update(ink);
                        _context.QuantityBridges.Add(quantityBridgeList[i]);
                        _context.SaveChanges();

                    }
                    else if (quantityBridgeList[i].PaperId != null)
                    {
                        Paper paper = _context.Papers.FirstOrDefault(p => p.PaperId == quantityBridgeList[i].PaperId);

                        // Calculate new quantity and average price
                        double totalQuantity = paper.Quantity + quantityBridgeList[i].Quantity;
                        decimal totalValue = (decimal)paper.Quantity * paper.Price +
                                             (decimal)quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;
                        decimal averagePrice = totalValue / (decimal)totalQuantity;

                        decimal newtotalBalance = quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;

                        quantityBridgeList[i].TotalBalance = newtotalBalance;
                        purchaseOrderRemainingBalance += newtotalBalance;
                        quantityBridgeList[i].TotalBalance = newtotalBalance;
                        // update to the old data in the bridge
                        quantityBridgeList[i].OldPrice = paper.Price;
                        quantityBridgeList[i].OldQuantity = paper.Quantity;
                        quantityBridgeList[i].OldTotalBalance = paper.TotalBalance;

                        // Update paper properties
                        paper.Quantity = (int)totalQuantity;
                        paper.Price = averagePrice;
                        paper.TotalBalance = (decimal)totalQuantity * averagePrice;

                        _context.Papers.Update(paper);
                        _context.QuantityBridges.Add(quantityBridgeList[i]);
                        _context.SaveChanges();

                    }
                    else if (quantityBridgeList[i].SuppliesId != null)
                    {
                        Supply supply = _context.Supplies.FirstOrDefault(p => p.SuppliesId == quantityBridgeList[i].SuppliesId);

                        // Calculate new quantity and average price
                        double totalQuantity = supply.Quantity + quantityBridgeList[i].Quantity;
                        decimal totalValue = (decimal)supply.Quantity * supply.Price +
                                             (decimal)quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;
                        decimal averagePrice = totalValue / (decimal)totalQuantity;


                        decimal newtotalBalance = quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;
                        purchaseOrderRemainingBalance += newtotalBalance;
                        quantityBridgeList[i].TotalBalance = newtotalBalance;

                        // update to the old data in the bridge
                        quantityBridgeList[i].OldPrice = supply.Price;
                        quantityBridgeList[i].OldQuantity = supply.Quantity;
                        quantityBridgeList[i].OldTotalBalance = supply.TotalBalance;

                        // Update paper properties
                        supply.Quantity = (int)totalQuantity;
                        supply.Price = averagePrice;
                        supply.TotalBalance = (decimal)totalQuantity * averagePrice;
                        _context.Supplies.Update(supply);
                        _context.QuantityBridges.Add(quantityBridgeList[i]);
                        _context.SaveChanges();

                    }
                    else if (quantityBridgeList[i].SparePartsId != null)
                    {

                        SparePart sparepart = _context.SpareParts.FirstOrDefault(sp => sp.SparePartsId == quantityBridgeList[i].SparePartsId);

                        //calculate new quantity
                        double totalQuantity = sparepart.Quantity + quantityBridgeList[i].Quantity;
                        decimal totalValue = (decimal)sparepart.Quantity * sparepart.Price +
                                             (decimal)quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;
                        decimal averagePrice = totalValue / (decimal)totalQuantity;


                        decimal newtotalBalance = quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;
                        purchaseOrderRemainingBalance += newtotalBalance;
                        quantityBridgeList[i].TotalBalance = newtotalBalance;

                        // update to the old data in the bridge
                        quantityBridgeList[i].OldPrice = sparepart.Price;
                        quantityBridgeList[i].OldQuantity = sparepart.Quantity;
                        quantityBridgeList[i].OldTotalBalance = sparepart.TotalBalance;

                        // Update paper properties
                        sparepart.Quantity = (int)totalQuantity;
                        sparepart.Price = averagePrice;
                        sparepart.TotalBalance = (decimal)totalQuantity * averagePrice;
                        _context.SpareParts.Update(sparepart);
                        _context.QuantityBridges.Add(quantityBridgeList[i]);
                        _context.SaveChanges();

                    }

                }
                PurchaseOrder purch = _context.PurchaseOrders.FirstOrDefault(p => p.PurchaseId == purchaseOrderNumber);
                purch.RemainingAmount = purchaseOrderRemainingBalance;
                _context.PurchaseOrders.Update(purch);
                _context.SaveChanges();
                return (true, "تمت عملية الشراء بنجاح");
            }
            catch (ArgumentException ex)
            {
                return (false, $"حدث خطأ: {ex.ToString()}");
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return (false, $"حدث خطأ: {ex.ToString()}");
            }



        }

        public InventoryReportViewModel GetCustomerRanking(DateOnly beginningDate, DateOnly endDate)
        {
            try
            {
                // Retrieve JobOrders within the specified date range
                var jobOrdersInRange = _context.JobOrders
                    .Where(jo => jo.StartDate >= beginningDate && jo.EndDate <= endDate)
                    .ToList();


                var customerRankings = (from jo in jobOrdersInRange
                                        where jo.CustomerId != null  // ensure we only include valid customers
                                        group jo by jo.CustomerId into g
                                        select new
                                        {
                                            CustomerId = g.Key,
                                            OrderCount = g.Count(),
                                            TotalBalance = g.Sum(x => x.EarnedRevenue ?? 0),
                                            unearnedBalance = g.Sum(x => x.UnearnedRevenue ?? 0),
                                            RemainingBalance = g.Sum(x => x.RemainingAmount ?? 0),
                                        }).ToList();

                // Join the aggregated data with Customers to get full customer details
                var rankedCustomers = (from cr in customerRankings
                                       join c in _context.Customers on cr.CustomerId equals c.CustomerId
                                       orderby cr.OrderCount descending, cr.TotalBalance descending
                                       select new
                                       {
                                           Customer = c,
                                           OrderCount = cr.OrderCount,
                                           TotalBalance = cr.TotalBalance,
                                           unearnedBalance = cr.unearnedBalance,
                                           RemainingBalance = cr.RemainingBalance
                                       }).ToList();

                // Prepare and return the view model
                InventoryReportViewModel rankingModel = new InventoryReportViewModel
                {
                    CustomerReport = rankedCustomers
                        .Select(x => (x.Customer, x.OrderCount, x.TotalBalance, x.unearnedBalance, x.RemainingBalance))
                        .ToList()
                };

                return rankingModel;
            }
            catch (ArgumentException ex)
            {
                return new InventoryReportViewModel();
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return new InventoryReportViewModel();
            }
        }


        public ReturnOrderDTO processSelection(ReturnOrderDTO returnDto)
        {
            try
            {
                // purchase order
                if (returnDto.ReturnInOut == false)
                {
                    List<QuantityBridge> purchasedItems = _context.QuantityBridges
                        //.Include(q => q.PurchaseId)
                        .Where(q => q.PurchaseId == returnDto.purchaseID)
                        .ToList();
                    returnDto.ListOfordered = purchasedItems;
                }
                else if (returnDto.ReturnInOut == true)
                {
                    // job order
                    List<QuantityBridge> requisitedItems = _context.QuantityBridges
                        //.Include(q => q.RequisiteId)
                        .Where(q => q.RequisiteId == returnDto.JobOrderId)
                        .ToList();
                    returnDto.ListOfordered = requisitedItems;
                }
                return returnDto;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return new ReturnOrderDTO();
            }

        }

        public List<JobOrder> GetRecentJobOrdersWithCustomers()
        {
            try
            {
                var today = DateOnly.FromDateTime(DateTime.Now);
                var fromDate = today.AddDays(-30);

                return _context.JobOrders
                    .Include(j => j.Customer)
                    .Where(j => j.StartDate != null && j.StartDate >= fromDate && j.StartDate <= today)
                    .OrderByDescending(j => j.StartDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return new List<JobOrder>();
            }
        }

        public List<PurchaseOrder> GetRecentPurchaseOrderwithSuppliers()
        {
            try
            {
                var today = DateOnly.FromDateTime(DateTime.Now);
                var fromDate = today.AddDays(-30);

                return _context.PurchaseOrders
                    .Include(p => p.Vendor)
                    .Where(p => p.PurchaseDate != null && p.PurchaseDate >= fromDate && p.PurchaseDate <= today)
                    .OrderByDescending(p => p.PurchaseDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return new List<PurchaseOrder>();
            }
        }

        public List<Employee> GetActiveEmployees() => _context.Employees.Where(e => e.Activated).ToList();

        public List<object> GetJobOrderItems(int jobOrderId)
        {
            try
            {

                var paperItems = _context.QuantityBridges
                    .Where(qb => qb.Requisite.JobOrderId == jobOrderId && qb.PaperId != null)
                    .Join(_context.Papers, qb => qb.PaperId, paper => paper.PaperId,
                        (qb, paper) => new
                        {
                            itemType = "الورق",
                            itemId = paper.PaperId,
                            name = paper.Name,

                            quantity = qb.Quantity
                        })
                    .ToList();

                var inkItems = _context.QuantityBridges
                    .Where(qb => qb.Requisite.JobOrderId == jobOrderId && qb.InkId != null)
                    .Join(_context.Inks, qb => qb.InkId, ink => ink.InkId,
                        (qb, ink) => new
                        {
                            itemType = "الحبر",
                            itemId = ink.InkId,
                            name = ink.Name,

                            quantity = qb.Quantity
                        })
                    .ToList();

                var supplyItems = _context.QuantityBridges
                    .Where(qb => qb.Requisite.JobOrderId == jobOrderId && qb.SuppliesId != null)
                    .Join(_context.Supplies, qb => qb.SuppliesId, supply => supply.SuppliesId,
                        (qb, supply) => new
                        {
                            itemType = "المستلزمات",
                            itemId = supply.SuppliesId,
                            name = supply.Name,

                            quantity = qb.Quantity
                        })
                    .ToList();


                var allItems = new List<object>();
                allItems.AddRange(paperItems);
                allItems.AddRange(inkItems);
                allItems.AddRange(supplyItems);

                return allItems;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return new List<object>();
            }
        }

        public List<object> GetPurchaseOrderItems(int purchaseId)
        {
            try
            {
                var paperItems = _context.QuantityBridges
                    .Where(qb => qb.PurchaseId == purchaseId && qb.PaperId != null)
                    .Join(_context.Papers, qb => qb.PaperId, paper => paper.PaperId,
                        (qb, paper) => new
                        {
                            itemType = "الورق",
                            itemId = paper.PaperId,
                            name = paper.Name,
                            quantity = qb.Quantity
                        })
                    .ToList();

                var inkItems = _context.QuantityBridges
                    .Where(qb => qb.PurchaseId == purchaseId && qb.InkId != null)
                    .Join(_context.Inks, qb => qb.InkId, ink => ink.InkId,
                        (qb, ink) => new
                        {
                            itemType = "الحبر",
                            itemId = ink.InkId,
                            name = ink.Name,
                            quantity = qb.NumberOfUnits
                        })
                    .ToList();

                var supplyItems = _context.QuantityBridges
                    .Where(qb => qb.PurchaseId == purchaseId && qb.SuppliesId != null)
                    .Join(_context.Supplies, qb => qb.SuppliesId, supply => supply.SuppliesId,
                        (qb, supply) => new
                        {
                            itemType = "المستلزمات",
                            itemId = supply.SuppliesId,
                            name = supply.Name,
                            quantity = qb.Quantity
                        })
                    .ToList();
                var sparepartItems = _context.QuantityBridges
                .Where(qb => qb.PurchaseId == purchaseId && qb.SparePartsId != null)
                .Join(_context.SpareParts, qb => qb.SparePartsId, SparePart => SparePart.SparePartsId,
                    (qb, SparePart) => new
                    {
                        itemType = "قطع غيار",
                        itemId = SparePart.SparePartsId,
                        name = SparePart.Name,
                        quantity = qb.Quantity
                    })
                .ToList();

                var allItems = new List<object>();
                allItems.AddRange(paperItems);
                allItems.AddRange(inkItems);
                allItems.AddRange(supplyItems);
                allItems.AddRange(sparepartItems);

                return allItems;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return new List<object>();
            }
        }

        public (bool success, string message) ReturnOrder(ReturnOrderDTO returnDTO)
        {

            try
            {
                List<QuantityBridge> quantityBridgeList = returnDTO.BridgeList;
                ReturnsOrder returnOrder = new ReturnsOrder
                {
                    EmployeeId = returnDTO.EmployeeId,
                    PurchaseId = returnDTO.purchaseID,
                    JobOrderId = returnDTO.JobOrderId,
                    ReturnsNotes = returnDTO.ReturnsNotes,
                    ReturnInOut = returnDTO.ReturnInOut,
                };

                _context.ReturnsOrders.Add(returnOrder);
                _context.SaveChanges();

                int returnOrderNumber = _context.ReturnsOrders
                        .OrderByDescending(po => po.ReturnId)
                        .Select(po => po.ReturnId)
                        .FirstOrDefault();

                decimal totalReturnValue = 0;
                for (int i = 0; i < quantityBridgeList.Count; i++)
                {
                    quantityBridgeList[i].ReturnId = returnOrderNumber;
                    quantityBridgeList[i].QuantityBridgeId = null;

                    if (returnOrder.ReturnInOut == true && returnOrder.JobOrderId != null)
                    {
                        if (quantityBridgeList[i].InkId != null)
                        {
                            var ink = _context.Inks.FirstOrDefault(p => p.InkId == quantityBridgeList[i].InkId);
                            if (ink != null)
                            {
                                quantityBridgeList[i].OldPrice = ink.Price;
                                quantityBridgeList[i].OldQuantity = ink.Quantity;
                                quantityBridgeList[i].OldTotalBalance = ink.TotalBalance;

                                ink.Quantity += quantityBridgeList[i].Quantity;


                                quantityBridgeList[i].Price = (decimal)quantityBridgeList[i].OldPrice;


                                quantityBridgeList[i].TotalBalance = quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;
                                //ink.TotalBalance = ink.Quantity * ink.Price;


                                _context.Inks.Update(ink);
                                _context.QuantityBridges.Add(quantityBridgeList[i]);
                            }
                        }
                        else if (quantityBridgeList[i].PaperId != null)
                        {
                            var paper = _context.Papers.FirstOrDefault(p => p.PaperId == quantityBridgeList[i].PaperId);
                            if (paper != null)
                            {
                                quantityBridgeList[i].OldPrice = paper.Price;
                                quantityBridgeList[i].OldQuantity = paper.Quantity;
                                quantityBridgeList[i].OldTotalBalance = paper.TotalBalance;

                                paper.Quantity += quantityBridgeList[i].Quantity;


                                quantityBridgeList[i].Price = (decimal)quantityBridgeList[i].OldPrice;


                                quantityBridgeList[i].TotalBalance = quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;
                                paper.TotalBalance = quantityBridgeList[i].TotalBalance;
                                paper.TotalBalance = paper.Quantity * paper.Price;



                                _context.Papers.Update(paper);
                                _context.QuantityBridges.Add(quantityBridgeList[i]);
                            }
                        }
                        else if (quantityBridgeList[i].SuppliesId != null)
                        {
                            var supply = _context.Supplies.FirstOrDefault(p => p.SuppliesId == quantityBridgeList[i].SuppliesId);
                            if (supply != null)
                            {
                                quantityBridgeList[i].OldPrice = supply.Price;
                                quantityBridgeList[i].OldQuantity = supply.Quantity;
                                quantityBridgeList[i].OldTotalBalance = supply.TotalBalance;

                                supply.Quantity += quantityBridgeList[i].Quantity;


                                quantityBridgeList[i].Price = (decimal)quantityBridgeList[i].OldPrice;
                                quantityBridgeList[i].TotalBalance = quantityBridgeList[i].Quantity * quantityBridgeList[i].Price;
                                supply.TotalBalance = supply.Quantity * supply.Price;


                                _context.Supplies.Update(supply);
                                _context.QuantityBridges.Add(quantityBridgeList[i]);
                            }
                        }
                    }

                    if (returnOrder.ReturnInOut == false && returnOrder.PurchaseId != null)
                    {

                        var quantityBridge = _context.QuantityBridges
                                            .Where(qb => qb.PurchaseId == returnOrder.PurchaseId)
                                            .ToList();

                        foreach (var qb in quantityBridge)
                        {

                            if (quantityBridgeList[i].InkId != null && qb.InkId == quantityBridgeList[i].InkId)
                            {
                                var ink = _context.Inks.FirstOrDefault(p => p.InkId == quantityBridgeList[i].InkId);
                                if (ink != null)
                                {
                                    decimal? itemPrice = qb.UnitPrice;
                                    decimal? itemReturnValue = itemPrice * quantityBridgeList[i].NumberOfUnits;
                                    totalReturnValue += (decimal)itemReturnValue;

                                    var avgPrice = _context.QuantityBridges
                                        .Where(x => x.InkId == ink.InkId && x.PurchaseId != null)
                                        .Average(x => x.UnitPrice);


                                    quantityBridgeList[i].OldPrice = ink.UnitPrice;
                                    quantityBridgeList[i].OldNumberOfUnits = ink.NumberOfUnits;
                                    quantityBridgeList[i].OldTotalBalance = ink.TotalBalance;
                                    quantityBridgeList[i].UnitPrice = avgPrice;


                                    ink.NumberOfUnits -= quantityBridgeList[i].NumberOfUnits;

                                    quantityBridgeList[i].TotalBalance = quantityBridgeList[i].NumberOfUnits * avgPrice;
                                    ink.TotalBalance = ink.NumberOfUnits * ink.UnitPrice;


                                    _context.Inks.Update(ink);
                                    _context.QuantityBridges.Add(quantityBridgeList[i]);
                                }
                            }

                            else if (quantityBridgeList[i].PaperId != null && qb.PaperId == quantityBridgeList[i].PaperId)
                            {
                                var paper = _context.Papers.FirstOrDefault(p => p.PaperId == quantityBridgeList[i].PaperId);
                                if (paper != null)
                                {
                                    decimal itemPrice = qb.Price;
                                    decimal itemReturnValue = itemPrice * quantityBridgeList[i].Quantity;
                                    totalReturnValue += itemReturnValue;

                                    var avgPrice = _context.QuantityBridges
                                        .Where(x => x.PaperId == paper.PaperId && x.PurchaseId != null)
                                        .Average(x => x.Price);


                                    quantityBridgeList[i].OldPrice = paper.Price;
                                    quantityBridgeList[i].OldQuantity = paper.Quantity;
                                    quantityBridgeList[i].OldTotalBalance = paper.TotalBalance;

                                    quantityBridgeList[i].Price = avgPrice;


                                    paper.Quantity -= quantityBridgeList[i].Quantity;

                                    quantityBridgeList[i].TotalBalance = quantityBridgeList[i].Quantity * avgPrice;
                                    paper.TotalBalance = paper.Quantity * paper.Price;


                                    _context.Papers.Update(paper);
                                    _context.QuantityBridges.Add(quantityBridgeList[i]);
                                }
                            }

                            else if (quantityBridgeList[i].SuppliesId != null && qb.SuppliesId == quantityBridgeList[i].SuppliesId)
                            {
                                var supply = _context.Supplies.FirstOrDefault(p => p.SuppliesId == quantityBridgeList[i].SuppliesId);
                                if (supply != null)
                                {
                                    decimal itemPrice = qb.Price;
                                    decimal itemReturnValue = itemPrice * quantityBridgeList[i].Quantity;
                                    totalReturnValue += itemReturnValue;

                                    var avgPrice = _context.QuantityBridges
                                 .Where(x => x.SuppliesId == supply.SuppliesId && x.PurchaseId != null)
                                  .Average(x => x.Price);


                                    quantityBridgeList[i].OldPrice = supply.Price;
                                    quantityBridgeList[i].OldQuantity = supply.Quantity;
                                    quantityBridgeList[i].OldTotalBalance = supply.TotalBalance;

                                    quantityBridgeList[i].Price = avgPrice;


                                    supply.Quantity -= quantityBridgeList[i].Quantity;
                                    quantityBridgeList[i].TotalBalance = quantityBridgeList[i].Quantity * avgPrice;
                                    supply.TotalBalance = supply.Quantity * supply.Price;


                                    _context.Supplies.Update(supply);
                                    _context.QuantityBridges.Add(quantityBridgeList[i]);
                                }
                            }
                            else if (quantityBridgeList[i].SparePartsId != null && qb.SparePartsId == quantityBridgeList[i].SparePartsId)
                            {
                                var sparePart = _context.SpareParts.FirstOrDefault(s => s.SparePartsId == quantityBridgeList[i].SparePartsId);
                                if (sparePart != null)
                                {
                                    decimal itemPrice = qb.Price;
                                    decimal itemReturnValue = itemPrice * quantityBridgeList[i].Quantity;
                                    totalReturnValue += itemReturnValue;

                                    var avgPrice = _context.QuantityBridges
                                 .Where(x => x.SparePartsId == sparePart.SparePartsId && x.PurchaseId != null)
                                  .Average(x => x.Price);


                                    quantityBridgeList[i].OldPrice = sparePart.Price;
                                    quantityBridgeList[i].OldQuantity = sparePart.Quantity;
                                    quantityBridgeList[i].OldTotalBalance = sparePart.TotalBalance;

                                    quantityBridgeList[i].Price = avgPrice;


                                    sparePart.Quantity -= quantityBridgeList[i].Quantity;
                                    quantityBridgeList[i].TotalBalance = quantityBridgeList[i].Quantity * avgPrice;
                                    sparePart.TotalBalance = sparePart.Quantity * sparePart.Price;



                                    _context.SpareParts.Update(sparePart);
                                    _context.QuantityBridges.Add(quantityBridgeList[i]);
                                }
                            }

                        }
                    }
                }
                _context.SaveChanges();

                if (returnOrder.ReturnInOut == false && returnOrder.PurchaseId != null)
                {
                    var purchaseOrder = _context.PurchaseOrders.FirstOrDefault(p => p.PurchaseId == returnOrder.PurchaseId);
                    if (purchaseOrder != null)
                    {
                        purchaseOrder.RemainingAmount -= totalReturnValue;

                        if (purchaseOrder.RemainingAmount < 0)
                        {
                            purchaseOrder.RemainingAmount = 0;
                        }

                        _context.PurchaseOrders.Update(purchaseOrder);
                        _context.SaveChanges();
                    }
                }


                return (true, "تم المرتجع بنجاح");
            }

            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return (false, $"حدث خطأ: {ex.ToString()}");
            }
        }
        public (bool success, string message) ReturnOrder2(ReturnOrderDTO returnDTO)
        {
            try
            {
                List<QuantityBridge> quantityBridgeList = returnDTO.BridgeList;
                ReturnsOrder returnOrder = new ReturnsOrder
                {
                    EmployeeId = returnDTO.EmployeeId,
                    PurchaseId = returnDTO.purchaseID,
                    JobOrderId = returnDTO.JobOrderId,
                    ReturnsNotes = returnDTO.ReturnsNotes,
                    ReturnInOut = returnDTO.ReturnInOut,
                };

                _context.ReturnsOrders.Add(returnOrder);
                _context.SaveChanges();

                int returnOrderNumber = _context.ReturnsOrders
                        .OrderByDescending(po => po.ReturnId)
                        .Select(po => po.ReturnId)
                        .FirstOrDefault();

                for (int i = 0; i < quantityBridgeList.Count; i++)
                {
                    quantityBridgeList[i].ReturnId = returnOrderNumber;
                    quantityBridgeList[i].QuantityBridgeId = null;


                    if (returnOrder.ReturnInOut == true && returnOrder.JobOrderId != null)
                    {
                        if (quantityBridgeList[i].InkId != null)
                        {
                            var ink = _context.Inks.FirstOrDefault(p => p.InkId == quantityBridgeList[i].InkId);
                            if (ink != null)
                            {

                                quantityBridgeList[i].OldPrice = ink.Price;
                                quantityBridgeList[i].OldQuantity = ink.Quantity;
                                quantityBridgeList[i].OldTotalBalance = ink.TotalBalance;


                                ink.Quantity += quantityBridgeList[i].Quantity;


                                _context.Inks.Update(ink);
                                _context.QuantityBridges.Add(quantityBridgeList[i]);
                                _context.SaveChanges();
                            }
                        }
                        else if (quantityBridgeList[i].PaperId != null)
                        {
                            var paper = _context.Papers.FirstOrDefault(p => p.PaperId == quantityBridgeList[i].PaperId);
                            if (paper != null)
                            {

                                quantityBridgeList[i].OldPrice = paper.Price;
                                quantityBridgeList[i].OldQuantity = paper.Quantity;
                                quantityBridgeList[i].OldTotalBalance = paper.TotalBalance;


                                paper.Quantity += quantityBridgeList[i].Quantity;


                                _context.Papers.Update(paper);
                                _context.QuantityBridges.Add(quantityBridgeList[i]);
                                _context.SaveChanges();
                            }
                        }
                        else if (quantityBridgeList[i].SuppliesId != null)
                        {
                            var supply = _context.Supplies.FirstOrDefault(p => p.SuppliesId == quantityBridgeList[i].SuppliesId);
                            if (supply != null)
                            {

                                quantityBridgeList[i].OldPrice = supply.Price;
                                quantityBridgeList[i].OldQuantity = supply.Quantity;
                                quantityBridgeList[i].OldTotalBalance = supply.TotalBalance;


                                supply.Quantity += quantityBridgeList[i].Quantity;

                                _context.Supplies.Update(supply);
                                _context.QuantityBridges.Add(quantityBridgeList[i]);
                                _context.SaveChanges();
                            }
                        }
                    }


                    else if (!returnOrder.ReturnInOut == false && returnOrder.PurchaseId != null)
                    {
                        if (quantityBridgeList[i].InkId != null)
                        {
                            var ink = _context.Inks.FirstOrDefault(p => p.InkId == quantityBridgeList[i].InkId);
                            if (ink != null)
                            {

                                quantityBridgeList[i].OldPrice = ink.Price;
                                quantityBridgeList[i].OldQuantity = ink.Quantity;
                                quantityBridgeList[i].OldTotalBalance = ink.TotalBalance;


                                ink.Quantity -= quantityBridgeList[i].Quantity;

                                _context.Inks.Update(ink);
                                _context.QuantityBridges.Add(quantityBridgeList[i]);
                                _context.SaveChanges();
                            }
                        }
                        else if (quantityBridgeList[i].PaperId != null)
                        {
                            var paper = _context.Papers.FirstOrDefault(p => p.PaperId == quantityBridgeList[i].PaperId);
                            if (paper != null)
                            {

                                quantityBridgeList[i].OldPrice = paper.Price;
                                quantityBridgeList[i].OldQuantity = paper.Quantity;
                                quantityBridgeList[i].OldTotalBalance = paper.TotalBalance;


                                paper.Quantity -= quantityBridgeList[i].Quantity;

                                _context.Papers.Update(paper);
                                _context.QuantityBridges.Add(quantityBridgeList[i]);
                                _context.SaveChanges();
                            }
                        }
                        else if (quantityBridgeList[i].SuppliesId != null)
                        {
                            var supply = _context.Supplies.FirstOrDefault(p => p.SuppliesId == quantityBridgeList[i].SuppliesId);
                            if (supply != null)
                            {

                                quantityBridgeList[i].OldPrice = supply.Price;
                                quantityBridgeList[i].OldQuantity = supply.Quantity;
                                quantityBridgeList[i].OldTotalBalance = supply.TotalBalance;


                                supply.Quantity -= quantityBridgeList[i].Quantity;


                                _context.Supplies.Update(supply);
                                _context.QuantityBridges.Add(quantityBridgeList[i]);
                                _context.SaveChanges();
                            }
                        }
                    }
                }

                return (true, "تم المرتجع بنجاح");
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return (false, $"حدث خطأ: {ex.ToString()}");
            }
        }

        public bool editCharacteristic(int CharId, ColorWeightSize CWS)
        {
            try
            {
                var existing = _context.ColorWeightSizes.FirstOrDefault(c => c.ColorWeightSizeId == CharId);
                if (existing == null) return false;

                existing.Type = CWS.Type;


                if (CWS.Type == 0)
                {
                    existing.Colored = CWS.Colored;
                    existing.Weight = 0;
                    existing.Size = null;
                }
                else if (CWS.Type == 1)
                {
                    existing.Weight = CWS.Weight;
                    existing.Size = null;
                    existing.Colored = null;
                }
                else if (CWS.Type == 2)
                {
                    existing.Size = CWS.Size;
                    existing.Colored = null;
                    existing.Weight = 0;
                }

                _context.Update(existing);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
            }
        }

        public ColorWeightSize GetCharacteristicByID(int CharId)
        {
            try
            {
                ColorWeightSize foundCWS = _context.ColorWeightSizes.FirstOrDefault(p => p.ColorWeightSizeId == CharId);
                if (foundCWS == null)
                {
                    throw new ArgumentException("Characteristic not found.");
                }
                return foundCWS;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        public bool addCharacteristic(ColorWeightSize CWS)
        {
            try
            {
                var newCharacteristic = new ColorWeightSize
                {
                    Type = CWS.Type
                };


                if (CWS.Type == 0)
                {
                    newCharacteristic.Colored = CWS.Colored;
                    newCharacteristic.Weight = 0;
                    newCharacteristic.Size = null;
                }
                else if (CWS.Type == 1)
                {
                    newCharacteristic.Weight = CWS.Weight;
                    newCharacteristic.Size = null;
                    newCharacteristic.Colored = null;
                }
                else if (CWS.Type == 2)
                {
                    newCharacteristic.Size = CWS.Size;
                    newCharacteristic.Colored = null;
                    newCharacteristic.Weight = 0;
                }

                _context.ColorWeightSizes.Add(newCharacteristic);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
            }
        }

        public List<ColorWeightSize> ViewAllColorWeightSize()
        {
            try
            {
                List<ColorWeightSize> colorWeightSizeList = _context.ColorWeightSizes.ToList();
                return colorWeightSizeList;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return new List<ColorWeightSize>();
            }
        }

        public List<MachineStore> ViewAllMachineStore()
        {
            try
            {
                List<MachineStore> machineStoreList = _context.MachineStores.ToList();
                return machineStoreList;
            }
            catch (ArgumentException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        public List<SparePart> ViewAllSpareParts()
        {
            try
            {
                List<SparePart> sparePartsList = _context.SpareParts.ToList();
                return sparePartsList;
            }
            catch (ArgumentException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        public MachineStore getMachineByID(int machineID)
        {

            try
            {

                MachineStore machine = _context.MachineStores.FirstOrDefault(machine => machine.MachineStoreId == machineID);
                return machine;

            }
            catch (Exception ex)
            {

                WriteException.WriteExceptionToFile(ex);
                return null;
            }


        }

        public bool editMachineStore(int machineID, MachineStore newmachine)
        {

            try
            {

                MachineStore oldmachine = _context.MachineStores.FirstOrDefault(mac => mac.MachineStoreId == machineID);
                if (oldmachine != null)
                {

                    oldmachine.Name = newmachine.Name;
                    oldmachine.Activated = newmachine.Activated;

                    _context.MachineStores.Update(oldmachine);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception exc)
            {
                WriteException.WriteExceptionToFile(exc);
                return false;

            }

        }

        public SparePart getSparePartByID(int sparePartsID)
        {

            try
            {

                SparePart sparepart = _context.SpareParts.FirstOrDefault(sp => sp.SparePartsId == sparePartsID);
                return sparepart;

            }
            catch (Exception ex)
            {

                WriteException.WriteExceptionToFile(ex);
                return null;
            }


        }

        public bool editSpareParts(int sparePartID, SparePart newSparePart)
        {

            try
            {

                SparePart oldSparePart = _context.SpareParts.FirstOrDefault(sp => sp.SparePartsId == sparePartID);

                if (oldSparePart == null)
                {
                    return false;
                }
                else
                {

                    oldSparePart.Name = newSparePart.Name;
                    oldSparePart.ReorderPoint = newSparePart.ReorderPoint;
                    oldSparePart.Activated = newSparePart.Activated;

                    _context.SpareParts.Update(oldSparePart);
                    _context.SaveChanges();
                    return true;

                }


            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
            }


        }

        public bool AddMachine(MachineStore machine)
        {
            try
            {
                if (machine == null)
                {
                    throw new ArgumentNullException(nameof(machine));
                }

                MachineStore addedMachine = new MachineStore();

                addedMachine.Name = machine.Name;
                addedMachine.Activated = true;
                _context.MachineStores.Add(addedMachine);
                _context.SaveChanges();

                return true;
            }
            catch (ArgumentException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
            }
        }

        public bool AddSparePart(SparePart spareparts)
        {
            try
            {
                if (spareparts == null)
                {
                    throw new ArgumentNullException(nameof(spareparts));
                }

                SparePart addedSparePart = new SparePart();

                addedSparePart.Name = spareparts.Name;
                addedSparePart.Price = spareparts.Price;
                addedSparePart.Quantity = spareparts.Quantity;
                addedSparePart.TotalBalance = spareparts.Price * spareparts.Quantity;
                addedSparePart.ReorderPoint = spareparts.ReorderPoint;
                addedSparePart.Activated = true;
                _context.SpareParts.Add(addedSparePart);
                _context.SaveChanges();

                return true;
            }
            catch (ArgumentException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
            }
        }

        public List<MachineStore> GetActiveMachines()
        {
            try
            {
                return _context.MachineStores
                     .Where(ms => (bool)ms.Activated)
                     .ToList();
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return new List<MachineStore>();
            }
        }

        public (bool success, string message) PerpetualRequisite(PerpetualRequisiteDTO perpetualDTO)
        {

            try
            {

                List<QuantityBridge> quantityBridgeList = perpetualDTO.BridgeList;

                PerpetualRequisiteOrder perpetualRequisite = new PerpetualRequisiteOrder();
                perpetualRequisite.EmployeeId = perpetualDTO.EmployeeId;
                perpetualRequisite.MachineStoreId = perpetualDTO.MachineStoreId;
                perpetualRequisite.RequisiteNotes = perpetualDTO.RequisiteNotes;
                _context.PerpetualRequisiteOrders.Add(perpetualRequisite);
                _context.SaveChanges();

                int perpetualRequisiteOrderNumber = _context.PerpetualRequisiteOrders
                          .OrderByDescending(pr => pr.PerpetualRequisiteId)
                          .Select(pr => pr.PerpetualRequisiteId)
                          .FirstOrDefault();

                for (int i = 0; i < quantityBridgeList.Count; i++)
                {
                    quantityBridgeList[i].PerpetualRequisiteId = perpetualRequisiteOrderNumber;
                    quantityBridgeList[i].QuantityBridgeId = null;

                    if (quantityBridgeList[i].InkId != null)
                    {

                        Ink ink = _context.Inks.FirstOrDefault(p => p.InkId == quantityBridgeList[i].InkId);

                        // Calculate new quantity and average price
                        double newNumberOfUnits = ink.NumberOfUnits - quantityBridgeList[i].NumberOfUnits;
                        decimal totalValue = (decimal)newNumberOfUnits * ink.UnitPrice;

                        quantityBridgeList[i].UnitPrice = ink.UnitPrice;
                        quantityBridgeList[i].OldNumberOfUnits = ink.NumberOfUnits;
                        quantityBridgeList[i].OldTotalBalance = ink.TotalBalance;
                        quantityBridgeList[i].TotalBalance = quantityBridgeList[i].NumberOfUnits * ink.UnitPrice;

                        // Update paper properties
                        ink.NumberOfUnits = (int)newNumberOfUnits;
                        //ink.Price = averagePrice;
                        ink.TotalBalance = totalValue;

                        _context.Inks.Update(ink);
                        _context.QuantityBridges.Add(quantityBridgeList[i]);
                        _context.SaveChanges();

                    }
                    else if (quantityBridgeList[i].SparePartsId != null)
                    {
                        SparePart sparePart = _context.SpareParts.FirstOrDefault(sp => sp.SparePartsId == quantityBridgeList[i].SparePartsId);

                        // Calculate new quantity and average price
                        double newQuantity = sparePart.Quantity - quantityBridgeList[i].Quantity;
                        decimal totalValue = (decimal)newQuantity * sparePart.Price;

                        quantityBridgeList[i].OldPrice = sparePart.Price;
                        quantityBridgeList[i].OldQuantity = sparePart.Quantity;
                        quantityBridgeList[i].OldTotalBalance = sparePart.TotalBalance;
                        quantityBridgeList[i].Price = sparePart.Price;
                        quantityBridgeList[i].TotalBalance = quantityBridgeList[i].Quantity * sparePart.Price;

                        sparePart.Quantity = (int)newQuantity;
                        sparePart.TotalBalance = totalValue;
                        _context.SpareParts.Update(sparePart);
                        _context.QuantityBridges.Add(quantityBridgeList[i]);
                        _context.SaveChanges();

                    }


                }
                return (true, "تمت انشاء اذن الصرف بنجاح");
            }

            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return (false, $"حدث خطأ: {ex.ToString()}");
            }
        }

        public List<PurchaseOrderVM> ViewAllPurchaseOrder()                                              
 {
            try
            {
                List<PurchaseOrder> purchaseordersList = _context.PurchaseOrders.OrderByDescending(p => p.PurchaseDate).ToList();
                List<Employee> employeesList = _context.Employees.ToList();
                List<Vendor> vendorsList = _context.Vendors.ToList();
                List<PurchaseOrderVM> purchaseOrderVMsList = new List<PurchaseOrderVM>();

                foreach (PurchaseOrder purchaseOrder in purchaseordersList)
                {
                    Vendor vendor = vendorsList.FirstOrDefault(v => v.VendorId == purchaseOrder.VendorId);
                    Employee employee = employeesList.FirstOrDefault(e => e.EmployeeId == purchaseOrder.EmployeeId);

                    if (vendor != null && employee != null)
                    {
                        PurchaseOrderVM purchasetoView = new PurchaseOrderVM();


                        purchasetoView.VendorId = vendor.VendorId;
                        purchasetoView.VendorName = vendor.VendorName;
                        purchasetoView.VendorEmail = vendor.VendorEmail;
                        purchasetoView.VendorAddress = vendor.VendorAddress;
                        purchasetoView.VendorNotes = vendor.VendorNotes;


                        purchasetoView.PurchaseId = purchaseOrder.PurchaseId;
                        purchasetoView.PurchaseDate = purchaseOrder.PurchaseDate;
                        purchasetoView.RemainingAmount = purchaseOrder.RemainingAmount;
                        purchasetoView.PaidAmount = purchaseOrder.PaidAmount;
                        purchasetoView.PurchaseNotes = purchaseOrder.PurchaseNotes;

                        purchasetoView.VendorId = vendor.VendorId;

                        // third add the employee data
                        purchasetoView.EmployeeId = purchasetoView.EmployeeId;
                        purchasetoView.EmployeeName = purchasetoView.EmployeeName;

                        purchaseOrderVMsList.Add(purchasetoView);
                    }
                }
                return purchaseOrderVMsList;
            }

            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }
        public PurchaseOrderSpecificationsViewModel ShowPurchaseOrderSpecifications(int purchaseId)
        {
            try
            {
                PurchaseOrder purchaseOrder = _context.PurchaseOrders.FirstOrDefault(p => p.PurchaseId == purchaseId);
                if (purchaseOrder == null) return null;

                PurchaseOrderSpecificationsViewModel vm = new PurchaseOrderSpecificationsViewModel
                {
                    PurchaseId = purchaseOrder.PurchaseId,
                    RemainingAmount = purchaseOrder.RemainingAmount,
                    PaidAmount = purchaseOrder.PaidAmount,
                    PurchaseNotes = purchaseOrder.PurchaseNotes,
                    EmployeeId = purchaseOrder.EmployeeId,
                    VendorId = purchaseOrder.VendorId
                };

                Vendor vend = vm.VendorId.HasValue
                    ? _context.Vendors.FirstOrDefault(v => v.VendorId == vm.VendorId)
                    : null;
                vm.VendorName = vend?.VendorName;

                List<Employee> employees = new List<Employee>();
                if (purchaseOrder.EmployeeId != null)
                    employees.Add(_context.Employees.FirstOrDefault(e => e.EmployeeId == purchaseOrder.EmployeeId));
                vm.Employees = employees.Where(e => e != null).ToList();

                List<ReturnsOrder> returnOrders = _context.ReturnsOrders
                    .Where(r => r.PurchaseId == purchaseId)
                    .ToList();
                vm.ReturnOrders = returnOrders;

                List<int> returnIds = returnOrders.Select(r => r.ReturnId).ToList();

                var allBridges = _context.QuantityBridges
                    .Where(q => q.PurchaseId == purchaseId || (q.ReturnId != null && returnIds.Contains(q.ReturnId.Value)))
                    .ToList();

                var purchased = allBridges.Where(q => q.PurchaseId != null).ToList();
                var returned = allBridges.Where(q => q.ReturnId != null).ToList();

                List<QuantityBridge> combined = new List<QuantityBridge>();

                foreach (var group in purchased.GroupBy(q =>
                    q.InkId != null ? $"Ink-{q.InkId}" :
                    q.PaperId != null ? $"Paper-{q.PaperId}" :
                    q.SuppliesId != null ? $"Supply-{q.SuppliesId}" :
                    q.SparePartsId != null ? $"Spare-{q.SparePartsId}" : "Other"))
                {
                    var first = group.First();
                    var newBridge = new QuantityBridge
                    {
                        PurchaseId = first.PurchaseId,
                        InkId = first.InkId,
                        PaperId = first.PaperId,
                        SuppliesId = first.SuppliesId,
                        SparePartsId = first.SparePartsId,
                        Quantity = first.InkId != null ? 0 : group.Sum(x => x.Quantity),
                        NumberOfUnits = first.InkId != null ? group.Sum(x => x.NumberOfUnits) : 0,
                        Price = first.InkId != null ? 0 : first.Price,
                        UnitPrice = first.InkId != null ? first.UnitPrice : 0
                    };
                    combined.Add(newBridge);
                }

                foreach (var group in returned.GroupBy(q =>
                    q.InkId != null ? $"Ink-{q.InkId}" :
                    q.PaperId != null ? $"Paper-{q.PaperId}" :
                    q.SuppliesId != null ? $"Supply-{q.SuppliesId}" :
                    q.SparePartsId != null ? $"Spare-{q.SparePartsId}" : "Other"))
                {
                    var first = group.First();
                    var newBridge = new QuantityBridge
                    {
                        ReturnId = first.ReturnId,
                        InkId = first.InkId,
                        PaperId = first.PaperId,
                        SuppliesId = first.SuppliesId,
                        SparePartsId = first.SparePartsId,
                        Quantity = first.InkId != null ? 0 : group.Sum(x => x.Quantity),
                        NumberOfUnits = first.InkId != null ? group.Sum(x => x.NumberOfUnits) : 0,
                        Price = first.InkId != null ? 0 : first.Price,
                        UnitPrice = first.InkId != null ? first.UnitPrice : 0
                    };
                    combined.Add(newBridge);
                }

                vm.QuantityBridge = combined;

                List<Paper> papers = new List<Paper>();
                List<Ink> inks = new List<Ink>();
                List<Supply> supplies = new List<Supply>();
                List<SparePart> spareParts = new List<SparePart>();

                foreach (var qb in combined)
                {
                    if (qb.InkId != null)
                    {
                        var ink = _context.Inks.FirstOrDefault(i => i.InkId == qb.InkId);
                        if (ink != null) inks.Add(ink);
                    }
                    else if (qb.PaperId != null)
                    {
                        var paper = _context.Papers.FirstOrDefault(p => p.PaperId == qb.PaperId);
                        if (paper != null) papers.Add(paper);
                    }
                    else if (qb.SuppliesId != null)
                    {
                        var supply = _context.Supplies.FirstOrDefault(s => s.SuppliesId == qb.SuppliesId);
                        if (supply != null) supplies.Add(supply);
                    }
                    else if (qb.SparePartsId != null)
                    {
                        var spare = _context.SpareParts.FirstOrDefault(s => s.SparePartsId == qb.SparePartsId);
                        if (spare != null) spareParts.Add(spare);
                    }
                }

                vm.Papers = papers;
                vm.Inks = inks;
                vm.Supplies = supplies;
                vm.SpareParts = spareParts;

                return vm;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }


    }
}
