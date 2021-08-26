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
   public partial class CoordinateFileReference : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string idField;

      private string fileNameField;

      private object[] itemsField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
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

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
      public string FileName
      {
         get
         {
            return this.fileNameField;
         }
         set
         {
            this.fileNameField = value;
            this.RaisePropertyChanged("FileName");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("FileDigest", typeof(DigestType), Order = 2)]
      [System.Xml.Serialization.XmlElementAttribute("FileHash", typeof(byte[]), DataType = "base64Binary", IsNullable = true, Order = 2)]
      public object[] Items
      {
         get
         {
            return this.itemsField;
         }
         set
         {
            this.itemsField = value;
            this.RaisePropertyChanged("Items");
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
