using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ThothSystemVersion1.Models;
namespace ThothSystemVersion1.Database;
public partial class ThothContext : DbContext
{
    public ThothContext()
    {
    }

    public ThothContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Ink> Inks { get; set; }

    public virtual DbSet<JobOrder> JobOrders { get; set; }

    public virtual DbSet<Labour> Labours { get; set; }

    public virtual DbSet<Machine> Machines { get; set; }

    public virtual DbSet<MiscellaneousExpense> MiscellaneousExpenses { get; set; }

    public virtual DbSet<Paper> Papers { get; set; }

    public virtual DbSet<PaymentOrder> PaymentOrders { get; set; }

    public virtual DbSet<PhysicalCountOrder> PhysicalCountOrders { get; set; }

    public virtual DbSet<ProcessBridge> ProcessBridges { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<QuantityBridge> QuantityBridges { get; set; }

    public virtual DbSet<RecieptsOrder> RecieptsOrders { get; set; }

    public virtual DbSet<RequisiteOrder> RequisiteOrders { get; set; }

    public virtual DbSet<ReturnsOrder> ReturnsOrders { get; set; }

    public virtual DbSet<Supply> Supplies { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-VAR1OCQ;Database=ThothSystem;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_100_CS_AI");

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__B611CB9D783D521D");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.CustomerPhone, "UQ__Customer__311068C4E9673657").IsUnique();

            entity.HasIndex(e => e.CustomerEmail, "UQ__Customer__FFE82D72387E9B7E").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("customerID");
            entity.Property(e => e.Activated).HasDefaultValue(true);
            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(500)
                .HasDefaultValue("")
                .HasColumnName("customerAddress");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(250)
                .HasDefaultValue("")
                .HasColumnName("customerEmail");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("customerName");
            entity.Property(e => e.CustomerNotes)
                .HasMaxLength(2500)
                .HasDefaultValue("")
                .HasColumnName("customerNotes");
            entity.Property(e => e.CustomerPhone)
                .HasMaxLength(15)
                .HasDefaultValue("")
                .HasColumnName("customerPhone");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C134C9A18AF66C12");

            entity.ToTable("Employee", tb =>
                {
                    tb.HasTrigger("trg_DeleteEmployee");
                    tb.HasTrigger("trg_DeleteEmployee_PurchaseOrder");
                    tb.HasTrigger("trg_UpdateEmployee");
                    tb.HasTrigger("trg_UpdateEmployee_PurchaseOrder");
                });

            entity.HasIndex(e => e.EmployeeUserName, "UQ__Employee__8D8E404745DF0EE3").IsUnique();

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .HasColumnName("employeeID");
            entity.Property(e => e.Activated).HasDefaultValue(true);
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("employeeName");
            entity.Property(e => e.EmployeePassword)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("employeePassword");
            entity.Property(e => e.EmployeeUserName)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("employeeUserName");
            entity.Property(e => e.JobRole).HasColumnName("jobRole");
        });

