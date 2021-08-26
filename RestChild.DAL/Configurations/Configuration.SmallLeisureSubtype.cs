using System;
using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Сведения о малых формах досуга (занятости) детей
        /// </summary>
        private static void SmallLeisureSubtype(Context context)
        {
            if (context.SmallLeisureSubtype.Any())
            {
                return;
            }

            context.SmallLeisureType.AddOrUpdate(r => r.Id,
                new SmallLeisureType
                {
                    Id = 1,
                    Name = "Спортивные мероприятия",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    MergeSubtypes = false
                },
                new SmallLeisureType
                {
                    Id = 2,
                    Name = "Туристические мероприятия",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    MergeSubtypes = false
                },
                new SmallLeisureType
                {
                    Id = 3,
                    Name = "Фестивали и акции",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    MergeSubtypes = false
                },
                new SmallLeisureType
                {
                    Id = 4,
                    Name = "Трудовая деятельность",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    MergeSubtypes = false
                },
                new SmallLeisureType
                {
                    Id = 5,
                    Name = "Волонтерская деятельность",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    MergeSubtypes = false
                },
                new SmallLeisureType
                {
                    Id = 6,
                    Name = "Досуговая деятельность",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    MergeSubtypes = false
                },
                new SmallLeisureType
                {
                    Id = 7,
                    Name = "Профилактическая деятельность",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    MergeSubtypes = false
                },
                new SmallLeisureType
                {
                    Id = 8,
                    Name = "Иное",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    MergeSubtypes = false
                },
                new SmallLeisureType
                {
                    Id = 9,
                    Name = "ИТОГО",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    MergeSubtypes = false,
                    Formula = "=SUM(C{0}:U{0})"
                },
                new SmallLeisureType
                {
                    Id = 10,
                    Name = "Примечание",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    MergeSubtypes = false,
                    IsTextData = true
                }
            );

            context.SaveChanges();

            context.SmallLeisureSubtype.AddOrUpdate(r => r.Id,
                new SmallLeisureSubtype
                {
                    Id = 1,
                    Name = "походы",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    SmallLeisureTypeId = 2
                },
                new SmallLeisureSubtype
                {
                    Id = 2,
                    Name = "слеты",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    SmallLeisureTypeId = 2
                },
                new SmallLeisureSubtype
                {
                    Id = 3,
                    Name = "иные",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    SmallLeisureTypeId = 2
                },
                new SmallLeisureSubtype
                {
                    Id = 4,
                    Name = "творческие",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    SmallLeisureTypeId = 3
                },
                new SmallLeisureSubtype
                {
                    Id = 5,
                    Name = "культурно-просветительские",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    SmallLeisureTypeId = 3
                },
                new SmallLeisureSubtype
                {
                    Id = 6,
                    Name = "военно-патриотические",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    SmallLeisureTypeId = 3
                },
                new SmallLeisureSubtype
                {
                    Id = 7,
                    Name = "иные",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    SmallLeisureTypeId = 3
                },
                new SmallLeisureSubtype
                {
                    Id = 8,
                    Name = "трудовые объединения, бригады",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    SmallLeisureTypeId = 4
                },
                new SmallLeisureSubtype
                {
                    Id = 9,
                    Name = "временное трудоустройство",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    SmallLeisureTypeId = 4
                },
                new SmallLeisureSubtype
                {
                    Id = 10,
                    Name = "иные",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    SmallLeisureTypeId = 4
                },
                new SmallLeisureSubtype
                {
                    Id = 11,
                    Name = "дворцовые площадки",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    SmallLeisureTypeId = 6
                },
                new SmallLeisureSubtype
                {
                    Id = 12,
                    Name = "клубная работа (кружки, секции)",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    SmallLeisureTypeId = 6
                },
                new SmallLeisureSubtype
                {
                    Id = 13,
                    Name = "мастер-классы",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    SmallLeisureTypeId = 6
                },
                new SmallLeisureSubtype
                {
                    Id = 14,
                    Name = "технопарки",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    SmallLeisureTypeId = 6
                },
                new SmallLeisureSubtype
                {
                    Id = 15,
                    Name = "иные",
                    LastUpdateTick = DateTime.Now.Ticks,
                    IsActive = true,
                    SmallLeisureTypeId = 6
                });
        }
    }
}
