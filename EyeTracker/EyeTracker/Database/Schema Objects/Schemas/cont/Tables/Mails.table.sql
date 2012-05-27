CREATE TABLE [cont].[Mails] (
	[ID]			INT IDENTITY(1,1)	NOT NULL,
	[IsSystem]		BIT					NOT NULL,
	[Url]			NVARCHAR(256)		NOT NULL,
	[ThemeID]		INT					NOT NULL
);
GO

ALTER TABLE [cont].[Mails]
	ADD CONSTRAINT [PK_Mails] PRIMARY KEY CLUSTERED ([ID]);
GO

ALTER TABLE [cont].[Mails]
    ADD CONSTRAINT [FK_Mails_Themes] 
	FOREIGN KEY ([ThemeID]) REFERENCES [cont].[Themes] ([ID]);
GO
