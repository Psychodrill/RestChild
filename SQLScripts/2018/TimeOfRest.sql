USE [RestChild]
GO

declare @y bigint
----- ������ ��� ��������!!!!
set @y = 5 -- ��� 2018

SET IDENTITY_INSERT [dbo].[TimeOfRest] ON

INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10401, '��� 2018 ���',2, 1, 5, 30, 2018, 1, @y, null ,0, 401)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10402, '���� 2018 ���',2, 1, 6, 30, 2018, 1, @y, null ,0, 402)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10403, '���� 2018 ���',2, 1, 7, 30, 2018, 1, @y, null ,0, 403)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10404, '������ 2018 ���',2, 1, 8, 30, 2018, 1, @y, null ,0, 404)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10405, '�������� 2018 ���',2, 1, 9, 30, 2018, 1, @y, null ,0, 405)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10406, '������� 2018 ���',2, 1, 10, 30, 2018, 1, @y, null ,0, 406)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10407, '���� (I �����) 2018 ���',1, 1, 6, 23, 2018, 1, @y, 1 ,0, 407)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10408, '����-���� (II �����) 2018 ���',1, 24, 6, 23, 2018, 1, @y, 2 ,0, 408)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10409, '����-������ (III �����) 2018 ���',1, 17, 7, 23, 2018, 1, @y, 3 ,0, 409)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10410, '������ (IV �����) 2018 ���',1, 9, 8, 23, 2018, 1, @y, 4 ,0, 410)

INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10411, '��� 2018 ���',13, 1, 5, 30, 2018, 1, @y, null ,0, 411)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10412, '���� 2018 ���',13, 1, 6, 30, 2018, 1, @y, null ,0, 412)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10413, '���� 2018 ���',13, 1, 7, 30, 2018, 1, @y, null ,0, 413)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10414, '������ 2018 ���',13, 1, 8, 30, 2018, 1, @y, null ,0, 414)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10415, '�������� 2018 ���',13, 1, 9, 30, 2018, 1, @y, null ,0, 415)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10416, '������� 2018 ���',13, 1, 10, 30, 2018, 1, @y, null ,0, 416)


SET IDENTITY_INSERT [dbo].[TimeOfRest] OFF