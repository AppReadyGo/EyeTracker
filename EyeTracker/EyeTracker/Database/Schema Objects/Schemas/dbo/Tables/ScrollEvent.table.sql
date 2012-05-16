CREATE TABLE [dbo].[ScrollEvent] (
    [Id]                 BIGINT IDENTITY (1, 1) NOT NULL,
    [FirstTouchId]       BIGINT NOT NULL,
    [LastTouchId]        BIGINT NOT NULL,
    [SessionInfoEventId] BIGINT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

