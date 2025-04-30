using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
        => optionsBuilder.UseSqlServer("Server=DESKTOP-2K9M8PS;Database=ThothSystem;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_100_CS_AI");

        modelBuilder.Entity<ColorWeightSize>(entity =>
        {
            entity.HasKey(e => e.ColorWeightSizeId).HasName("PK_ColorWei_CB626C94FAE272BF");

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
            entity.HasKey(e => e.CustomerId).HasName("PK_Customer_B611CB9D505F0A72");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.CustomerPhone, "UQ_Customer_311068C4D5C64637").IsUnique();

            entity.HasIndex(e => e.CustomerEmail, "UQ_Customer_FFE82D7216D6C4D9").IsUnique();

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
            entity.HasKey(e => e.EmployeeId).HasName("PK_Employee_C134C9A1A997094B");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.EmployeeUserName, "UQ_Employee_8D8E40478F1D472E").IsUnique();

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
                .HasDefaultValue(true)
                .HasColumnName("forgetpassword");
            entity.Property(e => e.JobRole).HasColumnName("jobRole");
        });

        modelBuilder.Entity<Ink>(entity =>
        {
            entity.HasKey(e => e.InkId).HasName("PK_Ink_13DA8703BABEE992");

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

        modelBuilder.Entity<JobOrder>(entity =>
        {
            entity.HasKey(e => e.JobOrderId).HasName("PK_JobOrder_759794651F212C2D");

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
                .HasConstraintName("FK_JobOrdercustom_6383C8BA");

            entity.HasOne(d => d.Employee).WithMany(p => p.JobOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_JobOrderemploy_6477ECF3");
        });

        modelBuilder.Entity<Labour>(entity =>
        {
            entity.HasKey(e => e.LabourId).HasName("PK_Labour_681E871CFB9E23DE");

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
            entity.HasKey(e => e.MachineId).HasName("PK_Machine_D1ABE00DBB46738B");

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
            entity.HasKey(e => e.MiscellaneousExpensesId).HasName("PK_Miscella_9EC2AFA3E8286D0B");

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
                .HasConstraintName("FK_Miscellanemplo_160F4887");

            entity.HasOne(d => d.JobOrder).WithMany(p => p.MiscellaneousExpenses)
                .HasForeignKey(d => d.JobOrderId)
                .HasConstraintName("FK_MiscellanjobOr_151B244E");
        });

        modelBuilder.Entity<Paper>(entity =>
        {
            entity.HasKey(e => e.PaperId).HasName("PK_Paper_396E2853EB3A8F9C");

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
            entity.HasKey(e => e.PaymentId).HasName("PK_PaymentO_A0D9EFA6BA13A792");

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
                .HasConstraintName("FK_PaymentOremplo_6442E2C9");

            entity.HasOne(d => d.Purchase).WithMany(p => p.PaymentOrders)
                .HasForeignKey(d => d.PurchaseId)
                .HasConstraintName("FK_PaymentOrpurch_634EBE90");
        });

        modelBuilder.Entity<PhysicalCountOrder>(entity =>
        {
            entity.HasKey(e => e.PhysicalCountId).HasName("PK_Physical_428288461C33FEF3");

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
                .HasConstraintName("FK_PhysicalCemplo_4F47C5E3");
        });

        modelBuilder.Entity<ProcessBridge>(entity =>
        {
            entity.HasKey(e => e.ProcessBridgeId).HasName("PK_ProcessB_0B3E7649781D8CDA");

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
            entity.Property(e => e.OldMachinePrice)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("oldMachinePrice");
            entity.Property(e => e.OldlabourPrice)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("oldlabourPrice");
            entity.Property(e => e.TotalLabourValue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalLabourValue");
            entity.Property(e => e.TotalMachineValue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalMachineValue");

            entity.HasOne(d => d.Employee).WithMany(p => p.ProcessBridges)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_ProcessBremplo_7A672E12");

            entity.HasOne(d => d.JobOrder).WithMany(p => p.ProcessBridges)
                .HasForeignKey(d => d.JobOrderId)
                .HasConstraintName("FK_ProcessBrjobOr_778AC167");

            entity.HasOne(d => d.Labour).WithMany(p => p.ProcessBridges)
                .HasForeignKey(d => d.LabourId)
                .HasConstraintName("FK_ProcessBrlabou_797309D9");

            entity.HasOne(d => d.Machine).WithMany(p => p.ProcessBridges)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK_ProcessBrmachi_787EE5A0");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK_Purchase_0261224C5D7AB821");

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
                .HasConstraintName("FK_PurchaseOemplo_1EA48E88");

            entity.HasOne(d => d.Vendor).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseOvendo_1F98B2C1");
        });

        modelBuilder.Entity<QuantityBridge>(entity =>
        {
            entity.HasKey(e => e.QuantityBridgeId).HasName("PK_Quantity_74C92419315EA66C");

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
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("oldTotalBalance");
            entity.Property(e => e.PaperId).HasColumnName("paperID");
            entity.Property(e => e.PhysicalCountId).HasColumnName("physicalCountID");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.PurchaseId).HasColumnName("purchaseID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.RequisiteId).HasColumnName("requisiteID");
            entity.Property(e => e.ReturnId).HasColumnName("returnID");
            entity.Property(e => e.SuppliesId).HasColumnName("suppliesID");
            entity.Property(e => e.TotalBalance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalBalance");

            entity.HasOne(d => d.Ink).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.InkId)
                .HasConstraintName("FK_QuantityBinkID_59C55456");

            entity.HasOne(d => d.Paper).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.PaperId)
                .HasConstraintName("FK_QuantityBpaper_5AB9788F");

            entity.HasOne(d => d.PhysicalCount).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.PhysicalCountId)
                .HasConstraintName("FK_QuantityBphysi_5CA1C101");

            entity.HasOne(d => d.Purchase).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.PurchaseId)
                .HasConstraintName("FK_QuantityBpurch_57DD0BE4");

            entity.HasOne(d => d.Requisite).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.RequisiteId)
                .HasConstraintName("FK_QuantityBrequi_58D1301D");

            entity.HasOne(d => d.Return).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.ReturnId)
                .HasConstraintName("FK_QuantityBretur_56E8E7AB");

            entity.HasOne(d => d.Supplies).WithMany(p => p.QuantityBridges)
                .HasForeignKey(d => d.SuppliesId)
                .HasConstraintName("FK_QuantityBsuppl_5BAD9CC8");
        });

        modelBuilder.Entity<RecieptsOrder>(entity =>
        {
            entity.HasKey(e => e.RecieptId).HasName("PK_Reciepts_71D49F6382C45C01");

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
                .HasConstraintName("FK_RecieptsOemplo_6BE40491");

            entity.HasOne(d => d.JobOrder).WithMany(p => p.RecieptsOrders)
                .HasForeignKey(d => d.JobOrderId)
                .HasConstraintName("FK_RecieptsOjobOr_6AEFE058");
        });

        modelBuilder.Entity<RequisiteOrder>(entity =>
        {
            entity.HasKey(e => e.RequisiteId).HasName("PK_Requisit_32556601673ED26F");

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
                .HasConstraintName("FK_Requisiteemplo_2BFE89A6");

            entity.HasOne(d => d.JobOrder).WithMany(p => p.RequisiteOrders)
                .HasForeignKey(d => d.JobOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequisitejobOr_2CF2ADDF");
        });

        modelBuilder.Entity<ReturnsOrder>(entity =>
        {
            entity.HasKey(e => e.ReturnId).HasName("PK_ReturnsO_EBA763F92C063760");

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
                .HasConstraintName("FK_ReturnsOremplo_2739D489");

            entity.HasOne(d => d.JobOrder).WithMany(p => p.ReturnsOrders)
                .HasForeignKey(d => d.JobOrderId)
                .HasConstraintName("FK_ReturnsOrjobOr_25518C17");

            entity.HasOne(d => d.Purchase).WithMany(p => p.ReturnsOrders)
                .HasForeignKey(d => d.PurchaseId)
                .HasConstraintName("FK_ReturnsOrpurch_2645B050");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.HasKey(e => e.SuppliesId).HasName("PK_Supplies_C654A0F4B0355706");

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
            entity.HasKey(e => e.VendorId).HasName("PK_Vendor_EC65C4E3B8E71EED");

            entity.ToTable("Vendor");

            entity.HasIndex(e => e.VendorEmail, "UQ_Vendor_61AB83AB72A3ACF6").IsUnique();

            entity.HasIndex(e => e.VendorPhone, "UQ_Vendor_8FB8D3A3C2656EA4").IsUnique();

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