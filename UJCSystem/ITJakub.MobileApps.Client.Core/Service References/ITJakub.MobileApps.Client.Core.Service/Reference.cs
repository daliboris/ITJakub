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
// This code was auto-generated by Microsoft.VisualStudio.ServiceReference.Platforms, version 12.0.21005.1
// 
namespace ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Institution", Namespace="http://schemas.datacontract.org/2004/07/ITJakub.MobileApps.DataContracts")]
    public partial class Institution : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string NameField;
        
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
    [System.Runtime.Serialization.DataContractAttribute(Name="InstitutionDetails", Namespace="http://schemas.datacontract.org/2004/07/ITJakub.MobileApps.DataContracts")]
    public partial class InstitutionDetails : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.DateTime CreateTimeField;
        
        private System.Collections.ObjectModel.ObservableCollection<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.UserBasicDetails> EmployeesField;
        
        private long IdField;
        
        private string NameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreateTime {
            get {
                return this.CreateTimeField;
            }
            set {
                if ((this.CreateTimeField.Equals(value) != true)) {
                    this.CreateTimeField = value;
                    this.RaisePropertyChanged("CreateTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.ObjectModel.ObservableCollection<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.UserBasicDetails> Employees {
            get {
                return this.EmployeesField;
            }
            set {
                if ((object.ReferenceEquals(this.EmployeesField, value) != true)) {
                    this.EmployeesField = value;
                    this.RaisePropertyChanged("Employees");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Id {
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
    [System.Runtime.Serialization.DataContractAttribute(Name="UserBasicDetails", Namespace="http://schemas.datacontract.org/2004/07/ITJakub.MobileApps.DataContracts")]
    public partial class UserBasicDetails : object, System.ComponentModel.INotifyPropertyChanged {
        
        private long IdField;
        
        private ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.User UserField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Id {
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
        public ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.User User {
            get {
                return this.UserField;
            }
            set {
                if ((object.ReferenceEquals(this.UserField, value) != true)) {
                    this.UserField = value;
                    this.RaisePropertyChanged("User");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/ITJakub.MobileApps.DataContracts")]
    public partial class User : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string EmailField;
        
        private string FirstNameField;
        
        private string LastNameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName {
            get {
                return this.FirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNameField, value) != true)) {
                    this.FirstNameField = value;
                    this.RaisePropertyChanged("FirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName {
            get {
                return this.LastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNameField, value) != true)) {
                    this.LastNameField = value;
                    this.RaisePropertyChanged("LastName");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="UserDetails", Namespace="http://schemas.datacontract.org/2004/07/ITJakub.MobileApps.DataContracts")]
    public partial class UserDetails : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Collections.ObjectModel.ObservableCollection<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.Group> CreatedGroupsField;
        
        private System.Collections.ObjectModel.ObservableCollection<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.Task> CreatedTasksField;
        
        private long IdField;
        
        private System.Collections.ObjectModel.ObservableCollection<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.Group> MemberOfGroupsField;
        
        private ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.User UserField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.ObjectModel.ObservableCollection<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.Group> CreatedGroups {
            get {
                return this.CreatedGroupsField;
            }
            set {
                if ((object.ReferenceEquals(this.CreatedGroupsField, value) != true)) {
                    this.CreatedGroupsField = value;
                    this.RaisePropertyChanged("CreatedGroups");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.ObjectModel.ObservableCollection<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.Task> CreatedTasks {
            get {
                return this.CreatedTasksField;
            }
            set {
                if ((object.ReferenceEquals(this.CreatedTasksField, value) != true)) {
                    this.CreatedTasksField = value;
                    this.RaisePropertyChanged("CreatedTasks");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Id {
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
        public System.Collections.ObjectModel.ObservableCollection<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.Group> MemberOfGroups {
            get {
                return this.MemberOfGroupsField;
            }
            set {
                if ((object.ReferenceEquals(this.MemberOfGroupsField, value) != true)) {
                    this.MemberOfGroupsField = value;
                    this.RaisePropertyChanged("MemberOfGroups");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.User User {
            get {
                return this.UserField;
            }
            set {
                if ((object.ReferenceEquals(this.UserField, value) != true)) {
                    this.UserField = value;
                    this.RaisePropertyChanged("User");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Group", Namespace="http://schemas.datacontract.org/2004/07/ITJakub.MobileApps.DataContracts")]
    public partial class Group : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Collections.ObjectModel.ObservableCollection<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.UserBasicDetails> MembersField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.ObjectModel.ObservableCollection<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.UserBasicDetails> Members {
            get {
                return this.MembersField;
            }
            set {
                if ((object.ReferenceEquals(this.MembersField, value) != true)) {
                    this.MembersField = value;
                    this.RaisePropertyChanged("Members");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Task", Namespace="http://schemas.datacontract.org/2004/07/ITJakub.MobileApps.DataContracts")]
    public partial class Task : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string DataField;
        
        private string NameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Data {
            get {
                return this.DataField;
            }
            set {
                if ((object.ReferenceEquals(this.DataField, value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="TaskDetails", Namespace="http://schemas.datacontract.org/2004/07/ITJakub.MobileApps.DataContracts")]
    public partial class TaskDetails : object, System.ComponentModel.INotifyPropertyChanged {
        
        private ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.User AuthorField;
        
        private System.DateTime CreateTimeField;
        
        private long IdField;
        
        private ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.Task TaskField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.User Author {
            get {
                return this.AuthorField;
            }
            set {
                if ((object.ReferenceEquals(this.AuthorField, value) != true)) {
                    this.AuthorField = value;
                    this.RaisePropertyChanged("Author");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreateTime {
            get {
                return this.CreateTimeField;
            }
            set {
                if ((this.CreateTimeField.Equals(value) != true)) {
                    this.CreateTimeField = value;
                    this.RaisePropertyChanged("CreateTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Id {
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
        public ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.Task Task {
            get {
                return this.TaskField;
            }
            set {
                if ((object.ReferenceEquals(this.TaskField, value) != true)) {
                    this.TaskField = value;
                    this.RaisePropertyChanged("Task");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="CreateGroupResponse", Namespace="http://schemas.datacontract.org/2004/07/ITJakub.MobileApps.DataContracts")]
    public partial class CreateGroupResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string EnterCodeField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EnterCode {
            get {
                return this.EnterCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.EnterCodeField, value) != true)) {
                    this.EnterCodeField = value;
                    this.RaisePropertyChanged("EnterCode");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="GroupDetails", Namespace="http://schemas.datacontract.org/2004/07/ITJakub.MobileApps.DataContracts")]
    public partial class GroupDetails : object, System.ComponentModel.INotifyPropertyChanged {
        
        private ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.User AuthorField;
        
        private System.DateTime CreateTimeField;
        
        private ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.Group GroupField;
        
        private long IdField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.User Author {
            get {
                return this.AuthorField;
            }
            set {
                if ((object.ReferenceEquals(this.AuthorField, value) != true)) {
                    this.AuthorField = value;
                    this.RaisePropertyChanged("Author");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreateTime {
            get {
                return this.CreateTimeField;
            }
            set {
                if ((this.CreateTimeField.Equals(value) != true)) {
                    this.CreateTimeField = value;
                    this.RaisePropertyChanged("CreateTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.Group Group {
            get {
                return this.GroupField;
            }
            set {
                if ((object.ReferenceEquals(this.GroupField, value) != true)) {
                    this.GroupField = value;
                    this.RaisePropertyChanged("Group");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Id {
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
    [System.Runtime.Serialization.DataContractAttribute(Name="SynchronizedObjectDetails", Namespace="http://schemas.datacontract.org/2004/07/ITJakub.MobileApps.DataContracts")]
    public partial class SynchronizedObjectDetails : object, System.ComponentModel.INotifyPropertyChanged {
        
        private ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.User AuthorField;
        
        private System.DateTime CreateTimeField;
        
        private long IdField;
        
        private ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.SynchronizedObject SynchronizedObjectField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.User Author {
            get {
                return this.AuthorField;
            }
            set {
                if ((object.ReferenceEquals(this.AuthorField, value) != true)) {
                    this.AuthorField = value;
                    this.RaisePropertyChanged("Author");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreateTime {
            get {
                return this.CreateTimeField;
            }
            set {
                if ((this.CreateTimeField.Equals(value) != true)) {
                    this.CreateTimeField = value;
                    this.RaisePropertyChanged("CreateTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Id {
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
        public ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.SynchronizedObject SynchronizedObject {
            get {
                return this.SynchronizedObjectField;
            }
            set {
                if ((object.ReferenceEquals(this.SynchronizedObjectField, value) != true)) {
                    this.SynchronizedObjectField = value;
                    this.RaisePropertyChanged("SynchronizedObject");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="SynchronizedObject", Namespace="http://schemas.datacontract.org/2004/07/ITJakub.MobileApps.DataContracts")]
    public partial class SynchronizedObject : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string DataField;
        
        private string ObjectTypeField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Data {
            get {
                return this.DataField;
            }
            set {
                if ((object.ReferenceEquals(this.DataField, value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ObjectType {
            get {
                return this.ObjectTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.ObjectTypeField, value) != true)) {
                    this.ObjectTypeField = value;
                    this.RaisePropertyChanged("ObjectType");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ITJakub.MobileApps.Client.Core.Service.IMobileAppsService")]
    public interface IMobileAppsService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMobileAppsService/CreateInstitution", ReplyAction="http://tempuri.org/IMobileAppsService/CreateInstitutionResponse")]
        System.Threading.Tasks.Task CreateInstitutionAsync(ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.Institution institution);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMobileAppsService/GetInstitutionDetails", ReplyAction="http://tempuri.org/IMobileAppsService/GetInstitutionDetailsResponse")]
        System.Threading.Tasks.Task<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.InstitutionDetails> GetInstitutionDetailsAsync(string institutionId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMobileAppsService/AddUserToInstitution", ReplyAction="http://tempuri.org/IMobileAppsService/AddUserToInstitutionResponse")]
        System.Threading.Tasks.Task AddUserToInstitutionAsync(string enterCode, string userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMobileAppsService/CreateUser", ReplyAction="http://tempuri.org/IMobileAppsService/CreateUserResponse")]
        System.Threading.Tasks.Task CreateUserAsync(string authenticationProvider, string authenticationProviderToken, ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMobileAppsService/GetUserDetails", ReplyAction="http://tempuri.org/IMobileAppsService/GetUserDetailsResponse")]
        System.Threading.Tasks.Task<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.UserDetails> GetUserDetailsAsync(string userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMobileAppsService/GetTasksForApplication", ReplyAction="http://tempuri.org/IMobileAppsService/GetTasksForApplicationResponse")]
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.TaskDetails>> GetTasksForApplicationAsync(string applicationId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMobileAppsService/CreateTaskForApplication", ReplyAction="http://tempuri.org/IMobileAppsService/CreateTaskForApplicationResponse")]
        System.Threading.Tasks.Task CreateTaskForApplicationAsync(string applicationId, string userId, ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.Task task);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMobileAppsService/CreateGroup", ReplyAction="http://tempuri.org/IMobileAppsService/CreateGroupResponse")]
        System.Threading.Tasks.Task<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.CreateGroupResponse> CreateGroupAsync(string userId, ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.Group group);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMobileAppsService/AssignTaskToGroup", ReplyAction="http://tempuri.org/IMobileAppsService/AssignTaskToGroupResponse")]
        System.Threading.Tasks.Task AssignTaskToGroupAsync(string groupId, string taskId, string userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMobileAppsService/AddUserToGroup", ReplyAction="http://tempuri.org/IMobileAppsService/AddUserToGroupResponse")]
        System.Threading.Tasks.Task AddUserToGroupAsync(string enterCode, string userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMobileAppsService/GetGroupDetails", ReplyAction="http://tempuri.org/IMobileAppsService/GetGroupDetailsResponse")]
        System.Threading.Tasks.Task<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.GroupDetails> GetGroupDetailsAsync(string groupId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMobileAppsService/GetSynchronizedObjects", ReplyAction="http://tempuri.org/IMobileAppsService/GetSynchronizedObjectsResponse")]
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.SynchronizedObjectDetails>> GetSynchronizedObjectsAsync(string groupId, string applicationId, string objectType, string since);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMobileAppsService/CreateSynchronizedObject", ReplyAction="http://tempuri.org/IMobileAppsService/CreateSynchronizedObjectResponse")]
        System.Threading.Tasks.Task CreateSynchronizedObjectAsync(string groupId, string applicationId, string userId, ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.SynchronizedObject synchronizedObject);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMobileAppsServiceChannel : ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.IMobileAppsService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MobileAppsServiceClient : System.ServiceModel.ClientBase<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.IMobileAppsService>, ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.IMobileAppsService {
        
        public MobileAppsServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Threading.Tasks.Task CreateInstitutionAsync(ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.Institution institution) {
            return base.Channel.CreateInstitutionAsync(institution);
        }
        
        public System.Threading.Tasks.Task<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.InstitutionDetails> GetInstitutionDetailsAsync(string institutionId) {
            return base.Channel.GetInstitutionDetailsAsync(institutionId);
        }
        
        public System.Threading.Tasks.Task AddUserToInstitutionAsync(string enterCode, string userId) {
            return base.Channel.AddUserToInstitutionAsync(enterCode, userId);
        }
        
        public System.Threading.Tasks.Task CreateUserAsync(string authenticationProvider, string authenticationProviderToken, ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.User user) {
            return base.Channel.CreateUserAsync(authenticationProvider, authenticationProviderToken, user);
        }
        
        public System.Threading.Tasks.Task<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.UserDetails> GetUserDetailsAsync(string userId) {
            return base.Channel.GetUserDetailsAsync(userId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.TaskDetails>> GetTasksForApplicationAsync(string applicationId) {
            return base.Channel.GetTasksForApplicationAsync(applicationId);
        }
        
        public System.Threading.Tasks.Task CreateTaskForApplicationAsync(string applicationId, string userId, ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.Task task) {
            return base.Channel.CreateTaskForApplicationAsync(applicationId, userId, task);
        }
        
        public System.Threading.Tasks.Task<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.CreateGroupResponse> CreateGroupAsync(string userId, ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.Group group) {
            return base.Channel.CreateGroupAsync(userId, group);
        }
        
        public System.Threading.Tasks.Task AssignTaskToGroupAsync(string groupId, string taskId, string userId) {
            return base.Channel.AssignTaskToGroupAsync(groupId, taskId, userId);
        }
        
        public System.Threading.Tasks.Task AddUserToGroupAsync(string enterCode, string userId) {
            return base.Channel.AddUserToGroupAsync(enterCode, userId);
        }
        
        public System.Threading.Tasks.Task<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.GroupDetails> GetGroupDetailsAsync(string groupId) {
            return base.Channel.GetGroupDetailsAsync(groupId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.SynchronizedObjectDetails>> GetSynchronizedObjectsAsync(string groupId, string applicationId, string objectType, string since) {
            return base.Channel.GetSynchronizedObjectsAsync(groupId, applicationId, objectType, since);
        }
        
        public System.Threading.Tasks.Task CreateSynchronizedObjectAsync(string groupId, string applicationId, string userId, ITJakub.MobileApps.Client.Core.ITJakub.MobileApps.Client.Core.Service.SynchronizedObject synchronizedObject) {
            return base.Channel.CreateSynchronizedObjectAsync(groupId, applicationId, userId, synchronizedObject);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
    }
}
