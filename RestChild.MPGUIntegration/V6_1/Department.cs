﻿using System;
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
   public partial class Department : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string nameField;

      private string codeField;

      private string innField;

      private string ogrnField;

      private System.Nullable<System.DateTime> regDateField;

      private string systemCodeField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 0)]
      public string Name
      {
         get
         {
            return this.nameField;
         }
         set
         {
            this.nameField = value;
            this.RaisePropertyChanged("Name");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 1)]
      public string Code
      {
         get
         {
            return this.codeField;
         }
         set
         {
            this.codeField = value;
            this.RaisePropertyChanged("Code");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 2)]
      public string Inn
      {
         get
         {
            return this.innField;
         }
         set
         {
            this.innField = value;
            this.RaisePropertyChanged("Inn");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 3)]
      public string Ogrn
      {
         get
         {
            return this.ogrnField;
         }
         set
         {
            this.ogrnField = value;
            this.RaisePropertyChanged("Ogrn");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 4)]
      public System.Nullable<System.DateTime> RegDate
      {
         get
         {
            return this.regDateField;
         }
         set
         {
            this.regDateField = value;
            this.RaisePropertyChanged("RegDate");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 5)]
      public string SystemCode
      {
         get
         {
            return this.systemCodeField;
         }
         set
         {
            this.systemCodeField = value;
            this.RaisePropertyChanged("SystemCode");
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
