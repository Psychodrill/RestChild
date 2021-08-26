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
    public partial class Address : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string countryField;

        private string countryCodeField;

        private string postalCodeField;

        private string localityField;

        private string regionField;

        private string cityField;

        private string townField;

        private string streetField;

        private string houseField;

        private string buildingField;

        private string structureField;

        private string facilityField;

        private string ownershipField;

        private string flatField;

        private string pOBoxField;

        private string okatoField;

        private string kladrCodeField;

        private string kladrStreetCodeField;

        private string bTIDistrictCodeField;

        private string bTIRegionCodeField;

        private string bTIStreetCodeField;

        private string bTIBuildingCodeField;

        private string bTIAltCodeField;

        private string bTIFlatCodeField;

        private string fiasCodeField;

        private string literaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string Country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
                this.RaisePropertyChanged("Country");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string CountryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
                this.RaisePropertyChanged("CountryCode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string PostalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                this.postalCodeField = value;
                this.RaisePropertyChanged("PostalCode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string Locality
        {
            get
            {
                return this.localityField;
            }
            set
            {
                this.localityField = value;
                this.RaisePropertyChanged("Locality");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string Region
        {
            get
            {
                return this.regionField;
            }
            set
            {
                this.regionField = value;
                this.RaisePropertyChanged("Region");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
                this.RaisePropertyChanged("City");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string Town
        {
            get
            {
                return this.townField;
            }
            set
            {
                this.townField = value;
                this.RaisePropertyChanged("Town");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public string Street
        {
            get
            {
                return this.streetField;
            }
            set
            {
                this.streetField = value;
                this.RaisePropertyChanged("Street");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public string House
        {
            get
            {
                return this.houseField;
            }
            set
            {
                this.houseField = value;
                this.RaisePropertyChanged("House");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public string Building
        {
            get
            {
                return this.buildingField;
            }
            set
            {
                this.buildingField = value;
                this.RaisePropertyChanged("Building");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public string Structure
        {
            get
            {
                return this.structureField;
            }
            set
            {
                this.structureField = value;
                this.RaisePropertyChanged("Structure");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public string Facility
        {
            get
            {
                return this.facilityField;
            }
            set
            {
                this.facilityField = value;
                this.RaisePropertyChanged("Facility");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        public string Ownership
        {
            get
            {
                return this.ownershipField;
            }
            set
            {
                this.ownershipField = value;
                this.RaisePropertyChanged("Ownership");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        public string Flat
        {
            get
            {
                return this.flatField;
            }
            set
            {
                this.flatField = value;
                this.RaisePropertyChanged("Flat");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
        public string POBox
        {
            get
            {
                return this.pOBoxField;
            }
            set
            {
                this.pOBoxField = value;
                this.RaisePropertyChanged("POBox");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
        public string Okato
        {
            get
            {
                return this.okatoField;
            }
            set
            {
                this.okatoField = value;
                this.RaisePropertyChanged("Okato");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
        public string KladrCode
        {
            get
            {
                return this.kladrCodeField;
            }
            set
            {
                this.kladrCodeField = value;
                this.RaisePropertyChanged("KladrCode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
        public string KladrStreetCode
        {
            get
            {
                return this.kladrStreetCodeField;
            }
            set
            {
                this.kladrStreetCodeField = value;
                this.RaisePropertyChanged("KladrStreetCode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
        public string BTIDistrictCode
        {
            get
            {
                return this.bTIDistrictCodeField;
            }
            set
            {
                this.bTIDistrictCodeField = value;
                this.RaisePropertyChanged("BTIDistrictCode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 19)]
        public string BTIRegionCode
        {
            get
            {
                return this.bTIRegionCodeField;
            }
            set
            {
                this.bTIRegionCodeField = value;
                this.RaisePropertyChanged("BTIRegionCode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 20)]
        public string BTIStreetCode
        {
            get
            {
                return this.bTIStreetCodeField;
            }
            set
            {
                this.bTIStreetCodeField = value;
                this.RaisePropertyChanged("BTIStreetCode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 21)]
        public string BTIBuildingCode
        {
            get
            {
                return this.bTIBuildingCodeField;
            }
            set
            {
                this.bTIBuildingCodeField = value;
                this.RaisePropertyChanged("BTIBuildingCode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 22)]
        public string BTIAltCode
        {
            get
            {
                return this.bTIAltCodeField;
            }
            set
            {
                this.bTIAltCodeField = value;
                this.RaisePropertyChanged("BTIAltCode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 23)]
        public string BTIFlatCode
        {
            get
            {
                return this.bTIFlatCodeField;
            }
            set
            {
                this.bTIFlatCodeField = value;
                this.RaisePropertyChanged("BTIFlatCode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 24)]
        public string FiasCode
        {
            get
            {
                return this.fiasCodeField;
            }
            set
            {
                this.fiasCodeField = value;
                this.RaisePropertyChanged("FiasCode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 25)]
        public string Litera
        {
            get
            {
                return this.literaField;
            }
            set
            {
                this.literaField = value;
                this.RaisePropertyChanged("Litera");
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
