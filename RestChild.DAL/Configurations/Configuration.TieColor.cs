using System;
using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Цвета галстука
        /// </summary>
        private static void TieColor(Context context)
        {
            if (!context.TieColor.Any(t => t.Id == 0))
            {
                context.Database.ExecuteSqlCommand("DELETE FROM TieColor");
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT (TieColor, RESEED, -1)");
                context.Database.ExecuteSqlCommand("INSERT INTO TieColor(Name, IsActive) VALUES('Оранжевый галстук', 'true')");
                context.Database.ExecuteSqlCommand("INSERT INTO TieColor(Name, IsActive) VALUES('Оранжевый галстук и синий значок', 'true')");
                context.Database.ExecuteSqlCommand("INSERT INTO TieColor(Name, IsActive) VALUES('Оранжевый галстук и 2 синих значка', 'true')");
                context.Database.ExecuteSqlCommand("INSERT INTO TieColor(Name, IsActive) VALUES('Синий галстук', 'true')");
                context.Database.ExecuteSqlCommand("INSERT INTO TieColor(Name, IsActive) VALUES('Синий галстук и зеленый значок', 'true')");
                context.Database.ExecuteSqlCommand("INSERT INTO TieColor(Name, IsActive) VALUES('Синий галстук и 2 зеленых значка', 'true')");
                context.Database.ExecuteSqlCommand("INSERT INTO TieColor(Name, IsActive) VALUES('Синий галстук и 3 зеленых значка', 'true')");
                context.Database.ExecuteSqlCommand("INSERT INTO TieColor(Name, IsActive) VALUES('Синий галстук и 4 зеленых значка', 'true')");
                context.Database.ExecuteSqlCommand("INSERT INTO TieColor(Name, IsActive) VALUES('Зеленый галстук', 'true')");
            }

            context.TieColor.AddOrUpdate(c => c.Id
                , new TieColor
                {
                    Id = 0,
                    Eid = 0,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Оранжевый галстук",
                    IsActive = true,
                    Raiting = 30
                }
                , new TieColor
                {
                    Id = 1,
                    Eid = 1,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Оранжевый галстук и синий значок",
                    IsActive = true,
                    Raiting = 40
                }
                , new TieColor
                {
                    Id = 2,
                    Eid = 2,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Оранжевый галстук и 2 синих значка",
                    IsActive = true,
                    Raiting = 50
                }
                , new TieColor
                {
                    Id = 3,
                    Eid = 3,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Синий галстук",
                    IsActive = true,
                    Raiting = 70
                }
                , new TieColor
                {
                    Id = 4,
                    Eid = 4,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Синий галстук и зеленый значок",
                    IsActive = true,
                    Raiting = 85
                }
                , new TieColor
                {
                    Id = 5,
                    Eid = 5,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Синий галстук и 2 зеленых значка",
                    IsActive = true,
                    Raiting = 100
                }
                , new TieColor
                {
                    Id = 6,
                    Eid = 6,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Синий галстук и 3 зеленых значка",
                    IsActive = false,
                    Raiting = 100
                }
                , new TieColor
                {
                    Id = 7,
                    Eid = 7,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Синий галстук и 4 зеленых значка",
                    IsActive = false,
                    Raiting = 100
                }
                , new TieColor
                {
                    Id = 8,
                    Eid = 8,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Зеленый галстук",
                    IsActive = true,
                    Raiting = 130
                }
            );
        }
    }
}
