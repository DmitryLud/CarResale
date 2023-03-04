--CREATE DATABASE CarResale

USE CarResale

DROP TABLE IF EXISTS [Orders], [Cars], [ReceiptInvoice], [Models], [Customers], [Marks]

CREATE TABLE [Marks](
	[ID] INT IDENTITY NOT NULL,
	[Mark] VARCHAR(30) NOT NULL,
	CONSTRAINT [PK__Mark__ID] PRIMARY KEY ([ID])
)

CREATE TABLE [Models](
	[ID] INT IDENTITY NOT NULL,
	[MarkID] INT NOT NULL,
	[Model] VARCHAR(30) NOT NULL,
	CONSTRAINT [PK__Model__ID] PRIMARY KEY ([ID]),
	CONSTRAINT [FK__Mark] FOREIGN KEY ([MarkID]) REFERENCES [Marks]([ID])
)

CREATE TABLE [ReceiptInvoice](
	[ID] INT IDENTITY NOT NULL,
	[Date of acquisition] DATE NOT NULL,
	[Acquisistion price] DECIMAL(14,2) NOT NULL,
	[Other costs] DECIMAL(14,2) NULL DEFAULT(0),
	[Total acquisistion price] DECIMAL(14,2) NOT NULL,
	CONSTRAINT [PK__Receipt__ID] PRIMARY KEY ([ID]),
	CONSTRAINT [CHECK_Date_of_acquisition] CHECK ([Date of acquisition] < GETDATE())
)

CREATE TABLE [Cars](
	[ID] INT IDENTITY NOT NULL,
	[ModelID] INT NOT NULL,
	[ReceiptInvoiceID] INT NOT NULL,
	[VIN] VARCHAR(17) NOT NULL,
	[TRIM] VARCHAR(30) NOT NULL,
	[Year] INT NOT NULL,
	[Color] VARCHAR(20) NOT NULL,
	[Mileage] FLOAT NOT NULL,
	[Transmission] VARCHAR(9) NOT NULL,
	[FuelType] VARCHAR(8) NOT NULL,
	CONSTRAINT [PK__Car__ID] PRIMARY KEY ([ID]),
	CONSTRAINT [FK__Model] FOREIGN KEY ([ModelID]) REFERENCES [Models]([ID]),
	CONSTRAINT [FK__Car__ReceiptInvoice] FOREIGN KEY ([ReceiptInvoiceID]) REFERENCES ReceiptInvoice([ID]),
	CONSTRAINT [CHECK_Year] CHECK ([Year] < YEAR(GETDATE())),
	CONSTRAINT [CHECK_VIN] CHECK (LEN([VIN]) = 17)
)

CREATE TABLE [Customers](
	[ID] INT IDENTITY NOT NULL,
	[Surname] VARCHAR(30) NOT NULL,
	[Name] VARCHAR(30) NOT NULL,
	[Phone] VARCHAR(13) NOT NULL,
	[Email] VARCHAR(50) NOT NULL,
	CONSTRAINT [PK__Customer__ID] PRIMARY KEY ([ID]),
	CONSTRAINT [CHECK_Phone] CHECK ([Phone] LIKE(+'375[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')),
	CONSTRAINT [CHECK_Email] CHECK ([Email] LIKE '%@gmail.com' OR [Email] LIKE '%@mail.ru'),
)

CREATE TABLE [Orders](
	[ID] INT IDENTITY NOT NULL,
	[CarID] INT NOT NULL,
	[CustomerID] INT NOT NULL,
	[Sale date] DATE NOT NULL,
	[Sale price] DECIMAL(14,2) NOT NULL,
	CONSTRAINT [PK__Order__ID] PRIMARY KEY ([ID]),
	CONSTRAINT [FK__Customer__ID] FOREIGN KEY ([CustomerID]) REFERENCES Customers([ID]),
	CONSTRAINT [FK__Order__Car] FOREIGN KEY ([CarID]) REFERENCES Cars([ID])
)
GO