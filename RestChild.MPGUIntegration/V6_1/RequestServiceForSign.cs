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
   public partial class RequestServiceForSign : object, System.ComponentModel.INotifyPropertyChanged
   {

      private DictionaryItem serviceTypeField;

      private System.Nullable<int> copiesField;

      private bool copiesFieldSpecified;

      private BaseDeclarant[] contactsField;

      private ServiceDocument[] documentsField;

      private System.Xml.XmlElement customAttributesField;

      private string idField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
      public DictionaryItem ServiceType
      {
         get
         {
            return this.serviceTypeField;
         }
         set
         {
            this.serviceTypeField = value;
            this.RaisePropertyChanged("ServiceType");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 1)]
      public System.Nullable<int> Copies
      {
         get
         {
            return this.copiesField;
         }
         set
         {
            this.copiesField = value;
            this.RaisePropertyChanged("Copies");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlIgnoreAttribute()]
      public bool CopiesSpecified
      {
         get
         {
            return this.copiesFieldSpecified;
         }
         set
         {
            this.copiesFieldSpecified = value;
            this.RaisePropertyChanged("CopiesSpecified");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlArrayAttribute(Order = 2)]
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
      [System.Xml.Serialization.XmlArrayAttribute(Order = 3)]
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
      [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
      public System.Xml.XmlElement CustomAttributes
      {
         get
         {
            return this.customAttributesField;
         }
         set
         {
            this.customAttributesField = value;
            this.RaisePropertyChanged("CustomAttributes");
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
