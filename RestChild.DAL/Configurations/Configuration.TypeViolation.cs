using System;
using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Виды нарушений
        /// </summary>
        private static void TypeViolation(Context context)
        {
            var yearId = context.YearOfRest.OrderBy(y => y.Year).FirstOrDefault()?.Id ?? 1;

            context.TypeViolation.AddOrUpdate(r => r.Id, new TypeViolation
                {
                    Id = 1,
                    Name =
                        "п. 4.1 Порядка Приказа: Привлечение отдыхающих и сопровождающих лиц в прошедшем и (или) текущем календарном году к ответственности в связи с нарушениями законодательства в период их отдыха и оздоровления;",
                    LastUpdateTick = DateTime.Now.Ticks,
                    YearOfRestId = yearId,
                    Eid = 1
                }, new TypeViolation
                {
                    Id = 2,
                    Name =
                        "п. 4.2 Порядка Приказа: Нарушение отдыхающими и сопровождающими лицами в прошедшем и (или) текущем календарном году правил перевозок пассажиров, правил отдыха и оздоровления, правил пребывания в организациях отдыха и оздоровления в период отдыха и оздоровления.",
                    Eid = 2,
                    YearOfRestId = yearId,
                    LastUpdateTick = DateTime.Now.Ticks
                }, new TypeViolation
                {
                    Id = 3,
                    Name =
                        "п. 4.3 Порядка Приказа: Неосуществление без уважительных причин отдыха и оздоровления на основании путевки на отдых и оздоровление, выданной в прошедшем и (или) текущем календарном году.",
                    Eid = 3,
                    LastUpdateTick = DateTime.Now.Ticks,
                    YearOfRestId = yearId
                }, new TypeViolation
                {
                    Id = 4,
                    Name =
                        "пункт 9.1.8. Порядка: Наличие сведений о нарушениях правил отдыха и оздоровления в период отдыха и оздоровления в текущем календарном году ребёнком, сопровождающим лицом (в случае организации совместного выездного отдыха), лицом из числа детей-сирот и детей, оставшихся без попечения родителей.",
                    LastUpdateTick = DateTime.Now.Ticks,
                    Eid = 4
                }, new TypeViolation
                {
                    Id = 5,
                    Name =
                        "пункт 9.1.9. Порядка: Наличие сведений о нарушениях сопровождающим лицом в текущем календарном году обязательств, предусмотренных соглашением об осуществлении обязанностей по сопровождению детей во время отдыха и оздоровления родителем или иным законным представителем либо доверенным лицом для сопровождения во время отдыха и оздоровления, заключенным с ГАУК \"Мосгортур\".",
                    Eid = 5,
                    LastUpdateTick = DateTime.Now.Ticks
                }, new TypeViolation
                {
                    Id = 6,
                    Name =
                        "пункт 9.1.10. Порядка: Наличие сведений о неосуществлении отдыха и оздоровления без уважительных причин, указанных в пункте 10.1.2 Порядка, на основании предоставленной в текущем календарном году путевки для отдыха и оздоровления с оплатой стоимости путевки для отдыха и оздоровления за счет средств бюджета города Москвы.",
                    Eid = 6,
                    LastUpdateTick = DateTime.Now.Ticks
                }, new TypeViolation
                {
                    Id = 7,
                    Name =
                        "пункт 9.1.11. Порядка: Наличие сведений о нарушении условий использования сертификата на отдых и оздоровление, установленных пунктами 8(1).1-8(1).4 настоящего Порядка.",
                    Eid = 7,
                    LastUpdateTick = DateTime.Now.Ticks
                }
            );

            SetEidAndLastUpdateTicks(context.TypeViolation.ToList());
            context.SaveChanges();
        }
    }
}
