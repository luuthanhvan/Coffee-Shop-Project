DROP DATABASE QuanLyQuanCafe
CREATE DATABASE QuanLyQuanCafe
GO

USE QuanLyQuanCafe
GO

CREATE TABLE TableFood
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Bàn chưa có tên',
	status NVARCHAR(100) NOT NULL DEFAULT N'Trống'
)
GO

CREATE TABLE Account
(
	UserName NVARCHAR(100) PRIMARY KEY	,
	Display NVARCHAR(100) NOT NULL,
	PassWord NVARCHAR(300) NOT NULL,
	Type INT NOT NULL DEFAULT 0
	
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
	name NVARCHAR(100)DEFAULT N'Chưa đặt tên' NOT NULL,
	idCategory INT NOT NULL,
	price FLOAT DEFAULT 0 NOT NULL
	
	FOREIGN KEY (idCategory) REFERENCES FoodCategory(id)
	
)
GO

CREATE TABLE Bill
(
	id INT IDENTITY PRIMARY KEY,
	DateCheckIn DATE DEFAULT GETDATE(),
	DateCheckOut DATE,
	idTable INT NOT NULL,
	status INT NOT NULL DEFAULT 0,
	discount FLOAT,
	totalPrice FLOAT
	
	FOREIGN KEY(idTable) REFERENCES TableFood(id)
)
GO

CREATE TABLE BillInfo
(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idFood INT NOT NULL,
	count INT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idBill) REFERENCES Bill(id),
	FOREIGN KEY (idFood) REFERENCES Food(id)
	
)
GO

--Tao them
CREATE TABLE SalaryStaff
(
	id INT IDENTITY PRIMARY KEY,
	userName NVARCHAR(100), 
	CheckIn DATETIME ,
	CheckOut DATETIME DEFAULT NULL,
	FOREIGN KEY (userName) REFERENCES Account(userName)
)
GO

-- Them ban
DECLARE @i INT=1
WHILE @i<15
BEGIN
	INSERT TableFood(name) values ('Bàn '+CAST(@i AS nvarchar(100) ))
	SET @i = @i+1
END
GO

--Them category
INSERT FoodCategory(name) VALUES (N'Hải sản')
INSERT FoodCategory(name) VALUES (N'Lâm sản')
INSERT FoodCategory(name) VALUES (N'Nông sản')
INSERT FoodCategory(name) VALUES (N'Nước')
GO

--Them mon an
INSERT Food(name,idCategory,price) VALUES(N'Mực một nắng nướng sa tế',1,50000)
INSERT Food(name,idCategory,price) VALUES(N'Nghêu hấp xả',1,35000)
INSERT Food(name,idCategory,price) VALUES(N'Tôm nướng muối ớt',1,50000)
INSERT Food(name,idCategory,price) VALUES(N'Dú dê nướng ngói',2,40000)
INSERT Food(name,idCategory,price) VALUES(N'Heo rừng nướng muối ớt',2,40000)
INSERT Food(name,idCategory,price) VALUES(N'Xoài lắc',3,15000)
INSERT Food(name,idCategory,price) VALUES(N'7Up',4,12000)
INSERT Food(name,idCategory,price) VALUES(N'Sting',4,12000)
GO

--Them bill
INSERT Bill(DateCheckIn,DateCheckOut,idTable,status) VALUES(GETDATE(),NULL,2,0)
INSERT Bill(DateCheckIn,DateCheckOut,idTable,status) VALUES(GETDATE(),GETDATE(),3,1)
GO

-- Them tai khoan
INSERT Account (UserName, Display, PassWord, Type)
VALUES  (N'ltvan', N'Lưu Thanh Vân', N'310799', 1)
INSERT Account (UserName, Display, PassWord, Type)
VALUES  (N'tvan', N'Thanh Van', N'311999', 0)
INSERT Account (UserName, Display, PassWord, Type)
VALUES  (N'mquan', N'Lê Minh Quân', N'B1509945', 0)
GO

