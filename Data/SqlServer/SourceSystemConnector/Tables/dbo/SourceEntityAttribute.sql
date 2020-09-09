CREATE TABLE [dbo].[SourceEntityAttribute]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[SourceEntityId] INT NOT NULL FOREIGN KEY REFERENCES dbo.SourceEntity(Id),

	[Name] VARCHAR(100) NOT NULL,
	[PrimaryKey] BIT NOT NULL DEFAULT 0,
	[Hashed] BIT NOT NULL DEFAULT 1,
	[Description]	VARCHAR(200) NULL,
	[CodeType]		VARCHAR(100) NOT NULL,
	[DataType]		VARCHAR(100) NULL,
	[DataTypeLength] int NULL,
	[DataTypePrecision] int NULL,
	[Position]		int NULL,
	[IsNullable] BIT NOT NULL DEFAULT 0,
	[IsIdentity] BIT NOT NULL DEFAULT 0,

	[CreatedDate] DATETIME2 NOT NULL DEFAULT getdate(),
	[UpdatedDate] DATETIME2 NULL,
	[DeletedDate] DATETIME2 NULL,
)
