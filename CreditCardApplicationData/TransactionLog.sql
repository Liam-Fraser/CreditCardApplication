CREATE TABLE [dbo].[TransactionLog]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[UserName] TEXT NOT NULL,
	[Date] DATETIME NOT NULL,
    [DOB] DATE NOT NULL, 
    [CardId] INT NOT NULL,
	[QualifiedForCard] BIT DEFAULT 1
);
