CREATE TABLE [dbo].[CreditCards]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[CardName] TEXT NOT NULL,
	[MinimumAge] INT NOT NULL,
	[MinimumSalary] INT NOT NULL, 
    [MaximumSalary] INT NULL
)
