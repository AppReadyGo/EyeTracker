CREATE TABLE [dbo].[Click] (
    [Id]         BIGINT   IDENTITY (1, 1) NOT NULL,
    [Date]       DATETIME NOT NULL,
    [X]          INT      NOT NULL,
    [Y]          INT      NOT NULL,
    [PageViewId] BIGINT   NOT NULL,
	[Orientation] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

GO
ALTER TABLE [dbo].[Click]
    ADD CONSTRAINT [FK_Click_PageView] FOREIGN KEY ([PageViewId]) REFERENCES [dbo].[PageView] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE;


GO


