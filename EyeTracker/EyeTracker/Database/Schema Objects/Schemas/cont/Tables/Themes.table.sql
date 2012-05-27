CREATE TABLE [cont].[Themes] (
	[ID]			INT					NOT NULL,
	[Name]			NVARCHAR(256)		NOT NULL,
	[Url]			NVARCHAR(256)		NOT NULL,
	[Type]			INT					NOT NULL
);
GO

ALTER TABLE [cont].[Themes]
	ADD CONSTRAINT [PK_Themes] PRIMARY KEY CLUSTERED ([ID]);
GO
