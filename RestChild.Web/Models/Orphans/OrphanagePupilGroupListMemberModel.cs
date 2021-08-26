using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Модель воспитанника в списке
    /// </summary>
    public class OrphanagePupilGroupListMemberModel : ViewModelBase<PupilGroupListMember>
    {
        public OrphanagePupilGroupListMemberModel() : base (new PupilGroupListMember())
        {
            DrugDoses = new Dictionary<string, PupilGroupListMemberDrugDose>();
        }

        public OrphanagePupilGroupListMemberModel(PupilGroupListMember pupilGroupListMember) : base (pupilGroupListMember)
        {
            DrugDoses = pupilGroupListMember.GroupPupilDoses?.ToDictionary(ss => ss.Id.ToString(), ss => ss);
        }

        /// <summary>
        ///     Наркотики
        /// </summary>
        public IDictionary<string, PupilGroupListMemberDrugDose> DrugDoses { get; set; }

        public override PupilGroupListMember BuildData()
        {
            var res = base.BuildData();
            res.GroupPupilDoses = DrugDoses?.Values.ToList() ?? new List<PupilGroupListMemberDrugDose>(0);
            return res;
        }

    }
}
