use ThothSystem;

------------------------------------------------------------
-- 1. Insert 30 Customers
------------------------------------------------------------
DECLARE @i INT = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO Customer (customerName, customerAddress, customerEmail, customerNotes, customerPhone)
    VALUES (
        N'عميل ' + CAST(@i AS NVARCHAR(10)),
        N'عنوان العميل ' + CAST(@i AS NVARCHAR(10)),
        'customer' + CAST(@i AS NVARCHAR(10)) + '@example.com',
        N'ملاحظات العميل ' + CAST(@i AS NVARCHAR(10)),
        '01000000' + RIGHT('00' + CAST(@i AS VARCHAR(2)),2)
    );
    SET @i = @i + 1;
END;
GO

------------------------------------------------------------
-- 2. Insert 30 Vendors
------------------------------------------------------------
dECLARE @i int = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO Vendor (vendorName, vendorAddress, vendorEmail, vendorNotes, vendorPhone)
    VALUES (
        N'مورد ' + CAST(@i AS NVARCHAR(10)),
        N'عنوان المورد ' + CAST(@i AS NVARCHAR(10)),
        'vendor' + CAST(@i AS NVARCHAR(10)) + '@example.com',
        N'ملاحظات المورد ' + CAST(@i AS NVARCHAR(10)),
        '01100000' + RIGHT('00' + CAST(@i AS VARCHAR(2)),2)
    );
    SET @i = @i + 1;
END;
GO

------------------------------------------------------------
-- 3. Insert 30 Employees
------------------------------------------------------------
dECLARE @i int = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO Employee (employeeID, employeeUserName, employeePassword, employeeName, jobRole)
    VALUES (
        'EMP' + RIGHT('000' + CAST(@i AS VARCHAR(3)),3),
        N'مستخدم ' + CAST(@i AS NVARCHAR(10)),
        N'كلمةالسر' + CAST(@i AS NVARCHAR(10)),
        N'الموظف ' + CAST(@i AS NVARCHAR(10)),
        ((@i - 1) % 3) + 1  -- cycles through job roles 1,2,3
    );
    SET @i = @i + 1;
END;
GO

------------------------------------------------------------
-- 4. Insert 30 Labour Records
------------------------------------------------------------
dECLARE @i int = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO Labour (labourProcessName, price)
    VALUES (
        N'عملية ' + CAST(@i AS NVARCHAR(10)),
        50.00 + (@i * 10)   -- price > 0.0
    );
    SET @i = @i + 1;
END;
GO

------------------------------------------------------------
-- 5. Insert 30 Machine Records
------------------------------------------------------------
dECLARE @i int = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO Machine (machineProcessName, price)
    VALUES (
        N'آلة ' + CAST(@i AS NVARCHAR(10)),
        200.00 + (@i * 20)  -- price > 0
    );
    SET @i = @i + 1;
END;
GO

------------------------------------------------------------
-- 6. Insert 30 Paper Records
------------------------------------------------------------
dECLARE @i int = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO Paper (name, type, weight, totalBalance, colored, quantity, price, reorderPoint)
    VALUES (
        N'ورقة ' + CAST(@i AS NVARCHAR(10)),
        N'A4',
        80.00,                -- weight (>= 0)
        100.00 + @i,          -- totalBalance
        N'أبيض',
        100 + @i,             -- quantity (>= 0)
        1.50,                 -- price (> 0)
        50.00                 -- reorderPoint
    );
    SET @i = @i + 1;
END;
GO

------------------------------------------------------------
-- 7. Insert 30 Ink Records
------------------------------------------------------------
dECLARE @i int = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO Ink (name, totalBalance, colored, price, quantity, reorderPoint)
    VALUES (
        N'حبر ' + CAST(@i AS NVARCHAR(10)),
        100.00 + @i,
        N'أسود',
        2.50,                 -- price (> 0)
        200 + @i,             -- quantity (>= 0)
        50.00                 -- reorderPoint
    );
    SET @i = @i + 1;
END;
GO

------------------------------------------------------------
-- 8. Insert 30 Supplies Records
------------------------------------------------------------
dECLARE @i int = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO Supplies (name, totalBalance, price, quantity, reorderPoint)
    VALUES (
        N'مستلزمات ' + CAST(@i AS NVARCHAR(10)),
        0.0,
        5.00,                 -- price (> 0)
        30 + @i,              -- quantity (> 0)
        20.00                 -- reorderPoint
    );
    SET @i = @i + 1;
END;
GO

------------------------------------------------------------
-- 9. Insert 30 JobOrder Records
------------------------------------------------------------
dECLARE @i int = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO JobOrder (remainingAmount, unearnedRevenue, jobOrdernotes, earnedRevenue, orderProgress, customerID, endDate, employeeID)
    VALUES (
        0.00, 
        0.00, 
        N'ملاحظات الطلب ' + CAST(@i AS NVARCHAR(10)),
        100.00 + @i, 
        N'قيد الانتظار', 
        @i,  -- customerID (must exist)
        DATEADD(day, 10, GETDATE()),  -- endDate (startDate < endDate)
        'EMP' + RIGHT('000' + CAST(@i AS VARCHAR(3)),3)  -- employeeID (must exist)
    );
    SET @i = @i + 1;
END;
GO

