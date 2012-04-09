CREATE TABLE [dbo].[AccountProfiler] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [UpdateFriquency] INT             NOT NULL,
    [Price]           DECIMAL (19, 5) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

