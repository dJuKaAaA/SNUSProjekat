﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Trending.CoreAnalogOutputRef {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AnalogOutput", Namespace="http://schemas.datacontract.org/2004/07/SCADACore.Models")]
    [System.SerializableAttribute()]
    public partial class AnalogOutput : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double HighLimitField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IOAddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LowLimitField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TagNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UnitsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double ValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double HighLimit {
            get {
                return this.HighLimitField;
            }
            set {
                if ((this.HighLimitField.Equals(value) != true)) {
                    this.HighLimitField = value;
                    this.RaisePropertyChanged("HighLimit");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IOAddress {
            get {
                return this.IOAddressField;
            }
            set {
                if ((this.IOAddressField.Equals(value) != true)) {
                    this.IOAddressField = value;
                    this.RaisePropertyChanged("IOAddress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double LowLimit {
            get {
                return this.LowLimitField;
            }
            set {
                if ((this.LowLimitField.Equals(value) != true)) {
                    this.LowLimitField = value;
                    this.RaisePropertyChanged("LowLimit");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TagName {
            get {
                return this.TagNameField;
            }
            set {
                if ((object.ReferenceEquals(this.TagNameField, value) != true)) {
                    this.TagNameField = value;
                    this.RaisePropertyChanged("TagName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Units {
            get {
                return this.UnitsField;
            }
            set {
                if ((object.ReferenceEquals(this.UnitsField, value) != true)) {
                    this.UnitsField = value;
                    this.RaisePropertyChanged("Units");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Value {
            get {
                return this.ValueField;
            }
            set {
                if ((this.ValueField.Equals(value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CoreAnalogOutputRef.IAnalogOutputService")]
    public interface IAnalogOutputService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogOutputService/GetAll", ReplyAction="http://tempuri.org/IAnalogOutputService/GetAllResponse")]
        Trending.CoreAnalogOutputRef.AnalogOutput[] GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogOutputService/GetAll", ReplyAction="http://tempuri.org/IAnalogOutputService/GetAllResponse")]
        System.Threading.Tasks.Task<Trending.CoreAnalogOutputRef.AnalogOutput[]> GetAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogOutputService/GetForIOAddress", ReplyAction="http://tempuri.org/IAnalogOutputService/GetForIOAddressResponse")]
        Trending.CoreAnalogOutputRef.AnalogOutput GetForIOAddress(int ioAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogOutputService/GetForIOAddress", ReplyAction="http://tempuri.org/IAnalogOutputService/GetForIOAddressResponse")]
        System.Threading.Tasks.Task<Trending.CoreAnalogOutputRef.AnalogOutput> GetForIOAddressAsync(int ioAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogOutputService/Save", ReplyAction="http://tempuri.org/IAnalogOutputService/SaveResponse")]
        void Save(Trending.CoreAnalogOutputRef.AnalogOutput analogOutput);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogOutputService/Save", ReplyAction="http://tempuri.org/IAnalogOutputService/SaveResponse")]
        System.Threading.Tasks.Task SaveAsync(Trending.CoreAnalogOutputRef.AnalogOutput analogOutput);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogOutputService/SetNewValue", ReplyAction="http://tempuri.org/IAnalogOutputService/SetNewValueResponse")]
        void SetNewValue(int ioAddress, double newValue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogOutputService/SetNewValue", ReplyAction="http://tempuri.org/IAnalogOutputService/SetNewValueResponse")]
        System.Threading.Tasks.Task SetNewValueAsync(int ioAddress, double newValue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogOutputService/Create", ReplyAction="http://tempuri.org/IAnalogOutputService/CreateResponse")]
        Trending.CoreAnalogOutputRef.AnalogOutput Create(Trending.CoreAnalogOutputRef.AnalogOutput output);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogOutputService/Create", ReplyAction="http://tempuri.org/IAnalogOutputService/CreateResponse")]
        System.Threading.Tasks.Task<Trending.CoreAnalogOutputRef.AnalogOutput> CreateAsync(Trending.CoreAnalogOutputRef.AnalogOutput output);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogOutputService/GetByTagName", ReplyAction="http://tempuri.org/IAnalogOutputService/GetByTagNameResponse")]
        Trending.CoreAnalogOutputRef.AnalogOutput GetByTagName(string tagName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogOutputService/GetByTagName", ReplyAction="http://tempuri.org/IAnalogOutputService/GetByTagNameResponse")]
        System.Threading.Tasks.Task<Trending.CoreAnalogOutputRef.AnalogOutput> GetByTagNameAsync(string tagName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAnalogOutputServiceChannel : Trending.CoreAnalogOutputRef.IAnalogOutputService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AnalogOutputServiceClient : System.ServiceModel.ClientBase<Trending.CoreAnalogOutputRef.IAnalogOutputService>, Trending.CoreAnalogOutputRef.IAnalogOutputService {
        
        public AnalogOutputServiceClient() {
        }
        
        public AnalogOutputServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AnalogOutputServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AnalogOutputServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AnalogOutputServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Trending.CoreAnalogOutputRef.AnalogOutput[] GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<Trending.CoreAnalogOutputRef.AnalogOutput[]> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
        
        public Trending.CoreAnalogOutputRef.AnalogOutput GetForIOAddress(int ioAddress) {
            return base.Channel.GetForIOAddress(ioAddress);
        }
        
        public System.Threading.Tasks.Task<Trending.CoreAnalogOutputRef.AnalogOutput> GetForIOAddressAsync(int ioAddress) {
            return base.Channel.GetForIOAddressAsync(ioAddress);
        }
        
        public void Save(Trending.CoreAnalogOutputRef.AnalogOutput analogOutput) {
            base.Channel.Save(analogOutput);
        }
        
        public System.Threading.Tasks.Task SaveAsync(Trending.CoreAnalogOutputRef.AnalogOutput analogOutput) {
            return base.Channel.SaveAsync(analogOutput);
        }
        
        public void SetNewValue(int ioAddress, double newValue) {
            base.Channel.SetNewValue(ioAddress, newValue);
        }
        
        public System.Threading.Tasks.Task SetNewValueAsync(int ioAddress, double newValue) {
            return base.Channel.SetNewValueAsync(ioAddress, newValue);
        }
        
        public Trending.CoreAnalogOutputRef.AnalogOutput Create(Trending.CoreAnalogOutputRef.AnalogOutput output) {
            return base.Channel.Create(output);
        }
        
        public System.Threading.Tasks.Task<Trending.CoreAnalogOutputRef.AnalogOutput> CreateAsync(Trending.CoreAnalogOutputRef.AnalogOutput output) {
            return base.Channel.CreateAsync(output);
        }
        
        public Trending.CoreAnalogOutputRef.AnalogOutput GetByTagName(string tagName) {
            return base.Channel.GetByTagName(tagName);
        }
        
        public System.Threading.Tasks.Task<Trending.CoreAnalogOutputRef.AnalogOutput> GetByTagNameAsync(string tagName) {
            return base.Channel.GetByTagNameAsync(tagName);
        }
    }
}
