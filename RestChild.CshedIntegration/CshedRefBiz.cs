using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.CshedIntegration.CshedRefBiz
{
   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class ErrorMessageException : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string messageField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string message
      {
         get
         {
            return this.messageField;
         }
         set
         {
            this.messageField = value;
            this.RaisePropertyChanged("message");
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

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class Property : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string nameField;

      private string valueField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
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
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
      public string Value
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

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class CreateDocumentRequest : object, System.ComponentModel.INotifyPropertyChanged
   {

      private byte[] documentField;

      private string sSOIDField;

      private string documentClassField;

      private string fromSystemCodeField;

      private string uRNField;

      private Property[] propertiesField;

      private string serverField;

      private string serverStoreField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "base64Binary", Order = 0)]
      public byte[] Document
      {
         get
         {
            return this.documentField;
         }
         set
         {
            this.documentField = value;
            this.RaisePropertyChanged("Document");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
      public string SSOID
      {
         get
         {
            return this.sSOIDField;
         }
         set
         {
            this.sSOIDField = value;
            this.RaisePropertyChanged("SSOID");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 2)]
      public string DocumentClass
      {
         get
         {
            return this.documentClassField;
         }
         set
         {
            this.documentClassField = value;
            this.RaisePropertyChanged("DocumentClass");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 3)]
      public string FromSystemCode
      {
         get
         {
            return this.fromSystemCodeField;
         }
         set
         {
            this.fromSystemCodeField = value;
            this.RaisePropertyChanged("FromSystemCode");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 4)]
      public string URN
      {
         get
         {
            return this.uRNField;
         }
         set
         {
            this.uRNField = value;
            this.RaisePropertyChanged("URN");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 5)]
      [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
      public Property[] properties
      {
         get
         {
            return this.propertiesField;
         }
         set
         {
            this.propertiesField = value;
            this.RaisePropertyChanged("properties");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 6)]
      public string Server
      {
         get
         {
            return this.serverField;
         }
         set
         {
            this.serverField = value;
            this.RaisePropertyChanged("Server");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 7)]
      public string ServerStore
      {
         get
         {
            return this.serverStoreField;
         }
         set
         {
            this.serverStoreField = value;
            this.RaisePropertyChanged("ServerStore");
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

   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ServiceModel.ServiceContractAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", ConfigurationName = "CshedService.CustomWebServiceImpl")]
   public interface CustomWebServiceImpl
   {

      // CODEGEN: Generating message contract since the operation CreateDocument is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CreateDocumentRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CreateDocumentResponse" +
          "")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CreateDocument/Fault/E" +
          "rrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.CreateDocumentResponse CreateDocument(RestChild.CshedIntegration.CshedRefBiz.CreateDocumentRequest1 request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CreateDocumentRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CreateDocumentResponse" +
          "")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.CreateDocumentResponse> CreateDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.CreateDocumentRequest1 request);

      // CODEGEN: Generating message contract since the operation UpdateDocumentVersion is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/UpdateDocumentVersionR" +
          "equest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/UpdateDocumentVersionR" +
          "esponse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/UpdateDocumentVersion/" +
          "Fault/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentVersionResponse UpdateDocumentVersion(RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentVersionRequest request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/UpdateDocumentVersionR" +
          "equest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/UpdateDocumentVersionR" +
          "esponse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentVersionResponse> UpdateDocumentVersionAsync(RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentVersionRequest request);

      // CODEGEN: Generating message contract since the operation GetDocument is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentResponse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocument/Fault/Erro" +
          "rMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.GetDocumentResponse1 GetDocument(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest1 request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentResponse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetDocumentResponse1> GetDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest1 request);

      // CODEGEN: Generating message contract since the operation GetDocumentData is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentDataRequest" +
          "", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentDataRespons" +
          "e")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentData/Fault/" +
          "ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.GetDocumentDataResponse GetDocumentData(RestChild.CshedIntegration.CshedRefBiz.GetDocumentDataRequest request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentDataRequest" +
          "", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentDataRespons" +
          "e")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetDocumentDataResponse> GetDocumentDataAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentDataRequest request);

      // CODEGEN: Generating message contract since the operation GetSignedDocumentData is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetSignedDocumentDataR" +
          "equest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetSignedDocumentDataR" +
          "esponse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetSignedDocumentData/" +
          "Fault/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.GetSignedDocumentDataResponse GetSignedDocumentData(RestChild.CshedIntegration.CshedRefBiz.GetSignedDocumentDataRequest request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetSignedDocumentDataR" +
          "equest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetSignedDocumentDataR" +
          "esponse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetSignedDocumentDataResponse> GetSignedDocumentDataAsync(RestChild.CshedIntegration.CshedRefBiz.GetSignedDocumentDataRequest request);

      // CODEGEN: Generating message contract since the operation GetDocumentProperties is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentPropertiesR" +
          "equest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentPropertiesR" +
          "esponse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentProperties/" +
          "Fault/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.GetDocumentPropertiesResponse1 GetDocumentProperties(RestChild.CshedIntegration.CshedRefBiz.GetDocumentPropertiesRequest request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentPropertiesR" +
          "equest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentPropertiesR" +
          "esponse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetDocumentPropertiesResponse1> GetDocumentPropertiesAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentPropertiesRequest request);

      // CODEGEN: Generating message contract since the operation SearchDocuments is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/SearchDocumentsRequest" +
          "", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/SearchDocumentsRespons" +
          "e")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/SearchDocuments/Fault/" +
          "ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.SearchDocumentsResponse SearchDocuments(RestChild.CshedIntegration.CshedRefBiz.SearchDocumentsRequest1 request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/SearchDocumentsRequest" +
          "", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/SearchDocumentsRespons" +
          "e")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.SearchDocumentsResponse> SearchDocumentsAsync(RestChild.CshedIntegration.CshedRefBiz.SearchDocumentsRequest1 request);

      // CODEGEN: Generating message contract since the operation DeleteDocuments is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/DeleteDocumentsRequest" +
          "", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/DeleteDocumentsRespons" +
          "e")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/DeleteDocuments/Fault/" +
          "ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentsResponse DeleteDocuments(RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentsRequest request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/DeleteDocumentsRequest" +
          "", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/DeleteDocumentsRespons" +
          "e")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentsResponse> DeleteDocumentsAsync(RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentsRequest request);

      // CODEGEN: Generating message contract since the operation CreateFolder is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CreateFolderRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CreateFolderResponse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CreateFolder/Fault/Err" +
          "orMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.CreateFolderResponse CreateFolder(RestChild.CshedIntegration.CshedRefBiz.CreateFolderRequest1 request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CreateFolderRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CreateFolderResponse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.CreateFolderResponse> CreateFolderAsync(RestChild.CshedIntegration.CshedRefBiz.CreateFolderRequest1 request);

      // CODEGEN: Generating message contract since the operation GetDocumentsInFolder is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentsInFolderRe" +
          "quest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentsInFolderRe" +
          "sponse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentsInFolder/F" +
          "ault/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.GetDocumentsInFolderResponse GetDocumentsInFolder(RestChild.CshedIntegration.CshedRefBiz.GetDocumentsInFolderRequest request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentsInFolderRe" +
          "quest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentsInFolderRe" +
          "sponse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetDocumentsInFolderResponse> GetDocumentsInFolderAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentsInFolderRequest request);

      // CODEGEN: Generating message contract since the operation GetFoldersInFolder is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFoldersInFolderRequ" +
          "est", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFoldersInFolderResp" +
          "onse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFoldersInFolder/Fau" +
          "lt/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.GetFoldersInFolderResponse GetFoldersInFolder(RestChild.CshedIntegration.CshedRefBiz.GetFoldersInFolderRequest request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFoldersInFolderRequ" +
          "est", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFoldersInFolderResp" +
          "onse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetFoldersInFolderResponse> GetFoldersInFolderAsync(RestChild.CshedIntegration.CshedRefBiz.GetFoldersInFolderRequest request);

      // CODEGEN: Generating message contract since the operation GetParentFolder is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetParentFolderRequest" +
          "", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetParentFolderRespons" +
          "e")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetParentFolder/Fault/" +
          "ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.GetParentFolderResponse GetParentFolder(RestChild.CshedIntegration.CshedRefBiz.GetParentFolderRequest1 request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetParentFolderRequest" +
          "", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetParentFolderRespons" +
          "e")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetParentFolderResponse> GetParentFolderAsync(RestChild.CshedIntegration.CshedRefBiz.GetParentFolderRequest1 request);

      // CODEGEN: Generating message contract since the operation DeleteDocumentVersion is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/DeleteDocumentVersionR" +
          "equest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/DeleteDocumentVersionR" +
          "esponse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/DeleteDocumentVersion/" +
          "Fault/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentVersionResponse DeleteDocumentVersion(RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentVersionRequest1 request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/DeleteDocumentVersionR" +
          "equest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/DeleteDocumentVersionR" +
          "esponse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentVersionResponse> DeleteDocumentVersionAsync(RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentVersionRequest1 request);

      // CODEGEN: Generating message contract since the operation UpdateDocumentProperties is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/UpdateDocumentProperti" +
          "esRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/UpdateDocumentProperti" +
          "esResponse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/UpdateDocumentProperti" +
          "es/Fault/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentPropertiesResponse UpdateDocumentProperties(RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentPropertiesRequest request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/UpdateDocumentProperti" +
          "esRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/UpdateDocumentProperti" +
          "esResponse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentPropertiesResponse> UpdateDocumentPropertiesAsync(RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentPropertiesRequest request);

      // CODEGEN: Generating message contract since the operation FolderDocument is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/FolderDocumentRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/FolderDocumentResponse" +
          "")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/FolderDocument/Fault/E" +
          "rrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.FolderDocumentResponse FolderDocument(RestChild.CshedIntegration.CshedRefBiz.FolderDocumentRequest1 request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/FolderDocumentRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/FolderDocumentResponse" +
          "")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.FolderDocumentResponse> FolderDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.FolderDocumentRequest1 request);

      // CODEGEN: Generating message contract since the operation GetFoldersOfDocument is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFoldersOfDocumentRe" +
          "quest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFoldersOfDocumentRe" +
          "sponse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFoldersOfDocument/F" +
          "ault/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.GetFoldersOfDocumentResponse GetFoldersOfDocument(RestChild.CshedIntegration.CshedRefBiz.GetFoldersOfDocumentRequest1 request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFoldersOfDocumentRe" +
          "quest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFoldersOfDocumentRe" +
          "sponse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetFoldersOfDocumentResponse> GetFoldersOfDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.GetFoldersOfDocumentRequest1 request);

      // CODEGEN: Generating message contract since the operation SetDocumentPermissions is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/SetDocumentPermissions" +
          "Request", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/SetDocumentPermissions" +
          "Response")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/SetDocumentPermissions" +
          "/Fault/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.SetDocumentPermissionsResponse SetDocumentPermissions(RestChild.CshedIntegration.CshedRefBiz.SetDocumentPermissionsRequest1 request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/SetDocumentPermissions" +
          "Request", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/SetDocumentPermissions" +
          "Response")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.SetDocumentPermissionsResponse> SetDocumentPermissionsAsync(RestChild.CshedIntegration.CshedRefBiz.SetDocumentPermissionsRequest1 request);

      // CODEGEN: Generating message contract since the operation SetFolderPermissions is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/SetFolderPermissionsRe" +
          "quest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/SetFolderPermissionsRe" +
          "sponse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/SetFolderPermissions/F" +
          "ault/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.SetFolderPermissionsResponse SetFolderPermissions(RestChild.CshedIntegration.CshedRefBiz.SetFolderPermissionsRequest1 request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/SetFolderPermissionsRe" +
          "quest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/SetFolderPermissionsRe" +
          "sponse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.SetFolderPermissionsResponse> SetFolderPermissionsAsync(RestChild.CshedIntegration.CshedRefBiz.SetFolderPermissionsRequest1 request);

      // CODEGEN: Generating message contract since the operation GetDocumentPermissions is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentPermissions" +
          "Request", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentPermissions" +
          "Response")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentPermissions" +
          "/Fault/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.GetDocumentPermissionsResponse GetDocumentPermissions(RestChild.CshedIntegration.CshedRefBiz.GetDocumentPermissionsRequest1 request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentPermissions" +
          "Request", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetDocumentPermissions" +
          "Response")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetDocumentPermissionsResponse> GetDocumentPermissionsAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentPermissionsRequest1 request);

      // CODEGEN: Generating message contract since the operation GetFolderPermissions is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFolderPermissionsRe" +
          "quest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFolderPermissionsRe" +
          "sponse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFolderPermissions/F" +
          "ault/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.GetFolderPermissionsResponse GetFolderPermissions(RestChild.CshedIntegration.CshedRefBiz.GetFolderPermissionsRequest1 request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFolderPermissionsRe" +
          "quest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFolderPermissionsRe" +
          "sponse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetFolderPermissionsResponse> GetFolderPermissionsAsync(RestChild.CshedIntegration.CshedRefBiz.GetFolderPermissionsRequest1 request);

      // CODEGEN: Generating message contract since the operation CreateAnnotation is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CreateAnnotationReques" +
          "t", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CreateAnnotationRespon" +
          "se")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CreateAnnotation/Fault" +
          "/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.CreateAnnotationResponse CreateAnnotation(RestChild.CshedIntegration.CshedRefBiz.CreateAnnotationRequest1 request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CreateAnnotationReques" +
          "t", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CreateAnnotationRespon" +
          "se")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.CreateAnnotationResponse> CreateAnnotationAsync(RestChild.CshedIntegration.CshedRefBiz.CreateAnnotationRequest1 request);

      // CODEGEN: Generating message contract since the operation RetrieveAnnotations is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/RetrieveAnnotationsReq" +
          "uest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/RetrieveAnnotationsRes" +
          "ponse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/RetrieveAnnotations/Fa" +
          "ult/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationsResponse RetrieveAnnotations(RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationsRequest request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/RetrieveAnnotationsReq" +
          "uest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/RetrieveAnnotationsRes" +
          "ponse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationsResponse> RetrieveAnnotationsAsync(RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationsRequest request);

      // CODEGEN: Generating message contract since the operation RetrieveAnnotation is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/RetrieveAnnotationRequ" +
          "est", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/RetrieveAnnotationResp" +
          "onse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/RetrieveAnnotation/Fau" +
          "lt/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationResponse RetrieveAnnotation(RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationRequest1 request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/RetrieveAnnotationRequ" +
          "est", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/RetrieveAnnotationResp" +
          "onse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationResponse> RetrieveAnnotationAsync(RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationRequest1 request);

      // CODEGEN: Generating message contract since the operation DeleteAnnotation is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/DeleteAnnotationReques" +
          "t", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/DeleteAnnotationRespon" +
          "se")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/DeleteAnnotation/Fault" +
          "/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.DeleteAnnotationResponse DeleteAnnotation(RestChild.CshedIntegration.CshedRefBiz.DeleteAnnotationRequest request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/DeleteAnnotationReques" +
          "t", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/DeleteAnnotationRespon" +
          "se")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.DeleteAnnotationResponse> DeleteAnnotationAsync(RestChild.CshedIntegration.CshedRefBiz.DeleteAnnotationRequest request);

      // CODEGEN: Generating message contract since the operation RetrieveDocAndAnnotations is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/RetrieveDocAndAnnotati" +
          "onsRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/RetrieveDocAndAnnotati" +
          "onsResponse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/RetrieveDocAndAnnotati" +
          "ons/Fault/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.RetrieveDocAndAnnotationsResponse1 RetrieveDocAndAnnotations(RestChild.CshedIntegration.CshedRefBiz.RetrieveDocAndAnnotationsRequest request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/RetrieveDocAndAnnotati" +
          "onsRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/RetrieveDocAndAnnotati" +
          "onsResponse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.RetrieveDocAndAnnotationsResponse1> RetrieveDocAndAnnotationsAsync(RestChild.CshedIntegration.CshedRefBiz.RetrieveDocAndAnnotationsRequest request);

      // CODEGEN: Generating message contract since the operation ValidateDocument is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/ValidateDocumentReques" +
          "t", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/ValidateDocumentRespon" +
          "se")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/ValidateDocument/Fault" +
          "/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentResponse ValidateDocument(RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentRequest request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/ValidateDocumentReques" +
          "t", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/ValidateDocumentRespon" +
          "se")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentResponse> ValidateDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentRequest request);

      // CODEGEN: Generating message contract since the operation ValidateDocumentWithInfo is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/ValidateDocumentWithIn" +
          "foRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/ValidateDocumentWithIn" +
          "foResponse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/ValidateDocumentWithIn" +
          "fo/Fault/ErrorMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentWithInfoResponse1 ValidateDocumentWithInfo(RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentWithInfoRequest request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/ValidateDocumentWithIn" +
          "foRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/ValidateDocumentWithIn" +
          "foResponse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentWithInfoResponse1> ValidateDocumentWithInfoAsync(RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentWithInfoRequest request);

      // CODEGEN: Generating message contract since the operation GetFileNetId is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFileNetIdRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFileNetIdResponse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFileNetId/Fault/Err" +
          "orMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.GetFileNetIdResponse GetFileNetId(RestChild.CshedIntegration.CshedRefBiz.GetFileNetIdRequest request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFileNetIdRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetFileNetIdResponse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetFileNetIdResponse> GetFileNetIdAsync(RestChild.CshedIntegration.CshedRefBiz.GetFileNetIdRequest request);

      // CODEGEN: Generating message contract since the operation GetCustomId is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetCustomIdRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetCustomIdResponse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetCustomId/Fault/Erro" +
          "rMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.GetCustomIdResponse GetCustomId(RestChild.CshedIntegration.CshedRefBiz.GetCustomIdRequest request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetCustomIdRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/GetCustomIdResponse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetCustomIdResponse> GetCustomIdAsync(RestChild.CshedIntegration.CshedRefBiz.GetCustomIdRequest request);

      // CODEGEN: Generating message contract since the operation CopyDocument is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CopyDocumentRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CopyDocumentResponse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CopyDocument/Fault/Err" +
          "orMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.CopyDocumentResponse CopyDocument(RestChild.CshedIntegration.CshedRefBiz.CopyDocumentRequest1 request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CopyDocumentRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/CopyDocumentResponse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.CopyDocumentResponse> CopyDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.CopyDocumentRequest1 request);

      // CODEGEN: Generating message contract since the operation moveDocument is neither RPC nor document wrapped.
      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/moveDocumentRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/moveDocumentResponse")]
      [System.ServiceModel.FaultContractAttribute(typeof(RestChild.CshedIntegration.CshedRefBiz.ErrorMessageException), Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/moveDocument/Fault/Err" +
          "orMessageException", Name = "ErrorMessageException")]
      [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
      [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Property[]))]
      RestChild.CshedIntegration.CshedRefBiz.moveDocumentResponse moveDocument(RestChild.CshedIntegration.CshedRefBiz.moveDocumentRequest request);

      [System.ServiceModel.OperationContractAttribute(Action = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/moveDocumentRequest", ReplyAction = "http://cloud.mos.ru/customWebService2/CustomWebServiceImpl/moveDocumentResponse")]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.moveDocumentResponse> moveDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.moveDocumentRequest request);
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class CreateDocumentRequest1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.CreateDocumentRequest CreateDocument;

      public CreateDocumentRequest1()
      {
      }

      public CreateDocumentRequest1(RestChild.CshedIntegration.CshedRefBiz.CreateDocumentRequest CreateDocument)
      {
         this.CreateDocument = CreateDocument;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class CreateDocumentResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public string CustomID;

      public CreateDocumentResponse()
      {
      }

      public CreateDocumentResponse(string CustomID)
      {
         this.CustomID = CustomID;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class UpdateDocumentRequest : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string documentIdField;

      private byte[] documentField;

      private string sSOIDField;

      private Property[] propertiesField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string DocumentId
      {
         get
         {
            return this.documentIdField;
         }
         set
         {
            this.documentIdField = value;
            this.RaisePropertyChanged("DocumentId");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "base64Binary", Order = 1)]
      public byte[] Document
      {
         get
         {
            return this.documentField;
         }
         set
         {
            this.documentField = value;
            this.RaisePropertyChanged("Document");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 2)]
      public string SSOID
      {
         get
         {
            return this.sSOIDField;
         }
         set
         {
            this.sSOIDField = value;
            this.RaisePropertyChanged("SSOID");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 3)]
      [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
      public Property[] properties
      {
         get
         {
            return this.propertiesField;
         }
         set
         {
            this.propertiesField = value;
            this.RaisePropertyChanged("properties");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class UpdateDocumentVersionRequest
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentRequest UpdateDocumentVersion;

      public UpdateDocumentVersionRequest()
      {
      }

      public UpdateDocumentVersionRequest(RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentRequest UpdateDocumentVersion)
      {
         this.UpdateDocumentVersion = UpdateDocumentVersion;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class UpdateDocumentVersionResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public string UpdatedId;

      public UpdateDocumentVersionResponse()
      {
      }

      public UpdateDocumentVersionResponse(string UpdatedId)
      {
         this.UpdatedId = UpdatedId;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class GetDocumentRequest : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string documentIdField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string DocumentId
      {
         get
         {
            return this.documentIdField;
         }
         set
         {
            this.documentIdField = value;
            this.RaisePropertyChanged("DocumentId");
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

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class GetDocumentResponse : object, System.ComponentModel.INotifyPropertyChanged
   {

      private byte[] documentField;

      private Property[] propertiesField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "base64Binary", Order = 0)]
      public byte[] Document
      {
         get
         {
            return this.documentField;
         }
         set
         {
            this.documentField = value;
            this.RaisePropertyChanged("Document");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
      [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
      public Property[] properties
      {
         get
         {
            return this.propertiesField;
         }
         set
         {
            this.propertiesField = value;
            this.RaisePropertyChanged("properties");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetDocumentRequest1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetDocument;

      public GetDocumentRequest1()
      {
      }

      public GetDocumentRequest1(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetDocument)
      {
         this.GetDocument = GetDocument;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetDocumentResponse1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetDocumentResponse GetDocumentResponse;

      public GetDocumentResponse1()
      {
      }

      public GetDocumentResponse1(RestChild.CshedIntegration.CshedRefBiz.GetDocumentResponse GetDocumentResponse)
      {
         this.GetDocumentResponse = GetDocumentResponse;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetDocumentDataRequest
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetDocumentData;

      public GetDocumentDataRequest()
      {
      }

      public GetDocumentDataRequest(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetDocumentData)
      {
         this.GetDocumentData = GetDocumentData;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetDocumentDataResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary", IsNullable = true)]
      public byte[] DocumentData;

      public GetDocumentDataResponse()
      {
      }

      public GetDocumentDataResponse(byte[] DocumentData)
      {
         this.DocumentData = DocumentData;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetSignedDocumentDataRequest
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetSignedDocumentData;

      public GetSignedDocumentDataRequest()
      {
      }

      public GetSignedDocumentDataRequest(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetSignedDocumentData)
      {
         this.GetSignedDocumentData = GetSignedDocumentData;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetSignedDocumentDataResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary", IsNullable = true)]
      public byte[] SignedDocumentData;

      public GetSignedDocumentDataResponse()
      {
      }

      public GetSignedDocumentDataResponse(byte[] SignedDocumentData)
      {
         this.SignedDocumentData = SignedDocumentData;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class GetDocumentPropertiesResponse : object, System.ComponentModel.INotifyPropertyChanged
   {

      private Property[] propertiesField;

      /// <remarks/>
      [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
      public Property[] properties
      {
         get
         {
            return this.propertiesField;
         }
         set
         {
            this.propertiesField = value;
            this.RaisePropertyChanged("properties");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetDocumentPropertiesRequest
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetDocumentProperties;

      public GetDocumentPropertiesRequest()
      {
      }

      public GetDocumentPropertiesRequest(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetDocumentProperties)
      {
         this.GetDocumentProperties = GetDocumentProperties;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetDocumentPropertiesResponse1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetDocumentPropertiesResponse GetDocumentPropertiesResponse;

      public GetDocumentPropertiesResponse1()
      {
      }

      public GetDocumentPropertiesResponse1(RestChild.CshedIntegration.CshedRefBiz.GetDocumentPropertiesResponse GetDocumentPropertiesResponse)
      {
         this.GetDocumentPropertiesResponse = GetDocumentPropertiesResponse;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class SearchDocumentsRequest : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string searchQueryField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string searchQuery
      {
         get
         {
            return this.searchQueryField;
         }
         set
         {
            this.searchQueryField = value;
            this.RaisePropertyChanged("searchQuery");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class SearchDocumentsRequest1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.SearchDocumentsRequest SearchDocuments;

      public SearchDocumentsRequest1()
      {
      }

      public SearchDocumentsRequest1(RestChild.CshedIntegration.CshedRefBiz.SearchDocumentsRequest SearchDocuments)
      {
         this.SearchDocuments = SearchDocuments;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class SearchDocumentsResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlArrayAttribute(IsNullable = true)]
      [System.Xml.Serialization.XmlArrayItemAttribute("item", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
      public string[] CustomIDs;

      public SearchDocumentsResponse()
      {
      }

      public SearchDocumentsResponse(string[] CustomIDs)
      {
         this.CustomIDs = CustomIDs;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class DeleteDocumentsRequest
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlArrayAttribute(IsNullable = true)]
      [System.Xml.Serialization.XmlArrayItemAttribute("Documents", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
      public string[] DeleteDocuments;

      public DeleteDocumentsRequest()
      {
      }

      public DeleteDocumentsRequest(string[] DeleteDocuments)
      {
         this.DeleteDocuments = DeleteDocuments;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class DeleteDocumentsResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public System.Nullable<int> NumOfDeleted;

      public DeleteDocumentsResponse()
      {
      }

      public DeleteDocumentsResponse(System.Nullable<int> NumOfDeleted)
      {
         this.NumOfDeleted = NumOfDeleted;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class CreateFolderRequest : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string pathField;

      private string serverField;

      private string serverStoreField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string Path
      {
         get
         {
            return this.pathField;
         }
         set
         {
            this.pathField = value;
            this.RaisePropertyChanged("Path");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
      public string Server
      {
         get
         {
            return this.serverField;
         }
         set
         {
            this.serverField = value;
            this.RaisePropertyChanged("Server");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 2)]
      public string ServerStore
      {
         get
         {
            return this.serverStoreField;
         }
         set
         {
            this.serverStoreField = value;
            this.RaisePropertyChanged("ServerStore");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class CreateFolderRequest1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.CreateFolderRequest CreateFolder;

      public CreateFolderRequest1()
      {
      }

      public CreateFolderRequest1(RestChild.CshedIntegration.CshedRefBiz.CreateFolderRequest CreateFolder)
      {
         this.CreateFolder = CreateFolder;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class CreateFolderResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public string FolderPath;

      public CreateFolderResponse()
      {
      }

      public CreateFolderResponse(string FolderPath)
      {
         this.FolderPath = FolderPath;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class GetFilesInFolderRequest : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string pathField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string Path
      {
         get
         {
            return this.pathField;
         }
         set
         {
            this.pathField = value;
            this.RaisePropertyChanged("Path");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetDocumentsInFolderRequest
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetFilesInFolderRequest GetDocumentsInFolder;

      public GetDocumentsInFolderRequest()
      {
      }

      public GetDocumentsInFolderRequest(RestChild.CshedIntegration.CshedRefBiz.GetFilesInFolderRequest GetDocumentsInFolder)
      {
         this.GetDocumentsInFolder = GetDocumentsInFolder;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetDocumentsInFolderResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlArrayAttribute(IsNullable = true)]
      [System.Xml.Serialization.XmlArrayItemAttribute("item", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
      public string[] DocumentsInFolder;

      public GetDocumentsInFolderResponse()
      {
      }

      public GetDocumentsInFolderResponse(string[] DocumentsInFolder)
      {
         this.DocumentsInFolder = DocumentsInFolder;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetFoldersInFolderRequest
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetFilesInFolderRequest GetFoldersInFolder;

      public GetFoldersInFolderRequest()
      {
      }

      public GetFoldersInFolderRequest(RestChild.CshedIntegration.CshedRefBiz.GetFilesInFolderRequest GetFoldersInFolder)
      {
         this.GetFoldersInFolder = GetFoldersInFolder;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetFoldersInFolderResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlArrayAttribute(IsNullable = true)]
      [System.Xml.Serialization.XmlArrayItemAttribute("item", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
      public string[] FoldersInFolder;

      public GetFoldersInFolderResponse()
      {
      }

      public GetFoldersInFolderResponse(string[] FoldersInFolder)
      {
         this.FoldersInFolder = FoldersInFolder;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class GetParentFolderRequest : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string pathField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string Path
      {
         get
         {
            return this.pathField;
         }
         set
         {
            this.pathField = value;
            this.RaisePropertyChanged("Path");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetParentFolderRequest1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetParentFolderRequest GetParentFolder;

      public GetParentFolderRequest1()
      {
      }

      public GetParentFolderRequest1(RestChild.CshedIntegration.CshedRefBiz.GetParentFolderRequest GetParentFolder)
      {
         this.GetParentFolder = GetParentFolder;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetParentFolderResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public string ParentFolder;

      public GetParentFolderResponse()
      {
      }

      public GetParentFolderResponse(string ParentFolder)
      {
         this.ParentFolder = ParentFolder;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class DeleteDocumentVersionRequest : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string documentIdField;

      private Version versionField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string DocumentId
      {
         get
         {
            return this.documentIdField;
         }
         set
         {
            this.documentIdField = value;
            this.RaisePropertyChanged("DocumentId");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
      public Version Version
      {
         get
         {
            return this.versionField;
         }
         set
         {
            this.versionField = value;
            this.RaisePropertyChanged("Version");
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

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class Version : object, System.ComponentModel.INotifyPropertyChanged
   {

      private int majorField;

      private int minorField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public int Major
      {
         get
         {
            return this.majorField;
         }
         set
         {
            this.majorField = value;
            this.RaisePropertyChanged("Major");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
      public int Minor
      {
         get
         {
            return this.minorField;
         }
         set
         {
            this.minorField = value;
            this.RaisePropertyChanged("Minor");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class DeleteDocumentVersionRequest1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentVersionRequest DeleteDocumentVersion;

      public DeleteDocumentVersionRequest1()
      {
      }

      public DeleteDocumentVersionRequest1(RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentVersionRequest DeleteDocumentVersion)
      {
         this.DeleteDocumentVersion = DeleteDocumentVersion;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class DeleteDocumentVersionResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Name = "DeleteDocumentVersionResponse", Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public string DeleteDocumentVersionResponse1;

      public DeleteDocumentVersionResponse()
      {
      }

      public DeleteDocumentVersionResponse(string DeleteDocumentVersionResponse1)
      {
         this.DeleteDocumentVersionResponse1 = DeleteDocumentVersionResponse1;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class UpdatePropertiesRequest : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string documentIdField;

      private Property[] propertiesField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string DocumentId
      {
         get
         {
            return this.documentIdField;
         }
         set
         {
            this.documentIdField = value;
            this.RaisePropertyChanged("DocumentId");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
      [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
      public Property[] properties
      {
         get
         {
            return this.propertiesField;
         }
         set
         {
            this.propertiesField = value;
            this.RaisePropertyChanged("properties");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class UpdateDocumentPropertiesRequest
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.UpdatePropertiesRequest UpdateDocumentProperties;

      public UpdateDocumentPropertiesRequest()
      {
      }

      public UpdateDocumentPropertiesRequest(RestChild.CshedIntegration.CshedRefBiz.UpdatePropertiesRequest UpdateDocumentProperties)
      {
         this.UpdateDocumentProperties = UpdateDocumentProperties;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class UpdateDocumentPropertiesResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Name = "UpdateDocumentPropertiesResponse", Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public string UpdateDocumentPropertiesResponse1;

      public UpdateDocumentPropertiesResponse()
      {
      }

      public UpdateDocumentPropertiesResponse(string UpdateDocumentPropertiesResponse1)
      {
         this.UpdateDocumentPropertiesResponse1 = UpdateDocumentPropertiesResponse1;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class FolderDocumentRequest : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string documentIdField;

      private string pathField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string DocumentId
      {
         get
         {
            return this.documentIdField;
         }
         set
         {
            this.documentIdField = value;
            this.RaisePropertyChanged("DocumentId");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
      public string Path
      {
         get
         {
            return this.pathField;
         }
         set
         {
            this.pathField = value;
            this.RaisePropertyChanged("Path");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class FolderDocumentRequest1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.FolderDocumentRequest FolderDocument;

      public FolderDocumentRequest1()
      {
      }

      public FolderDocumentRequest1(RestChild.CshedIntegration.CshedRefBiz.FolderDocumentRequest FolderDocument)
      {
         this.FolderDocument = FolderDocument;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class FolderDocumentResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Name = "FolderDocumentResponse", Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public string FolderDocumentResponse1;

      public FolderDocumentResponse()
      {
      }

      public FolderDocumentResponse(string FolderDocumentResponse1)
      {
         this.FolderDocumentResponse1 = FolderDocumentResponse1;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class GetFoldersOfDocumentRequest : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string documentIdField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string DocumentId
      {
         get
         {
            return this.documentIdField;
         }
         set
         {
            this.documentIdField = value;
            this.RaisePropertyChanged("DocumentId");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetFoldersOfDocumentRequest1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetFoldersOfDocumentRequest GetFoldersOfDocument;

      public GetFoldersOfDocumentRequest1()
      {
      }

      public GetFoldersOfDocumentRequest1(RestChild.CshedIntegration.CshedRefBiz.GetFoldersOfDocumentRequest GetFoldersOfDocument)
      {
         this.GetFoldersOfDocument = GetFoldersOfDocument;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetFoldersOfDocumentResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlArrayAttribute(IsNullable = true)]
      [System.Xml.Serialization.XmlArrayItemAttribute("item", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
      public string[] FoldersOfDocument;

      public GetFoldersOfDocumentResponse()
      {
      }

      public GetFoldersOfDocumentResponse(string[] FoldersOfDocument)
      {
         this.FoldersOfDocument = FoldersOfDocument;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class SetDocumentPermissionsRequest : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string documentIdField;

      private Permission[] permissionsField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string DocumentId
      {
         get
         {
            return this.documentIdField;
         }
         set
         {
            this.documentIdField = value;
            this.RaisePropertyChanged("DocumentId");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
      [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
      public Permission[] permissions
      {
         get
         {
            return this.permissionsField;
         }
         set
         {
            this.permissionsField = value;
            this.RaisePropertyChanged("permissions");
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

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class Permission : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string granteeNameField;

      private string[] accessRightsField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string GranteeName
      {
         get
         {
            return this.granteeNameField;
         }
         set
         {
            this.granteeNameField = value;
            this.RaisePropertyChanged("GranteeName");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("AccessRights", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
      public string[] AccessRights
      {
         get
         {
            return this.accessRightsField;
         }
         set
         {
            this.accessRightsField = value;
            this.RaisePropertyChanged("AccessRights");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class SetDocumentPermissionsRequest1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.SetDocumentPermissionsRequest SetDocumentPermissions;

      public SetDocumentPermissionsRequest1()
      {
      }

      public SetDocumentPermissionsRequest1(RestChild.CshedIntegration.CshedRefBiz.SetDocumentPermissionsRequest SetDocumentPermissions)
      {
         this.SetDocumentPermissions = SetDocumentPermissions;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class SetDocumentPermissionsResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Name = "SetDocumentPermissionsResponse", Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public string SetDocumentPermissionsResponse1;

      public SetDocumentPermissionsResponse()
      {
      }

      public SetDocumentPermissionsResponse(string SetDocumentPermissionsResponse1)
      {
         this.SetDocumentPermissionsResponse1 = SetDocumentPermissionsResponse1;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class SetFolderPermissionsRequest : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string pathField;

      private Permission[] permissionsField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string Path
      {
         get
         {
            return this.pathField;
         }
         set
         {
            this.pathField = value;
            this.RaisePropertyChanged("Path");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
      [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
      public Permission[] permissions
      {
         get
         {
            return this.permissionsField;
         }
         set
         {
            this.permissionsField = value;
            this.RaisePropertyChanged("permissions");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class SetFolderPermissionsRequest1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.SetFolderPermissionsRequest SetFolderPermissions;

      public SetFolderPermissionsRequest1()
      {
      }

      public SetFolderPermissionsRequest1(RestChild.CshedIntegration.CshedRefBiz.SetFolderPermissionsRequest SetFolderPermissions)
      {
         this.SetFolderPermissions = SetFolderPermissions;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class SetFolderPermissionsResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Name = "SetFolderPermissionsResponse", Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public string SetFolderPermissionsResponse1;

      public SetFolderPermissionsResponse()
      {
      }

      public SetFolderPermissionsResponse(string SetFolderPermissionsResponse1)
      {
         this.SetFolderPermissionsResponse1 = SetFolderPermissionsResponse1;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class GetDocumentPermissionsRequest : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string documentIdField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string DocumentId
      {
         get
         {
            return this.documentIdField;
         }
         set
         {
            this.documentIdField = value;
            this.RaisePropertyChanged("DocumentId");
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

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class GetPermissionsResponse : object, System.ComponentModel.INotifyPropertyChanged
   {

      private Permission[] permissionsField;

      /// <remarks/>
      [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
      public Permission[] permissions
      {
         get
         {
            return this.permissionsField;
         }
         set
         {
            this.permissionsField = value;
            this.RaisePropertyChanged("permissions");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetDocumentPermissionsRequest1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetDocumentPermissionsRequest GetDocumentPermissions;

      public GetDocumentPermissionsRequest1()
      {
      }

      public GetDocumentPermissionsRequest1(RestChild.CshedIntegration.CshedRefBiz.GetDocumentPermissionsRequest GetDocumentPermissions)
      {
         this.GetDocumentPermissions = GetDocumentPermissions;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetDocumentPermissionsResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetPermissionsResponse PermissionsOfDocument;

      public GetDocumentPermissionsResponse()
      {
      }

      public GetDocumentPermissionsResponse(RestChild.CshedIntegration.CshedRefBiz.GetPermissionsResponse PermissionsOfDocument)
      {
         this.PermissionsOfDocument = PermissionsOfDocument;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class GetFolderPermissionsRequest : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string pathField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string Path
      {
         get
         {
            return this.pathField;
         }
         set
         {
            this.pathField = value;
            this.RaisePropertyChanged("Path");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetFolderPermissionsRequest1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetFolderPermissionsRequest GetFolderPermissions;

      public GetFolderPermissionsRequest1()
      {
      }

      public GetFolderPermissionsRequest1(RestChild.CshedIntegration.CshedRefBiz.GetFolderPermissionsRequest GetFolderPermissions)
      {
         this.GetFolderPermissions = GetFolderPermissions;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetFolderPermissionsResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetPermissionsResponse PermissionsOfFolder;

      public GetFolderPermissionsResponse()
      {
      }

      public GetFolderPermissionsResponse(RestChild.CshedIntegration.CshedRefBiz.GetPermissionsResponse PermissionsOfFolder)
      {
         this.PermissionsOfFolder = PermissionsOfFolder;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class CreateAnnotationRequest : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string documentIdField;

      private byte[] documentField;

      private string descriptionField;

      private string mimeTypeField;

      private signType signTypeField;

      private bool failOnErrorsField;

      private Property[] propertiesField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string DocumentId
      {
         get
         {
            return this.documentIdField;
         }
         set
         {
            this.documentIdField = value;
            this.RaisePropertyChanged("DocumentId");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "base64Binary", Order = 1)]
      public byte[] Document
      {
         get
         {
            return this.documentField;
         }
         set
         {
            this.documentField = value;
            this.RaisePropertyChanged("Document");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 2)]
      public string Description
      {
         get
         {
            return this.descriptionField;
         }
         set
         {
            this.descriptionField = value;
            this.RaisePropertyChanged("Description");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 3)]
      public string MimeType
      {
         get
         {
            return this.mimeTypeField;
         }
         set
         {
            this.mimeTypeField = value;
            this.RaisePropertyChanged("MimeType");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 4)]
      public signType SignType
      {
         get
         {
            return this.signTypeField;
         }
         set
         {
            this.signTypeField = value;
            this.RaisePropertyChanged("SignType");
         }
      }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public bool FailOnErrors {
            get {
                return this.failOnErrorsField;
            }
            set {
                this.failOnErrorsField = value;
                this.RaisePropertyChanged("FailOnErrors");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public Property[] Properties {
            get {
                return this.propertiesField;
            }
            set {
                this.propertiesField = value;
                this.RaisePropertyChanged("Properties");
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

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3190.0")]
   [System.SerializableAttribute()]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public enum signType
   {

      /// <remarks/>
      CADES,

      /// <remarks/>
      XMLDSIG,

      /// <remarks/>
      XADES,

      /// <remarks/>
      EIS,

      /// <remarks/>
      DEFERRED,
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class CreateAnnotationRequest1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.CreateAnnotationRequest CreateAnnotation;

      public CreateAnnotationRequest1()
      {
      }

      public CreateAnnotationRequest1(RestChild.CshedIntegration.CshedRefBiz.CreateAnnotationRequest CreateAnnotation)
      {
         this.CreateAnnotation = CreateAnnotation;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class CreateAnnotationResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public string AnnotationId;

      public CreateAnnotationResponse()
      {
      }

      public CreateAnnotationResponse(string AnnotationId)
      {
         this.AnnotationId = AnnotationId;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class RetrieveAnnotationsRequest
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest RetrieveAnnotations;

      public RetrieveAnnotationsRequest()
      {
      }

      public RetrieveAnnotationsRequest(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest RetrieveAnnotations)
      {
         this.RetrieveAnnotations = RetrieveAnnotations;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class RetrieveAnnotationsResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlArrayAttribute(IsNullable = true)]
      [System.Xml.Serialization.XmlArrayItemAttribute("item", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
      public string[] AnnotationIds;

      public RetrieveAnnotationsResponse()
      {
      }

      public RetrieveAnnotationsResponse(string[] AnnotationIds)
      {
         this.AnnotationIds = AnnotationIds;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class RetrieveAnnotationRequest : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string documentIdField;

      private string annotationIdField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string DocumentId
      {
         get
         {
            return this.documentIdField;
         }
         set
         {
            this.documentIdField = value;
            this.RaisePropertyChanged("DocumentId");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
      public string AnnotationId
      {
         get
         {
            return this.annotationIdField;
         }
         set
         {
            this.annotationIdField = value;
            this.RaisePropertyChanged("AnnotationId");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class RetrieveAnnotationRequest1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationRequest RetrieveAnnotation;

      public RetrieveAnnotationRequest1()
      {
      }

      public RetrieveAnnotationRequest1(RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationRequest RetrieveAnnotation)
      {
         this.RetrieveAnnotation = RetrieveAnnotation;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class RetrieveAnnotationResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary", IsNullable = true)]
      public byte[] Annotation;

      public RetrieveAnnotationResponse()
      {
      }

      public RetrieveAnnotationResponse(byte[] Annotation)
      {
         this.Annotation = Annotation;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class DeleteAnnotationRequest
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationRequest DeleteAnnotation;

      public DeleteAnnotationRequest()
      {
      }

      public DeleteAnnotationRequest(RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationRequest DeleteAnnotation)
      {
         this.DeleteAnnotation = DeleteAnnotation;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class DeleteAnnotationResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      public bool DeleteAnnotationResult;

      public DeleteAnnotationResponse()
      {
      }

      public DeleteAnnotationResponse(bool DeleteAnnotationResult)
      {
         this.DeleteAnnotationResult = DeleteAnnotationResult;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class RetrieveDocAndAnnotationsResponse : object, System.ComponentModel.INotifyPropertyChanged
   {

      private byte[] documentField;

      private byte[][] annotationsField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "base64Binary", Order = 0)]
      public byte[] Document
      {
         get
         {
            return this.documentField;
         }
         set
         {
            this.documentField = value;
            this.RaisePropertyChanged("Document");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
      [System.Xml.Serialization.XmlArrayItemAttribute("Annotation", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "base64Binary", IsNullable = false)]
      public byte[][] annotations
      {
         get
         {
            return this.annotationsField;
         }
         set
         {
            this.annotationsField = value;
            this.RaisePropertyChanged("annotations");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class RetrieveDocAndAnnotationsRequest
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest RetrieveDocAndAnnotations;

      public RetrieveDocAndAnnotationsRequest()
      {
      }

      public RetrieveDocAndAnnotationsRequest(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest RetrieveDocAndAnnotations)
      {
         this.RetrieveDocAndAnnotations = RetrieveDocAndAnnotations;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class RetrieveDocAndAnnotationsResponse1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.RetrieveDocAndAnnotationsResponse RetrieveDocAndAnnotationsResponse;

      public RetrieveDocAndAnnotationsResponse1()
      {
      }

      public RetrieveDocAndAnnotationsResponse1(RestChild.CshedIntegration.CshedRefBiz.RetrieveDocAndAnnotationsResponse RetrieveDocAndAnnotationsResponse)
      {
         this.RetrieveDocAndAnnotationsResponse = RetrieveDocAndAnnotationsResponse;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class ValidateDocumentRequest
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest ValidateDocument;

      public ValidateDocumentRequest()
      {
      }

      public ValidateDocumentRequest(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest ValidateDocument)
      {
         this.ValidateDocument = ValidateDocument;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class ValidateDocumentResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlArrayAttribute(IsNullable = true)]
      [System.Xml.Serialization.XmlArrayItemAttribute("item", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
      public System.Nullable<bool>[] ValidateDocumentStatuses;

      public ValidateDocumentResponse()
      {
      }

      public ValidateDocumentResponse(System.Nullable<bool>[] ValidateDocumentStatuses)
      {
         this.ValidateDocumentStatuses = ValidateDocumentStatuses;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class ValidateDocumentWithInfoResponse : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string documentIdField;

      private ValidateDocumentInfo[] validateInfoField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string DocumentId
      {
         get
         {
            return this.documentIdField;
         }
         set
         {
            this.documentIdField = value;
            this.RaisePropertyChanged("DocumentId");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
      [System.Xml.Serialization.XmlArrayItemAttribute("ValidateInfo", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
      public ValidateDocumentInfo[] validateInfo
      {
         get
         {
            return this.validateInfoField;
         }
         set
         {
            this.validateInfoField = value;
            this.RaisePropertyChanged("validateInfo");
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

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class ValidateDocumentInfo : object, System.ComponentModel.INotifyPropertyChanged
   {

      private Property[] propertiesField;

      /// <remarks/>
      [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
      public Property[] properties
      {
         get
         {
            return this.propertiesField;
         }
         set
         {
            this.propertiesField = value;
            this.RaisePropertyChanged("properties");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class ValidateDocumentWithInfoRequest
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest ValidateDocumentWithInfo;

      public ValidateDocumentWithInfoRequest()
      {
      }

      public ValidateDocumentWithInfoRequest(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest ValidateDocumentWithInfo)
      {
         this.ValidateDocumentWithInfo = ValidateDocumentWithInfo;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class ValidateDocumentWithInfoResponse1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentWithInfoResponse ValidateDocumentWithInfoResponse;

      public ValidateDocumentWithInfoResponse1()
      {
      }

      public ValidateDocumentWithInfoResponse1(RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentWithInfoResponse ValidateDocumentWithInfoResponse)
      {
         this.ValidateDocumentWithInfoResponse = ValidateDocumentWithInfoResponse;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetFileNetIdRequest
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetFileNetId;

      public GetFileNetIdRequest()
      {
      }

      public GetFileNetIdRequest(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetFileNetId)
      {
         this.GetFileNetId = GetFileNetId;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetFileNetIdResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public string FileNetId;

      public GetFileNetIdResponse()
      {
      }

      public GetFileNetIdResponse(string FileNetId)
      {
         this.FileNetId = FileNetId;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetCustomIdRequest
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetCustomId;

      public GetCustomIdRequest()
      {
      }

      public GetCustomIdRequest(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetCustomId)
      {
         this.GetCustomId = GetCustomId;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class GetCustomIdResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public string CustomId;

      public GetCustomIdResponse()
      {
      }

      public GetCustomIdResponse(string CustomId)
      {
         this.CustomId = CustomId;
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://cloud.mos.ru/customWebService2/")]
   public partial class CopyDocumentRequest : object, System.ComponentModel.INotifyPropertyChanged
   {

      private string documentIdField;

      private string targetURLField;

      private string targetOSField;

      private string targetUsernameField;

      private string targetPasswordField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
      public string documentId
      {
         get
         {
            return this.documentIdField;
         }
         set
         {
            this.documentIdField = value;
            this.RaisePropertyChanged("documentId");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
      public string targetURL
      {
         get
         {
            return this.targetURLField;
         }
         set
         {
            this.targetURLField = value;
            this.RaisePropertyChanged("targetURL");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 2)]
      public string targetOS
      {
         get
         {
            return this.targetOSField;
         }
         set
         {
            this.targetOSField = value;
            this.RaisePropertyChanged("targetOS");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 3)]
      public string targetUsername
      {
         get
         {
            return this.targetUsernameField;
         }
         set
         {
            this.targetUsernameField = value;
            this.RaisePropertyChanged("targetUsername");
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 4)]
      public string targetPassword
      {
         get
         {
            return this.targetPasswordField;
         }
         set
         {
            this.targetPasswordField = value;
            this.RaisePropertyChanged("targetPassword");
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

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class CopyDocumentRequest1
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.CopyDocumentRequest CopyDocument;

      public CopyDocumentRequest1()
      {
      }

      public CopyDocumentRequest1(RestChild.CshedIntegration.CshedRefBiz.CopyDocumentRequest CopyDocument)
      {
         this.CopyDocument = CopyDocument;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class CopyDocumentResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public string FileNetId;

      public CopyDocumentResponse()
      {
      }

      public CopyDocumentResponse(string FileNetId)
      {
         this.FileNetId = FileNetId;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class moveDocumentRequest
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest moveDocument;

      public moveDocumentRequest()
      {
      }

      public moveDocumentRequest(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest moveDocument)
      {
         this.moveDocument = moveDocument;
      }
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
   [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
   public partial class moveDocumentResponse
   {

      [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://cloud.mos.ru/customWebService2/", Order = 0)]
      [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
      public string movedCustomId;

      public moveDocumentResponse()
      {
      }

      public moveDocumentResponse(string movedCustomId)
      {
         this.movedCustomId = movedCustomId;
      }
   }

   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   public interface CustomWebServiceImplChannel : RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl, System.ServiceModel.IClientChannel
   {
   }

   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
   public partial class CustomWebServiceImplClient : System.ServiceModel.ClientBase<RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl>, RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl
   {

      public CustomWebServiceImplClient()
      {
      }

      public CustomWebServiceImplClient(string endpointConfigurationName) :
              base(endpointConfigurationName)
      {
      }

      public CustomWebServiceImplClient(string endpointConfigurationName, string remoteAddress) :
              base(endpointConfigurationName, remoteAddress)
      {
      }

      public CustomWebServiceImplClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
              base(endpointConfigurationName, remoteAddress)
      {
      }

      public CustomWebServiceImplClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
              base(binding, remoteAddress)
      {
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.CreateDocumentResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.CreateDocument(RestChild.CshedIntegration.CshedRefBiz.CreateDocumentRequest1 request)
      {
         return base.Channel.CreateDocument(request);
      }

      public string CreateDocument(RestChild.CshedIntegration.CshedRefBiz.CreateDocumentRequest CreateDocument1)
      {
         RestChild.CshedIntegration.CshedRefBiz.CreateDocumentRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.CreateDocumentRequest1();
         inValue.CreateDocument = CreateDocument1;
         RestChild.CshedIntegration.CshedRefBiz.CreateDocumentResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).CreateDocument(inValue);
         return retVal.CustomID;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.CreateDocumentResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.CreateDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.CreateDocumentRequest1 request)
      {
         return base.Channel.CreateDocumentAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.CreateDocumentResponse> CreateDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.CreateDocumentRequest CreateDocument)
      {
         RestChild.CshedIntegration.CshedRefBiz.CreateDocumentRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.CreateDocumentRequest1();
         inValue.CreateDocument = CreateDocument;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).CreateDocumentAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentVersionResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.UpdateDocumentVersion(RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentVersionRequest request)
      {
         return base.Channel.UpdateDocumentVersion(request);
      }

      public string UpdateDocumentVersion(RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentRequest UpdateDocumentVersion1)
      {
         RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentVersionRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentVersionRequest();
         inValue.UpdateDocumentVersion = UpdateDocumentVersion1;
         RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentVersionResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).UpdateDocumentVersion(inValue);
         return retVal.UpdatedId;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentVersionResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.UpdateDocumentVersionAsync(RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentVersionRequest request)
      {
         return base.Channel.UpdateDocumentVersionAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentVersionResponse> UpdateDocumentVersionAsync(RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentRequest UpdateDocumentVersion)
      {
         RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentVersionRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentVersionRequest();
         inValue.UpdateDocumentVersion = UpdateDocumentVersion;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).UpdateDocumentVersionAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.GetDocumentResponse1 RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetDocument(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest1 request)
      {
         return base.Channel.GetDocument(request);
      }

      public RestChild.CshedIntegration.CshedRefBiz.GetDocumentResponse GetDocument(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetDocument1)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest1();
         inValue.GetDocument = GetDocument1;
         RestChild.CshedIntegration.CshedRefBiz.GetDocumentResponse1 retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetDocument(inValue);
         return retVal.GetDocumentResponse;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetDocumentResponse1> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest1 request)
      {
         return base.Channel.GetDocumentAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetDocumentResponse1> GetDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetDocument)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest1();
         inValue.GetDocument = GetDocument;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetDocumentAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.GetDocumentDataResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetDocumentData(RestChild.CshedIntegration.CshedRefBiz.GetDocumentDataRequest request)
      {
         return base.Channel.GetDocumentData(request);
      }

      public byte[] GetDocumentData(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetDocumentData1)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetDocumentDataRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.GetDocumentDataRequest();
         inValue.GetDocumentData = GetDocumentData1;
         RestChild.CshedIntegration.CshedRefBiz.GetDocumentDataResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetDocumentData(inValue);
         return retVal.DocumentData;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetDocumentDataResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetDocumentDataAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentDataRequest request)
      {
         return base.Channel.GetDocumentDataAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetDocumentDataResponse> GetDocumentDataAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetDocumentData)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetDocumentDataRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.GetDocumentDataRequest();
         inValue.GetDocumentData = GetDocumentData;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetDocumentDataAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.GetSignedDocumentDataResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetSignedDocumentData(RestChild.CshedIntegration.CshedRefBiz.GetSignedDocumentDataRequest request)
      {
         return base.Channel.GetSignedDocumentData(request);
      }

      public byte[] GetSignedDocumentData(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetSignedDocumentData1)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetSignedDocumentDataRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.GetSignedDocumentDataRequest();
         inValue.GetSignedDocumentData = GetSignedDocumentData1;
         RestChild.CshedIntegration.CshedRefBiz.GetSignedDocumentDataResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetSignedDocumentData(inValue);
         return retVal.SignedDocumentData;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetSignedDocumentDataResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetSignedDocumentDataAsync(RestChild.CshedIntegration.CshedRefBiz.GetSignedDocumentDataRequest request)
      {
         return base.Channel.GetSignedDocumentDataAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetSignedDocumentDataResponse> GetSignedDocumentDataAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetSignedDocumentData)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetSignedDocumentDataRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.GetSignedDocumentDataRequest();
         inValue.GetSignedDocumentData = GetSignedDocumentData;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetSignedDocumentDataAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.GetDocumentPropertiesResponse1 RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetDocumentProperties(RestChild.CshedIntegration.CshedRefBiz.GetDocumentPropertiesRequest request)
      {
         return base.Channel.GetDocumentProperties(request);
      }

      public RestChild.CshedIntegration.CshedRefBiz.GetDocumentPropertiesResponse GetDocumentProperties(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetDocumentProperties1)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetDocumentPropertiesRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.GetDocumentPropertiesRequest();
         inValue.GetDocumentProperties = GetDocumentProperties1;
         RestChild.CshedIntegration.CshedRefBiz.GetDocumentPropertiesResponse1 retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetDocumentProperties(inValue);
         return retVal.GetDocumentPropertiesResponse;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetDocumentPropertiesResponse1> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetDocumentPropertiesAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentPropertiesRequest request)
      {
         return base.Channel.GetDocumentPropertiesAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetDocumentPropertiesResponse1> GetDocumentPropertiesAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetDocumentProperties)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetDocumentPropertiesRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.GetDocumentPropertiesRequest();
         inValue.GetDocumentProperties = GetDocumentProperties;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetDocumentPropertiesAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.SearchDocumentsResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.SearchDocuments(RestChild.CshedIntegration.CshedRefBiz.SearchDocumentsRequest1 request)
      {
         return base.Channel.SearchDocuments(request);
      }

      public string[] SearchDocuments(RestChild.CshedIntegration.CshedRefBiz.SearchDocumentsRequest SearchDocuments1)
      {
         RestChild.CshedIntegration.CshedRefBiz.SearchDocumentsRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.SearchDocumentsRequest1();
         inValue.SearchDocuments = SearchDocuments1;
         RestChild.CshedIntegration.CshedRefBiz.SearchDocumentsResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).SearchDocuments(inValue);
         return retVal.CustomIDs;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.SearchDocumentsResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.SearchDocumentsAsync(RestChild.CshedIntegration.CshedRefBiz.SearchDocumentsRequest1 request)
      {
         return base.Channel.SearchDocumentsAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.SearchDocumentsResponse> SearchDocumentsAsync(RestChild.CshedIntegration.CshedRefBiz.SearchDocumentsRequest SearchDocuments)
      {
         RestChild.CshedIntegration.CshedRefBiz.SearchDocumentsRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.SearchDocumentsRequest1();
         inValue.SearchDocuments = SearchDocuments;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).SearchDocumentsAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentsResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.DeleteDocuments(RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentsRequest request)
      {
         return base.Channel.DeleteDocuments(request);
      }

      public System.Nullable<int> DeleteDocuments(string[] DeleteDocuments1)
      {
         RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentsRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentsRequest();
         inValue.DeleteDocuments = DeleteDocuments1;
         RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentsResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).DeleteDocuments(inValue);
         return retVal.NumOfDeleted;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentsResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.DeleteDocumentsAsync(RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentsRequest request)
      {
         return base.Channel.DeleteDocumentsAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentsResponse> DeleteDocumentsAsync(string[] DeleteDocuments)
      {
         RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentsRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentsRequest();
         inValue.DeleteDocuments = DeleteDocuments;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).DeleteDocumentsAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.CreateFolderResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.CreateFolder(RestChild.CshedIntegration.CshedRefBiz.CreateFolderRequest1 request)
      {
         return base.Channel.CreateFolder(request);
      }

      public string CreateFolder(RestChild.CshedIntegration.CshedRefBiz.CreateFolderRequest CreateFolder1)
      {
         RestChild.CshedIntegration.CshedRefBiz.CreateFolderRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.CreateFolderRequest1();
         inValue.CreateFolder = CreateFolder1;
         RestChild.CshedIntegration.CshedRefBiz.CreateFolderResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).CreateFolder(inValue);
         return retVal.FolderPath;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.CreateFolderResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.CreateFolderAsync(RestChild.CshedIntegration.CshedRefBiz.CreateFolderRequest1 request)
      {
         return base.Channel.CreateFolderAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.CreateFolderResponse> CreateFolderAsync(RestChild.CshedIntegration.CshedRefBiz.CreateFolderRequest CreateFolder)
      {
         RestChild.CshedIntegration.CshedRefBiz.CreateFolderRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.CreateFolderRequest1();
         inValue.CreateFolder = CreateFolder;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).CreateFolderAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.GetDocumentsInFolderResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetDocumentsInFolder(RestChild.CshedIntegration.CshedRefBiz.GetDocumentsInFolderRequest request)
      {
         return base.Channel.GetDocumentsInFolder(request);
      }

      public string[] GetDocumentsInFolder(RestChild.CshedIntegration.CshedRefBiz.GetFilesInFolderRequest GetDocumentsInFolder1)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetDocumentsInFolderRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.GetDocumentsInFolderRequest();
         inValue.GetDocumentsInFolder = GetDocumentsInFolder1;
         RestChild.CshedIntegration.CshedRefBiz.GetDocumentsInFolderResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetDocumentsInFolder(inValue);
         return retVal.DocumentsInFolder;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetDocumentsInFolderResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetDocumentsInFolderAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentsInFolderRequest request)
      {
         return base.Channel.GetDocumentsInFolderAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetDocumentsInFolderResponse> GetDocumentsInFolderAsync(RestChild.CshedIntegration.CshedRefBiz.GetFilesInFolderRequest GetDocumentsInFolder)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetDocumentsInFolderRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.GetDocumentsInFolderRequest();
         inValue.GetDocumentsInFolder = GetDocumentsInFolder;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetDocumentsInFolderAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.GetFoldersInFolderResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetFoldersInFolder(RestChild.CshedIntegration.CshedRefBiz.GetFoldersInFolderRequest request)
      {
         return base.Channel.GetFoldersInFolder(request);
      }

      public string[] GetFoldersInFolder(RestChild.CshedIntegration.CshedRefBiz.GetFilesInFolderRequest GetFoldersInFolder1)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetFoldersInFolderRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.GetFoldersInFolderRequest();
         inValue.GetFoldersInFolder = GetFoldersInFolder1;
         RestChild.CshedIntegration.CshedRefBiz.GetFoldersInFolderResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetFoldersInFolder(inValue);
         return retVal.FoldersInFolder;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetFoldersInFolderResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetFoldersInFolderAsync(RestChild.CshedIntegration.CshedRefBiz.GetFoldersInFolderRequest request)
      {
         return base.Channel.GetFoldersInFolderAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetFoldersInFolderResponse> GetFoldersInFolderAsync(RestChild.CshedIntegration.CshedRefBiz.GetFilesInFolderRequest GetFoldersInFolder)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetFoldersInFolderRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.GetFoldersInFolderRequest();
         inValue.GetFoldersInFolder = GetFoldersInFolder;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetFoldersInFolderAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.GetParentFolderResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetParentFolder(RestChild.CshedIntegration.CshedRefBiz.GetParentFolderRequest1 request)
      {
         return base.Channel.GetParentFolder(request);
      }

      public string GetParentFolder(RestChild.CshedIntegration.CshedRefBiz.GetParentFolderRequest GetParentFolder1)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetParentFolderRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.GetParentFolderRequest1();
         inValue.GetParentFolder = GetParentFolder1;
         RestChild.CshedIntegration.CshedRefBiz.GetParentFolderResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetParentFolder(inValue);
         return retVal.ParentFolder;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetParentFolderResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetParentFolderAsync(RestChild.CshedIntegration.CshedRefBiz.GetParentFolderRequest1 request)
      {
         return base.Channel.GetParentFolderAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetParentFolderResponse> GetParentFolderAsync(RestChild.CshedIntegration.CshedRefBiz.GetParentFolderRequest GetParentFolder)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetParentFolderRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.GetParentFolderRequest1();
         inValue.GetParentFolder = GetParentFolder;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetParentFolderAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentVersionResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.DeleteDocumentVersion(RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentVersionRequest1 request)
      {
         return base.Channel.DeleteDocumentVersion(request);
      }

      public string DeleteDocumentVersion(RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentVersionRequest DeleteDocumentVersion1)
      {
         RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentVersionRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentVersionRequest1();
         inValue.DeleteDocumentVersion = DeleteDocumentVersion1;
         RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentVersionResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).DeleteDocumentVersion(inValue);
         return retVal.DeleteDocumentVersionResponse1;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentVersionResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.DeleteDocumentVersionAsync(RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentVersionRequest1 request)
      {
         return base.Channel.DeleteDocumentVersionAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentVersionResponse> DeleteDocumentVersionAsync(RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentVersionRequest DeleteDocumentVersion)
      {
         RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentVersionRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.DeleteDocumentVersionRequest1();
         inValue.DeleteDocumentVersion = DeleteDocumentVersion;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).DeleteDocumentVersionAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentPropertiesResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.UpdateDocumentProperties(RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentPropertiesRequest request)
      {
         return base.Channel.UpdateDocumentProperties(request);
      }

      public string UpdateDocumentProperties(RestChild.CshedIntegration.CshedRefBiz.UpdatePropertiesRequest UpdateDocumentProperties1)
      {
         RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentPropertiesRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentPropertiesRequest();
         inValue.UpdateDocumentProperties = UpdateDocumentProperties1;
         RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentPropertiesResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).UpdateDocumentProperties(inValue);
         return retVal.UpdateDocumentPropertiesResponse1;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentPropertiesResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.UpdateDocumentPropertiesAsync(RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentPropertiesRequest request)
      {
         return base.Channel.UpdateDocumentPropertiesAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentPropertiesResponse> UpdateDocumentPropertiesAsync(RestChild.CshedIntegration.CshedRefBiz.UpdatePropertiesRequest UpdateDocumentProperties)
      {
         RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentPropertiesRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.UpdateDocumentPropertiesRequest();
         inValue.UpdateDocumentProperties = UpdateDocumentProperties;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).UpdateDocumentPropertiesAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.FolderDocumentResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.FolderDocument(RestChild.CshedIntegration.CshedRefBiz.FolderDocumentRequest1 request)
      {
         return base.Channel.FolderDocument(request);
      }

      public string FolderDocument(RestChild.CshedIntegration.CshedRefBiz.FolderDocumentRequest FolderDocument1)
      {
         RestChild.CshedIntegration.CshedRefBiz.FolderDocumentRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.FolderDocumentRequest1();
         inValue.FolderDocument = FolderDocument1;
         RestChild.CshedIntegration.CshedRefBiz.FolderDocumentResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).FolderDocument(inValue);
         return retVal.FolderDocumentResponse1;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.FolderDocumentResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.FolderDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.FolderDocumentRequest1 request)
      {
         return base.Channel.FolderDocumentAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.FolderDocumentResponse> FolderDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.FolderDocumentRequest FolderDocument)
      {
         RestChild.CshedIntegration.CshedRefBiz.FolderDocumentRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.FolderDocumentRequest1();
         inValue.FolderDocument = FolderDocument;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).FolderDocumentAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.GetFoldersOfDocumentResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetFoldersOfDocument(RestChild.CshedIntegration.CshedRefBiz.GetFoldersOfDocumentRequest1 request)
      {
         return base.Channel.GetFoldersOfDocument(request);
      }

      public string[] GetFoldersOfDocument(RestChild.CshedIntegration.CshedRefBiz.GetFoldersOfDocumentRequest GetFoldersOfDocument1)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetFoldersOfDocumentRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.GetFoldersOfDocumentRequest1();
         inValue.GetFoldersOfDocument = GetFoldersOfDocument1;
         RestChild.CshedIntegration.CshedRefBiz.GetFoldersOfDocumentResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetFoldersOfDocument(inValue);
         return retVal.FoldersOfDocument;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetFoldersOfDocumentResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetFoldersOfDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.GetFoldersOfDocumentRequest1 request)
      {
         return base.Channel.GetFoldersOfDocumentAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetFoldersOfDocumentResponse> GetFoldersOfDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.GetFoldersOfDocumentRequest GetFoldersOfDocument)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetFoldersOfDocumentRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.GetFoldersOfDocumentRequest1();
         inValue.GetFoldersOfDocument = GetFoldersOfDocument;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetFoldersOfDocumentAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.SetDocumentPermissionsResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.SetDocumentPermissions(RestChild.CshedIntegration.CshedRefBiz.SetDocumentPermissionsRequest1 request)
      {
         return base.Channel.SetDocumentPermissions(request);
      }

      public string SetDocumentPermissions(RestChild.CshedIntegration.CshedRefBiz.SetDocumentPermissionsRequest SetDocumentPermissions1)
      {
         RestChild.CshedIntegration.CshedRefBiz.SetDocumentPermissionsRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.SetDocumentPermissionsRequest1();
         inValue.SetDocumentPermissions = SetDocumentPermissions1;
         RestChild.CshedIntegration.CshedRefBiz.SetDocumentPermissionsResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).SetDocumentPermissions(inValue);
         return retVal.SetDocumentPermissionsResponse1;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.SetDocumentPermissionsResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.SetDocumentPermissionsAsync(RestChild.CshedIntegration.CshedRefBiz.SetDocumentPermissionsRequest1 request)
      {
         return base.Channel.SetDocumentPermissionsAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.SetDocumentPermissionsResponse> SetDocumentPermissionsAsync(RestChild.CshedIntegration.CshedRefBiz.SetDocumentPermissionsRequest SetDocumentPermissions)
      {
         RestChild.CshedIntegration.CshedRefBiz.SetDocumentPermissionsRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.SetDocumentPermissionsRequest1();
         inValue.SetDocumentPermissions = SetDocumentPermissions;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).SetDocumentPermissionsAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.SetFolderPermissionsResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.SetFolderPermissions(RestChild.CshedIntegration.CshedRefBiz.SetFolderPermissionsRequest1 request)
      {
         return base.Channel.SetFolderPermissions(request);
      }

      public string SetFolderPermissions(RestChild.CshedIntegration.CshedRefBiz.SetFolderPermissionsRequest SetFolderPermissions1)
      {
         RestChild.CshedIntegration.CshedRefBiz.SetFolderPermissionsRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.SetFolderPermissionsRequest1();
         inValue.SetFolderPermissions = SetFolderPermissions1;
         RestChild.CshedIntegration.CshedRefBiz.SetFolderPermissionsResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).SetFolderPermissions(inValue);
         return retVal.SetFolderPermissionsResponse1;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.SetFolderPermissionsResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.SetFolderPermissionsAsync(RestChild.CshedIntegration.CshedRefBiz.SetFolderPermissionsRequest1 request)
      {
         return base.Channel.SetFolderPermissionsAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.SetFolderPermissionsResponse> SetFolderPermissionsAsync(RestChild.CshedIntegration.CshedRefBiz.SetFolderPermissionsRequest SetFolderPermissions)
      {
         RestChild.CshedIntegration.CshedRefBiz.SetFolderPermissionsRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.SetFolderPermissionsRequest1();
         inValue.SetFolderPermissions = SetFolderPermissions;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).SetFolderPermissionsAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.GetDocumentPermissionsResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetDocumentPermissions(RestChild.CshedIntegration.CshedRefBiz.GetDocumentPermissionsRequest1 request)
      {
         return base.Channel.GetDocumentPermissions(request);
      }

      public RestChild.CshedIntegration.CshedRefBiz.GetPermissionsResponse GetDocumentPermissions(RestChild.CshedIntegration.CshedRefBiz.GetDocumentPermissionsRequest GetDocumentPermissions1)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetDocumentPermissionsRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.GetDocumentPermissionsRequest1();
         inValue.GetDocumentPermissions = GetDocumentPermissions1;
         RestChild.CshedIntegration.CshedRefBiz.GetDocumentPermissionsResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetDocumentPermissions(inValue);
         return retVal.PermissionsOfDocument;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetDocumentPermissionsResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetDocumentPermissionsAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentPermissionsRequest1 request)
      {
         return base.Channel.GetDocumentPermissionsAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetDocumentPermissionsResponse> GetDocumentPermissionsAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentPermissionsRequest GetDocumentPermissions)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetDocumentPermissionsRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.GetDocumentPermissionsRequest1();
         inValue.GetDocumentPermissions = GetDocumentPermissions;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetDocumentPermissionsAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.GetFolderPermissionsResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetFolderPermissions(RestChild.CshedIntegration.CshedRefBiz.GetFolderPermissionsRequest1 request)
      {
         return base.Channel.GetFolderPermissions(request);
      }

      public RestChild.CshedIntegration.CshedRefBiz.GetPermissionsResponse GetFolderPermissions(RestChild.CshedIntegration.CshedRefBiz.GetFolderPermissionsRequest GetFolderPermissions1)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetFolderPermissionsRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.GetFolderPermissionsRequest1();
         inValue.GetFolderPermissions = GetFolderPermissions1;
         RestChild.CshedIntegration.CshedRefBiz.GetFolderPermissionsResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetFolderPermissions(inValue);
         return retVal.PermissionsOfFolder;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetFolderPermissionsResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetFolderPermissionsAsync(RestChild.CshedIntegration.CshedRefBiz.GetFolderPermissionsRequest1 request)
      {
         return base.Channel.GetFolderPermissionsAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetFolderPermissionsResponse> GetFolderPermissionsAsync(RestChild.CshedIntegration.CshedRefBiz.GetFolderPermissionsRequest GetFolderPermissions)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetFolderPermissionsRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.GetFolderPermissionsRequest1();
         inValue.GetFolderPermissions = GetFolderPermissions;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetFolderPermissionsAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.CreateAnnotationResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.CreateAnnotation(RestChild.CshedIntegration.CshedRefBiz.CreateAnnotationRequest1 request)
      {
         return base.Channel.CreateAnnotation(request);
      }

      public string CreateAnnotation(RestChild.CshedIntegration.CshedRefBiz.CreateAnnotationRequest CreateAnnotation1)
      {
         RestChild.CshedIntegration.CshedRefBiz.CreateAnnotationRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.CreateAnnotationRequest1();
         inValue.CreateAnnotation = CreateAnnotation1;
         RestChild.CshedIntegration.CshedRefBiz.CreateAnnotationResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).CreateAnnotation(inValue);
         return retVal.AnnotationId;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.CreateAnnotationResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.CreateAnnotationAsync(RestChild.CshedIntegration.CshedRefBiz.CreateAnnotationRequest1 request)
      {
         return base.Channel.CreateAnnotationAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.CreateAnnotationResponse> CreateAnnotationAsync(RestChild.CshedIntegration.CshedRefBiz.CreateAnnotationRequest CreateAnnotation)
      {
         RestChild.CshedIntegration.CshedRefBiz.CreateAnnotationRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.CreateAnnotationRequest1();
         inValue.CreateAnnotation = CreateAnnotation;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).CreateAnnotationAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationsResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.RetrieveAnnotations(RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationsRequest request)
      {
         return base.Channel.RetrieveAnnotations(request);
      }

      public string[] RetrieveAnnotations(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest RetrieveAnnotations1)
      {
         RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationsRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationsRequest();
         inValue.RetrieveAnnotations = RetrieveAnnotations1;
         RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationsResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).RetrieveAnnotations(inValue);
         return retVal.AnnotationIds;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationsResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.RetrieveAnnotationsAsync(RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationsRequest request)
      {
         return base.Channel.RetrieveAnnotationsAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationsResponse> RetrieveAnnotationsAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest RetrieveAnnotations)
      {
         RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationsRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationsRequest();
         inValue.RetrieveAnnotations = RetrieveAnnotations;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).RetrieveAnnotationsAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.RetrieveAnnotation(RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationRequest1 request)
      {
         return base.Channel.RetrieveAnnotation(request);
      }

      public byte[] RetrieveAnnotation(RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationRequest RetrieveAnnotation1)
      {
         RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationRequest1();
         inValue.RetrieveAnnotation = RetrieveAnnotation1;
         RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).RetrieveAnnotation(inValue);
         return retVal.Annotation;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.RetrieveAnnotationAsync(RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationRequest1 request)
      {
         return base.Channel.RetrieveAnnotationAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationResponse> RetrieveAnnotationAsync(RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationRequest RetrieveAnnotation)
      {
         RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationRequest1();
         inValue.RetrieveAnnotation = RetrieveAnnotation;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).RetrieveAnnotationAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.DeleteAnnotationResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.DeleteAnnotation(RestChild.CshedIntegration.CshedRefBiz.DeleteAnnotationRequest request)
      {
         return base.Channel.DeleteAnnotation(request);
      }

      public bool DeleteAnnotation(RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationRequest DeleteAnnotation1)
      {
         RestChild.CshedIntegration.CshedRefBiz.DeleteAnnotationRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.DeleteAnnotationRequest();
         inValue.DeleteAnnotation = DeleteAnnotation1;
         RestChild.CshedIntegration.CshedRefBiz.DeleteAnnotationResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).DeleteAnnotation(inValue);
         return retVal.DeleteAnnotationResult;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.DeleteAnnotationResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.DeleteAnnotationAsync(RestChild.CshedIntegration.CshedRefBiz.DeleteAnnotationRequest request)
      {
         return base.Channel.DeleteAnnotationAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.DeleteAnnotationResponse> DeleteAnnotationAsync(RestChild.CshedIntegration.CshedRefBiz.RetrieveAnnotationRequest DeleteAnnotation)
      {
         RestChild.CshedIntegration.CshedRefBiz.DeleteAnnotationRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.DeleteAnnotationRequest();
         inValue.DeleteAnnotation = DeleteAnnotation;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).DeleteAnnotationAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.RetrieveDocAndAnnotationsResponse1 RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.RetrieveDocAndAnnotations(RestChild.CshedIntegration.CshedRefBiz.RetrieveDocAndAnnotationsRequest request)
      {
         return base.Channel.RetrieveDocAndAnnotations(request);
      }

      public RestChild.CshedIntegration.CshedRefBiz.RetrieveDocAndAnnotationsResponse RetrieveDocAndAnnotations(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest RetrieveDocAndAnnotations1)
      {
         RestChild.CshedIntegration.CshedRefBiz.RetrieveDocAndAnnotationsRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.RetrieveDocAndAnnotationsRequest();
         inValue.RetrieveDocAndAnnotations = RetrieveDocAndAnnotations1;
         RestChild.CshedIntegration.CshedRefBiz.RetrieveDocAndAnnotationsResponse1 retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).RetrieveDocAndAnnotations(inValue);
         return retVal.RetrieveDocAndAnnotationsResponse;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.RetrieveDocAndAnnotationsResponse1> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.RetrieveDocAndAnnotationsAsync(RestChild.CshedIntegration.CshedRefBiz.RetrieveDocAndAnnotationsRequest request)
      {
         return base.Channel.RetrieveDocAndAnnotationsAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.RetrieveDocAndAnnotationsResponse1> RetrieveDocAndAnnotationsAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest RetrieveDocAndAnnotations)
      {
         RestChild.CshedIntegration.CshedRefBiz.RetrieveDocAndAnnotationsRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.RetrieveDocAndAnnotationsRequest();
         inValue.RetrieveDocAndAnnotations = RetrieveDocAndAnnotations;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).RetrieveDocAndAnnotationsAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.ValidateDocument(RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentRequest request)
      {
         return base.Channel.ValidateDocument(request);
      }

      public System.Nullable<bool>[] ValidateDocument(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest ValidateDocument1)
      {
         RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentRequest();
         inValue.ValidateDocument = ValidateDocument1;
         RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).ValidateDocument(inValue);
         return retVal.ValidateDocumentStatuses;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.ValidateDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentRequest request)
      {
         return base.Channel.ValidateDocumentAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentResponse> ValidateDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest ValidateDocument)
      {
         RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentRequest();
         inValue.ValidateDocument = ValidateDocument;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).ValidateDocumentAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentWithInfoResponse1 RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.ValidateDocumentWithInfo(RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentWithInfoRequest request)
      {
         return base.Channel.ValidateDocumentWithInfo(request);
      }

      public RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentWithInfoResponse ValidateDocumentWithInfo(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest ValidateDocumentWithInfo1)
      {
         RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentWithInfoRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentWithInfoRequest();
         inValue.ValidateDocumentWithInfo = ValidateDocumentWithInfo1;
         RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentWithInfoResponse1 retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).ValidateDocumentWithInfo(inValue);
         return retVal.ValidateDocumentWithInfoResponse;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentWithInfoResponse1> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.ValidateDocumentWithInfoAsync(RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentWithInfoRequest request)
      {
         return base.Channel.ValidateDocumentWithInfoAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentWithInfoResponse1> ValidateDocumentWithInfoAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest ValidateDocumentWithInfo)
      {
         RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentWithInfoRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.ValidateDocumentWithInfoRequest();
         inValue.ValidateDocumentWithInfo = ValidateDocumentWithInfo;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).ValidateDocumentWithInfoAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.GetFileNetIdResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetFileNetId(RestChild.CshedIntegration.CshedRefBiz.GetFileNetIdRequest request)
      {
         return base.Channel.GetFileNetId(request);
      }

      public string GetFileNetId(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetFileNetId1)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetFileNetIdRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.GetFileNetIdRequest();
         inValue.GetFileNetId = GetFileNetId1;
         RestChild.CshedIntegration.CshedRefBiz.GetFileNetIdResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetFileNetId(inValue);
         return retVal.FileNetId;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetFileNetIdResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetFileNetIdAsync(RestChild.CshedIntegration.CshedRefBiz.GetFileNetIdRequest request)
      {
         return base.Channel.GetFileNetIdAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetFileNetIdResponse> GetFileNetIdAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetFileNetId)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetFileNetIdRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.GetFileNetIdRequest();
         inValue.GetFileNetId = GetFileNetId;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetFileNetIdAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.GetCustomIdResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetCustomId(RestChild.CshedIntegration.CshedRefBiz.GetCustomIdRequest request)
      {
         return base.Channel.GetCustomId(request);
      }

      public string GetCustomId(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetCustomId1)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetCustomIdRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.GetCustomIdRequest();
         inValue.GetCustomId = GetCustomId1;
         RestChild.CshedIntegration.CshedRefBiz.GetCustomIdResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetCustomId(inValue);
         return retVal.CustomId;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetCustomIdResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.GetCustomIdAsync(RestChild.CshedIntegration.CshedRefBiz.GetCustomIdRequest request)
      {
         return base.Channel.GetCustomIdAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.GetCustomIdResponse> GetCustomIdAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest GetCustomId)
      {
         RestChild.CshedIntegration.CshedRefBiz.GetCustomIdRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.GetCustomIdRequest();
         inValue.GetCustomId = GetCustomId;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).GetCustomIdAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.CopyDocumentResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.CopyDocument(RestChild.CshedIntegration.CshedRefBiz.CopyDocumentRequest1 request)
      {
         return base.Channel.CopyDocument(request);
      }

      public string CopyDocument(RestChild.CshedIntegration.CshedRefBiz.CopyDocumentRequest CopyDocument1)
      {
         RestChild.CshedIntegration.CshedRefBiz.CopyDocumentRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.CopyDocumentRequest1();
         inValue.CopyDocument = CopyDocument1;
         RestChild.CshedIntegration.CshedRefBiz.CopyDocumentResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).CopyDocument(inValue);
         return retVal.FileNetId;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.CopyDocumentResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.CopyDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.CopyDocumentRequest1 request)
      {
         return base.Channel.CopyDocumentAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.CopyDocumentResponse> CopyDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.CopyDocumentRequest CopyDocument)
      {
         RestChild.CshedIntegration.CshedRefBiz.CopyDocumentRequest1 inValue = new RestChild.CshedIntegration.CshedRefBiz.CopyDocumentRequest1();
         inValue.CopyDocument = CopyDocument;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).CopyDocumentAsync(inValue);
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      RestChild.CshedIntegration.CshedRefBiz.moveDocumentResponse RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.moveDocument(RestChild.CshedIntegration.CshedRefBiz.moveDocumentRequest request)
      {
         return base.Channel.moveDocument(request);
      }

      public string moveDocument(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest moveDocument1)
      {
         RestChild.CshedIntegration.CshedRefBiz.moveDocumentRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.moveDocumentRequest();
         inValue.moveDocument = moveDocument1;
         RestChild.CshedIntegration.CshedRefBiz.moveDocumentResponse retVal = ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).moveDocument(inValue);
         return retVal.movedCustomId;
      }

      [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
      System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.moveDocumentResponse> RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl.moveDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.moveDocumentRequest request)
      {
         return base.Channel.moveDocumentAsync(request);
      }

      public System.Threading.Tasks.Task<RestChild.CshedIntegration.CshedRefBiz.moveDocumentResponse> moveDocumentAsync(RestChild.CshedIntegration.CshedRefBiz.GetDocumentRequest moveDocument)
      {
         RestChild.CshedIntegration.CshedRefBiz.moveDocumentRequest inValue = new RestChild.CshedIntegration.CshedRefBiz.moveDocumentRequest();
         inValue.moveDocument = moveDocument;
         return ((RestChild.CshedIntegration.CshedRefBiz.CustomWebServiceImpl)(this)).moveDocumentAsync(inValue);
      }
   }
}
