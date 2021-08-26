using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Типы персон в заявлении на визит в МГТ
        /// </summary>
        /// <param name="context"></param>
        private static void MgtVisitBookingPersonType(Context context)
        {
            context.MGTVisitBookingPersonType.AddOrUpdate(s => s.Id, new MGTVisitBookingPersonType
            {
                Id = (long) MGTVisitBookingPersonTypes.Declarant,
                Code = "declarant",
                Name = "Заявитель"
            }, new MGTVisitBookingPersonType
            {
                Id = (long) MGTVisitBookingPersonTypes.Child,
                Code = "child",
                Name = "Ребёнок"
            });

            SetEidAndLastUpdateTicks(context.MGTVisitBookingPersonType.ToList());
            context.SaveChanges();
        }
    }
}
