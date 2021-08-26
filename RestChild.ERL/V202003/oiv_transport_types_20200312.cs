using System.Xml.Serialization;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ComponentModel;

namespace RestChild.ERL.V202003
{
    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv/mq")]
    [XmlRoot(Namespace = "http://erl.msr.com/schemas/oiv/mq", IsNullable = false)]
    public partial class message_response
    {
        private messageIdentity identityField;

        private response2 responseField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public messageIdentity identity
        {
            get
            {
                return this.identityField;
            }
            set
            {
                this.identityField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public response2 response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(messageIdentityWithSession))]
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv/mq")]
    public partial class messageIdentity
    {

        private string senderField;

        private string message_idField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string sender
        {
            get
            {
                return this.senderField;
            }
            set
            {
                this.senderField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string message_id
        {
            get
            {
                return this.message_idField;
            }
            set
            {
                this.message_idField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class wrong_citizen_benefit
    {

        private string benefittype_pkField;

        private System.DateTime begin_dateField;

        private bool begin_dateFieldSpecified;

        private System.DateTime end_dateField;

        private bool end_dateFieldSpecified;

        private string statement_numberField;

        private string decisionField;

        private System.DateTime decision_dateField;

        private bool decision_dateFieldSpecified;

        private string decision_document_pkField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string benefittype_pk
        {
            get
            {
                return this.benefittype_pkField;
            }
            set
            {
                this.benefittype_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime begin_date
        {
            get
            {
                return this.begin_dateField;
            }
            set
            {
                this.begin_dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool begin_dateSpecified
        {
            get
            {
                return this.begin_dateFieldSpecified;
            }
            set
            {
                this.begin_dateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime end_date
        {
            get
            {
                return this.end_dateField;
            }
            set
            {
                this.end_dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool end_dateSpecified
        {
            get
            {
                return this.end_dateFieldSpecified;
            }
            set
            {
                this.end_dateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string statement_number
        {
            get
            {
                return this.statement_numberField;
            }
            set
            {
                this.statement_numberField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string decision
        {
            get
            {
                return this.decisionField;
            }
            set
            {
                this.decisionField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime decision_date
        {
            get
            {
                return this.decision_dateField;
            }
            set
            {
                this.decision_dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool decision_dateSpecified
        {
            get
            {
                return this.decision_dateFieldSpecified;
            }
            set
            {
                this.decision_dateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string decision_document_pk
        {
            get
            {
                return this.decision_document_pkField;
            }
            set
            {
                this.decision_document_pkField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class wrong_citizen_benefitcategory
    {

        private string benefitcategory_pkField;

        private System.DateTime begin_dateField;

        private System.DateTime end_dateField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string benefitcategory_pk
        {
            get
            {
                return this.benefitcategory_pkField;
            }
            set
            {
                this.benefitcategory_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime begin_date
        {
            get
            {
                return this.begin_dateField;
            }
            set
            {
                this.begin_dateField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime end_date
        {
            get
            {
                return this.end_dateField;
            }
            set
            {
                this.end_dateField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class wrong_person
    {

        private string citizen_pkField;

        private string provider_identifierField;

        private wrong_citizen_benefitcategory[] wrong_citizen_benefitcategoriesField;

        private wrong_citizen_benefit[] wrong_citizen_benefitsField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string citizen_pk
        {
            get
            {
                return this.citizen_pkField;
            }
            set
            {
                this.citizen_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string provider_identifier
        {
            get
            {
                return this.provider_identifierField;
            }
            set
            {
                this.provider_identifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public wrong_citizen_benefitcategory[] wrong_citizen_benefitcategories
        {
            get
            {
                return this.wrong_citizen_benefitcategoriesField;
            }
            set
            {
                this.wrong_citizen_benefitcategoriesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public wrong_citizen_benefit[] wrong_citizen_benefits
        {
            get
            {
                return this.wrong_citizen_benefitsField;
            }
            set
            {
                this.wrong_citizen_benefitsField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class person_outgoing
    {

        private string citizen_pkField;

        private string provider_identifierField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string citizen_pk
        {
            get
            {
                return this.citizen_pkField;
            }
            set
            {
                this.citizen_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string provider_identifier
        {
            get
            {
                return this.provider_identifierField;
            }
            set
            {
                this.provider_identifierField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class granting_benefit
    {

        private string benefittype_pkField;

        private object[] itemsField;

        private ItemsChoiceType[] itemsElementNameField;

        private string justificationField;

        private string decision_code_pkField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string benefittype_pk
        {
            get
            {
                return this.benefittype_pkField;
            }
            set
            {
                this.benefittype_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement("decision", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [XmlElement("decision_date", typeof(System.DateTime), Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        [XmlElement("decision_document_pk", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [XmlElement("statement_number", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [XmlElement("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string justification
        {
            get
            {
                return this.justificationField;
            }
            set
            {
                this.justificationField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string decision_code_pk
        {
            get
            {
                return this.decision_code_pkField;
            }
            set
            {
                this.decision_code_pkField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv", IncludeInSchema = false)]
    public enum ItemsChoiceType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":decision")]
        decision,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":decision_date")]
        decision_date,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":decision_document_pk")]
        decision_document_pk,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":statement_number")]
        statement_number,
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class granting_citizen_benefit
    {

        private string citizen_pkField;

        private string provider_identifierField;

        private granting_benefit[] granting_benefitsField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string citizen_pk
        {
            get
            {
                return this.citizen_pkField;
            }
            set
            {
                this.citizen_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string provider_identifier
        {
            get
            {
                return this.provider_identifierField;
            }
            set
            {
                this.provider_identifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public granting_benefit[] granting_benefits
        {
            get
            {
                return this.granting_benefitsField;
            }
            set
            {
                this.granting_benefitsField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class person_transaction
    {

        private string benefittype_pkField;

        private System.DateTime dateField;

        private string money_equivalentField;

        private string decisionField;

        private System.DateTime decision_dateField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string benefittype_pk
        {
            get
            {
                return this.benefittype_pkField;
            }
            set
            {
                this.benefittype_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.DateTime date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "nonNegativeInteger")]
        public string money_equivalent
        {
            get
            {
                return this.money_equivalentField;
            }
            set
            {
                this.money_equivalentField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string decision
        {
            get
            {
                return this.decisionField;
            }
            set
            {
                this.decisionField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime decision_date
        {
            get
            {
                return this.decision_dateField;
            }
            set
            {
                this.decision_dateField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class person_transactions
    {

        private string citizen_pkField;

        private string provider_identifierField;

        private person_transaction[] transactionField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string citizen_pk
        {
            get
            {
                return this.citizen_pkField;
            }
            set
            {
                this.citizen_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string provider_identifier
        {
            get
            {
                return this.provider_identifierField;
            }
            set
            {
                this.provider_identifierField = value;
            }
        }

        /// <remarks/>
        [XmlElement("transaction", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public person_transaction[] transaction
        {
            get
            {
                return this.transactionField;
            }
            set
            {
                this.transactionField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class benefit_form
    {

        private string amountField;

        private string itemField;

        private ItemChoiceType1 itemElementNameField;

        private string money_equivalentField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "nonNegativeInteger")]
        public string amount
        {
            get
            {
                return this.amountField;
            }
            set
            {
                this.amountField = value;
            }
        }

        /// <remarks/>
        [XmlElement("OKEICode", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "nonNegativeInteger")]
        [XmlElement("measuryCode", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "nonNegativeInteger")]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType1 ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "nonNegativeInteger")]
        public string money_equivalent
        {
            get
            {
                return this.money_equivalentField;
            }
            set
            {
                this.money_equivalentField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv", IncludeInSchema = false)]
    public enum ItemChoiceType1
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":OKEICode")]
        OKEICode,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":measuryCode")]
        measuryCode,
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class citizen_benefit
    {

        private string benefittype_pkField;

        private int action_codeField;

        private System.DateTime begin_dateField;

        private System.DateTime end_dateField;

        private string statement_numberField;

        private string decisionField;

        private System.Nullable<System.DateTime> decision_dateField;

        private bool decision_dateFieldSpecified;

        private string decision_document_pkField;

        private string[] needs_criteriaField;

        private citizen_benefitBenefit_info benefit_infoField;

        private document[] benefit_documentsField;

        private citizen_benefitBenefitcategories benefitcategoriesField;

        private citizen_relation[] citizen_relationsField;

        private string idField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string benefittype_pk
        {
            get
            {
                return this.benefittype_pkField;
            }
            set
            {
                this.benefittype_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int action_code
        {
            get
            {
                return this.action_codeField;
            }
            set
            {
                this.action_codeField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime begin_date
        {
            get
            {
                return this.begin_dateField;
            }
            set
            {
                this.begin_dateField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime end_date
        {
            get
            {
                return this.end_dateField;
            }
            set
            {
                this.end_dateField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string statement_number
        {
            get
            {
                return this.statement_numberField;
            }
            set
            {
                this.statement_numberField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string decision
        {
            get
            {
                return this.decisionField;
            }
            set
            {
                this.decisionField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.Nullable<System.DateTime> decision_date
        {
            get
            {
                return this.decision_dateField;
            }
            set
            {
                this.decision_dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool decision_dateSpecified
        {
            get
            {
                return this.decision_dateFieldSpecified;
            }
            set
            {
                this.decision_dateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string decision_document_pk
        {
            get
            {
                return this.decision_document_pkField;
            }
            set
            {
                this.decision_document_pkField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("criteria", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public string[] needs_criteria
        {
            get
            {
                return this.needs_criteriaField;
            }
            set
            {
                this.needs_criteriaField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public citizen_benefitBenefit_info benefit_info
        {
            get
            {
                return this.benefit_infoField;
            }
            set
            {
                this.benefit_infoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("benefit_document", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public document[] benefit_documents
        {
            get
            {
                return this.benefit_documentsField;
            }
            set
            {
                this.benefit_documentsField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public citizen_benefitBenefitcategories benefitcategories
        {
            get
            {
                return this.benefitcategoriesField;
            }
            set
            {
                this.benefitcategoriesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public citizen_relation[] citizen_relations
        {
            get
            {
                return this.citizen_relationsField;
            }
            set
            {
                this.citizen_relationsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class citizen_benefitBenefit_info
    {

        private object itemField;

        private ItemChoiceType2 itemElementNameField;

        /// <remarks/>
        [XmlElement("benefit_summa", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "nonNegativeInteger")]
        [XmlElement("exemption_form", typeof(citizen_benefitBenefit_infoExemption_form), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [XmlElement("natural_form", typeof(benefit_form), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [XmlElement("service_form", typeof(benefit_form), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType2 ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class citizen_benefitBenefit_infoExemption_form : benefit_form
    {

        private bool monetizationField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool monetization
        {
            get
            {
                return this.monetizationField;
            }
            set
            {
                this.monetizationField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv", IncludeInSchema = false)]
    public enum ItemChoiceType2
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":benefit_summa")]
        benefit_summa,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":exemption_form")]
        exemption_form,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":natural_form")]
        natural_form,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":service_form")]
        service_form,
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class document
    {

        private string doctype_pkField;

        private string serialField;

        private string numberField;

        private string docauthority_pkField;

        private string docauthority_nameField;

        private System.DateTime doc_issuedateField;

        private System.DateTime begin_dateField;

        private bool begin_dateFieldSpecified;

        private System.DateTime end_dateField;

        private bool end_dateFieldSpecified;

        private bool realnessField;

        private bool realnessFieldSpecified;

        private System.DateTime reassessment_dateField;

        private bool reassessment_dateFieldSpecified;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string doctype_pk
        {
            get
            {
                return this.doctype_pkField;
            }
            set
            {
                this.doctype_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string serial
        {
            get
            {
                return this.serialField;
            }
            set
            {
                this.serialField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string docauthority_pk
        {
            get
            {
                return this.docauthority_pkField;
            }
            set
            {
                this.docauthority_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string docauthority_name
        {
            get
            {
                return this.docauthority_nameField;
            }
            set
            {
                this.docauthority_nameField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime doc_issuedate
        {
            get
            {
                return this.doc_issuedateField;
            }
            set
            {
                this.doc_issuedateField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime begin_date
        {
            get
            {
                return this.begin_dateField;
            }
            set
            {
                this.begin_dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool begin_dateSpecified
        {
            get
            {
                return this.begin_dateFieldSpecified;
            }
            set
            {
                this.begin_dateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime end_date
        {
            get
            {
                return this.end_dateField;
            }
            set
            {
                this.end_dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool end_dateSpecified
        {
            get
            {
                return this.end_dateFieldSpecified;
            }
            set
            {
                this.end_dateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool realness
        {
            get
            {
                return this.realnessField;
            }
            set
            {
                this.realnessField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool realnessSpecified
        {
            get
            {
                return this.realnessFieldSpecified;
            }
            set
            {
                this.realnessFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime reassessment_date
        {
            get
            {
                return this.reassessment_dateField;
            }
            set
            {
                this.reassessment_dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool reassessment_dateSpecified
        {
            get
            {
                return this.reassessment_dateFieldSpecified;
            }
            set
            {
                this.reassessment_dateFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class citizen_benefitBenefitcategories
    {

        private object[] itemsField;

        /// <remarks/>
        [XmlElement("benefitcategory", typeof(citizen_benefitBenefitcategoriesBenefitcategory), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [XmlElement("benefitcategory_pk", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class citizen_benefitBenefitcategoriesBenefitcategory
    {

        private string benefitcategory_pkField;

        private System.DateTime begin_dateField;

        private bool begin_dateFieldSpecified;

        private System.DateTime end_dateField;

        private bool end_dateFieldSpecified;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string benefitcategory_pk
        {
            get
            {
                return this.benefitcategory_pkField;
            }
            set
            {
                this.benefitcategory_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime begin_date
        {
            get
            {
                return this.begin_dateField;
            }
            set
            {
                this.begin_dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool begin_dateSpecified
        {
            get
            {
                return this.begin_dateFieldSpecified;
            }
            set
            {
                this.begin_dateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime end_date
        {
            get
            {
                return this.end_dateField;
            }
            set
            {
                this.end_dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool end_dateSpecified
        {
            get
            {
                return this.end_dateFieldSpecified;
            }
            set
            {
                this.end_dateFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class citizen_relation
    {

        private string citizen_pkField;

        private string provider_identifierField;

        private string person_relation_type_codeField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string citizen_pk
        {
            get
            {
                return this.citizen_pkField;
            }
            set
            {
                this.citizen_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string provider_identifier
        {
            get
            {
                return this.provider_identifierField;
            }
            set
            {
                this.provider_identifierField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string person_relation_type_code
        {
            get
            {
                return this.person_relation_type_codeField;
            }
            set
            {
                this.person_relation_type_codeField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class person_benefit
    {

        private string citizen_pkField;

        private string provider_identifierField;

        private citizen_benefit[] citizen_benefitsField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string citizen_pk
        {
            get
            {
                return this.citizen_pkField;
            }
            set
            {
                this.citizen_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string provider_identifier
        {
            get
            {
                return this.provider_identifierField;
            }
            set
            {
                this.provider_identifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public citizen_benefit[] citizen_benefits
        {
            get
            {
                return this.citizen_benefitsField;
            }
            set
            {
                this.citizen_benefitsField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class citizen_benefitcategory_identity
    {

        private string benefitcategory_pkField;

        private System.DateTime begin_dateField;

        private System.DateTime end_dateField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string benefitcategory_pk
        {
            get
            {
                return this.benefitcategory_pkField;
            }
            set
            {
                this.benefitcategory_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime begin_date
        {
            get
            {
                return this.begin_dateField;
            }
            set
            {
                this.begin_dateField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime end_date
        {
            get
            {
                return this.end_dateField;
            }
            set
            {
                this.end_dateField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class citizen_benefitcategory
    {

        private citizen_benefitcategory_identity citizen_benefitcategory_identityField;

        private int action_codeField;

        private string statement_numberField;

        private document[] confirming_documentsField;

        private citizen_relation[] citizen_relationsField;

        private string idField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public citizen_benefitcategory_identity citizen_benefitcategory_identity
        {
            get
            {
                return this.citizen_benefitcategory_identityField;
            }
            set
            {
                this.citizen_benefitcategory_identityField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int action_code
        {
            get
            {
                return this.action_codeField;
            }
            set
            {
                this.action_codeField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string statement_number
        {
            get
            {
                return this.statement_numberField;
            }
            set
            {
                this.statement_numberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("confirming_document", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public document[] confirming_documents
        {
            get
            {
                return this.confirming_documentsField;
            }
            set
            {
                this.confirming_documentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public citizen_relation[] citizen_relations
        {
            get
            {
                return this.citizen_relationsField;
            }
            set
            {
                this.citizen_relationsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class person_benefitcategory
    {

        private string citizen_pkField;

        private string provider_identifierField;

        private citizen_benefitcategory[] citizen_benefitcategoriesField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string citizen_pk
        {
            get
            {
                return this.citizen_pkField;
            }
            set
            {
                this.citizen_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string provider_identifier
        {
            get
            {
                return this.provider_identifierField;
            }
            set
            {
                this.provider_identifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public citizen_benefitcategory[] citizen_benefitcategories
        {
            get
            {
                return this.citizen_benefitcategoriesField;
            }
            set
            {
                this.citizen_benefitcategoriesField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class decision_family_data
    {

        private string decisionField;

        private System.DateTime decision_dateField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string decision
        {
            get
            {
                return this.decisionField;
            }
            set
            {
                this.decisionField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime decision_date
        {
            get
            {
                return this.decision_dateField;
            }
            set
            {
                this.decision_dateField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class address
    {

        private string bti_identityField;

        private string post_indexField;

        private string settlement_codeField;

        private string itemField;

        private ItemChoiceType itemElementNameField;

        private string house_codeField;

        private string houseField;

        private string corpusField;

        private string buildingField;

        private string flatField;

        private string address_textField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string bti_identity
        {
            get
            {
                return this.bti_identityField;
            }
            set
            {
                this.bti_identityField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string post_index
        {
            get
            {
                return this.post_indexField;
            }
            set
            {
                this.post_indexField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string settlement_code
        {
            get
            {
                return this.settlement_codeField;
            }
            set
            {
                this.settlement_codeField = value;
            }
        }

        /// <remarks/>
        [XmlElement("street_code", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [XmlElement("street_name", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string house_code
        {
            get
            {
                return this.house_codeField;
            }
            set
            {
                this.house_codeField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string house
        {
            get
            {
                return this.houseField;
            }
            set
            {
                this.houseField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string corpus
        {
            get
            {
                return this.corpusField;
            }
            set
            {
                this.corpusField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string building
        {
            get
            {
                return this.buildingField;
            }
            set
            {
                this.buildingField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string flat
        {
            get
            {
                return this.flatField;
            }
            set
            {
                this.flatField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string address_text
        {
            get
            {
                return this.address_textField;
            }
            set
            {
                this.address_textField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv", IncludeInSchema = false)]
    public enum ItemChoiceType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":street_code")]
        street_code,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":street_name")]
        street_name,
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class citizen_address
    {

        private citizen_addressAddress_type address_typeField;

        private address addressField;

        private System.DateTime registration_dateField;

        private bool registration_dateFieldSpecified;

        private System.DateTime departure_dateField;

        private bool departure_dateFieldSpecified;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public citizen_addressAddress_type address_type
        {
            get
            {
                return this.address_typeField;
            }
            set
            {
                this.address_typeField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public address address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime registration_date
        {
            get
            {
                return this.registration_dateField;
            }
            set
            {
                this.registration_dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool registration_dateSpecified
        {
            get
            {
                return this.registration_dateFieldSpecified;
            }
            set
            {
                this.registration_dateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime departure_date
        {
            get
            {
                return this.departure_dateField;
            }
            set
            {
                this.departure_dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool departure_dateSpecified
        {
            get
            {
                return this.departure_dateFieldSpecified;
            }
            set
            {
                this.departure_dateFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://erl.msr.com/schemas/oiv")]
    public enum citizen_addressAddress_type
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class identity_document
    {

        private string doctype_pkField;

        private string serialField;

        private string numberField;

        private string docauthority_pkField;

        private string docauthority_nameField;

        private System.DateTime doc_issuedateField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string doctype_pk
        {
            get
            {
                return this.doctype_pkField;
            }
            set
            {
                this.doctype_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string serial
        {
            get
            {
                return this.serialField;
            }
            set
            {
                this.serialField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string docauthority_pk
        {
            get
            {
                return this.docauthority_pkField;
            }
            set
            {
                this.docauthority_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string docauthority_name
        {
            get
            {
                return this.docauthority_nameField;
            }
            set
            {
                this.docauthority_nameField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime doc_issuedate
        {
            get
            {
                return this.doc_issuedateField;
            }
            set
            {
                this.doc_issuedateField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class address_birth
    {

        private string address_textField;

        private string region_codeField;

        private string area_codeField;

        private string settlement_codeField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string address_text
        {
            get
            {
                return this.address_textField;
            }
            set
            {
                this.address_textField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string region_code
        {
            get
            {
                return this.region_codeField;
            }
            set
            {
                this.region_codeField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string area_code
        {
            get
            {
                return this.area_codeField;
            }
            set
            {
                this.area_codeField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string settlement_code
        {
            get
            {
                return this.settlement_codeField;
            }
            set
            {
                this.settlement_codeField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class citizen
    {

        private string citizen_pkField;

        private string surnameField;

        private string firstnameField;

        private string patronymicField;

        private System.DateTime birthdayField;

        private sex sexField;

        private bool sexFieldSpecified;

        private address_birth birth_placeField;

        private string birth_surnameField;

        private string snilsField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string citizen_pk
        {
            get
            {
                return this.citizen_pkField;
            }
            set
            {
                this.citizen_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string surname
        {
            get
            {
                return this.surnameField;
            }
            set
            {
                this.surnameField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string firstname
        {
            get
            {
                return this.firstnameField;
            }
            set
            {
                this.firstnameField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string patronymic
        {
            get
            {
                return this.patronymicField;
            }
            set
            {
                this.patronymicField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime birthday
        {
            get
            {
                return this.birthdayField;
            }
            set
            {
                this.birthdayField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public sex sex
        {
            get
            {
                return this.sexField;
            }
            set
            {
                this.sexField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool sexSpecified
        {
            get
            {
                return this.sexFieldSpecified;
            }
            set
            {
                this.sexFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public address_birth birth_place
        {
            get
            {
                return this.birth_placeField;
            }
            set
            {
                this.birth_placeField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string birth_surname
        {
            get
            {
                return this.birth_surnameField;
            }
            set
            {
                this.birth_surnameField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string snils
        {
            get
            {
                return this.snilsField;
            }
            set
            {
                this.snilsField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public enum sex
    {

        /// <remarks/>
        F,

        /// <remarks/>
        M,
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class person
    {

        private citizen citizenField;

        private identity_document identity_documentField;

        private string innField;

        private string omsField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public citizen citizen
        {
            get
            {
                return this.citizenField;
            }
            set
            {
                this.citizenField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public identity_document identity_document
        {
            get
            {
                return this.identity_documentField;
            }
            set
            {
                this.identity_documentField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string inn
        {
            get
            {
                return this.innField;
            }
            set
            {
                this.innField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string oms
        {
            get
            {
                return this.omsField;
            }
            set
            {
                this.omsField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class person_incoming
    {

        private person personField;

        private string provider_identifierField;

        private citizen_address[] citizen_addressesField;

        private person_incomingFamily_data family_dataField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public person person
        {
            get
            {
                return this.personField;
            }
            set
            {
                this.personField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string provider_identifier
        {
            get
            {
                return this.provider_identifierField;
            }
            set
            {
                this.provider_identifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public citizen_address[] citizen_addresses
        {
            get
            {
                return this.citizen_addressesField;
            }
            set
            {
                this.citizen_addressesField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public person_incomingFamily_data family_data
        {
            get
            {
                return this.family_dataField;
            }
            set
            {
                this.family_dataField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class person_incomingFamily_data
    {

        private decision_family_data itemField;

        /// <remarks/>
        [XmlElement("many_children", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decision_family_data Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(error_with_identity))]
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class error
    {

        private string codeField;

        private string commentField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string comment
        {
            get
            {
                return this.commentField;
            }
            set
            {
                this.commentField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class error_with_identity : error
    {

        private string[] ref_idField;

        /// <remarks/>
        [XmlElement("ref_id", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string[] ref_id
        {
            get
            {
                return this.ref_idField;
            }
            set
            {
                this.ref_idField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class record_error
    {

        private string citizen_pkField;

        private string provider_identifierField;

        private error_with_identity[] errorsField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string citizen_pk
        {
            get
            {
                return this.citizen_pkField;
            }
            set
            {
                this.citizen_pkField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string provider_identifier
        {
            get
            {
                return this.provider_identifierField;
            }
            set
            {
                this.provider_identifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("error", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public error_with_identity[] errors
        {
            get
            {
                return this.errorsField;
            }
            set
            {
                this.errorsField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class result
    {

        private string codeField;

        private string messageField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(response2))]
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv")]
    public partial class response
    {

        private System.DateTime processing_dateField;

        private result resultField;

        private record_error[] record_errorsField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime processing_date
        {
            get
            {
                return this.processing_dateField;
            }
            set
            {
                this.processing_dateField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public result result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("error", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public record_error[] record_errors
        {
            get
            {
                return this.record_errorsField;
            }
            set
            {
                this.record_errorsField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "response", Namespace = "http://erl.msr.com/schemas/oiv/mq")]
    public partial class response2 : response
    {

        private messageIdentityWithSession request_identityField;

        private error package_errorField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public messageIdentityWithSession request_identity
        {
            get
            {
                return this.request_identityField;
            }
            set
            {
                this.request_identityField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public error package_error
        {
            get
            {
                return this.package_errorField;
            }
            set
            {
                this.package_errorField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv/mq")]
    public partial class messageIdentityWithSession : messageIdentity
    {

        private session sessionField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public session session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv/mq")]
    public partial class session
    {

        private string session_idField;

        private long countField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string session_id
        {
            get
            {
                return this.session_idField;
            }
            set
            {
                this.session_idField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long count
        {
            get
            {
                return this.countField;
            }
            set
            {
                this.countField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv/mq")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://erl.msr.com/schemas/oiv/mq", IsNullable = false)]
    public partial class oiv_persons_incoming
    {

        private messageIdentityWithSession identityField;

        private person_incoming[] personsField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public messageIdentityWithSession identity
        {
            get
            {
                return this.identityField;
            }
            set
            {
                this.identityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public person_incoming[] persons
        {
            get
            {
                return this.personsField;
            }
            set
            {
                this.personsField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv/mq")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://erl.msr.com/schemas/oiv/mq", IsNullable = false)]
    public partial class citizen_benefitcategory_incoming
    {

        private messageIdentityWithSession identityField;

        private person_benefitcategory[] person_benefitcategoriesField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public messageIdentityWithSession identity
        {
            get
            {
                return this.identityField;
            }
            set
            {
                this.identityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public person_benefitcategory[] person_benefitcategories
        {
            get
            {
                return this.person_benefitcategoriesField;
            }
            set
            {
                this.person_benefitcategoriesField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv/mq")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://erl.msr.com/schemas/oiv/mq", IsNullable = false)]
    public partial class citizen_benefits_incoming
    {

        private messageIdentityWithSession identityField;

        private person_benefit[] person_benefitsField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public messageIdentityWithSession identity
        {
            get
            {
                return this.identityField;
            }
            set
            {
                this.identityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public person_benefit[] person_benefits
        {
            get
            {
                return this.person_benefitsField;
            }
            set
            {
                this.person_benefitsField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv/mq")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://erl.msr.com/schemas/oiv/mq", IsNullable = false)]
    public partial class citizen_transactions_incoming
    {

        private messageIdentityWithSession identityField;

        private person_transactions[] person_transactionField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public messageIdentityWithSession identity
        {
            get
            {
                return this.identityField;
            }
            set
            {
                this.identityField = value;
            }
        }

        /// <remarks/>
        [XmlElement("person_transaction", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public person_transactions[] person_transaction
        {
            get
            {
                return this.person_transactionField;
            }
            set
            {
                this.person_transactionField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv/mq")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://erl.msr.com/schemas/oiv/mq", IsNullable = false)]
    public partial class granting_citizen_benefit_incoming
    {

        private messageIdentityWithSession identityField;

        private granting_citizen_benefit[] granting_citizen_benefitsField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public messageIdentityWithSession identity
        {
            get
            {
                return this.identityField;
            }
            set
            {
                this.identityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public granting_citizen_benefit[] granting_citizen_benefits
        {
            get
            {
                return this.granting_citizen_benefitsField;
            }
            set
            {
                this.granting_citizen_benefitsField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv/mq")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://erl.msr.com/schemas/oiv/mq", IsNullable = false)]
    public partial class oiv_persons_outgoing
    {

        private messageIdentity identityField;

        private person_outgoing[] personsField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public messageIdentity identity
        {
            get
            {
                return this.identityField;
            }
            set
            {
                this.identityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public person_outgoing[] persons
        {
            get
            {
                return this.personsField;
            }
            set
            {
                this.personsField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.7.3081.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://erl.msr.com/schemas/oiv/mq")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://erl.msr.com/schemas/oiv/mq", IsNullable = false)]
    public partial class wrong_persons_outgoing
    {

        private messageIdentity identityField;

        private wrong_person[] wrong_personsField;

        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public messageIdentity identity
        {
            get
            {
                return this.identityField;
            }
            set
            {
                this.identityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public wrong_person[] wrong_persons
        {
            get
            {
                return this.wrong_personsField;
            }
            set
            {
                this.wrong_personsField = value;
            }
        }
    }

}

