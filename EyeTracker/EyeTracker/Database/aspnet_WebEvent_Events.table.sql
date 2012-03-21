CREATE TABLE [dbo].[aspnet_WebEvent_Events] (
    [EventId]                CHAR (32)       COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [EventTimeUtc]           DATETIME        NOT NULL,
    [EventTime]              DATETIME        NOT NULL,
    [EventType]              NVARCHAR (256)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [EventSequence]          DECIMAL (19)    NOT NULL,
    [EventOccurrence]        DECIMAL (19)    NOT NULL,
    [EventCode]              INT             NOT NULL,
    [EventDetailCode]        INT             NOT NULL,
    [Message]                NVARCHAR (1024) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [ApplicationPath]        NVARCHAR (256)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [ApplicationVirtualPath] NVARCHAR (256)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [MachineName]            NVARCHAR (256)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [RequestUrl]             NVARCHAR (1024) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [ExceptionType]          NVARCHAR (256)  COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [Details]                NTEXT           COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    PRIMARY KEY CLUSTERED ([EventId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

