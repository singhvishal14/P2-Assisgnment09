-- Create Datitabase OrdersDB
create database OrderDB
use OrderDB

-- Create Customers Table
create table Customers(
CustomerId int primary key,
FirstName nvarchar(50),
LastName nvarchar(50)
)

INSERT INTO Customers (CustomerId, FirstName, LastName)
VALUES
    (1, 'Arun', 'Saxena'),
    (2, 'Arpit', 'Sharma'),
    (3, 'Riya', 'Goyal');

-- Create Orders Table
create table Orders (
    OrderId INT PRIMARY KEY,
    CustomerId INT FOREIGN KEY REFERENCES Customers(CustomerId),
    OrderDate DATETIME,
    TotalAmount DECIMAL(18, 2)
)
INSERT INTO Orders (OrderId, CustomerId, OrderDate, TotalAmount)
VALUES
    (1, 1, '2023-08-23', 100.00),
    (2, 2, '2023-08-24', 150.00),
    (3, 1, '2023-08-25', 75.50)

-- Create PlaceOrder Stored Procedure
USE OrderDB -- Assuming you are using the "OrderDB" database

-- Create PlaceOrder Stored Procedure
USE OrderDB; -- Assuming you are using the "OrderDB" database

CREATE PROCEDURE PlaceOrder
    @CustomerId INT,
    @TotalAmount DECIMAL(18, 2)
AS
BEGIN
    -- Insert into Orders Table
    INSERT INTO Orders (CustomerId, OrderDate, TotalAmount)
    VALUES (@CustomerId, GETDATE(), @TotalAmount);
END

-- Call the PlaceOrder Stored Procedure
EXEC PlaceOrder @CustomerId = 1, @TotalAmount = 100.00
SELECT * FROM Orders
SELECT * FROM Customers