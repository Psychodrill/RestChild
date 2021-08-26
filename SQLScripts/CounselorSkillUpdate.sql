DECLARE @LastInsert bigint;

update [dbo].[Skill] set IsActive=0 where Id in (22, 47, 53, 64, 168, 176, 184)
update dbo.Skill set Name='������ �����' where Id=10
update dbo.Skill set Name='�����������' where Id=12
update dbo.Skill set Name='������������ � web-������' where Id=30
update dbo.Skill set Name='��������� �����' where Id=51
update dbo.Skill set Name='����������' where Id=56
update dbo.Skill set Name='������������ �����������' where Id=57
update dbo.Skill set Name='���������' where Id=63
update dbo.Skill set Name='������������ �������' where Id=66
update dbo.Skill set Name='��������� �����' where Id=67
update dbo.Skill set Name='���������' where Id=164
update dbo.Skill set Name='����������' where Id=177
update dbo.Skill set Name='��������' where Id=178

INSERT INTO dbo.Skill(Name, SortOrder, IsActive, NeedText, NeedVocabulary, SkillsGroupId) VALUES ('������', 99999, 'true', 'true', 'false', 1)
INSERT INTO dbo.Skill(Name, SortOrder, IsActive, NeedText, NeedVocabulary, SkillsGroupId) VALUES ('������', 99999, 'true', 'true', 'false', 2)
INSERT INTO dbo.Skill(Name, SortOrder, IsActive, NeedText, NeedVocabulary, SkillsGroupId) VALUES ('������', 99999, 'true', 'true', 'false', 3)
INSERT INTO dbo.Skill(Name, SortOrder, IsActive, NeedText, NeedVocabulary, SkillsGroupId) VALUES ('��������� ����', 0350, 'true', 'false', 'true', 4)
SET @LastInsert = @@IDENTITY
INSERT INTO dbo.SkillVocabulary(Name, IsActive, SkillId, SortOrder) VALUES ('����� � �������� �� ��������', 'true', @LastInsert, 100)
INSERT INTO dbo.SkillVocabulary(Name, IsActive, SkillId, SortOrder) VALUES ('����� � ���� ���������', 'true', @LastInsert, 200)
INSERT INTO dbo.SkillVocabulary(Name, IsActive, SkillId, SortOrder) VALUES ('������ ��������', 'true', @LastInsert, 300)

INSERT INTO dbo.Skill(Name, SortOrder, IsActive, NeedText, NeedVocabulary, SkillsGroupId) VALUES ('������', 99999, 'true', 'true', 'false', 5)
INSERT INTO dbo.Skill(Name, SortOrder, IsActive, NeedText, NeedVocabulary, SkillsGroupId) VALUES ('������', 99999, 'true', 'true', 'false', 6)

update dbo.SkillsGroup set Name='���������� ������' where Id=1