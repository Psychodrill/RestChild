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
   public partial class RequestDocument : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string docCodeField;

      private bool requiredField;

      private string noteField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
      public string DocCode
      {
         get
         {
            return this.docCodeField;
         }
         set
         {
            this.docCodeField = value;
            this.RaisePropertyChanged("DocCode");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
      public bool Required
      {
         get
         {
            return this.requiredField;
         }
         set
         {
            this.requiredField = value;
            this.RaisePropertyChanged("Required");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
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
