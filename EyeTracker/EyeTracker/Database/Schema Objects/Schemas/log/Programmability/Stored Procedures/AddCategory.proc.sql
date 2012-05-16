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


