CREATE TABLE [dbo].[Screen] (
    [Id]            BIGINT       IDENTITY (1, 1) NOT NULL,
    [ApplicationId] INT          NOT NULL,
    [Width]         INT          NOT NULL,
    [Height]        INT          NOT NULL,
    [FileExtension] NVARCHAR (5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

