-- APEX WMS Database Schema Creation Script
-- Run this in SQL Server Management Studio

-- Create Tables

-- 1. Suppliers Table
CREATE TABLE [Suppliers] (
    [SupplierId] INT PRIMARY KEY IDENTITY(1,1),
    [SupplierName] NVARCHAR(100) NOT NULL,
    [ContactPerson] NVARCHAR(100),
    [Email] NVARCHAR(100),
    [Phone] NVARCHAR(20),
    [Address] NVARCHAR(255),
    [City] NVARCHAR(50),
    [PostalCode] NVARCHAR(20),
    [Country] NVARCHAR(50),
    [CreatedDate] DATETIME2 NOT NULL,
    [ModifiedDate] DATETIME2 NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1
);

-- 2. Products Table
CREATE TABLE [Products] (
    [ProductId] INT PRIMARY KEY IDENTITY(1,1),
    [ProductName] NVARCHAR(150) NOT NULL,
    [SKU] NVARCHAR(50) NOT NULL UNIQUE,
    [Description] NVARCHAR(500),
    [UnitPrice] DECIMAL(10, 2) NOT NULL,
    [SupplierId] INT NOT NULL,
    [ReorderLevel] INT,
    [ReorderQuantity] INT,
    [CreatedDate] DATETIME2 NOT NULL,
    [ModifiedDate] DATETIME2 NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers]([SupplierId])
);

-- 3. Inventories Table
CREATE TABLE [Inventories] (
    [InventoryId] INT PRIMARY KEY IDENTITY(1,1),
    [ProductId] INT NOT NULL,
    [Location] NVARCHAR(100) NOT NULL,
    [QuantityOnHand] INT NOT NULL DEFAULT 0,
    [QuantityReserved] INT NOT NULL DEFAULT 0,
    [LastStockCheck] DATETIME2,
    [CreatedDate] DATETIME2 NOT NULL,
    [ModifiedDate] DATETIME2 NOT NULL,
    UNIQUE([ProductId], [Location]),
    FOREIGN KEY ([ProductId]) REFERENCES [Products]([ProductId])
);

-- 4. Orders Table
CREATE TABLE [Orders] (
    [OrderId] INT PRIMARY KEY IDENTITY(1,1),
    [OrderNumber] NVARCHAR(50) NOT NULL UNIQUE,
    [SupplierId] INT,
    [OrderDate] DATETIME2 NOT NULL,
    [DeliveryDate] DATETIME2,
    [Status] INT NOT NULL DEFAULT 0,
    [TotalAmount] DECIMAL(12, 2),
    [Notes] NVARCHAR(500),
    [CreatedDate] DATETIME2 NOT NULL,
    [ModifiedDate] DATETIME2 NOT NULL,
    FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers]([SupplierId])
);

-- 5. OrderDetails Table
CREATE TABLE [OrderDetails] (
    [OrderDetailId] INT PRIMARY KEY IDENTITY(1,1),
    [OrderId] INT NOT NULL,
    [ProductId] INT NOT NULL,
    [Quantity] INT NOT NULL,
    [UnitPrice] DECIMAL(10, 2) NOT NULL,
    [LineTotal] DECIMAL(12, 2) NOT NULL,
    FOREIGN KEY ([OrderId]) REFERENCES [Orders]([OrderId]),
    FOREIGN KEY ([ProductId]) REFERENCES [Products]([ProductId])
);

-- 6. StockMovements Table
CREATE TABLE [StockMovements] (
    [StockMovementId] INT PRIMARY KEY IDENTITY(1,1),
    [ProductId] INT NOT NULL,
    [MovementType] INT NOT NULL,
    [Quantity] INT NOT NULL,
    [Location] NVARCHAR(100),
    [Reference] NVARCHAR(100),
    [Notes] NVARCHAR(500),
    [CreatedDate] DATETIME2 NOT NULL,
    FOREIGN KEY ([ProductId]) REFERENCES [Products]([ProductId])
);

-- 7. AspNetRoles Table
CREATE TABLE [AspNetRoles] (
    [Id] NVARCHAR(450) PRIMARY KEY,
    [Name] NVARCHAR(256),
    [NormalizedName] NVARCHAR(256),
    [ConcurrencyStamp] NVARCHAR(MAX)
);

-- 8. AspNetUsers Table
CREATE TABLE [AspNetUsers] (
    [Id] NVARCHAR(450) PRIMARY KEY,
    [UserName] NVARCHAR(256),
    [NormalizedUserName] NVARCHAR(256),
    [Email] NVARCHAR(256),
    [NormalizedEmail] NVARCHAR(256),
    [EmailConfirmed] BIT NOT NULL DEFAULT 0,
    [PasswordHash] NVARCHAR(MAX),
    [SecurityStamp] NVARCHAR(MAX),
    [ConcurrencyStamp] NVARCHAR(MAX),
    [PhoneNumber] NVARCHAR(MAX),
    [PhoneNumberConfirmed] BIT NOT NULL DEFAULT 0,
    [TwoFactorEnabled] BIT NOT NULL DEFAULT 0,
    [LockoutEnd] DATETIMEOFFSET,
    [LockoutEnabled] BIT NOT NULL DEFAULT 1,
    [AccessFailedCount] INT NOT NULL DEFAULT 0
);

