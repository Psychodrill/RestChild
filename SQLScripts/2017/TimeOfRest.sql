USE [RestChildAiso]
GO

declare @y bigint

set @y = 10017 -- ��� 2017

SET IDENTITY_INSERT [dbo].[TimeOfRest] ON

INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (201, '��� 2017 ���',2, 1, 5, 30, 2017, 1, @y, null ,0, 201)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (202, '���� 2017 ���',2, 1, 6, 30, 2017, 1, @y, null ,0, 202)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (203, '���� 2017 ���',2, 1, 7, 30, 2017, 1, @y, null ,0, 203)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (204, '������ 2017 ���',2, 1, 8, 30, 2017, 1, @y, null ,0, 204)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (205, '�������� 2017 ���',2, 1, 9, 30, 2017, 1, @y, null ,0, 205)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (206, '������� 2017 ���',2, 1, 10, 30, 2017, 1, @y, null ,0, 206)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (207, '���� (I �����) 2017 ���',1, 1, 6, 23, 2017, 1, @y, 1 ,0, 207)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (208, '����-���� (II �����) 2017 ���',1, 24, 6, 23, 2017, 1, @y, 2 ,0, 208)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (209, '����-������ (III �����) 2017 ���',1, 17, 7, 23, 2017, 1, @y, 3 ,0, 209)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (210, '������ (IV �����) 2017 ���',1, 9, 8, 23, 2017, 1, @y, 4 ,0, 210)

INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (211, '��� 2017 ���',13, 1, 5, 30, 2017, 1, @y, null ,0, 211)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (212, '���� 2017 ���',13, 1, 6, 30, 2017, 1, @y, null ,0, 212)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (213, '���� 2017 ���',13, 1, 7, 30, 2017, 1, @y, null ,0, 213)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (214, '������ 2017 ���',13, 1, 8, 30, 2017, 1, @y, null ,0, 214)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (215, '�������� 2017 ���',13, 1, 9, 30, 2017, 1, @y, null ,0, 215)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (216, '������� 2017 ���',13, 1, 10, 30, 2017, , @y, null ,0, 216)


SET IDENTITY_INSERT [dbo].[TimeOfRest] OFF