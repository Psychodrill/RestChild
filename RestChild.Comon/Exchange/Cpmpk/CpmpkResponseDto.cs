using System.Runtime.Serialization;

namespace RestChild.Comon.Exchange.Cpmpk
{
    /// <summary>
    ///     ответ ЦПМПК
    /// </summary>
    [DataContract]
    public class CpmpkResponseDto
    {
        /// <summary>
        ///     Наличие заключения
        /// </summary>
        [DataMember(Name = "available")]
        public bool Available { get; set; }

        /// <summary>
        ///     Наличие адаптированной общеобразовательной программы (АООП)
        /// </summary>
        [DataMember(Name = "aoop")]
        public bool? Aoop { get; set; }


        /// <summary>
        ///     Номер заключения
        /// </summary>
        [DataMember(Name = "number")]
        public string Number { get; set; }


        /// <summary>
        ///     Дата выдачи заключения
        /// </summary>
        [DataMember(Name = "date")]
        public string Date { get; set; }
    }
}
