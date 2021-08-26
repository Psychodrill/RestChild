using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    [DataContract]
    public class CoworkersFilterModel
    {
        public CoworkersFilterModel()
        {
            PageNumber = 1;
        }

        [DataMember] public bool NotNessary { get; set; }

        [DataMember] public long? ParentTaskId { get; set; }

        [DataMember] public long? HotelTypeId { get; set; }

        [DataMember] public long? HotelId { get; set; }

        [DataMember] public long? YearOfRestId { get; set; }

        [DataMember] public long? GroupedTimeOfRestId { get; set; }

        [DataMember] public string Name { get; set; }

        [DataMember] public long? SubjectOfRestId { get; set; }

        [DataMember] public long? CoworkerType { get; set; }

        [IgnoreDataMember] public CommonPagedList<IGrouping<Bout, Coworker>> Result { get; set; }

        [DataMember] public int PageNumber { get; set; }

        /// <summary>
        ///     Начальная страница
        /// </summary>
        [DataMember]
        public int PageStart { get; set; }

        /// <summary>
        ///     Последняя страница в пагинаторе
        /// </summary>
        [DataMember]
        public int PageEnd { get; set; }

        /// <summary>
        ///     Последняя страница
        /// </summary>
        [DataMember]
        public int PageLast { get; set; }

        #region

        [IgnoreDataMember] public Hotels Hotels { get; set; }

        [IgnoreDataMember] public List<HotelType> HotelTypes { get; set; }

        [IgnoreDataMember] public List<YearOfRest> YearsOfRest { get; set; }

        [IgnoreDataMember] public List<GroupedTimeOfRest> GroupedTimeOfRest { get; set; }

        [IgnoreDataMember] public List<SubjectOfRest> SubjectsOfRest { get; set; }

        #endregion
    }
}
