﻿using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.InterfaceServices;
using ThothSystemVersion1.Models;
using ThothSystemVersion1.ModifiedModels;
using ThothSystemVersion1.Utilities;
using ThothSystemVersion1.ViewModels;
using Machine = ThothSystemVersion1.Models.Machine;

namespace ThothSystemVersion1.BusinessLogicLayers
{
    public class CostBusinessLogicLayer : CostService
    {

        private readonly ThothContext _context;


        public CostBusinessLogicLayer(ThothContext context)
        {
            _context = context;
        }

        public bool addMachineAndLabourExpense(MachineLabourDTO machlabdto)
        {
            try
            {
                List<ProcessBridge> proBrid = new List<ProcessBridge>();
                MiscellaneousExpense misc = new MiscellaneousExpense();
                misc.JobOrderId = machlabdto.JobOrderId;
                misc.EmployeeId = machlabdto.EmployeeId;
                misc.JobOrderId = machlabdto.JobOrderId;
                misc.MaterialProcessingExpense = machlabdto.MaterialProcessingExpense;
                misc.FilmsProcessingExpense = machlabdto.FilmsProcessingExpense;
                misc.MaterialsTotal = machlabdto.MaterialsTotal;
                misc.TotalAfterMaterials = machlabdto.TotalAfterMaterials;
                misc.AdminstrativeExpense = machlabdto.AdminstrativeExpense;
                misc.TotalExpenses = machlabdto.TotalExpenses;
                misc.Percentage = machlabdto.Percentage;
                misc.TotalAfterPercentage = machlabdto.TotalAfterPercentage;
                misc.MinistryOfFinance = machlabdto.MinistryOfFinance;
                misc.EmployeeImprovmentBox = machlabdto.EmployeeImprovmentBox;
                misc.ValueAddedTax = machlabdto.ValueAddedTax;
                misc.totalAfterEmplyeeImprovementbox = machlabdto.totalAfterEmplyeeImprovementbox;
                misc.FinalTotal = machlabdto.FinalTotal;
                _context.MiscellaneousExpenses.Add(misc);
                _context.SaveChanges();

                proBrid = machlabdto.processBridges;

                for (int i = 0; i < proBrid.Count; i++)
                {

                    if (proBrid[i].MachineId != null)
                    {
                        Machine machine = _context.Machines.FirstOrDefault(m => m.MachineId == proBrid[i].MachineId);

                        proBrid[i].MachineHourPrice = machine.Price;
                        proBrid[i].TotalMachineValue = machine.Price * proBrid[i].NumberOfHours;
                        proBrid[i].JobOrderId = machlabdto.JobOrderId;
                        //proBrid[i].OldMachinePrice = machine.Price;
                        proBrid[i].EmployeeId = machlabdto.EmployeeId;

                        _context.ProcessBridges.Add(proBrid[i]);
                        _context.SaveChanges();

                    }
                    else if (proBrid[i].LabourId != null)
                    {
                        Labour labour = _context.Labours.FirstOrDefault(m => m.LabourId == proBrid[i].LabourId);
                        proBrid[i].LabourHourPrice = labour.Price;
                        proBrid[i].TotalLabourValue = labour.Price * proBrid[i].NumberOfHours;
                        proBrid[i].JobOrderId = machlabdto.JobOrderId;
                        //proBrid[i].OldlabourPrice = labour.Price;
                        proBrid[i].EmployeeId = machlabdto.EmployeeId;

                        _context.ProcessBridges.Add(proBrid[i]);
                        _context.SaveChanges();

                    }


                    else
                    {
                        return false;

                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
            }
        }

        public bool addMiscelleneousExpense(int jobOrderId)
        {
            try
            {
                throw new NotImplementedException();

            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return false;
            }
        }
        public (bool Success, string Message) EditLabour(int LabourID, Labour lab)
        {
            try
            {
                if (lab == null)
                {
                    return (false, "بيانات العملية المطلوبة غير متوفرة.");
                }
                Labour exsistingLabour = _context.Labours.FirstOrDefault(a => a.LabourId == lab.LabourId);
                if (exsistingLabour == null)
                {
                    return (false, "العملية غير موجودة.");
                }
                exsistingLabour.LabourProcessName = lab.LabourProcessName;
                exsistingLabour.Price = lab.Price;
                exsistingLabour.Activated = lab.Activated;

                _context.Labours.Update(exsistingLabour); // Mark the entity as modified
                _context.SaveChanges(); // Save changes to the database
                return (true, $" تم تعديل العملية {exsistingLabour.LabourProcessName}"); // Success

            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return (false, $"حدث خطأ: {ex.Message}");
            }
        }

        public (bool Success, string Message) EditMachine(int MachineID, Machine machine)
        {
            try
            {
                if (machine == null)
                {
                    return (false, "بيانات الالة المطلوبة غير متوفرة.");
                }
                Machine exsistingMachine = _context.Machines.FirstOrDefault(a => a.MachineId == machine.MachineId);
                if (exsistingMachine == null)
                {
                    return (false, "الالة غير موجود.");
                }
                exsistingMachine.MachineProcessName = machine.MachineProcessName;
                exsistingMachine.Price = machine.Price;
                exsistingMachine.Activated = machine.Activated;

                _context.Machines.Update(exsistingMachine); // Mark the entity as modified
                _context.SaveChanges(); // Save changes to the database
                return (true, $" تم تعديل الالة  {exsistingMachine.MachineProcessName}"); // Success

            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return (false, $"حدث خطأ: {ex.Message}");
            }
        }
        public bool newLabour(Labour labour)
        {
            try
            {
                if (labour == null)
                {
                    throw new ArgumentNullException(nameof(labour));
                }
                Labour addedLabour = new Labour();
                addedLabour.LabourProcessName = labour.LabourProcessName;
                addedLabour.Price = labour.Price;
                addedLabour.Activated = true;
                _context.Labours.Add(addedLabour);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                WriteException.WriteExceptionToFile(ex);
                throw new ApplicationException("An error occurred while adding the labour.", ex);
            }
        }

        public bool newMachine(Machine machine)
        {
            try
            {
                if (machine == null)
                {
                    throw new ArgumentNullException(nameof(machine));
                }
                Machine addedMachine = new Machine();
                addedMachine.MachineProcessName = machine.MachineProcessName;
                addedMachine.Price = machine.Price;
                addedMachine.Activated = true;
                _context.Machines.Add(addedMachine);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                WriteException.WriteExceptionToFile(ex);
                throw new ApplicationException("An error occurred while adding the machine.", ex);
            }
        }

        public List<Labour> ViewAllLabour()
        {
            try
            {
                List<Labour> labourList = _context.Labours.ToList();
                return labourList;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;

            }
        }
        public List<Machine> ViewAllMachines()
        {
            try
            {
                List<Machine> machineList = _context.Machines.ToList();
                return machineList;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;

            }
        }
        public List<Machine> getActiveMachine()
        {
            try
            {

                List<Machine> machineList = _context.Machines.Where(mach => mach.Activated == true).ToList();
                return machineList;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;

            }
        }
        public List<Labour> getActiveLabour()
        {
            try
            {
                List<Labour> labourList = _context.Labours.Where(mach => mach.Activated == true).ToList();
                return labourList;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;

            }
        }
        public Labour GetLabourById(int id)
        {
            try
            {
                Labour labour = _context.Labours.Find(id); // Synchronous Find
                if (labour == null)
                {
                    throw new ArgumentException("Labour not found.");
                }
                return labour;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                throw new ApplicationException("An error occurred while fetching the labour.", ex);
            }
        }


        public Machine GetMachineById(int id)
        {
            try
            {
                Machine machine = _context.Machines.Find(id); // Synchronous Find
                if (machine == null)
                {
                    throw new ArgumentException("Machine not found.");
                }
                return machine;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                throw new ApplicationException("An error occurred while fetching the machine.", ex);
            }
        }

        public List<JobOrderCustEmpVM> GetJobOrdersWithoutProcessBridge()
        {

            try
            {
                // Get job orders without process bridges
                var jobOrderIdsWithBridges = _context.ProcessBridges
                    .Where(pb => pb.JobOrderId != null)
                    .Select(pb => pb.JobOrderId.Value)
                    .Distinct()
                    .ToList();

                var jobOrdersWithoutBridges = _context.JobOrders
                    .Where(jo => !jobOrderIdsWithBridges.Contains(jo.JobOrderId))
                    .ToList();

                // Handle customer IDs (assuming CustomerId is int)
                var customerIds = jobOrdersWithoutBridges
                    .Where(jo => jo.CustomerId.HasValue)
                    .Select(jo => jo.CustomerId.Value)
                    .Distinct()
                    .ToList();

                // Handle employee IDs (now as string)
                var employeeIds = jobOrdersWithoutBridges
                    .Where(jo => !string.IsNullOrEmpty(jo.EmployeeId))
                    .Select(jo => jo.EmployeeId)
                    .Distinct()
                    .ToList();

                // Get related data
                var customers = _context.Customers
                    .Where(c => customerIds.Contains(c.CustomerId))
                    .ToDictionary(c => c.CustomerId);

                var employees = _context.Employees
                    .Where(e => employeeIds.Contains(e.EmployeeId))
                    .ToDictionary(e => e.EmployeeId);

                // Create view models with proper null checking
                return jobOrdersWithoutBridges
                    .Where(jo => jo.CustomerId.HasValue &&
                                !string.IsNullOrEmpty(jo.EmployeeId) &&
                                customers.ContainsKey(jo.CustomerId.Value) &&
                                employees.ContainsKey(jo.EmployeeId))
                    .Select(jo => new JobOrderCustEmpVM
                    {
                        // Customer data
                        CustomerId = jo.CustomerId.Value,
                        CustomerAddress = customers[jo.CustomerId.Value].CustomerAddress,
                        CustomerPhone = customers[jo.CustomerId.Value].CustomerPhone,
                        CustomerName = customers[jo.CustomerId.Value].CustomerName,
                        CustomerEmail = customers[jo.CustomerId.Value].CustomerEmail,

                        // Job Order data
                        JobOrderId = jo.JobOrderId,
                        OrderProgress = jo.OrderProgress,
                        RemainingAmount = jo.RemainingAmount,
                        EarnedRevenue = jo.EarnedRevenue,
                        UnearnedRevenue = jo.UnearnedRevenue,
                        StartDate = jo.StartDate,
                        EndDate = jo.EndDate,
                        JobOrdernotes = jo.JobOrdernotes,

                        // Employee data (now handling string ID)
                        EmployeeId = jo.EmployeeId,
                        EmployeeName = employees[jo.EmployeeId].EmployeeName
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        public List<JobOrderCustEmpVM> GetJobOrdersWithProcessBridge()
        {
            try
            {
                // Get job orders WITH process bridges
                var jobOrderIdsWithBridges = _context.ProcessBridges
                    .Where(pb => pb.JobOrderId != null)
                    .Select(pb => pb.JobOrderId.Value)
                    .Distinct()
                    .ToList();

                var jobOrdersWithBridges = _context.JobOrders
                    .Where(jo => jobOrderIdsWithBridges.Contains(jo.JobOrderId)) // Changed condition
                    .ToList();

                // Handle customer IDs
                var customerIds = jobOrdersWithBridges
                    .Where(jo => jo.CustomerId.HasValue)
                    .Select(jo => jo.CustomerId.Value)
                    .Distinct()
                    .ToList();

                // Handle employee IDs
                var employeeIds = jobOrdersWithBridges
                    .Where(jo => !string.IsNullOrEmpty(jo.EmployeeId))
                    .Select(jo => jo.EmployeeId)
                    .Distinct()
                    .ToList();

                // Get related data
                var customers = _context.Customers
                    .Where(c => customerIds.Contains(c.CustomerId))
                    .ToDictionary(c => c.CustomerId);

                var employees = _context.Employees
                    .Where(e => employeeIds.Contains(e.EmployeeId))
                    .ToDictionary(e => e.EmployeeId);

                // Create view models
                return jobOrdersWithBridges
                    .Where(jo => jo.CustomerId.HasValue &&
                                !string.IsNullOrEmpty(jo.EmployeeId) &&
                                customers.ContainsKey(jo.CustomerId.Value) &&
                                employees.ContainsKey(jo.EmployeeId))
                    .Select(jo => new JobOrderCustEmpVM
                    {
                        CustomerId = jo.CustomerId.Value,
                        CustomerAddress = customers[jo.CustomerId.Value].CustomerAddress,
                        CustomerPhone = customers[jo.CustomerId.Value].CustomerPhone,
                        CustomerName = customers[jo.CustomerId.Value].CustomerName,
                        CustomerEmail = customers[jo.CustomerId.Value].CustomerEmail,

                        JobOrderId = jo.JobOrderId,
                        OrderProgress = jo.OrderProgress,
                        RemainingAmount = jo.RemainingAmount,
                        EarnedRevenue = jo.EarnedRevenue,
                        UnearnedRevenue = jo.UnearnedRevenue,
                        StartDate = jo.StartDate,
                        EndDate = jo.EndDate,
                        JobOrdernotes = jo.JobOrdernotes,

                        EmployeeId = jo.EmployeeId,
                        EmployeeName = employees[jo.EmployeeId].EmployeeName
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///fetch data of another controllers
        ///

        public JobOrderCost GetJobOrderForCost(int jobOrderID)
        {

            try
            {
                decimal paperBalance = 0;
                decimal inkBalance = 0;
                decimal supplyBalance = 0;
                List<ModifiedQuantityBridge> modifiedQuantityBridgeList = new List<ModifiedQuantityBridge>();

                JobOrder existingJobOrder = _context.JobOrders
                    .Include(j => j.Customer)
                    .Include(j => j.Employee)
                    .FirstOrDefault(v => v.JobOrderId == jobOrderID);
                Employee employees = _context.Employees.FirstOrDefault(emp => emp.EmployeeId == existingJobOrder.EmployeeId);
                Customer customer = _context.Customers.FirstOrDefault(cus => cus.CustomerId == existingJobOrder.CustomerId);
                JobOrderCost joborder = new JobOrderCost();
                joborder.CustomerId = customer.CustomerId;
                joborder.CustomerAddress = customer.CustomerAddress;
                joborder.CustomerPhone = customer.CustomerPhone;
                joborder.CustomerName = customer.CustomerName;
                joborder.CustomerEmail = customer.CustomerEmail;
                joborder.JobOrderId = existingJobOrder.JobOrderId;
                joborder.OrderProgress = existingJobOrder.OrderProgress;
                joborder.RemainingAmount = existingJobOrder.RemainingAmount;
                joborder.JobOrderSource = existingJobOrder.JobOrderSource;
                joborder.EarnedRevenue = existingJobOrder.EarnedRevenue;
                joborder.UnearnedRevenue = existingJobOrder.UnearnedRevenue;
                joborder.StartDate = existingJobOrder.StartDate;
                joborder.EndDate = existingJobOrder.EndDate;
                joborder.JobOrdernotes = existingJobOrder.JobOrdernotes;
                joborder.EmployeeId = existingJobOrder.EmployeeId;
                joborder.EmployeeName = employees.EmployeeName;
                List<RequisiteOrder> req = _context.RequisiteOrders.Where(req => req.JobOrderId == jobOrderID).ToList();
                List<ReturnsOrder> ret = _context.ReturnsOrders.Where(req => req.JobOrderId == jobOrderID).ToList();

                for (int i = 0; i < req.Count; i++)
                {
                    List<QuantityBridge> qb = _context.QuantityBridges.Where(q => q.RequisiteId == req[i].RequisiteId).ToList();
                    foreach (QuantityBridge item in qb)
                    {

                        if (item.PaperId != null)
                        {
                            paperBalance += item.TotalBalance ?? 0;
                            ModifiedQuantityBridge modified = new ModifiedQuantityBridge();
                            modified.PaperId = item.PaperId;
                            modified.PaperName = _context.Papers.FirstOrDefault(paper => paper.PaperId == item.PaperId).Name;
                            modified.Quantity = item.Quantity;
                            modified.Price = item.Price;
                            modified.TotalBalance = item.TotalBalance;
                            modifiedQuantityBridgeList.Add(modified);
                        }
                        else if (item.InkId != null)
                        {
                            inkBalance += item.TotalBalance ?? 0;
                            ModifiedQuantityBridge modified = new ModifiedQuantityBridge();
                            modified.InkId = item.InkId;
                            modified.InkName = _context.Inks.FirstOrDefault(ink => ink.InkId == item.InkId).Name;
                            modified.Quantity = item.Quantity;
                            modified.Price = item.Price;
                            modified.TotalBalance = item.TotalBalance;
                            modifiedQuantityBridgeList.Add(modified);

                        }
                        else if (item.SuppliesId != null)
                        {
                            supplyBalance += item.TotalBalance ?? 0;
                            ModifiedQuantityBridge modified = new ModifiedQuantityBridge();
                            modified.SuppliesId = item.SuppliesId;
                            modified.SuppliesName = _context.Supplies.FirstOrDefault(supply => supply.SuppliesId == item.SuppliesId).Name;
                            modified.Quantity = item.Quantity;
                            modified.Price = item.Price;
                            modified.TotalBalance = item.TotalBalance;
                            modifiedQuantityBridgeList.Add(modified);

                        }
                    }
                }
                for (int i = 0; i < ret.Count; i++)
                {
                    List<QuantityBridge> qb = _context.QuantityBridges.Where(q => q.ReturnId == ret[i].ReturnId).ToList();
                    foreach (QuantityBridge item in qb)
                    {

                        if (item.PaperId != null)
                        {
                            paperBalance -= item.TotalBalance ?? 0;
                            ModifiedQuantityBridge modified = modifiedQuantityBridgeList.FirstOrDefault(mod => mod.PaperId == item.PaperId);
                            modified.Quantity -= item.Quantity;
                            modified.TotalBalance -= item.TotalBalance;

                        }
                        else if (item.InkId != null)
                        {
                            inkBalance -= item.TotalBalance ?? 0;
                            ModifiedQuantityBridge modified = modifiedQuantityBridgeList.FirstOrDefault(mod => mod.InkId == item.InkId);
                            modified.Quantity -= item.Quantity;
                            modified.TotalBalance -= item.TotalBalance;
                        }
                        else if (item.SuppliesId != null)
                        {
                            supplyBalance -= item.TotalBalance ?? 0;
                            ModifiedQuantityBridge modified = modifiedQuantityBridgeList.FirstOrDefault(mod => mod.SuppliesId == item.SuppliesId);
                            modified.Quantity -= item.Quantity;
                            modified.TotalBalance -= item.TotalBalance;
                        }
                    }
                }
                joborder.paperBalance = paperBalance;
                joborder.inkBalance = inkBalance;
                joborder.supplyBalance = supplyBalance;
                joborder.modifiedQuantityBridgeList = modifiedQuantityBridgeList;
                return joborder ?? throw new ArgumentException("امر العمل غير موجود");
            }   
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return null;

            }


        }

        public CostReportVM CostReport(DateOnly beginningDate, DateOnly endingDate)
        {
            try
            {
                var costReportVM = new CostReportVM();
                var jobOrders = _context.JobOrders
                                        .Where(j => j.StartDate >= beginningDate &&
                                                    j.EndDate <= endingDate)
                                        .ToList();

                costReportVM.JobOrdersCount = jobOrders.Count;
                costReportVM.JobOrderUnearnedRevenue = jobOrders.Sum(j => j.UnearnedRevenue ?? 0);
                costReportVM.JobOrderEarnedRevenue = jobOrders.Sum(j => j.EarnedRevenue ?? 0);
                costReportVM.JobOrderRemainingAmount = jobOrders.Sum(j => j.RemainingAmount ?? 0);

                var jobIds = jobOrders.Select(j => j.JobOrderId).ToList();

                var requisites = _context.RequisiteOrders
                                         .Where(r => jobIds.Contains(r.JobOrderId))
                                         .ToList();

                var returns = _context.ReturnsOrders
                                      .Where(rn => jobIds.Contains(rn.JobOrderId ?? 0))
                                      .ToList();

                var reqIds = requisites.Select(r => r.RequisiteId).ToList();
                var retIds = returns.Select(r => r.ReturnId).ToList();

                var quantityBridges = _context.QuantityBridges
                                              .Where(q => (q.RequisiteId != null && reqIds.Contains(q.RequisiteId.Value)) ||
                                                          (q.ReturnId != null && retIds.Contains(q.ReturnId.Value)))
                                              .ToList();

                var misc = _context.MiscellaneousExpenses
                                   .Where(m => jobIds.Contains(m.JobOrderId ?? 0))
                                   .AsNoTracking()
                                   .ToList();

                decimal totalBalance = quantityBridges.Sum(q =>
                                    q.RequisiteId != null ? (q.TotalBalance ?? 0)
                                                          : -(q.TotalBalance ?? 0));

                costReportVM.TotalCost = totalBalance;

                decimal miscAdded = misc.Sum(m => m.TotalAfterPercentage);
                decimal miscUnrecovered = misc.Sum(m => m.FinalTotal - m.TotalAfterPercentage);

                costReportVM.TotalRevenue = costReportVM.JobOrderUnearnedRevenue
                                          + costReportVM.JobOrderEarnedRevenue
                                          + costReportVM.JobOrderRemainingAmount
                                          + miscAdded;

                costReportVM.NetProfit = costReportVM.TotalRevenue
                                       - costReportVM.TotalCost
                                       - miscUnrecovered;

                return costReportVM;
            }
            catch (Exception ex)
            {
                WriteException.WriteExceptionToFile(ex);
                return new CostReportVM();
            }
        }
    }
}

