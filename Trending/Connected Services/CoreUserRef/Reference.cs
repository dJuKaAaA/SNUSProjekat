﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Trending.CoreUserRef {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/SCADACore.Models")]
    [System.SerializableAttribute()]
    public partial class User : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Trending.CoreUserRef.Role RoleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SurnameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
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
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Trending.CoreUserRef.Role Role {
            get {
                return this.RoleField;
            }
            set {
                if ((this.RoleField.Equals(value) != true)) {
                    this.RoleField = value;
                    this.RaisePropertyChanged("Role");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Surname {
            get {
                return this.SurnameField;
            }
            set {
                if ((object.ReferenceEquals(this.SurnameField, value) != true)) {
                    this.SurnameField = value;
                    this.RaisePropertyChanged("Surname");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Role", Namespace="http://schemas.datacontract.org/2004/07/SCADACore.Models")]
    public enum Role : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Admin = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Standard = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CoreUserRef.IUserService")]
    public interface IUserService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/LogIn", ReplyAction="http://tempuri.org/IUserService/LogInResponse")]
        Trending.CoreUserRef.User LogIn(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/LogIn", ReplyAction="http://tempuri.org/IUserService/LogInResponse")]
        System.Threading.Tasks.Task<Trending.CoreUserRef.User> LogInAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/CreateAccount", ReplyAction="http://tempuri.org/IUserService/CreateAccountResponse")]
        Trending.CoreUserRef.User CreateAccount(Trending.CoreUserRef.User user, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/CreateAccount", ReplyAction="http://tempuri.org/IUserService/CreateAccountResponse")]
        System.Threading.Tasks.Task<Trending.CoreUserRef.User> CreateAccountAsync(Trending.CoreUserRef.User user, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetAll", ReplyAction="http://tempuri.org/IUserService/GetAllResponse")]
        Trending.CoreUserRef.User[] GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetAll", ReplyAction="http://tempuri.org/IUserService/GetAllResponse")]
        System.Threading.Tasks.Task<Trending.CoreUserRef.User[]> GetAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetById", ReplyAction="http://tempuri.org/IUserService/GetByIdResponse")]
        Trending.CoreUserRef.User GetById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetById", ReplyAction="http://tempuri.org/IUserService/GetByIdResponse")]
        System.Threading.Tasks.Task<Trending.CoreUserRef.User> GetByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetByUsername", ReplyAction="http://tempuri.org/IUserService/GetByUsernameResponse")]
        Trending.CoreUserRef.User GetByUsername(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetByUsername", ReplyAction="http://tempuri.org/IUserService/GetByUsernameResponse")]
        System.Threading.Tasks.Task<Trending.CoreUserRef.User> GetByUsernameAsync(string username);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserServiceChannel : Trending.CoreUserRef.IUserService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserServiceClient : System.ServiceModel.ClientBase<Trending.CoreUserRef.IUserService>, Trending.CoreUserRef.IUserService {
        
        public UserServiceClient() {
        }
        
        public UserServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Trending.CoreUserRef.User LogIn(string username, string password) {
            return base.Channel.LogIn(username, password);
        }
        
        public System.Threading.Tasks.Task<Trending.CoreUserRef.User> LogInAsync(string username, string password) {
            return base.Channel.LogInAsync(username, password);
        }
        
        public Trending.CoreUserRef.User CreateAccount(Trending.CoreUserRef.User user, string password) {
            return base.Channel.CreateAccount(user, password);
        }
        
        public System.Threading.Tasks.Task<Trending.CoreUserRef.User> CreateAccountAsync(Trending.CoreUserRef.User user, string password) {
            return base.Channel.CreateAccountAsync(user, password);
        }
        
        public Trending.CoreUserRef.User[] GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<Trending.CoreUserRef.User[]> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
        
        public Trending.CoreUserRef.User GetById(int id) {
            return base.Channel.GetById(id);
        }
        
        public System.Threading.Tasks.Task<Trending.CoreUserRef.User> GetByIdAsync(int id) {
            return base.Channel.GetByIdAsync(id);
        }
        
        public Trending.CoreUserRef.User GetByUsername(string username) {
            return base.Channel.GetByUsername(username);
        }
        
        public System.Threading.Tasks.Task<Trending.CoreUserRef.User> GetByUsernameAsync(string username) {
            return base.Channel.GetByUsernameAsync(username);
        }
    }
}
