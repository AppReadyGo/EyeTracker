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

