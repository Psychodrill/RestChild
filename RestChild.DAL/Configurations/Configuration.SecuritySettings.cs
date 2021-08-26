using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Comon.Extensions;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Настройки безопасности
        /// </summary>
        private static void SecuritySettings(Context context)
        {
            context.SecuritySetting.AddIfNotExists(new SecuritySetting
            {
                Id = 1,
                Name = "Не указано",
                ValueJson = "20"
            }, s => s.Id == 1);

            context.SecuritySetting.AddIfNotExists(new SecuritySetting
            {
                Id = 2,
                Name = "Не указано",
                ValueJson = "3"
            }, s => s.Id == 2);

            context.SecuritySetting.AddIfNotExists(new SecuritySetting
            {
                Id = 3,
                Name = "Не указано",
                ValueJson = "30"
            }, s => s.Id == 3);

            context.SecuritySetting.AddIfNotExists(new SecuritySetting
            {
                Id = 4,
                Name = "Не указано",
                ValueJson = "365"
            }, s => s.Id == 4);

            context.SecuritySetting.AddIfNotExists(new SecuritySetting
            {
                Id = 5,
                Name = "Не указано",
                ValueJson = "8"
            }, s => s.Id == 5);

            context.SecuritySetting.AddIfNotExists(new SecuritySetting
            {
                Id = 6,
                Name = "Не указано",
                ValueJson = "150"
            }, s => s.Id == 6);

            context.SecuritySetting.AddIfNotExists(new SecuritySetting
            {
                Id = 7,
                Name = "Не указано",
                ValueJson = "90"
            }, s => s.Id == 7);

            SetEidAndLastUpdateTicks(context.SecuritySetting.ToList());
            context.SaveChanges();
        }
    }
}
