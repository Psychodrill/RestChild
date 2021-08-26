using System;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto
{
    [Serializable]
    [DataContract]
    public class PaymentCheckResult
    {
        public string SocialSupportName { get; set; }
        public string SocialSupportId { get; set; }
        public PaymentCheckResultVolume[] Volumes { get; set; }

        public bool CanUse { get; set; }
    }

    [Serializable]
    [DataContract]
    public class PaymentCheckResultVolume
    {
        public DateTime? AssignmentDate { get; set; }

        public string AssignmentDateType { get; set; }

        public decimal? Volume { get; set; }
    }
}
