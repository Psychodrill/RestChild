using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace RestChild.MPGUIntegration.V61
{
    /// <remarks />
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
    public class TaskType
    {
        private Department departmentField;

        private ItemsChoiceType[] itemsElementNameField;

        private string[] itemsField;
        private string messageIdField;

        private Person responsibleField;

        private DateTime taskDateField;

        private string taskIdField;

        private string taskNumberField;

        /// <remarks />
        public string MessageId
        {
            get => messageIdField;
            set => messageIdField = value;
        }

        /// <remarks />
        public string TaskId
        {
            get => taskIdField;
            set => taskIdField = value;
        }

        /// <remarks />
        public string TaskNumber
        {
            get => taskNumberField;
            set => taskNumberField = value;
        }

        /// <remarks />
        public DateTime TaskDate
        {
            get => taskDateField;
            set => taskDateField = value;
        }

        /// <remarks />
        public Person Responsible
        {
            get => responsibleField;
            set => responsibleField = value;
        }

        /// <remarks />
        public Department Department
        {
            get => departmentField;
            set => departmentField = value;
        }

        /// <remarks />
        [XmlElement("FunctionTypeCode", typeof(string))]
        [XmlElement("ServiceNumber", typeof(string))]
        [XmlElement("ServiceTypeCode", typeof(string))]
        [XmlChoiceIdentifier("ItemsElementName")]
        public string[] Items
        {
            get => itemsField;
            set => itemsField = value;
        }

        /// <remarks />
        [XmlElement("ItemsElementName")]
        [XmlIgnore]
        public ItemsChoiceType[] ItemsElementName
        {
            get => itemsElementNameField;
            set => itemsElementNameField = value;
        }
    }

    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [XmlType(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", IncludeInSchema = false)]
    public enum ItemsChoiceType
    {
        /// <remarks />
        FunctionTypeCode,

        /// <remarks />
        ServiceNumber,

        /// <remarks />
        ServiceTypeCode
    }
}
