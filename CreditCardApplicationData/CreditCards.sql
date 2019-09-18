CREATE TABLE [dbo].[CreditCards]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[CardName] TEXT NOT NULL,
	[MinimumAge] INT NOT NULL DEFAULT 0,
	[MinimumSalary] INT NOT NULL DEFAULT 0, 
    [MaximumSalary] INT NOT NULL DEFAULT -1
)
