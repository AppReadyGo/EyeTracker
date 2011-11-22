CREATE TABLE [dbo].[Screen] (
    [Id]            BIGINT       IDENTITY (1, 1) NOT NULL,
    [Width]         INT          NOT NULL,
    [Height]        INT          NOT NULL,
    [ApplicationId] INT          NULL,
    [FileExtension] NVARCHAR (5) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

