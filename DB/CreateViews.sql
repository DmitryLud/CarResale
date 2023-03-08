USE CarResale
GO 

CREATE OR ALTER VIEW ViewCar
AS
	SELECT * FROM [Car]
GO

CREATE OR ALTER VIEW ViewCustomer
AS
	SELECT * FROM [Customer]
GO

CREATE OR ALTER VIEW ViewMark
AS
	SELECT * FROM [Mark]
GO

CREATE OR ALTER VIEW ViewModel
AS
	SELECT * FROM [Model]
GO

CREATE OR ALTER VIEW ViewOrder 
AS
	SELECT * FROM [Order]
GO

CREATE OR ALTER VIEW ViewReceiptInvoice
AS
	SELECT * FROM [ReceiptInvoice]
GO

CREATE OR ALTER VIEW MonthlyReport 
AS
	SELECT C.VIN, MA.[Name] AS 'Mark', M.[Name] AS 'Model', CUS.[Name], CUS.[Surname], RI.[Date of acquisition], RI.[Acquisistion price], RI.[Total acquisistion price],
		O.[Sale price], (O.[Sale price] - RI.[Total acquisistion price]) AS 'FinancialResult'
	FROM [Order] O
	JOIN [Car] C ON O.CarID = C.ID
	JOIN [Model] M ON M.ID = C.ModelID
	JOIN [Mark] MA ON MA.ID = M.MarkID
	JOIN [ReceiptInvoice] RI ON RI.ID = C.[ReceiptInvoiceID]
	JOIN [Customer] CUS ON O.CustomerID = CUS.ID
	WHERE O.[Sale date] BETWEEN CONVERT(DATE, GETDATE()) AND DATEADD(DAY, -30, CONVERT(DATE, GETDATE()))
GO

CREATE OR ALTER VIEW DailyReport 
AS
	SELECT C.VIN, MA.[Name] AS 'Mark', M.[Name] AS 'Model', CUS.[Name], CUS.[Surname], RI.[Acquisistion price], RI.[Total acquisistion price],
		O.[Sale price], (O.[Sale price] - RI.[Total acquisistion price]) AS 'FinancialResult'
	FROM [Order] O
	JOIN [Car] C ON O.CarID = C.ID
	JOIN [Model] M ON M.ID = C.ModelID
	JOIN [Mark] MA ON MA.ID = M.MarkID
	JOIN [ReceiptInvoice] RI ON RI.ID = C.[ReceiptInvoiceID]
	JOIN [Customer] CUS ON O.CustomerID = CUS.ID
	WHERE O.[Sale date] = CONVERT(DATE, GETDATE())
GO