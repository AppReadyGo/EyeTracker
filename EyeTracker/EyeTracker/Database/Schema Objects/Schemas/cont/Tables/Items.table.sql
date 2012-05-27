CREATE TABLE [cont].[Items] (
	[ID]			INT IDENTITY(1,1)	NOT NULL,
	[SubKey]		NVARCHAR(256)		NOT NULL,
	[IsHTML]		BIT					NOT NULL,
	[Value]			NVARCHAR(MAX)		NOT NULL,
	[PageID]		INT					NULL,
	[MailID]		INT					NULL,
	[KeyID]			INT					NULL
);
GO

ALTER TABLE [cont].[Items]
	ADD CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED ([ID]);
GO

ALTER TABLE [cont].[Items]
    ADD CONSTRAINT [FK_Items_Pages] 
	FOREIGN KEY ([PageID]) REFERENCES [cont].[Pages] ([ID]);
GO

ALTER TABLE [cont].[Items]
    ADD CONSTRAINT [FK_Items_Mails] 
	FOREIGN KEY ([MailID]) REFERENCES [cont].[Mails] ([ID]);
GO

ALTER TABLE [cont].[Items]
    ADD CONSTRAINT [FK_Items_Keys] 
	FOREIGN KEY ([KeyID]) REFERENCES [cont].[Keys] ([ID]);
GO
