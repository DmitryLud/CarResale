USE CarResale
GO

CREATE OR ALTER PROC GetModels
	@MarkID INT
AS
	SELECT [Name] FROM Model
	WHERE [MarkID] = @MarkID
GO

CREATE OR ALTER PROC SpecificDayReport 
	@Date DATE
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

CREATE OR ALTER PROC SpecificPeriodReport
	@DateStart DATE, @DateEnd DATE
AS
	SELECT C.VIN, MA.[Name] AS 'Mark', M.[Name] AS 'Model', CUS.[Name], CUS.[Surname], RI.[Date of acquisition], RI.[Acquisistion price], RI.[Total acquisistion price],
		O.[Sale price], (O.[Sale price] - RI.[Total acquisistion price]) AS 'FinancialResult'
	FROM [Order] O
	JOIN [Car] C ON O.CarID = C.ID
	JOIN [Model] M ON M.ID = C.ModelID
	JOIN [Mark] MA ON MA.ID = M.MarkID
	JOIN [ReceiptInvoice] RI ON RI.ID = C.[ReceiptInvoiceID]
	JOIN [Customer] CUS ON O.CustomerID = CUS.ID
	WHERE O.[Sale date] BETWEEN @DateStart AND @DateEnd
GO