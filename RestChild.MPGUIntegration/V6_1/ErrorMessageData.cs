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
   public partial class ErrorMessageData : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string errorCodeField;

      private string errorTextField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
      public string ErrorCode
      {
         get
         {
            return this.errorCodeField;
         }
         set
         {
            this.errorCodeField = value;
            this.RaisePropertyChanged("ErrorCode");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
      public string ErrorText
      {
         get
         {
            return this.errorTextField;
         }
         set
         {
            this.errorTextField = value;
            this.RaisePropertyChanged("ErrorText");
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