------------------------------------------------------------
-- 10. Insert 30 PurchaseOrder Records
------------------------------------------------------------
dECLARE @i int = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO PurchaseOrder (employeeID, vendorID, purchaseNotes)
    VALUES (
        'EMP' + RIGHT('000' + CAST(@i AS VARCHAR(3)),3), -- employeeID exists
        @i,                                              -- vendorID exists
        N'ملاحظات طلب الشراء ' + CAST(@i AS NVARCHAR(10))
    );
    SET @i = @i + 1;
END;
GO

------------------------------------------------------------
-- 11. Insert 30 PhysicalCountOrder Records
------------------------------------------------------------
dECLARE @i int = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO PhysicalCountOrder (employeeID, physicalCountNotes)
    VALUES (
        'EMP' + RIGHT('000' + CAST(@i AS VARCHAR(3)),3),
        N'الجرد الفعلي ' + CAST(@i AS NVARCHAR(10))
    );
    SET @i = @i + 1;
END;
GO

------------------------------------------------------------
-- 12. Insert 30 ProcessBridge Records
------------------------------------------------------------
dECLARE @i int = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO ProcessBridge (jobOrderID, machineID, labourID, totalMachinePrice, totalLabourPrice, numberOfHours, employeeID)
    VALUES (
        @i,  -- jobOrderID exists
        @i,  -- machineID exists
        @i,  -- labourID exists
        200.00 + (@i * 20),  -- totalMachinePrice (>= 0)
        50.00 + (@i * 10),   -- totalLabourPrice (>= 0)
        2.5,                 -- numberOfHours (>= 0)
        'EMP' + RIGHT('000' + CAST(@i AS VARCHAR(3)),3)  -- employeeID exists
    );
    SET @i = @i + 1;
END;
GO

------------------------------------------------------------
-- 13. Insert 30 MiscellaneousExpenses Records
------------------------------------------------------------
dECLARE @i int = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO MiscellaneousExpenses (
        jobOrderID, employeeID, materialProcessingExpense, filmsProcessingExpense, 
        materialsTotal, totalAfterMaterials, adminstrativeExpense, totalExpenses, 
        percentage, totalAfterPercentage, ministryOfFinance, employeeImprovmentBox, 
        valueAddedTax, finalTotal
    )
    VALUES (
        @i,  -- jobOrderID exists
        'EMP' + RIGHT('000' + CAST(@i AS VARCHAR(3)),3),
        10.00 + @i,   -- materialProcessingExpense (>= 0)
        5.00,         -- filmsProcessingExpense (>= 0)
        20.00 + @i,   -- materialsTotal (>= 0)
        30.00 + @i,   -- totalAfterMaterials (>= 0)
        2.00,         -- adminstrativeExpense (>= 0)
        32.00 + @i,   -- totalExpenses (>= 0)
        5.00,         -- percentage (>= 0)
        37.00 + @i,   -- totalAfterPercentage (>= 0)
        10.00,        -- ministryOfFinance
        5.00,         -- employeeImprovmentBox
        2.00,         -- valueAddedTax
        44.00 + @i    -- finalTotal (>= 0)
    );
    SET @i = @i + 1;
END;
GO

------------------------------------------------------------
-- 14. Insert 30 ReturnsOrder Records
------------------------------------------------------------
dECLARE @i int = 1;
WHILE @i <= 30
BEGIN
    -- Alternate: even rows use jobOrderID; odd rows use purchaseID.
    IF (@i % 2 = 0)
    BEGIN
        INSERT INTO ReturnsOrder (returnDate, jobOrderID, purchaseID, employeeID, returnsNotes)
        VALUES (
            GETDATE(),
            @i,      -- jobOrderID exists
            NULL,
            'EMP' + RIGHT('000' + CAST(@i AS VARCHAR(3)),3),
            N'ملاحظات مرتجع الطلب ' + CAST(@i AS NVARCHAR(10))
        );
    END
    ELSE
    BEGIN
        INSERT INTO ReturnsOrder (returnDate, jobOrderID, purchaseID, employeeID, returnsNotes)
        VALUES (
            GETDATE(),
            NULL,
            @i,      -- purchaseID exists
            'EMP' + RIGHT('000' + CAST(@i AS VARCHAR(3)),3),
            N'ملاحظات مرتجع الطلب ' + CAST(@i AS NVARCHAR(10))
        );
    END
    SET @i = @i + 1;
END;
GO

------------------------------------------------------------
-- 15. Insert 30 RequisiteOrder Records
------------------------------------------------------------
dECLARE @i int = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO RequisiteOrder (requisiteDate, employeeID, jobOrderID, requisiteNotes)
    VALUES (
        GETDATE(),
        'EMP' + RIGHT('000' + CAST(@i AS VARCHAR(3)),3),
        @i,  -- jobOrderID exists
        N'ملاحظات طلب المستلزمات ' + CAST(@i AS NVARCHAR(10))
    );
    SET @i = @i + 1;
END;
GO

------------------------------------------------------------
-- 16. Insert 30 QuantityBridge Records
------------------------------------------------------------
dECLARE @i int = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO QuantityBridge (price, returnID, purchaseID, quantity, requisiteID, paperID, inkID, suppliesID, physicalCountID)
    VALUES (
        10.00, 
        @i,       -- returnID (from ReturnsOrder)
        @i,       -- purchaseID (from PurchaseOrder)
        5 + @i,   -- quantity (> 0)
        @i,       -- requisiteID (from RequisiteOrder)
        @i,       -- paperID (from Paper)
        @i,       -- inkID (from Ink)
        @i,       -- suppliesID (from Supplies)
        @i        -- physicalCountID (from PhysicalCountOrder)
    );
    SET @i = @i + 1;
END;
GO
