use ThothSystem;
----------------------------------------------------------
-- 1. Insert 30 records into Customer
----------------------------------------------------------
DECLARE @i INT = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO Customer (customerName, customerAddress, customerEmail, customerNotes, customerPhone)
    VALUES (
        'Customer' + CAST(@i AS VARCHAR(10)),
        'Address' + CAST(@i AS VARCHAR(10)),
        'customer' + CAST(@i AS VARCHAR(10)) + '@example.com',
        'Notes' + CAST(@i AS VARCHAR(10)),
        CAST(1111111110 + @i AS VARCHAR(10))
    );
    SET @i = @i + 1;
END;
GO
----------------------------------------------------------
-- 2. Insert 30 records into Vendor
----------------------------------------------------------
DECLARE @i INT = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO Vendor (vendorName, vendorAddress, vendorEmail, vendorNotes, vendorPhone)
    VALUES (
        'Vendor' + CAST(@i AS VARCHAR(10)),
        'Address' + CAST(@i AS VARCHAR(10)),
        'vendor' + CAST(@i AS VARCHAR(10)) + '@example.com',
        'Notes' + CAST(@i AS VARCHAR(10)),
        CAST(2222222220 + @i AS VARCHAR(10))
    );
    SET @i = @i + 1;
END;
GO
----------------------------------------------------------
-- 3. Insert 30 records into Employee
----------------------------------------------------------
DECLARE @i INT = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO Employee (employeeID, employeeUserName, employeePassword, employeeName, jobRole)
    VALUES (
        'E' + CAST(@i AS VARCHAR(10)),
        'user' + CAST(@i AS VARCHAR(10)),
        'pass' + CAST(@i AS VARCHAR(10)),
        'Employee' + CAST(@i AS VARCHAR(10)),
        'Role' + CAST(@i AS VARCHAR(10))
    );
    SET @i = @i + 1;
END;
GO
----------------------------------------------------------
-- 4. Insert 30 records into Labour
----------------------------------------------------------
DECLARE @i INT = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO Labour (labourProcessName, price)
    VALUES (
        'LabourProcess' + CAST(@i AS VARCHAR(10)),
        @i * 10.00
    );
    SET @i = @i + 1;
END;
GO
----------------------------------------------------------
-- 5. Insert 30 records into Machine
----------------------------------------------------------
DECLARE @i INT = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO Machine (machineProcessName, price)
    VALUES (
        'Machine' + CAST(@i AS VARCHAR(10)),
        @i * 100.00
    );
    SET @i = @i + 1;
END;
GO
----------------------------------------------------------
-- 6. Insert 30 records into Paper
----------------------------------------------------------
DECLARE @i INT = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO Paper (name, type, weight, totalBalance, colored, quantity, price, reorderPoint)
    VALUES (
        'Paper' + CAST(@i AS VARCHAR(10)),
        'Type' + CAST(@i AS VARCHAR(10)),
        50.00,
        100.00,
        'Yes',
        100,
        5.00,
        10.00
    );
    SET @i = @i + 1;
END;
GO
----------------------------------------------------------
-- 7. Insert 30 records into Ink
----------------------------------------------------------
DECLARE @i INT = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO Ink (name, totalBalance, colored, price, expireDate, quantity, reorderPoint)
    VALUES (
        'Ink' + CAST(@i AS VARCHAR(10)),
        100.00,
        'No',
        10.00,
        '2025-12-31',
        50,
        5.00
    );
    SET @i = @i + 1;
END;
GO
----------------------------------------------------------
-- 8. Insert 30 records into Supplies
----------------------------------------------------------
DECLARE @i INT = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO Supplies (name, totalBalance, price, quantity, reorderPoint)
    VALUES (
        'Supplies' + CAST(@i AS VARCHAR(10)),
        200.00,
        15.00,
        30,
        5.00
    );
    SET @i = @i + 1;
END;
GO
----------------------------------------------------------
-- 9. Insert 30 records into JobOrder
----------------------------------------------------------
DECLARE @i INT = 1;
WHILE @i <= 30
BEGIN
    DECLARE @progress VARCHAR(20);
    SET @progress = CASE 
                      WHEN (@i % 3) = 1 THEN 'Pending'
                      WHEN (@i % 3) = 2 THEN 'In Progress'
                      ELSE 'Completed'
                    END;
    INSERT INTO JobOrder (remainingAmount, unearnedRevenue, jobOrdernotes, earnedRevenue, orderProgress, customerID, startDate, endDate, employeeID)
    VALUES (
        @i * 5.00,
        @i * 2.00,
        'JobOrder' + CAST(@i AS VARCHAR(10)),
        @i * 3.00,
        @progress,
        @i,                             -- References CustomerID (assumed 1–30)
        '2025-01-01',
        DATEADD(DAY, 1, '2025-01-01'),   -- Ensures startDate < endDate
        'E' + CAST(@i AS VARCHAR(10))    -- References EmployeeID (E1–E30)
    );
    SET @i = @i + 1;
