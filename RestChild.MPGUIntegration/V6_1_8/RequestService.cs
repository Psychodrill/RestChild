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
    public partial class RequestService : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string regNumField;

        private System.Nullable<System.DateTime> regDateField;

        private string serviceNumberField;

        private System.Nullable<decimal> servicePriceField;

        private bool servicePriceFieldSpecified;

        private System.Nullable<System.DateTime> prepareTargetDateField;

        private bool prepareTargetDateFieldSpecified;

        private System.Nullable<System.DateTime> outputTargetDateField;

        private bool outputTargetDateFieldSpecified;

        private Person responsibleField;

        private Department departmentField;

        private DictionaryItem[] declineReasonsField;

        private Department createdByDepartmentField;

        private System.Nullable<System.DateTime> prepareFactDateField;

        private bool prepareFactDateFieldSpecified;

        private System.Nullable<System.DateTime> outputFactDateField;

        private bool outputFactDateFieldSpecified;

        private System.Nullable<OutputKindType> outputKindField;

        private string portalNumField;

        private string parentServiceNumberField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string RegNum
        {
            get
            {
                return this.regNumField;
            }
            set
            {
                this.regNumField = value;
                this.RaisePropertyChanged("RegNum");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 1)]
        public System.Nullable<System.DateTime> RegDate
        {
            get
            {
                return this.regDateField;
            }
            set
            {
                this.regDateField = value;
                this.RaisePropertyChanged("RegDate");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string ServiceNumber
        {
            get
            {
                return this.serviceNumberField;
            }
            set
            {
                this.serviceNumberField = value;
                this.RaisePropertyChanged("ServiceNumber");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 3)]
        public System.Nullable<decimal> ServicePrice
        {
            get
            {
                return this.servicePriceField;
            }
            set
            {
                this.servicePriceField = value;
                this.RaisePropertyChanged("ServicePrice");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ServicePriceSpecified
        {
            get
            {
                return this.servicePriceFieldSpecified;
            }
            set
            {
                this.servicePriceFieldSpecified = value;
                this.RaisePropertyChanged("ServicePriceSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 4)]
        public System.Nullable<System.DateTime> PrepareTargetDate
        {
            get
            {
                return this.prepareTargetDateField;
            }
            set
            {
                this.prepareTargetDateField = value;
                this.RaisePropertyChanged("PrepareTargetDate");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PrepareTargetDateSpecified
        {
            get
            {
                return this.prepareTargetDateFieldSpecified;
            }
            set
            {
                this.prepareTargetDateFieldSpecified = value;
                this.RaisePropertyChanged("PrepareTargetDateSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 5)]
        public System.Nullable<System.DateTime> OutputTargetDate
        {
            get
            {
                return this.outputTargetDateField;
            }
            set
            {
                this.outputTargetDateField = value;
                this.RaisePropertyChanged("OutputTargetDate");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OutputTargetDateSpecified
        {
            get
            {
                return this.outputTargetDateFieldSpecified;
            }
            set
            {
                this.outputTargetDateFieldSpecified = value;
                this.RaisePropertyChanged("OutputTargetDateSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 6)]
        public Person Responsible
        {
            get
            {
                return this.responsibleField;
            }
            set
            {
                this.responsibleField = value;
                this.RaisePropertyChanged("Responsible");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 7)]
        public Department Department
        {
            get
            {
                return this.departmentField;
            }
            set
            {
                this.departmentField = value;
                this.RaisePropertyChanged("Department");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 8)]
        public DictionaryItem[] DeclineReasons
        {
            get
            {
                return this.declineReasonsField;
            }
            set
            {
                this.declineReasonsField = value;
                this.RaisePropertyChanged("DeclineReasons");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public Department CreatedByDepartment
        {
            get
            {
                return this.createdByDepartmentField;
            }
            set
            {
                this.createdByDepartmentField = value;
                this.RaisePropertyChanged("CreatedByDepartment");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 10)]
        public System.Nullable<System.DateTime> PrepareFactDate
        {
            get
            {
                return this.prepareFactDateField;
            }
            set
            {
                this.prepareFactDateField = value;
                this.RaisePropertyChanged("PrepareFactDate");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PrepareFactDateSpecified
        {
            get
            {
                return this.prepareFactDateFieldSpecified;
            }
            set
            {
                this.prepareFactDateFieldSpecified = value;
                this.RaisePropertyChanged("PrepareFactDateSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 11)]
        public System.Nullable<System.DateTime> OutputFactDate
        {
            get
            {
                return this.outputFactDateField;
            }
            set
            {
                this.outputFactDateField = value;
                this.RaisePropertyChanged("OutputFactDate");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OutputFactDateSpecified
        {
            get
            {
                return this.outputFactDateFieldSpecified;
            }
            set
            {
                this.outputFactDateFieldSpecified = value;
                this.RaisePropertyChanged("OutputFactDateSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 12)]
        public System.Nullable<OutputKindType> OutputKind
        {
            get
            {
                return this.outputKindField;
            }
            set
            {
                this.outputKindField = value;
                this.RaisePropertyChanged("OutputKind");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        public string PortalNum
        {
            get
            {
                return this.portalNumField;
            }
            set
            {
                this.portalNumField = value;
                this.RaisePropertyChanged("PortalNum");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
        public string ParentServiceNumber
        {
            get
            {
                return this.parentServiceNumberField;
            }
            set
            {
                this.parentServiceNumberField = value;
                this.RaisePropertyChanged("ParentServiceNumber");
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
