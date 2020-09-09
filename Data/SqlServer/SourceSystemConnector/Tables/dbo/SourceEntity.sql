CREATE TABLE [dbo].[SourceEntity]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[SourceSystemId] INT NOT NULL FOREIGN KEY REFERENCES dbo.SourceSystem(Id),
	
	[SchemaName] VARCHAR(25) NOT NULL,
	[Name] VARCHAR(100) NOT NULL,
	[FriendlyName] VARCHAR(100) NOT NULL,
	[Type] VARCHAR(50) NULL,
	[EntityCreatedDate] DATETIME2 NULL,
	[EntityLastUpdatedDate] DATETIME2 NULL,
	[SourceColumnCount] INT NULL,
	[Description] VARCHAR(200) NULL,
	
	[CreatedDate] DATETIME2 NOT NULL DEFAULT getdate(),
	[UpdatedDate] DATETIME2 NULL,
	[DeletedDate] DATETIME2 NULL, 
)
