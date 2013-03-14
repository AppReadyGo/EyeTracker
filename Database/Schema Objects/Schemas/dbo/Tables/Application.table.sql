CREATE TABLE [dbo].[Application] (
    [ID]			INT				IDENTITY (1, 1) NOT NULL,
    [Description]	NVARCHAR (225)	NOT NULL,
    [CreateDate]	DATETIME		NOT NULL,
    [Type]			INT				NOT NULL,
    [UserID]		INT				NOT NULL,
	[PackageID]		INT				NULL
);
GO;

ALTER TABLE [dbo].[Application]
ADD CONSTRAINT [PK_Application] PRIMARY KEY CLUSTERED ([Id] ASC);
GO;

ALTER TABLE [dbo].[Application]
ADD CONSTRAINT [FK_Application_User] FOREIGN KEY ([UserID]) REFERENCES [usr].[Users] ([ID]);
GO

ALTER TABLE [dbo].[Application]
ADD CONSTRAINT [FK_Application_Package] FOREIGN KEY ([PackageID]) REFERENCES [dbo].[Packages] ([ID]);
GO


