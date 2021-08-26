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
   public partial class RequestDocumentsData : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string requestIdField;

      private System.Nullable<System.DateTime> responseDateField;

      private bool responseDateFieldSpecified;

      private System.Nullable<System.DateTime> planDateField;

      private bool planDateFieldSpecified;

      private StatusType statusField;

      private Person responsibleField;

      private RequestDocument[] documentsField;

      private string noteField;

      private string serviceNumberField;

      private string reasonCodeField;

      private Department departmentField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
      public string RequestId
      {
         get
         {
            return this.requestIdField;
         }
         set
         {
            this.requestIdField = value;
            this.RaisePropertyChanged("RequestId");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 1)]
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
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 2)]
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
      [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
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
      [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
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
      [System.Xml.Serialization.XmlElementAttribute("Documents", Order = 5)]
      public RequestDocument[] Documents
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
      [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
      public string ReasonCode
      {
         get
         {
            return this.reasonCodeField;
         }
         set
         {
            this.reasonCodeField = value;
            this.RaisePropertyChanged("ReasonCode");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
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
