using System.Collections.Generic;
using System.Linq;
using iTextSharp.text.io;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Properties;

namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Модель фильтра поиска работника
    /// </summary>
    public class OrphanageCollaboratorsFilterModel : BaseFilterModel<OrphanageCollaboratorsResultListModel>
    {
        public OrphanageCollaboratorsFilterModel()
        {
            Collaborators = new CommonPagedList<OrphanageCollaboratorsResultListModel>(new List<OrphanageCollaboratorsResultListModel>(), 1, Settings.Default.TablePageSize, 0);
            PageNumber = 1;
            Deleted = false;
        }

        public OrphanageCollaboratorsFilterModel(ICollection<OrganisatorCollaborator> collection)
        {
            Collaborators = new CommonPagedList<OrphanageCollaboratorsResultListModel>(collection?.Select(ss => new OrphanageCollaboratorsResultListModel
            {
                Id = ss.Id,
                Name = ss.Applicant?.GetFio(),
                Position = ss.Position?.Name,
                IsDeleted = ss.Applicant?.IsDeleted ?? true
            }).Take(Settings.Default.TablePageSize).ToList() ?? new List<OrphanageCollaboratorsResultListModel>(0), 1, Settings.Default.TablePageSize, collection.Count);
            OrphanageId = collection?.Select(ss => ss.OrganisatonId).FirstOrDefault();
            PageNumber = 1;
        }

        public OrphanageCollaboratorsFilterModel(long orphanageId) : this()
        {
            OrphanageId = orphanageId;
        }

        /// <summary>
        ///     Идентификатор детского дома
        /// </summary>
        public long? OrphanageId { get; set; }

        /// <summary>
        ///     ФИО работника
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Сотрудники
        /// </summary>
        public CommonPagedList<OrphanageCollaboratorsResultListModel> Collaborators { get; set; }

        /// <summary>
        ///     Включая удаленных
        /// </summary>
        public bool Deleted { get; set; }
    }
}
