
namespace RestChild.EGASIntegration.EGASPromDuo
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ehd.mos.com/")]
    public partial class authFault : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string faultCodeField;

        private string faultStringField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public string faultCode
        {
            get
            {
                return this.faultCodeField;
            }
            set
            {
                this.faultCodeField = value;
                this.RaisePropertyChanged("faultCode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
        public string faultString
        {
            get
            {
                return this.faultStringField;
            }
            set
            {
                this.faultStringField = value;
                this.RaisePropertyChanged("faultString");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ehd.mos.com/")]
    public partial class EhdCatalog : object, System.ComponentModel.INotifyPropertyChanged
    {

        private System.Nullable<long> idField;

        private bool idFieldSpecified;

        private string fullNameField;

        private string technicalNameField;

        private string shortNameField;

        private string accountingObjectField;

        private string keywordsField;

        private string vidField;

        private string typeField;

        private string periodField;

        private System.Nullable<bool> hasGeoField;

        private bool hasGeoFieldSpecified;

        private string categoriesField;

        private string oivField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 0)]
        public System.Nullable<long> id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                this.RaisePropertyChanged("id");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool idSpecified
        {
            get
            {
                return this.idFieldSpecified;
            }
            set
            {
                this.idFieldSpecified = value;
                this.RaisePropertyChanged("idSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 1)]
        public string fullName
        {
            get
            {
                return this.fullNameField;
            }
            set
            {
                this.fullNameField = value;
                this.RaisePropertyChanged("fullName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 2)]
        public string technicalName
        {
            get
            {
                return this.technicalNameField;
            }
            set
            {
                this.technicalNameField = value;
                this.RaisePropertyChanged("technicalName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 3)]
        public string shortName
        {
            get
            {
                return this.shortNameField;
            }
            set
            {
                this.shortNameField = value;
                this.RaisePropertyChanged("shortName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 4)]
        public string accountingObject
        {
            get
            {
                return this.accountingObjectField;
            }
            set
            {
                this.accountingObjectField = value;
                this.RaisePropertyChanged("accountingObject");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 5)]
        public string keywords
        {
            get
            {
                return this.keywordsField;
            }
            set
            {
                this.keywordsField = value;
                this.RaisePropertyChanged("keywords");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 6)]
        public string vid
        {
            get
            {
                return this.vidField;
            }
            set
            {
                this.vidField = value;
                this.RaisePropertyChanged("vid");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 7)]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
                this.RaisePropertyChanged("type");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 8)]
        public string period
        {
            get
            {
                return this.periodField;
            }
            set
            {
                this.periodField = value;
                this.RaisePropertyChanged("period");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 9)]
        public System.Nullable<bool> hasGeo
        {
            get
            {
                return this.hasGeoField;
            }
            set
            {
                this.hasGeoField = value;
                this.RaisePropertyChanged("hasGeo");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool hasGeoSpecified
        {
            get
            {
                return this.hasGeoFieldSpecified;
            }
            set
            {
                this.hasGeoFieldSpecified = value;
                this.RaisePropertyChanged("hasGeoSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 10)]
        public string categories
        {
            get
            {
                return this.categoriesField;
            }
            set
            {
                this.categoriesField = value;
                this.RaisePropertyChanged("categories");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 11)]
        public string oiv
        {
            get
            {
                return this.oivField;
            }
            set
            {
                this.oivField = value;
                this.RaisePropertyChanged("oiv");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ehd.mos.com/")]
    public partial class Exception : object, System.ComponentModel.INotifyPropertyChanged
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://ehd.mos.com/", ConfigurationName = "EGASPromDuo.soap")]
    public interface soap
    {

        // CODEGEN: Parameter 'ehdCatalogs' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlArrayItemAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getCatalogListRequest", ReplyAction = "http://ehd.mos.com/soap/getCatalogListResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(RestChild.EGASIntegration.EGASPromDuo.authFault), Action = "http://ehd.mos.com/soap/getCatalogList/Fault/AuthException", Name = "AuthFault")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "ehdCatalogs")]
        RestChild.EGASIntegration.EGASPromDuo.getCatalogListResponse getCatalogList(RestChild.EGASIntegration.EGASPromDuo.getCatalogListRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getCatalogListRequest", ReplyAction = "http://ehd.mos.com/soap/getCatalogListResponse")]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getCatalogListResponse> getCatalogListAsync(RestChild.EGASIntegration.EGASPromDuo.getCatalogListRequest request);

        // CODEGEN: Parameter 'idCatalog' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getCatalogSpecRequest", ReplyAction = "http://ehd.mos.com/soap/getCatalogSpecResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(RestChild.EGASIntegration.EGASPromDuo.authFault), Action = "http://ehd.mos.com/soap/getCatalogSpec/Fault/AuthException", Name = "AuthFault")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "ehdAttrSpec")]
        RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecResponse getCatalogSpec(RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getCatalogSpecRequest", ReplyAction = "http://ehd.mos.com/soap/getCatalogSpecResponse")]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecResponse> getCatalogSpecAsync(RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecRequest request);

        // CODEGEN: Parameter 'idCatalog' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getCatalogItemsRequest", ReplyAction = "http://ehd.mos.com/soap/getCatalogItemsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(RestChild.EGASIntegration.EGASPromDuo.authFault), Action = "http://ehd.mos.com/soap/getCatalogItems/Fault/AuthException", Name = "AuthFault")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "ehdCatalogItemsset")]
        RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsResponse getCatalogItems(RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getCatalogItemsRequest", ReplyAction = "http://ehd.mos.com/soap/getCatalogItemsResponse")]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsResponse> getCatalogItemsAsync(RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsRequest request);

        // CODEGEN: Parameter 'idCatalog' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getCatalogItemsNewRequest", ReplyAction = "http://ehd.mos.com/soap/getCatalogItemsNewResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(RestChild.EGASIntegration.EGASPromDuo.authFault), Action = "http://ehd.mos.com/soap/getCatalogItemsNew/Fault/AuthException", Name = "AuthFault")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "ehdCatalogItemsset")]
        RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsNewResponse getCatalogItemsNew(RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsNewRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getCatalogItemsNewRequest", ReplyAction = "http://ehd.mos.com/soap/getCatalogItemsNewResponse")]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsNewResponse> getCatalogItemsNewAsync(RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsNewRequest request);

        // CODEGEN: Parameter 'idCatalog' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getCatalogStatsRequest", ReplyAction = "http://ehd.mos.com/soap/getCatalogStatsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(RestChild.EGASIntegration.EGASPromDuo.authFault), Action = "http://ehd.mos.com/soap/getCatalogStats/Fault/AuthException", Name = "AuthFault")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "ehdCatalogStats")]
        RestChild.EGASIntegration.EGASPromDuo.getCatalogStatsResponse getCatalogStats(RestChild.EGASIntegration.EGASPromDuo.getCatalogStatsRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getCatalogStatsRequest", ReplyAction = "http://ehd.mos.com/soap/getCatalogStatsResponse")]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getCatalogStatsResponse> getCatalogStatsAsync(RestChild.EGASIntegration.EGASPromDuo.getCatalogStatsRequest request);

        // CODEGEN: Parameter 'ehdDictionaries' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlArrayItemAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getAllDictRequest", ReplyAction = "http://ehd.mos.com/soap/getAllDictResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(RestChild.EGASIntegration.EGASPromDuo.authFault), Action = "http://ehd.mos.com/soap/getAllDict/Fault/AuthException", Name = "AuthFault")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "ehdDictionaries")]
        RestChild.EGASIntegration.EGASPromDuo.getAllDictResponse getAllDict(RestChild.EGASIntegration.EGASPromDuo.getAllDictRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getAllDictRequest", ReplyAction = "http://ehd.mos.com/soap/getAllDictResponse")]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getAllDictResponse> getAllDictAsync(RestChild.EGASIntegration.EGASPromDuo.getAllDictRequest request);

        // CODEGEN: Parameter 'ehdDictionaryItems' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlArrayItemAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getDictItemRequest", ReplyAction = "http://ehd.mos.com/soap/getDictItemResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(RestChild.EGASIntegration.EGASPromDuo.authFault), Action = "http://ehd.mos.com/soap/getDictItem/Fault/AuthException", Name = "AuthFault")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "ehdDictionaryItems")]
        RestChild.EGASIntegration.EGASPromDuo.getDictItemResponse getDictItem(RestChild.EGASIntegration.EGASPromDuo.getDictItemRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getDictItemRequest", ReplyAction = "http://ehd.mos.com/soap/getDictItemResponse")]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getDictItemResponse> getDictItemAsync(RestChild.EGASIntegration.EGASPromDuo.getDictItemRequest request);

        // CODEGEN: Parameter 'getUserBySessionResponse' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getUserBySessionRequest", ReplyAction = "http://ehd.mos.com/soap/getUserBySessionResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(RestChild.EGASIntegration.EGASPromDuo.Exception), Action = "http://ehd.mos.com/soap/getUserBySession/Fault/Exception", Name = "Exception")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "getUserBySessionResponse")]
        RestChild.EGASIntegration.EGASPromDuo.getUserBySessionResponse getUserBySession(RestChild.EGASIntegration.EGASPromDuo.getUserBySessionRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getUserBySessionRequest", ReplyAction = "http://ehd.mos.com/soap/getUserBySessionResponse")]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getUserBySessionResponse> getUserBySessionAsync(RestChild.EGASIntegration.EGASPromDuo.getUserBySessionRequest request);

        // CODEGEN: Parameter 'ehdDictionaryItemsV2' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlArrayItemAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getDictItemV2Request", ReplyAction = "http://ehd.mos.com/soap/getDictItemV2Response")]
        [System.ServiceModel.FaultContractAttribute(typeof(RestChild.EGASIntegration.EGASPromDuo.authFault), Action = "http://ehd.mos.com/soap/getDictItemV2/Fault/AuthException", Name = "AuthFault")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "ehdDictionaryItemsV2")]
        RestChild.EGASIntegration.EGASPromDuo.getDictItemV2Response getDictItemV2(RestChild.EGASIntegration.EGASPromDuo.getDictItemV2Request request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getDictItemV2Request", ReplyAction = "http://ehd.mos.com/soap/getDictItemV2Response")]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getDictItemV2Response> getDictItemV2Async(RestChild.EGASIntegration.EGASPromDuo.getDictItemV2Request request);

        // CODEGEN: Parameter 'setDataResponse' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/setDataInRequest", ReplyAction = "http://ehd.mos.com/soap/setDataInResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(RestChild.EGASIntegration.EGASPromDuo.Exception), Action = "http://ehd.mos.com/soap/setDataIn/Fault/Exception", Name = "Exception")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "setDataResponse")]
        RestChild.EGASIntegration.EGASPromDuo.setDataInResponse setDataIn(RestChild.EGASIntegration.EGASPromDuo.setDataInRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/setDataInRequest", ReplyAction = "http://ehd.mos.com/soap/setDataInResponse")]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.setDataInResponse> setDataInAsync(RestChild.EGASIntegration.EGASPromDuo.setDataInRequest request);

        // CODEGEN: Parameter 'idCatalog' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getCatalogSpecNewRequest", ReplyAction = "http://ehd.mos.com/soap/getCatalogSpecNewResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(RestChild.EGASIntegration.EGASPromDuo.authFault), Action = "http://ehd.mos.com/soap/getCatalogSpecNew/Fault/AuthException", Name = "AuthFault")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "ehdAttrSpecNew")]
        RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecNewResponse getCatalogSpecNew(RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecNewRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ehd.mos.com/soap/getCatalogSpecNewRequest", ReplyAction = "http://ehd.mos.com/soap/getCatalogSpecNewResponse")]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecNewResponse> getCatalogSpecNewAsync(RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecNewRequest request);
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getCatalogList", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getCatalogListRequest
    {

        public getCatalogListRequest()
        {
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getCatalogListResponse", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getCatalogListResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("ehdCatalog")]
        public RestChild.EGASIntegration.EGASPromDuo.EhdCatalog[] ehdCatalogs;

        public getCatalogListResponse()
        {
        }

        public getCatalogListResponse(RestChild.EGASIntegration.EGASPromDuo.EhdCatalog[] ehdCatalogs)
        {
            this.ehdCatalogs = ehdCatalogs;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ehd.mos.com/")]
    public partial class EhdAttrSpec : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string geoTypeField;

        private string geoFormatField;

        private long countField;

        private EhdCommonAttribute[] ehdCommonAttributeField;

        private EhdException[] ehdExceptionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string geoType
        {
            get
            {
                return this.geoTypeField;
            }
            set
            {
                this.geoTypeField = value;
                this.RaisePropertyChanged("geoType");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://ehd.mos.com", Order = 1)]
        public string geoFormat
        {
            get
            {
                return this.geoFormatField;
            }
            set
            {
                this.geoFormatField = value;
                this.RaisePropertyChanged("geoFormat");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public long count
        {
            get
            {
                return this.countField;
            }
            set
            {
                this.countField = value;
                this.RaisePropertyChanged("count");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ehdCommonAttribute", IsNullable = true, Order = 3)]
        public EhdCommonAttribute[] ehdCommonAttribute
        {
            get
            {
                return this.ehdCommonAttributeField;
            }
            set
            {
                this.ehdCommonAttributeField = value;
                this.RaisePropertyChanged("ehdCommonAttribute");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ehdException", IsNullable = true, Order = 4)]
        public EhdException[] ehdException
        {
            get
            {
                return this.ehdExceptionField;
            }
            set
            {
                this.ehdExceptionField = value;
                this.RaisePropertyChanged("ehdException");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ehd.mos.com/")]
    public partial class EhdCommonAttribute : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string idField;

        private string typeIdField;

        private string nameField;

        private string typeField;

        private System.Nullable<bool> isPrimaryKeyField;

        private bool isPrimaryKeyFieldSpecified;

        private System.Nullable<bool> isEditField;

        private bool isEditFieldSpecified;

        private System.Nullable<bool> isReqField;

        private bool isReqFieldSpecified;

        private string fieldMaskField;

        private string tehNameField;

        private string maxLengthField;

        private System.Nullable<int> maxLengthDecimalField;

        private bool maxLengthDecimalFieldSpecified;

        private System.Nullable<int> dictIdField;

        private bool dictIdFieldSpecified;

        private System.Nullable<int> refCatalogField;

        private bool refCatalogFieldSpecified;

        private System.Nullable<int> isDeletedField;

        private bool isDeletedFieldSpecified;

        private System.Nullable<int> isDeletedTmpField;

        private bool isDeletedTmpFieldSpecified;

        private System.Nullable<bool> isMultiField;

        private bool isAutocompleteField;

        private bool isAutocompleteFieldSpecified;

        private int isManualField;

        private bool isManualFieldSpecified;

        private int isManualInputGeoField;

        private bool isManualInputGeoFieldSpecified;

        private string colnameField;

        private string fillForField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 0)]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                this.RaisePropertyChanged("id");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 1)]
        public string typeId
        {
            get
            {
                return this.typeIdField;
            }
            set
            {
                this.typeIdField = value;
                this.RaisePropertyChanged("typeId");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 2)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
                this.RaisePropertyChanged("name");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 3)]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
                this.RaisePropertyChanged("type");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 4)]
        public System.Nullable<bool> isPrimaryKey
        {
            get
            {
                return this.isPrimaryKeyField;
            }
            set
            {
                this.isPrimaryKeyField = value;
                this.RaisePropertyChanged("isPrimaryKey");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isPrimaryKeySpecified
        {
            get
            {
                return this.isPrimaryKeyFieldSpecified;
            }
            set
            {
                this.isPrimaryKeyFieldSpecified = value;
                this.RaisePropertyChanged("isPrimaryKeySpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 5)]
        public System.Nullable<bool> isEdit
        {
            get
            {
                return this.isEditField;
            }
            set
            {
                this.isEditField = value;
                this.RaisePropertyChanged("isEdit");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isEditSpecified
        {
            get
            {
                return this.isEditFieldSpecified;
            }
            set
            {
                this.isEditFieldSpecified = value;
                this.RaisePropertyChanged("isEditSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 6)]
        public System.Nullable<bool> isReq
        {
            get
            {
                return this.isReqField;
            }
            set
            {
                this.isReqField = value;
                this.RaisePropertyChanged("isReq");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isReqSpecified
        {
            get
            {
                return this.isReqFieldSpecified;
            }
            set
            {
                this.isReqFieldSpecified = value;
                this.RaisePropertyChanged("isReqSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 7)]
        public string fieldMask
        {
            get
            {
                return this.fieldMaskField;
            }
            set
            {
                this.fieldMaskField = value;
                this.RaisePropertyChanged("fieldMask");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 8)]
        public string tehName
        {
            get
            {
                return this.tehNameField;
            }
            set
            {
                this.tehNameField = value;
                this.RaisePropertyChanged("tehName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 9)]
        public string maxLength
        {
            get
            {
                return this.maxLengthField;
            }
            set
            {
                this.maxLengthField = value;
                this.RaisePropertyChanged("maxLength");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 10)]
        public System.Nullable<int> maxLengthDecimal
        {
            get
            {
                return this.maxLengthDecimalField;
            }
            set
            {
                this.maxLengthDecimalField = value;
                this.RaisePropertyChanged("maxLengthDecimal");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool maxLengthDecimalSpecified
        {
            get
            {
                return this.maxLengthDecimalFieldSpecified;
            }
            set
            {
                this.maxLengthDecimalFieldSpecified = value;
                this.RaisePropertyChanged("maxLengthDecimalSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 11)]
        public System.Nullable<int> dictId
        {
            get
            {
                return this.dictIdField;
            }
            set
            {
                this.dictIdField = value;
                this.RaisePropertyChanged("dictId");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool dictIdSpecified
        {
            get
            {
                return this.dictIdFieldSpecified;
            }
            set
            {
                this.dictIdFieldSpecified = value;
                this.RaisePropertyChanged("dictIdSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 12)]
        public System.Nullable<int> refCatalog
        {
            get
            {
                return this.refCatalogField;
            }
            set
            {
                this.refCatalogField = value;
                this.RaisePropertyChanged("refCatalog");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool refCatalogSpecified
        {
            get
            {
                return this.refCatalogFieldSpecified;
            }
            set
            {
                this.refCatalogFieldSpecified = value;
                this.RaisePropertyChanged("refCatalogSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 13)]
        public System.Nullable<int> isDeleted
        {
            get
            {
                return this.isDeletedField;
            }
            set
            {
                this.isDeletedField = value;
                this.RaisePropertyChanged("isDeleted");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isDeletedSpecified
        {
            get
            {
                return this.isDeletedFieldSpecified;
            }
            set
            {
                this.isDeletedFieldSpecified = value;
                this.RaisePropertyChanged("isDeletedSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 14)]
        public System.Nullable<int> isDeletedTmp
        {
            get
            {
                return this.isDeletedTmpField;
            }
            set
            {
                this.isDeletedTmpField = value;
                this.RaisePropertyChanged("isDeletedTmp");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isDeletedTmpSpecified
        {
            get
            {
                return this.isDeletedTmpFieldSpecified;
            }
            set
            {
                this.isDeletedTmpFieldSpecified = value;
                this.RaisePropertyChanged("isDeletedTmpSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 15)]
        public System.Nullable<bool> isMulti
        {
            get
            {
                return this.isMultiField;
            }
            set
            {
                this.isMultiField = value;
                this.RaisePropertyChanged("isMulti");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
        public bool isAutocomplete
        {
            get
            {
                return this.isAutocompleteField;
            }
            set
            {
                this.isAutocompleteField = value;
                this.RaisePropertyChanged("isAutocomplete");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isAutocompleteSpecified
        {
            get
            {
                return this.isAutocompleteFieldSpecified;
            }
            set
            {
                this.isAutocompleteFieldSpecified = value;
                this.RaisePropertyChanged("isAutocompleteSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
        public int isManual
        {
            get
            {
                return this.isManualField;
            }
            set
            {
                this.isManualField = value;
                this.RaisePropertyChanged("isManual");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isManualSpecified
        {
            get
            {
                return this.isManualFieldSpecified;
            }
            set
            {
                this.isManualFieldSpecified = value;
                this.RaisePropertyChanged("isManualSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
        public int isManualInputGeo
        {
            get
            {
                return this.isManualInputGeoField;
            }
            set
            {
                this.isManualInputGeoField = value;
                this.RaisePropertyChanged("isManualInputGeo");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isManualInputGeoSpecified
        {
            get
            {
                return this.isManualInputGeoFieldSpecified;
            }
            set
            {
                this.isManualInputGeoFieldSpecified = value;
                this.RaisePropertyChanged("isManualInputGeoSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 19)]
        public string colname
        {
            get
            {
                return this.colnameField;
            }
            set
            {
                this.colnameField = value;
                this.RaisePropertyChanged("colname");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 20)]
        public string fillFor
        {
            get
            {
                return this.fillForField;
            }
            set
            {
                this.fillForField = value;
                this.RaisePropertyChanged("fillFor");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ehd.mos.com/")]
    public partial class EhdException : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string errorCodeField;

        private string errorTextField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 0)]
        public string errorCode
        {
            get
            {
                return this.errorCodeField;
            }
            set
            {
                this.errorCodeField = value;
                this.RaisePropertyChanged("errorCode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 1)]
        public string errorText
        {
            get
            {
                return this.errorTextField;
            }
            set
            {
                this.errorTextField = value;
                this.RaisePropertyChanged("errorText");
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getCatalogSpec", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getCatalogSpecRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int idCatalog;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 1)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int version;

        public getCatalogSpecRequest()
        {
        }

        public getCatalogSpecRequest(int idCatalog, int version)
        {
            this.idCatalog = idCatalog;
            this.version = version;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getCatalogSpecResponse", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getCatalogSpecResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        public RestChild.EGASIntegration.EGASPromDuo.EhdAttrSpec ehdAttrSpec;

        public getCatalogSpecResponse()
        {
        }

        public getCatalogSpecResponse(RestChild.EGASIntegration.EGASPromDuo.EhdAttrSpec ehdAttrSpec)
        {
            this.ehdAttrSpec = ehdAttrSpec;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ehd.mos.com/")]
    public partial class EhdCatalogItems : object, System.ComponentModel.INotifyPropertyChanged
    {

        private EhdCatalogItem[] ehdCatalogItemField;

        private EhdException[] ehdExceptionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ehdCatalogAttr", Order = 0)]
        public EhdCatalogItem[] ehdCatalogItem
        {
            get
            {
                return this.ehdCatalogItemField;
            }
            set
            {
                this.ehdCatalogItemField = value;
                this.RaisePropertyChanged("ehdCatalogItem");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ehd.mos.com/")]
    public partial class EhdCatalogItem : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string idField;

        private System.Nullable<int> isDeletedField;

        private bool isDeletedFieldSpecified;

        private System.Nullable<int> isDeletedTmpField;

        private bool isDeletedTmpFieldSpecified;

        private string tehNameField;

        private string typeField;

        private string dictValueField;

        private object valueField;

        private EhdCatalogItems groupValueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 0)]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                this.RaisePropertyChanged("id");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 1)]
        public System.Nullable<int> isDeleted
        {
            get
            {
                return this.isDeletedField;
            }
            set
            {
                this.isDeletedField = value;
                this.RaisePropertyChanged("isDeleted");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isDeletedSpecified
        {
            get
            {
                return this.isDeletedFieldSpecified;
            }
            set
            {
                this.isDeletedFieldSpecified = value;
                this.RaisePropertyChanged("isDeletedSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 2)]
        public System.Nullable<int> isDeletedTmp
        {
            get
            {
                return this.isDeletedTmpField;
            }
            set
            {
                this.isDeletedTmpField = value;
                this.RaisePropertyChanged("isDeletedTmp");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isDeletedTmpSpecified
        {
            get
            {
                return this.isDeletedTmpFieldSpecified;
            }
            set
            {
                this.isDeletedTmpFieldSpecified = value;
                this.RaisePropertyChanged("isDeletedTmpSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 3)]
        public string tehName
        {
            get
            {
                return this.tehNameField;
            }
            set
            {
                this.tehNameField = value;
                this.RaisePropertyChanged("tehName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 4)]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
                this.RaisePropertyChanged("type");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 5)]
        public string dictValue
        {
            get
            {
                return this.dictValueField;
            }
            set
            {
                this.dictValueField = value;
                this.RaisePropertyChanged("dictValue");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 6)]
        public object value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
                this.RaisePropertyChanged("value");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 7)]
        public EhdCatalogItems groupValue
        {
            get
            {
                return this.groupValueField;
            }
            set
            {
                this.groupValueField = value;
                this.RaisePropertyChanged("groupValue");
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getCatalogItems", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getCatalogItemsRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int idCatalog;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 1)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int start;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 2)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int end;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 3)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool hideDeleted;

        /*[System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ehd.mos.com/", Order=4)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int idGlobalObject;*/

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 5)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string filters;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 6)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string status;

        /*[System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ehd.mos.com/", Order=7)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int idParentCatalog;*/

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 8)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool fetchGeodata;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 9)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string geoType;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 10)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool getSigned;

        public getCatalogItemsRequest()
        {
        }

        public getCatalogItemsRequest(int idCatalog, int start, int end, bool hideDeleted, int idGlobalObject, string filters, string status, int idParentCatalog, bool fetchGeodata, string geoType, bool getSigned)
        {
            this.idCatalog = idCatalog;
            this.start = start;
            this.end = end;
            this.hideDeleted = hideDeleted;
            //this.idGlobalObject = idGlobalObject;
            this.filters = filters;
            this.status = status;
            //this.idParentCatalog = idParentCatalog;
            this.fetchGeodata = fetchGeodata;
            this.geoType = geoType;
            this.getSigned = getSigned;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getCatalogItemsResponse", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getCatalogItemsResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("ehdCatalogItem", IsNullable = false)]
        public RestChild.EGASIntegration.EGASPromDuo.EhdCatalogItems[] ehdCatalogItemsset;

        public getCatalogItemsResponse()
        {
        }

        public getCatalogItemsResponse(RestChild.EGASIntegration.EGASPromDuo.EhdCatalogItems[] ehdCatalogItemsset)
        {
            this.ehdCatalogItemsset = ehdCatalogItemsset;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getCatalogItemsNew", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getCatalogItemsNewRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int idCatalog;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 1)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int global_id;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 2)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string system_object_id;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 3)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool getSigned;

        public getCatalogItemsNewRequest()
        {
        }

        public getCatalogItemsNewRequest(int idCatalog, int global_id, string system_object_id, bool getSigned)
        {
            this.idCatalog = idCatalog;
            this.global_id = global_id;
            this.system_object_id = system_object_id;
            this.getSigned = getSigned;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getCatalogItemsNewResponse", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getCatalogItemsNewResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        public RestChild.EGASIntegration.EGASPromDuo.EhdCatalogItems ehdCatalogItemsset;

        public getCatalogItemsNewResponse()
        {
        }

        public getCatalogItemsNewResponse(RestChild.EGASIntegration.EGASPromDuo.EhdCatalogItems ehdCatalogItemsset)
        {
            this.ehdCatalogItemsset = ehdCatalogItemsset;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ehd.mos.com/")]
    public partial class EhdCatalogStats : object, System.ComponentModel.INotifyPropertyChanged
    {

        private System.Nullable<int> catalogIdField;

        private bool catalogIdFieldSpecified;

        private string fullNameField;

        private string technicalNameField;

        private System.Nullable<int> cntActiveObjField;

        private bool cntActiveObjFieldSpecified;

        private System.Nullable<int> cntDelObjField;

        private bool cntDelObjFieldSpecified;

        private System.Nullable<int> cntNotSubscribeField;

        private bool cntNotSubscribeFieldSpecified;

        private System.Nullable<int> cntErrorField;

        private bool cntErrorFieldSpecified;

        private System.Nullable<int> cntGeoErrorField;

        private bool cntGeoErrorFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 0)]
        public System.Nullable<int> catalogId
        {
            get
            {
                return this.catalogIdField;
            }
            set
            {
                this.catalogIdField = value;
                this.RaisePropertyChanged("catalogId");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool catalogIdSpecified
        {
            get
            {
                return this.catalogIdFieldSpecified;
            }
            set
            {
                this.catalogIdFieldSpecified = value;
                this.RaisePropertyChanged("catalogIdSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 1)]
        public string fullName
        {
            get
            {
                return this.fullNameField;
            }
            set
            {
                this.fullNameField = value;
                this.RaisePropertyChanged("fullName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 2)]
        public string technicalName
        {
            get
            {
                return this.technicalNameField;
            }
            set
            {
                this.technicalNameField = value;
                this.RaisePropertyChanged("technicalName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 3)]
        public System.Nullable<int> cntActiveObj
        {
            get
            {
                return this.cntActiveObjField;
            }
            set
            {
                this.cntActiveObjField = value;
                this.RaisePropertyChanged("cntActiveObj");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool cntActiveObjSpecified
        {
            get
            {
                return this.cntActiveObjFieldSpecified;
            }
            set
            {
                this.cntActiveObjFieldSpecified = value;
                this.RaisePropertyChanged("cntActiveObjSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 4)]
        public System.Nullable<int> cntDelObj
        {
            get
            {
                return this.cntDelObjField;
            }
            set
            {
                this.cntDelObjField = value;
                this.RaisePropertyChanged("cntDelObj");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool cntDelObjSpecified
        {
            get
            {
                return this.cntDelObjFieldSpecified;
            }
            set
            {
                this.cntDelObjFieldSpecified = value;
                this.RaisePropertyChanged("cntDelObjSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 5)]
        public System.Nullable<int> cntNotSubscribe
        {
            get
            {
                return this.cntNotSubscribeField;
            }
            set
            {
                this.cntNotSubscribeField = value;
                this.RaisePropertyChanged("cntNotSubscribe");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool cntNotSubscribeSpecified
        {
            get
            {
                return this.cntNotSubscribeFieldSpecified;
            }
            set
            {
                this.cntNotSubscribeFieldSpecified = value;
                this.RaisePropertyChanged("cntNotSubscribeSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 6)]
        public System.Nullable<int> cntError
        {
            get
            {
                return this.cntErrorField;
            }
            set
            {
                this.cntErrorField = value;
                this.RaisePropertyChanged("cntError");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool cntErrorSpecified
        {
            get
            {
                return this.cntErrorFieldSpecified;
            }
            set
            {
                this.cntErrorFieldSpecified = value;
                this.RaisePropertyChanged("cntErrorSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 7)]
        public System.Nullable<int> cntGeoError
        {
            get
            {
                return this.cntGeoErrorField;
            }
            set
            {
                this.cntGeoErrorField = value;
                this.RaisePropertyChanged("cntGeoError");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool cntGeoErrorSpecified
        {
            get
            {
                return this.cntGeoErrorFieldSpecified;
            }
            set
            {
                this.cntGeoErrorFieldSpecified = value;
                this.RaisePropertyChanged("cntGeoErrorSpecified");
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getCatalogStats", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getCatalogStatsRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int idCatalog;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 1)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int countSubscribe;

        public getCatalogStatsRequest()
        {
        }

        public getCatalogStatsRequest(int idCatalog, int countSubscribe)
        {
            this.idCatalog = idCatalog;
            this.countSubscribe = countSubscribe;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getCatalogStatsResponse", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getCatalogStatsResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        public RestChild.EGASIntegration.EGASPromDuo.EhdCatalogStats ehdCatalogStats;

        public getCatalogStatsResponse()
        {
        }

        public getCatalogStatsResponse(RestChild.EGASIntegration.EGASPromDuo.EhdCatalogStats ehdCatalogStats)
        {
            this.ehdCatalogStats = ehdCatalogStats;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ehd.mos.com/")]
    public partial class EhdDictionary : object, System.ComponentModel.INotifyPropertyChanged
    {

        private System.Nullable<long> idField;

        private bool idFieldSpecified;

        private string nameField;

        private System.Nullable<int> totalField;

        private bool totalFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 0)]
        public System.Nullable<long> id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                this.RaisePropertyChanged("id");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool idSpecified
        {
            get
            {
                return this.idFieldSpecified;
            }
            set
            {
                this.idFieldSpecified = value;
                this.RaisePropertyChanged("idSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 1)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
                this.RaisePropertyChanged("name");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 2)]
        public System.Nullable<int> total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
                this.RaisePropertyChanged("total");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool totalSpecified
        {
            get
            {
                return this.totalFieldSpecified;
            }
            set
            {
                this.totalFieldSpecified = value;
                this.RaisePropertyChanged("totalSpecified");
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getAllDict", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getAllDictRequest
    {

        public getAllDictRequest()
        {
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getAllDictResponse", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getAllDictResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("ehdDictionary")]
        public RestChild.EGASIntegration.EGASPromDuo.EhdDictionary[] ehdDictionaries;

        public getAllDictResponse()
        {
        }

        public getAllDictResponse(RestChild.EGASIntegration.EGASPromDuo.EhdDictionary[] ehdDictionaries)
        {
            this.ehdDictionaries = ehdDictionaries;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ehd.mos.com/")]
    public partial class EhdDictionaryItem : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string idField;

        private string parent_idField;

        private string nameField;

        private string enNameField;

        private int isDeletedField;

        private bool isDeletedFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 0)]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                this.RaisePropertyChanged("id");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 1)]
        public string parent_id
        {
            get
            {
                return this.parent_idField;
            }
            set
            {
                this.parent_idField = value;
                this.RaisePropertyChanged("parent_id");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 2)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
                this.RaisePropertyChanged("name");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 3)]
        public string enName
        {
            get
            {
                return this.enNameField;
            }
            set
            {
                this.enNameField = value;
                this.RaisePropertyChanged("enName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public int isDeleted
        {
            get
            {
                return this.isDeletedField;
            }
            set
            {
                this.isDeletedField = value;
                this.RaisePropertyChanged("isDeleted");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isDeletedSpecified
        {
            get
            {
                return this.isDeletedFieldSpecified;
            }
            set
            {
                this.isDeletedFieldSpecified = value;
                this.RaisePropertyChanged("isDeletedSpecified");
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getDictItem", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getDictItemRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long dictionaryId;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 1)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int start;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 2)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int end;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 3)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string elementId;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 4)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int version;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 5)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool showDeleted;

        public getDictItemRequest()
        {
        }

        public getDictItemRequest(long dictionaryId, int start, int end, string elementId, int version, bool showDeleted)
        {
            this.dictionaryId = dictionaryId;
            this.start = start;
            this.end = end;
            this.elementId = elementId;
            this.version = version;
            this.showDeleted = showDeleted;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getDictItemResponse", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getDictItemResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("ehdDictionary")]
        public RestChild.EGASIntegration.EGASPromDuo.EhdDictionaryItem[] ehdDictionaryItems;

        public getDictItemResponse()
        {
        }

        public getDictItemResponse(RestChild.EGASIntegration.EGASPromDuo.EhdDictionaryItem[] ehdDictionaryItems)
        {
            this.ehdDictionaryItems = ehdDictionaryItems;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ehd.mos.com/")]
    public partial class ResultUid : object, System.ComponentModel.INotifyPropertyChanged
    {

        private int uidField;

        private bool uidFieldSpecified;

        private string loginField;

        private string nameField;

        private string surnameField;

        private string patronameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public int uid
        {
            get
            {
                return this.uidField;
            }
            set
            {
                this.uidField = value;
                this.RaisePropertyChanged("uid");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool uidSpecified
        {
            get
            {
                return this.uidFieldSpecified;
            }
            set
            {
                this.uidFieldSpecified = value;
                this.RaisePropertyChanged("uidSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 1)]
        public string login
        {
            get
            {
                return this.loginField;
            }
            set
            {
                this.loginField = value;
                this.RaisePropertyChanged("login");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 2)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
                this.RaisePropertyChanged("name");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 3)]
        public string surname
        {
            get
            {
                return this.surnameField;
            }
            set
            {
                this.surnameField = value;
                this.RaisePropertyChanged("surname");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 4)]
        public string patroname
        {
            get
            {
                return this.patronameField;
            }
            set
            {
                this.patronameField = value;
                this.RaisePropertyChanged("patroname");
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getUserBySession", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getUserBySessionRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string session_id;

        public getUserBySessionRequest()
        {
        }

        public getUserBySessionRequest(string session_id)
        {
            this.session_id = session_id;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getUserBySessionResponse", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getUserBySessionResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "getUserBySessionResponse", Namespace = "http://ehd.mos.com/", Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("getUserBySessionResponse")]
        public RestChild.EGASIntegration.EGASPromDuo.ResultUid[] getUserBySessionResponse1;

        public getUserBySessionResponse()
        {
        }

        public getUserBySessionResponse(RestChild.EGASIntegration.EGASPromDuo.ResultUid[] getUserBySessionResponse1)
        {
            this.getUserBySessionResponse1 = getUserBySessionResponse1;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ehd.mos.com/")]
    public partial class EhdDictionaryItemV2 : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string idField;

        private string parent_idField;

        private string nameField;

        private string enNameField;

        private int isDeletedField;

        private bool isDeletedFieldSpecified;

        private DictAttrV2[] dictAttrsV2Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 0)]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                this.RaisePropertyChanged("id");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 1)]
        public string parent_id
        {
            get
            {
                return this.parent_idField;
            }
            set
            {
                this.parent_idField = value;
                this.RaisePropertyChanged("parent_id");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 2)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
                this.RaisePropertyChanged("name");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 3)]
        public string enName
        {
            get
            {
                return this.enNameField;
            }
            set
            {
                this.enNameField = value;
                this.RaisePropertyChanged("enName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public int isDeleted
        {
            get
            {
                return this.isDeletedField;
            }
            set
            {
                this.isDeletedField = value;
                this.RaisePropertyChanged("isDeleted");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isDeletedSpecified
        {
            get
            {
                return this.isDeletedFieldSpecified;
            }
            set
            {
                this.isDeletedFieldSpecified = value;
                this.RaisePropertyChanged("isDeletedSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("dictAttrsV2", IsNullable = true, Order = 5)]
        public DictAttrV2[] dictAttrsV2
        {
            get
            {
                return this.dictAttrsV2Field;
            }
            set
            {
                this.dictAttrsV2Field = value;
                this.RaisePropertyChanged("dictAttrsV2");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ehd.mos.com/")]
    public partial class DictAttrV2 : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string tehNameField;

        private string nameField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 0)]
        public string tehName
        {
            get
            {
                return this.tehNameField;
            }
            set
            {
                this.tehNameField = value;
                this.RaisePropertyChanged("tehName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 1)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
                this.RaisePropertyChanged("name");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 2)]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
                this.RaisePropertyChanged("value");
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getDictItemV2", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getDictItemV2Request
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long dictionaryId;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 1)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int start;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 2)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int end;

        public getDictItemV2Request()
        {
        }

        public getDictItemV2Request(long dictionaryId, int start, int end)
        {
            this.dictionaryId = dictionaryId;
            this.start = start;
            this.end = end;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getDictItemV2Response", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getDictItemV2Response
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("ehdDictionaryItemV2")]
        public RestChild.EGASIntegration.EGASPromDuo.EhdDictionaryItemV2[] ehdDictionaryItemsV2;

        public getDictItemV2Response()
        {
        }

        public getDictItemV2Response(RestChild.EGASIntegration.EGASPromDuo.EhdDictionaryItemV2[] ehdDictionaryItemsV2)
        {
            this.ehdDictionaryItemsV2 = ehdDictionaryItemsV2;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ehd.mos.com/")]
    public partial class ResultInfo : object, System.ComponentModel.INotifyPropertyChanged
    {

        private System.Nullable<int> statusField;

        private bool statusFieldSpecified;

        private string messageField;

        private string detail_messageField;

        private System.Nullable<long> global_idField;

        private bool global_idFieldSpecified;

        private string system_object_idField;

        private string actionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 0)]
        public System.Nullable<int> status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
                this.RaisePropertyChanged("status");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool statusSpecified
        {
            get
            {
                return this.statusFieldSpecified;
            }
            set
            {
                this.statusFieldSpecified = value;
                this.RaisePropertyChanged("statusSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 1)]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 2)]
        public string detail_message
        {
            get
            {
                return this.detail_messageField;
            }
            set
            {
                this.detail_messageField = value;
                this.RaisePropertyChanged("detail_message");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 3)]
        public System.Nullable<long> global_id
        {
            get
            {
                return this.global_idField;
            }
            set
            {
                this.global_idField = value;
                this.RaisePropertyChanged("global_id");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool global_idSpecified
        {
            get
            {
                return this.global_idFieldSpecified;
            }
            set
            {
                this.global_idFieldSpecified = value;
                this.RaisePropertyChanged("global_idSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 4)]
        public string system_object_id
        {
            get
            {
                return this.system_object_idField;
            }
            set
            {
                this.system_object_idField = value;
                this.RaisePropertyChanged("system_object_id");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 5)]
        public string action
        {
            get
            {
                return this.actionField;
            }
            set
            {
                this.actionField = value;
                this.RaisePropertyChanged("action");
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "setDataIn", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class setDataInRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string xml;

        public setDataInRequest()
        {
        }

        public setDataInRequest(string xml)
        {
            this.xml = xml;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "setDataInResponse", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class setDataInResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("setDataResponse")]
        public RestChild.EGASIntegration.EGASPromDuo.ResultInfo[] setDataResponse;

        public setDataInResponse()
        {
        }

        public setDataInResponse(RestChild.EGASIntegration.EGASPromDuo.ResultInfo[] setDataResponse)
        {
            this.setDataResponse = setDataResponse;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ehd.mos.com/")]
    public partial class EhdAttrSpecNew : object, System.ComponentModel.INotifyPropertyChanged
    {

        private long countField;

        private EhdCommonAttributeNew[] ehdCommonAttributeField;

        private EhdException[] ehdExceptionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public long count
        {
            get
            {
                return this.countField;
            }
            set
            {
                this.countField = value;
                this.RaisePropertyChanged("count");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ehdCommonAttribute", IsNullable = true, Order = 1)]
        public EhdCommonAttributeNew[] ehdCommonAttribute
        {
            get
            {
                return this.ehdCommonAttributeField;
            }
            set
            {
                this.ehdCommonAttributeField = value;
                this.RaisePropertyChanged("ehdCommonAttribute");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ehdException", IsNullable = true, Order = 2)]
        public EhdException[] ehdException
        {
            get
            {
                return this.ehdExceptionField;
            }
            set
            {
                this.ehdExceptionField = value;
                this.RaisePropertyChanged("ehdException");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ehd.mos.com/")]
    public partial class EhdCommonAttributeNew : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string idField;

        private string typeIdField;

        private string nameField;

        private string colnameField;

        private string typeField;

        private System.Nullable<bool> isPrimaryKeyField;

        private bool isPrimaryKeyFieldSpecified;

        private System.Nullable<bool> isEditField;

        private bool isEditFieldSpecified;

        private System.Nullable<bool> isReqField;

        private bool isReqFieldSpecified;

        private string fieldMaskField;

        private string tehNameField;

        private string maxLengthField;

        private System.Nullable<int> maxLengthDecimalField;

        private bool maxLengthDecimalFieldSpecified;

        private System.Nullable<int> dictIdField;

        private bool dictIdFieldSpecified;

        private System.Nullable<int> refCatalogField;

        private bool refCatalogFieldSpecified;

        private System.Nullable<int> isDeletedField;

        private bool isDeletedFieldSpecified;

        private System.Nullable<int> isDeletedTmpField;

        private bool isDeletedTmpFieldSpecified;

        private System.Nullable<bool> isMultiField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 0)]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                this.RaisePropertyChanged("id");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 1)]
        public string typeId
        {
            get
            {
                return this.typeIdField;
            }
            set
            {
                this.typeIdField = value;
                this.RaisePropertyChanged("typeId");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 2)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
                this.RaisePropertyChanged("name");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 3)]
        public string colname
        {
            get
            {
                return this.colnameField;
            }
            set
            {
                this.colnameField = value;
                this.RaisePropertyChanged("colname");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 4)]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
                this.RaisePropertyChanged("type");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 5)]
        public System.Nullable<bool> isPrimaryKey
        {
            get
            {
                return this.isPrimaryKeyField;
            }
            set
            {
                this.isPrimaryKeyField = value;
                this.RaisePropertyChanged("isPrimaryKey");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isPrimaryKeySpecified
        {
            get
            {
                return this.isPrimaryKeyFieldSpecified;
            }
            set
            {
                this.isPrimaryKeyFieldSpecified = value;
                this.RaisePropertyChanged("isPrimaryKeySpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 6)]
        public System.Nullable<bool> isEdit
        {
            get
            {
                return this.isEditField;
            }
            set
            {
                this.isEditField = value;
                this.RaisePropertyChanged("isEdit");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isEditSpecified
        {
            get
            {
                return this.isEditFieldSpecified;
            }
            set
            {
                this.isEditFieldSpecified = value;
                this.RaisePropertyChanged("isEditSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 7)]
        public System.Nullable<bool> isReq
        {
            get
            {
                return this.isReqField;
            }
            set
            {
                this.isReqField = value;
                this.RaisePropertyChanged("isReq");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isReqSpecified
        {
            get
            {
                return this.isReqFieldSpecified;
            }
            set
            {
                this.isReqFieldSpecified = value;
                this.RaisePropertyChanged("isReqSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 8)]
        public string fieldMask
        {
            get
            {
                return this.fieldMaskField;
            }
            set
            {
                this.fieldMaskField = value;
                this.RaisePropertyChanged("fieldMask");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 9)]
        public string tehName
        {
            get
            {
                return this.tehNameField;
            }
            set
            {
                this.tehNameField = value;
                this.RaisePropertyChanged("tehName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 10)]
        public string maxLength
        {
            get
            {
                return this.maxLengthField;
            }
            set
            {
                this.maxLengthField = value;
                this.RaisePropertyChanged("maxLength");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 11)]
        public System.Nullable<int> maxLengthDecimal
        {
            get
            {
                return this.maxLengthDecimalField;
            }
            set
            {
                this.maxLengthDecimalField = value;
                this.RaisePropertyChanged("maxLengthDecimal");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool maxLengthDecimalSpecified
        {
            get
            {
                return this.maxLengthDecimalFieldSpecified;
            }
            set
            {
                this.maxLengthDecimalFieldSpecified = value;
                this.RaisePropertyChanged("maxLengthDecimalSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 12)]
        public System.Nullable<int> dictId
        {
            get
            {
                return this.dictIdField;
            }
            set
            {
                this.dictIdField = value;
                this.RaisePropertyChanged("dictId");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool dictIdSpecified
        {
            get
            {
                return this.dictIdFieldSpecified;
            }
            set
            {
                this.dictIdFieldSpecified = value;
                this.RaisePropertyChanged("dictIdSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 13)]
        public System.Nullable<int> refCatalog
        {
            get
            {
                return this.refCatalogField;
            }
            set
            {
                this.refCatalogField = value;
                this.RaisePropertyChanged("refCatalog");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool refCatalogSpecified
        {
            get
            {
                return this.refCatalogFieldSpecified;
            }
            set
            {
                this.refCatalogFieldSpecified = value;
                this.RaisePropertyChanged("refCatalogSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 14)]
        public System.Nullable<int> isDeleted
        {
            get
            {
                return this.isDeletedField;
            }
            set
            {
                this.isDeletedField = value;
                this.RaisePropertyChanged("isDeleted");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isDeletedSpecified
        {
            get
            {
                return this.isDeletedFieldSpecified;
            }
            set
            {
                this.isDeletedFieldSpecified = value;
                this.RaisePropertyChanged("isDeletedSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 15)]
        public System.Nullable<int> isDeletedTmp
        {
            get
            {
                return this.isDeletedTmpField;
            }
            set
            {
                this.isDeletedTmpField = value;
                this.RaisePropertyChanged("isDeletedTmp");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isDeletedTmpSpecified
        {
            get
            {
                return this.isDeletedTmpFieldSpecified;
            }
            set
            {
                this.isDeletedTmpFieldSpecified = value;
                this.RaisePropertyChanged("isDeletedTmpSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 16)]
        public System.Nullable<bool> isMulti
        {
            get
            {
                return this.isMultiField;
            }
            set
            {
                this.isMultiField = value;
                this.RaisePropertyChanged("isMulti");
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getCatalogSpecNew", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getCatalogSpecNewRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int idCatalog;

        public getCatalogSpecNewRequest()
        {
        }

        public getCatalogSpecNewRequest(int idCatalog)
        {
            this.idCatalog = idCatalog;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "getCatalogSpecNewResponse", WrapperNamespace = "http://ehd.mos.com/", IsWrapped = true)]
    public partial class getCatalogSpecNewResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ehd.mos.com/", Order = 0)]
        public RestChild.EGASIntegration.EGASPromDuo.EhdAttrSpecNew ehdAttrSpecNew;

        public getCatalogSpecNewResponse()
        {
        }

        public getCatalogSpecNewResponse(RestChild.EGASIntegration.EGASPromDuo.EhdAttrSpecNew ehdAttrSpecNew)
        {
            this.ehdAttrSpecNew = ehdAttrSpecNew;
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface soapChannel : RestChild.EGASIntegration.EGASPromDuo.soap, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class soapClient : System.ServiceModel.ClientBase<RestChild.EGASIntegration.EGASPromDuo.soap>, RestChild.EGASIntegration.EGASPromDuo.soap
    {

        public soapClient()
        {
        }

        public soapClient(string endpointConfigurationName) :
                base(endpointConfigurationName)
        {
        }

        public soapClient(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public soapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public soapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        RestChild.EGASIntegration.EGASPromDuo.getCatalogListResponse RestChild.EGASIntegration.EGASPromDuo.soap.getCatalogList(RestChild.EGASIntegration.EGASPromDuo.getCatalogListRequest request)
        {
            return base.Channel.getCatalogList(request);
        }

        public RestChild.EGASIntegration.EGASPromDuo.EhdCatalog[] getCatalogList()
        {
            RestChild.EGASIntegration.EGASPromDuo.getCatalogListRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.getCatalogListRequest();
            RestChild.EGASIntegration.EGASPromDuo.getCatalogListResponse retVal = ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getCatalogList(inValue);
            return retVal.ehdCatalogs;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getCatalogListResponse> RestChild.EGASIntegration.EGASPromDuo.soap.getCatalogListAsync(RestChild.EGASIntegration.EGASPromDuo.getCatalogListRequest request)
        {
            return base.Channel.getCatalogListAsync(request);
        }

        public System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getCatalogListResponse> getCatalogListAsync()
        {
            RestChild.EGASIntegration.EGASPromDuo.getCatalogListRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.getCatalogListRequest();
            return ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getCatalogListAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecResponse RestChild.EGASIntegration.EGASPromDuo.soap.getCatalogSpec(RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecRequest request)
        {
            return base.Channel.getCatalogSpec(request);
        }

        public RestChild.EGASIntegration.EGASPromDuo.EhdAttrSpec getCatalogSpec(int idCatalog, int version)
        {
            RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecRequest();
            inValue.idCatalog = idCatalog;
            inValue.version = version;
            RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecResponse retVal = ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getCatalogSpec(inValue);
            return retVal.ehdAttrSpec;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecResponse> RestChild.EGASIntegration.EGASPromDuo.soap.getCatalogSpecAsync(RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecRequest request)
        {
            return base.Channel.getCatalogSpecAsync(request);
        }

        public System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecResponse> getCatalogSpecAsync(int idCatalog, int version)
        {
            RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecRequest();
            inValue.idCatalog = idCatalog;
            inValue.version = version;
            return ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getCatalogSpecAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsResponse RestChild.EGASIntegration.EGASPromDuo.soap.getCatalogItems(RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsRequest request)
        {
            return base.Channel.getCatalogItems(request);
        }

        public RestChild.EGASIntegration.EGASPromDuo.EhdCatalogItems[] getCatalogItems(int idCatalog, int start, int end, bool hideDeleted, int idGlobalObject, string filters, string status, int idParentCatalog, bool fetchGeodata, string geoType, bool getSigned)
        {
            RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsRequest();
            inValue.idCatalog = idCatalog;
            inValue.start = start;
            inValue.end = end;
            inValue.hideDeleted = hideDeleted;
            // inValue.idGlobalObject = idGlobalObject;
            inValue.filters = filters;
            inValue.status = status;
            // inValue.idParentCatalog = idParentCatalog;
            inValue.fetchGeodata = fetchGeodata;
            inValue.geoType = geoType;
            inValue.getSigned = getSigned;
            RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsResponse retVal = ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getCatalogItems(inValue);
            return retVal.ehdCatalogItemsset;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsResponse> RestChild.EGASIntegration.EGASPromDuo.soap.getCatalogItemsAsync(RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsRequest request)
        {
            return base.Channel.getCatalogItemsAsync(request);
        }

        public System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsResponse> getCatalogItemsAsync(int idCatalog, int start, int end, bool hideDeleted, int idGlobalObject, string filters, string status, int idParentCatalog, bool fetchGeodata, string geoType, bool getSigned)
        {
            RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsRequest();
            inValue.idCatalog = idCatalog;
            inValue.start = start;
            inValue.end = end;
            inValue.hideDeleted = hideDeleted;
            // inValue.idGlobalObject = idGlobalObject;
            inValue.filters = filters;
            inValue.status = status;
            //  inValue.idParentCatalog = idParentCatalog;
            inValue.fetchGeodata = fetchGeodata;
            inValue.geoType = geoType;
            inValue.getSigned = getSigned;
            return ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getCatalogItemsAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsNewResponse RestChild.EGASIntegration.EGASPromDuo.soap.getCatalogItemsNew(RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsNewRequest request)
        {
            return base.Channel.getCatalogItemsNew(request);
        }

        public RestChild.EGASIntegration.EGASPromDuo.EhdCatalogItems getCatalogItemsNew(int idCatalog, int global_id, string system_object_id, bool getSigned)
        {
            RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsNewRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsNewRequest();
            inValue.idCatalog = idCatalog;
            inValue.global_id = global_id;
            inValue.system_object_id = system_object_id;
            inValue.getSigned = getSigned;
            RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsNewResponse retVal = ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getCatalogItemsNew(inValue);
            return retVal.ehdCatalogItemsset;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsNewResponse> RestChild.EGASIntegration.EGASPromDuo.soap.getCatalogItemsNewAsync(RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsNewRequest request)
        {
            return base.Channel.getCatalogItemsNewAsync(request);
        }

        public System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsNewResponse> getCatalogItemsNewAsync(int idCatalog, int global_id, string system_object_id, bool getSigned)
        {
            RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsNewRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.getCatalogItemsNewRequest();
            inValue.idCatalog = idCatalog;
            inValue.global_id = global_id;
            inValue.system_object_id = system_object_id;
            inValue.getSigned = getSigned;
            return ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getCatalogItemsNewAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        RestChild.EGASIntegration.EGASPromDuo.getCatalogStatsResponse RestChild.EGASIntegration.EGASPromDuo.soap.getCatalogStats(RestChild.EGASIntegration.EGASPromDuo.getCatalogStatsRequest request)
        {
            return base.Channel.getCatalogStats(request);
        }

        public RestChild.EGASIntegration.EGASPromDuo.EhdCatalogStats getCatalogStats(int idCatalog, int countSubscribe)
        {
            RestChild.EGASIntegration.EGASPromDuo.getCatalogStatsRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.getCatalogStatsRequest();
            inValue.idCatalog = idCatalog;
            inValue.countSubscribe = countSubscribe;
            RestChild.EGASIntegration.EGASPromDuo.getCatalogStatsResponse retVal = ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getCatalogStats(inValue);
            return retVal.ehdCatalogStats;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getCatalogStatsResponse> RestChild.EGASIntegration.EGASPromDuo.soap.getCatalogStatsAsync(RestChild.EGASIntegration.EGASPromDuo.getCatalogStatsRequest request)
        {
            return base.Channel.getCatalogStatsAsync(request);
        }

        public System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getCatalogStatsResponse> getCatalogStatsAsync(int idCatalog, int countSubscribe)
        {
            RestChild.EGASIntegration.EGASPromDuo.getCatalogStatsRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.getCatalogStatsRequest();
            inValue.idCatalog = idCatalog;
            inValue.countSubscribe = countSubscribe;
            return ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getCatalogStatsAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        RestChild.EGASIntegration.EGASPromDuo.getAllDictResponse RestChild.EGASIntegration.EGASPromDuo.soap.getAllDict(RestChild.EGASIntegration.EGASPromDuo.getAllDictRequest request)
        {
            return base.Channel.getAllDict(request);
        }

        public RestChild.EGASIntegration.EGASPromDuo.EhdDictionary[] getAllDict()
        {
            RestChild.EGASIntegration.EGASPromDuo.getAllDictRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.getAllDictRequest();
            RestChild.EGASIntegration.EGASPromDuo.getAllDictResponse retVal = ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getAllDict(inValue);
            return retVal.ehdDictionaries;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getAllDictResponse> RestChild.EGASIntegration.EGASPromDuo.soap.getAllDictAsync(RestChild.EGASIntegration.EGASPromDuo.getAllDictRequest request)
        {
            return base.Channel.getAllDictAsync(request);
        }

        public System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getAllDictResponse> getAllDictAsync()
        {
            RestChild.EGASIntegration.EGASPromDuo.getAllDictRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.getAllDictRequest();
            return ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getAllDictAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        RestChild.EGASIntegration.EGASPromDuo.getDictItemResponse RestChild.EGASIntegration.EGASPromDuo.soap.getDictItem(RestChild.EGASIntegration.EGASPromDuo.getDictItemRequest request)
        {
            return base.Channel.getDictItem(request);
        }

        public RestChild.EGASIntegration.EGASPromDuo.EhdDictionaryItem[] getDictItem(long dictionaryId, int start, int end, string elementId, int version, bool showDeleted)
        {
            RestChild.EGASIntegration.EGASPromDuo.getDictItemRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.getDictItemRequest();
            inValue.dictionaryId = dictionaryId;
            inValue.start = start;
            inValue.end = end;
            inValue.elementId = elementId;
            inValue.version = version;
            inValue.showDeleted = showDeleted;
            RestChild.EGASIntegration.EGASPromDuo.getDictItemResponse retVal = ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getDictItem(inValue);
            return retVal.ehdDictionaryItems;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getDictItemResponse> RestChild.EGASIntegration.EGASPromDuo.soap.getDictItemAsync(RestChild.EGASIntegration.EGASPromDuo.getDictItemRequest request)
        {
            return base.Channel.getDictItemAsync(request);
        }

        public System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getDictItemResponse> getDictItemAsync(long dictionaryId, int start, int end, string elementId, int version, bool showDeleted)
        {
            RestChild.EGASIntegration.EGASPromDuo.getDictItemRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.getDictItemRequest();
            inValue.dictionaryId = dictionaryId;
            inValue.start = start;
            inValue.end = end;
            inValue.elementId = elementId;
            inValue.version = version;
            inValue.showDeleted = showDeleted;
            return ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getDictItemAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        RestChild.EGASIntegration.EGASPromDuo.getUserBySessionResponse RestChild.EGASIntegration.EGASPromDuo.soap.getUserBySession(RestChild.EGASIntegration.EGASPromDuo.getUserBySessionRequest request)
        {
            return base.Channel.getUserBySession(request);
        }

        public RestChild.EGASIntegration.EGASPromDuo.ResultUid[] getUserBySession(string session_id)
        {
            RestChild.EGASIntegration.EGASPromDuo.getUserBySessionRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.getUserBySessionRequest();
            inValue.session_id = session_id;
            RestChild.EGASIntegration.EGASPromDuo.getUserBySessionResponse retVal = ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getUserBySession(inValue);
            return retVal.getUserBySessionResponse1;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getUserBySessionResponse> RestChild.EGASIntegration.EGASPromDuo.soap.getUserBySessionAsync(RestChild.EGASIntegration.EGASPromDuo.getUserBySessionRequest request)
        {
            return base.Channel.getUserBySessionAsync(request);
        }

        public System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getUserBySessionResponse> getUserBySessionAsync(string session_id)
        {
            RestChild.EGASIntegration.EGASPromDuo.getUserBySessionRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.getUserBySessionRequest();
            inValue.session_id = session_id;
            return ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getUserBySessionAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        RestChild.EGASIntegration.EGASPromDuo.getDictItemV2Response RestChild.EGASIntegration.EGASPromDuo.soap.getDictItemV2(RestChild.EGASIntegration.EGASPromDuo.getDictItemV2Request request)
        {
            return base.Channel.getDictItemV2(request);
        }

        public RestChild.EGASIntegration.EGASPromDuo.EhdDictionaryItemV2[] getDictItemV2(long dictionaryId, int start, int end)
        {
            RestChild.EGASIntegration.EGASPromDuo.getDictItemV2Request inValue = new RestChild.EGASIntegration.EGASPromDuo.getDictItemV2Request();
            inValue.dictionaryId = dictionaryId;
            inValue.start = start;
            inValue.end = end;
            RestChild.EGASIntegration.EGASPromDuo.getDictItemV2Response retVal = ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getDictItemV2(inValue);
            return retVal.ehdDictionaryItemsV2;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getDictItemV2Response> RestChild.EGASIntegration.EGASPromDuo.soap.getDictItemV2Async(RestChild.EGASIntegration.EGASPromDuo.getDictItemV2Request request)
        {
            return base.Channel.getDictItemV2Async(request);
        }

        public System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getDictItemV2Response> getDictItemV2Async(long dictionaryId, int start, int end)
        {
            RestChild.EGASIntegration.EGASPromDuo.getDictItemV2Request inValue = new RestChild.EGASIntegration.EGASPromDuo.getDictItemV2Request();
            inValue.dictionaryId = dictionaryId;
            inValue.start = start;
            inValue.end = end;
            return ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getDictItemV2Async(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        RestChild.EGASIntegration.EGASPromDuo.setDataInResponse RestChild.EGASIntegration.EGASPromDuo.soap.setDataIn(RestChild.EGASIntegration.EGASPromDuo.setDataInRequest request)
        {
            return base.Channel.setDataIn(request);
        }

        public RestChild.EGASIntegration.EGASPromDuo.ResultInfo[] setDataIn(string xml)
        {
            RestChild.EGASIntegration.EGASPromDuo.setDataInRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.setDataInRequest();
            inValue.xml = xml;
            RestChild.EGASIntegration.EGASPromDuo.setDataInResponse retVal = ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).setDataIn(inValue);
            return retVal.setDataResponse;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.setDataInResponse> RestChild.EGASIntegration.EGASPromDuo.soap.setDataInAsync(RestChild.EGASIntegration.EGASPromDuo.setDataInRequest request)
        {
            return base.Channel.setDataInAsync(request);
        }

        public System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.setDataInResponse> setDataInAsync(string xml)
        {
            RestChild.EGASIntegration.EGASPromDuo.setDataInRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.setDataInRequest();
            inValue.xml = xml;
            return ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).setDataInAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecNewResponse RestChild.EGASIntegration.EGASPromDuo.soap.getCatalogSpecNew(RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecNewRequest request)
        {
            return base.Channel.getCatalogSpecNew(request);
        }

        public RestChild.EGASIntegration.EGASPromDuo.EhdAttrSpecNew getCatalogSpecNew(int idCatalog)
        {
            RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecNewRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecNewRequest();
            inValue.idCatalog = idCatalog;
            RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecNewResponse retVal = ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getCatalogSpecNew(inValue);
            return retVal.ehdAttrSpecNew;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecNewResponse> RestChild.EGASIntegration.EGASPromDuo.soap.getCatalogSpecNewAsync(RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecNewRequest request)
        {
            return base.Channel.getCatalogSpecNewAsync(request);
        }

        public System.Threading.Tasks.Task<RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecNewResponse> getCatalogSpecNewAsync(int idCatalog)
        {
            RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecNewRequest inValue = new RestChild.EGASIntegration.EGASPromDuo.getCatalogSpecNewRequest();
            inValue.idCatalog = idCatalog;
            return ((RestChild.EGASIntegration.EGASPromDuo.soap)(this)).getCatalogSpecNewAsync(inValue);
        }
    }
}
