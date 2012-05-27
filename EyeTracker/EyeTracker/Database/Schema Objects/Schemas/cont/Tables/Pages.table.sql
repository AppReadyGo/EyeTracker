CREATE TABLE [cont].[Pages] (
	[ID]			INT  IDENTITY(1,1)	NOT NULL,
	[Url]				NVARCHAR(256)	NOT NULL,
	[ThemeID]			INT				NOT NULL
);
GO

ALTER TABLE [cont].[Pages]
	ADD CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED ([ID]);
GO

ALTER TABLE [cont].[Pages]
    ADD CONSTRAINT [FK_Pages_Themes] 
	FOREIGN KEY ([ThemeID]) REFERENCES [cont].[Themes] ([ID]);
GO
