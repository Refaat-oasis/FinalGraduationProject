-- Create the database and switch to it
CREATE DATABASE ThothSystem;
GO

USE ThothSystem;
GO

---------------------------------
-- Tables Creation
---------------------------------

-- 1. Customer Table
CREATE TABLE Customer (
    customerID INT IDENTITY(1,1) PRIMARY KEY,
    customerName VARCHAR(255) NOT NULL,
    customerAddress VARCHAR(500) NULL,
    customerEmail VARCHAR(250) NULL UNIQUE,
    customerNotes VARCHAR(2500) NULL,
    customerPhone VARCHAR(15) NOT NULL UNIQUE
);
GO

-- 2. Vendor Table
CREATE TABLE Vendor (
    vendorID INT IDENTITY(1,1) PRIMARY KEY,
    vendorName VARCHAR(255) NOT NULL,
    vendorAddress VARCHAR(500) NULL,
    vendorEmail VARCHAR(250) NULL UNIQUE,
    vendorNotes VARCHAR(2500) NULL,
    vendorPhone VARCHAR(15) NOT NULL UNIQUE
);
GO

-- 3. Employee Table
CREATE TABLE Employee (
    employeeID VARCHAR(30) NOT NULL PRIMARY KEY,
    employeeUserName VARCHAR(255) NOT NULL UNIQUE,
    employeePassword VARCHAR(255) NOT NULL,
    employeeName VARCHAR(255) NOT NULL,
    jobRole VARCHAR(25) NOT NULL
);
GO

-- 4. Labour Table
CREATE TABLE Labour (
    labourID INT IDENTITY(1,1) PRIMARY KEY,
    labourProcessName VARCHAR(255) NOT NULL,
    price DECIMAL(10,2) NOT NULL CHECK (price > 0.0)
);
GO

-- 5. JobOrder Table
CREATE TABLE JobOrder (
    jobOrderID INT IDENTITY(1,1) PRIMARY KEY,
    remainingAmount DECIMAL(10,2) DEFAULT 0 CHECK (remainingAmount >= 0),
    unearnedRevenue DECIMAL(10,2) DEFAULT 0 CHECK (unearnedRevenue >= 0),
    jobOrdernotes VARCHAR(100) DEFAULT '',
    earnedRevenue DECIMAL(10,2) DEFAULT 0 CHECK (earnedRevenue >= 0),
    orderProgress VARCHAR(20) DEFAULT 'Pending' 
         CHECK (orderProgress IN ('Pending', 'In Progress', 'Completed')),
    customerID INT,
    startDate DATE DEFAULT GETDATE(),
    endDate DATE NOT NULL,
    employeeID VARCHAR(30),
    FOREIGN KEY (customerID) REFERENCES Customer(customerID),
    FOREIGN KEY (employeeID) REFERENCES Employee(employeeID),
    CHECK (startDate < endDate)
);
GO

-- 6. Machine Table
CREATE TABLE Machine (
    machineID INT IDENTITY(1,1) PRIMARY KEY,
    machineProcessName VARCHAR(100) NOT NULL UNIQUE,
    price DECIMAL(10,2) NOT NULL CHECK (price > 0)
);
GO

-- 7. ProcessBridge Table
CREATE TABLE ProcessBridge (
    jobOrderID INT,
    machineID INT,
    labourID INT,
    totalMachinePrice DECIMAL(10,2) NOT NULL CHECK (totalMachinePrice >= 0),
    totalLabourPrice DECIMAL(10,2) NOT NULL CHECK (totalLabourPrice >= 0),
    numberOfHours DECIMAL(10,2) NOT NULL CHECK (numberOfHours >= 0),
    employeeID VARCHAR(30),
    FOREIGN KEY (jobOrderID) REFERENCES JobOrder(jobOrderID),
    FOREIGN KEY (machineID) REFERENCES Machine(machineID),
    FOREIGN KEY (labourID) REFERENCES Labour(labourID),
    FOREIGN KEY (employeeID) REFERENCES Employee(employeeID)
);
GO

