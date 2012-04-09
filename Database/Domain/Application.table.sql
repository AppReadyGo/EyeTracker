CREATE TABLE [dbo].[Application] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (225) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [CreateDate]  DATETIME       NOT NULL,
    [Type]        INT            NOT NULL,
    [PortfolioId] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

