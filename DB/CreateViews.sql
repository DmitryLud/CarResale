USE CarResale
GO 

CREATE OR ALTER VIEW ViewCars
AS
	SELECT * FROM [Cars]
GO

CREATE OR ALTER VIEW ViewCustomers
AS
	SELECT * FROM [Customers]
GO

CREATE OR ALTER VIEW ViewMarks
AS
	SELECT * FROM [Marks]
GO

CREATE OR ALTER VIEW ViewModels
AS
	SELECT * FROM [Models]
GO

CREATE OR ALTER VIEW ViewOrders 
AS
	SELECT * FROM [Orders]
GO

CREATE OR ALTER VIEW ViewReceiptInvoice
AS
	SELECT * FROM [ReceiptInvoice]
GO

CREATE OR ALTER VIEW CarColors 
AS
	SELECT DISTINCT [Color] FROM [Cars]
GO

CREATE OR ALTER VIEW CarTrims 
AS
	SELECT DISTINCT [TRIM] FROM [Cars]
GO

CREATE OR ALTER VIEW MonthlyReport 
AS
	SELECT C.VIN, MA.Mark, M.Model, CUS.[Name], CUS.[Surname], RI.[Date of acquisition], RI.[Acquisistion price], RI.[Total acquisistion price],
		O.[Sale price], (O.[Sale price] - RI.[Total acquisistion price]) AS 'Financial result'
	FROM [Orders] O
	JOIN [Cars] C ON O.CarID = C.ID
	JOIN [Models] M ON M.ID = C.ModelID
	JOIN [Marks] MA ON MA.ID = M.MarkID
	JOIN [ReceiptInvoice] RI ON RI.ID = C.[ReceiptInvoiceID]
	JOIN [Customers] CUS ON O.CustomerID = CUS.ID
	WHERE O.[Sale date] BETWEEN CONVERT(DATE, GETDATE()) AND DATEADD(DAY, -30, CONVERT(DATE, GETDATE()))
GO

CREATE OR ALTER VIEW DailyReport 
AS
	SELECT C.VIN, MA.Mark, M.Model, CUS.[Name], CUS.[Surname], RI.[Acquisistion price], RI.[Total acquisistion price],
		O.[Sale price], (O.[Sale price] - RI.[Total acquisistion price]) AS 'Financial result'
	FROM [Orders] O
	JOIN [Cars] C ON O.CarID = C.ID
	JOIN [Models] M ON M.ID = C.ModelID
	JOIN [Marks] MA ON MA.ID = M.MarkID
	JOIN [ReceiptInvoice] RI ON RI.ID = C.[ReceiptInvoiceID]
	JOIN [Customers] CUS ON O.CustomerID = CUS.ID
	WHERE O.[Sale date] = CONVERT(DATE, GETDATE())
GO