END;
GO
----------------------------------------------------------
-- 10. Insert 30 records into ProcessBridge
----------------------------------------------------------
DECLARE @i INT = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO ProcessBridge (jobOrderID, machineID, labourID, totalMachinePrice, totalLabourPrice, numberOfHours, employeeID)
    VALUES (
        @i,                             -- JobOrderID (1–30)
        @i,                             -- MachineID (1–30)
        @i,                             -- LabourID (1–30)
        100.00,
        50.00,
        2.5,
        'E' + CAST(@i AS VARCHAR(10))
    );
    SET @i = @i + 1;
END;
GO
----------------------------------------------------------
-- 11. Insert 30 records into MiscellaneousExpenses
----------------------------------------------------------
DECLARE @i INT = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO MiscellaneousExpenses (
        jobOrderID, employeeID, materialProcessingExpense, filmsProcessingExpense, 
        materialsTotal, totalAfterMaterials, adminstrativeExpense, totalExpenses, 
        percentage, totalAfterPercentage, ministryOfFinance, employeeImprovmentBox, 
        valueAddedTax, finalTotal
    )
    VALUES (
        @i,
        'E' + CAST(@i AS VARCHAR(10)),
        10.00,
        5.00,
        15.00,
        15.00,
        2.00,
        17.00,
        5.00,
        12.00,
        3.00,
        1.00,
        0.50,
        12.50
    );
    SET @i = @i + 1;
END;
GO
----------------------------------------------------------
-- 12. Insert 30 records into ReturnsOrder
----------------------------------------------------------
DECLARE @i INT = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO ReturnsOrder (returnDate, jobOrderID, employeeID, returnsNotes)
    VALUES (
        '2025-01-01',
        @i,
        'E' + CAST(@i AS VARCHAR(10)),
        'Return Order' + CAST(@i AS VARCHAR(10))
    );
    SET @i = @i + 1;
END;
GO
----------------------------------------------------------
-- 13. Insert 30 records into RequisiteOrder
----------------------------------------------------------
DECLARE @i INT = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO RequisiteOrder (requisiteDate, employeeID, jobOrderID, requisiteNotes)
    VALUES (
        '2025-01-01',
        'E' + CAST(@i AS VARCHAR(10)),
        @i,
        'Requisite Order' + CAST(@i AS VARCHAR(10))
    );
    SET @i = @i + 1;
END;
GO
----------------------------------------------------------
-- 14. Insert 30 records into PurchaseOrder
----------------------------------------------------------
DECLARE @i INT = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO PurchaseOrder (purchaseDate, employeeID, vendorID, purchaseNotes)
    VALUES (
        '2025-01-01',
        'E' + CAST(@i AS VARCHAR(10)),
        @i,                             -- References VendorID (1–30)
        'Purchase Order' + CAST(@i AS VARCHAR(10))
    );
    SET @i = @i + 1;
END;
GO
----------------------------------------------------------
-- 15. Insert 30 records into PhysicalCountOrder
----------------------------------------------------------
DECLARE @i INT = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO PhysicalCountOrder (employeeID, physicalCountDate, physicalCountNotes)
    VALUES (
        'E' + CAST(@i AS VARCHAR(10)),
        '2025-01-01',
        'Physical Count Order' + CAST(@i AS VARCHAR(10))
    );
    SET @i = @i + 1;
END;
GO
----------------------------------------------------------
-- 16. Insert 30 records into QuantityBridge
----------------------------------------------------------
DECLARE @i INT = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO QuantityBridge (price, returnID, purchaseID, quantity, requisiteID, paperID, inkID, suppliesID, physicalCountID)
    VALUES (
        20.00,
        @i,   -- returnID from ReturnsOrder (1–30)
        @i,   -- purchaseID from PurchaseOrder (1–30)
        5,
        @i,   -- requisiteID from RequisiteOrder (1–30)
        @i,   -- paperID from Paper (1–30)
        @i,   -- inkID from Ink (1–30)
        @i,   -- suppliesID from Supplies (1–30)
        @i    -- physicalCountID from PhysicalCountOrder (1–30)
    );
    SET @i = @i + 1;
END;
GO