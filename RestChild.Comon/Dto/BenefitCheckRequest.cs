using System;
using System.Runtime.Serialization;
using RestChild.Comon.Exchange.Cpmpk;

namespace RestChild.Comon.Dto
{
    /// <summary>
    ///     запрос на проверку льготы
    /// </summary>
    public class BenefitCheckRequest
    {
        [DataMember(Name = "lastName", EmitDefaultValue = false)]
        public string LastName { get; set; }

        [DataMember(Name = "firstName", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        [DataMember(Name = "middleName", EmitDefaultValue = false)]
        public string MiddleName { get; set; }

        [DataMember(Name = "DateOfBirth", EmitDefaultValue = false)]
        public DateTime? DateOfBirth { get; set; }

        [DataMember(Name = "documentSeria", EmitDefaultValue = false)]
        public string DocumentSeria { get; set; }

        [DataMember(Name = "documentNumber", EmitDefaultValue = false)]
        public string DocumentNumber { get; set; }

        [DataMember(Name = "documentTypeId", EmitDefaultValue = false)]
        public long? DocumentTypeId { get; set; }

        [DataMember(Name = "snils", EmitDefaultValue = false)]
        public string Snils { get; set; }

        [DataMember(Name = "male", EmitDefaultValue = false)]
        public bool Male { get; set; }

        [DataMember(Name = "lastNameParent", EmitDefaultValue = false)]
        public string LastNameParent { get; set; }

        [DataMember(Name = "firstNameParent", EmitDefaultValue = false)]
        public string FirstNameParent { get; set; }

        [DataMember(Name = "middleNameParent", EmitDefaultValue = false)]
        public string MiddleNameParent { get; set; }

        [DataMember(Name = "maleParent", EmitDefaultValue = false)]
        public bool MaleParent { get; set; }

        [DataMember(Name = "dateOfBirthParent", EmitDefaultValue = false)]
        public DateTime? DateOfBirthParent { get; set; }

        [DataMember(Name = "documentDateOfIssue", EmitDefaultValue = false)]
        public DateTime? DocumentDateOfIssue { get; set; }

        [DataMember(Name = "documentSubjectIssue", EmitDefaultValue = false)]
        public string DocumentSubjectIssue { get; set; }

        [DataMember(Name = "region", EmitDefaultValue = false)]
        public string Region { get; set; }

        [DataMember(Name = "district", EmitDefaultValue = false)]
        public string District { get; set; }

        [DataMember(Name = "settlement", EmitDefaultValue = false)]
        public string Settlement { get; set; }

        /// <summary>
        ///     ответ от ЦПМПК
        /// </summary>
        public CpmpkResponseDto CpmpkResponse { get; set; }
    }
}
