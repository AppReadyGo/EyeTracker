CREATE TABLE [usr].[Users] (
    [ID]             INT           IDENTITY (1, 1) NOT NULL,
    [UserTypeID]     TINYINT       NOT NULL,
    [Email]          NVARCHAR (50) NOT NULL,
    [Password]       NVARCHAR (50) NOT NULL,
    [PasswordSalt]   NVARCHAR (50) NOT NULL,
    [CreateDate]     DATETIME      NOT NULL,
    [LastAccessDate] DATETIME      NULL,
	[Activated]		BIT			   NOT NULL,
	[Unsubscribed]  BIT			   NOT NULL,
	[FirstName]		NVARCHAR (100) NULL,
	[LastName]		NVARCHAR (100) NULL,
	[SpecialAccess] BIT			   NOT NULL,
	[MembershipID]	SMALLINT	   NOT NULL,
	[AcceptedTermsAndConditions] BIT NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);
GO

ALTER TABLE [usr].[Users]
ADD CONSTRAINT UC_Email UNIQUE ([Email])
GO

ALTER TABLE [usr].[Users]
ADD CONSTRAINT [FK_User_Membership] FOREIGN KEY ([MembershipID]) REFERENCES [usr].[Memberships] ([ID]);
GO