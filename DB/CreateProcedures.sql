USE CarResale
GO

CREATE OR ALTER PROC GetModels
	@MarkID INT
AS
	SELECT [Model] FROM Models
	WHERE [MarkID] = @MarkID
GO

CREATE OR ALTER PROC SpecificDayReport 
	@Date DATE
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

CREATE OR ALTER PROC SpecificPeriodReport
	@DateStart DATE, @DateEnd DATE
AS
	SELECT C.VIN, MA.Mark, M.Model, CUS.[Name], CUS.[Surname], RI.[Date of acquisition], RI.[Acquisistion price], RI.[Total acquisistion price],
		O.[Sale price], (O.[Sale price] - RI.[Total acquisistion price]) AS 'Financial result'
	FROM [Orders] O
	JOIN [Cars] C ON O.CarID = C.ID
	JOIN [Models] M ON M.ID = C.ModelID
	JOIN [Marks] MA ON MA.ID = M.MarkID
	JOIN [ReceiptInvoice] RI ON RI.ID = C.[ReceiptInvoiceID]
	JOIN [Customers] CUS ON O.CustomerID = CUS.ID
	WHERE O.[Sale date] BETWEEN @DateStart AND @DateEnd
GO