CREATE DATABASE ThothSystem;
GO

USE ThothSystem;
GO



ALTER DATABASE ThothSystem 
  SET SINGLE_USER 
  WITH ROLLBACK IMMEDIATE;

  ALTER DATABASE ThothSystem 
  COLLATE Arabic_100_CS_AI;


ALTER DATABASE ThothSystem 
  SET MULTI_USER;


-- Create the database and switch to it'


---------------------------------
-- Tables Creation
---------------------------------

-- 1. Customer Table
CREATE TABLE Customer (
    customerID INT IDENTITY(1,1) PRIMARY KEY,
    customerName NVARCHAR(255) NOT NULL DEFAULT '',
    customerAddress NVARCHAR(500) NULL DEFAULT '',
    customerEmail NVARCHAR(250) NULL UNIQUE DEFAULT '',
    customerNotes NVARCHAR(2500) NULL DEFAULT '',
    customerPhone NVARCHAR(15) NOT NULL UNIQUE DEFAULT '',
	Activated BIT DEFAULT 1
);
GO

-- 2. Vendor Table
CREATE TABLE Vendor (
    vendorID INT IDENTITY(1,1) PRIMARY KEY,
    vendorName NVARCHAR(255) NOT NULL DEFAULT '',
    vendorAddress NVARCHAR(500) NULL DEFAULT '',
    vendorEmail NVARCHAR(250) NULL UNIQUE DEFAULT '',
    vendorNotes NVARCHAR(2500) NULL DEFAULT '',
    vendorPhone NVARCHAR(15) NOT NULL UNIQUE DEFAULT '',
	Activated BIT DEFAULT 1
);
GO

-- 3. Employee Table
CREATE TABLE Employee (
    employeeID NVARCHAR(30) NOT NULL PRIMARY KEY,
    employeeUserName NVARCHAR(255) NOT NULL UNIQUE DEFAULT '',
    employeePassword NVARCHAR(255) NOT NULL DEFAULT '',
    employeeName NVARCHAR(255) NOT NULL DEFAULT '',
    jobRole INT NOT NULL DEFAULT 0,
	Activated BIT DEFAULT 1
);
GO

-- 4. Labour Table
CREATE TABLE Labour (
    labourID INT IDENTITY(1,1) PRIMARY KEY,
    labourProcessName NVARCHAR(255) NOT NULL DEFAULT '',
    price DECIMAL(10,2) NOT NULL DEFAULT 1 CHECK (price > 0.0),
	Activated BIT DEFAULT 1
);
GO

-- 5. JobOrder Table
CREATE TABLE JobOrder (
    jobOrderID INT IDENTITY(1,1) PRIMARY KEY,
    remainingAmount DECIMAL(10,2) DEFAULT 1 CHECK (remainingAmount >= 0),
    unearnedRevenue DECIMAL(10,2) DEFAULT 1 CHECK (unearnedRevenue >= 0),
    jobOrdernotes NVARCHAR(100) DEFAULT '',
    earnedRevenue DECIMAL(10,2) DEFAULT 1 CHECK (earnedRevenue >= 0),
    orderProgress NVARCHAR(20) DEFAULT 'قيد الانتظار' ,
    customerID INT,
    startDate DATE DEFAULT GETDATE(),
    endDate DATE NULL,
    employeeID NVARCHAR(30),
    FOREIGN KEY (customerID) REFERENCES Customer(customerID),
    FOREIGN KEY (employeeID) REFERENCES Employee(employeeID),
    CHECK (startDate < endDate)
);
GO

-- 6. Machine Table
CREATE TABLE Machine (
    machineID INT IDENTITY(1,1) PRIMARY KEY,
    machineProcessName NVARCHAR(100) NOT NULL DEFAULT '',
    price DECIMAL(10,2) NOT NULL DEFAULT 1 CHECK (price > 0),
	Activated BIT DEFAULT 1
);
GO

-- 7. ProcessBridge Table
CREATE TABLE ProcessBridge (
    ProcessBridgeID INT IDENTITY(1,1) PRIMARY KEY,
    jobOrderID INT,
    machineID INT,
    labourID INT,
    totalMachinePrice DECIMAL(10,2) DEFAULT 1 NOT NULL CHECK (totalMachinePrice >= 0),
    totalLabourPrice DECIMAL(10,2) NOT NULL DEFAULT 1 CHECK (totalLabourPrice >= 0),
    numberOfHours DECIMAL(10,2) NOT NULL DEFAULT 1 CHECK (numberOfHours >= 0),
    employeeID NVARCHAR(30),
    FOREIGN KEY (jobOrderID) REFERENCES JobOrder(jobOrderID),
    FOREIGN KEY (machineID) REFERENCES Machine(machineID),
    FOREIGN KEY (labourID) REFERENCES Labour(labourID),
    FOREIGN KEY (employeeID) REFERENCES Employee(employeeID)
);
GO

