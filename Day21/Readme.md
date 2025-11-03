step 1 : required tools

Frontend

Visual Studio Code

Node.js

Angular CLI

Backend

Visual Studio

.NET 6 

Database

SQL Server

SQL Server Management Studio (SSMS)

step 2 : start Angular

Install Node.js
Download and install from
https://nodejs.org

Install Angular CLI

npm install -g @angular/cli

Open project directory in terminal

cd frontend/my-angular-app

Install dependencies

npm install

Run Angular

ng serve --open

step 3 : sql server setup

Install SQL Server

Install SQL Server Management Studio (SSMS)

Connect to your SQL Server instance

Create database

CREATE DATABASE OnlineStoreDB

Use the database

USE OnlineStoreDB

Create tables

Users

CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Phoneno NVARCHAR(20),
    Password NVARCHAR(100) NOT NULL,
    AddressLine NVARCHAR(200),
    BuildingName NVARCHAR(200),
    Street NVARCHAR(200),
    PostalCode NVARCHAR(20)
)

Products

CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(150) NOT NULL,
    Price DECIMAL(10,2) NOT NULL
)

Orders

CREATE TABLE Orders (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    Id INT FOREIGN KEY REFERENCES Users(Id),
    OrderCreated DATETIME NOT NULL,
    Status NVARCHAR(50),
    Subtotal DECIMAL(10,2),
    DeliveryFee DECIMAL(10,2),
    TotalAmount DECIMAL(10,2),
    Note NVARCHAR(200),
    BagOption NVARCHAR(50),
    Type NVARCHAR(50)
)

OrderItems

CREATE TABLE OrderItems (
    OrderItemId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT FOREIGN KEY REFERENCES Orders(OrderId),
    ProductId INT FOREIGN KEY REFERENCES Products(ProductId),
    Qty INT NOT NULL,
    Price DECIMAL(10,2),
    TotalPrice AS (Qty * Price)
)

step 4 : backend setup

Install Visual Studio
Select workload: ASP.NET and Web Development

Open backend project folder

backend/BackendAPI


Update connection string in appsettings.json

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=OnlineStoreDB;Trusted_Connection=True;TrustServerCertificate=True;"
}


Install NuGet package

Microsoft.Data.SqlClient

Enable CORS in Program.cs

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});


Run project from Visual Studio
Swagger UI will open for API testing


step 5 : main features

User Registration

User Login

View Products

Place Order

Add Product to Order

View Order History

Cancel Order

Store user details and delivery address

step 6 : api endpoints

User
POST /api/User/register
POST /api/User/login
GET /api/User/{id}

Products
GET /api/Product
GET /api/Product/{id}
POST /api/Product
PUT /api/Product/{id}
DELETE /api/Product/{id}

Orders
GET /api/Order
GET /api/Order/{id}
POST /api/Order
POST /api/Order/add-item
DELETE /api/Order/{orderId}
GET /api/Order/all-items/{userId}
GET /api/Order/details/{userId}
GET /api/Order/latest/{userId}

step 7 : folder structure

Frontend
src/app/components

login

register

home

liveorders

orderhistory

offers

products

Backend
Controllers

UserController.cs

ProductController.cs

OrderController.cs

OrderItemController.cs

Repository

UserRepository.cs

ProductRepository.cs

OrderRepository.cs

OrderItemRepository.cs

Models

User.cs

Product.cs

Order.cs

OrderItem.cs

step 8 : how to run

Backend

Open project in Visual Studio

Set correct SQL connection in appsettings.json

Build and run

Swagger will open

Frontend

Navigate to project folder

Run Angular

ng serve --open


End of document.
