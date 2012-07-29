
CREATE TABLE [dbo].[ViewPart] (
    [Id]         BIGINT   IDENTITY (1, 1) NOT NULL,
    [X]          INT      NOT NULL,
    [Y]          INT      NOT NULL,
	[StartDate]          DATETIME NULL,
    [FinishDate]         DATETIME NULL,
	[Orientation]        INT      NULL,
    [PageViewId] BIGINT   NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

GO

ALTER TABLE [dbo].[ViewPart]
    ADD CONSTRAINT [FK_ViewPart_PageView] FOREIGN KEY ([PageViewId]) REFERENCES [dbo].[PageView] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE;


GO

