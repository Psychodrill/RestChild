﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.0.30319.33440.
// 
namespace ESZ.UI.Services.NSIDelta {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rstyle.com/nsi/delta")]
		[System.Xml.Serialization.XmlRootAttribute("containerDelta", Namespace = "http://rstyle.com/nsi/delta", IsNullable = false)]
    public partial class ContainerDelta {
        
        private Item[] itemField;
        
        private string nameField;
        
        private ContainerType typeField;
        
        private bool typeFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public Item[] item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ContainerType type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool typeSpecified {
            get {
                return this.typeFieldSpecified;
            }
            set {
                this.typeFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rstyle.com/nsi/delta")]
    public partial class Item {
        
        private Attribute[] attributeField;
        
        private string primaryKeyField;
        
        private Action actionField;
        
        private bool actionFieldSpecified;
        
        private long notificationIdField;
        
        private bool notificationIdFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("attribute", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public Attribute[] attribute {
            get {
                return this.attributeField;
            }
            set {
                this.attributeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string primaryKey {
            get {
                return this.primaryKeyField;
            }
            set {
                this.primaryKeyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public Action action {
            get {
                return this.actionField;
            }
            set {
                this.actionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool actionSpecified {
            get {
                return this.actionFieldSpecified;
            }
            set {
                this.actionFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public long notificationId {
            get {
                return this.notificationIdField;
            }
            set {
                this.notificationIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool notificationIdSpecified {
            get {
                return this.notificationIdFieldSpecified;
            }
            set {
                this.notificationIdFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rstyle.com/nsi/delta")]
    public partial class Attribute {
        
        private AttributeValue[] valueField;
        
        private GroupValue[] groupValueField;
        
        private string nameField;
        
        private string typeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("value", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AttributeValue[] value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("groupValue", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GroupValue[] groupValue {
            get {
                return this.groupValueField;
            }
            set {
                this.groupValueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://rstyle.com/nsi/delta")]
    public partial class AttributeValue {
        
        private int occurrenceField;
        
        private bool occurrenceFieldSpecified;
        
        private Action actionField;
        
        private bool actionFieldSpecified;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int occurrence {
            get {
                return this.occurrenceField;
            }
            set {
                this.occurrenceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool occurrenceSpecified {
            get {
                return this.occurrenceFieldSpecified;
            }
            set {
                this.occurrenceFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public Action action {
            get {
                return this.actionField;
            }
            set {
                this.actionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool actionSpecified {
            get {
                return this.actionFieldSpecified;
            }
            set {
                this.actionFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rstyle.com/nsi/delta")]
    public enum Action {
        
        /// <remarks/>
        ADDED,
        
        /// <remarks/>
        MODIFIED,
        
        /// <remarks/>
        DELETED,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rstyle.com/nsi/delta")]
    public partial class GroupValue {
        
        private Attribute[] attributeField;
        
        private int occurrenceField;
        
        private bool occurrenceFieldSpecified;
        
        private Action actionField;
        
        private bool actionFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("attribute", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public Attribute[] attribute {
            get {
                return this.attributeField;
            }
            set {
                this.attributeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int occurrence {
            get {
                return this.occurrenceField;
            }
            set {
                this.occurrenceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool occurrenceSpecified {
            get {
                return this.occurrenceFieldSpecified;
            }
            set {
                this.occurrenceFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public Action action {
            get {
                return this.actionField;
            }
            set {
                this.actionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool actionSpecified {
            get {
                return this.actionFieldSpecified;
            }
            set {
                this.actionFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rstyle.com/nsi/delta")]
    public enum ContainerType {
        
        /// <remarks/>
        CATALOG,
        
        /// <remarks/>
        HIERARCHY,
        
        /// <remarks/>
        LOOKUP_TABLE,
    }
}
