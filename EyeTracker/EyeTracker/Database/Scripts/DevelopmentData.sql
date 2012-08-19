-- =============================================
-- Script initilizate database with development data
-- =============================================
INSERT INTO [Fingerprint].[usr].[Memberships]
           ([ID]
           ,[Name])
     VALUES
           (1
           ,'Pro')
GO

INSERT INTO [usr].[Users] ([UserTypeID], [Email], [Password], [PasswordSalt], [CreateDate], [Activated], [FirstName], [LastName],[Unsubscribed], [SpecialAccess],[MembershipID], [AcceptedTermsAndConditions])
VALUES (1/*Staff*/, 'dev@mobillify.com', 'XW0mA5DzfN4XL851H/i1xNFFbMOdtjVAL6fjBN5monE='/*111111*/, '/WCjbQ==', '20120101', 1, 'Development', 'Mobillify', 0, 1, (select [ID] from [usr].[Memberships] where Name = 'Pro'), 1);

GO

INSERT INTO [Fingerprint].[usr].[StaffRoles]
           ([ID]
           ,[Name])
     VALUES
           (1,
           'admin');
GO

INSERT INTO [usr].[UserStaffRoles](UserID, RoleID)
VALUES((SELECT ID FROM [usr].[Users] WHERE Email = 'dev@mobillify.com'), 1)
GO

INSERT INTO [dbo].[Portfolio] ([Description] ,[TimeZone] ,[CreateDate] ,[UserId])
VALUES ('Demo Portfolio', 0, '20120525', (SELECT ID FROM [usr].[Users] WHERE Email = 'dev@mobillify.com'));
GO

INSERT INTO [dbo].[Application] ([Description] ,[CreateDate] ,[Type] ,[PortfolioId])
VALUES ('Demo Application', '20120525', 3 /*3 stands for android, see EyeTracker.Domain.Model.ApplicationType*/, (SELECT ID FROM [dbo].[Portfolio] WHERE [Description] = 'Demo Portfolio'));

GO
INSERT INTO [dbo].[OperationSystem] ([Name])
VALUES ('2.3.3');