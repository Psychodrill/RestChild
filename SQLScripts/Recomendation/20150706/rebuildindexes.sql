USE RestChild --Enter the name of the database you want to reindex 
 
 
DECLARE @TableName varchar(255) 
  
 
DECLARE TableCursor CURSOR FOR 
SELECT table_name FROM INFORMATION_SCHEMA.TABLES  
WHERE table_type = 'BASE TABLE' 
 
  
 
OPEN TableCursor 
 
  
 
FETCH NEXT FROM TableCursor INTO @TableName 
 
WHILE @@FETCH_STATUS = 0 
 
BEGIN 
 
DBCC DBREINDEX(@TableName,' ',90) 
 
FETCH NEXT FROM TableCursor INTO @TableName 
 
END 
 
  
 
CLOSE TableCursor 
 
  
 
DEALLOCATE TableCursor 