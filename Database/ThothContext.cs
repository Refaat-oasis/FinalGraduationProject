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

    public virtual DbSet<PhysicalCountOrder> PhysicalCountOrders { get; set; }

    public virtual DbSet<ProcessBridge> ProcessBridges { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<QuantityBridge> QuantityBridges { get; set; }

    public virtual DbSet<RequisiteOrder> RequisiteOrders { get; set; }

    public virtual DbSet<ReturnsOrder> ReturnsOrders { get; set; }

    public virtual DbSet<Supply> Supplies { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-VAR1OCQ;Database=ThothSystem;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__B611CB9DFF82C71D");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.CustomerPhone, "UQ__Customer__311068C4226DA7F2").IsUnique();

            entity.HasIndex(e => e.CustomerEmail, "UQ__Customer__FFE82D7214F0CBFB").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("customerID");
            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("customerAddress");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("customerEmail");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("customerName");
            entity.Property(e => e.CustomerNotes)
                .HasMaxLength(2500)
                .IsUnicode(false)
                .HasColumnName("customerNotes");
            entity.Property(e => e.CustomerPhone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("customerPhone");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C134C9A1479DF214");

            entity.ToTable("Employee", tb =>
                {
                    tb.HasTrigger("trg_DeleteEmployee");
                    tb.HasTrigger("trg_DeleteEmployee_PurchaseOrder");
                    tb.HasTrigger("trg_UpdateEmployee");
                    tb.HasTrigger("trg_UpdateEmployee_PurchaseOrder");
                });

            entity.HasIndex(e => e.EmployeeUserName, "UQ__Employee__8D8E404710088022").IsUnique();

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("employeeID");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("employeeName");
            entity.Property(e => e.EmployeePassword)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("employeePassword");
            entity.Property(e => e.EmployeeUserName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("employeeUserName");
            entity.Property(e => e.JobRole)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("jobRole");
        });

        modelBuilder.Entity<Ink>(entity =>
        {
            entity.HasKey(e => e.InkId).HasName("PK__Ink__13DA87035C75B095");

            entity.ToTable("Ink");

            entity.HasIndex(e => e.Name, "UQ__Ink__72E12F1BC5616048").IsUnique();

            entity.Property(e => e.InkId).HasColumnName("inkID");
            entity.Property(e => e.Colored)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("colored");
            entity.Property(e => e.ExpireDate).HasColumnName("expireDate");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReorderPoint)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("reorderPoint");
            entity.Property(e => e.TotalBalance)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalBalance");
        });

        modelBuilder.Entity<JobOrder>(entity =>
        {
            entity.HasKey(e => e.JobOrderId).HasName("PK__JobOrder__759794657C6FFA56");

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
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("earnedRevenue");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("employeeID");
            entity.Property(e => e.EndDate).HasColumnName("endDate");
            entity.Property(e => e.JobOrdernotes)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("jobOrdernotes");
            entity.Property(e => e.OrderProgress)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending")
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
                .HasConstraintName("FK__JobOrder__custom__4E88ABD4");

            entity.HasOne(d => d.Employee).WithMany(p => p.JobOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__JobOrder__employ__4F7CD00D");
        });

        modelBuilder.Entity<Labour>(entity =>
        {
            entity.HasKey(e => e.LabourId).HasName("PK__Labour__681E871C9EFF8743");

            entity.ToTable("Labour");

            entity.Property(e => e.LabourId).HasColumnName("labourID");
            entity.Property(e => e.LabourProcessName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("labourProcessName");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<Machine>(entity =>
        {
            entity.HasKey(e => e.MachineId).HasName("PK__Machine__D1ABE00D78DF6C03");

            entity.ToTable("Machine");

            entity.HasIndex(e => e.MachineProcessName, "UQ__Machine__D4D910C31A5E9ADB").IsUnique();

            entity.Property(e => e.MachineId).HasColumnName("machineID");
            entity.Property(e => e.MachineProcessName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("machineProcessName");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<MiscellaneousExpense>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.AdminstrativeExpense)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("adminstrativeExpense");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("employeeID");
            entity.Property(e => e.EmployeeImprovmentBox)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("employeeImprovmentBox");
            entity.Property(e => e.FilmsProcessingExpense)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("filmsProcessingExpense");
            entity.Property(e => e.FinalTotal)
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
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("ministryOfFinance");
            entity.Property(e => e.Percentage)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("percentage");
            entity.Property(e => e.TotalAfterMaterials)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalAfterMaterials");
            entity.Property(e => e.TotalAfterPercentage)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalAfterPercentage");
            entity.Property(e => e.TotalExpenses)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalExpenses");
            entity.Property(e => e.ValueAddedTax)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valueAddedTax");

            entity.HasOne(d => d.Employee).WithMany()
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Miscellan__emplo__6B24EA82");

            entity.HasOne(d => d.JobOrder).WithMany()
                .HasForeignKey(d => d.JobOrderId)
                .HasConstraintName("FK__Miscellan__jobOr__6A30C649");
        });

        modelBuilder.Entity<Paper>(entity =>
        {
            entity.HasKey(e => e.PaperId).HasName("PK__Paper__396E28531565A404");

            entity.ToTable("Paper");

            entity.Property(e => e.PaperId).HasColumnName("paperID");
            entity.Property(e => e.Colored)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("colored");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReorderPoint)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("reorderPoint");
            entity.Property(e => e.TotalBalance)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalBalance");
            entity.Property(e => e.Type)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("type");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("weight");
        });

        modelBuilder.Entity<PhysicalCountOrder>(entity =>
        {
            entity.HasKey(e => e.PhysicalCountId).HasName("PK__Physical__4282884694D2ED7D");

            entity.ToTable("PhysicalCountOrder");

            entity.Property(e => e.PhysicalCountId).HasColumnName("physicalCountID");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("employeeID");
            entity.Property(e => e.PhysicalCountDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("physicalCountDate");
            entity.Property(e => e.PhysicalCountNotes)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("physicalCountNotes");

            entity.HasOne(d => d.Employee).WithMany(p => p.PhysicalCountOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__PhysicalC__emplo__0D7A0286");
        });

        modelBuilder.Entity<ProcessBridge>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProcessBridge");

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("employeeID");
            entity.Property(e => e.JobOrderId).HasColumnName("jobOrderID");
            entity.Property(e => e.LabourId).HasColumnName("labourID");
            entity.Property(e => e.MachineId).HasColumnName("machineID");
            entity.Property(e => e.NumberOfHours)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("numberOfHours");
            entity.Property(e => e.TotalLabourPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalLabourPrice");
            entity.Property(e => e.TotalMachinePrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalMachinePrice");

            entity.HasOne(d => d.Employee).WithMany()
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__ProcessBr__emplo__5BE2A6F2");

            entity.HasOne(d => d.JobOrder).WithMany()
                .HasForeignKey(d => d.JobOrderId)
                .HasConstraintName("FK__ProcessBr__jobOr__59063A47");

            entity.HasOne(d => d.Labour).WithMany()
                .HasForeignKey(d => d.LabourId)
                .HasConstraintName("FK__ProcessBr__labou__5AEE82B9");

            entity.HasOne(d => d.Machine).WithMany()
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK__ProcessBr__machi__59FA5E80");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK__Purchase__0261224C1C0D236F");

            entity.ToTable("PurchaseOrder");

            entity.Property(e => e.PurchaseId).HasColumnName("purchaseID");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("employeeID");
            entity.Property(e => e.PurchaseDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("purchaseDate");
            entity.Property(e => e.PurchaseNotes)
                .HasMaxLength(2500)
                .IsUnicode(false)
                .HasColumnName("purchaseNotes");
            entity.Property(e => e.VendorId).HasColumnName("vendorID");

            entity.HasOne(d => d.Employee).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseO__emplo__08B54D69");

            entity.HasOne(d => d.Vendor).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseO__vendo__09A971A2");
        });

        modelBuilder.Entity<QuantityBridge>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("QuantityBridge");

            entity.Property(e => e.InkId).HasColumnName("inkID");
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

            entity.HasOne(d => d.Ink).WithMany()
                .HasForeignKey(d => d.InkId)
                .HasConstraintName("FK__QuantityB__inkID__14270015");

            entity.HasOne(d => d.Paper).WithMany()
                .HasForeignKey(d => d.PaperId)
                .HasConstraintName("FK__QuantityB__paper__151B244E");

            entity.HasOne(d => d.PhysicalCount).WithMany()
                .HasForeignKey(d => d.PhysicalCountId)
                .HasConstraintName("FK__QuantityB__physi__17036CC0");

            entity.HasOne(d => d.Purchase).WithMany()
                .HasForeignKey(d => d.PurchaseId)
                .HasConstraintName("FK__QuantityB__purch__123EB7A3");

            entity.HasOne(d => d.Requisite).WithMany()
                .HasForeignKey(d => d.RequisiteId)
                .HasConstraintName("FK__QuantityB__requi__1332DBDC");

            entity.HasOne(d => d.Return).WithMany()
                .HasForeignKey(d => d.ReturnId)
                .HasConstraintName("FK__QuantityB__retur__114A936A");

            entity.HasOne(d => d.Supplies).WithMany()
                .HasForeignKey(d => d.SuppliesId)
                .HasConstraintName("FK__QuantityB__suppl__160F4887");
        });

        modelBuilder.Entity<RequisiteOrder>(entity =>
        {
            entity.HasKey(e => e.RequisiteId).HasName("PK__Requisit__32556601EBBC3F48");

            entity.ToTable("RequisiteOrder");

            entity.Property(e => e.RequisiteId).HasColumnName("requisiteID");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("employeeID");
            entity.Property(e => e.JobOrderId).HasColumnName("jobOrderID");
            entity.Property(e => e.RequisiteDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("requisiteDate");
            entity.Property(e => e.RequisiteNotes)
                .HasMaxLength(2500)
                .IsUnicode(false)
                .HasColumnName("requisiteNotes");

            entity.HasOne(d => d.Employee).WithMany(p => p.RequisiteOrders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requisite__emplo__73BA3083");

            entity.HasOne(d => d.JobOrder).WithMany(p => p.RequisiteOrders)
                .HasForeignKey(d => d.JobOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requisite__jobOr__74AE54BC");
        });

        modelBuilder.Entity<ReturnsOrder>(entity =>
        {
            entity.HasKey(e => e.ReturnId).HasName("PK__ReturnsO__EBA763F90952C268");

            entity.ToTable("ReturnsOrder");

            entity.Property(e => e.ReturnId).HasColumnName("returnID");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("employeeID");
            entity.Property(e => e.JobOrderId).HasColumnName("jobOrderID");
            entity.Property(e => e.ReturnDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("returnDate");
            entity.Property(e => e.ReturnsNotes)
                .HasMaxLength(2500)
                .IsUnicode(false)
                .HasColumnName("returnsNotes");

            entity.HasOne(d => d.Employee).WithMany(p => p.ReturnsOrders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReturnsOr__emplo__6FE99F9F");

            entity.HasOne(d => d.JobOrder).WithMany(p => p.ReturnsOrders)
                .HasForeignKey(d => d.JobOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReturnsOr__jobOr__6EF57B66");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.HasKey(e => e.SuppliesId).HasName("PK__Supplies__C654A0F4076CB893");

            entity.HasIndex(e => e.Name, "UQ__Supplies__72E12F1BE684B78C").IsUnique();

            entity.Property(e => e.SuppliesId).HasColumnName("suppliesID");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReorderPoint)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("reorderPoint");
            entity.Property(e => e.TotalBalance)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalBalance");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PK__Vendor__EC65C4E3751E5BA4");

            entity.ToTable("Vendor");

            entity.HasIndex(e => e.VendorEmail, "UQ__Vendor__61AB83AB87D3BE8B").IsUnique();

            entity.HasIndex(e => e.VendorPhone, "UQ__Vendor__8FB8D3A3620D0CDB").IsUnique();

            entity.Property(e => e.VendorId).HasColumnName("vendorID");
            entity.Property(e => e.VendorAddress)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("vendorAddress");
            entity.Property(e => e.VendorEmail)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("vendorEmail");
            entity.Property(e => e.VendorName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("vendorName");
            entity.Property(e => e.VendorNotes)
                .HasMaxLength(2500)
                .IsUnicode(false)
                .HasColumnName("vendorNotes");
            entity.Property(e => e.VendorPhone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("vendorPhone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
