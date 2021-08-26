using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Commercial
{
    [DataContract(Name = "object")]
    public class BaseResponse
    {
        /// <summary>
        ///     ид объекта в базе даннных (число)
        /// </summary>
        [DataMember(Name = "id", Order = 1, EmitDefaultValue = false)]
        public long? Id { get; set; }

        /// <summary>
        ///     название объекта
        /// </summary>
        [DataMember(Name = "name", Order = 3, EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        ///     human-readable id
        /// </summary>
        [DataMember(Name = "code", IsRequired = false, EmitDefaultValue = false, Order = 2)]
        public string Code { get; set; }

        /// <summary>
        ///     наличие ошибки
        /// </summary>
        [DataMember(Name = "hasError", IsRequired = false, EmitDefaultValue = false, Order = 0)]
        public bool? HasError { get; set; }

        /// <summary>
        ///     код ошибки.
        /// </summary>
        [DataMember(Name = "errorMessage", IsRequired = false, EmitDefaultValue = false, Order = 0)]
        public string ErrorMessage { get; set; }
    }
}
