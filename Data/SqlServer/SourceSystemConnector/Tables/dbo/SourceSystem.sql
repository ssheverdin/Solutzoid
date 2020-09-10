CREATE TABLE [dbo].[SourceSystem]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] VARCHAR(100) NOT NULL UNIQUE,
	[ConnectionName] VARCHAR(100) NOT NULL UNIQUE,
	[Type] VARCHAR(100) NOT NULL,
	[Description] VARCHAR(200) NULL,
	[SyncEnabled] BIT NULL,
	[InitialSyncDate] DATETIME2 NULL,
	[LastSyncDate] DATETIME2 NULL,

	[CreatedDate] DATETIME2 NOT NULL DEFAULT getdate(),
	[UpdatedDate] DATETIME2 NULL,
	[DeletedDate] DATETIME2 NULL, 
)
