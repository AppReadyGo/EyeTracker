CREATE TABLE [dbo].[VisitEvent] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [Key]             NVARCHAR (13)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Date]            DATETIME       NOT NULL,
    [Path]            NVARCHAR (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [PreviousVisitId] INT            NULL,
    [Ip]              NVARCHAR (15)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [Language]        NVARCHAR (50)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [OS]              NVARCHAR (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [Browser]         NVARCHAR (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [ScreenWidth]     INT            NOT NULL,
    [ScreenHeight]    INT            NOT NULL,
    [ClientWidth]     INT            NOT NULL,
    [ClientHeight]    INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

