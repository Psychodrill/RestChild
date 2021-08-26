using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.MPGUIntegration.V61
{
   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3190.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
   public partial class StatusType : object, System.ComponentModel.INotifyPropertyChanged
   {

      private int statusCodeField;

      private string statusTitleField;

      private System.DateTime statusDateField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
      public int StatusCode
      {
         get
         {
            return this.statusCodeField;
         }
         set
         {
            this.statusCodeField = value;
            this.RaisePropertyChanged("StatusCode");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
      public string StatusTitle
      {
         get
         {
            return this.statusTitleField;
         }
         set
         {
            this.statusTitleField = value;
            this.RaisePropertyChanged("StatusTitle");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
      public System.DateTime StatusDate
      {
         get
         {
            return this.statusDateField;
         }
         set
         {
            this.statusDateField = value;
            this.RaisePropertyChanged("StatusDate");
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