--Tao thu tuc kiem tra dang nhap
CREATE PROC USP_Login
@userName nvarchar(100),@passWord nvarchar(300)
AS
BEGIN
	SELECT *FROM Account WHERE UserName = @userName AND PassWord = @passWord
END
GO

CREATE PROC USP_GetTableList
AS SELECT * FROM TableFood
GO

CREATE PROC USP_InsertBill
@idTable INT
AS
BEGIN
	INSERT dbo.Bill( DateCheckIn , DateCheckOut , idTable , status    )
	VALUES  ( GETDATE() , NULL , @idTable , 0  )
END
GO

CREATE PROC USP_InsertBillInfo
@idBill INT,@idFood INT, @count INT
AS
BEGIN
	DECLARE @isExitsBillInfo INT
	DECLARE @foodCount INT = 1
	
	SELECT @isExitsBillInfo = id, @foodCount = b.count 
	FROM dbo.BillInfo AS b 
	WHERE idBill = @idBill AND idFood = @idFood

	IF (@isExitsBillInfo > 0)
	BEGIN
		DECLARE @newCount INT = @foodCount + @count
		IF (@newCount > 0)
			UPDATE dbo.BillInfo	SET count = @foodCount + @count WHERE idFood = @idFood
		ELSE
			DELETE dbo.BillInfo WHERE idBill = @idBill AND idFood = @idFood
	END
	ELSE
	BEGIN
		INSERT	dbo.BillInfo
        ( idBill, idFood, count )
		VALUES  ( @idBill, -- idBill - int
          @idFood, -- idFood - int
          @count  -- count - int
          )
	END
END
GO

CREATE TRIGGER UTG_UpdateBillInfo
ON BillInfo FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @idBill INT
	
	SELECT @idBill = idBill FROM inserted
	
	DECLARE @idTable INT
	
	SELECT @idTable = idTable FROM  Bill WHERE id = @idBill AND status = 0
	
	UPDATE TableFood SET status = N'Có người' WHERE id = @idTable
	
END
GO

CREATE TRIGGER UTG_UpdateBill
ON dbo.Bill FOR UPDATE
AS
BEGIN
	DECLARE @idBill INT
	
	SELECT @idBill = id FROM Inserted	
	
	DECLARE @idTable INT
	
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill
	
	DECLARE @count int = 0
	
	SELECT @count = COUNT(*) FROM dbo.Bill WHERE idTable = @idTable AND status = 0
	
	IF (@count = 0)
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable
END
GO

CREATE PROC USP_SwitchTable
@idTable1 INT, @idTable2 int
AS BEGIN

	DECLARE @idBill1 int
	DECLARE @idBill2 int
	
	SELECT @idBill1 = id FROM dbo.Bill WHERE dbo.Bill.idTable = @idTable1 AND STATUS = 0
	SELECT @idBill2 = id FROM dbo.Bill WHERE dbo.Bill.idTable = @idTable2 AND STATUS = 0

	UPDATE dbo.Bill SET dbo.Bill.idTable = @idTable2 WHERE id = @idBill1
	UPDATE dbo.Bill SET dbo.Bill.idTable = @idTable1 WHERE id = @idBill2
	
	DECLARE @isFirstTablEmty INT = 0
	DECLARE @isSecondTablEmty INT = 0
	
	
	SELECT @isFirstTablEmty = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idBill1
	SELECT @isSecondTablEmty = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idBill2
	
	IF (@isFirstTablEmty = 0)
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable2
	ELSE 
		UPDATE dbo.TableFood SET status = N'Có người' WHERE id = @idTable2
		
		
	IF (@isSecondTablEmty= 0)
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable1
	ELSE 
		UPDATE dbo.TableFood SET status = N'Có người' WHERE id = @idTable1
END
GO

CREATE PROC USP_getListBillByDate
@checkIn date, @checkOut date
AS
BEGIN
	SELECT t.name,DateCheckIn,DateCheckOut, b.discount,b.totalPrice FROM Bill b JOIN BillInfo bi ON b.id = bi.idBill
	JOIN TableFood t ON t.id = b.idTable
	JOIN Food f ON f.id = bi.idFood
	 WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.status = 1
