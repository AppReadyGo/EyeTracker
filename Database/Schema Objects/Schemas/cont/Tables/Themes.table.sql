CREATE TABLE [cont].[Themes] (
	[ID]			INT					NOT NULL,
	[Name]			VARCHAR(256)		NOT NULL,
	[Url]			VARCHAR(256)		NOT NULL,
	[Type]			SMALLINT			NOT NULL
);
GO

ALTER TABLE [cont].[Themes]
	ADD CONSTRAINT [PK_Themes] PRIMARY KEY CLUSTERED ([ID]);
GO
