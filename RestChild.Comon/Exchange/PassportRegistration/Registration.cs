using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RestChild.Comon.Exchange.PassportRegistration
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://asur/fms/")]
    [XmlRoot(Namespace = "http://asur/fms/", IsNullable = false)]
    public class Registration
    {
        private string docTypeField;

        private string documentStatusField;

        private string dateTillField;

        private string locationFlatField;

        private string locationBuildingField;

        private string locationHouseField;

        private string locationStreetField;

        private string locationCityField;

        private string locationDistrictField;

        private string locationRegionField;

        private string documentDateField;

        private string documentNumberField;

        private string documentSeriesField;

        private string registrationTypeField;

        private string regionCodeField;

        private string sNILSField;

        private string birthDateField;

        private string givenNameField;

        private string firstNameField;

        private string lastNameField;

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string DocType
        {
            get
            {
                return this.docTypeField;
            }
            set
            {
                this.docTypeField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string DocumentStatus
        {
            get
            {
                return this.documentStatusField;
            }
            set
            {
                this.documentStatusField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string DateTill
        {
            get
            {
                return this.dateTillField;
            }
            set
            {
                this.dateTillField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string LocationFlat
        {
            get
            {
                return this.locationFlatField;
            }
            set
            {
                this.locationFlatField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string LocationBuilding
        {
            get
            {
                return this.locationBuildingField;
            }
            set
            {
                this.locationBuildingField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string LocationHouse
        {
            get
            {
                return this.locationHouseField;
            }
            set
            {
                this.locationHouseField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string LocationStreet
        {
            get
            {
                return this.locationStreetField;
            }
            set
            {
                this.locationStreetField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string LocationCity
        {
            get
            {
                return this.locationCityField;
            }
            set
            {
                this.locationCityField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string LocationDistrict
        {
            get
            {
                return this.locationDistrictField;
            }
            set
            {
                this.locationDistrictField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string LocationRegion
        {
            get
            {
                return this.locationRegionField;
            }
            set
            {
                this.locationRegionField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string DocumentDate
        {
            get
            {
                return this.documentDateField;
            }
            set
            {
                this.documentDateField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string DocumentNumber
        {
            get
            {
                return this.documentNumberField;
            }
            set
            {
                this.documentNumberField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string DocumentSeries
        {
            get
            {
                return this.documentSeriesField;
            }
            set
            {
                this.documentSeriesField = value;
            }
        }


        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string RegistrationType
        {
            get
            {
                return this.registrationTypeField;
            }
            set
            {
                this.registrationTypeField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string RegionCode
        {
            get
            {
                return this.regionCodeField;
            }
            set
            {
                this.regionCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string SNILS
        {
            get
            {
                return this.sNILSField;
            }
            set
            {
                this.sNILSField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string BirthDate
        {
            get
            {
                return this.birthDateField;
            }
            set
            {
                this.birthDateField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string GivenName
        {
            get
            {
                return this.givenNameField;
            }
            set
            {
                this.givenNameField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string FirstName
        {
            get
            {
                return this.firstNameField;
            }
            set
            {
                this.firstNameField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string LastName
        {
            get
            {
                return this.lastNameField;
            }
            set
            {
                this.lastNameField = value;
            }
        }
    }
}
