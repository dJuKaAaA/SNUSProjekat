﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Trending.CoreSimDriverRef {
    using System.Runtime.Serialization;
    
    
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CoreSimDriverRef.ISimDriver")]
    public interface ISimDriver {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISimDriver/GenerateValue", ReplyAction="http://tempuri.org/ISimDriver/GenerateValueResponse")]
        double GenerateValue(Trending.CoreSimDriverRef.DriverType driverType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISimDriver/GenerateValue", ReplyAction="http://tempuri.org/ISimDriver/GenerateValueResponse")]
        System.Threading.Tasks.Task<double> GenerateValueAsync(Trending.CoreSimDriverRef.DriverType driverType);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISimDriverChannel : Trending.CoreSimDriverRef.ISimDriver, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SimDriverClient : System.ServiceModel.ClientBase<Trending.CoreSimDriverRef.ISimDriver>, Trending.CoreSimDriverRef.ISimDriver {
        
        public SimDriverClient() {
        }
        
        public SimDriverClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SimDriverClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SimDriverClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SimDriverClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public double GenerateValue(Trending.CoreSimDriverRef.DriverType driverType) {
            return base.Channel.GenerateValue(driverType);
        }
        
        public System.Threading.Tasks.Task<double> GenerateValueAsync(Trending.CoreSimDriverRef.DriverType driverType) {
            return base.Channel.GenerateValueAsync(driverType);
        }
    }
}