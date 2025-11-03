USE OnlineStoreDB;
GO

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Phoneno NVARCHAR(15) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    AddressLine NVARCHAR(255) NULL,
    BuildingName NVARCHAR(255) NULL,
    Street NVARCHAR(255) NULL,
    PostalCode NVARCHAR(20) NULL
);

CREATE TABLE Orders (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    Id INT NOT NULL,                         
    OrderCreated DATETIME NOT NULL DEFAULT GETDATE(),
    Status NVARCHAR(50) NOT NULL,
    Subtotal DECIMAL(10,2) NOT NULL,
    DeliveryFee DECIMAL(10,2) NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    Note NVARCHAR(255) NULL,
    BagOption NVARCHAR(50) NULL,
    Type NVARCHAR(50) NULL,

    CONSTRAINT FK_Orders_Users FOREIGN KEY(Id)
        REFERENCES Users(Id)
);

CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(150) NOT NULL,
    Price DECIMAL(10,2) NOT NULL
);

CREATE TABLE OrderItems (
    OrderItemId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,                     
    ProductId INT NOT NULL,                   
    Qty INT NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    TotalPrice DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId)
        REFERENCES Orders(OrderId),

    CONSTRAINT FK_OrderItems_Products FOREIGN KEY (ProductId)
        REFERENCES Products(ProductId)
);