-- 8. MiscellaneousExpenses Table
CREATE TABLE MiscellaneousExpenses (
    jobOrderID INT,
    employeeID VARCHAR(30),
    materialProcessingExpense DECIMAL(10,2) DEFAULT 0 CHECK (materialProcessingExpense >= 0),
    filmsProcessingExpense DECIMAL(10,2) DEFAULT 0 CHECK (filmsProcessingExpense >= 0),
    materialsTotal DECIMAL(10,2) DEFAULT 0 CHECK (materialsTotal >= 0),
    totalAfterMaterials DECIMAL(10,2) NOT NULL CHECK (totalAfterMaterials >= 0),
    adminstrativeExpense DECIMAL(10,2) DEFAULT 0 CHECK (adminstrativeExpense >= 0),
    totalExpenses DECIMAL(10,2) NOT NULL CHECK (totalExpenses >= 0),
    percentage DECIMAL(10,2) NOT NULL CHECK (percentage >= 0),
    totalAfterPercentage DECIMAL(10,2) NOT NULL CHECK (totalAfterPercentage >= 0),
    ministryOfFinance DECIMAL(10,2) NOT NULL,
    employeeImprovmentBox DECIMAL(10,2) NOT NULL,
    valueAddedTax DECIMAL(10,2) NOT NULL,
    finalTotal DECIMAL(10,2) NOT NULL CHECK (finalTotal >= 0),
    FOREIGN KEY (jobOrderID) REFERENCES JobOrder(jobOrderID),
    FOREIGN KEY (employeeID) REFERENCES Employee(employeeID)
);
GO

-- 9. ReturnsOrder Table
CREATE TABLE ReturnsOrder (
    returnID INT IDENTITY(1,1) PRIMARY KEY,
    returnDate DATE DEFAULT GETDATE(),
    jobOrderID INT NOT NULL,
    employeeID VARCHAR(30) NOT NULL,
    returnsNotes VARCHAR(2500) NULL,
    FOREIGN KEY (jobOrderID) REFERENCES JobOrder(jobOrderID),
    FOREIGN KEY (employeeID) REFERENCES Employee(employeeID)
);
GO

-- 10. RequisiteOrder Table
CREATE TABLE RequisiteOrder (
    requisiteID INT IDENTITY(1,1) PRIMARY KEY,
    requisiteDate DATE DEFAULT GETDATE(),
    employeeID VARCHAR(30) NOT NULL,
    jobOrderID INT NOT NULL,
    requisiteNotes VARCHAR(2500) NULL,
    FOREIGN KEY (employeeID) REFERENCES Employee(employeeID),
    FOREIGN KEY (jobOrderID) REFERENCES JobOrder(jobOrderID)
);
GO

-- 11. Paper Table
CREATE TABLE Paper (
    paperID INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(30) NOT NULL,
    type VARCHAR(25) NULL,
    weight DECIMAL(10,2) NULL,
    totalBalance DECIMAL(10,2) DEFAULT 0.0,
    colored VARCHAR(10) NOT NULL,
    quantity INT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    reorderPoint DECIMAL(10,2) DEFAULT 0.0,
    CHECK (weight >= 0.0 AND price > 0.0 AND quantity >= 0)
);
GO

-- 12. Ink Table
CREATE TABLE Ink (
    inkID INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(30) NOT NULL UNIQUE,
    totalBalance DECIMAL(10,2) DEFAULT 0.00,
    colored VARCHAR(10) NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    expireDate DATE NOT NULL,
    quantity INT NOT NULL,
    reorderPoint DECIMAL(10,2) DEFAULT 0.0,
    CHECK (price > 0.0 AND quantity >= 0)
);
GO

-- 13. Supplies Table
CREATE TABLE Supplies (
    suppliesID INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(30) NOT NULL UNIQUE,
    totalBalance DECIMAL(10,2) DEFAULT 0.0,
    price DECIMAL(10,2) NOT NULL,
    quantity INT NOT NULL,
    reorderPoint DECIMAL(10,2) DEFAULT 0.0,
    CHECK (quantity > 0 AND price > 0.0)
);
GO

-- 14. PurchaseOrder Table
CREATE TABLE PurchaseOrder (
    purchaseID INT IDENTITY(1,1) PRIMARY KEY,
    purchaseDate DATE DEFAULT GETDATE(),
    employeeID VARCHAR(30) NOT NULL,
    vendorID INT NOT NULL,
    purchaseNotes VARCHAR(2500) NULL,
    FOREIGN KEY (employeeID) REFERENCES Employee(employeeID),
    FOREIGN KEY (vendorID) REFERENCES Vendor(vendorID)
);
GO

-- 15. PhysicalCountOrder Table
CREATE TABLE PhysicalCountOrder (
    physicalCountID INT IDENTITY(1,1) PRIMARY KEY,
    employeeID VARCHAR(30),
    physicalCountDate DATE DEFAULT GETDATE(),
    physicalCountNotes VARCHAR(30),
    FOREIGN KEY (employeeID) REFERENCES Employee(employeeID)
);
GO

