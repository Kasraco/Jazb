﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace PaymentProject.BankSaman {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="PaymentIFBindingSoap", Namespace="urn:Foo")]
    public partial class PaymentIFBinding : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback RequestTokenOperationCompleted;
        
        private System.Threading.SendOrPostCallback RequestMultiSettleTypeTokenOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public PaymentIFBinding() {
            this.Url = global::PaymentProject.Properties.Settings.Default.PaymentProject_BankSaman_PaymentIFBinding;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event RequestTokenCompletedEventHandler RequestTokenCompleted;
        
        /// <remarks/>
        public event RequestMultiSettleTypeTokenCompletedEventHandler RequestMultiSettleTypeTokenCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("RequestToken", RequestNamespace="urn:Foo", ResponseNamespace="urn:Foo")]
        [return: System.Xml.Serialization.SoapElementAttribute("result")]
        public string RequestToken(string TermID, string ResNum, long TotalAmount, long SegAmount1, long SegAmount2, long SegAmount3, long SegAmount4, long SegAmount5, long SegAmount6, string AdditionalData1, string AdditionalData2, long Wage) {
            object[] results = this.Invoke("RequestToken", new object[] {
                        TermID,
                        ResNum,
                        TotalAmount,
                        SegAmount1,
                        SegAmount2,
                        SegAmount3,
                        SegAmount4,
                        SegAmount5,
                        SegAmount6,
                        AdditionalData1,
                        AdditionalData2,
                        Wage});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void RequestTokenAsync(string TermID, string ResNum, long TotalAmount, long SegAmount1, long SegAmount2, long SegAmount3, long SegAmount4, long SegAmount5, long SegAmount6, string AdditionalData1, string AdditionalData2, long Wage) {
            this.RequestTokenAsync(TermID, ResNum, TotalAmount, SegAmount1, SegAmount2, SegAmount3, SegAmount4, SegAmount5, SegAmount6, AdditionalData1, AdditionalData2, Wage, null);
        }
        
        /// <remarks/>
        public void RequestTokenAsync(string TermID, string ResNum, long TotalAmount, long SegAmount1, long SegAmount2, long SegAmount3, long SegAmount4, long SegAmount5, long SegAmount6, string AdditionalData1, string AdditionalData2, long Wage, object userState) {
            if ((this.RequestTokenOperationCompleted == null)) {
                this.RequestTokenOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRequestTokenOperationCompleted);
            }
            this.InvokeAsync("RequestToken", new object[] {
                        TermID,
                        ResNum,
                        TotalAmount,
                        SegAmount1,
                        SegAmount2,
                        SegAmount3,
                        SegAmount4,
                        SegAmount5,
                        SegAmount6,
                        AdditionalData1,
                        AdditionalData2,
                        Wage}, this.RequestTokenOperationCompleted, userState);
        }
        
        private void OnRequestTokenOperationCompleted(object arg) {
            if ((this.RequestTokenCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RequestTokenCompleted(this, new RequestTokenCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("RequestMultiSettleTypeToken", RequestNamespace="urn:Foo", ResponseNamespace="urn:Foo")]
        [return: System.Xml.Serialization.SoapElementAttribute("result")]
        public string RequestMultiSettleTypeToken(string TermID, string ResNum, string Amounts, long TotalAmount, long SegAmount1, long SegAmount2, long SegAmount3, long SegAmount4, long SegAmount5, long SegAmount6, string AdditionalData1, string AdditionalData2, long Wage) {
            object[] results = this.Invoke("RequestMultiSettleTypeToken", new object[] {
                        TermID,
                        ResNum,
                        Amounts,
                        TotalAmount,
                        SegAmount1,
                        SegAmount2,
                        SegAmount3,
                        SegAmount4,
                        SegAmount5,
                        SegAmount6,
                        AdditionalData1,
                        AdditionalData2,
                        Wage});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void RequestMultiSettleTypeTokenAsync(string TermID, string ResNum, string Amounts, long TotalAmount, long SegAmount1, long SegAmount2, long SegAmount3, long SegAmount4, long SegAmount5, long SegAmount6, string AdditionalData1, string AdditionalData2, long Wage) {
            this.RequestMultiSettleTypeTokenAsync(TermID, ResNum, Amounts, TotalAmount, SegAmount1, SegAmount2, SegAmount3, SegAmount4, SegAmount5, SegAmount6, AdditionalData1, AdditionalData2, Wage, null);
        }
        
        /// <remarks/>
        public void RequestMultiSettleTypeTokenAsync(string TermID, string ResNum, string Amounts, long TotalAmount, long SegAmount1, long SegAmount2, long SegAmount3, long SegAmount4, long SegAmount5, long SegAmount6, string AdditionalData1, string AdditionalData2, long Wage, object userState) {
            if ((this.RequestMultiSettleTypeTokenOperationCompleted == null)) {
                this.RequestMultiSettleTypeTokenOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRequestMultiSettleTypeTokenOperationCompleted);
            }
            this.InvokeAsync("RequestMultiSettleTypeToken", new object[] {
                        TermID,
                        ResNum,
                        Amounts,
                        TotalAmount,
                        SegAmount1,
                        SegAmount2,
                        SegAmount3,
                        SegAmount4,
                        SegAmount5,
                        SegAmount6,
                        AdditionalData1,
                        AdditionalData2,
                        Wage}, this.RequestMultiSettleTypeTokenOperationCompleted, userState);
        }
        
        private void OnRequestMultiSettleTypeTokenOperationCompleted(object arg) {
            if ((this.RequestMultiSettleTypeTokenCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RequestMultiSettleTypeTokenCompleted(this, new RequestMultiSettleTypeTokenCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void RequestTokenCompletedEventHandler(object sender, RequestTokenCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RequestTokenCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RequestTokenCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void RequestMultiSettleTypeTokenCompletedEventHandler(object sender, RequestMultiSettleTypeTokenCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RequestMultiSettleTypeTokenCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RequestMultiSettleTypeTokenCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591