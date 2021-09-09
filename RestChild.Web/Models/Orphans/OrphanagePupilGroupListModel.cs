using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Модель cписока/группы отправки
    /// </summary>
    public class OrphanagePupilGroupListModel : ViewModelBase<ListOfChilds>
    {
        public OrphanagePupilGroupListModel() : this(new ListOfChilds())
        {

        }

        public OrphanagePupilGroupListModel(ListOfChilds list) : base(list)
        {
            Pupils = list?.GroupPupils?.ToDictionary(ss => ss.Id.ToString(), sx => new OrphanagePupilGroupListMemberModel(sx)) ?? new Dictionary<string, OrphanagePupilGroupListMemberModel>();
            Collaborators = list?.GroupCollaborators?.ToDictionary(ss => ss.Id.ToString(), sx => sx) ?? new Dictionary<string, PupilGroupListCollaborator>();
            Transfers = list?.GroupTransfers?.ToDictionary(ss => ss.Id.ToString(), sx => sx) ?? new Dictionary<string, PupilGroupListTransfer>();
        }

        /// <summary>
        ///     Воспитанники
        /// </summary>
        public IDictionary<string, OrphanagePupilGroupListMemberModel> Pupils { get; set; }

        /// <summary>
        ///     Сопровождающие
        /// </summary>
        public IDictionary<string, PupilGroupListCollaborator> Collaborators { get; set; }

        /// <summary>
        ///     Трансфер
        /// </summary>
        public IDictionary<string, PupilGroupListTransfer> Transfers { get; set; }

        /// <summary>
        ///     Статус
        /// </summary>
        public ViewModelState State { get; set; }

        /// <summary>
        ///     Статус в который будет переведено
        /// </summary>
        public string StateMachineActionString { get; set; }

        /// <summary>
        ///     Активная вкладка
        /// </summary>
        public string ActiveTab { get; set; }

        /// <summary>
        ///     Ссылка на сформированную потребность группы
        /// </summary>
        public long PupilGroupRequestId { get; set; }

        /// <summary>
        ///     Ссылка на организацию (приют)
        /// </summary>
        public long OrganizationId { get; set; }

        /// <summary>
        ///     Наименование года
        /// </summary>
        public string YearOfRestName { get; set; }

        /// <summary>
        ///     Наименование группы (потребности)
        /// </summary>
        public string PupilGroupName { get; set; }

        /// <summary>
        ///     Наименование формы отдыха и оздоровления
        /// </summary>
        public string FormOfRestName { get; set; }

        /// <summary>
        ///     Наименование периода отдыха
        /// </summary>
        public string TimeOfRestName { get; set; }

        /// <summary>
        ///     Кол-во воспитанников
        /// </summary>
        public int PupilsCount { get; set; }

        /// <summary>
        ///     Кол-во сопровождающих от учреждения
        /// </summary>
        public int CollaboratorsCount { get; set; }

        /// <summary>
        ///     Кол-во сопровождающих от МГТ
        /// </summary>
        public int MGTCollaboratorsCount { get; set; }

        public override ListOfChilds BuildData()
        {
            var res = base.BuildData();
            res.GroupCollaborators = Collaborators?.Values.ToList() ?? new List<PupilGroupListCollaborator>();
            res.GroupTransfers = Transfers?.Values.Where(ss => ss.CountPeople >= 0).ToList() ?? new List<PupilGroupListTransfer>();
            res.GroupPupils = Pupils?.Values.Select(ss => ss.BuildData()).ToList() ?? new List<PupilGroupListMember>();
            res.Childs = res.GroupPupils.Select(ss => ss.Pupil?.Child).ToList();
            res.Attendants = res.GroupCollaborators.Select(ss => ss.OrganisatonCollaborator?.Applicant).ToList();

            res.CountChild = res.Childs?.Count() ?? 0;
            res.CountAttendants = res.Attendants?.Count() ?? 0;
            return res;
        }
    }
}
