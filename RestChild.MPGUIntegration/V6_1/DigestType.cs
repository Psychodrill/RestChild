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
   public partial class DigestType : object, System.ComponentModel.INotifyPropertyChanged
   {

      private DigestAlghoritm algorithmField;

      private byte[] valueField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
      public DigestAlghoritm Algorithm
      {
         get
         {
            return this.algorithmField;
         }
         set
         {
            this.algorithmField = value;
            this.RaisePropertyChanged("Algorithm");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary", Order = 1)]
      public byte[] Value
      {
         get
         {
            return this.valueField;
         }
         set
         {
            this.valueField = value;
            this.RaisePropertyChanged("Value");
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
