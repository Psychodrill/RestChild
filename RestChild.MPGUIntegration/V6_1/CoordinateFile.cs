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
   public partial class CoordinateFile : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string idField;

      private string fileIdInStoreField;

      private string storeNameField;

      private string fileNameField;

      private byte[] cmsSignatureField;

      private object[] itemsField;

      private string fileLinkField;

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
      public string FileIdInStore
      {
         get
         {
            return this.fileIdInStoreField;
         }
         set
         {
            this.fileIdInStoreField = value;
            this.RaisePropertyChanged("FileIdInStore");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
      public string StoreName
      {
         get
         {
            return this.storeNameField;
         }
         set
         {
            this.storeNameField = value;
            this.RaisePropertyChanged("StoreName");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
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
      [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary", Order = 4)]
      public byte[] CmsSignature
      {
         get
         {
            return this.cmsSignatureField;
         }
         set
         {
            this.cmsSignatureField = value;
            this.RaisePropertyChanged("CmsSignature");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("FileDigest", typeof(DigestType), Order = 5)]
      [System.Xml.Serialization.XmlElementAttribute("FileHash", typeof(byte[]), DataType = "base64Binary", Order = 5)]
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

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
      public string FileLink
      {
         get
         {
            return this.fileLinkField;
         }
         set
         {
            this.fileLinkField = value;
            this.RaisePropertyChanged("FileLink");
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
