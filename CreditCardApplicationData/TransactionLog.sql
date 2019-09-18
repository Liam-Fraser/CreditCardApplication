CREATE TABLE [dbo].[TransactionLog]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[User] TEXT NOT NULL,
	[Date] DATETIME NOT NULL,
    [DOB] DATE NOT NULL, 
    [CardId] INT NOT NULL, 
    CONSTRAINT [FK_TransactionLog_ToCreditCards] FOREIGN KEY ([CardId]) REFERENCES [CreditCards]([Id]) 
);
