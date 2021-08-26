use RestChild;

-- DELETE MatrialStatus
-- DELETE MilitaryDuty
-- DELETE TypeOfEducation
-- DELETE TieColor


DBCC CHECKIDENT (MatrialStatus, RESEED, 0)
DBCC CHECKIDENT (MilitaryDuty, RESEED, 0)
DBCC CHECKIDENT (TypeOfEducation, RESEED, 0)
DBCC CHECKIDENT (TieColor, RESEED, 0)

INSERT INTO MatrialStatus(Name, IsActive) VALUES ('������� �� �������(�) � �����', 'true')
INSERT INTO MatrialStatus(Name, IsActive) VALUES ('��������(�)', 'true')
INSERT INTO MatrialStatus(Name, IsActive) VALUES ('������� � ������������������ �����', 'true')

INSERT INTO MilitaryDuty(Name, IsActive) VALUES ('���������������', 'true')
INSERT INTO MilitaryDuty(Name, IsActive) VALUES ('�����������������', 'true')

INSERT INTO TypeOfEducation(Name, IsActive) VALUES ('�������', 'true')
INSERT INTO TypeOfEducation(Name, IsActive) VALUES ('������-�����������', 'true')
INSERT INTO TypeOfEducation(Name, IsActive) VALUES ('������������-������', 'true')
INSERT INTO TypeOfEducation(Name, IsActive) VALUES ('������', 'true')

INSERT INTO TieColor(Name, IsActive) VALUES ('��������� �������', 'true')
INSERT INTO TieColor(Name, IsActive) VALUES ('��������� ������� � ����� ������', 'true')
INSERT INTO TieColor(Name, IsActive) VALUES ('��������� ������� � 2 ����� ������', 'true')
INSERT INTO TieColor(Name, IsActive) VALUES ('����� �������', 'true')
INSERT INTO TieColor(Name, IsActive) VALUES ('����� ������� � ������� ������', 'true')
INSERT INTO TieColor(Name, IsActive) VALUES ('����� ������� � 2 ������� ������', 'true')
INSERT INTO TieColor(Name, IsActive) VALUES ('����� ������� � 3 ������� ������', 'true')
INSERT INTO TieColor(Name, IsActive) VALUES ('����� ������� � 4 ������� ������', 'true')
INSERT INTO TieColor(Name, IsActive) VALUES ('������� �������', 'true')