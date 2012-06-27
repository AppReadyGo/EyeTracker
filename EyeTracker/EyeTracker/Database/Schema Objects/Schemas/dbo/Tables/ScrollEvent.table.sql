/*
CREATE TABLE [dbo].[ScrollEvent] (
    [Id]                 BIGINT IDENTITY (1, 1) NOT NULL,
    [FirstTouchId]       BIGINT NOT NULL,
    [LastTouchId]        BIGINT NOT NULL,
    [SessionInfoEventId] BIGINT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

GO

ALTER TABLE [dbo].[ScrollEvent]
ADD CONSTRAINT [FK_ScrollEvent_ClickEvent] FOREIGN KEY ([FirstTouchId]) REFERENCES [dbo].[ClickEvent] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
*/
