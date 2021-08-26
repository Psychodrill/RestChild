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
   public partial class Headers : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string fromSystemCodeField;

      private string toSystemCodeField;

      private string messageIdField;

      private string relatesToField;

      private System.DateTime requestDateTimeField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
      public string FromSystemCode
      {
         get
         {
            return this.fromSystemCodeField;
         }
         set
         {
            this.fromSystemCodeField = value;
            this.RaisePropertyChanged("FromSystemCode");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
      public string ToSystemCode
      {
         get
         {
            return this.toSystemCodeField;
         }
         set
         {
            this.toSystemCodeField = value;
            this.RaisePropertyChanged("ToSystemCode");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
      public string MessageId
      {
         get
         {
            return this.messageIdField;
         }
         set
         {
            this.messageIdField = value;
            this.RaisePropertyChanged("MessageId");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
      public string RelatesTo
      {
         get
         {
            return this.relatesToField;
         }
         set
         {
            this.relatesToField = value;
            this.RaisePropertyChanged("RelatesTo");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
      public System.DateTime RequestDateTime
      {
         get
         {
            return this.requestDateTimeField;
         }
         set
         {
            this.requestDateTimeField = value;
            this.RaisePropertyChanged("RequestDateTime");
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
