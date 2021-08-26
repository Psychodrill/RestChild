using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.MPGUIntegration.V618
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
    public partial class ServiceDocument : object, System.ComponentModel.INotifyPropertyChanged
    {

        private DictionaryItem docKindField;

        private string docSubTypeField;

        private string objectIdField;

        private string docSerieField;

        private string docNumberField;

        private System.Nullable<System.DateTime> docDateField;

        private bool docDateFieldSpecified;

        private System.Nullable<System.DateTime> validityPeriodField;

        private bool validityPeriodFieldSpecified;

        private string whoSignField;

        private System.Nullable<int> listCountField;

        private bool listCountFieldSpecified;

        private System.Nullable<int> copyCountField;

        private bool copyCountFieldSpecified;

        private string divisionCodeField;

        private Note[] docNotesField;

        private CoordinateFileReference[] docFilesField;

        private System.Xml.XmlElement customAttributesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public DictionaryItem DocKind
        {
            get
            {
                return this.docKindField;
            }
            set
            {
                this.docKindField = value;
                this.RaisePropertyChanged("DocKind");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string DocSubType
        {
            get
            {
                return this.docSubTypeField;
            }
            set
            {
                this.docSubTypeField = value;
                this.RaisePropertyChanged("DocSubType");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string ObjectId
        {
            get
            {
                return this.objectIdField;
            }
            set
            {
                this.objectIdField = value;
                this.RaisePropertyChanged("ObjectId");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string DocSerie
        {
            get
            {
                return this.docSerieField;
            }
            set
            {
                this.docSerieField = value;
                this.RaisePropertyChanged("DocSerie");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string DocNumber
        {
            get
            {
                return this.docNumberField;
            }
            set
            {
                this.docNumberField = value;
                this.RaisePropertyChanged("DocNumber");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 5)]
        public System.Nullable<System.DateTime> DocDate
        {
            get
            {
                return this.docDateField;
            }
            set
            {
                this.docDateField = value;
                this.RaisePropertyChanged("DocDate");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DocDateSpecified
        {
            get
            {
                return this.docDateFieldSpecified;
            }
            set
            {
                this.docDateFieldSpecified = value;
                this.RaisePropertyChanged("DocDateSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 6)]
        public System.Nullable<System.DateTime> ValidityPeriod
        {
            get
            {
                return this.validityPeriodField;
            }
            set
            {
                this.validityPeriodField = value;
                this.RaisePropertyChanged("ValidityPeriod");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValidityPeriodSpecified
        {
            get
            {
                return this.validityPeriodFieldSpecified;
            }
            set
            {
                this.validityPeriodFieldSpecified = value;
                this.RaisePropertyChanged("ValidityPeriodSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public string WhoSign
        {
            get
            {
                return this.whoSignField;
            }
            set
            {
                this.whoSignField = value;
                this.RaisePropertyChanged("WhoSign");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 8)]
        public System.Nullable<int> ListCount
        {
            get
            {
                return this.listCountField;
            }
            set
            {
                this.listCountField = value;
                this.RaisePropertyChanged("ListCount");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ListCountSpecified
        {
            get
            {
                return this.listCountFieldSpecified;
            }
            set
            {
                this.listCountFieldSpecified = value;
                this.RaisePropertyChanged("ListCountSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 9)]
        public System.Nullable<int> CopyCount
        {
            get
            {
                return this.copyCountField;
            }
            set
            {
                this.copyCountField = value;
                this.RaisePropertyChanged("CopyCount");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CopyCountSpecified
        {
            get
            {
                return this.copyCountFieldSpecified;
            }
            set
            {
                this.copyCountFieldSpecified = value;
                this.RaisePropertyChanged("CopyCountSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public string DivisionCode
        {
            get
            {
                return this.divisionCodeField;
            }
            set
            {
                this.divisionCodeField = value;
                this.RaisePropertyChanged("DivisionCode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 11)]
        public Note[] DocNotes
        {
            get
            {
                return this.docNotesField;
            }
            set
            {
                this.docNotesField = value;
                this.RaisePropertyChanged("DocNotes");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 12)]
        public CoordinateFileReference[] DocFiles
        {
            get
            {
                return this.docFilesField;
            }
            set
            {
                this.docFilesField = value;
                this.RaisePropertyChanged("DocFiles");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        public System.Xml.XmlElement CustomAttributes
        {
            get
            {
                return this.customAttributesField;
            }
            set
            {
                this.customAttributesField = value;
                this.RaisePropertyChanged("CustomAttributes");
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
