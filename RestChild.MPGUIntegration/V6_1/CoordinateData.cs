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
   public partial class CoordinateData : object, System.ComponentModel.INotifyPropertyChanged
   {

      private RequestService serviceField;

      private RequestServiceForSign signServiceField;

      private object[] signatureField;

      private RequestStatus statusField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
      public RequestService Service
      {
         get
         {
            return this.serviceField;
         }
         set
         {
            this.serviceField = value;
            this.RaisePropertyChanged("Service");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
      public RequestServiceForSign SignService
      {
         get
         {
            return this.signServiceField;
         }
         set
         {
            this.signServiceField = value;
            this.RaisePropertyChanged("SignService");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("Signature", Order = 2)]
      public object[] Signature
      {
         get
         {
            return this.signatureField;
         }
         set
         {
            this.signatureField = value;
            this.RaisePropertyChanged("Signature");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
      public RequestStatus Status
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
