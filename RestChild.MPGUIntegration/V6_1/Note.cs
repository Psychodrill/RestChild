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
   public partial class Note : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string subjectField;

      private string textField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
      public string Subject
      {
         get
         {
            return this.subjectField;
         }
         set
         {
            this.subjectField = value;
            this.RaisePropertyChanged("Subject");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
      public string Text
      {
         get
         {
            return this.textField;
         }
         set
         {
            this.textField = value;
            this.RaisePropertyChanged("Text");
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
