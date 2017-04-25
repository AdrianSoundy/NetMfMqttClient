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
using Newtonsoft.Json.Linq;
using RKiss.Tools.AzureIoTHubTester.Dialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;
#endregion

namespace RKiss.Tools.AzureIoTHubTester
{
    public partial class Form1 : Form, IExtension<ServiceHostBase>
    {
        string CurrentProjectFilePath = string.Empty;
        private int rid = 0;
        private uint ttlInHrs = 0;
        TreeNode IoTHubsNode = null;

        TesterSettings Settings = new TesterSettings();

        #region Constructor
        public Form1()
        {
            InitializeComponent();

            this.webBrowserAzureIoTHub.Dock = DockStyle.Fill;
            this.splitContainerEntities.FixedPanel = FixedPanel.Panel1;
            this.splitContainerMain.FixedPanel = FixedPanel.Panel2;
            this.message.Dock = DockStyle.Fill;
            this.publisher.Dock = DockStyle.Fill;
            this.devicesView.Dock = DockStyle.Fill;
            this.restClient.Dock = DockStyle.Fill;
            this.publisher.RaisePublishButton += Publisher1_RaisePublishButton;
            this.publisher.Visible = false;
            this.publisher.EnablePublish = false;
            this.restClient.Visible = false;
            this.restClient.RaiseSendButton += RestClient_RaiseSendButton;
            this.pictureBoxAZureIoTHub.Visible = false;
            this.pictureBoxAZureIoTHub.Dock = DockStyle.Fill;

            // root node
            IoTHubsNode = this.treeViewIoTHubs.Nodes[NodeDefaults.IoTHubs];

            // TimeToLive in Hours
            ttlInHrs = UInt32.TryParse(ConfigurationManager.AppSettings["timeToLiveInHrs"], out ttlInHrs) ? ttlInHrs : 24;
        }

      
        #endregion

        #region Form
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Settings.Reload();
            this.ShowNodes();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Settings.Save();
            HostServices.Current.Close();
        }
        #endregion

