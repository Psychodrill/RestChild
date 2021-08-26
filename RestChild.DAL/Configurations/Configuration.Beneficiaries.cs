using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Кто обладает льготой
        /// </summary>
        private static void Beneficiaries(Context context)
        {
            context.Beneficiaries.AddOrUpdate(r => r.Id, new Beneficiaries
            {
                Id = (long) BeneficiariesEnum.Child,
                Name = "Ребёнок"
            }, new Beneficiaries
            {
                Id = (long) BeneficiariesEnum.Applicant,
                Name = "Заявитель"
            }, new Beneficiaries
            {
                Id = (long) BeneficiariesEnum.SecondParent,
                Name = "Второй родитель"
            });

            SetEidAndLastUpdateTicks(context.Beneficiaries.ToList());
            context.SaveChanges();
        }
    }
}
