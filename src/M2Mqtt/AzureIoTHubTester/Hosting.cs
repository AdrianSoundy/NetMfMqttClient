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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

#endregion

namespace RKiss.Tools.AzureIoTHubTester
{
    #region MqttClient activator
    // this is a service activator acrross the appDomains
    public sealed class MqttClientActivator : MarshalByRefObject, IDisposable
    {
        DateTime _created = DateTime.Now;
        string _name = string.Empty;
        MqttClient _client = null;
        ConfigData _configData = null;
        string[] _topics = null;

        #region Dispose
        void IDisposable.Dispose()
        {
            Disconnect();
        }
        public override object InitializeLifetimeService()
        {
            // infinite lifetime
            return null;
        }
        #endregion

        #region Create
        public static MqttClientActivator Create(AppDomain appDomain, ConfigData config)
        {           
            string _assemblyName = Assembly.GetAssembly(typeof(MqttClientActivator)).FullName;
            MqttClientActivator activator = appDomain.CreateInstanceAndUnwrap(_assemblyName, typeof(MqttClientActivator).ToString()) as MqttClientActivator;
            activator.SetClient(config);
            return activator;
        }

        private void SetClient(ConfigData config)
        {
            try
            {
                if (_client == null)
                {
                    _client = new MqttClient(config.BrokerAddress.Trim(), config.BrokerPort, true, null, null, MqttSslProtocols.TLSv1_2);
                    _client.ProtocolVersion = MqttProtocolVersion.Version_3_1_1;

                    // event when connection has been dropped
                    _client.ConnectionClosed += Client_ConnectionClosed;

                    // handler for received messages on the subscribed topics
                    _client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

                    // handler for publisher
                    _client.MqttMsgPublished += Client_MqttMsgPublished;

                    // handler for subscriber 
                    _client.MqttMsgSubscribed += Client_MqttMsgSubscribed;

                    // handler for unsubscriber
                    _client.MqttMsgUnsubscribed += client_MqttMsgUnsubscribed;

                    _name = config.BrokerAddress + "/" + config.Name;

                    this._configData = config;
                    this.AddToStorage(this);

                    LogMessage($"[{this.Name}] Client has been created in the appDomain {AppDomain.CurrentDomain.FriendlyName}");
                }
                else
                {
                    throw new InvalidOperationException("The MqttClient has been already setup");
                }
            }
            finally
            {
                CallContext.FreeNamedDataSlot("_config");
            }
        }

        #region events
        private void Client_ConnectionClosed(object sender, EventArgs e)
        {
            LogMessage($"[{this.Name}] Connection closed", "Warning");
            var payload = new MqttMsgEventArgs("$iothub/clientproxy/", Encoding.UTF8.GetBytes("Disconnected"), false, 0, false);
            this.ForwardMessageAsync(payload).Wait();
        }

        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            LogMessage($"[{this.Name}] Subscriber received at {e.Topic}", "HighlightInfo");
            this.ForwardMessageAsync(new MqttMsgEventArgs(e)).Wait();
        }