        #region ToolStrip
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AzureIoTHubTesterAboutBox().ShowDialog();
        }
        #endregion

        #region IExtension<ServiceHostBase> Members
        public void Attach(ServiceHostBase owner) { }
        public void Detach(ServiceHostBase owner) { }
        #endregion

        #region Helpers
        private void ShowNodes()
        {
            if (Settings.IoTHubNamespaces != null && Settings.IoTHubNamespaces.Count > 0 && this.IoTHubsNode.Nodes.Count == 0)
            {
                this.IoTHubsNode.ExpandAll();
            }
            else if (Settings.IoTHubNamespaces != null && Settings.IoTHubNamespaces.Count == 0)
            {
                //this.IoTHubNode.Nodes.Clear();
            }
        }
       
        public void ShowText(string text)
        {
            //this.richTextBoxMessage.Text = text;
        }
        #endregion

        #region Insert Message
        public void AddMessageToTreview(MqttMsgEventArgs payload, ConfigData config)
        {
            try
            {
                string brokerHostName = config.BrokerAddress.Split('.').FirstOrDefault();
                string deviceId = config.Name;


                this.InvokeEx(delegate ()
                {
                    Color color = Color.Black;
                    TreeNode node = null;
                    var iotHubNode = this.IoTHubsNode.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == brokerHostName);
                    var deviceNode = iotHubNode.Nodes[NodeDefaults.Devices].Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == deviceId);

                    if (payload.Topic.StartsWith("$iothub/twin/"))
                    {
                        if (payload.Topic.IndexOf("res/400/") > 0 || payload.Topic.IndexOf("res/404/") > 0 || payload.Topic.IndexOf("res/500/") > 0)
                            color = Color.Red;
                        node = deviceNode.Nodes[NodeDefaults.Twin];
                    }
                    else if (payload.Topic.StartsWith("$iothub/methods/POST/"))
                    {
                        node = deviceNode.Nodes[NodeDefaults.Methods];
                    }
                    else if (payload.Topic.StartsWith($"devices/{deviceId}/messages/devicebound/"))
                    {
                        node = deviceNode.Nodes[NodeDefaults.Messages];
                    }
                    else if (payload.Topic.StartsWith("$iothub/clientproxy/"))
                    {
                        deviceNode.ImageIndex = 5;
                        deviceNode.SelectedImageIndex = 5;
                        deviceNode.ToolTipText = "Device has been disconnected";
                        if (this.treeViewIoTHubs.SelectedNode == deviceNode)
                            this.publisher.Enabled = false;
                    }
                    else if (payload.Topic.StartsWith("$iothub/logmessage/Info"))
                    {
                        this.richTextBoxLog.AppendText(Encoding.UTF8.GetString(payload.Message) + "\n", Color.Gray);
                    }
                    else if (payload.Topic.StartsWith("$iothub/logmessage/HighlightInfo"))
                    {
                        this.richTextBoxLog.AppendText(Encoding.UTF8.GetString(payload.Message) + "\n", Color.Black);
                    }
                    else if (payload.Topic.StartsWith("$iothub/logmessage/Error"))
                    {
                        this.richTextBoxLog.AppendText(Encoding.UTF8.GetString(payload.Message) + "\n", Color.Red);
                    }
                    else if (payload.Topic.StartsWith("$iothub/logmessage/Warning"))
                    {
                        this.richTextBoxLog.AppendText(Encoding.UTF8.GetString(payload.Message) + "\n", Color.Magenta);
                    }

                    
                    if (node != null)
                    {
                        var messageNode = new TreeNode()
                        {
                            ImageIndex = 4,
                            SelectedImageIndex = 4,
                            Name = "Message",
                            Text = payload.Topic,
                            Tag = new NodeState() { Config = config, Payload = "{\r\n\r\n}", Tag = payload },
                            ForeColor = color
                        };
                        node.Nodes.Add(messageNode);
                        if (node.Nodes.Count == 1)
                            node.ExpandAll();
                    }
                });
            }
            catch (Exception ex)
            {
                Trace.WriteLine("AddMessageToTreview failed, reason: " + ex.Message);
            }
        }

        public void UpdateStatusForMessageNode(string messageId, string status, ConfigData config)
        {
            try
            {
                this.InvokeEx(delegate ()
                {

                });
            }
            catch (Exception ex)
            {
                Trace.WriteLine("UpdateStatusForMessageNode failed, reason: " + ex.Message);
            }
        }
        #endregion

        #region Node Clicks
        private void treeViewIoTHubs_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = this.treeViewIoTHubs.GetNodeAt(e.Location);
            if (node == null)
                return;
           
            if (e.Button == MouseButtons.Right)
            {
                this.treeViewIoTHubs.SelectedNode = node;

                if (node.Name == NodeDefaults.IoTHubs)
                {
                    this.contextMenuStripIoTHubs.Show(this.treeViewIoTHubs, e.Location);
                }
                else if (node.Name == NodeDefaults.IoTHub)
                {
                    this.contextMenuStripIoTHub.Show(this.treeViewIoTHubs, e.Location);
                }
                else if (node.Name == NodeDefaults.Devices)
                {
                    this.contextMenuStripDevices.Show(this.treeViewIoTHubs, e.Location);
                }
                else if (node.Name == "Message")
                {
                    this.contextMenuStripMessage.Show(this.treeViewIoTHubs, e.Location);
                }
                else if (node.Name.StartsWith("Device/"))
                {
                    this.clearToolStripMenuItem.Visible = node.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Nodes.Count > 0) != null;                  
                    this.reconnectToolStripMenuItemDevice.Visible = node.SelectedImageIndex == 5;
                    this.reconnectToolStripMenuItemDevice.Enabled = node.ToolTipText != "Re-connection failed";
                    this.stopSamplingToolStripMenuItemDevice.Visible = node.ToolTipText == "Device is sampling";
                    this.contextMenuStripDevice.Show(this.treeViewIoTHubs, e.Location);
                    if (node.SelectedImageIndex == 5)
                        this.publisher.EnablePublish = false;
                }
                else if (node.Name == NodeDefaults.Twin || node.Name == NodeDefaults.Methods || node.Name == NodeDefaults.Messages)
                {
                    this.getToolStripMenuItem.Visible = node.Name == NodeDefaults.Twin;
                    this.toolStripMenuItemClearAllMessages.Visible = node.Nodes.Count > 0;
                    if (node.Parent.SelectedImageIndex != 5)
                        this.contextMenuStripSubscribers.Show(this.treeViewIoTHubs, e.Location);
                    if (node.Parent.SelectedImageIndex == 5 && this.publisher.EnablePublish)
                        this.publisher.EnablePublish = false;
                }
                else if (node.Name == NodeDefaults.RESTAPI)
                {
                    this.contextMenuStripRESTAPI.Show(this.treeViewIoTHubs, e.Location);
                }
            }
        }

        private void treeViewIoTHubs_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null)
                return;

            TreeNode node = this.treeViewIoTHubs.SelectedNode;
            if (node != null && node.Name.StartsWith("Publisher/"))
            {
                e.Node.BeginEdit();
            }
            else if (node != null && node.Name.StartsWith("Message"))
            {
                ConfigData config = node.Parent.Tag as ConfigData;
                if (config != null && node.ImageIndex == 23 && (config.Action == null || config.Action != "None"))
                {
                    #region Run Action

                    #endregion
                }
            }
        }
        #endregion

        #region Message
        private void toolStripMenuItemRemoveMessage_Click(object sender, EventArgs e)
        {
            try
            {
                var node = this.treeViewIoTHubs.SelectedNode;
                if (node.Name == "Message")
                {
                    node.Remove();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerMessage(), "Remove Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode node = this.treeViewIoTHubs.SelectedNode;
                if (node != null)
                {
                    if (node.Name.StartsWith("Publisher/") || node.Name.StartsWith("Sender/"))
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerMessage(), "Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode node = this.treeViewIoTHubs.SelectedNode;
                if (node != null)
                {
                    if (node.Name.StartsWith("Publisher/") || node.Name.StartsWith("Sender/"))
                    {





                        // File.WriteAllText(config.FileName, xmlmessage);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerMessage(), "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode node = this.treeViewIoTHubs.SelectedNode;
                if (node != null)
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerMessage(), "Save As", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = this.treeViewIoTHubs.SelectedNode;
            if (node != null)
            {
                if (node.Name.StartsWith("Publisher/") || node.Name.StartsWith("Sender/"))
                {
                    var package = Clipboard.GetDataObject().GetData(DataFormats.Serializable) as Dictionary<string, object>;
                    if (package != null)
                    {

                    }
                }
            }
        }
        #endregion

        #region Subscriber
        private void toolStripMenuItemCopyMessage_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode node = this.treeViewIoTHubs.SelectedNode;
                if (node.Name.StartsWith("Message"))
                {
                    Clipboard.SetDataObject(node.Tag, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerMessage(), "Copy Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Connect
        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripMenuItemIoTHubConnect_Click(sender, e);
        }

        private void toolStripMenuItemIoTHubConnect_Click(object sender, EventArgs e)
        {
            string connectionString = null;  
            
            //"HostName=rk2016-IoT.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=/MblOtAsZHTnRm6L/NjkfL3lClZGdL7RuDz58zxHecs=";
            //
            //string iothubNamespace = SharedAccessSignatureBuilder.GetHostNameNamespaceFromConnectionString(connectionString);
            //string sas = SharedAccessSignatureBuilder.GetSASTokenFromConnectionString(connectionString);
            //string address = $"https://{iothubNamespace}.azure-devices.net/devices?top=100&api-version=2016-11-14";

            //string sas = "SharedAccessSignature sr=rk2016-IoT.azure-devices.net&sig=bX3KH1sPH8tlxdXBAk0eXJ2mhnsyfK75j5xrqH5OV08%3d&se=1488408044&skn=iothubowner";
            //string sas = SharedAccessSignatureBuilder.GetSASToken($"{iothubNamespace}.azure-devices.net", "/MblOtAsZHTnRm6L/NjkfL3lClZGdL7RuDz58zxHecs=", "iothubowner");

            TreeNode selNode = this.treeViewIoTHubs.SelectedNode;
            var listOfConnectedNamespaces = selNode.Nodes.Cast<TreeNode>().Select(n => n.Text);

            var dialog = new NamespaceDialog(this.Settings.IoTHubNamespaces);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.Settings.IoTHubNamespaces = dialog.IoTHubNamespaces;
                connectionString = dialog.SelectedNamespace;
            }
            else
            {
                return;
            }

            try
            {               
                string iothubNamespace = SharedAccessSignatureBuilder.GetHostNameNamespaceFromConnectionString(connectionString);
                if (listOfConnectedNamespaces.Contains(iothubNamespace))
                    throw new Exception($"Selected namespace '{iothubNamespace}' is already connected.");

                string devices = ServiceHelper.GetDevicesAsync(connectionString).Result;

                var node = new TreeNode()
                {
                    ImageIndex = 16,
                    SelectedImageIndex = 16,
                    Name = NodeDefaults.IoTHub,
                    Text = iothubNamespace,
                    Tag = connectionString,
                };
                var node2 = new TreeNode()
                {
                    ImageIndex = 32,
                    SelectedImageIndex = 32,
                    Name = NodeDefaults.RESTAPI,
                    Text = NodeDefaults.RESTAPI,
                    Tag = "{\"method\":\"GET\",\"url\":\"/devices?top=100&api-version=2016-11-14\"}",
                    //Tag = SharedAccessSignatureBuilder.GetSASTokenFromConnectionString(connectionString),
                    StateImageKey = SharedAccessSignatureBuilder.GetSASTokenFromConnectionString(connectionString, this.ttlInHrs)
                };

                var node3 = new TreeNode()
                {
                    ImageIndex = 14,
                    SelectedImageIndex = 14,
                    Name = NodeDefaults.Devices,
                    Text = NodeDefaults.Devices,
                    Tag = JRaw.Parse(devices).ToString(Newtonsoft.Json.Formatting.Indented),
                };
               
                this.LoadRestApiFile(node2, AppDomain.CurrentDomain.BaseDirectory + $"/{iothubNamespace}.json");
                node.Nodes.Add(node2);
                node.Nodes.Add(node3);
                node2.Expand();
                node.Expand();
                
                this.IoTHubsNode.Nodes.Add(node);
                this.IoTHubsNode.Expand();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerMessage(), "Connect namespace", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripMenuItemDeviceConnect_Click(object sender, EventArgs e)
        {
            //string deviceId = "myFirstDevice";
            //string deviceKey = "gawEUX35+rXrR+FEii9/StF+WfSNFZGPT0ju7tLskxI=";
            //username = "rk2016-IoT.azure-devices.net/myDevice/api-version=2016-11-14";
            //password = "SharedAccessSignature sr=rk2016-IoT.azure-devices.net%2Fdevices%2FmyDevice&sig=uc326g7WDDYaEutGWxwuVU1FUW3Nllb5oM6ldI1cJVA%3D&se=1513709835";
            //deviceId = "myDevice";
            //string password = "SharedAccessSignature sr=rk2016-IoT.azure-devices.net%2Fdevices%2FmyFirstDevice&sig=X1YNVJJb%2Bt1TGC2bnXcrVYKQgid1LiXnIoVPS2UtKBA%3D&se=1511717060";

            TreeNode selNode = this.treeViewIoTHubs.SelectedNode;

            string deviceId = this.devicesView.SelectedDevice;
            string deviceKey = null;

            // collect all connected devices
            var listOfConnectedDevices = selNode.Nodes.Cast<TreeNode>().Select(n => n.Text);

            var deviceInfo = new[] { new { deviceId = "", authentication = new { symmetricKey = new { primaryKey = "" } } } };
            var listOfRegisteredDevices = JsonConvert.DeserializeAnonymousType((string)selNode.Tag, deviceInfo);

            try
            {
                if (selNode.Name != "Devices")
                    throw new Exception("Fatal error, wrong selected Node, must be 'Devices'");

                if(listOfRegisteredDevices.Count() == 0)
                    throw new Exception("There is no registered device in the IoTHub");

                // assign devicedId/deviceKey
                if (string.IsNullOrEmpty(deviceId))
                {
                    var query = listOfRegisteredDevices.FirstOrDefault(d => listOfConnectedDevices.Contains(d.deviceId) == false);
                    if (query == null)
                        throw new Exception("All devices are connected");
                    deviceId = query.deviceId;
                    deviceKey = query.authentication.symmetricKey.primaryKey;
                }
                else
                {
                    var query = listOfRegisteredDevices.FirstOrDefault(d => d.deviceId == deviceId);
                    if (query == null)
                        throw new Exception($"Selected device '{deviceId}' is not registered in the Azure IoT Hub.");
                    deviceId = query.deviceId;
                    deviceKey = query.authentication.symmetricKey.primaryKey;
                }

                if (listOfConnectedDevices.Contains(deviceId))
                    throw new Exception($"Selected device '{deviceId}' is already connected.");

                  
                int port = 8883;
                string iothubNamespace = selNode.Parent.Text;
                string address = $"{iothubNamespace}.azure-devices.net";
                string username = $"{address}/{deviceId}/api-version=2016-11-14";
                string password = SharedAccessSignatureBuilder.GetSASToken($"{address}/devices/{deviceId}", deviceKey, null, this.ttlInHrs);
                string testerAddress = "net.pipe://localhost/AzureIoTHubTester_" + Process.GetCurrentProcess().Id;

                string id = Guid.NewGuid().ToString();
                ConfigData config = new ConfigData() { Name = deviceId, Id = id, Username = username, Password = password, BrokerPort = port, BrokerAddress = address, TesterAddress = testerAddress };

                string appDomainName = config.Name + "/" + config.Id;
                config.HostName = appDomainName;

                // double check
                if (listOfConnectedDevices.Contains(deviceId))
                    throw new Exception($"Device {deviceId} is already connected");

                // sample
                //string telemetrySample = Templates.TelemetrySample.Replace("_time_", DateTime.UtcNow.ToString()).Replace("_deviceId_", deviceId);
                //string telemetrySample = this.UpdateValues(deviceId, Interlocked.Add(ref rid, 0), Templates.TelemetrySample);
                string telemetrySample = Templates.TelemetrySample;

                ThreadPool.QueueUserWorkItem(delegate (object state)
                {
                    try
                    {
                        using (var progress = new ProgressNode(this, selNode, 6, 14))
                        {
                            HostServices.Current.Add(appDomainName, config);
                            HostServices.Current.Open(appDomainName);

                            this.InvokeEx(delegate ()
                            {
                                var node = new TreeNode()
                                {
                                    ImageIndex = 3,
                                    SelectedImageIndex = 3,
                                    Name = "Device/" + id,
                                    Text = deviceId,
                                    StateImageKey = deviceKey,
                                    Tag = new NodeState() { Config = config, Payload = telemetrySample, Topic = $"devices/{deviceId}/messages/events/" }
                                    };

                                node.Nodes.Add(new TreeNode("twin", 25, 25) { Name = "twin", Tag = new NodeState() { Config = config, Payload = "" } });
                                node.Nodes.Add(new TreeNode("methods", 26, 26) { Name = "methods", Tag = new NodeState() { Config = config, Payload = "" } });
                                node.Nodes.Add(new TreeNode("messages", 2, 2) { Name = "messages", Tag = new NodeState() { Config = config, Payload = "" } });
                                node.ExpandAll();

                                this.publisher.Enabled = true;
                                selNode.Nodes.Add(node);
                                this.treeViewIoTHubs.SelectedNode = node;
                                selNode.Expand();
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        HostServices.Current.Close(appDomainName);
                        MessageBox.Show(ex.InnerMessage(), $"Connect device '{deviceId}'", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerMessage(), "Connect device", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Events
        private void treeViewIoTHubs_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = false;
            TreeNode selNode = this.treeViewIoTHubs.SelectedNode;
            if (selNode != null)
            {
                if (selNode.Name.StartsWith("Device/") || selNode.Name == NodeDefaults.Twin || selNode.Name == NodeDefaults.Message)
                {
                    (selNode.Tag as NodeState).Payload = this.publisher.Payload;
                    (selNode.Tag as NodeState).Topic = this.publisher.Topic;

                }
                else if (selNode.Name == NodeDefaults.HttpRequest || selNode.Name == NodeDefaults.RESTAPI)
                {
                    selNode.Tag = this.restClient.UpdateState(Convert.ToString(selNode.Tag));
                }
             }
        }

        private void treeViewIoTHubs_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selNode = this.treeViewIoTHubs.SelectedNode;
            if (selNode != null)
            {
                //make visible user controll
                this.publisher.Visible = selNode.Name.StartsWith("Device/") || selNode.Name == NodeDefaults.Twin || (selNode.Name == NodeDefaults.Message && selNode.Parent != null && selNode.Parent.Name != NodeDefaults.Messages);
                this.message.Visible = (selNode.Name == NodeDefaults.Message && selNode.Parent != null && selNode.Parent.Name == NodeDefaults.Messages);
                //this.richTextBoxMessage.Visible = selNode.Name == NodeDefaults.IoTHubs || selNode.Name == NodeDefaults.Methods || selNode.Name == NodeDefaults.Messages;
                this.devicesView.Visible = selNode.Name == NodeDefaults.Devices;
                this.pictureBoxAZureIoTHub.Visible = selNode.Name == NodeDefaults.IoTHubs;
                this.restClient.Visible = selNode.Name == NodeDefaults.RESTAPI || selNode.Name == NodeDefaults.HttpRequest;
                this.webBrowserAzureIoTHub.Visible = selNode.Name == NodeDefaults.IoTHub;

                if (selNode.Name.StartsWith("Message"))
                {
                    this.publisher.EnablePublish = selNode.Parent.Parent.ImageIndex != 5;
                    this.publisher.Payload = (selNode.Tag as NodeState).Payload;

                    if (selNode.Parent != null && selNode.Parent.Name == NodeDefaults.Twin)
                    {
                        this.publisher.Title1 = "Twin Subscriber";
                        this.publisher.Title2 = "Publisher";
                        this.publisher.Description = "Update device twin reported";
                        this.publisher.Topic = $"$iothub/twin/PATCH/properties/reported/?$rid={this.rid}";
                        this.publisher.QoS = 1;
                        if (this.publisher.Payload.Trim() == string.Empty)
                            this.publisher.Payload = "{\r\n\r\n}";
                    }
                    else if (selNode.Parent != null && selNode.Parent.Name == NodeDefaults.Methods)
                    {
                        var topicParts = selNode.Text.Split(new char[] { '=' }, 2);
                        this.publisher.Title1 = "Method Subscriber";
                        this.publisher.Title2 = "Publisher";
                        this.publisher.Description = "Send response for method";
                        this.publisher.Topic = $"$iothub/methods/res/200/?$rid={topicParts.LastOrDefault()}";
                        this.publisher.QoS = 1;
                        this.publisher.EnablePublish = selNode.ImageIndex == 4;
                    }
                    else if (selNode.Parent != null && selNode.Parent.Name == NodeDefaults.Messages)
                    {
                        this.message.Title = "C2D Subscriber";
                        this.publisher.EnablePublish = false;
                    }

                    var payload = (selNode.Tag as NodeState).Tag as MqttMsgEventArgs;
                    if (payload != null)
                    {
                        dynamic body = null;

                        try
                        {
                            body = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(payload.Message));
                        }
                        catch
                        {
                            body = Encoding.UTF8.GetString(payload.Message);
                        }
                        var msg = new { Topic = payload.Topic, Timestamp = payload.Timestamp, DupFlag = payload.DupFlag, QosLevel = payload.QosLevel, Retain = payload.Retain };
                        string header = JsonConvert.SerializeObject(msg, Newtonsoft.Json.Formatting.Indented);

                        if (selNode.Parent.Name == NodeDefaults.Messages)
                        {
                            this.message.ClearMessagePanel();
                            this.message.AddText("MQTT:\r\n", Color.Gray);
                            this.message.AddText(header + "\r\n\r\n", Color.Gray);
                            this.message.AddText("Payload:\r\n", Color.Gray);
                            this.message.AddText(Convert.ToString(body), Color.Blue);
                        }
                        else
                        {
                            this.publisher.ClearMessagePanel();
                            this.publisher.AddText("MQTT:\r\n", Color.Gray);
                            this.publisher.AddText(header + "\r\n\r\n", Color.Gray);
                            this.publisher.AddText("Payload:\r\n", Color.Gray);
                            this.publisher.AddText(Convert.ToString(body), Color.Blue, false);
                        }
                    }
                }
                else if (selNode.Name == NodeDefaults.Twin)
                {
                    this.publisher.Title1 = "Twin Subscriber";
                    this.publisher.Title2 = "Publisher";
                    this.publisher.ClearMessagePanel();
                    this.publisher.Payload = (selNode.Tag as NodeState).Payload;
                    this.publisher.Description = "Get Device Twin";
                    this.publisher.EnablePublish = true;
                    this.publisher.Topic = $"$iothub/twin/GET/?$rid={this.rid}";
                    this.publisher.QoS = 1;

                }
                else if (selNode.Name.StartsWith("Device/"))
                {
                    this.publisher.Topic = (selNode.Tag as NodeState).Topic;
                    //this.publisher.Topic = $"devices/{selNode.Text}/messages/events/";
                    this.publisher.Title1 = "D2C Subscriber";
                    this.publisher.Title2 = "Publisher";
                    this.publisher.ClearMessagePanel();
                    this.publisher.Payload = (selNode.Tag as NodeState).Payload;
                    this.publisher.Description = "Fire D2C Message";
                    this.publisher.EnablePublish = string.IsNullOrEmpty(selNode.ToolTipText);
                    this.publisher.QoS = 0;
                }
                else if (selNode.Name == NodeDefaults.Devices)
                {
                    ThreadPool.QueueUserWorkItem(delegate (object state)
                    {
                        string connectionString = "";
                        string devices = "";
                        this.InvokeEx(delegate () { connectionString = (string)selNode.Parent.Tag; });
                        this.InvokeEx(delegate () { this.toolStripMenuItemDeviceConnect.Enabled = false; });
                        using (var progress = new ProgressNode(this, selNode, 6, 14))
                        {
                            devices = ServiceHelper.GetDevicesAsync(connectionString).Result;
                            selNode.Tag = devices;
                        }
                        this.InvokeEx(delegate () { this.devicesView.Update(devices); });
                        this.InvokeEx(delegate () { this.toolStripMenuItemDeviceConnect.Enabled = true; });
                    });
                    this.publisher.Description = string.Empty;
                    this.publisher.EnablePublish = false;
                }
                else if (selNode.Name == NodeDefaults.Messages || selNode.Name == NodeDefaults.Methods)
                {
                    this.publisher.ClearMessagePanel();
                    this.message.Clear();
                    this.publisher.Description = string.Empty;
                    this.publisher.EnablePublish = false;
                }
                else if (selNode.Name == NodeDefaults.RESTAPI)
                {
                    this.publisher.EnablePublish = false;
                    this.restClient.Visible = true;
                    this.restClient.SetState(Convert.ToString(selNode.Tag), selNode.Parent.Text, selNode.StateImageKey);
                    //this.restClient.SetState("{\"method\":\"GET\",\"url\":\"/devices?top=100&api-version=2016-11-14\"}", selNode.Parent.Text, Convert.ToString(selNode.Tag));
                }
                else if (selNode.Name == NodeDefaults.HttpRequest)
                {
                    this.publisher.EnablePublish = false;
                    this.restClient.Visible = true;
                    //this.restClient.SetState(Convert.ToString(selNode.Tag), selNode.Parent.Parent.Parent.Text, Convert.ToString(selNode.Parent.Parent.Tag));
                    this.restClient.SetState(Convert.ToString(selNode.Tag), selNode.Parent.Parent.Parent.Text, Convert.ToString(selNode.Parent.Parent.StateImageKey));
                }
                else
                {                 
                    this.publisher.ClearMessagePanel();
                    this.publisher.Description = string.Empty;
                    this.publisher.EnablePublish = false;
                }

                //disable publisher for unconnected device
                this.publisher.Enabled = !(selNode.Parent != null && selNode.Parent.Parent != null && (selNode.SelectedImageIndex == 5 || selNode.Parent.SelectedImageIndex == 5 || selNode.Parent.Parent.SelectedImageIndex == 5));
            }
        }
        #endregion

        #region Helpers
        private void DisconnectDevice_Worker(TreeNode selNode = null, int imageIndex = 5, bool bShowErrorBox = true)
        {
            if (selNode == null)
                selNode = this.treeViewIoTHubs.SelectedNode;

            if(selNode.Name.StartsWith("Device/") == false)
                return;

            ThreadPool.QueueUserWorkItem(delegate (object state)
            {
                try
                {
                    var config = (selNode.Tag as NodeState).Config;
                    using (var progressnode = new ProgressNode(this, selNode, 6, imageIndex))
                    {
                        HostServices.Current.Close(config.HostName);
                        config.IsConnected = false;
                    }

                    this.InvokeEx(() =>
                    {
                        this.treeViewIoTHubs.SelectedNode = selNode.Parent;
                        selNode.Remove();
                    });                   
                }
                catch (Exception ex)
                {
                    if(bShowErrorBox)
                        MessageBox.Show(ex.Message, "Disconnect device", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        private void DisconnectDevices_Worker(TreeNode selNode = null, int imageIndex = 5, bool bShowErrorBox = true)
        {
            if (selNode == null)
                selNode = this.treeViewIoTHubs.SelectedNode;

            if (selNode.Name != NodeDefaults.IoTHub)
                return;

            var devicesNode = selNode.Nodes[NodeDefaults.Devices];
            if (devicesNode.Nodes.Count == 0)
                return;
           
            ThreadPool.QueueUserWorkItem(delegate (object state)
            {
                try
                {
                    foreach (TreeNode node in devicesNode.Nodes)
                    {
                        this.DisconnectDevice_Worker(node, 5, false);
                    }
                }
                catch (Exception ex)
                {
                    if (bShowErrorBox)
                        MessageBox.Show(ex.Message, "Unconnect all devices", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        private void UpdateConfig(string namespaceAddress, List<IoTHubConnection> namespaces, ConfigDataBase config)
        {
            if (string.IsNullOrEmpty(namespaceAddress))
            {

            }
            else
            {
                string ns = new Uri(namespaceAddress).Host.Split('.')[0];
                var query = from c in namespaces where c.Namespace == ns select c;
                if (query == null || query.Count() == 0)
                    throw new Exception(string.Format("Missing connection for namespace = {0}", ns));
            }
        }

        private bool IsTesterConnected()
        {
            return this.IoTHubsNode.Nodes.Count > 0;
        }

        private bool IsTesterEmpty()
        {
            if (this.IoTHubsNode.Nodes.Count == 0)
                return true;
            //throw new Exception("Missing connection to the Service Bus");

            return true;
        }

        private void TesterWorker_RemoveAllEntities()
        {
            ThreadPool.QueueUserWorkItem(delegate (object state)
            {
                try
                {
                    this.InvokeEx(() => this.fileToolStripMenuItem.Enabled = false);
                    using (var progressnode = new ProgressNode(this, this.IoTHubsNode, 6, 0))
                    {
                        this.InvokeEx(() => this.publisher.ClearMessagePanel());

                        this.CurrentProjectFilePath = string.Empty;
                        //this.NodeDirty(this, new EventArgs());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerMessage(), "Close project - Removing all entities.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.InvokeEx(() => this.fileToolStripMenuItem.Enabled = true);
                }
            });
        }
        #endregion

        #region ToolTip
        private void treeViewIoTHubs_MouseMove(object sender, MouseEventArgs e)
        {
            TreeNode node = this.treeViewIoTHubs.GetNodeAt(e.Location);
            if (node != null && node.Name == "Message")
            {

            }
        }
        #endregion

        #region Drag&Drop
        private void treeViewIoTHubs_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var node = e.Item as TreeNode;
            if (node != null && node.Name != "Message")
                DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void treeViewIoTHubs_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void treeViewIoTHubs_DragDrop(object sender, DragEventArgs e)
        {
            Point targetPoint = this.treeViewIoTHubs.PointToClient(new Point(e.X, e.Y));
            var targetNode = this.treeViewIoTHubs.GetNodeAt(targetPoint);
            var draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            if (targetNode != null && draggedNode.Parent == targetNode.Parent && targetNode.Name != "Message" && !draggedNode.Equals(targetNode))
            {
                draggedNode.Remove();
                targetNode.Parent.Nodes.Insert(targetNode.Index, draggedNode);
                targetNode.Expand();
            }
        }
        #endregion      

        #region Buttons
        private void RestClient_RaiseSendButton(object sender, SendEventArgs e)
        {
            TreeNode selNode = this.treeViewIoTHubs.SelectedNode;
            if (selNode.Name == NodeDefaults.RESTAPI || selNode.Name == NodeDefaults.HttpRequest)
            {                
                ThreadPool.QueueUserWorkItem(delegate (object state)
                {
                    HttpResponseMessage response = null;
                    var watch = new Stopwatch();
                    watch.Start();
                    try
                    {
                        dynamic sas = selNode.Name == NodeDefaults.RESTAPI ? selNode.StateImageKey : selNode.Parent.Parent.StateImageKey;
                        if (e.Request.Headers.Contains("Authorization") == false && e.Request.RequestUri.Query.IndexOf("&sig=") < 0)
                            e.Request.Headers.TryAddWithoutValidation("Authorization", Convert.ToString(sas));

                        using (var progress = new ProgressNode(this, selNode, 6, selNode.ImageIndex))
                        {
                            this.InvokeEx(delegate () { this.restClient.Enabled = false; });
                            HttpClient client = new HttpClient();
                            response = client.SendAsync(e.Request).Result;
                            response.EnsureSuccessStatusCode();
                            watch.Stop();
                            this.InvokeEx(delegate ()
                            {                                                
                                string text = response.Content.ReadAsStringAsync().Result;
                                string label = response.StatusCode.ToString();
                                string status = response.ToString();
                                this.restClient.SetResponse(label, status, watch.ElapsedMilliseconds, text);

                                if(response.Headers.Contains("x-ms-continuation"))
                                {
                                    this.restClient.EnableNext(response.Headers.FirstOrDefault(h => h.Key == "x-ms-continuation").Value.FirstOrDefault());
                                }

                                // update tag
                                //if (selNode.Name == NodeDefaults.HttpRequest)
                                {
                                    dynamic jsonObject = JsonConvert.DeserializeObject(Convert.ToString(selNode.Tag));
                                    jsonObject._rspLabel = label;
                                    jsonObject._rspStatus = status;
                                    jsonObject._rspTime = watch.ElapsedMilliseconds;
                                    jsonObject._rspPayload = text;
                                    selNode.Tag = JsonConvert.SerializeObject(jsonObject);

                                    // assembly an url addreess for uploading a blob
                                    if(jsonObject.category == "UploadBlobFile" && jsonObject.name == "ReferenceUploadBlobFile")
                                    {
                                        var refUploadType = new { correlationId = "", hostName = "", containerName = "", blobName = "", sasToken = "" };
                                        dynamic refUpload = JsonConvert.DeserializeAnonymousType(text, refUploadType);
                                        if (string.IsNullOrEmpty(refUpload.sasToken) == false)
                                        {
                                            string urlUploadBlob = $"https://{refUpload.hostName}/{refUpload.containerName}/{refUpload.blobName}{refUpload.sasToken}";
                                            this.richTextBoxLog.AppendText($"{DateTime.Now.ToLocalTime().ToString()}: correlationId = {refUpload.correlationId}" + "\n", Color.Green);
                                            this.richTextBoxLog.AppendText($"{DateTime.Now.ToLocalTime().ToString()}: url = {urlUploadBlob}" + "\n", Color.Green);
                                        }
                                    }
                                }                              
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        if (watch.IsRunning)
                        {
                            watch.Stop();
                            this.InvokeEx(delegate () { this.restClient.LoadingTime = watch.ElapsedMilliseconds; });
                        }
                        if (response != null && !response.IsSuccessStatusCode)
                            this.InvokeEx(delegate () { this.restClient.ErrorStatus = response.StatusCode.ToString(); });
                        else
                            this.InvokeEx(delegate () { this.restClient.ErrorStatus = ex.InnerMessage(); });
                    }                   
                    this.InvokeEx(delegate () { this.restClient.Enabled = true; });
                });
            }
        }

        private void Publisher1_RaisePublishButton(object sender, PublishEventArgs e)
        {
            TreeNode selNode = this.treeViewIoTHubs.SelectedNode;
            string deviceId = this.GetDeviceId(selNode);
            bool isDeviceNode = selNode.Name.StartsWith("Device/");
            if (selNode.Name == NodeDefaults.Twin || selNode.Name == NodeDefaults.Message || isDeviceNode)
            {
                var config = (selNode.Tag as NodeState) != null ? (selNode.Tag as NodeState).Config : (selNode.Parent.Tag as NodeState).Config;              
                string name = config.BrokerAddress + "/" + config.Name;

                ThreadPool.QueueUserWorkItem(delegate (object state)
                {
                    try 
                    {
                        var client = HostServices.Current.GetClient(name);
                        if (client == null)
                            throw new Exception($"Internal Error: Unconnect device '{config.Name}'");

                        if (e.IsJson)
                        {
                            var token = JToken.Parse(e.Payload);
                            if (token is JArray)
                            {
                                if (isDeviceNode)
                                {
                                    this.InvokeEx(delegate () { selNode.ToolTipText = "Device is sampling"; });
                                    this.InvokeEx(delegate () { this.publisher.EnablePublish = false; });
                                    
                                    using (var progress = new ProgressNode(this, selNode, 6, 3))
                                    {
                                        var array = token as JArray;
                                        int downcounter = array.Count() == 1 ? e.Counter : array.Count();
                                        do
                                        {
                                            foreach (var item in array)
                                            {
                                                string jsontext = this.UpdateValues(deviceId, Interlocked.Increment(ref rid), (item as JObject).ToString(Newtonsoft.Json.Formatting.None));

                                                var code = client.Publish(e.Topic, jsontext, e.QoS, e.Retain);
                                                this.InvokeEx(delegate ()
                                                {
                                                    if (selNode.ToolTipText == "Device has been disconnected")
                                                    {
                                                        progress.EndImageIndex = 5;
                                                        throw new Exception($"[{selNode.Text}] - {selNode.ToolTipText}");
                                                    }
                                                    if (selNode.ToolTipText == "Device sampling has been stopped")
                                                    {
                                                        throw new Exception($"[{selNode.Text}] - {selNode.ToolTipText}");
                                                    }
                                                    var currentSelectedNode = this.treeViewIoTHubs.SelectedNode;
                                                    if (currentSelectedNode.Name.StartsWith("Device/") && selNode == currentSelectedNode)
                                                        this.publisher.AppendMessage($"\r\n [{downcounter}, {selNode.Text}, {DateTime.UtcNow}]:\r\t" + jsontext);
                                                });
                                                downcounter--;
                                                Thread.Sleep(e.PeriodTime);
                                            }
                                        }
                                        while (downcounter > 0);
                                    }
                                }
                                else
                                {
                                    var code = client.Publish(e.Topic, e.Payload, e.QoS, e.Retain);
                                }
                            }
                            else if (token is JObject)
                            {
                                string jsontext = this.UpdateValues(deviceId, Interlocked.Add(ref rid, 0), (token as JObject).ToString(Newtonsoft.Json.Formatting.None));

                                var code = client.Publish(e.Topic, jsontext, e.QoS, e.Retain);
                                if (isDeviceNode)
                                    this.InvokeEx(delegate () { this.publisher.AppendMessage($"\r\n[0, {selNode.Text}, {DateTime.UtcNow}]: " + jsontext); });
                            }                           
                        }
                        else
                        {
                            var code = client.Publish(e.Topic, e.Payload, e.QoS, e.Retain);
                            if (isDeviceNode)
                                this.InvokeEx(delegate () { this.publisher.AppendMessage($"\r\n[0, {selNode.Text}, {DateTime.UtcNow}]: " + e.Payload); });
                        }
                    }
                    catch (Exception ex)
                    {
                        this.InvokeEx(delegate ()
                        {
                            this.richTextBoxLog.AppendText($"{DateTime.Now.ToLocalTime().ToString()}: Erro/Warning at Publish on the topic - {ex.InnerMessage()}" + "\n", Color.Red);
                        });
                    }

                    Interlocked.Increment(ref rid);

                    this.InvokeEx(delegate ()
                    {                                            
                        if (selNode.Name == NodeDefaults.Message && selNode.Parent.Name == NodeDefaults.Methods)
                        {
                            selNode.ImageIndex = 24;
                            selNode.SelectedImageIndex = 24;
                            selNode.ForeColor = Color.Blue;
                            this.publisher.EnablePublish = false;
                        }
                        else if (selNode.Name.StartsWith("Device/"))
                        {
                            selNode.ToolTipText = "";
                            this.publisher.EnablePublish = true;
                        }
                    });
                });
            }
        }
        #endregion

        #region Clicks
        private void toolStripMenuItemDeviceUnconnect_Click(object sender, EventArgs e)
        {
            this.DisconnectDevice_Worker();
        }

        private void toolStripMenuItemClearAllMessages_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.treeViewIoTHubs.SelectedNode;
            if (selNode.Name == NodeDefaults.Twin || selNode.Name == NodeDefaults.Methods || selNode.Name == NodeDefaults.Messages)
            {
                selNode.Nodes.Clear();
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.treeViewIoTHubs.SelectedNode;
            if (selNode.Name.StartsWith("Device/"))
            {
                foreach (TreeNode node in selNode.Nodes)
                {
                    node.Nodes.Clear();
                }
            }
        }

        private void copySASToolStripMenuItemRESTAPI_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.treeViewIoTHubs.SelectedNode;
            if (selNode != null && selNode.Name == NodeDefaults.RESTAPI)
            {
                Clipboard.SetText(selNode.StateImageKey);
            }
        }

        private void refreshToolStripMenuItemDevices_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.treeViewIoTHubs.SelectedNode;
            if (selNode != null && selNode.Name == NodeDefaults.Devices)
            {
                string connectionString = (string)selNode.Parent.Tag;
     
                ThreadPool.QueueUserWorkItem(delegate (object state)
                {
                    try
                    {
                        this.InvokeEx(() => this.contextMenuStripDevices.Enabled = false);
                        using (var progressnode = new ProgressNode(this, selNode, 6, 14))
                        {
                            string devices = ServiceHelper.GetDevicesAsync(connectionString).Result;
                            this.InvokeEx(() =>
                            {                              
                                selNode.Tag = devices;
                                this.devicesView.Update(devices);
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerMessage(), "Refresh all registered devices", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.InvokeEx(() => this.contextMenuStripDevices.Enabled = true);
                    }
                });
            }
        }

        private void getToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.treeViewIoTHubs.SelectedNode;
            if (selNode != null && selNode.Name == NodeDefaults.Twin)
            {
                ThreadPool.QueueUserWorkItem(delegate (object state)
                {
                    try
                    {
                        this.InvokeEx(() => this.getToolStripMenuItem.Enabled = false);
                        using (var progressnode = new ProgressNode(this, selNode, 6, 0))
                        {
                            var config = (selNode.Tag as NodeState).Config;
                            string name = config.BrokerAddress + "/" + config.Name;
                            var client = HostServices.Current.GetClient(name);
                            if (client == null)
                                throw new Exception($"Internal Error: Unconnect device '{config.Name}'");
                            var code = client.Publish($"$iothub/twin/GET/?$rid={this.rid}", "", 1, false);
                        }
                        this.InvokeEx(() => selNode.ExpandAll());
                        Interlocked.Increment(ref rid);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerMessage(), "Get device twin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.InvokeEx(() => this.getToolStripMenuItem.Enabled = true);
                    }
                });
            }
        }

        private void reconnectToolStripMenuItemDevice_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.treeViewIoTHubs.SelectedNode;
            if (selNode != null && selNode.Name.StartsWith("Device/") && selNode.SelectedImageIndex == 5)
            {
                ConfigData config = (selNode.Tag as NodeState).Config;
                string appDomainName = config.HostName;
                string deviceId = config.Name;

                // recreate a new password valid for ttlInHrs from now
                string password = SharedAccessSignatureBuilder.GetSASToken($"{config.BrokerAddress}/devices/{deviceId}", selNode.StateImageKey, null, this.ttlInHrs);

                ThreadPool.QueueUserWorkItem(delegate (object state)
                {
                    this.InvokeEx(() => this.reconnectToolStripMenuItemDevice.Enabled = false);
                    try
                    {                       
                        using (var progress = new ProgressNode(this, selNode, 6, 3))
                        {                        
                            HostServices.Current.Open(appDomainName, password);                                                      
                        }
                        this.InvokeEx(() => this.reconnectToolStripMenuItemDevice.Enabled = true);
                        this.InvokeEx(() => selNode.ToolTipText = "");
                        this.InvokeEx(() => this.publisher.EnablePublish = true);
                        this.InvokeEx(() => this.publisher.Enabled = true);
                    }
                    catch (Exception ex)
                    {                    
                        this.InvokeEx(() => selNode.ToolTipText = "Re-connection failed");
                        this.InvokeEx(() => selNode.ForeColor = Color.Red);
                        MessageBox.Show(ex.InnerMessage("M2Mqtt.Exceptions", "Connection problem"), $"Unconnect device '{deviceId}'", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
            }                
        }

        private void stopSamplingToolStripMenuItemDevice_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.treeViewIoTHubs.SelectedNode;
            if (selNode != null && selNode.Name.StartsWith("Device/") && selNode.SelectedImageIndex != 3)
            {
                selNode.ToolTipText = "Device sampling has been stopped";
                this.stopSamplingToolStripMenuItemDevice.Visible = false;
            }
                
        }

        private void loadSamplesToolStripMenuItemDevice_Click(object sender, EventArgs e)
        {           
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Title = "Select a template file for device sampling",
                RestoreDirectory = true,
                InitialDirectory = @"C:\Templates\IoT",
                Multiselect = false,
                AddExtension = false,
                Filter = "Load file(*.json;*.url)|*.json|Insert files (*.url)|*.url"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string text = File.ReadAllText(dialog.FileName);
                    this.publisher.Payload = JRaw.Parse(text).ToString(Newtonsoft.Json.Formatting.Indented);                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerMessage(), Path.GetFileName(dialog.FileName));
                }
            }
        }

        private void toolStripMenuItemIoTHubUnconnect_Click(object sender, EventArgs e)
        {
            var selNode = this.treeViewIoTHubs.SelectedNode;
            if (selNode.Name != NodeDefaults.IoTHub)
                return;

            var devicesNode = selNode.Nodes[NodeDefaults.Devices];
            if (devicesNode.Nodes.Count == 0)
            {
                selNode.Remove();
                return;
            }

            if (MessageBox.Show("Are you sure to unconnect all devices from this IoTHub?", "Unconnect all devices", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.DisconnectDevices_Worker();
            }
        }

        private void LoadRestApiFile(TreeNode node, string filepath)
        {
            if (node.Name != NodeDefaults.RESTAPI)
                return;

            try
            {
                string jsontext = File.ReadAllText(filepath);
                var reqSchema = new { category = "", name = "", method = "", url = "", headers = "", payload = new JObject(), description = "", _rspLabel="", _rspStatus="", _rspTime=0L, _rspPayload="" };
                var obj = JsonConvert.DeserializeAnonymousType(jsontext, new[] { reqSchema });
                List<string> list = obj.Select(i => i.category).Distinct().ToList();

                node.Nodes.Clear();
                foreach (string category in list)
                {
                    var categoryNode = new TreeNode(category, 31, 31) { Name = NodeDefaults.CategoryRESTAPI, Tag = node.Tag };
                    var names = obj.Where(c => c.category == category).Select(n => n.name).ToList();
                    foreach (string name in names)
                    {
                        var request = obj.Where(r => r.category == category && r.name == name);
                        var jsonReq = JsonConvert.SerializeObject(request.FirstOrDefault());
                        categoryNode.Nodes.Add(new TreeNode(name, 19, 19) { Name = NodeDefaults.HttpRequest, Tag = jsonReq });
                    }
                    node.Nodes.Add(categoryNode);
                }
            }
            catch (Exception)
            {

            }
        }

        private string GetDeviceId(TreeNode node)
        {
            TreeNode walkingNode = node;
            while (walkingNode != null)
            {
                if (walkingNode.Name.StartsWith("Device/"))
                    return walkingNode.Text;
                else
                    walkingNode = walkingNode.Parent;
            }
            return string.Empty;            
        }

        private string UpdateValues(string deviceId, int counter, string jsontext)
        {
            Random rand = new Random();
            return jsontext.Replace("$DateTime.UtcNow", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture)).
                Replace("$DeviceID", deviceId).
                Replace("\"$Counter\"", counter.ToString()).
                Replace("\"$Sensor.WindSpeed\"", Math.Round(10 + rand.NextDouble() * 4 - 2, 4).ToString()).
                Replace("\"$Sensor.Temp\"", Math.Round(15 + rand.NextDouble() * 6 - 2, 2).ToString()).
                Replace("\"$Sensor.Humidity\"", Math.Round(80 + rand.NextDouble() * 4 - 2, 2).ToString());
        }

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBoxLog.Clear();
        }

        private void copyConnectionStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.treeViewIoTHubs.SelectedNode;
            if (selNode != null && selNode.Name.StartsWith("Device/"))
            {
                Clipboard.SetText($"HostName={selNode.Parent.Parent.Text}.azure-devices.net;DeviceId={selNode.Text};SharedAccessKey={selNode.StateImageKey}");
            }
        }

        private void copySASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = this.treeViewIoTHubs.SelectedNode;
            if (selNode != null && selNode.Name.StartsWith("Device/"))
            {
                Clipboard.SetText(SharedAccessSignatureBuilder.GetSASToken($"{selNode.Parent.Parent.Text}.azure-devices.net/devices/{selNode.Text}", selNode.StateImageKey, null, this.ttlInHrs));
            }
        }
        #endregion

    }
}

