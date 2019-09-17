﻿CREATE TABLE [dbo].[TransactionLog]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[User] TEXT NOT NULL,
	[Date] DATETIME NOT NULL,
	[CardName] TEXT NULL, 
    [DOB] DATE NOT NULL
);
