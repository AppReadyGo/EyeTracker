CREATE PROCEDURE [utls].[CreateBinaryInserts]

@tableName nvarchar(100),
@whereClause nvarchar(MAX) = '',
@orderByClause nvarchar(MAX) = ''

AS

--Declare variables
DECLARE @tableHasIdentity bit
DECLARE @sql nvarchar(MAX)
DECLARE @cols nvarchar(MAX)
DECLARE @vals nvarchar(MAX)
SET @cols = ''
SET @vals = ''

--Determine if table has an identity column
SELECT @tableHasIdentity = 
OBJECTPROPERTY(OBJECT_ID(TABLE_NAME), 'TableHasIdentity')
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = @tableName

--Do we need 'SET IDENTITY_INSERT tableName ON' statement?
IF @tableHasIdentity = 1
	BEGIN
		SET @sql = 'SELECT TOP 1 ''SET IDENTITY_INSERT ' + 
@tableName + ' ON '' FROM ' + @tableName
		EXEC sp_executesql @sql
	END

--Build list of columns and values
SELECT @cols = @cols + ',' + '[' + column_name + ']', @vals = @vals + 
	'+'',''+ISNULL(master.dbo.fn_varbintohexstr(cast([' + 
column_name + '] as varbinary(max))),''NULL'')' 
FROM INFORMATION_SCHEMA.columns 
WHERE TABLE_NAME = @tableName and DATA_TYPE != 'timestamp'

--Build SQL string
SET @sql = 'SELECT ''INSERT INTO [' + @tableName + '] (' + 
SUBSTRING(@cols,2,LEN(@cols)) + ') ' + 
			'VALUES (''+' + SUBSTRING(@vals, 6, 
LEN(@vals)) + '+'')'' FROM ' + @tableName
--Adjust @whereClause and @orderByClause
IF LEN(@whereClause) > 0
	SET @sql = @sql + ' WHERE ' + @whereClause
IF LEN(@orderByClause) > 0
	SET @sql= @sql + ' ORDER BY ' + @orderByClause

--Execute SQL string
exec sp_executesql @sql

--Do we need 'SET IDENTITY_INSERT tableName OFF' statement?
IF @tableHasIdentity = 1
	BEGIN
		SET @sql = 'SELECT TOP 1 ''SET IDENTITY_INSERT ' + 
@tableName + ' OFF '' FROM ' + @tableName
		EXEC sp_executesql @sql
	END
