CREATE TABLE [dbo].[ViewPartEvent] (
    [Id]          BIGINT   IDENTITY (1, 1) NOT NULL,
    [ScrollTop]   INT      NOT NULL,
    [ScrollLeft]  INT      NOT NULL,
    [TimeSpan]    BIGINT   NOT NULL,
    [Date]        DATETIME NOT NULL,
    [VisitInfoId] BIGINT   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

