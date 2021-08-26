using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Xml.Serialization;

namespace RestChild.MPGUIntegration.V61
{
   [Serializable]
   [DebuggerStepThrough]
   [GeneratedCode("System.ServiceModel", "4.0.0.0")]
   [EditorBrowsable(EditorBrowsableState.Advanced)]
   [MessageContract(WrapperName = "CoordinateMessage", WrapperNamespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", IsWrapped = true)]
   [XmlType(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
   [XmlRoot("CoordinateMessage", Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
   public partial class CoordinateMessage
   {
      [System.ServiceModel.MessageHeaderAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
      public Headers ServiceHeader;

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 0)]
      public CoordinateData CoordinateDataMessage;

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 1)]
      public CoordinateFile[] Files;

      public CoordinateMessage()
      {
      }

      public CoordinateMessage(Headers ServiceHeader, CoordinateData CoordinateDataMessage, CoordinateFile[] Files)
      {
         this.ServiceHeader = ServiceHeader;
         this.CoordinateDataMessage = CoordinateDataMessage;
         this.Files = Files;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(WrapperName = "SendRequestsMessage", WrapperNamespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", IsWrapped = true)]
   public partial class SendRequestsMessage
   {

      [System.ServiceModel.MessageHeaderAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
      public Headers ServiceHeader;

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 0)]
      public CoordinateData[] CoordinateDataMessages;

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 1)]
      public CoordinateFile[] Files;

      public SendRequestsMessage()
      {
      }

      public SendRequestsMessage(Headers ServiceHeader, CoordinateData[] CoordinateDataMessages, CoordinateFile[] Files)
      {
         this.ServiceHeader = ServiceHeader;
         this.CoordinateDataMessages = CoordinateDataMessages;
         this.Files = Files;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(WrapperName = "CoordinateTaskMessage", WrapperNamespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", IsWrapped = true)]
   public partial class CoordinateTaskMessage
   {

      [System.ServiceModel.MessageHeaderAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
      public Headers ServiceHeader;

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 0)]
      public CoordinateTaskData CoordinateTaskDataMessage;

      [System.ServiceModel.MessageBodyMemberAttribute(Name = "ServiceHeader", Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 1)]
      public Headers ServiceHeader1;

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 2)]
      public CoordinateFile[] Files;

      public CoordinateTaskMessage()
      {
      }

      public CoordinateTaskMessage(Headers ServiceHeader, CoordinateTaskData CoordinateTaskDataMessage, Headers ServiceHeader1, CoordinateFile[] Files)
      {
         this.ServiceHeader = ServiceHeader;
         this.CoordinateTaskDataMessage = CoordinateTaskDataMessage;
         this.ServiceHeader1 = ServiceHeader1;
         this.Files = Files;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(WrapperName = "SendTasksMessage", WrapperNamespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", IsWrapped = true)]
   public partial class SendTasksMessage
   {

      [System.ServiceModel.MessageHeaderAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
      public Headers ServiceHeader;

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 0)]
      public CoordinateTaskData[] CoordinateTaskDataMessages;

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 1)]
      public CoordinateFile[] Files;

      public SendTasksMessage()
      {
      }

      public SendTasksMessage(Headers ServiceHeader, CoordinateTaskData[] CoordinateTaskDataMessages, CoordinateFile[] Files)
      {
         this.ServiceHeader = ServiceHeader;
         this.CoordinateTaskDataMessages = CoordinateTaskDataMessages;
         this.Files = Files;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(WrapperName = "CoordinateSendTaskStatusesMessage", WrapperNamespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", IsWrapped = true)]
   public partial class CoordinateSendTaskStatusesMessage
   {

      [System.ServiceModel.MessageHeaderAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
      public Headers ServiceHeader;

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 0)]
      public CoordinateTaskStatusData CoordinateTaskStatusDataMessage;

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 1)]
      public CoordinateFile[] Files;

      public CoordinateSendTaskStatusesMessage()
      {
      }

      public CoordinateSendTaskStatusesMessage(Headers ServiceHeader, CoordinateTaskStatusData CoordinateTaskStatusDataMessage, CoordinateFile[] Files)
      {
         this.ServiceHeader = ServiceHeader;
         this.CoordinateTaskStatusDataMessage = CoordinateTaskStatusDataMessage;
         this.Files = Files;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(WrapperName = "SendTaskStatusesMessages", WrapperNamespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", IsWrapped = true)]
   public partial class SendTaskStatusesMessages
   {

      [System.ServiceModel.MessageHeaderAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
      public Headers ServiceHeader;

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 0)]
      public CoordinateTaskStatusData[] CoordinateTaskStatusDataMessages;

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 1)]
      public CoordinateFile[] Files;

      public SendTaskStatusesMessages()
      {
      }

      public SendTaskStatusesMessages(Headers ServiceHeader, CoordinateTaskStatusData[] CoordinateTaskStatusDataMessages, CoordinateFile[] Files)
      {
         this.ServiceHeader = ServiceHeader;
         this.CoordinateTaskStatusDataMessages = CoordinateTaskStatusDataMessages;
         this.Files = Files;
      }
   }

   [DebuggerStepThrough]
   [GeneratedCode("System.ServiceModel", "4.0.0.0")]
   [EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
   [MessageContract(WrapperName = "CoordinateStatusMessage", WrapperNamespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", IsWrapped = true)]
   [XmlType(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
   [XmlRoot("CoordinateStatusMessage", Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]

   public partial class CoordinateStatusMessage
   {

      [System.ServiceModel.MessageHeaderAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
      public Headers ServiceHeader;

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 0)]
      public CoordinateStatusData CoordinateStatusDataMessage;

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 1)]
      public CoordinateFile[] Files;

      public CoordinateStatusMessage()
      {
      }

      public CoordinateStatusMessage(Headers ServiceHeader, CoordinateStatusData CoordinateStatusDataMessage, CoordinateFile[] Files)
      {
         this.ServiceHeader = ServiceHeader;
         this.CoordinateStatusDataMessage = CoordinateStatusDataMessage;
         this.Files = Files;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(WrapperName = "SetFilesAndStatusesMessage", WrapperNamespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", IsWrapped = true)]
   public partial class SetFilesAndStatusesMessage
   {

      [System.ServiceModel.MessageHeaderAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
      public Headers ServiceHeader;

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 0)]
      public CoordinateFile[] Files;

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 1)]
      public SetFilesAndStatusesData[] StatusesMessage;

      public SetFilesAndStatusesMessage()
      {
      }

      public SetFilesAndStatusesMessage(Headers ServiceHeader, CoordinateFile[] Files, SetFilesAndStatusesData[] StatusesMessage)
      {
         this.ServiceHeader = ServiceHeader;
         this.Files = Files;
         this.StatusesMessage = StatusesMessage;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(WrapperName = "ErrorMessage", WrapperNamespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", IsWrapped = true)]
   public partial class ErrorMessage
   {

      [System.ServiceModel.MessageHeaderAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
      public Headers ServiceHeader;

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 0)]
      public ErrorMessageData Error;

      public ErrorMessage()
      {
      }

      public ErrorMessage(Headers ServiceHeader, ErrorMessageData Error)
      {
         this.ServiceHeader = ServiceHeader;
         this.Error = Error;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class RequestDocumentsMessage
   {

      [System.ServiceModel.MessageHeaderAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
      public Headers ServiceHeader;

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 0)]
      public RequestDocumentsData GetRequestDocumentsMessage;

      public RequestDocumentsMessage()
      {
      }

      public RequestDocumentsMessage(Headers ServiceHeader, RequestDocumentsData GetRequestDocumentsMessage)
      {
         this.ServiceHeader = ServiceHeader;
         this.GetRequestDocumentsMessage = GetRequestDocumentsMessage;
      }
   }
}
