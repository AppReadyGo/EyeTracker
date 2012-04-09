CREATE TABLE [dbo].[ViewPart] (
    [Id]         BIGINT   IDENTITY (1, 1) NOT NULL,
    [Date]       DATETIME NOT NULL,
    [X]          INT      NOT NULL,
    [Y]          INT      NOT NULL,
    [PageViewId] BIGINT   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

