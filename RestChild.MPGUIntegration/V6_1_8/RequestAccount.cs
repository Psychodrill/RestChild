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
    public partial class RequestAccount : BaseDeclarant
    {

        private string fullNameField;

        private string nameField;

        private string brandNameField;

        private string brandField;

        private string ogrnField;

        private string ogrnAuthorityField;

        private string ogrnAuthorityAddressField;

        private string ogrnNumField;

        private System.Nullable<System.DateTime> ogrnDateField;

        private bool ogrnDateFieldSpecified;

        private string innField;

        private string innAuthorityField;

        private string innAuthorityAddressField;

        private string innNumField;

        private System.Nullable<System.DateTime> innDateField;

        private bool innDateFieldSpecified;

        private string kppField;

        private string okpoField;

        private DictionaryItem organizationFormField;

        private Address postalAddressField;

        private Address factAddressField;

        private RequestContact orgHeadField;

        private RequestContact orgContactField;

        private string okvedField;

        private string okfsField;

        private string bankNameField;

        private string bankBikField;

        private string corrAccountField;

        private string setAccountField;

        private string phoneField;

        private string faxField;

        private string eMailField;

        private string webSiteField;

        private AccountType organizationTypeField;

        private string corpIdField;

        private string ssoIdField;

        private string mdmIdField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string FullName
        {
            get
            {
                return this.fullNameField;
            }
            set
            {
                this.fullNameField = value;
                this.RaisePropertyChanged("FullName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
                this.RaisePropertyChanged("Name");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string BrandName
        {
            get
            {
                return this.brandNameField;
            }
            set
            {
                this.brandNameField = value;
                this.RaisePropertyChanged("BrandName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string Brand
        {
            get
            {
                return this.brandField;
            }
            set
            {
                this.brandField = value;
                this.RaisePropertyChanged("Brand");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string Ogrn
        {
            get
            {
                return this.ogrnField;
            }
            set
            {
                this.ogrnField = value;
                this.RaisePropertyChanged("Ogrn");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string OgrnAuthority
        {
            get
            {
                return this.ogrnAuthorityField;
            }
            set
            {
                this.ogrnAuthorityField = value;
                this.RaisePropertyChanged("OgrnAuthority");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string OgrnAuthorityAddress
        {
            get
            {
                return this.ogrnAuthorityAddressField;
            }
            set
            {
                this.ogrnAuthorityAddressField = value;
                this.RaisePropertyChanged("OgrnAuthorityAddress");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public string OgrnNum
        {
            get
            {
                return this.ogrnNumField;
            }
            set
            {
                this.ogrnNumField = value;
                this.RaisePropertyChanged("OgrnNum");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 8)]
        public System.Nullable<System.DateTime> OgrnDate
        {
            get
            {
                return this.ogrnDateField;
            }
            set
            {
                this.ogrnDateField = value;
                this.RaisePropertyChanged("OgrnDate");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OgrnDateSpecified
        {
            get
            {
                return this.ogrnDateFieldSpecified;
            }
            set
            {
                this.ogrnDateFieldSpecified = value;
                this.RaisePropertyChanged("OgrnDateSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public string Inn
        {
            get
            {
                return this.innField;
            }
            set
            {
                this.innField = value;
                this.RaisePropertyChanged("Inn");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public string InnAuthority
        {
            get
            {
                return this.innAuthorityField;
            }
            set
            {
                this.innAuthorityField = value;
                this.RaisePropertyChanged("InnAuthority");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public string InnAuthorityAddress
        {
            get
            {
                return this.innAuthorityAddressField;
            }
            set
            {
                this.innAuthorityAddressField = value;
                this.RaisePropertyChanged("InnAuthorityAddress");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        public string InnNum
        {
            get
            {
                return this.innNumField;
            }
            set
            {
                this.innNumField = value;
                this.RaisePropertyChanged("InnNum");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 13)]
        public System.Nullable<System.DateTime> InnDate
        {
            get
            {
                return this.innDateField;
            }
            set
            {
                this.innDateField = value;
                this.RaisePropertyChanged("InnDate");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool InnDateSpecified
        {
            get
            {
                return this.innDateFieldSpecified;
            }
            set
            {
                this.innDateFieldSpecified = value;
                this.RaisePropertyChanged("InnDateSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
        public string Kpp
        {
            get
            {
                return this.kppField;
            }
            set
            {
                this.kppField = value;
                this.RaisePropertyChanged("Kpp");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
        public string Okpo
        {
            get
            {
                return this.okpoField;
            }
            set
            {
                this.okpoField = value;
                this.RaisePropertyChanged("Okpo");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
        public DictionaryItem OrganizationForm
        {
            get
            {
                return this.organizationFormField;
            }
            set
            {
                this.organizationFormField = value;
                this.RaisePropertyChanged("OrganizationForm");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
        public Address PostalAddress
        {
            get
            {
                return this.postalAddressField;
            }
            set
            {
                this.postalAddressField = value;
                this.RaisePropertyChanged("PostalAddress");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
        public Address FactAddress
        {
            get
            {
                return this.factAddressField;
            }
            set
            {
                this.factAddressField = value;
                this.RaisePropertyChanged("FactAddress");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 19)]
        public RequestContact OrgHead
        {
            get
            {
                return this.orgHeadField;
            }
            set
            {
                this.orgHeadField = value;
                this.RaisePropertyChanged("OrgHead");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 20)]
        public RequestContact OrgContact
        {
            get
            {
                return this.orgContactField;
            }
            set
            {
                this.orgContactField = value;
                this.RaisePropertyChanged("OrgContact");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 21)]
        public string Okved
        {
            get
            {
                return this.okvedField;
            }
            set
            {
                this.okvedField = value;
                this.RaisePropertyChanged("Okved");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 22)]
        public string Okfs
        {
            get
            {
                return this.okfsField;
            }
            set
            {
                this.okfsField = value;
                this.RaisePropertyChanged("Okfs");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 23)]
        public string BankName
        {
            get
            {
                return this.bankNameField;
            }
            set
            {
                this.bankNameField = value;
                this.RaisePropertyChanged("BankName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 24)]
        public string BankBik
        {
            get
            {
                return this.bankBikField;
            }
            set
            {
                this.bankBikField = value;
                this.RaisePropertyChanged("BankBik");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 25)]
        public string CorrAccount
        {
            get
            {
                return this.corrAccountField;
            }
            set
            {
                this.corrAccountField = value;
                this.RaisePropertyChanged("CorrAccount");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 26)]
        public string SetAccount
        {
            get
            {
                return this.setAccountField;
            }
            set
            {
                this.setAccountField = value;
                this.RaisePropertyChanged("SetAccount");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 27)]
        public string Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
                this.RaisePropertyChanged("Phone");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 28)]
        public string Fax
        {
            get
            {
                return this.faxField;
            }
            set
            {
                this.faxField = value;
                this.RaisePropertyChanged("Fax");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 29)]
        public string EMail
        {
            get
            {
                return this.eMailField;
            }
            set
            {
                this.eMailField = value;
                this.RaisePropertyChanged("EMail");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 30)]
        public string WebSite
        {
            get
            {
                return this.webSiteField;
            }
            set
            {
                this.webSiteField = value;
                this.RaisePropertyChanged("WebSite");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 31)]
        public AccountType OrganizationType
        {
            get
            {
                return this.organizationTypeField;
            }
            set
            {
                this.organizationTypeField = value;
                this.RaisePropertyChanged("OrganizationType");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 32)]
        public string CorpId
        {
            get
            {
                return this.corpIdField;
            }
            set
            {
                this.corpIdField = value;
                this.RaisePropertyChanged("CorpId");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 33)]
        public string SsoId
        {
            get
            {
                return this.ssoIdField;
            }
            set
            {
                this.ssoIdField = value;
                this.RaisePropertyChanged("SsoId");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 34)]
        public string MdmId
        {
            get
            {
                return this.mdmIdField;
            }
            set
            {
                this.mdmIdField = value;
                this.RaisePropertyChanged("MdmId");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                this.RaisePropertyChanged("Id");
            }
        }
    }
}
