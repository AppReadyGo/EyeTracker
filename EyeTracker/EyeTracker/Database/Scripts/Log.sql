CREATE SCHEMA [log];
GO
CREATE TABLE [log].[Log](
        [ID] [INT] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
        [EventID] [INT] NULL,
        [Priority] [INT] NOT NULL,
        [Severity] [nvarchar](32) NOT NULL,
        [Title] [nvarchar](256) NOT NULL,
        [TIMESTAMP] [datetime] NOT NULL,
        [MachineName] [nvarchar](32) NOT NULL,
        [AppDomainName] [nvarchar](512) NOT NULL,
        [ProcessID] [nvarchar](256) NOT NULL,
        [ProcessName] [nvarchar](512) NOT NULL,
        [ThreadName] [nvarchar](512) NULL,
        [Win32ThreadId] [nvarchar](128) NULL,
        [Message] [nvarchar](1500) NULL,
        [FormattedMessage] [nvarchar](MAX) NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
        [ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [log].[Category](
        [ID] [INT] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
        [Name] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
        [ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [log].[CategoryLog](
        [ID] [INT] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
        [CategoryID] [INT] NOT NULL,
        [LogID] [INT] NOT NULL,
 CONSTRAINT [PK_CategoryLog] PRIMARY KEY CLUSTERED 
(
        [ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [log].[CategoryLog]  WITH CHECK ADD  CONSTRAINT [FK_CategoryLog_Audit] FOREIGN KEY([LogID])
REFERENCES [log].[Log] ([ID])
GO

ALTER TABLE [log].[CategoryLog] CHECK CONSTRAINT [FK_CategoryLog_Audit]
GO

ALTER TABLE [log].[CategoryLog]  WITH CHECK ADD  CONSTRAINT [FK_CategoryLog_Category] FOREIGN KEY([CategoryID])
REFERENCES [log].[Category] ([ID])
GO

ALTER TABLE [log].[CategoryLog] CHECK CONSTRAINT [FK_CategoryLog_Category]
GO
-- =============================================
-- Author:              Yuri Panshin
-- Create date: 26/02/2010
-- Description: Add category log link 
-- =============================================
CREATE PROCEDURE [log].[CategoryLogInsert]
        @CategoryID INT,
        @LogID INT
AS
SET NOCOUNT ON;      
DECLARE @TranStarted   bit              
SET @TranStarted = 0              
IF( @@TRANCOUNT = 0 )              
BEGIN              
        BEGIN TRANSACTION              
        SET @TranStarted = 1              
END              
BEGIN TRY 


DECLARE @RETURN INT 
DECLARE @CatCategoryLog INT
SELECT @CatCategoryLog FROM [log].[CategoryLog] WHERE CategoryID=@CategoryID AND LogID = @LogID
IF @CatCategoryLog IS NULL
BEGIN
        INSERT INTO [log].[CategoryLog] (CategoryID, LogID) VALUES(@CategoryID, @LogID)
        SET @RETURN= SCOPE_IDENTITY()
END
ELSE  SET @RETURN= @CatCategoryLog

IF( @TranStarted = 1 )               
BEGIN              
        COMMIT TRANSACTION              
        RETURN @RETURN              
END              
END TRY              
BEGIN CATCH         
IF( @TranStarted = 1 )              
BEGIN               
   ROLLBACK TRANSACTION              
END               
ELSE               
   RETURN 1              
END CATCH

GO
-- =============================================
-- Author:              Yuri Panshin
-- Create date: 26/02/2010
-- Description: Add caategory to logging system 
-- Sample: EXEC [log].[AddCategory] @categoryName=N'Test',@CategoryLog=9569 
-- =============================================
CREATE PROCEDURE [log].[AddCategory]  
        @CategoryName nvarchar(64),  
        @LogID INT  
AS  
SET NOCOUNT ON;      
DECLARE @TranStarted   bit              
SET @TranStarted = 0              
IF( @@TRANCOUNT = 0 )              
BEGIN              
        BEGIN TRANSACTION              
        SET @TranStarted = 1              
END              
BEGIN TRY               
   
DECLARE @CatID INT 
 
SELECT @CatID = ID FROM [log].[Category] WHERE Name = @CategoryName 
  
IF @CatID IS NULL  
BEGIN  
  INSERT INTO  [log].[Category] (Name) VALUES(@CategoryName)
    
  SELECT @CatID = @@IDENTITY  
END  
  
EXEC [log].[CategoryLogInsert] @CatID, @LogID   
      
IF( @TranStarted = 1 )               
BEGIN              
        COMMIT TRANSACTION              
        RETURN @CatID              
END              
END TRY              
BEGIN CATCH         
IF( @TranStarted = 1 )              
BEGIN               
   ROLLBACK TRANSACTION              
END               
ELSE               
    RETURN 1              
END CATCH


GO
-- =============================================
-- Author:              Yuri Panshin
-- Create date: 26/02/2010
-- Description: Clear all logs
-- =============================================
CREATE PROCEDURE [log].[ClearLog]
AS
SET NOCOUNT ON;      
DECLARE @TranStarted   bit              
SET @TranStarted = 0              
IF( @@TRANCOUNT = 0 )              
BEGIN              
        BEGIN TRANSACTION              
        SET @TranStarted = 1              
END              
BEGIN TRY 
              
        DELETE FROM [log].[CategoryLog]
        DELETE FROM [log].[Log]
		DELETE FROM [log].[Category]
    
IF( @TranStarted = 1 )               
BEGIN              
        COMMIT TRANSACTION              
        RETURN 0              
END              
END TRY              
BEGIN CATCH         
IF( @TranStarted = 1 )              
BEGIN               
   ROLLBACK TRANSACTION              
END               
ELSE               
   RETURN 1              
END CATCH

GO
-- =============================================
-- Author:              Yuri Panshin
-- Create date: 26/02/2010
-- Description: Write log 
-- =============================================
CREATE PROCEDURE [log].[LogWrite]  
        @EventID INT,   
        @Priority INT,   
        @Severity nvarchar(32),   
        @Title nvarchar(256),   
        @TIMESTAMP datetime,  
        @MachineName nvarchar(32),   
        @AppDomainName nvarchar(512),  
        @ProcessID nvarchar(256),  
        @ProcessName nvarchar(512),  
        @ThreadName nvarchar(512),  
        @Win32ThreadId nvarchar(128),  
        @Message nvarchar(1500),  
        @FormattedMessage ntext,  
        @LogID INT OUTPUT  
AS   
SET NOCOUNT ON;      
DECLARE @TranStarted   bit              
SET @TranStarted = 0              
IF( @@TRANCOUNT = 0 )              
BEGIN              
        BEGIN TRANSACTION              
        SET @TranStarted = 1              
END              
BEGIN TRY               


INSERT INTO [log].[Log] (  
        [EventID],  
        [Priority],  
        [Severity],  
        [Title],  
        [TIMESTAMP],  
        [MachineName],  
        [AppDomainName],  
        [ProcessID],  
        [ProcessName],  
        [ThreadName],  
        [Win32ThreadId],  
        [Message],  
        [FormattedMessage])  
VALUES (  
        @EventID,   
        @Priority,   
        @Severity,   
        @Title,   
        @TIMESTAMP,  
        @MachineName,   
        @AppDomainName,  
        @ProcessID,  
        @ProcessName,  
        @ThreadName,  
        @Win32ThreadId,  
        @Message,  
        @FormattedMessage)  

SET @LogID = SCOPE_IDENTITY()  
  
IF( @TranStarted = 1 )               
BEGIN              
        COMMIT TRANSACTION              
        RETURN @LogID              
END              
END TRY              
BEGIN CATCH         
IF( @TranStarted = 1 )              
BEGIN               
   ROLLBACK TRANSACTION              
END               
ELSE               
    RETURN 1              
END CATCH

GO