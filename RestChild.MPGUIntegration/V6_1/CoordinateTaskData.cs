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
   public partial class CoordinateTaskData : object, System.ComponentModel.INotifyPropertyChanged
   {

      private TaskType taskField;

      private TaskDataType dataField;

      private object signatureField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
      public TaskType Task
      {
         get
         {
            return this.taskField;
         }
         set
         {
            this.taskField = value;
            this.RaisePropertyChanged("Task");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
      public TaskDataType Data
      {
         get
         {
            return this.dataField;
         }
         set
         {
            this.dataField = value;
            this.RaisePropertyChanged("Data");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
      public object Signature
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
