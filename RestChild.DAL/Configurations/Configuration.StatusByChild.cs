using System.Data.Entity.Migrations;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Статусы по отношению к ребенку
        /// </summary>
        /// <param name="context"></param>
        private static void StatusByChild(Context context)
        {
            context.StatusByChild.AddOrUpdate(s => s.Id,
                new StatusByChild {Id = 1, Name = "Мать", IsActive = true, ForAgent = true},
                new StatusByChild {Id = 2, Name = "Отец", IsActive = true, ForAgent = true},
                new StatusByChild {Id = 3, Name = "Опекун", IsActive = true},
                new StatusByChild {Id = 4, Name = "Попечитель", IsActive = true},
                new StatusByChild {Id = 5, Name = "Приемный родитель", IsActive = true},
                new StatusByChild {Id = 6, Name = "Патронатный воспитатель", IsActive = true},
                new StatusByChild {Id = 7, Name = "Бабушка"},
                new StatusByChild {Id = 8, Name = "Дедушка"},
                new StatusByChild {Id = 9, Name = "Совершеннолетний полнородный (неполнородный) брат"},
                new StatusByChild {Id = 10, Name = "Совершеннолетняя полнородная (неполнородная) сестра"},
                new StatusByChild {Id = 11, Name = "Дядя"},
                new StatusByChild {Id = 12, Name = "Тетя"});
        }
    }
}
