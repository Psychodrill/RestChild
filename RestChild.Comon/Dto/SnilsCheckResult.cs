using System;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto
{
    [Serializable]
    [DataContract]
    public class SnilsCheckResult
    {
        public string CheckResult { get; set; }

        public bool CanUse { get; set; }
    }
}
