CREATE DATABASE CoffeeShop
GO

USE CoffeeShop
GO

-- Food
-- TableFood
-- FoodCategory
-- Account
-- Bill
-- BillDetails

CREATE TABLE TableFood(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Bàn chưa có tên',
	status NVARCHAR(100) NOT NULL DEFAULT N'Trống'	-- Trống || Có người
)
GO

CREATE TABLE Account
(
	UserName NVARCHAR(100) PRIMARY KEY,	
	DisplayName NVARCHAR(100) NOT NULL DEFAULT N'ltvan',
	PassWord NVARCHAR(1000) NOT NULL DEFAULT 0,
	Type INT NOT NULL  DEFAULT 0 -- 1: admin && 0: staff
)
GO

CREATE TABLE FoodCategory
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
)
GO

CREATE TABLE Food
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	idCategory INT NOT NULL,
	price FLOAT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idCategory) REFERENCES dbo.FoodCategory(id)
)
GO

CREATE TABLE Bill
(
	id INT IDENTITY PRIMARY KEY,
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	DateCheckOut DATE,
	idTable INT NOT NULL,
	status INT NOT NULL DEFAULT 0 -- 1: đã thanh toán && 0: chưa thanh toán
	
	FOREIGN KEY (idTable) REFERENCES dbo.TableFood(id)
)
GO

CREATE TABLE BillDetails
(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idFood INT NOT NULL,
	count INT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idBill) REFERENCES dbo.Bill(id),
	FOREIGN KEY (idFood) REFERENCES dbo.Food(id)
)
GO

INSERT INTO dbo.Account (UserName, DisplayName, PassWord, Type)
VALUES  (N'ltvan', N'Luu Thanh Van', N'310799', 1)
INSERT INTO dbo.Account (UserName, DisplayName, PassWord, Type)
VALUES  (N'tvan', N'Thanh Van', N'311999', 0)
GO

CREATE PROC USP_GetAccountByUserName
@userName nvarchar(100)
AS 
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName
END
GO

-- test USP_GetAccountByUserName Procedure
EXEC dbo.USP_GetAccountByUserName @userName = N'ltvan'
GO

CREATE PROC USP_Login
@username nvarchar(100), @password nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @username AND PassWord = @password
END
GO

-- test USP_Login Procedure
EXEC dbo.USP_Login @userName = N'ltvan', @password = N'310799'
GO

-- add table food info
DECLARE @i INT = 1
WHILE @i <= 10
BEGIN
	INSERT dbo.TableFood (name) VALUES (N'Bàn ' + CAST(@i AS nvarchar(100)))
	SET @i = @i + 1
END
GO

CREATE PROC USP_GetTableList
AS SELECT * FROM dbo.TableFood
GO

UPDATE dbo.TableFood SET status = N'Có người' WHERE id = 9
GO

EXEC dbo.USP_GetTableList
GO

-- add category
INSERT dbo.FoodCategory (name) VALUES (N'Hải sản')
INSERT dbo.FoodCategory (name) VALUES  (N'Nông sản')
INSERT dbo.FoodCategory (name) VALUES  (N'Lâm sản')
INSERT dbo.FoodCategory (name) VALUES  (N'Nước')
GO

-- add food
INSERT dbo.Food (name, idCategory, price) VALUES  ( N'Mực một nắng nướng sa tế', 1, 120000)
INSERT dbo.Food (name, idCategory, price) VALUES  ( N'Hào nướng', 1, 150000)
INSERT dbo.Food (name, idCategory, price) VALUES  ( N'Lụa xào', 1, 50000)
INSERT dbo.Food (name, idCategory, price) VALUES  (N'Nghêu hấp xả', 1, 50000)
INSERT dbo.Food (name, idCategory, price) VALUES ( N'Thịt heo nướng sữa', 2, 60000)
INSERT dbo.Food (name, idCategory, price) VALUES ( N'Heo rừng nướng muối ớt', 3, 75000)
INSERT dbo.Food (name, idCategory, price) VALUES (N'7Up', 4, 15000)
INSERT dbo.Food (name, idCategory, price) VALUES (N'Cafe', 4, 12000)
INSERT dbo.Food (name, idCategory, price) VALUES (N'Cafe sữa', 4, 15000)
INSERT dbo.Food (name, idCategory, price) VALUES (N'Đá chanh', 4, 6000)
GO

-- add bill
INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status)
VALUES  (GETDATE(), NULL, 3, 0)

INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status)
VALUES  (GETDATE(), NULL, 4, 0)

INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status)
VALUES  (GETDATE(), GETDATE(), 4, 1)

INSERT dbo.Bill (DateCheckIn, DateCheckOut, idTable, status)
VALUES  (GETDATE(), GETDATE(), 5, 1)
GO

-- add bill details
INSERT	dbo.BillDetails (idBill, idFood, count) VALUES (1, 1, 2)
INSERT	dbo.BillDetails (idBill, idFood, count) VALUES (1, 3, 4)
INSERT	dbo.BillDetails (idBill, idFood, count) VALUES (1, 5, 1)
INSERT	dbo.BillDetails (idBill, idFood, count) VALUES (2, 1, 2)
INSERT	dbo.BillDetails (idBill, idFood, count) VALUES (2, 6, 2)
INSERT	dbo.BillDetails (idBill, idFood, count) VALUES (3, 5, 2)     
GO

SELECT * FROM dbo.Food
SELECT * FROM dbo.FoodCategory
SELECT * FROM dbo.Bill
SELECT * FROM dbo.BillDetails