        private void Client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            LogMessage($"[{this.Name}] Response from publish {e.MessageId.ToString()}");
        }

        private void Client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            LogMessage($"[{this.Name}] Response from subscribe {e.MessageId.ToString()}");
        }
        private void client_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            LogMessage($"[{this.Name}] Response from unsibscribe {e.MessageId.ToString()}");
        }
        #endregion

        private void AddToStorage(MqttClientActivator activator)
        {
            List<MqttClientActivator> activators = this.GetStorage();
            if (activators.Exists(delegate (MqttClientActivator host) { return host.Name == activator.Name; }))
            {
                LogMessage($"[{this.Name}] Internal Error during add appDomain {AppDomain.CurrentDomain.FriendlyName} to storage, activator alredy exist it", "Error");
                throw new InvalidOperationException(string.Format("The client '{0}' is already hosted in the appDomain '{1}'", activator.Name, AppDomain.CurrentDomain.FriendlyName));
            }
            activators.Add(this);
        }
        private void RemoveFromStorage(MqttClientActivator activator)
        {
            List<MqttClientActivator> activators = this.GetStorage();
            if (activators.Exists(delegate (MqttClientActivator host) { return host.Name == activator.Name; }))
            {
                activators.Remove(activator);
            }
        }
        private List<MqttClientActivator> GetStorage()
        {
            string key = typeof(MqttClientActivator).FullName;
            List<MqttClientActivator> activators = AppDomain.CurrentDomain.GetData(key) as List<MqttClientActivator>;
            if (activators == null)
            {
                lock (AppDomain.CurrentDomain.FriendlyName)
                {
                    activators = AppDomain.CurrentDomain.GetData(key) as List<MqttClientActivator>;
                    if (activators == null)
                    {
                        activators = new List<MqttClientActivator>();
                        AppDomain.CurrentDomain.SetData(key, activators);
                    }
                }
            }
            return activators;
        }
        #endregion

        #region Remoting
        public void Connect(string password = null)
        {
            if (_client != null)
            {
                try
                {
                    if (_client.IsConnected == false)
                    {
                        // update a SAS for reconnection
                        if (string.IsNullOrEmpty(password) == false)
                            this._configData.Password = password;

                        byte connCode = _client.Connect(this._configData.Name, this._configData.Username, this._configData.Password, false, MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false, "$iothub/twin/GET/?$rid=911", "Disconnected", false, 60);

                        if(connCode != 0)
                            throw new Exception($"Connect failed, code = {connCode}");

                        _topics = new string[] { "$iothub/methods/POST/#", $"devices/{_configData.Name}/messages/devicebound/#", "$iothub/twin/PATCH/properties/desired/#", "$iothub/twin/res/#" };

                        ushort subCode = _client.Subscribe(_topics, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

                        LogMessage($"[{this.Name}] Connected", "HighlightInfo");
                    }
                }
                catch (Exception ex)
                {
                    RemoveFromStorage(this);
                    LogMessage($"[{this.Name}] Connecting device failed: {ex.Message}", "Error");
                    throw ex;
                }
            }
        }
        public void Disconnect()
        {
            if (_client != null && _client.IsConnected)
            {
                try
                {
                    //if(_topics != null)
                    //    _client.Unsubscribe(_topics);

                    _client.Disconnect();
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {                   
                    try
                    {
                        // one more change to disconnect
                        if (_client.IsConnected)
                        {
                            _client.Disconnect();
                            Thread.Sleep(1000);
                        }
                    }
                    catch(Exception ex2)
                    {
                        throw new Exception($"Disconnect device failed, error = {ex2.Message}");
                    } 
                    finally
                    {
                        LogMessage($"[{this.Name}] Disconnecting device failed: {ex.Message}", "Error");
                    }                   
                }
            }
        }
        public void Abort()
        {
            if (_client != null)
            {
                try
                {
                    //_host.Abort();
                    Trace.WriteLine(string.Format("ServiceHostActivator '{0}' aborted", this.Name));
                }
                catch (Exception ex)
                {
                    LogMessage($"[{this.Name}] Aborting device failed: {ex.Message}", "Error");
                    throw ex;
                }
            }
        }
        public MqttClient Client { get { return _client; } } // valid only for default domain! 
        public AppDomain AppDomainHost { get { return AppDomain.CurrentDomain; } }
        public string Name { get { return _name; } }
        public DateTime Created { get { return _created; } }
        public ushort Publish(string topic, string payload, byte qos, bool retain)
        {
            if (_client.IsConnected)
                return _client.Publish(topic, Encoding.UTF8.GetBytes(payload), qos, retain);
            else
                return 0;
        }
        #endregion

        #region WCF
        private async Task ForwardMessageAsync(MqttMsgEventArgs e)
        {
            await Task.Run( () =>
            {
                ChannelFactory<IGenericOneWayContract> factory = null;
                try
                {
                    var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
                    var se = new ServiceEndpoint(ContractDescription.GetContract(typeof(IGenericOneWayContract)), binding, new EndpointAddress(this._configData.TesterAddress));
                    factory = new ChannelFactory<IGenericOneWayContract>(se);
                    var channel = factory.CreateChannel();

                    using (var scope = new OperationContextScope((IContextChannel)channel))
                    {
                        var message = Message.CreateMessage(MessageVersion.Soap12WSAddressing10, "*", JsonConvert.SerializeObject(e));
                        message.Headers.Add(MessageHeader.CreateHeader(ConfigData.XName.LocalName, ConfigData.XName.NamespaceName, this._configData));
                        channel.ProcessMessage(message);

                        Trace.WriteLine("VirtualService: --- Message has been sent to tester ---");
                    }
                    factory.Close();
                }
                catch (CommunicationException ex)
                {
                    if (factory != null)
                    {
                        if (factory.State == CommunicationState.Faulted)
                            factory.Abort();
                        else if (factory.State != CommunicationState.Closed)
                            factory.Close();
                        factory = null;
                    }
                    Trace.WriteLine(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
                catch (Exception ex)
                {
                    if (factory != null)
                    {
                        if (factory.State == CommunicationState.Faulted)
                            factory.Abort();
                        else if (factory.State != CommunicationState.Closed)
                            factory.Close();
                        factory = null;
                    }
                    Trace.WriteLine(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            });
        }

        public void LogMessage(string message, string severity = "Info")
        {                       
            var payload = new MqttMsgEventArgs($"$iothub/logmessage/{severity}", Encoding.UTF8.GetBytes($"{DateTime.Now.ToLocalTime().ToString()}: {message}"), false, 0, false);
            this.ForwardMessageAsync(payload).Wait();
        }
        #endregion
    }
    #endregion

    #region HostServices
    public sealed class HostServices : IDisposable
    {
        #region Private Members
        ReaderWriterLock _rwl = new ReaderWriterLock();
        string _Name = string.Empty;
        List<AppDomain> _appDomains = new List<AppDomain>();
        static string _key = typeof(MqttClientActivator).FullName;
        bool _selfHosted = false;
        #endregion

        #region Constructors
        public HostServices()
            : this(false)
        {
        }
        public HostServices(bool selfHosted)
        {
            _selfHosted = selfHosted;
            Trace.WriteLine(string.Format("The HostServices '{0}' runtime has been created, selfHosted={1}", _Name, selfHosted));
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            try
            {
                _rwl.AcquireWriterLock(TimeSpan.FromSeconds(60));
                _appDomains.Clear();
            }
            finally
            {
                _rwl.ReleaseWriterLock();
            }
        }
        #endregion

        #region Name
        public string Name
        {
            get { return _Name; }
        }
        #endregion

        #region Add
        public void Add(string appDomainName, ConfigData config)
        {
            try
            {
                _rwl.AcquireWriterLock(TimeSpan.FromSeconds(60));
                appDomainName = ValidateAppDomainName(appDomainName);
                AppDomain appDomain = this.CreateDomainHost(appDomainName);
                MqttClientActivator.Create(appDomain, config);
            }
            finally
            {
                _rwl.ReleaseWriterLock();
            }
        }
     
        private AppDomain CreateDomainHost(string appDomainName)
        {
            AppDomain appDomain = _appDomains.Find(delegate(AppDomain ad) { return ad.FriendlyName == appDomainName; });
            if (appDomain == null)
            {
                appDomain = AppDomain.CurrentDomain.FriendlyName == appDomainName ? AppDomain.CurrentDomain : AppDomain.CreateDomain(appDomainName);
                _appDomains.Add(appDomain);
                Trace.WriteLine(string.Format("The AppDomain '{0}' has been created", appDomainName));

                appDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
                {
                    Trace.WriteLine(string.Format("[{0}] UnhandledException = {1}", (sender as AppDomain).FriendlyName, e.ExceptionObject));
                };
                appDomain.DomainUnload += delegate(object sender, EventArgs e)
                {
                    Trace.WriteLine(string.Format("[{0}] DomainUnload", (sender as AppDomain).FriendlyName));
                };
                appDomain.ProcessExit += delegate(object sender, EventArgs e)
                {
                    Trace.WriteLine(string.Format("[{0}] ProcessExit", (sender as AppDomain).FriendlyName ));
                };
            }
            return appDomain;
        }
        private string ValidateAppDomainName(string appDomainName)
        {
            if (string.IsNullOrEmpty(appDomainName) || appDomainName == "*" || appDomainName.ToLower() == "default")
            {
                appDomainName = AppDomain.CurrentDomain.FriendlyName;
            }
            return appDomainName;
        }
        #endregion

        #region IsDomainExists
        public bool IsDomainExists(string appDomainName)
        {
            try
            {
                _rwl.AcquireWriterLock(TimeSpan.FromSeconds(60));
                appDomainName = ValidateAppDomainName(appDomainName);
                AppDomain appDomain = _appDomains.Find(delegate(AppDomain ad) { return ad.FriendlyName == appDomainName; });
                return appDomain != null;
            }
            finally
            {
                _rwl.ReleaseWriterLock();
            }

        }
        #endregion

        #region GetAppDomainHost
        public AppDomain this[string appDomainName]
        {
            get
            {
                try
                {
                    _rwl.AcquireWriterLock(TimeSpan.FromSeconds(60));
                    appDomainName = ValidateAppDomainName(appDomainName);
                    AppDomain appDomain = _appDomains.Find(delegate(AppDomain ad) { return ad.FriendlyName == appDomainName; });
                    if (appDomain != null)
                    {
                        return appDomain;
                    }
                    else
                    {
                        throw new InvalidOperationException(string.Format("Requested appDomain '{0}' doesn't exist in the catalog", appDomainName));
                    }
                }
                finally
                {
                    _rwl.ReleaseWriterLock();
                }
            }
        }
        #endregion

        #region GetHostedServices
        public List<ServiceHostActivatorStatus> GetHostedServices
        {
            get
            {
                List<ServiceHostActivatorStatus> lists = new List<ServiceHostActivatorStatus>();
                try
                {
                    _rwl.AcquireReaderLock(TimeSpan.FromSeconds(60));
                    foreach (AppDomain appDomain in _appDomains)
                    {
                        List<MqttClientActivator> activators = appDomain.GetData(_key) as List<MqttClientActivator>;
                        if (activators != null)
                        {
                            foreach (MqttClientActivator activator in activators)
                            {
                                lists.Add(new ServiceHostActivatorStatus 
                                { 
                                    Created=activator.Created,
                                    AppDomainHostName = activator.AppDomainHost.FriendlyName, 
                                    //State=activator.State,
                                    Name=activator.Name,
                                    //SubscriptionAddress = activator.
                                     
                                });
                            }
                        }
                    }
                }
                finally
                {
                    _rwl.ReleaseReaderLock();
                }
                return lists;
            }
        }
        #endregion

        #region GetHostedService
        public MqttClientActivator GetClient(string name, bool flagRemove = false)
        {
            MqttClientActivator client = null;
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Not valid service name");

            try
            {
                _rwl.AcquireReaderLock(TimeSpan.FromSeconds(60));
                foreach (AppDomain appDomain in _appDomains)
                {
                    List<MqttClientActivator> activators = appDomain.GetData(_key) as List<MqttClientActivator>;
                    if (activators != null)
                    {
                        MqttClientActivator activator = activators.Find(delegate(MqttClientActivator a) { return a.Name == name; });
                        if (activator != null)
                        {
                            client = activator;
                            if (flagRemove)
                            {
                                activators.Remove(activator);
                            }
                        }
                    }
                }
            }
            finally
            {
                _rwl.ReleaseReaderLock();
            }
            return client;

        }
        #endregion

        #region Open
        public int Open()
        {
            int count = 0;
            try
            {
                _rwl.AcquireWriterLock(TimeSpan.FromSeconds(60));
                foreach (AppDomain ad in _appDomains)
                {
                    List<MqttClientActivator> activators = ad.GetData(_key) as List<MqttClientActivator>;
                    if (activators != null)
                    {
                        activators.ForEach(delegate(MqttClientActivator activator)
                        {
                            activator.Connect();
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                count = _appDomains.Count;
                _rwl.ReleaseWriterLock();
            }
            return count;
        }
        public void Open(string appDomainName, string password = null)
        {
            try
            {
                _rwl.AcquireWriterLock(TimeSpan.FromSeconds(60));
                appDomainName = ValidateAppDomainName(appDomainName);
                AppDomain appDomain = _appDomains.Find(delegate(AppDomain ad) { return ad.FriendlyName == appDomainName; });
                if (appDomain != null)
                {
                    if (appDomain.IsDefaultAppDomain() == false)
                    {
                        List<MqttClientActivator> activators = appDomain.GetData(_key) as List<MqttClientActivator>;
                        if (activators != null)
                        {
                            activators.ForEach(delegate(MqttClientActivator activator)
                            {
                                Trace.WriteLine(string.Format("Open service '{0}'",  activator.Name));
                                activator.Connect(password);
                            });
                            Trace.WriteLine(string.Format("Open services in the AppDomain '{0}'", appDomainName));
                        }
                        else
                        {
                            Trace.WriteLine(string.Format("None services for opening in the AppDomain '{0}'", appDomainName));
                        }                    
                    }
                }
                else
                {
                    throw new InvalidOperationException(string.Format("Open '{0}' appDomain host failed - doesn't exist", appDomainName));
                }
            }
            finally
            {
                _rwl.ReleaseWriterLock();
            }
        }
        #endregion

        #region Close
        public void Close()
        {
            try
            {
                _rwl.AcquireWriterLock(TimeSpan.FromSeconds(60));
                _appDomains.Reverse();
                foreach (AppDomain ad in _appDomains)
                {
                    List<MqttClientActivator> activators = ad.GetData(_key) as List<MqttClientActivator>;
                    if (activators != null)
                    {
                        activators.Reverse();
                        activators.ForEach(delegate(MqttClientActivator activator)
                        {
                            activator.Disconnect();
                        });
                        activators.Clear();
                    }
                }

                int count = _appDomains.RemoveAll(delegate(AppDomain ad)
                {
                    if (!ad.IsDefaultAppDomain())
                    {
                        AppDomain.Unload(ad);
                    }
                    return true;
                });
                _appDomains.Clear();
            }
            finally
            {
                _rwl.ReleaseWriterLock();
            }
        }
        public void Close(string appDomainName)
        {
            this.Close(appDomainName, true);
        }
        public bool Close(string appDomainName, bool bThrow = false)
        {
            bool bHasBeenClosed = false;
            try
            {
                _rwl.AcquireWriterLock(TimeSpan.FromSeconds(60));

                appDomainName = ValidateAppDomainName(appDomainName);
                AppDomain appDomain = _appDomains.Find(delegate(AppDomain ad) { return ad.FriendlyName == appDomainName; });
                if (appDomain != null)
                {
                    if (appDomain.IsDefaultAppDomain() == false)
                    {

                        List<MqttClientActivator> activators = appDomain.GetData(_key) as List<MqttClientActivator>;                        
                        if (activators != null)
                        {
                            activators.Reverse();
                            activators.ForEach(delegate(MqttClientActivator activator)
                            {
                                activator.Disconnect();
                            });
                            activators.Clear();
                        }

                        // clean-up                        
                        _appDomains.Remove(appDomain);
                        AppDomain.Unload(appDomain);
                        bHasBeenClosed = true;
                    }
                    else if (bThrow)
                    {
                        throw new InvalidOperationException("The Close operation can't be processed on the default appDomain");
                    }
                }
            }
            finally
            {
                _rwl.ReleaseWriterLock();
            }
            return bHasBeenClosed;
        }
        #endregion

        #region Abort
        public bool Abort(string appDomainName, bool bThrow)
        {
            bool bHasBeenAborted = false;
            try
            {
                _rwl.AcquireWriterLock(TimeSpan.FromSeconds(60));

                appDomainName = ValidateAppDomainName(appDomainName);
                AppDomain appDomain = _appDomains.Find(delegate(AppDomain ad) { return ad.FriendlyName == appDomainName; });

                if (appDomain != null)
                {
                    if (appDomain.IsDefaultAppDomain() == false)
                    {
                        List<MqttClientActivator> activators = appDomain.GetData(_key) as List<MqttClientActivator>;
                        activators.Reverse();
                        if (activators != null)
                        {
                            activators.ForEach(delegate(MqttClientActivator activator)
                            {
                                activator.Abort();
                            });
                        }

                        // clean-up
                        activators.Clear();
                        _appDomains.Remove(appDomain);
                        AppDomain.Unload(appDomain);
                        bHasBeenAborted = true;
                    }
                    else if (bThrow)
                    {
                        throw new InvalidOperationException("The Abort operation can't be processed on the default appDomain");
                    }
                }
            }
            finally
            {
                _rwl.ReleaseWriterLock();
            }
            return bHasBeenAborted;
        }
        #endregion

        #region Current
        public static HostServices Current
        {
            get
            {
                string key = typeof(HostServices).FullName;
                HostServices hostservices = AppDomain.CurrentDomain.GetData(key) as HostServices;
                if (hostservices == null)
                {
                    lock (AppDomain.CurrentDomain.FriendlyName)
                    {
                        hostservices = AppDomain.CurrentDomain.GetData(key) as HostServices;
                        if (hostservices == null)
                        {
                            hostservices = new HostServices(true);
                            AppDomain.CurrentDomain.SetData(key, hostservices);
                        }
                    }
                }
                return hostservices;
            }
        }
        #endregion
    }
    #endregion

    public class MqttMsgEventArgs
    {
        public MqttMsgEventArgs()
        {
        }
        public MqttMsgEventArgs(string topic, byte[] message, bool dupFlag, byte qosLevel, bool retain)
        {
            Timestamp = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
            Topic = topic;
            Message = message;
            DupFlag = dupFlag;
            QosLevel = qosLevel;
            Retain = retain;
        }
        public MqttMsgEventArgs(MqttMsgPublishEventArgs args) : this(args.Topic, args.Message, args.DupFlag, args.QosLevel, args.Retain) { }
      
        public string Timestamp { get; set; }
        public bool DupFlag { get; set; }
        public byte[] Message { get; set; }
        public byte QosLevel { get; set; }
        public bool Retain { get; set; }
        public string Topic { get; set; }

    }
}