-- 8. MiscellaneousExpenses Table
CREATE TABLE MiscellaneousExpenses (
    MiscellaneousExpensesID INT IDENTITY(1,1) PRIMARY KEY,
    jobOrderID INT,
    employeeID NVARCHAR(30),
    materialProcessingExpense DECIMAL(10,2) DEFAULT 1 CHECK (materialProcessingExpense >= 0),
    filmsProcessingExpense DECIMAL(10,2) DEFAULT 1 CHECK (filmsProcessingExpense >= 0),
    materialsTotal DECIMAL(10,2) DEFAULT 1 CHECK (materialsTotal >= 0),
    totalAfterMaterials DECIMAL(10,2) NOT NULL DEFAULT 1 CHECK (totalAfterMaterials >= 0),
    adminstrativeExpense DECIMAL(10,2) DEFAULT 1 CHECK (adminstrativeExpense >= 0),
    totalExpenses DECIMAL(10,2) NOT NULL DEFAULT 1 CHECK (totalExpenses >= 0),
    percentage DECIMAL(10,2) NOT NULL DEFAULT 1 CHECK (percentage >= 0),
    totalAfterPercentage DECIMAL(10,2) NOT NULL DEFAULT 1 CHECK (totalAfterPercentage >= 0),
    ministryOfFinance DECIMAL(10,2) NOT NULL DEFAULT 1,
    employeeImprovmentBox DECIMAL(10,2) NOT NULL DEFAULT 1,
    valueAddedTax DECIMAL(10,2) NOT NULL DEFAULT 1,
    finalTotal DECIMAL(10,2) NOT NULL DEFAULT 1 CHECK (finalTotal >= 0),
    FOREIGN KEY (jobOrderID) REFERENCES JobOrder(jobOrderID),
    FOREIGN KEY (employeeID) REFERENCES Employee(employeeID)
);
GO

-- 14. PurchaseOrder Table
CREATE TABLE PurchaseOrder (
    purchaseID INT IDENTITY(1,1) PRIMARY KEY,
    purchaseDate DATE DEFAULT GETDATE(),
    employeeID NVARCHAR(30) NOT NULL,
    vendorID INT NOT NULL,
    purchaseNotes NVARCHAR(2500) NULL DEFAULT '',
    FOREIGN KEY (employeeID) REFERENCES Employee(employeeID),
    FOREIGN KEY (vendorID) REFERENCES Vendor(vendorID)
);
GO

-- 9. ReturnsOrder Table
CREATE TABLE ReturnsOrder (
    returnID INT IDENTITY(1,1) PRIMARY KEY,
    returnDate DATE DEFAULT GETDATE(),
    jobOrderID INT  NULL,
	purchaseID INT NULL,
    returnInOut BIT DEFAULT 1,
    employeeID NVARCHAR(30) NOT NULL,
    returnsNotes NVARCHAR(2500) NULL DEFAULT '',
    FOREIGN KEY (jobOrderID) REFERENCES JobOrder(jobOrderID),
	FOREIGN KEY (purchaseID) REFERENCES PurchaseOrder(purchaseID),
    FOREIGN KEY (employeeID) REFERENCES Employee(employeeID)
);
GO

-- 10. RequisiteOrder Table
CREATE TABLE RequisiteOrder (
    requisiteID INT IDENTITY(1,1) PRIMARY KEY,
    requisiteDate DATE DEFAULT GETDATE(),
    employeeID NVARCHAR(30) NOT NULL,
    jobOrderID INT NOT NULL,
    requisiteNotes NVARCHAR(2500) NULL DEFAULT '',
    FOREIGN KEY (employeeID) REFERENCES Employee(employeeID),
    FOREIGN KEY (jobOrderID) REFERENCES JobOrder(jobOrderID)
);
GO

-- 11. Paper Table
CREATE TABLE Paper (
    paperID INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(30) NOT NULL DEFAULT '',
    type NVARCHAR(25) NULL DEFAULT '',
    weight DECIMAL(10,2) NULL DEFAULT 1,
    totalBalance DECIMAL(10,2) DEFAULT 1,
    colored NVARCHAR(10) NOT NULL DEFAULT '',
    quantity INT NOT NULL DEFAULT 1,
    price DECIMAL(10,2) NOT NULL DEFAULT 1,
    reorderPoint DECIMAL(10,2) DEFAULT 1,
    CHECK (weight >= 0.0 AND price > 0.0 AND quantity >= 0),
	Activated BIT DEFAULT 1
);
GO

-- 12. Ink Table
CREATE TABLE Ink (
    inkID INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(30) NOT NULL DEFAULT '',
    totalBalance DECIMAL(10,2) DEFAULT 1,
    colored NVARCHAR(20) NOT NULL DEFAULT '',
    price DECIMAL(10,2) NOT NULL DEFAULT 1,
    quantity INT NOT NULL DEFAULT 1,
    reorderPoint DECIMAL(10,2) DEFAULT 1,
    CHECK (price > 0.0 AND quantity >= 0),
	Activated BIT DEFAULT 1
);
GO

-- 13. Supplies Table
CREATE TABLE Supplies (
    suppliesID INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(30) NOT NULL DEFAULT '',
    totalBalance DECIMAL(10,2) DEFAULT 1,
    price DECIMAL(10,2) NOT NULL DEFAULT 1,
    quantity INT NOT NULL DEFAULT 1,
    reorderPoint DECIMAL(10,2) DEFAULT 1,
    CHECK (quantity > 0 AND price > 0.0),
	Activated BIT DEFAULT 1
);
GO

-- 15. PhysicalCountOrder Table
CREATE TABLE PhysicalCountOrder (
    physicalCountID INT IDENTITY(1,1) PRIMARY KEY,
    employeeID NVARCHAR(30),
    physicalCountDate DATE DEFAULT GETDATE(),
    physicalCountNotes NVARCHAR(2500) DEFAULT '',
    FOREIGN KEY (employeeID) REFERENCES Employee(employeeID)
);
GO

-- 16. QuantityBridge Table
CREATE TABLE QuantityBridge (
    QuantityBridgeID INT IDENTITY(1,1) PRIMARY KEY,
    price DECIMAL(10,2) NOT NULL DEFAULT 1 CHECK (price > 0.0),
    returnID INT NULL,
    purchaseID INT NULL,
    quantity INT NOT NULL DEFAULT 1 CHECK (quantity > 0),
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