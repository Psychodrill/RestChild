using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Должности в оздоровительной организации
        /// </summary>
        private static void OrganizationCollaboratorPostType(Context context)
        {
            context.OrganizationCollaboratorPostType.AddOrUpdate(r => r.Id, new OrganizationCollaboratorPostType
                {
                    Id = (long) OrphanageCollaboratorType.Director,
                    Name = "Директор",
                    IsActive = true
                },
                new OrganizationCollaboratorPostType
                {
                    Id = (long) OrphanageCollaboratorType.ResponsibleForRest,
                    Name = "Ответственный за отдых",
                    IsActive = true
                },
                new OrganizationCollaboratorPostType
                {
                    Id = (long) OrphanageCollaboratorType.AdditionalContactPerson,
                    Name = "Дополнительное контактное лицо",
                    IsActive = true
                },
                new OrganizationCollaboratorPostType
                {
                    Id = (long) OrphanageCollaboratorType.Attendant,
                    Name = "Сопровождающий",
                    IsActive = true
                });

            SetEidAndLastUpdateTicks(context.OrganizationCollaboratorPostType.ToList());
            context.SaveChanges();
        }
    }
}
