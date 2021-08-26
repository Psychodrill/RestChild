using System;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto
{
    [Serializable]
    [DataContract]
    public class ResidentPreferentialCategories
    {
        [DataMember] public string Preferentical { get; set; }

        [DataMember] public string PreferenticalCode { get; set; }

        [DataMember] public DateTime? StartDate { get; set; }

        [DataMember] public DateTime? EndDate { get; set; }

        [DataMember] public string StartReasonName { get; set; }

        [DataMember] public string StartReasonSeries { get; set; }

        [DataMember] public string StartReasonNumber { get; set; }

        [DataMember] public string StartReasonPlaceOfIssue { get; set; }

        [DataMember] public DateTime? StartReasonDateOfIssue { get; set; }

        [DataMember] public string EndReasonName { get; set; }

        [DataMember] public string EndReasonSeries { get; set; }

        [DataMember] public string EndReasonNumber { get; set; }

        [DataMember] public string EndReasonPlaceOfIssue { get; set; }

        [DataMember] public DateTime? EndReasonDateOfIssue { get; set; }

        /// <summary>
        ///     можно использовать
        /// </summary>
        [DataMember]
        public bool CanUse { get; set; }

        /// <summary>
        ///     Малообеспеченность
        /// </summary>
        [DataMember]
        public bool LowIncome { get; set; }
    }
}
