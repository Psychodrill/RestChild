﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.7.3081.0.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", IsNullable=false)]
public partial class CoordinateSendTaskStatusesMessage {
    
    private CoordinateTaskStatusData coordinateTaskStatusDataMessageField;
    
    /// <remarks/>
    public CoordinateTaskStatusData CoordinateTaskStatusDataMessage {
        get {
            return this.coordinateTaskStatusDataMessageField;
        }
        set {
            this.coordinateTaskStatusDataMessageField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
public partial class CoordinateTaskStatusData {
    
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

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
public partial class StatusType {
    
    private int statusCodeField;
    
    private System.DateTime statusDateField;
    
    /// <remarks/>
    public int StatusCode {
        get {
            return this.statusCodeField;
        }
        set {
            this.statusCodeField = value;
        }
    }
    
    /// <remarks/>
    public System.DateTime StatusDate {
        get {
            return this.statusDateField;
        }
        set {
            this.statusDateField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
public partial class TaskDataType {
    
    private string documentTypeCodeField;
    
    private System.Xml.XmlElement parameterField;
    
    private bool includeXmlViewField;
    
    private bool includeBinaryViewField;
    
    /// <remarks/>
    public string DocumentTypeCode {
        get {
            return this.documentTypeCodeField;
        }
        set {
            this.documentTypeCodeField = value;
        }
    }
    
    /// <remarks/>
    public System.Xml.XmlElement Parameter {
        get {
            return this.parameterField;
        }
        set {
            this.parameterField = value;
        }
    }
    
    /// <remarks/>
    public bool IncludeXmlView {
        get {
            return this.includeXmlViewField;
        }
        set {
            this.includeXmlViewField = value;
        }
    }
    
    /// <remarks/>
    public bool IncludeBinaryView {
        get {
            return this.includeBinaryViewField;
        }
        set {
            this.includeBinaryViewField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
public partial class Department {
    
    private string nameField;
    
    private string codeField;
    
    private string innField;
    
    private string ogrnField;
    
    private System.DateTime regDateField;
    
    private string systemCodeField;
    
    /// <remarks/>
    public string Name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    public string Code {
        get {
            return this.codeField;
        }
        set {
            this.codeField = value;
        }
    }
    
    /// <remarks/>
    public string Inn {
        get {
            return this.innField;
        }
        set {
            this.innField = value;
        }
    }
    
    /// <remarks/>
    public string Ogrn {
        get {
            return this.ogrnField;
        }
        set {
            this.ogrnField = value;
        }
    }
    
    /// <remarks/>
    public System.DateTime RegDate {
        get {
            return this.regDateField;
        }
        set {
            this.regDateField = value;
        }
    }
    
    /// <remarks/>
    public string SystemCode {
        get {
            return this.systemCodeField;
        }
        set {
            this.systemCodeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
public partial class Person {
    
    private string lastNameField;
    
    private string firstNameField;
    
    private string middleNameField;
    
    private string jobTitleField;
    
    private string phoneField;
    
    private string emailField;
    
    /// <remarks/>
    public string LastName {
        get {
            return this.lastNameField;
        }
        set {
            this.lastNameField = value;
        }
    }
    
    /// <remarks/>
    public string FirstName {
        get {
            return this.firstNameField;
        }
        set {
            this.firstNameField = value;
        }
    }
    
    /// <remarks/>
    public string MiddleName {
        get {
            return this.middleNameField;
        }
        set {
            this.middleNameField = value;
        }
    }
    
    /// <remarks/>
    public string JobTitle {
        get {
            return this.jobTitleField;
        }
        set {
            this.jobTitleField = value;
        }
    }
    
    /// <remarks/>
    public string Phone {
        get {
            return this.phoneField;
        }
        set {
            this.phoneField = value;
        }
    }
    
    /// <remarks/>
    public string Email {
        get {
            return this.emailField;
        }
        set {
            this.emailField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
public partial class TaskType {
    
    private string messageIdField;
    
    private string taskIdField;
    
    private string taskNumberField;
    
    private System.DateTime taskDateField;
    
    private Person responsibleField;
    
    private Department departmentField;
    
    private string[] itemsField;
    
    private ItemsChoiceType[] itemsElementNameField;
    
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
    public string TaskNumber {
        get {
            return this.taskNumberField;
        }
        set {
            this.taskNumberField = value;
        }
    }
    
    /// <remarks/>
    public System.DateTime TaskDate {
        get {
            return this.taskDateField;
        }
        set {
            this.taskDateField = value;
        }
    }
    
    /// <remarks/>
    public Person Responsible {
        get {
            return this.responsibleField;
        }
        set {
            this.responsibleField = value;
        }
    }
    
    /// <remarks/>
    public Department Department {
        get {
            return this.departmentField;
        }
        set {
            this.departmentField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("FunctionTypeCode", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("ServiceNumber", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("ServiceTypeCode", typeof(string))]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
    public string[] Items {
        get {
            return this.itemsField;
        }
        set {
            this.itemsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public ItemsChoiceType[] ItemsElementName {
        get {
            return this.itemsElementNameField;
        }
        set {
            this.itemsElementNameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", IncludeInSchema=false)]
public enum ItemsChoiceType {
    
    /// <remarks/>
    FunctionTypeCode,
    
    /// <remarks/>
    ServiceNumber,
    
    /// <remarks/>
    ServiceTypeCode,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
public partial class CoordinateTaskData {
    
    private TaskType taskField;
    
    private TaskDataType dataField;
    
    private System.Xml.XmlAttribute[] anyAttrField;
    
    /// <remarks/>
    public TaskType Task {
        get {
            return this.taskField;
        }
        set {
            this.taskField = value;
        }
    }
    
    /// <remarks/>
    public TaskDataType Data {
        get {
            return this.dataField;
        }
        set {
            this.dataField = value;
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

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
public partial class CoordinateFile {
    
    private string idField;
    
    private string fileIdInStoreField;
    
    private string storeNameField;
    
    private string fileNameField;
    
    private byte[] cmsSignatureField;
    
    private byte[] fileHashField;
    
    /// <remarks/>
    public string Id {
        get {
            return this.idField;
        }
        set {
            this.idField = value;
        }
    }
    
    /// <remarks/>
    public string FileIdInStore {
        get {
            return this.fileIdInStoreField;
        }
        set {
            this.fileIdInStoreField = value;
        }
    }
    
    /// <remarks/>
    public string StoreName {
        get {
            return this.storeNameField;
        }
        set {
            this.storeNameField = value;
        }
    }
    
    /// <remarks/>
    public string FileName {
        get {
            return this.fileNameField;
        }
        set {
            this.fileNameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
    public byte[] CmsSignature {
        get {
            return this.cmsSignatureField;
        }
        set {
            this.cmsSignatureField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
    public byte[] FileHash {
        get {
            return this.fileHashField;
        }
        set {
            this.fileHashField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
public partial class ArrayOfCoordinateFile {
    
    private CoordinateFile[] coordinateFileField;
    
    private System.Xml.XmlAttribute[] anyAttrField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("CoordinateFile")]
    public CoordinateFile[] CoordinateFile {
        get {
            return this.coordinateFileField;
        }
        set {
            this.coordinateFileField = value;
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

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
public partial class TaskResult {
    
    private TaskResultType resultTypeField;
    
    private int resultCodeField;
    
    private System.Xml.XmlElement xmlViewField;
    
    private ArrayOfCoordinateFile binaryViewField;
    
    /// <remarks/>
    public TaskResultType ResultType {
        get {
            return this.resultTypeField;
        }
        set {
            this.resultTypeField = value;
        }
    }
    
    /// <remarks/>
    public int ResultCode {
        get {
            return this.resultCodeField;
        }
        set {
            this.resultCodeField = value;
        }
    }
    
    /// <remarks/>
    public System.Xml.XmlElement XmlView {
        get {
            return this.xmlViewField;
        }
        set {
            this.xmlViewField = value;
        }
    }
    
    /// <remarks/>
    public ArrayOfCoordinateFile BinaryView {
        get {
            return this.binaryViewField;
        }
        set {
            this.binaryViewField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
public enum TaskResultType {
    
    /// <remarks/>
    PositiveAnswer,
    
    /// <remarks/>
    NegativeAnswer,
    
    /// <remarks/>
    Error,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="http://asguf.mos.ru/rkis_gu/coordinate/v6_1/", IsNullable=false)]
public partial class CoordinateTaskMessage {
    
    private CoordinateTaskData coordinateTaskDataMessageField;
    
    /// <remarks/>
    public CoordinateTaskData CoordinateTaskDataMessage {
        get {
            return this.coordinateTaskDataMessageField;
        }
        set {
            this.coordinateTaskDataMessageField = value;
        }
    }
}