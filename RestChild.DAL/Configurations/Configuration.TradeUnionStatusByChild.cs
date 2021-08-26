using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Профсоюзный список. Статусы по отношению к ребёнку
        /// </summary>
        private static void TradeUnionStatusByChild(Context context)
        {
            context.TradeUnionStatusByChild.AddOrUpdate(r => r.Id,
                new TradeUnionStatusByChild
                {
                    Id = 1,
                    Name = "Мать"
                },
                new TradeUnionStatusByChild
                {
                    Id = 2,
                    Name = "Отец"
                },
                new TradeUnionStatusByChild
                {
                    Id = 3,
                    Name = "Бабушка"
                },
                new TradeUnionStatusByChild
                {
                    Id = 4,
                    Name = "Дедушка"
                },
                new TradeUnionStatusByChild
                {
                    Id = 5,
                    Name = "Сестра"
                },
                new TradeUnionStatusByChild
                {
                    Id = 6,
                    Name = "Брат"
                },
                new TradeUnionStatusByChild
                {
                    Id = 7,
                    Name = "Дядя"
                },
                new TradeUnionStatusByChild
                {
                    Id = 8,
                    Name = "Тетя"
                },
                new TradeUnionStatusByChild
                {
                    Id = 9,
                    Name = "Опекун"
                },
                new TradeUnionStatusByChild
                {
                    Id = 10,
                    Name = "Приемный/патронатный родитель"
                });


            SetEidAndLastUpdateTicks(context.TradeUnionStatusByChild.ToList());
            context.SaveChanges();
        }
    }
}
