CREATE TABLE [dbo].[PageView] (
    [Id]                BIGINT         NOT NULL,
    [Date]              DATETIME       NOT NULL,
    [Path]              NVARCHAR (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Ip]                NVARCHAR (15)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [LanguageId]        INT            NULL,
    [CountryId]         INT            NULL,
    [City]              NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [OperationSystemId] INT            NULL,
    [BrowserId]         INT            NULL,
    [ScreenWidth]       INT            NOT NULL,
    [ScreenHeight]      INT            NOT NULL,
    [ClientWidth]       INT            NOT NULL,
    [ClientHeight]      INT            NOT NULL,
    [ApplicationId]     INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