END
GO

CREATE PROC USP_getNumBillByDate
@checkIn date, @checkOut date
AS
BEGIN
	SELECT COUNT(*) FROM Bill b JOIN BillInfo bi ON b.id = bi.idBill
	JOIN TableFood t ON t.id = b.idTable
	JOIN Food f ON f.id = bi.idFood
	 WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.status = 1
END
GO

CREATE PROC USP_getListBillByDatePage
@checkIn date, @checkOut date, @page int
AS
BEGIN
	DECLARE @pageRows INT = 10
	DECLARE @selectRows INT = @pageRows * @page
	DECLARE @exceptRows INT = (@page -1) * @pageRows
	


	SELECT TOP (@selectRows)  t.name,DateCheckIn,DateCheckOut, b.discount,b.totalPrice FROM Bill b JOIN BillInfo bi ON b.id = bi.idBill
	JOIN TableFood t ON t.id = b.idTable
	JOIN Food f ON f.id = bi.idFood
	 WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.status = 1 AND b.id NOT IN (SELECT TOP (@exceptRows) id FROM Bill  )
END
GO

CREATE PROC USP_UpdateAccount
@userName NVARCHAR(100), @displayName NVARCHAR(100),@password NVARCHAR(100), @newPassword NVARCHAR(100)
AS
BEGIN
	DECLARE @isRighPass INT = 0
	SELECT	@isRighPass = COUNT(*) FROM Account WHERE UserName = @userName AND password = @password 
	IF(@isRighPass =1)
	BEGIN
		IF(@newPassword = NULL OR @newPassword = '')
		BEGIN
			UPDATE Account SET Display = @displayName WHERE UserName = @userName
		END
		ELSE
		UPDATE Account SET Display = @displayName, PassWord = @newPassword WHERE UserName = @userName
	END
END
GO

CREATE TRIGGER UTG_DeleteBillInfo
ON BillInfo FOR DELETE
AS
BEGIN
	DECLARE @idBillInfo INT
	DECLARE @idBill INT
	SELECT @idBillInfo = id, @idBill = deleted.idBill FROM deleted
	
	DECLARE @idTable INT
	SELECT @idTable = idTable FROM Bill WHERE id = @idBill
	
	DECLARE @count INT = 0
	SELECT @count = COUNT(*) FROM BillInfo AS bi, Bill AS b 
	WHERE b.id = bi.idBill AND b.id = @idBill AND b.status = 0
	
	IF(@count = 0)
		UPDATE TableFood SET status = N'Trống' WHERE id = @idTable
END
GO

CREATE PROC USP_InsertSalaryStaffCheckIn
@userName NVARCHAR(100)
AS
BEGIN	
	DECLARE @id INT = -1
	SELECT @id = MAX(id) FROM SalaryStaff WHERE userName =  @userName AND CheckOut is NULL
	IF @id = -1 
		BEGIN			
			INSERT INTO SalaryStaff(userName,CheckIn) VALUES (@userName, GETDATE())
		END
END
GO

CREATE PROC USP_InsertSalaryStaffCheckOut
@userName NVARCHAR(100)
AS
BEGIN
	DECLARE @id INT
	SELECT @id = MAX(id) FROM SalaryStaff WHERE userName = @userName AND CheckOut is NULL
	UPDATE SalaryStaff SET Checkout = GETDATE() WHERE Id = @id
	SELECT * FROM SalaryStaff

END
GO

CREATE PROC USP_getListSalaryByDate
@userName NVARCHAR(100),@checkIn datetime, @checkOut datetime
AS
BEGIN
	DECLARE @Salary Float = 15000.00
	SELECT CheckIn, CheckOut,CAST( DATEDIFF(HOUR,CheckIn , Checkout) AS int) AS Hour,
	(CAST( DATEDIFF(HOUR,CheckIn , Checkout) AS int)* @Salary) AS Money FROM SalaryStaff
	 WHERE CheckIn >= @checkIn AND CheckOut <= @checkOut AND userName = @userName
END
GO