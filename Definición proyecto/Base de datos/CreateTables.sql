use elgnomo;
-- Create Product table
CREATE TABLE Product (
Id INT AUTO_INCREMENT PRIMARY KEY,
Name VARCHAR(100),
Price DECIMAL(10,2),
Category VARCHAR(50),
Brand VARCHAR(50),
Cost DECIMAL(10,2),
Image LONGTEXT,
Discount DECIMAL(5,2),
QuantityInStock INT
);

-- Create Role table
CREATE TABLE Role (
Id INT AUTO_INCREMENT PRIMARY KEY,
Name VARCHAR(50),
Description VARCHAR(100)
);

-- Create User table
CREATE TABLE User (
Id INT AUTO_INCREMENT PRIMARY KEY,
FirstName VARCHAR(50),
LastName VARCHAR(50),
Email VARCHAR(100),
PasswordHash VARCHAR(50)
);

-- Create Role_User table
CREATE TABLE Role_User (
Id INT AUTO_INCREMENT PRIMARY KEY,
RoleId INT,
UserId INT,
FOREIGN KEY (RoleId) REFERENCES Role(Id),
FOREIGN KEY (UserId) REFERENCES User(Id)
);