using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Виды и подвиды ограничения
        /// </summary>
        private static void TypeOfRestriction(Context context)
        {
            var maxId = context.TypeOfRestriction.Select(s => s.Id).DefaultIfEmpty().Max();
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT (TypeOfRestriction, RESEED, {maxId})");
            context.TypeOfRestriction.AddOrUpdate(r => r.Id,
                new TypeOfRestriction
                {
                    Id = 1,
                    Name = "Нарушения статодинамической функции (двигательной)",
                    IsActive = false,
                    Eid = 1
                },
                new TypeOfRestriction
                {
                    Id = 2,
                    Name =
                        "Нарушения функций кровообращения, дыхания, пищеварения, выделения, обмена веществ и энергии, внутренней секреции и др.",
                    IsActive = false,
                    Eid = 2
                },
                new TypeOfRestriction
                {
                    Id = 3,
                    Name = "Сенсорные (зрения, слуха, обоняния, осязания и др.)",
                    IsActive = false,
                    Eid = 3
                },
                new TypeOfRestriction
                {
                    Id = 4,
                    Name = "Ментальные (восприятия, внимания, памяти, мышления, речи, эмоций, воли и др.)",
                    IsActive = false,
                    Eid = 4
                },
                new TypeOfRestriction
                {
                    Id = 5,
                    Name =
                        "Нарушения опорно-двигательного аппарата (передвигается с помощью инвалидного кресла-коляски)",
                    IsActive = false,
                    Eid = 5
                },
                new TypeOfRestriction
                {
                    Id = 6,
                    Name =
                        "Нарушения опорно-двигательного аппарата (передвигается самостоятельно или с опорой: ходунки, трость, краб)",
                    IsActive = false,
                    Eid = 6
                },
                new TypeOfRestriction
                {
                    Id = 7,
                    Name =
                        "Нарушения опорно-двигательного аппарата",
                    IsActive = true,
                    Eid = 7
                },
                new TypeOfRestriction
                {
                    Id = 8,
                    Name =
                        "Сенсорные нарушения",
                    IsActive = true,
                    Eid = 8
                },
                new TypeOfRestriction
                {
                    Id = 9,
                    Name =
                        "Ментальные, психические и неврологические нарушения",
                    IsActive = true,
                    Eid = 9
                },
                new TypeOfRestriction
                {
                    Id = 10,
                    Name =
                        "Нарушения функций систем организма",
                    IsActive = true,
                    Eid = 10
                },
                new TypeOfRestriction
                {
                    Id = 11,
                    Name =
                        "Дети, имеющие заключение ЦПМПК города Москвы (адаптированная основная общеобразовательная программа)",
                    IsActive = false,
                    Eid = 11
                });

            context.SaveChanges();

            context.TypeOfSubRestriction.AddOrUpdate(new TypeOfSubRestriction
                {
                    Id = 1,
                    Name = "Передвигается с помощью инвалидного кресла-коляски",
                    Eid = 1,
                    TypeOfRestrictionId = 7,
                    RestrictionGroupId = (long) RestrictionGroupEnum.WithAnAccessibleEnvironment
                },
                new TypeOfSubRestriction
                {
                    Id = 2,
                    Name = "Передвигается самостоятельно или с опорой (ходунки, трость, краб)",
                    Eid = 2,
                    TypeOfRestrictionId = 7,
                    RestrictionGroupId = (long) RestrictionGroupEnum.PartiallyAccessible
                },
                new TypeOfSubRestriction
                {
                    Id = 3,
                    Name = "Передвигается на небольшие расстояния самостоятельно или с опорой",
                    Eid = 3,
                    TypeOfRestrictionId = 7,
                    RestrictionGroupId = (long) RestrictionGroupEnum.PartiallyAccessible
                },
                new TypeOfSubRestriction
                {
                    Id = 4,
                    Name = "Слабовидящие",
                    Eid = 4,
                    TypeOfRestrictionId = 8
                },
                new TypeOfSubRestriction
                {
                    Id = 5,
                    Name = "Слепые",
                    Eid = 5,
                    TypeOfRestrictionId = 8,
                    RestrictionGroupId = (long) RestrictionGroupEnum.PartiallyAccessible
                },
                new TypeOfSubRestriction
                {
                    Id = 6,
                    Name = "Слабослышащие",
                    Eid = 6,
                    TypeOfRestrictionId = 8
                },
                new TypeOfSubRestriction
                {
                    Id = 7,
                    Name = "Глухие",
                    Eid = 7,
                    TypeOfRestrictionId = 8
                },
                new TypeOfSubRestriction
                {
                    Id = 8,
                    Name = "Слепоглухие",
                    Eid = 8,
                    TypeOfRestrictionId = 8,
                    RestrictionGroupId = (long) RestrictionGroupEnum.PartiallyAccessible
                },
                new TypeOfSubRestriction
                {
                    Id = 9,
                    Name = "Снижение интеллекта",
                    Eid = 9,
                    TypeOfRestrictionId = 9
                },
                new TypeOfSubRestriction
                {
                    Id = 10,
                    Name = "Нарушения поведения и общения",
                    Eid = 10,
                    TypeOfRestrictionId = 9
                },
                new TypeOfSubRestriction
                {
                    Id = 11,
                    Name = "Эпилепсия",
                    Eid = 11,
                    TypeOfRestrictionId = 9
                },
                new TypeOfSubRestriction
                {
                    Id = 12,
                    Name = "Нарушения функций кровообращения (в том числе гемофилия)",
                    Eid = 12,
                    TypeOfRestrictionId = 10
                },
                new TypeOfSubRestriction
                {
                    Id = 13,
                    Name = "Нарушения функций пищеварения (в том числе панкреатит)",
                    Eid = 13,
                    TypeOfRestrictionId = 10
                },
                new TypeOfSubRestriction
                {
                    Id = 14,
                    Name = "Нарушения функций эндокринной системы и метаболизма (в том числе сахарный диабет)",
                    Eid = 14,
                    TypeOfRestrictionId = 10
                },
                new TypeOfSubRestriction
                {
                    Id = 15,
                    Name = "Нарушения функций дыхательной системы (в том числе астма)",
                    Eid = 15,
                    TypeOfRestrictionId = 10
                },
                new TypeOfSubRestriction
                {
                    Id = 16,
                    Name = "Нарушения функций иммунной системы (в том числе ВИЧ, СПИД)",
                    Eid = 16,
                    TypeOfRestrictionId = 10
                },
                new TypeOfSubRestriction
                {
                    Id = 17,
                    Name = "Онкологические заболевания",
                    Eid = 17,
                    TypeOfRestrictionId = 10
                });

            context.SaveChanges();

            SetEidAndLastUpdateTicks(context.TypeOfSubRestriction.ToList());
            context.SaveChanges();
        }
    }
}
