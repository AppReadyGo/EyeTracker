-- =============================================
-- Script Template
-- =============================================
INSERT INTO [dbo].[Application] ([Description] ,[CreateDate] ,[Type] ,[PortfolioId])
VALUES ('RunNow', '20120525', 3 /*3 stands for android, see EyeTracker.Domain.Model.ApplicationType*/, (SELECT ID FROM [dbo].[Portfolio] WHERE [Description] = 'Demo Portfolio'));
