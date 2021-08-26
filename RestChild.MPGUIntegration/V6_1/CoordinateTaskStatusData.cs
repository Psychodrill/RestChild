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
   public partial class CoordinateTaskStatusData
   {

       private string messageIdField;

       private string taskIdField;

       private StatusType statusField;

       private string statusNoteField;

       private string asyncTicketField;

       private TaskResult resultField;

       private System.Xml.XmlAttribute[] anyAttrField;

       /// <remarks/>
       public string MessageId {
           get {
               return this.messageIdField;
           }
           set {
               this.messageIdField = value;
           }
       }

       /// <remarks/>
       public string TaskId {
           get {
               return this.taskIdField;
           }
           set {
               this.taskIdField = value;
           }
       }

       /// <remarks/>
       public StatusType Status {
           get {
               return this.statusField;
           }
           set {
               this.statusField = value;
           }
       }

       /// <remarks/>
       public string StatusNote {
           get {
               return this.statusNoteField;
           }
           set {
               this.statusNoteField = value;
           }
       }

       /// <remarks/>
       public string AsyncTicket {
           get {
               return this.asyncTicketField;
           }
           set {
               this.asyncTicketField = value;
           }
       }

       /// <remarks/>
       public TaskResult Result {
           get {
               return this.resultField;
           }
           set {
               this.resultField = value;
           }
       }

       /// <remarks/>
       [System.Xml.Serialization.XmlAnyAttributeAttribute()]
       public System.Xml.XmlAttribute[] AnyAttr {
           get {
               return this.anyAttrField;
           }
           set {
               this.anyAttrField = value;
           }
       }
   }
}
