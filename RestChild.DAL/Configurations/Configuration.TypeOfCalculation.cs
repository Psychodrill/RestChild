using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Виды начисления
        /// </summary>
        /// <param name="context"></param>
        private static void TypeOfCalculation(Context context)
        {
            context.TypeOfCalculation.AddOrUpdate(
                s => s.Id,
                new TypeOfCalculation
                {
                    Id = (long) TypeOfCalculationEnum.AddonPlace,
                    Name = "Дополнительное место",
                    IsActive = true
                }, new TypeOfCalculation
                {
                    Id = (long) TypeOfCalculationEnum.Request,
                    Name = "Заявление",
                    IsActive = true
                }, new TypeOfCalculation
                {
                    Id = (long) TypeOfCalculationEnum.SpecializedCampsChild,
                    Name = "Ребёнок (профильные лагеря)",
                    IsActive = true
                }, new TypeOfCalculation
                {
                    Id = (long) TypeOfCalculationEnum.SpecializedCampsAttendant,
                    Name = "Сопровождающий (профильные лагеря)",
                    IsActive = true
                }, new TypeOfCalculation
                {
                    Id = (long) TypeOfCalculationEnum.Service,
                    Name = "Услуга",
                    IsActive = true
                }, new TypeOfCalculation
                {
                    Id = (long) TypeOfCalculationEnum.Transport,
                    Name = "Транспорт",
                    IsActive = true
                });

            SetEidAndLastUpdateTicks(context.TypeOfCalculation.ToList());
            context.SaveChanges();
        }
    }
}
