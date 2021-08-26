using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RestChild.Comon.Exchange.PassportRegistration
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class ServiceProperties
    {
        private string snilsField;

        private string passport_serieField;

        private string passport_dateField;

        private string passportField;

        private string middlenameField;

        private string lastnameField;

        private string firstnameField;

        private byte doctypeField;

        private string birthdateField;

        private string address2_stateorprovinceField;

        private string address1_townField;

        private string address1_stateorprovinceField;

        private string address1_line4Field;

        private string address1_line3Field;

        private string address1_line2Field;

        private string address1_line1Field;

        private string address1_countyField;

        /// <remarks/>
        [XmlElement(Namespace = "")]
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

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string passport_serie
        {
            get
            {
                return this.passport_serieField;
            }
            set
            {
                this.passport_serieField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string passport_date
        {
            get
            {
                return this.passport_dateField;
            }
            set
            {
                this.passport_dateField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string passport
        {
            get
            {
                return this.passportField;
            }
            set
            {
                this.passportField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string middlename
        {
            get
            {
                return this.middlenameField;
            }
            set
            {
                this.middlenameField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string lastname
        {
            get
            {
                return this.lastnameField;
            }
            set
            {
                this.lastnameField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
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
        [XmlElement(Namespace = "")]
        public byte doctype
        {
            get
            {
                return this.doctypeField;
            }
            set
            {
                this.doctypeField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string birthdate
        {
            get
            {
                return this.birthdateField;
            }
            set
            {
                this.birthdateField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string address2_stateorprovince
        {
            get
            {
                return this.address2_stateorprovinceField;
            }
            set
            {
                this.address2_stateorprovinceField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string address1_town
        {
            get
            {
                return this.address1_townField;
            }
            set
            {
                this.address1_townField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string address1_stateorprovince
        {
            get
            {
                return this.address1_stateorprovinceField;
            }
            set
            {
                this.address1_stateorprovinceField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string address1_line4
        {
            get
            {
                return this.address1_line4Field;
            }
            set
            {
                this.address1_line4Field = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string address1_line3
        {
            get
            {
                return this.address1_line3Field;
            }
            set
            {
                this.address1_line3Field = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string address1_line2
        {
            get
            {
                return this.address1_line2Field;
            }
            set
            {
                this.address1_line2Field = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string address1_line1
        {
            get
            {
                return this.address1_line1Field;
            }
            set
            {
                this.address1_line1Field = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "")]
        public string address1_county
        {
            get
            {
                return this.address1_countyField;
            }
            set
            {
                this.address1_countyField = value;
            }
        }
    }
}
