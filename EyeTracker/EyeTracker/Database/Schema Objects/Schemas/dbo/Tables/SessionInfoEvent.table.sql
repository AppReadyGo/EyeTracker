--CREATE TABLE [dbo].[SessionInfoEvent] (
--    [Id]             BIGINT         IDENTITY (1, 1) NOT NULL,
--    [Path]           NVARCHAR (225) NOT NULL,
--    [ClientWidth]    INT            NOT NULL,
--    [ClientHeight]   INT            NOT NULL,
--    [StartDate]      DATETIME       NOT NULL,
--    [CloseDate]      DATETIME       NOT NULL,
--    [PackageEventId] BIGINT         NOT NULL,
--    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
--);
--GO

--ALTER TABLE [dbo].[SessionInfoEvent]
--ADD CONSTRAINT [FK_SessionInfoEvent_PackageEvent] FOREIGN KEY ([PackageEventId]) REFERENCES [dbo].[PackageEvent] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;
--GO



