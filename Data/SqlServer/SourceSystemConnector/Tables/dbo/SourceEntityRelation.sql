CREATE TABLE [dbo].[SourceEntityRelation]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[SourceEntityId] INT NOT NULL FOREIGN KEY REFERENCES dbo.SourceEntity(Id),

	[Name] VARCHAR(100) NOT NULL,

	[CreatedDate] DATETIME2 NOT NULL DEFAULT getdate(),
	[UpdatedDate] DATETIME2 NULL,
	[DeletedDate] DATETIME2 NULL,
)
