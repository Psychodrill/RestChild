using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace RestChild.MPGUIntegration.V61
{
   [GeneratedCode("System.Xml", "4.7.3190.0")]
   [Serializable]
   [DebuggerStepThrough]
   [DesignerCategory("code")]
   [XmlType(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]

   public partial class CoordinateStatusData : object, System.ComponentModel.INotifyPropertyChanged
   {

      private System.Nullable<System.DateTime> responseDateField;

      private bool responseDateFieldSpecified;

      private System.Nullable<System.DateTime> planDateField;

      private bool planDateFieldSpecified;

      private StatusType statusField;

      private Person responsibleField;

      private ServiceDocument[] documentsField;

      private BaseDeclarant[] contactsField;

      private string noteField;

      private RequestResult resultField;

      private string serviceNumberField;

      private DictionaryItem reasonField;

      private Department departmentField;

      private Department createdByDepartmentField;

      private string statusIdField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 0)]
      public System.Nullable<System.DateTime> ResponseDate
      {
         get
         {
            return this.responseDateField;
         }
         set
         {
            this.responseDateField = value;
            this.RaisePropertyChanged("ResponseDate");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlIgnoreAttribute()]
      public bool ResponseDateSpecified
      {
         get
         {
            return this.responseDateFieldSpecified;
         }
         set
         {
            this.responseDateFieldSpecified = value;
            this.RaisePropertyChanged("ResponseDateSpecified");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 1)]
      public System.Nullable<System.DateTime> PlanDate
      {
         get
         {
            return this.planDateField;
         }
         set
         {
            this.planDateField = value;
            this.RaisePropertyChanged("PlanDate");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlIgnoreAttribute()]
      public bool PlanDateSpecified
      {
         get
         {
            return this.planDateFieldSpecified;
         }
         set
         {
            this.planDateFieldSpecified = value;
            this.RaisePropertyChanged("PlanDateSpecified");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
      public StatusType Status
      {
         get
         {
            return this.statusField;
         }
         set
         {
            this.statusField = value;
            this.RaisePropertyChanged("Status");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 3)]
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
      [System.Xml.Serialization.XmlArrayAttribute(Order = 4)]
      public ServiceDocument[] Documents
      {
         get
         {
            return this.documentsField;
         }
         set
         {
            this.documentsField = value;
            this.RaisePropertyChanged("Documents");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlArrayAttribute(Order = 5)]
      public BaseDeclarant[] Contacts
      {
         get
         {
            return this.contactsField;
         }
         set
         {
            this.contactsField = value;
            this.RaisePropertyChanged("Contacts");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
      public string Note
      {
         get
         {
            return this.noteField;
         }
         set
         {
            this.noteField = value;
            this.RaisePropertyChanged("Note");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
      public RequestResult Result
      {
         get
         {
            return this.resultField;
         }
         set
         {
            this.resultField = value;
            this.RaisePropertyChanged("Result");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
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
      [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
      public DictionaryItem Reason
      {
         get
         {
            return this.reasonField;
         }
         set
         {
            this.reasonField = value;
            this.RaisePropertyChanged("Reason");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 10)]
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
      [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
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
      [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
      public string StatusId
      {
         get
         {
            return this.statusIdField;
         }
         set
         {
            this.statusIdField = value;
            this.RaisePropertyChanged("StatusId");
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
