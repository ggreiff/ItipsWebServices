﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by wsdl, Version=4.0.30319.18020.
// 
namespace wsPortfoliosVersion {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="psPortfoliosVersionSoap", Namespace="http://prosight.com/wsdl/5.0/psPortfoliosVersion/")]
    public partial class psPortfoliosVersion : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback DebugOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetVersionInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetAllVersionsOperationCompleted;
        
        /// <remarks/>
        public psPortfoliosVersion() {
            this.Url = "http://localhost/ProSightWs/psPortfoliosVersion.asmx";
        }
        
        /// <remarks/>
        public event DebugCompletedEventHandler DebugCompleted;
        
        /// <remarks/>
        public event GetVersionInfoCompletedEventHandler GetVersionInfoCompleted;
        
        /// <remarks/>
        public event GetAllVersionsCompletedEventHandler GetAllVersionsCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://prosight.com/wsdl/Portfolios/4.0/psPortfoliosVersion/Debug", RequestNamespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosVersion", ResponseNamespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosVersion", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public psRETURN_VALUES Debug() {
            object[] results = this.Invoke("Debug", new object[0]);
            return ((psRETURN_VALUES)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDebug(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Debug", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public psRETURN_VALUES EndDebug(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((psRETURN_VALUES)(results[0]));
        }
        
        /// <remarks/>
        public void DebugAsync() {
            this.DebugAsync(null);
        }
        
        /// <remarks/>
        public void DebugAsync(object userState) {
            if ((this.DebugOperationCompleted == null)) {
                this.DebugOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDebugOperationCompleted);
            }
            this.InvokeAsync("Debug", new object[0], this.DebugOperationCompleted, userState);
        }
        
        private void OnDebugOperationCompleted(object arg) {
            if ((this.DebugCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DebugCompleted(this, new DebugCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://prosight.com/wsdl/Portfolios/4.0/psPortfoliosVersion/GetVersionInfo", RequestNamespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosVersion", ResponseNamespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosVersion", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public psPortfoliosVersionInfo GetVersionInfo(string sName) {
            object[] results = this.Invoke("GetVersionInfo", new object[] {
                        sName});
            return ((psPortfoliosVersionInfo)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetVersionInfo(string sName, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetVersionInfo", new object[] {
                        sName}, callback, asyncState);
        }
        
        /// <remarks/>
        public psPortfoliosVersionInfo EndGetVersionInfo(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((psPortfoliosVersionInfo)(results[0]));
        }
        
        /// <remarks/>
        public void GetVersionInfoAsync(string sName) {
            this.GetVersionInfoAsync(sName, null);
        }
        
        /// <remarks/>
        public void GetVersionInfoAsync(string sName, object userState) {
            if ((this.GetVersionInfoOperationCompleted == null)) {
                this.GetVersionInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetVersionInfoOperationCompleted);
            }
            this.InvokeAsync("GetVersionInfo", new object[] {
                        sName}, this.GetVersionInfoOperationCompleted, userState);
        }
        
        private void OnGetVersionInfoOperationCompleted(object arg) {
            if ((this.GetVersionInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetVersionInfoCompleted(this, new GetVersionInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://prosight.com/wsdl/Portfolios/4.0/psPortfoliosVersion/GetAllVersions", RequestNamespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosVersion", ResponseNamespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosVersion", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public psPortfoliosVersionInfo[] GetAllVersions() {
            object[] results = this.Invoke("GetAllVersions", new object[0]);
            return ((psPortfoliosVersionInfo[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetAllVersions(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetAllVersions", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public psPortfoliosVersionInfo[] EndGetAllVersions(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((psPortfoliosVersionInfo[])(results[0]));
        }
        
        /// <remarks/>
        public void GetAllVersionsAsync() {
            this.GetAllVersionsAsync(null);
        }
        
        /// <remarks/>
        public void GetAllVersionsAsync(object userState) {
            if ((this.GetAllVersionsOperationCompleted == null)) {
                this.GetAllVersionsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAllVersionsOperationCompleted);
            }
            this.InvokeAsync("GetAllVersions", new object[0], this.GetAllVersionsOperationCompleted, userState);
        }
        
        private void OnGetAllVersionsOperationCompleted(object arg) {
            if ((this.GetAllVersionsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAllVersionsCompleted(this, new GetAllVersionsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosVersion")]
    public enum psRETURN_VALUES {
        
        /// <remarks/>
        ERR_OK,
        
        /// <remarks/>
        ERR_INTERNAL,
        
        /// <remarks/>
        ERR_COMMON_ID_ALREADY_EXISTS,
        
        /// <remarks/>
        ERR_COMMON_ID_UNKNOWN,
        
        /// <remarks/>
        ERR_MULTIPLE_COMMON_ID,
        
        /// <remarks/>
        ERR_NOT_ITEM,
        
        /// <remarks/>
        ERR_NO_NAME_GIVEN,
        
        /// <remarks/>
        ERR_PORTFOLIO_UNKNOWN,
        
        /// <remarks/>
        ERR_PORTFOLIO_NOT_QBP,
        
        /// <remarks/>
        ERR_NAME_ALREADY_EXISTS,
        
        /// <remarks/>
        ERR_NOT_PORTFOLIO,
        
        /// <remarks/>
        ERR_PORTFOLIO_TYPE_MISMATCH,
        
        /// <remarks/>
        ERR_PHASE_NOT_FOUND,
        
        /// <remarks/>
        ERR_NO_SUCH_PHASE_FOR_ITEM,
        
        /// <remarks/>
        ERR_NO_COMMON_ID,
        
        /// <remarks/>
        ERR_ILLEGAL_STATUS,
        
        /// <remarks/>
        ERR_ILLEGAL_PORTFOLIO_TYPE,
        
        /// <remarks/>
        ERR_LIFE_CYCLE_NOT_FOUND,
        
        /// <remarks/>
        ERR_ILLEGAL_GUID,
        
        /// <remarks/>
        ERR_NO_PHASES_FOR_LIFE_CYCLE,
        
        /// <remarks/>
        ERR_ILLEGAL_DATE,
        
        /// <remarks/>
        ERR_ILLEGAL_DESCRIPTION,
        
        /// <remarks/>
        ERR_ITEM_DOES_NOT_EXIST,
        
        /// <remarks/>
        ERR_NO_VALUE_LIST_NAME,
        
        /// <remarks/>
        ERR_VALUE_LIST_DOES_NOT_EXIST,
        
        /// <remarks/>
        ERR_CANNOT_UPDATE_SYSTEM_VALUE_LIST,
        
        /// <remarks/>
        ERR_DUPLICATE_TEXT_IN_VALUE_LIST,
        
        /// <remarks/>
        ERR_DUPLICATE_ID_IN_VALUE_LIST,
        
        /// <remarks/>
        ERR_VALUE_DOES_NOT_EXIST,
        
        /// <remarks/>
        ERR_VALUE_ID_NOT_INTEGER,
        
        /// <remarks/>
        ERR_ILLEGAL_NAME,
        
        /// <remarks/>
        ERR_CATEGORY_NOT_SUPPORTED,
        
        /// <remarks/>
        ERR_CATEGORY_UNKNOWN,
        
        /// <remarks/>
        ERR_ILLEGAL_OPERATOR,
        
        /// <remarks/>
        ERR_VALUE_NOT_INTEGER,
        
        /// <remarks/>
        ERR_VALUE_NOT_NUMERIC,
        
        /// <remarks/>
        ERR_PORTFOLIO_NOT_IN_SCOPE,
        
        /// <remarks/>
        ERR_ILLEGAL_INDICATOR,
        
        /// <remarks/>
        ERR_BAD_PARAMETER,
        
        /// <remarks/>
        ERR_ILLEGAL_DEPENDENCY,
        
        /// <remarks/>
        ERR_ADD_NEW_NEVER_CALLED,
        
        /// <remarks/>
        ERR_SECURITY_NOT_SET,
        
        /// <remarks/>
        ERR_SECURITY_VIOLATION,
        
        /// <remarks/>
        ERR_LIFE_CYCLE_IN_PORTFOLIO,
        
        /// <remarks/>
        ERR_PHASE_IN_LIFE_CYCLE,
        
        /// <remarks/>
        ERR_NO_DELETE_PERMISSION,
        
        /// <remarks/>
        ERR_NO_EDIT_PERMISSION_ON_PARENT,
        
        /// <remarks/>
        ERR_NO_FURTHER_DETAILS_AVAILABLE,
        
        /// <remarks/>
        ERR_DUPLICATE_PHASE_IN_LIFE_CYCLE,
        
        /// <remarks/>
        ERR_PHASE_ACTUAL_START_DATE_LATER_THAN_END_DATE,
        
        /// <remarks/>
        ERR_PHASE_PLANED_START_DATE_LATER_THAN_END_DATE,
        
        /// <remarks/>
        ERR_MORE_THAN_ONE_CURRENT_PHASE_FOR_ITEM,
        
        /// <remarks/>
        ERR_MENU_XML_DOES_NOT_CONFORM_TO_XSD,
        
        /// <remarks/>
        ERR_SETTINGS_DO_NOT_EXIST,
        
        /// <remarks/>
        ERR_SETTINGS_ALREADY_EXIST,
        
        /// <remarks/>
        ERR_OBJECT_IN_USE,
        
        /// <remarks/>
        ERR_EXCEEDED_AUTHORIZED_NUMBER_OF_USERS,
        
        /// <remarks/>
        ERR_PHASE_FRCST_START_DATE_LATER_THAN_END_DATE,
        
        /// <remarks/>
        ERR_SPECIFIC_EMAIL_ALERT_ALREADY_EXISTS,
        
        /// <remarks/>
        ERR_XML_EXPORT_TEMPLATE_ERROR,
        
        /// <remarks/>
        ERR_XML_EXPORT_ERROR,
        
        /// <remarks/>
        ERR_XML_EXPORT_VALIDATION_ERROR,
        
        /// <remarks/>
        ERR_VERSION_NAME_DOES_NOT_EXIST,
        
        /// <remarks/>
        ERR_ATTACHMENT_UNKNOWN,
        
        /// <remarks/>
        NOT_SPECIFIED,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://prosight.com/type/Portfolios/5.0/psPortfoliosVersion")]
    public partial class psPortfoliosVersionInfo {
        
        private string nameField;
        
        private string dateField;
        
        private bool isDateRelativeField;
        
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
        public string Date {
            get {
                return this.dateField;
            }
            set {
                this.dateField = value;
            }
        }
        
        /// <remarks/>
        public bool IsDateRelative {
            get {
                return this.isDateRelativeField;
            }
            set {
                this.isDateRelativeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    public delegate void DebugCompletedEventHandler(object sender, DebugCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DebugCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DebugCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public psRETURN_VALUES Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((psRETURN_VALUES)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    public delegate void GetVersionInfoCompletedEventHandler(object sender, GetVersionInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetVersionInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetVersionInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public psPortfoliosVersionInfo Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((psPortfoliosVersionInfo)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    public delegate void GetAllVersionsCompletedEventHandler(object sender, GetAllVersionsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.18020")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAllVersionsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAllVersionsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public psPortfoliosVersionInfo[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((psPortfoliosVersionInfo[])(this.results[0]));
            }
        }
    }
}