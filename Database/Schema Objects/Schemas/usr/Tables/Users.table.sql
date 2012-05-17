CREATE TABLE [usr].[Users] (
    [ID]             INT           IDENTITY (1, 1) NOT NULL,
    [UserTypeID]     TINYINT       NOT NULL,
    [Email]          NVARCHAR (50) NOT NULL,
    [Password]       NVARCHAR (50) NOT NULL,
    [PasswordSalt]   NVARCHAR (50) NOT NULL,
    [CreateDate]     DATETIME      NOT NULL,
    [LastAccessDate] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

