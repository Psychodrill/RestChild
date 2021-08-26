using System;

namespace RestChild.Comon.Dto
{
    /// <summary>
    ///     проверка родства
    /// </summary>
    public class RelationshipCheckResult
    {
        public string ChildLastName { get; set; }
        public string ChildFirstName { get; set; }
        public string ChildPatronymic { get; set; }
        public DateTime? ChildBirthDate { get; set; }
        public string ChildBirthPlace { get; set; }
        public string FatherLastName { get; set; }
        public string FatherFirstName { get; set; }
        public string FatherPatronymic { get; set; }
        public string FatherCitizenship { get; set; }
        public DateTime? FatherBirthDate { get; set; }
        public string MotherLastName { get; set; }
        public string MotherFirstName { get; set; }
        public string MotherPatronymic { get; set; }
        public string MotherCitizenship { get; set; }
        public DateTime? MotherBirthDate { get; set; }
        public string ActRequisitesActNumber { get; set; }
        public DateTime? ActRequisitesActDate { get; set; }
        public string ActRequisitesNameOfRegistrar { get; set; }
        public string CertSeries { get; set; }
        public string CertNumber { get; set; }
        public DateTime? CertlssueDate { get; set; }
    }
}