        modelBuilder.Entity<Ink>(entity =>
        {
            entity.HasKey(e => e.InkId).HasName("PK__Ink__13DA870371A7A5F7");

            entity.ToTable("Ink");

            entity.Property(e => e.InkId).HasColumnName("inkID");
            entity.Property(e => e.Activated).HasDefaultValue(true);
            entity.Property(e => e.Colored)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("colored");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasDefaultValue("")
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.ReorderPoint)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("reorderPoint");
            entity.Property(e => e.TotalBalance)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalBalance");
        });

        modelBuilder.Entity<JobOrder>(entity =>
        {
            entity.HasKey(e => e.JobOrderId).HasName("PK__JobOrder__759794652E3081F3");

            entity.ToTable("JobOrder", tb =>
                {
                    tb.HasTrigger("trg_DeleteJobOrder");
                    tb.HasTrigger("trg_DeleteJobOrder_ReturnsOrder");
                    tb.HasTrigger("trg_UpdateJobOrder");
                    tb.HasTrigger("trg_UpdateJobOrder_ReturnsOrder");
                });

            entity.Property(e => e.JobOrderId).HasColumnName("jobOrderID");
            entity.Property(e => e.CustomerId).HasColumnName("customerID");
            entity.Property(e => e.EarnedRevenue)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("earnedRevenue");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .HasColumnName("employeeID");
            entity.Property(e => e.EndDate).HasColumnName("endDate");
            entity.Property(e => e.JobOrdernotes)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("jobOrdernotes");
            entity.Property(e => e.OrderProgress)
                .HasMaxLength(20)
                .HasDefaultValue("قيد الانتظار")
                .HasColumnName("orderProgress");
            entity.Property(e => e.RemainingAmount)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("remainingAmount");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("startDate");
            entity.Property(e => e.UnearnedRevenue)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("unearnedRevenue");

            entity.HasOne(d => d.Customer).WithMany(p => p.JobOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__JobOrder__custom__60A75C0F");

            entity.HasOne(d => d.Employee).WithMany(p => p.JobOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__JobOrder__employ__619B8048");
        });

        modelBuilder.Entity<Labour>(entity =>
        {
            entity.HasKey(e => e.LabourId).HasName("PK__Labour__681E871C0E3C6B3C");

            entity.ToTable("Labour");

            entity.Property(e => e.LabourId).HasColumnName("labourID");
            entity.Property(e => e.Activated).HasDefaultValue(true);
            entity.Property(e => e.LabourProcessName)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("labourProcessName");
            entity.Property(e => e.Price)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<Machine>(entity =>
        {
            entity.HasKey(e => e.MachineId).HasName("PK__Machine__D1ABE00DB733CCA7");

            entity.ToTable("Machine");

            entity.Property(e => e.MachineId).HasColumnName("machineID");
            entity.Property(e => e.Activated).HasDefaultValue(true);
            entity.Property(e => e.MachineProcessName)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("machineProcessName");
            entity.Property(e => e.Price)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<MiscellaneousExpense>(entity =>
        {
            entity.HasKey(e => e.MiscellaneousExpensesId).HasName("PK__Miscella__9EC2AFA36D387507");

            entity.Property(e => e.MiscellaneousExpensesId).HasColumnName("MiscellaneousExpensesID");
            entity.Property(e => e.AdminstrativeExpense)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("adminstrativeExpense");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .HasColumnName("employeeID");
            entity.Property(e => e.EmployeeImprovmentBox)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("employeeImprovmentBox");
            entity.Property(e => e.FilmsProcessingExpense)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("filmsProcessingExpense");
            entity.Property(e => e.FinalTotal)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("finalTotal");
            entity.Property(e => e.JobOrderId).HasColumnName("jobOrderID");
            entity.Property(e => e.MaterialProcessingExpense)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("materialProcessingExpense");
            entity.Property(e => e.MaterialsTotal)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("materialsTotal");
            entity.Property(e => e.MinistryOfFinance)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("ministryOfFinance");
            entity.Property(e => e.Percentage)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("percentage");
            entity.Property(e => e.TotalAfterMaterials)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalAfterMaterials");
            entity.Property(e => e.TotalAfterPercentage)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalAfterPercentage");
            entity.Property(e => e.TotalExpenses)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalExpenses");
            entity.Property(e => e.ValueAddedTax)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valueAddedTax");
            entity.Property(e => e.totalAfterEmplyeeImprovementbox)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalAfterEmplyeeImprovementbox");
            entity.HasOne(d => d.Employee).WithMany(p => p.MiscellaneousExpenses)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Miscellan__emplo__0F624AF8");

            entity.HasOne(d => d.JobOrder).WithMany(p => p.MiscellaneousExpenses)
                .HasForeignKey(d => d.JobOrderId)
                .HasConstraintName("FK__Miscellan__jobOr__0E6E26BF");
        });

        modelBuilder.Entity<Paper>(entity =>
        {
            entity.HasKey(e => e.PaperId).HasName("PK__Paper__396E28533F15E59D");

            entity.ToTable("Paper");

            entity.Property(e => e.PaperId).HasColumnName("paperID");
            entity.Property(e => e.Activated).HasDefaultValue(true);
            entity.Property(e => e.Colored)
                .HasMaxLength(10)
                .HasDefaultValue("")
                .HasColumnName("colored");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasDefaultValue("")
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.ReorderPoint)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("reorderPoint");
            entity.Property(e => e.TotalBalance)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalBalance");
            entity.Property(e => e.Type)
                .HasMaxLength(25)
                .HasDefaultValue("")
                .HasColumnName("type");
            entity.Property(e => e.Weight)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("weight");
        });

        modelBuilder.Entity<PaymentOrder>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__PaymentO__A0D9EFA6E8665386");

            entity.ToTable("PaymentOrder");

            entity.Property(e => e.PaymentId).HasColumnName("paymentID");
            entity.Property(e => e.Amount)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .HasColumnName("employeeID");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("paymentDate");
            entity.Property(e => e.PaymentNotes)
                .HasMaxLength(2500)
                .HasDefaultValue("")
                .HasColumnName("paymentNotes");
            entity.Property(e => e.PurchaseId).HasColumnName("purchaseID");

            entity.HasOne(d => d.Employee).WithMany(p => p.PaymentOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__PaymentOr__emplo__5AB9788F");

            entity.HasOne(d => d.Purchase).WithMany(p => p.PaymentOrders)
                .HasForeignKey(d => d.PurchaseId)
                .HasConstraintName("FK__PaymentOr__purch__59C55456");
        });

        modelBuilder.Entity<PhysicalCountOrder>(entity =>
        {
            entity.HasKey(e => e.PhysicalCountId).HasName("PK__Physical__428288467BA421FA");

            entity.ToTable("PhysicalCountOrder");

            entity.Property(e => e.PhysicalCountId).HasColumnName("physicalCountID");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .HasColumnName("employeeID");
            entity.Property(e => e.PhysicalCountDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("physicalCountDate");
            entity.Property(e => e.PhysicalCountNotes)
                .HasMaxLength(2500)
                .HasDefaultValue("")
                .HasColumnName("physicalCountNotes");

            entity.HasOne(d => d.Employee).WithMany(p => p.PhysicalCountOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__PhysicalC__emplo__46B27FE2");
        });

        modelBuilder.Entity<ProcessBridge>(entity =>
        {
            entity.HasKey(e => e.ProcessBridgeId).HasName("PK__ProcessB__0B3E7649D5310630");

            entity.ToTable("ProcessBridge");

            entity.Property(e => e.ProcessBridgeId).HasColumnName("ProcessBridgeID");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .HasColumnName("employeeID");
            entity.Property(e => e.JobOrderId).HasColumnName("jobOrderID");
            entity.Property(e => e.LabourHourPrice)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("labourHourPrice");
            entity.Property(e => e.LabourId).HasColumnName("labourID");
            entity.Property(e => e.MachineHourPrice)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("machineHourPrice");
            entity.Property(e => e.MachineId).HasColumnName("machineID");
            entity.Property(e => e.NumberOfHours)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("numberOfHours");
            entity.Property(e => e.OldMachinePrice)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("oldMachinePrice");
            entity.Property(e => e.OldlabourPrice)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("oldlabourPrice");
            entity.Property(e => e.TotalLabourValue)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalLabourValue");
            entity.Property(e => e.TotalMachineValue)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalMachineValue");

            entity.HasOne(d => d.Employee).WithMany(p => p.ProcessBridges)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__ProcessBr__emplo__778AC167");

            entity.HasOne(d => d.JobOrder).WithMany(p => p.ProcessBridges)
                .HasForeignKey(d => d.JobOrderId)
                .HasConstraintName("FK__ProcessBr__jobOr__74AE54BC");

            entity.HasOne(d => d.Labour).WithMany(p => p.ProcessBridges)
                .HasForeignKey(d => d.LabourId)
                .HasConstraintName("FK__ProcessBr__labou__76969D2E");

            entity.HasOne(d => d.Machine).WithMany(p => p.ProcessBridges)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK__ProcessBr__machi__75A278F5");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK__Purchase__0261224C31BB3F91");

            entity.ToTable("PurchaseOrder");

            entity.Property(e => e.PurchaseId).HasColumnName("purchaseID");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .HasColumnName("employeeID");
            entity.Property(e => e.PaidAmount)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("paidAmount");
            entity.Property(e => e.PurchaseDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("purchaseDate");
            entity.Property(e => e.PurchaseNotes)
                .HasMaxLength(2500)
                .HasDefaultValue("")
                .HasColumnName("purchaseNotes");
            entity.Property(e => e.RemainingAmount)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("remainingAmount");
            entity.Property(e => e.VendorId).HasColumnName("vendorID");

            entity.HasOne(d => d.Employee).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseO__emplo__160F4887");

            entity.HasOne(d => d.Vendor).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseO__vendo__17036CC0");
        });

        modelBuilder.Entity<QuantityBridge>(entity =>
        {
            entity.HasKey(e => e.QuantityBridgeId).HasName("PK__Quantity__74C924196D3553E9");

            entity.ToTable("QuantityBridge");

            entity.Property(e => e.QuantityBridgeId).HasColumnName("QuantityBridgeID");
            entity.Property(e => e.InkId).HasColumnName("inkID");
            entity.Property(e => e.OldPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("oldPrice");
            entity.Property(e => e.OldQuantity)
                .HasDefaultValue(1)
                .HasColumnName("oldQuantity");
            entity.Property(e => e.OldTotalBalance)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("oldTotalBalance");
            entity.Property(e => e.PaperId).HasColumnName("paperID");
            entity.Property(e => e.PhysicalCountId).HasColumnName("physicalCountID");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.PurchaseId).HasColumnName("purchaseID");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.RequisiteId).HasColumnName("requisiteID");
            entity.Property(e => e.ReturnId).HasColumnName("returnID");
            entity.Property(e => e.SuppliesId).HasColumnName("suppliesID");
            entity.Property(e => e.TotalBalance)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalBalance");

            entity.HasOne(d => d.Ink).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.InkId)
                .HasConstraintName("FK__QuantityB__inkID__51300E55");

            entity.HasOne(d => d.Paper).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.PaperId)
                .HasConstraintName("FK__QuantityB__paper__5224328E");

            entity.HasOne(d => d.PhysicalCount).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.PhysicalCountId)
                .HasConstraintName("FK__QuantityB__physi__540C7B00");

            entity.HasOne(d => d.Purchase).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.PurchaseId)
                .HasConstraintName("FK__QuantityB__purch__4F47C5E3");

            entity.HasOne(d => d.Requisite).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.RequisiteId)
                .HasConstraintName("FK__QuantityB__requi__503BEA1C");

            entity.HasOne(d => d.Return).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.ReturnId)
                .HasConstraintName("FK__QuantityB__retur__4E53A1AA");

            entity.HasOne(d => d.Supplies).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.SuppliesId)
                .HasConstraintName("FK__QuantityB__suppl__531856C7");
        });

        modelBuilder.Entity<RecieptsOrder>(entity =>
        {
            entity.HasKey(e => e.RecieptId).HasName("PK__Reciepts__71D49F63835FC591");

            entity.ToTable("RecieptsOrder");

            entity.Property(e => e.RecieptId).HasColumnName("recieptID");
            entity.Property(e => e.Amount)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .HasColumnName("employeeID");
            entity.Property(e => e.JobOrderId).HasColumnName("jobOrderID");
            entity.Property(e => e.ReceiptDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("receiptDate");
            entity.Property(e => e.ReceiptNotes)
                .HasMaxLength(2500)
                .HasDefaultValue("")
                .HasColumnName("receiptNotes");

            entity.HasOne(d => d.Employee).WithMany(p => p.RecieptsOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__RecieptsO__emplo__6166761E");

            entity.HasOne(d => d.JobOrder).WithMany(p => p.RecieptsOrders)
                .HasForeignKey(d => d.JobOrderId)
                .HasConstraintName("FK__RecieptsO__jobOr__607251E5");
        });

        modelBuilder.Entity<RequisiteOrder>(entity =>
        {
            entity.HasKey(e => e.RequisiteId).HasName("PK__Requisit__325566014071CA45");

            entity.ToTable("RequisiteOrder");

            entity.Property(e => e.RequisiteId).HasColumnName("requisiteID");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .HasColumnName("employeeID");
            entity.Property(e => e.JobOrderId).HasColumnName("jobOrderID");
            entity.Property(e => e.RequisiteDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("requisiteDate");
            entity.Property(e => e.RequisiteNotes)
                .HasMaxLength(2500)
                .HasDefaultValue("")
                .HasColumnName("requisiteNotes");

            entity.HasOne(d => d.Employee).WithMany(p => p.RequisiteOrders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requisite__emplo__236943A5");

            entity.HasOne(d => d.JobOrder).WithMany(p => p.RequisiteOrders)
                .HasForeignKey(d => d.JobOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requisite__jobOr__245D67DE");
        });

        modelBuilder.Entity<ReturnsOrder>(entity =>
        {
            entity.HasKey(e => e.ReturnId).HasName("PK__ReturnsO__EBA763F984751E2D");

            entity.ToTable("ReturnsOrder");

            entity.Property(e => e.ReturnId).HasColumnName("returnID");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .HasColumnName("employeeID");
            entity.Property(e => e.JobOrderId).HasColumnName("jobOrderID");
            entity.Property(e => e.PurchaseId).HasColumnName("purchaseID");
            entity.Property(e => e.ReturnDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("returnDate");
            entity.Property(e => e.ReturnInOut)
                .HasDefaultValue(true)
                .HasColumnName("returnInOut");
            entity.Property(e => e.ReturnsNotes)
                .HasMaxLength(2500)
                .HasDefaultValue("")
                .HasColumnName("returnsNotes");

            entity.HasOne(d => d.Employee).WithMany(p => p.ReturnsOrders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReturnsOr__emplo__1EA48E88");

            entity.HasOne(d => d.JobOrder).WithMany(p => p.ReturnsOrders)
                .HasForeignKey(d => d.JobOrderId)
                .HasConstraintName("FK__ReturnsOr__jobOr__1CBC4616");

            entity.HasOne(d => d.Purchase).WithMany(p => p.ReturnsOrders)
                .HasForeignKey(d => d.PurchaseId)
                .HasConstraintName("FK__ReturnsOr__purch__1DB06A4F");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.HasKey(e => e.SuppliesId).HasName("PK__Supplies__C654A0F4840227A9");

            entity.Property(e => e.SuppliesId).HasColumnName("suppliesID");
            entity.Property(e => e.Activated).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasDefaultValue("")
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.ReorderPoint)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("reorderPoint");
            entity.Property(e => e.TotalBalance)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalBalance");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PK__Vendor__EC65C4E3C8551B04");

            entity.ToTable("Vendor");

            entity.HasIndex(e => e.VendorEmail, "UQ__Vendor__61AB83AB61E414AE").IsUnique();

            entity.HasIndex(e => e.VendorPhone, "UQ__Vendor__8FB8D3A37443DA84").IsUnique();

            entity.Property(e => e.VendorId).HasColumnName("vendorID");
            entity.Property(e => e.Activated).HasDefaultValue(true);
            entity.Property(e => e.VendorAddress)
                .HasMaxLength(500)
                .HasDefaultValue("")
                .HasColumnName("vendorAddress");
            entity.Property(e => e.VendorEmail)
                .HasMaxLength(250)
                .HasDefaultValue("")
                .HasColumnName("vendorEmail");
            entity.Property(e => e.VendorName)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("vendorName");
            entity.Property(e => e.VendorNotes)
                .HasMaxLength(2500)
                .HasDefaultValue("")
                .HasColumnName("vendorNotes");
            entity.Property(e => e.VendorPhone)
                .HasMaxLength(15)
                .HasDefaultValue("")
                .HasColumnName("vendorPhone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
