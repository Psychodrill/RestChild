SET IDENTITY_INSERT dbo.City ON;
/****** Script for SelectTopNRows command from SSMS  ******/
insert into dbo.City
(Id, Name, [HaveAero],  [HaveRailway], [IsActive])
SELECT  0 [Id]
      ,'Москва' [Name]
      ,1 [HaveAero]
      ,1 [HaveRailway]
      ,1 [IsActive]
SET IDENTITY_INSERT dbo.City OFF;