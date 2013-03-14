BEGIN TRAN

ALTER TABLE dbo.Application
ADD [UserID] INT NULL;

EXEC('
UPDATE dbo.Application 
SET [UserID] = p.UserID
FROM dbo.Application
INNER JOIN dbo.Portfolio p ON PortfolioID = p.ID;')

ALTER TABLE dbo.Application
ALTER COLUMN [UserID] INT NOT NULL;

ALTER TABLE [dbo].[Application]
DROP CONSTRAINT [FK_Application_Portfolio] 

DROP TABLE dbo.Portfolio;

COMMIT TRAN
--ROLLBACK TRAN