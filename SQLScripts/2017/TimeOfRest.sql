USE [RestChildAiso]
GO

declare @y bigint

set @y = 10017 -- год 2017

SET IDENTITY_INSERT [dbo].[TimeOfRest] ON

INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (201, 'май 2017 год',2, 1, 5, 30, 2017, 1, @y, null ,0, 201)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (202, 'июнь 2017 год',2, 1, 6, 30, 2017, 1, @y, null ,0, 202)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (203, 'июль 2017 год',2, 1, 7, 30, 2017, 1, @y, null ,0, 203)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (204, 'август 2017 год',2, 1, 8, 30, 2017, 1, @y, null ,0, 204)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (205, 'сент€брь 2017 год',2, 1, 9, 30, 2017, 1, @y, null ,0, 205)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (206, 'окт€брь 2017 год',2, 1, 10, 30, 2017, 1, @y, null ,0, 206)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (207, 'июнь (I смена) 2017 год',1, 1, 6, 23, 2017, 1, @y, 1 ,0, 207)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (208, 'июнь-июль (II смена) 2017 год',1, 24, 6, 23, 2017, 1, @y, 2 ,0, 208)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (209, 'июль-август (III смена) 2017 год',1, 17, 7, 23, 2017, 1, @y, 3 ,0, 209)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (210, 'август (IV смена) 2017 год',1, 9, 8, 23, 2017, 1, @y, 4 ,0, 210)

INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (211, 'май 2017 год',13, 1, 5, 30, 2017, 1, @y, null ,0, 211)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (212, 'июнь 2017 год',13, 1, 6, 30, 2017, 1, @y, null ,0, 212)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (213, 'июль 2017 год',13, 1, 7, 30, 2017, 1, @y, null ,0, 213)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (214, 'август 2017 год',13, 1, 8, 30, 2017, 1, @y, null ,0, 214)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (215, 'сент€брь 2017 год',13, 1, 9, 30, 2017, 1, @y, null ,0, 215)
INSERT INTO [dbo].[TimeOfRest] ([Id], [Name],[TypeOfRestId],[DayOfMonth],[Month],[PeriodLength],[Year],[IsActive],[YearOfRestId],[GroupedTimeOfRestId],[LastUpdateTick],[Eid])
VALUES (216, 'окт€брь 2017 год',13, 1, 10, 30, 2017, , @y, null ,0, 216)


SET IDENTITY_INSERT [dbo].[TimeOfRest] OFF