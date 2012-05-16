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

