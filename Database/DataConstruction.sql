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
    remainingAmount DECIMAL(10,2) DEFAULT 0 CHECK (remainingAmount >= 0),
    unearnedRevenue DECIMAL(10,2) DEFAULT 0 CHECK (unearnedRevenue >= 0),
    jobOrdernotes NVARCHAR(100) DEFAULT '',
    earnedRevenue DECIMAL(10,2) DEFAULT 0 CHECK (earnedRevenue >= 0),
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

    machineHourPrice DECIMAL (10,2) DEFAULT 1,
    totalMachineValue DECIMAL(10,2) DEFAULT 0 NOT NULL CHECK (totalMachineValue >= 0),
    oldMachinePrice DECIMAL (10,2)DEFAULT 1 ,

    labourHourPrice DECIMAL (10,2)DEFAULT 1,
    oldlabourPrice DECIMAL (10,2)DEFAULT 1,
    totalLabourValue DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (totalLabourValue >= 0),

    numberOfHours DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (numberOfHours >= 0),
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
    materialProcessingExpense DECIMAL(10,2) DEFAULT 0 CHECK (materialProcessingExpense >= 0),
    filmsProcessingExpense DECIMAL(10,2) DEFAULT 0 CHECK (filmsProcessingExpense >= 0),
    materialsTotal DECIMAL(10,2) DEFAULT 0 CHECK (materialsTotal >= 0),
    totalAfterMaterials DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (totalAfterMaterials >= 0),
    adminstrativeExpense DECIMAL(10,2) DEFAULT 0 CHECK (adminstrativeExpense >= 0),
    totalExpenses DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (totalExpenses >= 0),
    percentage DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (percentage >= 0),
    totalAfterEmplyeeImprovementbox DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (totalAfterEmplyeeImprovementbox >= 0),
    totalAfterPercentage DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (totalAfterPercentage >= 0),
    ministryOfFinance DECIMAL(10,2) NOT NULL DEFAULT 0,
    employeeImprovmentBox DECIMAL(10,2) NOT NULL DEFAULT 0,
    valueAddedTax DECIMAL(10,2) NOT NULL DEFAULT 0,
    finalTotal DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (finalTotal >= 0),
    FOREIGN KEY (jobOrderID) REFERENCES JobOrder(jobOrderID),
    FOREIGN KEY (employeeID) REFERENCES Employee(employeeID)
);
GO

-- 14. PurchaseOrder Table
CREATE TABLE PurchaseOrder (
    purchaseID INT IDENTITY(1,1) PRIMARY KEY,
    purchaseDate DATE DEFAULT GETDATE(),
    employeeID NVARCHAR(30) NOT NULL,
    remainingAmount DECIMAL (10,2) DEFAULT 0.0 CHECK (remainingAmount >= 0),
    paidAmount DECIMAL (10,2) DEFAULT 0.0 CHECK (paidAmount >= 0),
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
    weight DECIMAL(10,2) NULL DEFAULT 0,
    totalBalance DECIMAL(10,2) DEFAULT 0,
    colored NVARCHAR(10) NOT NULL DEFAULT '',
    quantity INT NOT NULL DEFAULT 0,
    price DECIMAL(10,2) NOT NULL DEFAULT 0,
    reorderPoint DECIMAL(10,2) DEFAULT 0,
    CHECK (weight >= 0.0 AND price >= 0.0 AND quantity >= 0),
	Activated BIT DEFAULT 1
);
GO

-- 12. Ink Table
CREATE TABLE Ink (
    inkID INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(30) NOT NULL DEFAULT '',
    totalBalance DECIMAL(10,2) DEFAULT 0,
    colored NVARCHAR(20) NOT NULL DEFAULT '',
    price DECIMAL(10,2) NOT NULL DEFAULT 0,
    quantity INT NOT NULL DEFAULT 0,
    reorderPoint DECIMAL(10,2) DEFAULT 0,
    CHECK (price >= 0.0 AND quantity >= 0),
	Activated BIT DEFAULT 1
);
GO

-- 13. Supplies Table
CREATE TABLE Supplies (
    suppliesID INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(30) NOT NULL DEFAULT '',
    totalBalance DECIMAL(10,2) DEFAULT 0,
    price DECIMAL(10,2) NOT NULL DEFAULT 0,
    quantity INT NOT NULL DEFAULT 0,
    reorderPoint DECIMAL(10,2) DEFAULT 0,
    CHECK (quantity >= 0 AND price > 0.0),
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
    price DECIMAL(10,2) NULL ,
    returnID INT NULL,
    purchaseID INT NULL,
    quantity INT NOT NULL DEFAULT 0 CHECK (quantity >= 0),
    totalBalance DECIMAL(10,2) NULL DEFAULT 0,
    oldQuantity INT NULL DEFAULT 1 ,
    oldPrice DECIMAL(10,2) NULL ,
    oldTotalBalance DECIMAL(10,2) NULL DEFAULT 0,
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

-- 17. PaymentsOrder Table 

CREATE TABLE PaymentOrder(
    paymentID INT IDENTITY(1,1) PRIMARY KEY,
    purchaseID INT ,
    amount DECIMAL(10,2) DEFAULT 0 CHECK (amount >= 0),
    paymentDate  DATE DEFAULT GETDATE(),
    employeeID NVARCHAR(30) ,
	paymentNotes NVARCHAR(2500) DEFAULT '',
    FOREIGN KEY (purchaseID) REFERENCES PurchaseOrder(purchaseID),
    FOREIGN KEY (employeeID) REFERENCES Employee(employeeID)
  
);
GO


-- 18. RecieptsOrder Table 

CREATE TABLE RecieptsOrder(
    recieptID INT IDENTITY(1,1) PRIMARY KEY,
    jobOrderID INT ,
    amount DECIMAL(10,2) DEFAULT 0 CHECK (amount >= 0),
    receiptDate  DATE DEFAULT GETDATE(),
    employeeID NVARCHAR(30) ,
	receiptNotes NVARCHAR(2500) DEFAULT '',
    FOREIGN KEY (jobOrderID) REFERENCES JobOrder(jobOrderID),
    FOREIGN KEY (employeeID) REFERENCES Employee(employeeID)
  
);
GO
