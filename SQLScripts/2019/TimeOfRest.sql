USE [RestChild]
GO

declare @y bigint
----- ������ ��� �����!!!!
set @y = 10020 -- ��� 2019

SET IDENTITY_INSERT [dbo].[TimeOfRest] ON

INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10501, '��� 2019 ���',2, 1, 5, 30, 2019, 1, @y, null ,0, 10501)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10502, '���� 2019 ���',2, 1, 6, 30, 2019, 1, @y, null ,0, 10502)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10503, '���� 2019 ���',2, 1, 7, 30, 2019, 1, @y, null ,0, 10503)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10504, '������ 2019 ���',2, 1, 8, 30, 2019, 1, @y, null ,0, 10504)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10505, '�������� 2019 ���',2, 1, 9, 30, 2019, 1, @y, null ,0, 10505)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10506, '������� 2019 ���',2, 1, 10, 30, 2019, 1, @y, null ,0, 10506)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10507, '���� (I �����) 2019 ���',1, 1, 6, 23, 2019, 1, @y, 1 ,0, 10507)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10508, '����-���� (II �����) 2019 ���',1, 24, 6, 23, 2019, 1, @y, 2 ,0, 10508)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10509, '����-������ (III �����) 2019 ���',1, 17, 7, 23, 2019, 1, @y, 3 ,0, 10509)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10510, '������ (IV �����) 2019 ���',1, 9, 8, 23, 2019, 1, @y, 4 ,0, 10510)

INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10511, '��� 2019 ���',13, 1, 5, 30, 2019, 1, @y, null ,0, 10511)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10512, '���� 2019 ���',13, 1, 6, 30, 2019, 1, @y, null ,0, 10512)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10513, '���� 2019 ���',13, 1, 7, 30, 2019, 1, @y, null ,0, 10513)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10514, '������ 2019 ���',13, 1, 8, 30, 2019, 1, @y, null ,0, 10514)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10515, '�������� 2019 ���',13, 1, 9, 30, 2019, 1, @y, null ,0, 10515)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10516, '������� 2019 ���',13, 1, 10, 30, 2019, 1, @y, null ,0, 10516)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10517, '������ 2019 ���',13, 1, 4, 30, 2019, 1, @y, null ,0, 10517)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10518, '������ 2019 ���',13, 1, 11, 30, 2019, 1, @y, null ,0, 10518)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10519, '������� 2019 ���',13, 1, 12, 30, 2019, 1, @y, null ,0, 10519)

INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10520, '������ 2019 ���',2, 1, 4, 30, 2019, 1, @y, null ,0, 10520)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10521, '������ 2019 ���',2, 1, 11, 30, 2019, 1, @y, null ,0, 10521)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (10522, '������� 2019 ���',2, 1, 12, 30, 2019, 1, @y, null ,0, 10522)



SET IDENTITY_INSERT [dbo].[TimeOfRest] OFF