use RestChild;

-- DELETE MatrialStatus
-- DELETE MilitaryDuty
-- DELETE TypeOfEducation
-- DELETE TieColor


DBCC CHECKIDENT (MatrialStatus, RESEED, 0)
DBCC CHECKIDENT (MilitaryDuty, RESEED, 0)
DBCC CHECKIDENT (TypeOfEducation, RESEED, 0)
DBCC CHECKIDENT (TieColor, RESEED, 0)

INSERT INTO MatrialStatus(Name, IsActive) VALUES ('Никогда не состоял(а) в браке', 'true')
INSERT INTO MatrialStatus(Name, IsActive) VALUES ('Разведен(а)', 'true')
INSERT INTO MatrialStatus(Name, IsActive) VALUES ('Состоит в зарегистрированном браке', 'true')

INSERT INTO MilitaryDuty(Name, IsActive) VALUES ('Военнообязанный', 'true')
INSERT INTO MilitaryDuty(Name, IsActive) VALUES ('Невоеннообязанный', 'true')

INSERT INTO TypeOfEducation(Name, IsActive) VALUES ('Среднее', 'true')
INSERT INTO TypeOfEducation(Name, IsActive) VALUES ('Средне-специальное', 'true')
INSERT INTO TypeOfEducation(Name, IsActive) VALUES ('Неоконченное-высшее', 'true')
INSERT INTO TypeOfEducation(Name, IsActive) VALUES ('Высшее', 'true')

INSERT INTO TieColor(Name, IsActive) VALUES ('Оранжевый галстук', 'true')
INSERT INTO TieColor(Name, IsActive) VALUES ('Оранжевый галстук и синий значок', 'true')
INSERT INTO TieColor(Name, IsActive) VALUES ('Оранжевый галстук и 2 синих значка', 'true')
INSERT INTO TieColor(Name, IsActive) VALUES ('Синий галстук', 'true')
INSERT INTO TieColor(Name, IsActive) VALUES ('Синий галстук и зеленый значок', 'true')
INSERT INTO TieColor(Name, IsActive) VALUES ('Синий галстук и 2 зеленых значка', 'true')
INSERT INTO TieColor(Name, IsActive) VALUES ('Синий галстук и 3 зеленых значка', 'true')
INSERT INTO TieColor(Name, IsActive) VALUES ('Синий галстук и 4 зеленых значка', 'true')
INSERT INTO TieColor(Name, IsActive) VALUES ('Зеленый галстук', 'true')