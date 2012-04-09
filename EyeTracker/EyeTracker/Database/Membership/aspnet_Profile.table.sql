CREATE TABLE [dbo].[aspnet_Profile] (
    [UserId]               UNIQUEIDENTIFIER NOT NULL,
    [PropertyNames]        NTEXT            COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [PropertyValuesString] NTEXT            COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [PropertyValuesBinary] IMAGE            NOT NULL,
    [LastUpdatedDate]      DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION
);

