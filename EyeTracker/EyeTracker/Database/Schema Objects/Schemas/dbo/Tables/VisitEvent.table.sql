/*
CREATE TABLE [dbo].[VisitEvent] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [Key]             NVARCHAR (13)  NOT NULL,
    [Date]            DATETIME       NOT NULL,
    [Path]            NVARCHAR (256) NOT NULL,
    [PreviousVisitId] INT            NULL,
    [Ip]              NVARCHAR (15)  NULL,
    [Language]        NVARCHAR (50)  NULL,
    [OS]              NVARCHAR (150) NULL,
    [Browser]         NVARCHAR (150) NULL,
    [ScreenWidth]     INT            NOT NULL,
    [ScreenHeight]    INT            NOT NULL,
    [ClientWidth]     INT            NOT NULL,
    [ClientHeight]    INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);
*/

