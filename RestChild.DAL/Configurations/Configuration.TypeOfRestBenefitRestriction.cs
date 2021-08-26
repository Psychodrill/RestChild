using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Ограничения по льготе, виду отдыха
        /// </summary>
        private static void TypeOfRestBenefitRestriction(Context context)
        {
            var id = 0;
            context.TypeOfRestBenefitRestriction.AddOrUpdate(s => s.LastUpdateTick
                , new TypeOfRestBenefitRestriction
                {
                    BenefitTypeId = 19,
                    MinAge = 3,
                    MaxAge = 7,
                    TypeOfRestId = 10,
                    LastUpdateTick = ++id
                }, new TypeOfRestBenefitRestriction
                {
                    BenefitTypeId = 20,
                    MinAge = 4,
                    MaxAge = 16,
                    TypeOfRestId = 10,
                    LastUpdateTick = ++id
                }, new TypeOfRestBenefitRestriction
                {
                    BenefitTypeId = 21,
                    MinAge = 3,
                    MaxAge = 17,
                    TypeOfRestId = 10,
                    LastUpdateTick = ++id
                }, new TypeOfRestBenefitRestriction
                {
                    BenefitTypeId = 22,
                    MinAge = 3,
                    MaxAge = 17,
                    TypeOfRestId = 10,
                    LastUpdateTick = ++id
                }, new TypeOfRestBenefitRestriction
                {
                    BenefitTypeId = 46,
                    MinAge = 3,
                    MaxAge = 7,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsComplex,
                    LastUpdateTick = ++id
                }
            );

            context.SaveChanges();
        }
    }
}
