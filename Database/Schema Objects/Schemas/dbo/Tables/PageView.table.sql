CREATE TABLE [dbo].[PageView] (
    [Id]                BIGINT IDENTITY (1, 1) NOT NULL,
    [Date]              DATETIME       NOT NULL,
    [Path]              NVARCHAR (256) NOT NULL,
    [Ip]                NVARCHAR (15)  NULL,-- TODO: Remove
    [LanguageId]        INT            NULL,
    [CountryId]         INT            NULL,
    [City]              NVARCHAR (50)  NULL,
    [OperationSystemId] INT            NULL,
    [BrowserId]         INT            NULL,
    [ScreenWidth]       INT            NOT NULL,
    [ScreenHeight]      INT            NOT NULL,
    [ClientWidth]       INT            NOT NULL,
    [ClientHeight]      INT            NOT NULL,
    [ApplicationId]     INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);
GO

ALTER TABLE [dbo].[PageView]
ADD CONSTRAINT [FK_PageView_Browser] FOREIGN KEY ([BrowserId]) REFERENCES [dbo].[Browser] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[PageView]
ADD CONSTRAINT [FK_PageView_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[PageView]
ADD CONSTRAINT [FK_PageView_OperationSystem] FOREIGN KEY ([OperationSystemId]) REFERENCES [dbo].[OperationSystem] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[PageView]
ADD CONSTRAINT [FK_PageView_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[Application] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[PageView]
ADD CONSTRAINT [FK_PageView_Countries] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([GeoId]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

