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
    public partial class TaskDataType : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string documentTypeCodeField;

        private string parameterTypeCodeField;

        private System.Xml.XmlElement parameterField;

        private bool includeXmlViewField;

        private bool includeBinaryViewField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string DocumentTypeCode
        {
            get
            {
                return this.documentTypeCodeField;
            }
            set
            {
                this.documentTypeCodeField = value;
                this.RaisePropertyChanged("DocumentTypeCode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string ParameterTypeCode
        {
            get
            {
                return this.parameterTypeCodeField;
            }
            set
            {
                this.parameterTypeCodeField = value;
                this.RaisePropertyChanged("ParameterTypeCode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public System.Xml.XmlElement Parameter
        {
            get
            {
                return this.parameterField;
            }
            set
            {
                this.parameterField = value;
                this.RaisePropertyChanged("Parameter");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public bool IncludeXmlView
        {
            get
            {
                return this.includeXmlViewField;
            }
            set
            {
                this.includeXmlViewField = value;
                this.RaisePropertyChanged("IncludeXmlView");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public bool IncludeBinaryView
        {
            get
            {
                return this.includeBinaryViewField;
            }
            set
            {
                this.includeBinaryViewField = value;
                this.RaisePropertyChanged("IncludeBinaryView");
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
