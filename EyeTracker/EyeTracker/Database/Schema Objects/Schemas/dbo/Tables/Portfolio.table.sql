CREATE TABLE [dbo].[Portfolio] (
    [Id]          INT              IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (255)   NOT NULL,
    [TimeZone]    INT              NOT NULL,
    [CreateDate]  DATETIME         NOT NULL,
    [UserId]      UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

