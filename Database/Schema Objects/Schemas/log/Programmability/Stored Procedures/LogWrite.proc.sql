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

