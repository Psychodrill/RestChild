DECLARE @LastInsert bigint;

update [dbo].[Skill] set IsActive=0 where Id in (22, 47, 53, 64, 168, 176, 184)
update dbo.Skill set Name='Монтаж видео' where Id=10
update dbo.Skill set Name='Видеосъемка' where Id=12
update dbo.Skill set Name='Компьютерный и web-дизайн' where Id=30
update dbo.Skill set Name='Восточные танцы' where Id=51
update dbo.Skill set Name='Фотография' where Id=56
update dbo.Skill set Name='Классическая хореография' where Id=57
update dbo.Skill set Name='Рисование' where Id=63
update dbo.Skill set Name='Изготовление игрушек' where Id=66
update dbo.Skill set Name='Эстрадный танец' where Id=67
update dbo.Skill set Name='Аккордион' where Id=164
update dbo.Skill set Name='Фортепиано' where Id=177
update dbo.Skill set Name='Саксофон' where Id=178

INSERT INTO dbo.Skill(Name, SortOrder, IsActive, NeedText, NeedVocabulary, SkillsGroupId) VALUES ('Другое', 99999, 'true', 'true', 'false', 1)
INSERT INTO dbo.Skill(Name, SortOrder, IsActive, NeedText, NeedVocabulary, SkillsGroupId) VALUES ('Другое', 99999, 'true', 'true', 'false', 2)
INSERT INTO dbo.Skill(Name, SortOrder, IsActive, NeedText, NeedVocabulary, SkillsGroupId) VALUES ('Другое', 99999, 'true', 'true', 'false', 3)
INSERT INTO dbo.Skill(Name, SortOrder, IsActive, NeedText, NeedVocabulary, SkillsGroupId) VALUES ('Китайский язык', 0350, 'true', 'false', 'true', 4)
SET @LastInsert = @@IDENTITY
INSERT INTO dbo.SkillVocabulary(Name, IsActive, SkillId, SortOrder) VALUES ('Читаю и перевожу со словарем', 'true', @LastInsert, 100)
INSERT INTO dbo.SkillVocabulary(Name, IsActive, SkillId, SortOrder) VALUES ('Читаю и могу объяснить', 'true', @LastInsert, 200)
INSERT INTO dbo.SkillVocabulary(Name, IsActive, SkillId, SortOrder) VALUES ('Владею свободно', 'true', @LastInsert, 300)

INSERT INTO dbo.Skill(Name, SortOrder, IsActive, NeedText, NeedVocabulary, SkillsGroupId) VALUES ('Другое', 99999, 'true', 'true', 'false', 5)
INSERT INTO dbo.Skill(Name, SortOrder, IsActive, NeedText, NeedVocabulary, SkillsGroupId) VALUES ('Другое', 99999, 'true', 'true', 'false', 6)

update dbo.SkillsGroup set Name='Творческие навыки' where Id=1