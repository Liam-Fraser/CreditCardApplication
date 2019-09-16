CREATE TABLE [dbo].[TransactionLog]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[User] TEXT NOT NULL,
	[Date] DATE NOT NULL,
	[CardName] TEXT NULL
);
