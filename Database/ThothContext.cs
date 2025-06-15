using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1;

public partial class ThothContext : DbContext
{
    public ThothContext()
    {
    }

    public ThothContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ColorWeightSize> ColorWeightSizes { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Ink> Inks { get; set; }

    public virtual DbSet<JobOrder> JobOrders { get; set; }

    public virtual DbSet<Labour> Labours { get; set; }

    public virtual DbSet<Machine> Machines { get; set; }

    public virtual DbSet<MachineStore> MachineStores { get; set; }

    public virtual DbSet<MiscellaneousExpense> MiscellaneousExpenses { get; set; }

    public virtual DbSet<Paper> Papers { get; set; }

    public virtual DbSet<PaymentOrder> PaymentOrders { get; set; }

    public virtual DbSet<PerpetualRequisiteOrder> PerpetualRequisiteOrders { get; set; }

    public virtual DbSet<PhysicalCountOrder> PhysicalCountOrders { get; set; }

    public virtual DbSet<ProcessBridge> ProcessBridges { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<QuantityBridge> QuantityBridges { get; set; }

    public virtual DbSet<RecieptsOrder> RecieptsOrders { get; set; }

    public virtual DbSet<RequisiteOrder> RequisiteOrders { get; set; }

    public virtual DbSet<ReturnsOrder> ReturnsOrders { get; set; }

    public virtual DbSet<SparePart> SpareParts { get; set; }

    public virtual DbSet<Supply> Supplies { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-2K9M8PS;Database=ThothSystem;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_100_CS_AI");

        modelBuilder.Entity<ColorWeightSize>(entity =>
        {
            entity.HasKey(e => e.ColorWeightSizeId).HasName("PK__ColorWei__CB626C94C4667D3B");

            entity.ToTable("ColorWeightSize");

            entity.Property(e => e.ColorWeightSizeId).HasColumnName("colorWeightSizeID");
            entity.Property(e => e.Colored)
                .HasMaxLength(10)
                .HasDefaultValue("")
                .HasColumnName("colored");
            entity.Property(e => e.Size)
                .HasMaxLength(25)
                .HasDefaultValue("")
                .HasColumnName("size");
            entity.Property(e => e.Type)
                .HasDefaultValue(0)
                .HasColumnName("type");
            entity.Property(e => e.Weight)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("weight");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__B611CB9D70199C14");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.CustomerPhone, "UQ__Customer__311068C4BCB06FE3").IsUnique();

            entity.HasIndex(e => e.CustomerEmail, "UQ__Customer__FFE82D72EDDA13E1").IsUnique();

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
            entity.Property(e => e.CustomerSource)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("customerSource");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C134C9A1F3595DF4");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.EmployeeUserName, "UQ__Employee__8D8E4047A14C8980").IsUnique();

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
            entity.Property(e => e.Forgetpassword)
                .HasDefaultValue(false)
                .HasColumnName("forgetpassword");
            entity.Property(e => e.JobRole).HasColumnName("jobRole");
        });

        modelBuilder.Entity<Ink>(entity =>
        {
            entity.HasKey(e => e.InkId).HasName("PK__Ink__13DA870338631FDE");

            entity.ToTable("Ink");

            entity.Property(e => e.InkId).HasColumnName("inkID");
            entity.Property(e => e.Activated).HasDefaultValue(true);
            entity.Property(e => e.AverageQuantity).HasColumnName("averageQuantity");
            entity.Property(e => e.Colored)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("colored");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasDefaultValue("")
                .HasColumnName("name");
            entity.Property(e => e.NumberOfUnits).HasColumnName("numberOfUnits");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReorderPoint)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("reorderPoint");
            entity.Property(e => e.TotalBalance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalBalance");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("unitPrice");
        });

        modelBuilder.Entity<JobOrder>(entity =>
        {
            entity.HasKey(e => e.JobOrderId).HasName("PK__JobOrder__75979465DBC765F1");

            entity.ToTable("JobOrder");

            entity.Property(e => e.JobOrderId).HasColumnName("jobOrderID");
            entity.Property(e => e.CustomerId).HasColumnName("customerID");
            entity.Property(e => e.EarnedRevenue)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("earnedRevenue");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .HasColumnName("employeeID");
            entity.Property(e => e.EndDate).HasColumnName("endDate");
            entity.Property(e => e.JobOrderSource)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("jobOrderSource");
            entity.Property(e => e.JobOrdernotes)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("jobOrdernotes");
            entity.Property(e => e.OrderProgress)
                .HasMaxLength(20)
                .HasDefaultValue("قيد الانتظار")
                .HasColumnName("orderProgress");
            entity.Property(e => e.RemainingAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("remainingAmount");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("startDate");
            entity.Property(e => e.UnearnedRevenue)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("unearnedRevenue");

            entity.HasOne(d => d.Customer).WithMany(p => p.JobOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__JobOrder__custom__5070F446");

            entity.HasOne(d => d.Employee).WithMany(p => p.JobOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__JobOrder__employ__5165187F");
        });

        modelBuilder.Entity<Labour>(entity =>
        {
            entity.HasKey(e => e.LabourId).HasName("PK__Labour__681E871CC5AEFAB0");

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
            entity.HasKey(e => e.MachineId).HasName("PK__Machine__D1ABE00DF4EA28C6");

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

        modelBuilder.Entity<MachineStore>(entity =>
        {
            entity.HasKey(e => e.MachineStoreId).HasName("PK__MachineS__47734024B0BB042A");

            entity.ToTable("MachineStore");

            entity.Property(e => e.MachineStoreId).HasColumnName("machineStoreID");
            entity.Property(e => e.Activated).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasDefaultValue("")
                .HasColumnName("name");
        });

        modelBuilder.Entity<MiscellaneousExpense>(entity =>
        {
            entity.HasKey(e => e.MiscellaneousExpensesId).HasName("PK__Miscella__9EC2AFA3FD5E9E14");

            entity.Property(e => e.MiscellaneousExpensesId).HasColumnName("MiscellaneousExpensesID");
            entity.Property(e => e.AdminstrativeExpense)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("adminstrativeExpense");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .HasColumnName("employeeID");
            entity.Property(e => e.EmployeeImprovmentBox)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("employeeImprovmentBox");
            entity.Property(e => e.FilmsProcessingExpense)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("filmsProcessingExpense");
            entity.Property(e => e.FinalTotal)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("finalTotal");
            entity.Property(e => e.JobOrderId).HasColumnName("jobOrderID");
            entity.Property(e => e.MaterialProcessingExpense)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("materialProcessingExpense");
            entity.Property(e => e.MaterialsTotal)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("materialsTotal");
            entity.Property(e => e.MinistryOfFinance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("ministryOfFinance");
            entity.Property(e => e.Other)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("other");
            entity.Property(e => e.Percentage)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("percentage");
            entity.Property(e => e.totalAfterEmplyeeImprovementbox)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalAfterEmplyeeImprovementbox");
            entity.Property(e => e.TotalAfterMaterials)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalAfterMaterials");
            entity.Property(e => e.TotalAfterPercentage)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalAfterPercentage");
            entity.Property(e => e.TotalExpenses)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalExpenses");
            entity.Property(e => e.ValueAddedTax)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valueAddedTax");

            entity.HasOne(d => d.Employee).WithMany(p => p.MiscellaneousExpenses)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Miscellan__emplo__01142BA1");

            entity.HasOne(d => d.JobOrder).WithMany(p => p.MiscellaneousExpenses)
                .HasForeignKey(d => d.JobOrderId)
                .HasConstraintName("FK__Miscellan__jobOr__00200768");
        });

        modelBuilder.Entity<Paper>(entity =>
        {
            entity.HasKey(e => e.PaperId).HasName("PK__Paper__396E2853D76B311B");

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
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReorderPoint)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("reorderPoint");
            entity.Property(e => e.Size)
                .HasMaxLength(25)
                .HasDefaultValue("")
                .HasColumnName("size");
            entity.Property(e => e.TotalBalance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalBalance");
            entity.Property(e => e.Weight)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("weight");
        });

        modelBuilder.Entity<PaymentOrder>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__PaymentO__A0D9EFA65852B520");

            entity.ToTable("PaymentOrder");

            entity.Property(e => e.PaymentId).HasColumnName("paymentID");
            entity.Property(e => e.Amount)
                .HasDefaultValue(0m)
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
                .HasConstraintName("FK__PaymentOr__emplo__69FBBC1F");

            entity.HasOne(d => d.Purchase).WithMany(p => p.PaymentOrders)
                .HasForeignKey(d => d.PurchaseId)
                .HasConstraintName("FK__PaymentOr__purch__690797E6");
        });

        modelBuilder.Entity<PerpetualRequisiteOrder>(entity =>
        {
            entity.HasKey(e => e.PerpetualRequisiteId).HasName("PK__Perpetua__0DEF83AFCB7DE144");

            entity.ToTable("PerpetualRequisiteOrder");

            entity.Property(e => e.PerpetualRequisiteId).HasColumnName("perpetualRequisiteID");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .HasColumnName("employeeID");
            entity.Property(e => e.PerpetualRequisiteDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("perpetualRequisiteDate");
            entity.Property(e => e.RequisiteNotes)
                .HasMaxLength(2500)
                .HasDefaultValue("")
                .HasColumnName("requisiteNotes");

            entity.HasOne(d => d.Employee).WithMany(p => p.PerpetualRequisiteOrders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Perpetual__emplo__4E53A1AA");
        });

        modelBuilder.Entity<PhysicalCountOrder>(entity =>
        {
            entity.HasKey(e => e.PhysicalCountId).HasName("PK__Physical__4282884610B6561C");

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
                .HasConstraintName("FK__PhysicalC__emplo__3D2915A8");
        });

        modelBuilder.Entity<ProcessBridge>(entity =>
        {
            entity.HasKey(e => e.ProcessBridgeId).HasName("PK__ProcessB__0B3E7649085921F3");

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
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("numberOfHours");
            entity.Property(e => e.TotalLabourValue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalLabourValue");
            entity.Property(e => e.TotalMachineValue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalMachineValue");

            entity.HasOne(d => d.Employee).WithMany(p => p.ProcessBridges)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__ProcessBr__emplo__656C112C");

            entity.HasOne(d => d.JobOrder).WithMany(p => p.ProcessBridges)
                .HasForeignKey(d => d.JobOrderId)
                .HasConstraintName("FK__ProcessBr__jobOr__628FA481");

            entity.HasOne(d => d.Labour).WithMany(p => p.ProcessBridges)
                .HasForeignKey(d => d.LabourId)
                .HasConstraintName("FK__ProcessBr__labou__6477ECF3");

            entity.HasOne(d => d.Machine).WithMany(p => p.ProcessBridges)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK__ProcessBr__machi__6383C8BA");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK__Purchase__0261224C92E57577");

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
                .HasConstraintName("FK__PurchaseO__emplo__09A971A2");

            entity.HasOne(d => d.Vendor).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseO__vendo__0A9D95DB");
        });

        modelBuilder.Entity<QuantityBridge>(entity =>
        {
            entity.HasKey(e => e.QuantityBridgeId).HasName("PK__Quantity__74C9241927846971");

            entity.ToTable("QuantityBridge");

            entity.Property(e => e.QuantityBridgeId).HasColumnName("QuantityBridgeID");
            entity.Property(e => e.InkId).HasColumnName("inkID");
            entity.Property(e => e.MachineStoreId).HasColumnName("machineStoreID");
            entity.Property(e => e.NumberOfUnits)
                .HasDefaultValue(0)
                .HasColumnName("numberOfUnits");
            entity.Property(e => e.OldPrice)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("oldPrice");
            entity.Property(e => e.OldQuantity)
                .HasDefaultValue(1)
                .HasColumnName("oldQuantity");
            entity.Property(e => e.OldTotalBalance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("oldTotalBalance");
            entity.Property(e => e.PaperId).HasColumnName("paperID");
            entity.Property(e => e.PerpetualRequisiteId).HasColumnName("perpetualRequisiteID");
            entity.Property(e => e.PhysicalCountId).HasColumnName("physicalCountID");
            entity.Property(e => e.Price)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.PurchaseId).HasColumnName("purchaseID");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(0)
                .HasColumnName("quantity");
            entity.Property(e => e.RequisiteId).HasColumnName("requisiteID");
            entity.Property(e => e.ReturnId).HasColumnName("returnID");
            entity.Property(e => e.SparePartsId).HasColumnName("sparePartsID");
            entity.Property(e => e.SuppliesId).HasColumnName("suppliesID");
            entity.Property(e => e.TotalBalance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalBalance");
            entity.Property(e => e.UnitPrice)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("unitPrice");

            entity.HasOne(d => d.Ink).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.InkId)
                .HasConstraintName("FK__QuantityB__inkID__5F7E2DAC");

            entity.HasOne(d => d.MachineStore).WithMany(p => p.QuantityBridges)
               .HasForeignKey(d => d.MachineStoreId)
               .HasConstraintName("FK__QuantityB__machi__5AB9788F");

            entity.HasOne(d => d.Paper).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.PaperId)
                .HasConstraintName("FK__QuantityB__paper__607251E5");

            entity.HasOne(d => d.PerpetualRequisite).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.PerpetualRequisiteId)
                .HasConstraintName("FK__QuantityB__perpe__5BAD9CC8");

            entity.HasOne(d => d.PhysicalCount).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.PhysicalCountId)
                .HasConstraintName("FK__QuantityB__physi__625A9A57");

            entity.HasOne(d => d.Purchase).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.PurchaseId)
                .HasConstraintName("FK__QuantityB__purch__5D95E53A");

            entity.HasOne(d => d.Requisite).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.RequisiteId)
                .HasConstraintName("FK__QuantityB__requi__5E8A0973");

            entity.HasOne(d => d.Return).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.ReturnId)
                .HasConstraintName("FK__QuantityB__retur__5CA1C101");

            entity.HasOne(d => d.SpareParts).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.SparePartsId)
                .HasConstraintName("FK__QuantityB__spare__59C55456");

            entity.HasOne(d => d.Supplies).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.SuppliesId)
                .HasConstraintName("FK__QuantityB__suppl__6166761E");
        });

        modelBuilder.Entity<RecieptsOrder>(entity =>
        {
            entity.HasKey(e => e.RecieptId).HasName("PK__Reciepts__71D49F6301ED0132");

            entity.ToTable("RecieptsOrder");

            entity.Property(e => e.RecieptId).HasColumnName("recieptID");
            entity.Property(e => e.Amount)
                .HasDefaultValue(0m)
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
                .HasConstraintName("FK__RecieptsO__emplo__719CDDE7");

            entity.HasOne(d => d.JobOrder).WithMany(p => p.RecieptsOrders)
                .HasForeignKey(d => d.JobOrderId)
                .HasConstraintName("FK__RecieptsO__jobOr__70A8B9AE");
        });

        modelBuilder.Entity<RequisiteOrder>(entity =>
        {
            entity.HasKey(e => e.RequisiteId).HasName("PK__Requisit__3255660193410D59");

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
                .HasConstraintName("FK__Requisite__emplo__17036CC0");

            entity.HasOne(d => d.JobOrder).WithMany(p => p.RequisiteOrders)
                .HasForeignKey(d => d.JobOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requisite__jobOr__17F790F9");
        });

        modelBuilder.Entity<ReturnsOrder>(entity =>
        {
            entity.HasKey(e => e.ReturnId).HasName("PK__ReturnsO__EBA763F96A029E18");

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
                .HasConstraintName("FK__ReturnsOr__emplo__123EB7A3");

            entity.HasOne(d => d.JobOrder).WithMany(p => p.ReturnsOrders)
                .HasForeignKey(d => d.JobOrderId)
                .HasConstraintName("FK__ReturnsOr__jobOr__10566F31");

            entity.HasOne(d => d.Purchase).WithMany(p => p.ReturnsOrders)
                .HasForeignKey(d => d.PurchaseId)
                .HasConstraintName("FK__ReturnsOr__purch__114A936A");
        });

        modelBuilder.Entity<SparePart>(entity =>
        {
            entity.HasKey(e => e.SparePartsId).HasName("PK__SparePar__8FC6357ECC95072B");

            entity.Property(e => e.SparePartsId).HasColumnName("sparePartsID");
            entity.Property(e => e.Activated).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasDefaultValue("")
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReorderPoint)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("reorderPoint");
            entity.Property(e => e.TotalBalance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalBalance");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.HasKey(e => e.SuppliesId).HasName("PK__Supplies__C654A0F42469D3E3");

            entity.Property(e => e.SuppliesId).HasColumnName("suppliesID");
            entity.Property(e => e.Activated).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasDefaultValue("")
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReorderPoint)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("reorderPoint");
            entity.Property(e => e.TotalBalance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalBalance");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PK__Vendor__EC65C4E38167C426");

            entity.ToTable("Vendor");

            entity.HasIndex(e => e.VendorEmail, "UQ__Vendor__61AB83AB173F108A").IsUnique();

            entity.HasIndex(e => e.VendorPhone, "UQ__Vendor__8FB8D3A36DD2A066").IsUnique();

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