-- 16. QuantityBridge Table
CREATE TABLE QuantityBridge (
    price DECIMAL(10,2) NOT NULL CHECK (price > 0.0),
    returnID INT NULL,
    purchaseID INT NULL,
    quantity INT NOT NULL CHECK (quantity > 0),
    requisiteID INT NULL,
    paperID INT NULL,
    inkID INT NULL,
    suppliesID INT NULL,
    physicalCountID INT NULL,
    FOREIGN KEY (returnID) REFERENCES ReturnsOrder(returnID),
    FOREIGN KEY (purchaseID) REFERENCES PurchaseOrder(purchaseID),
    FOREIGN KEY (requisiteID) REFERENCES RequisiteOrder(requisiteID),
    FOREIGN KEY (inkID) REFERENCES Ink(inkID),
    FOREIGN KEY (paperID) REFERENCES Paper(paperID),
    FOREIGN KEY (suppliesID) REFERENCES Supplies(suppliesID),
    FOREIGN KEY (physicalCountID) REFERENCES PhysicalCountOrder(physicalCountID)
);
GO

---------------------------------
-- Triggers Creation
---------------------------------

-- Trigger: Cascading DELETE from JobOrder to ProcessBridge
CREATE TRIGGER trg_DeleteJobOrder
ON JobOrder
FOR DELETE
AS
BEGIN
    DELETE FROM ProcessBridge
    WHERE jobOrderID IN (SELECT jobOrderID FROM deleted);
END;
GO

-- Trigger: Cascading UPDATE from JobOrder to ProcessBridge
CREATE TRIGGER trg_UpdateJobOrder
ON JobOrder
FOR UPDATE
AS
BEGIN
    -- Assume single-row update for the primary key
    DECLARE @oldJobOrderID INT, @newJobOrderID INT;
    SELECT @oldJobOrderID = jobOrderID FROM deleted;
    SELECT @newJobOrderID = jobOrderID FROM inserted;
    
    IF @oldJobOrderID <> @newJobOrderID
    BEGIN
        UPDATE ProcessBridge
        SET jobOrderID = @newJobOrderID
        WHERE jobOrderID = @oldJobOrderID;
    END
END;
GO

-- Trigger: Cascading DELETE from Employee to ProcessBridge
CREATE TRIGGER trg_DeleteEmployee
ON Employee
FOR DELETE
AS
BEGIN
    DELETE FROM ProcessBridge
    WHERE employeeID IN (SELECT employeeID FROM deleted);
END;
GO

-- Trigger: Cascading UPDATE from Employee to ProcessBridge
CREATE TRIGGER trg_UpdateEmployee
ON Employee
FOR UPDATE
AS
BEGIN
    -- Assume single-row update for the primary key
    DECLARE @oldEmployeeID VARCHAR(30), @newEmployeeID VARCHAR(30);
    SELECT @oldEmployeeID = employeeID FROM deleted;
    SELECT @newEmployeeID = employeeID FROM inserted;
    
    IF @oldEmployeeID <> @newEmployeeID
    BEGIN
        UPDATE ProcessBridge
        SET employeeID = @newEmployeeID
        WHERE employeeID = @oldEmployeeID;
    END
END;
GO

-- Trigger: Cascading DELETE from JobOrder to ReturnsOrder
CREATE TRIGGER trg_DeleteJobOrder_ReturnsOrder
ON JobOrder
FOR DELETE
AS
BEGIN
    DELETE FROM ReturnsOrder
    WHERE jobOrderID IN (SELECT jobOrderID FROM deleted);
END;
GO

-- Trigger: Cascading UPDATE from JobOrder to ReturnsOrder
CREATE TRIGGER trg_UpdateJobOrder_ReturnsOrder
ON JobOrder
FOR UPDATE
AS
BEGIN
    -- Assume single-row update for the primary key
    DECLARE @oldJobOrderID INT, @newJobOrderID INT;
    SELECT @oldJobOrderID = jobOrderID FROM deleted;
    SELECT @newJobOrderID = jobOrderID FROM inserted;
    
    IF @oldJobOrderID <> @newJobOrderID
    BEGIN
        UPDATE ReturnsOrder
        SET jobOrderID = @newJobOrderID
        WHERE jobOrderID = @oldJobOrderID;
    END
END;
GO

-- Trigger: Cascading DELETE from Employee to PurchaseOrder
CREATE TRIGGER trg_DeleteEmployee_PurchaseOrder
ON Employee
FOR DELETE
AS
BEGIN
    DELETE FROM PurchaseOrder
    WHERE employeeID IN (SELECT employeeID FROM deleted);
END;
GO

-- Trigger: Cascading UPDATE from Employee to PurchaseOrder
CREATE TRIGGER trg_UpdateEmployee_PurchaseOrder
ON Employee
FOR UPDATE
AS
BEGIN
    -- Assume single-row update for the primary key
    DECLARE @oldEmployeeID VARCHAR(30), @newEmployeeID VARCHAR(30);
    SELECT @oldEmployeeID = employeeID FROM deleted;
    SELECT @newEmployeeID = employeeID FROM inserted;
    
    IF @oldEmployeeID <> @newEmployeeID
    BEGIN
        UPDATE PurchaseOrder
        SET employeeID = @newEmployeeID
        WHERE employeeID = @oldEmployeeID;
    END
END;
GO