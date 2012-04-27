﻿CREATE TABLE [dbo].[aspnet_Membership] (
    [ApplicationId]                          UNIQUEIDENTIFIER NOT NULL,
    [UserId]                                 UNIQUEIDENTIFIER NOT NULL,
    [Password]                               NVARCHAR (128)   COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [PasswordFormat]                         INT              DEFAULT ((0)) NOT NULL,
    [PasswordSalt]                           NVARCHAR (128)   COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [MobilePIN]                              NVARCHAR (16)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [Email]                                  NVARCHAR (256)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [LoweredEmail]                           NVARCHAR (256)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [PasswordQuestion]                       NVARCHAR (256)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [PasswordAnswer]                         NVARCHAR (128)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [IsApproved]                             BIT              NOT NULL,
    [IsLockedOut]                            BIT              NOT NULL,
    [CreateDate]                             DATETIME         NOT NULL,
    [LastLoginDate]                          DATETIME         NOT NULL,
    [LastPasswordChangedDate]                DATETIME         NOT NULL,
    [LastLockoutDate]                        DATETIME         NOT NULL,
    [FailedPasswordAttemptCount]             INT              NOT NULL,
    [FailedPasswordAttemptWindowStart]       DATETIME         NOT NULL,
    [FailedPasswordAnswerAttemptCount]       INT              NOT NULL,
    [FailedPasswordAnswerAttemptWindowStart] DATETIME         NOT NULL,
    [Comment]                                NTEXT            COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    PRIMARY KEY NONCLUSTERED ([UserId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY],
    FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[aspnet_Applications] ([ApplicationId]) ON DELETE NO ACTION ON UPDATE NO ACTION,
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION
);
