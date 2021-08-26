using System.Collections.Generic;
using System.Linq;
using RestChild.Domain;

namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Модель детского дома
    /// </summary>
    public class OrphanageModel : ViewModelBase<Organization>
    {
        public OrphanageModel() : base(new Organization())
        {
            Data.Orphanage = true;
            OrphanageAddress = new Dictionary<string, OrphanageAddress>();
        }

        public OrphanageModel(Organization organization) : base(organization)
        {
            OrphanageAddress = Data?.OrphanageOrganizationAddresses?.ToDictionary(x => x.Id.ToString(), x => x) ??
                               new Dictionary<string, OrphanageAddress>
                                   {{"0", new OrphanageAddress {Address = new Address()}}};
            if(OrphanageAddress.Count < 1)
            {
                OrphanageAddress["0"] = new OrphanageAddress { Address = new Address() };
            }

            Collaborators = Data?.OrganisatonCollaborators?.Where(ss => !ss.Applicant.IsDeleted && (ss.EntityId == null || ss.EntityId == ss.Id)).OrderBy(ss => ss.Applicant.LastName).ToDictionary(sx => sx.Id.ToString(), sa => sa);
            Pupils = Data?.OrphanageOrganizationAddresses?.SelectMany(ss => ss.Pupils).Where(ss => ss.Child.EntityId == null || ss.Child.EntityId == ss.Child.Id).OrderBy(ss => ss.Child.LastName).ToDictionary(sx => sx.Id.ToString(), sa => sa);
            Data.Orphanage = true;
        }

        /// <summary>
        ///     Активная вкладка
        /// </summary>
        public string ActiveTab { get; set; }

        /// <summary>
        ///     ссылка на историю
        /// </summary>
        public long? HistoryLinkId => Data?.HistoryLinkId;

        /// <summary>
        ///     Адреса детского дома
        /// </summary>
        public Dictionary<string, OrphanageAddress> OrphanageAddress { get; set; }

        /// <summary>
        ///     Cотрудники
        /// </summary>
        public Dictionary<string, OrganisatorCollaborator> Collaborators { get; set; }

        /// <summary>
        ///     Воспитанники
        /// </summary>
        public Dictionary<string, Pupil> Pupils { get; set; }

        /// <summary>
        ///     Статусная модель (на будущее)
        /// </summary>
        public ViewModelState State { get; set; }

        /// <summary>
        ///     Группы (потребности)
        /// </summary>
        public OrphanageGroupsFilterModel Groups { get; set; }

        /// <summary>
        ///     Группы (списки)
        /// </summary>
        public OrphanagePupilGroupListFilterModel Lists { get; set; }

        /// <summary>
        ///     построение данных
        /// </summary>
        public override Organization BuildData()
        {
            var data = base.BuildData();

            foreach (var key in OrphanageAddress
                                    ?.Where(ss =>
                                        ss.Value.Address == null || string.IsNullOrWhiteSpace(ss.Value.Address.Name) ||
                                        string.IsNullOrWhiteSpace(ss.Value.Address.FiasId)).Select(ss => ss.Key)
                                    .ToList() ?? new List<string>())
            {
                OrphanageAddress.Remove(key);
            }

            data.OrphanageOrganizationAddresses = OrphanageAddress?.Values.ToList() ?? new List<OrphanageAddress>();
            data.OrganisatonCollaborators = Collaborators?.Values.ToList() ?? new List<OrganisatorCollaborator>();
            return data;
        }
    }
}
