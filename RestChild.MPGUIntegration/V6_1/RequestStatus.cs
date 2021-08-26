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
   public partial class RequestStatus : object, System.ComponentModel.INotifyPropertyChanged
   {

      private StatusType statusField;

      private string reasonTextField;

      private System.Nullable<System.DateTime> validityPeriodField;

      private bool validityPeriodFieldSpecified;

      private Person responsibleField;

      private Department departmentField;

      private DictionaryItem reasonField;

      private Department createdByDepartmentField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
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
      [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
      public string ReasonText
      {
         get
         {
            return this.reasonTextField;
         }
         set
         {
            this.reasonTextField = value;
            this.RaisePropertyChanged("ReasonText");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 2)]
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
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 4)]
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
      [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
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
      [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
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
