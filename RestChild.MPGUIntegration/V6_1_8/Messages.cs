using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.MPGUIntegration.V618
{
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "CoordinateMessage", WrapperNamespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", IsWrapped = true)]
    public partial class CoordinateMessage
    {
        [System.ServiceModel.MessageHeaderAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
        public Headers ServiceHeader;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 0)]
        public CoordinateData CoordinateDataMessage;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 1)]
        public ArrayOfCoordinateFile Files;

        public CoordinateMessage()
        {
        }

        public CoordinateMessage(Headers ServiceHeader, CoordinateData CoordinateDataMessage, ArrayOfCoordinateFile Files)
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
        public ArrayOfCoordinateFile Files;

        public SendRequestsMessage()
        {
        }

        public SendRequestsMessage(Headers ServiceHeader, CoordinateData[] CoordinateDataMessages, ArrayOfCoordinateFile Files)
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

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 1)]
        public CoordinateTaskData CoordinateTaskDataMessage;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 2)]
        public ArrayOfCoordinateFile Files;

        public CoordinateTaskMessage()
        {
        }

        public CoordinateTaskMessage(Headers ServiceHeader, SignatureType Signature, CoordinateTaskData CoordinateTaskDataMessage, ArrayOfCoordinateFile Files)
        {
            this.ServiceHeader = ServiceHeader;
            this.Signature = Signature;
            this.CoordinateTaskDataMessage = CoordinateTaskDataMessage;
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
        public ArrayOfCoordinateFile Files;

        public SendTasksMessage()
        {
        }

        public SendTasksMessage(Headers ServiceHeader, CoordinateTaskData[] CoordinateTaskDataMessages, ArrayOfCoordinateFile Files)
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

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 1)]
        public CoordinateTaskStatusData CoordinateTaskStatusDataMessage;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 2)]
        public ArrayOfCoordinateFile Files;

        public CoordinateSendTaskStatusesMessage()
        {
        }

        public CoordinateSendTaskStatusesMessage(Headers ServiceHeader, SignatureType Signature, CoordinateTaskStatusData CoordinateTaskStatusDataMessage, ArrayOfCoordinateFile Files)
        {
            this.ServiceHeader = ServiceHeader;
            this.Signature = Signature;
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
        public ArrayOfCoordinateFile Files;

        public SendTaskStatusesMessages()
        {
        }

        public SendTaskStatusesMessages(Headers ServiceHeader, CoordinateTaskStatusData[] CoordinateTaskStatusDataMessages, ArrayOfCoordinateFile Files)
        {
            this.ServiceHeader = ServiceHeader;
            this.CoordinateTaskStatusDataMessages = CoordinateTaskStatusDataMessages;
            this.Files = Files;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "CoordinateStatusMessage", WrapperNamespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", IsWrapped = true)]
    public partial class CoordinateStatusMessage
    {
        [System.ServiceModel.MessageHeaderAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
        public Headers ServiceHeader;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 0)]
        public CoordinateStatusData CoordinateStatusDataMessage;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 1)]
        public ArrayOfCoordinateFile Files;

        public CoordinateStatusMessage()
        {
        }

        public CoordinateStatusMessage(Headers ServiceHeader, CoordinateStatusData CoordinateStatusDataMessage, ArrayOfCoordinateFile Files)
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
        public ArrayOfCoordinateFile Files;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", Order = 1)]
        public SetFilesAndStatusesData[] StatusesMessage;

        public SetFilesAndStatusesMessage()
        {
        }

        public SetFilesAndStatusesMessage(Headers ServiceHeader, ArrayOfCoordinateFile Files, SetFilesAndStatusesData[] StatusesMessage)
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
