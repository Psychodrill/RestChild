using System.Runtime.Serialization;

namespace RestChild.Web.Models
{
    /// <summary>
    ///     информация о дне
    /// </summary>
    [DataContract]
    public class MGTWorkningDayInfo
    {
        /// <summary>
        ///     Периоды
        /// </summary>
        [DataMember(Name = "is")]
        public string[] Intervals { get; set; }

        /// <summary>
        ///     Период
        /// </summary>
        [DataMember(Name = "i")]
        public short Interval { get; set; }
    }
}
