using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.MPGUIntegration.V61
{
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3190.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
   public partial class RequestContact : BaseDeclarant
   {

      private string lastNameField;

      private string firstNameField;

      private string middleNameField;

      private System.Nullable<GenderType> genderField;

      private System.Nullable<System.DateTime> birthDateField;

      private string snilsField;

      private string innField;

      private Address regAddressField;

      private Address factAddressField;

      private string mobilePhoneField;

      private string workPhoneField;

      private string homePhoneField;

      private string eMailField;

      private string nationField;

      private string citizenshipField;

      private System.Nullable<CitizenshipType> citizenshipTypeField;

      private string citizenshipCodeField;

      private Address birthAddressField;

      private string jobTitleField;

      private string oMSNumField;

      private System.Nullable<System.DateTime> oMSDateField;

      private bool oMSDateFieldSpecified;

      private string oMSCompanyField;

      private System.Nullable<System.DateTime> oMSValidityPeriodField;

      private bool oMSValidityPeriodFieldSpecified;

      private string ssoIdField;

      private System.Nullable<RegType> regAddressTypeField;

      private bool regAddressTypeFieldSpecified;

      private string idField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
      public string LastName
      {
         get
         {
            return this.lastNameField;
         }
         set
         {
            this.lastNameField = value;
            this.RaisePropertyChanged("LastName");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
      public string FirstName
      {
         get
         {
            return this.firstNameField;
         }
         set
         {
            this.firstNameField = value;
            this.RaisePropertyChanged("FirstName");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
      public string MiddleName
      {
         get
         {
            return this.middleNameField;
         }
         set
         {
            this.middleNameField = value;
            this.RaisePropertyChanged("MiddleName");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 3)]
      public System.Nullable<GenderType> Gender
      {
         get
         {
            return this.genderField;
         }
         set
         {
            this.genderField = value;
            this.RaisePropertyChanged("Gender");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(DataType = "date", IsNullable = true, Order = 4)]
      public System.Nullable<System.DateTime> BirthDate
      {
         get
         {
            return this.birthDateField;
         }
         set
         {
            this.birthDateField = value;
            this.RaisePropertyChanged("BirthDate");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
      public string Snils
      {
         get
         {
            return this.snilsField;
         }
         set
         {
            this.snilsField = value;
            this.RaisePropertyChanged("Snils");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
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
      [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
      public Address RegAddress
      {
         get
         {
            return this.regAddressField;
         }
         set
         {
            this.regAddressField = value;
            this.RaisePropertyChanged("RegAddress");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
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
      [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
      public string MobilePhone
      {
         get
         {
            return this.mobilePhoneField;
         }
         set
         {
            this.mobilePhoneField = value;
            this.RaisePropertyChanged("MobilePhone");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
      public string WorkPhone
      {
         get
         {
            return this.workPhoneField;
         }
         set
         {
            this.workPhoneField = value;
            this.RaisePropertyChanged("WorkPhone");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
      public string HomePhone
      {
         get
         {
            return this.homePhoneField;
         }
         set
         {
            this.homePhoneField = value;
            this.RaisePropertyChanged("HomePhone");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
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
      [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
      public string Nation
      {
         get
         {
            return this.nationField;
         }
         set
         {
            this.nationField = value;
            this.RaisePropertyChanged("Nation");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
      public string Citizenship
      {
         get
         {
            return this.citizenshipField;
         }
         set
         {
            this.citizenshipField = value;
            this.RaisePropertyChanged("Citizenship");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 15)]
      public System.Nullable<CitizenshipType> CitizenshipType
      {
         get
         {
            return this.citizenshipTypeField;
         }
         set
         {
            this.citizenshipTypeField = value;
            this.RaisePropertyChanged("CitizenshipType");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
      public string CitizenshipCode
      {
         get
         {
            return this.citizenshipCodeField;
         }
         set
         {
            this.citizenshipCodeField = value;
            this.RaisePropertyChanged("CitizenshipCode");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
      public Address BirthAddress
      {
         get
         {
            return this.birthAddressField;
         }
         set
         {
            this.birthAddressField = value;
            this.RaisePropertyChanged("BirthAddress");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
      public string JobTitle
      {
         get
         {
            return this.jobTitleField;
         }
         set
         {
            this.jobTitleField = value;
            this.RaisePropertyChanged("JobTitle");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 19)]
      public string OMSNum
      {
         get
         {
            return this.oMSNumField;
         }
         set
         {
            this.oMSNumField = value;
            this.RaisePropertyChanged("OMSNum");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 20)]
      public System.Nullable<System.DateTime> OMSDate
      {
         get
         {
            return this.oMSDateField;
         }
         set
         {
            this.oMSDateField = value;
            this.RaisePropertyChanged("OMSDate");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlIgnoreAttribute()]
      public bool OMSDateSpecified
      {
         get
         {
            return this.oMSDateFieldSpecified;
         }
         set
         {
            this.oMSDateFieldSpecified = value;
            this.RaisePropertyChanged("OMSDateSpecified");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 21)]
      public string OMSCompany
      {
         get
         {
            return this.oMSCompanyField;
         }
         set
         {
            this.oMSCompanyField = value;
            this.RaisePropertyChanged("OMSCompany");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 22)]
      public System.Nullable<System.DateTime> OMSValidityPeriod
      {
         get
         {
            return this.oMSValidityPeriodField;
         }
         set
         {
            this.oMSValidityPeriodField = value;
            this.RaisePropertyChanged("OMSValidityPeriod");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlIgnoreAttribute()]
      public bool OMSValidityPeriodSpecified
      {
         get
         {
            return this.oMSValidityPeriodFieldSpecified;
         }
         set
         {
            this.oMSValidityPeriodFieldSpecified = value;
            this.RaisePropertyChanged("OMSValidityPeriodSpecified");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 23)]
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
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 24)]
      public System.Nullable<RegType> RegAddressType
      {
         get
         {
            return this.regAddressTypeField;
         }
         set
         {
            this.regAddressTypeField = value;
            this.RaisePropertyChanged("RegAddressType");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlIgnoreAttribute()]
      public bool RegAddressTypeSpecified
      {
         get
         {
            return this.regAddressTypeFieldSpecified;
         }
         set
         {
            this.regAddressTypeFieldSpecified = value;
            this.RaisePropertyChanged("RegAddressTypeSpecified");
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
