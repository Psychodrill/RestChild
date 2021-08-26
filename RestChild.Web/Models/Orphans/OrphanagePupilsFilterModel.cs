using System.Collections.Generic;
using System.Linq;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Properties;

namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Модель фильтра поиска воспитанника
    /// </summary>
    public class OrphanagePupilsFilterModel
    {
        public OrphanagePupilsFilterModel()
        {
            Result = new CommonPagedList<OrphanagePupilsResultListModel>(new List<OrphanagePupilsResultListModel>(0), 1,
                Settings.Default.TablePageSize, 0);
            PageNumber = 1;
            IsFilled = false;
        }

        public OrphanagePupilsFilterModel(long orphanageId) : this()
        {
            OrphanageId = orphanageId;
        }

        public OrphanagePupilsFilterModel(ICollection<Pupil> collection)
        {
            Result = new CommonPagedList<OrphanagePupilsResultListModel>(
                collection?.Select(ss => new OrphanagePupilsResultListModel
                {
                    Id = ss.Id,
                    Name = ss.Child.GetFio(),
                    DateOfBirth = ss.Child?.DateOfBirth
                }).Take(Settings.Default.TablePageSize).ToList() ?? new List<OrphanagePupilsResultListModel>(0),
                1, Settings.Default.TablePageSize, collection.Count());

            OrphanageId = collection?.Where(ss => ss.OrphanageAddress != null).Select(ss => ss.OrphanageAddress.OrganisationId).FirstOrDefault();
            PageNumber = 1;
            IsInGroup = true;
            IsFilled = false;
        }

        public OrphanagePupilsFilterModel(PupilGroup group) : this(group.Pupils ?? new List<Pupil>(0))
        {
            GroupId = group.Id;
            OrphanageId = group.OrganizationId;
            CurrentState = group.StateId;
        }

        public OrphanagePupilsFilterModel(ListOfChilds list) : this(list.GroupPupils?.Select(ss => ss.Pupil).ToList() ?? new List<Pupil>(0))
        {
            ListId = list.Id;
            //OrphanageId = list?.PupilGroupRequest?.PupilGroup?.OrganizationId;
            CurrentState = list.StateId;
        }

        /// <summary>
        ///     Идентификатор группы
        /// </summary>
        public long? GroupId { get; set; }

        /// <summary>
        ///     Входит в указанную группу
        /// </summary>
        public bool IsInGroup { get; set; }


        /// <summary>
        ///     Идентификатор списка
        /// </summary>
        public long? ListId { get; set; }


        /// <summary>
        ///     Идентификатор детского дома
        /// </summary>
        public long? OrphanageId { get; set; }

        /// <summary>
        ///     ФИО воспитанника
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Возраст от
        /// </summary>
        public int? AgeFrom { get; set; }

        /// <summary>
        ///     Возраст до
        /// </summary>
        public int? AgeTo { get; set; }

        /// <summary>
        ///     Пол
        /// </summary>
        public bool? IsMale { get; set; }

        /// <summary>
        ///     Не включен в группы
        /// </summary>
        public bool IsNotInGroup { get; set; }

        /// <summary>
        ///     Включая выбивших
        /// </summary>
        public bool IncludeOut { get; set; }

        /// <summary>
        ///     Результат поиска
        /// </summary>
        public CommonPagedList<OrphanagePupilsResultListModel> Result { get; set; }

        /// <summary>
        ///     Текущий статус
        /// </summary>
        public long? CurrentState { get; set; }

        /// <summary>
        ///     Номер страницы
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        ///     Action
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        ///     Только заполненные
        /// </summary>
        public bool IsFilled { get; set; }
    }
}
