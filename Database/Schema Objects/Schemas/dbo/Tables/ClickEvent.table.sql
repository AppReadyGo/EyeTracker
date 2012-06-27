/*
CREATE TABLE [dbo].[ClickEvent] (
    [Id]                 BIGINT   IDENTITY (1, 1) NOT NULL,
    [ClientX]            INT      NOT NULL,
    [ClientY]            INT      NOT NULL,
    [Date]               DATETIME NOT NULL,
    [VisitInfoId]        BIGINT   NOT NULL,
    [Orientation]        INT      NULL,
    [SessionInfoEventId] BIGINT   NULL,
    [ScrollEventId]      BIGINT   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);
*/

