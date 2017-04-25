//*****************************************************************************
//    Description.....Azure IoT Hub Tester
//                                
//    Author..........Roman Kiss, rkiss@pathcom.com
//    Copyright © 2011 ATZ Consulting Inc. (see included license.rtf file)         
//                        
//    Date Created:    12/12/16
//
//    Note: 
//    Thanks for 3rd party treeview icons (selected from my desktop applications) 
//
//    Date        Modified By     Description
//-----------------------------------------------------------------------------
//    12/12/16   Roman Kiss     Initial Revision - Version 1.0.0.0 
//*****************************************************************************
//
#region Namespaces
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Linq;
#endregion

namespace RKiss.Tools.AzureIoTHubTester
{

    [ServiceContract(Namespace = "urn:rkiss.iot/tester/2016/12")]
    public interface IGenericOneWayContract
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        //[ReceiveContextEnabledAttribute(ManualControl=true)]
        void ProcessMessage(System.ServiceModel.Channels.Message msg);

        [OperationContract(IsOneWay = true, Action = "status")]
        void PostProcessMessage(string messageId, string status);
    }

    //[ServiceContract(Namespace = "urn:rkiss.iot/tester/2016/12", SessionMode = SessionMode.Required)]
    //public interface IGenericOneWaySessionContract
    //{
    //    [OperationContract(IsOneWay = true, Action = "*")]
    //    void ProcessMessage(System.ServiceModel.Channels.Message msg);
    //}


    public class NodeState
    {
        public ConfigData Config { get; set; }
        public string Message { get; set; }
        public bool EnablePublish { get; set; }
        public string Payload { get; set; }
        public string Topic { get; set; }
        public object Tag { get; set; }
    }

    [DataContract(Namespace = "urn:rkiss.iot/tester/2016/12")]
    public class ServiceHostActivatorStatus : ConfigData
    {
        [DataMember]
        public CommunicationState State { get; set; }
        [DataMember]
        public DateTime Created { get; set; }
        [DataMember]
        public string AppDomainHostName { get; set; }

    }

    [Serializable]
    [DataContract(Namespace = "urn:rkiss.iot/tester/2016/12")]
    public class ConfigData : ConfigDataBase, IExtension<ServiceHostBase>
    {
        [DataMember]
        public string TopicAddress { get; set; }
        [DataMember]
        public string SubscriptionAddress { get; set; }      
        [DataMember]
        public string TesterAddress { get; set; }
        [DataMember]
        public string HostName { get; set; }
        [DataMember]
        public bool RequiresSession { get; set; }
        [DataMember]
        public string Action { get; set; }

        public void Attach(ServiceHostBase owner) { }
        public void Detach(ServiceHostBase owner) { }

        public static XName XName { get { return XName.Get("ConfigData", "urn:rkiss.iot/tester/2016/12"); } }
    }

  
   

    [Serializable]
    [DataContract(Namespace = "urn:rkiss.iot/tester/2016/12")]
    public class ConfigDataBase
    {     
        [DataMember(Order=0)]
        public string Name { get; set; }
        [DataMember(Order = 1)]
        public string Type { get; set; }
        [DataMember(Order = 2)]
        public string DisplayName { get; set; }
        [DataMember(Order = 3)]
        public bool IsConnected { get; set; }
        [DataMember(Order = 4)]
        public string Id { get; set; }       
        [DataMember(Order = 5)]
        public string BrokerAddress { get; set; }
        [DataMember(Order = 6)]
        public string Username { get; set; }
        [DataMember(Order = 7)]
        public string Password { get; set; }

        [DataMember(Order = 8)]
        public int BrokerPort { get; set; }
        [DataMember(Order = 9)]
        public string ContentType { get; set; }   
    }

    [Serializable]
    [DataContract(Namespace = "urn:rkiss.iot/tester/2016/12")]
    public class IoTHubConnection
    {
        [DataMember]
        public string Namespace { get; set; }

        [DataMember]
        public string ConnectionString { get; set; }
    }

   

    public class ContentTypesConverter : System.ComponentModel.StringConverter
    {
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var list = new List<string>
            {
                "application/soap+msbin1;charset=UTF8", 
                "application/soap+msbinsession1;charset=UTF8",
                "application/soap+xml;charset=UTF8",
                "application/xml;charset=UTF8",
                "application/json;charset=UTF8",
                "text/plain;charset=UTF8"
            };
            return new System.ComponentModel.TypeConverter.StandardValuesCollection(list);
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }

    public sealed class TesterSettings : ApplicationSettingsBase
    {
        [UserScopedSettingAttribute()]
        [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Binary)]
        public List<IoTHubConnection> IoTHubNamespaces
        {
            get
            {
                return (List<IoTHubConnection>)this["IoTHubNamespaces"];
            }
            set
            {
                this["IoTHubNamespaces"] = (List<IoTHubConnection>)value;
            }
        }     
    }

    public delegate void PublishEventHandler(object sender, PublishEventArgs e);
    public class PublishEventArgs : EventArgs
    {
        public bool Retain { get; set; }
        public byte QoS { get; set; }
        public string Topic { get; set; }
        public string Payload { get; set; }
        public bool IsJson { get; set; }
        public TimeSpan PeriodTime { get; set; }
        public int Counter { get; set; }
    }

    public delegate void SendEventHandler(object sender, SendEventArgs e);
    public class SendEventArgs : EventArgs
    {
        public HttpRequestMessage Request { get; set; }
         
    }

    //public class DateTimeEditor2 : System.ComponentModel.Design.DateTimeEditor
    //{
    //    public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
    //    {
    //        object retval =  base.EditValue(context, provider, ((DateTime)value).ToLocalTime());
    //        return ((DateTime)retval).ToUniversalTime();
    //    }
    //}

    
    
   
    //[DataContract(Namespace = "urn:rkiss.sb/tester/2011/11")]
    //public class ProjectPackage
    //{
    //    [DataMember]
    //    public List<ConfigData> Consumers { get; set; }

 
    //}
}