-- 9. AspNetUserRoles Table
CREATE TABLE [AspNetUserRoles] (
    [UserId] NVARCHAR(450) NOT NULL,
    [RoleId] NVARCHAR(450) NOT NULL,
    PRIMARY KEY ([UserId], [RoleId]),
    FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id]),
    FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles]([Id])
);

-- 10. AspNetUserClaims Table
CREATE TABLE [AspNetUserClaims] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [UserId] NVARCHAR(450) NOT NULL,
    [ClaimType] NVARCHAR(MAX),
    [ClaimValue] NVARCHAR(MAX),
    FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id])
);

-- 11. AspNetRoleClaims Table
CREATE TABLE [AspNetRoleClaims] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [RoleId] NVARCHAR(450) NOT NULL,
    [ClaimType] NVARCHAR(MAX),
    [ClaimValue] NVARCHAR(MAX),
    FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles]([Id])
);

-- 12. AspNetUserLogins Table
CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] NVARCHAR(450) NOT NULL,
    [ProviderKey] NVARCHAR(450) NOT NULL,
    [ProviderDisplayName] NVARCHAR(MAX),
    [UserId] NVARCHAR(450) NOT NULL,
    PRIMARY KEY ([LoginProvider], [ProviderKey]),
    FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id])
);

-- 13. AspNetUserTokens Table
CREATE TABLE [AspNetUserTokens] (
    [UserId] NVARCHAR(450) NOT NULL,
    [LoginProvider] NVARCHAR(450) NOT NULL,
    [Name] NVARCHAR(450) NOT NULL,
    [Value] NVARCHAR(MAX),
    PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id])
);

-- Create Indexes
CREATE INDEX [IX_Products_SupplierId] ON [Products]([SupplierId]);
CREATE INDEX [IX_Inventories_ProductId] ON [Inventories]([ProductId]);
CREATE INDEX [IX_Orders_SupplierId] ON [Orders]([SupplierId]);
CREATE INDEX [IX_OrderDetails_OrderId] ON [OrderDetails]([OrderId]);
CREATE INDEX [IX_OrderDetails_ProductId] ON [OrderDetails]([ProductId]);
CREATE INDEX [IX_StockMovements_ProductId] ON [StockMovements]([ProductId]);
CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles]([RoleId]);
CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims]([UserId]);
CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims]([RoleId]);
CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins]([UserId]);

-- Insert Default Roles
INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
VALUES 
    (NEWID(), 'Admin', 'ADMIN', NEWID()),
    (NEWID(), 'Manager', 'MANAGER', NEWID()),
    (NEWID(), 'Operator', 'OPERATOR', NEWID());

-- Insert Sample Suppliers
INSERT INTO [Suppliers] ([SupplierName], [ContactPerson], [Email], [Phone], [Address], [City], [PostalCode], [Country], [CreatedDate], [ModifiedDate], [IsActive])
VALUES 
    ('TechSupply Inc.', 'John Doe', 'john@techsupply.com', '+1-555-0100', '123 Tech Street', 'San Francisco', '94102', 'USA', GETDATE(), GETDATE(), 1),
    ('Global Parts Ltd.', 'Jane Smith', 'jane@globalparts.com', '+1-555-0101', '456 Parts Avenue', 'New York', '10001', 'USA', GETDATE(), GETDATE(), 1);

-- Insert Sample Products
INSERT INTO [Products] ([ProductName], [SKU], [Description], [UnitPrice], [SupplierId], [ReorderLevel], [ReorderQuantity], [CreatedDate], [ModifiedDate], [IsActive])
VALUES 
    ('Industrial Motor', 'MTR-001', 'High-efficiency 3-phase industrial motor', 450.00, 1, 10, 25, GETDATE(), GETDATE(), 1),
    ('Control Panel', 'CPL-001', 'Programmable control panel with touchscreen', 750.00, 2, 5, 15, GETDATE(), GETDATE(), 1),
    ('Power Supply Unit', 'PSU-001', 'Regulated 24V DC power supply', 120.00, 1, 20, 50, GETDATE(), GETDATE(), 1);

-- Insert Sample Inventory
INSERT INTO [Inventories] ([ProductId], [Location], [QuantityOnHand], [QuantityReserved], [LastStockCheck], [CreatedDate], [ModifiedDate])
VALUES 
    (1, 'Warehouse A', 50, 10, GETDATE(), GETDATE(), GETDATE()),
    (2, 'Warehouse B', 25, 5, GETDATE(), GETDATE(), GETDATE()),
    (3, 'Warehouse A', 100, 20, GETDATE(), GETDATE(), GETDATE());

-- Print confirmation
PRINT 'Database schema created successfully!';
PRINT 'All tables have been created with sample data.';
