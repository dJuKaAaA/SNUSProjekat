﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RTU.AnalogInputService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AnalogInput", Namespace="http://schemas.datacontract.org/2004/07/SCADACore.Models")]
    [System.SerializableAttribute()]
    public partial class AnalogInput : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private RTU.AnalogInputService.TagAlarm[] AlarmsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private RTU.AnalogInputService.DriverType DriverField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double HighLimitField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IOAddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LowLimitField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool OnScanField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ScanTimeField;
        
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
        public RTU.AnalogInputService.TagAlarm[] Alarms {
            get {
                return this.AlarmsField;
            }
            set {
                if ((object.ReferenceEquals(this.AlarmsField, value) != true)) {
                    this.AlarmsField = value;
                    this.RaisePropertyChanged("Alarms");
                }
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
        public RTU.AnalogInputService.DriverType Driver {
            get {
                return this.DriverField;
            }
            set {
                if ((this.DriverField.Equals(value) != true)) {
                    this.DriverField = value;
                    this.RaisePropertyChanged("Driver");
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
        public bool OnScan {
            get {
                return this.OnScanField;
            }
            set {
                if ((this.OnScanField.Equals(value) != true)) {
                    this.OnScanField = value;
                    this.RaisePropertyChanged("OnScan");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ScanTime {
            get {
                return this.ScanTimeField;
            }
            set {
                if ((this.ScanTimeField.Equals(value) != true)) {
                    this.ScanTimeField = value;
                    this.RaisePropertyChanged("ScanTime");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TagAlarm", Namespace="http://schemas.datacontract.org/2004/07/SCADACore.Models")]
    [System.SerializableAttribute()]
    public partial class TagAlarm : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LimitField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private RTU.AnalogInputService.PriorityType PriorityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private RTU.AnalogInputService.AlarmType TypeField;
        
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
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Limit {
            get {
                return this.LimitField;
            }
            set {
                if ((this.LimitField.Equals(value) != true)) {
                    this.LimitField = value;
                    this.RaisePropertyChanged("Limit");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public RTU.AnalogInputService.PriorityType Priority {
            get {
                return this.PriorityField;
            }
            set {
                if ((this.PriorityField.Equals(value) != true)) {
                    this.PriorityField = value;
                    this.RaisePropertyChanged("Priority");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public RTU.AnalogInputService.AlarmType Type {
            get {
                return this.TypeField;
            }
            set {
                if ((this.TypeField.Equals(value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DriverType", Namespace="http://schemas.datacontract.org/2004/07/SCADACore.Models")]
    public enum DriverType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RealTime = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Sine = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Cosine = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Ramp = 3,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PriorityType", Namespace="http://schemas.datacontract.org/2004/07/SCADACore.Models")]
    public enum PriorityType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        High = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Medium = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Low = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AlarmType", Namespace="http://schemas.datacontract.org/2004/07/SCADACore.Models")]
    public enum AlarmType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Low = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        High = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AnalogInputService.IAnalogInputService")]
    public interface IAnalogInputService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogInputService/GetAll", ReplyAction="http://tempuri.org/IAnalogInputService/GetAllResponse")]
        RTU.AnalogInputService.AnalogInput[] GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogInputService/GetAll", ReplyAction="http://tempuri.org/IAnalogInputService/GetAllResponse")]
        System.Threading.Tasks.Task<RTU.AnalogInputService.AnalogInput[]> GetAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogInputService/GetForIOAddress", ReplyAction="http://tempuri.org/IAnalogInputService/GetForIOAddressResponse")]
        RTU.AnalogInputService.AnalogInput GetForIOAddress(int ioAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogInputService/GetForIOAddress", ReplyAction="http://tempuri.org/IAnalogInputService/GetForIOAddressResponse")]
        System.Threading.Tasks.Task<RTU.AnalogInputService.AnalogInput> GetForIOAddressAsync(int ioAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogInputService/Save", ReplyAction="http://tempuri.org/IAnalogInputService/SaveResponse")]
        void Save(RTU.AnalogInputService.AnalogInput analogInput);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogInputService/Save", ReplyAction="http://tempuri.org/IAnalogInputService/SaveResponse")]
        System.Threading.Tasks.Task SaveAsync(RTU.AnalogInputService.AnalogInput analogInput);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogInputService/Create", ReplyAction="http://tempuri.org/IAnalogInputService/CreateResponse")]
        RTU.AnalogInputService.AnalogInput Create(RTU.AnalogInputService.AnalogInput input);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogInputService/Create", ReplyAction="http://tempuri.org/IAnalogInputService/CreateResponse")]
        System.Threading.Tasks.Task<RTU.AnalogInputService.AnalogInput> CreateAsync(RTU.AnalogInputService.AnalogInput input);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogInputService/GetByTagName", ReplyAction="http://tempuri.org/IAnalogInputService/GetByTagNameResponse")]
        RTU.AnalogInputService.AnalogInput GetByTagName(string tagName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogInputService/GetByTagName", ReplyAction="http://tempuri.org/IAnalogInputService/GetByTagNameResponse")]
        System.Threading.Tasks.Task<RTU.AnalogInputService.AnalogInput> GetByTagNameAsync(string tagName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogInputService/SetNewValue", ReplyAction="http://tempuri.org/IAnalogInputService/SetNewValueResponse")]
        void SetNewValue(int ioAddress, double newValue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAnalogInputService/SetNewValue", ReplyAction="http://tempuri.org/IAnalogInputService/SetNewValueResponse")]
        System.Threading.Tasks.Task SetNewValueAsync(int ioAddress, double newValue);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAnalogInputServiceChannel : RTU.AnalogInputService.IAnalogInputService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AnalogInputServiceClient : System.ServiceModel.ClientBase<RTU.AnalogInputService.IAnalogInputService>, RTU.AnalogInputService.IAnalogInputService {
        
        public AnalogInputServiceClient() {
        }
        
        public AnalogInputServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AnalogInputServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AnalogInputServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AnalogInputServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public RTU.AnalogInputService.AnalogInput[] GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<RTU.AnalogInputService.AnalogInput[]> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
        
        public RTU.AnalogInputService.AnalogInput GetForIOAddress(int ioAddress) {
            return base.Channel.GetForIOAddress(ioAddress);
        }
        
        public System.Threading.Tasks.Task<RTU.AnalogInputService.AnalogInput> GetForIOAddressAsync(int ioAddress) {
            return base.Channel.GetForIOAddressAsync(ioAddress);
        }
        
        public void Save(RTU.AnalogInputService.AnalogInput analogInput) {
            base.Channel.Save(analogInput);
        }
        
        public System.Threading.Tasks.Task SaveAsync(RTU.AnalogInputService.AnalogInput analogInput) {
            return base.Channel.SaveAsync(analogInput);
        }
        
        public RTU.AnalogInputService.AnalogInput Create(RTU.AnalogInputService.AnalogInput input) {
            return base.Channel.Create(input);
        }
        
        public System.Threading.Tasks.Task<RTU.AnalogInputService.AnalogInput> CreateAsync(RTU.AnalogInputService.AnalogInput input) {
            return base.Channel.CreateAsync(input);
        }
        
        public RTU.AnalogInputService.AnalogInput GetByTagName(string tagName) {
            return base.Channel.GetByTagName(tagName);
        }
        
        public System.Threading.Tasks.Task<RTU.AnalogInputService.AnalogInput> GetByTagNameAsync(string tagName) {
            return base.Channel.GetByTagNameAsync(tagName);
        }
        
        public void SetNewValue(int ioAddress, double newValue) {
            base.Channel.SetNewValue(ioAddress, newValue);
        }
        
        public System.Threading.Tasks.Task SetNewValueAsync(int ioAddress, double newValue) {
            return base.Channel.SetNewValueAsync(ioAddress, newValue);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AnalogInputService.IScanService", CallbackContract=typeof(RTU.AnalogInputService.IScanServiceCallback))]
    public interface IScanService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IScanService/StartScan")]
        void StartScan(int ioAddress);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IScanService/StartScan")]
        System.Threading.Tasks.Task StartScanAsync(int ioAddress);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IScanService/EndScan")]
        void EndScan(int ioAddress);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IScanService/EndScan")]
        System.Threading.Tasks.Task EndScanAsync(int ioAddress);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IScanService/EndAllScans")]
        void EndAllScans();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IScanService/EndAllScans")]
        System.Threading.Tasks.Task EndAllScansAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IScanServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IScanService/AnalogScanDone")]
        void AnalogScanDone(int ioAddress, double value);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IScanService/DigitalScanDone")]
        void DigitalScanDone(int ioAddress, bool value);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IScanServiceChannel : RTU.AnalogInputService.IScanService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ScanServiceClient : System.ServiceModel.DuplexClientBase<RTU.AnalogInputService.IScanService>, RTU.AnalogInputService.IScanService {
        
        public ScanServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ScanServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ScanServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ScanServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ScanServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void StartScan(int ioAddress) {
            base.Channel.StartScan(ioAddress);
        }
        
        public System.Threading.Tasks.Task StartScanAsync(int ioAddress) {
            return base.Channel.StartScanAsync(ioAddress);
        }
        
        public void EndScan(int ioAddress) {
            base.Channel.EndScan(ioAddress);
        }
        
        public System.Threading.Tasks.Task EndScanAsync(int ioAddress) {
            return base.Channel.EndScanAsync(ioAddress);
        }
        
        public void EndAllScans() {
            base.Channel.EndAllScans();
        }
        
        public System.Threading.Tasks.Task EndAllScansAsync() {
            return base.Channel.EndAllScansAsync();
        }
    }
}
