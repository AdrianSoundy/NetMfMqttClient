namespace RKiss.Tools.AzureIoTHubTester
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("AzureIoTHubs");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainerEntities = new System.Windows.Forms.SplitContainer();
            this.groupBoxPanel1 = new System.Windows.Forms.GroupBox();
            this.treeViewIoTHubs = new System.Windows.Forms.TreeView();
            this.imageListIcons = new System.Windows.Forms.ImageList(this.components);
            this.webBrowserAzureIoTHub = new System.Windows.Forms.WebBrowser();
            this.pictureBoxAZureIoTHub = new System.Windows.Forms.PictureBox();
            this.restClient = new RKiss.Tools.AzureIoTHubTester.UserControls.RESTClient();
            this.message = new RKiss.Tools.AzureIoTHubTester.UserControls.Message();
            this.devicesView = new RKiss.Tools.AzureIoTHubTester.UserControls.Devices();
            this.publisher = new RKiss.Tools.AzureIoTHubTester.UserControls.Publisher();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToAzureIoTHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripDevices = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDeviceConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItemDevices = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripMessage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCopyMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStripIoTHubs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuIoTHubConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripDevice = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDeviceUnconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.reconnectToolStripMenuItemDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySASToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyConnectionStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.stopSamplingToolStripMenuItemDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSamplesToolStripMenuItemDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripSubscribers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.getToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemClearAllMessages = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripRESTAPI = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copySASToolStripMenuItemRESTAPI = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripIoTHub = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemIoTHubUnconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEntities)).BeginInit();
            this.splitContainerEntities.Panel1.SuspendLayout();
            this.splitContainerEntities.Panel2.SuspendLayout();
            this.splitContainerEntities.SuspendLayout();
            this.groupBoxPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAZureIoTHub)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStripDevices.SuspendLayout();
            this.contextMenuStripMessage.SuspendLayout();
            this.contextMenuStripIoTHubs.SuspendLayout();
            this.contextMenuStripDevice.SuspendLayout();
            this.contextMenuStripSubscribers.SuspendLayout();
            this.contextMenuStripRESTAPI.SuspendLayout();
            this.contextMenuStripIoTHub.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.groupBoxLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerEntities
            // 
            this.splitContainerEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEntities.Location = new System.Drawing.Point(0, 0);
            this.splitContainerEntities.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainerEntities.Name = "splitContainerEntities";
            // 
            // splitContainerEntities.Panel1
            // 
            this.splitContainerEntities.Panel1.Controls.Add(this.groupBoxPanel1);
            // 
            // splitContainerEntities.Panel2
            // 
            this.splitContainerEntities.Panel2.Controls.Add(this.webBrowserAzureIoTHub);
            this.splitContainerEntities.Panel2.Controls.Add(this.pictureBoxAZureIoTHub);
            this.splitContainerEntities.Panel2.Controls.Add(this.restClient);
            this.splitContainerEntities.Panel2.Controls.Add(this.message);
            this.splitContainerEntities.Panel2.Controls.Add(this.devicesView);
            this.splitContainerEntities.Panel2.Controls.Add(this.publisher);
            this.splitContainerEntities.Size = new System.Drawing.Size(1153, 777);
            this.splitContainerEntities.SplitterDistance = 394;
            this.splitContainerEntities.SplitterWidth = 6;
            this.splitContainerEntities.TabIndex = 0;
            // 
            // groupBoxPanel1
            // 
            this.groupBoxPanel1.Controls.Add(this.treeViewIoTHubs);
            this.groupBoxPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPanel1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBoxPanel1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBoxPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupBoxPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxPanel1.Name = "groupBoxPanel1";
            this.groupBoxPanel1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxPanel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBoxPanel1.Size = new System.Drawing.Size(394, 777);
            this.groupBoxPanel1.TabIndex = 0;
            this.groupBoxPanel1.TabStop = false;
            // 
            // treeViewIoTHubs
            // 
            this.treeViewIoTHubs.AllowDrop = true;
            this.treeViewIoTHubs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewIoTHubs.HideSelection = false;
            this.treeViewIoTHubs.ImageIndex = 0;
            this.treeViewIoTHubs.ImageList = this.imageListIcons;
            this.treeViewIoTHubs.Location = new System.Drawing.Point(3, 21);
            this.treeViewIoTHubs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeViewIoTHubs.Name = "treeViewIoTHubs";
            treeNode1.Name = "AzureIoTHubs";
            treeNode1.Text = "AzureIoTHubs";
            this.treeViewIoTHubs.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeViewIoTHubs.SelectedImageIndex = 0;
            this.treeViewIoTHubs.ShowNodeToolTips = true;
            this.treeViewIoTHubs.Size = new System.Drawing.Size(388, 754);
            this.treeViewIoTHubs.TabIndex = 0;
            this.treeViewIoTHubs.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeViewIoTHubs_ItemDrag);
            this.treeViewIoTHubs.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewIoTHubs_BeforeSelect);
            this.treeViewIoTHubs.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewIoTHubs_AfterSelect);
            this.treeViewIoTHubs.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewIoTHubs_NodeMouseClick);
            this.treeViewIoTHubs.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewIoTHubs_NodeMouseDoubleClick);
            this.treeViewIoTHubs.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeViewIoTHubs_DragDrop);
            this.treeViewIoTHubs.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeViewIoTHubs_DragEnter);
            this.treeViewIoTHubs.MouseMove += new System.Windows.Forms.MouseEventHandler(this.treeViewIoTHubs_MouseMove);
            // 
            // imageListIcons
            // 
            this.imageListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListIcons.ImageStream")));
            this.imageListIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListIcons.Images.SetKeyName(0, "Azure.ico");
            this.imageListIcons.Images.SetKeyName(1, "Queue.ico");
            this.imageListIcons.Images.SetKeyName(2, "Topic.ico");
            this.imageListIcons.Images.SetKeyName(3, "queue16.ico");
            this.imageListIcons.Images.SetKeyName(4, "message.ico");
            this.imageListIcons.Images.SetKeyName(5, "disableExec.bmp");
            this.imageListIcons.Images.SetKeyName(6, "Rotate1.ico");
            this.imageListIcons.Images.SetKeyName(7, "Rotate2.ico");
            this.imageListIcons.Images.SetKeyName(8, "Rotate3.ico");
            this.imageListIcons.Images.SetKeyName(9, "Rotate4.ico");
            this.imageListIcons.Images.SetKeyName(10, "Rotate5.ico");
            this.imageListIcons.Images.SetKeyName(11, "Rotate6.ico");
            this.imageListIcons.Images.SetKeyName(12, "Rotate7.ico");
            this.imageListIcons.Images.SetKeyName(13, "Rotate8.ico");
            this.imageListIcons.Images.SetKeyName(14, "service.ico");
            this.imageListIcons.Images.SetKeyName(15, "disableservice.bmp");
            this.imageListIcons.Images.SetKeyName(16, "cloud16.ico");
            this.imageListIcons.Images.SetKeyName(17, "Project.ico");
            this.imageListIcons.Images.SetKeyName(18, "BUSHEAD.ICO");
            this.imageListIcons.Images.SetKeyName(19, "rest.ico");
            this.imageListIcons.Images.SetKeyName(20, "warn.ico");
            this.imageListIcons.Images.SetKeyName(21, "callForwarding.ICO");
            this.imageListIcons.Images.SetKeyName(22, "callDone.ICO");
            this.imageListIcons.Images.SetKeyName(23, "callError.ICO");
            this.imageListIcons.Images.SetKeyName(24, "Response.ICO");
            this.imageListIcons.Images.SetKeyName(25, "Subscription.ico");
            this.imageListIcons.Images.SetKeyName(26, "Compute Emulator.ico");
            this.imageListIcons.Images.SetKeyName(27, "call.ICO");
            this.imageListIcons.Images.SetKeyName(28, "disableExec.bmp");
            this.imageListIcons.Images.SetKeyName(29, "folderHttpRequests.ico");
            this.imageListIcons.Images.SetKeyName(30, "cloudservice.ico");
            this.imageListIcons.Images.SetKeyName(31, "folder_web.ico");
            this.imageListIcons.Images.SetKeyName(32, "Web.ico");
            // 
            // webBrowserAzureIoTHub
            // 
            this.webBrowserAzureIoTHub.Location = new System.Drawing.Point(471, 38);
            this.webBrowserAzureIoTHub.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.webBrowserAzureIoTHub.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserAzureIoTHub.Name = "webBrowserAzureIoTHub";
            this.webBrowserAzureIoTHub.ScriptErrorsSuppressed = true;
            this.webBrowserAzureIoTHub.Size = new System.Drawing.Size(209, 78);
            this.webBrowserAzureIoTHub.TabIndex = 6;
            this.webBrowserAzureIoTHub.Url = new System.Uri(" https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-what-is-iot-hub ", System.UriKind.Absolute);
            this.webBrowserAzureIoTHub.Visible = false;
            // 
            // pictureBoxAZureIoTHub
            // 
            this.pictureBoxAZureIoTHub.BackColor = System.Drawing.Color.White;
            this.pictureBoxAZureIoTHub.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.AzureIotHub;
            this.pictureBoxAZureIoTHub.Location = new System.Drawing.Point(507, 548);
            this.pictureBoxAZureIoTHub.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxAZureIoTHub.Name = "pictureBoxAZureIoTHub";
            this.pictureBoxAZureIoTHub.Size = new System.Drawing.Size(100, 50);
            this.pictureBoxAZureIoTHub.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxAZureIoTHub.TabIndex = 5;
            this.pictureBoxAZureIoTHub.TabStop = false;
            this.pictureBoxAZureIoTHub.Visible = false;
            // 
            // restClient
            // 
            this.restClient.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.restClient.Location = new System.Drawing.Point(25, 548);
            this.restClient.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.restClient.Name = "restClient";
            this.restClient.Size = new System.Drawing.Size(413, 179);
            this.restClient.TabIndex = 4;
            this.restClient.Visible = false;
            // 
            // message
            // 
            this.message.Location = new System.Drawing.Point(25, 22);
            this.message.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(402, 76);
            this.message.TabIndex = 3;
            this.message.Visible = false;
            // 
            // devicesView
            // 
            this.devicesView.Location = new System.Drawing.Point(25, 420);
            this.devicesView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.devicesView.Name = "devicesView";
            this.devicesView.Size = new System.Drawing.Size(424, 66);
            this.devicesView.TabIndex = 2;
            this.devicesView.Visible = false;
            // 
            // publisher
            // 
            this.publisher.BackColor = System.Drawing.SystemColors.Control;
            this.publisher.Description = "Description";
            this.publisher.EnablePublish = true;
            this.publisher.Location = new System.Drawing.Point(25, 135);
            this.publisher.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.publisher.Name = "publisher";
            this.publisher.Payload = "";
            this.publisher.QoS = 0;
            this.publisher.Retain = false;
            this.publisher.Size = new System.Drawing.Size(388, 261);
            this.publisher.TabIndex = 1;
            this.publisher.Topic = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1153, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToAzureIoTHubToolStripMenuItem,
            this.clearLogToolStripMenuItem,
            this.toolStripSeparator6,
            this.exitToolStripMenuItemFile});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // connectToAzureIoTHubToolStripMenuItem
            // 
            this.connectToAzureIoTHubToolStripMenuItem.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.Azure;
            this.connectToAzureIoTHubToolStripMenuItem.Name = "connectToAzureIoTHubToolStripMenuItem";
            this.connectToAzureIoTHubToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
            this.connectToAzureIoTHubToolStripMenuItem.Text = "Connect";
            this.connectToAzureIoTHubToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // clearLogToolStripMenuItem
            // 
            this.clearLogToolStripMenuItem.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.deleteMessage;
            this.clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
            this.clearLogToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
            this.clearLogToolStripMenuItem.Text = "Clear Log";
            this.clearLogToolStripMenuItem.Click += new System.EventHandler(this.clearLogToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(208, 6);
            // 
            // exitToolStripMenuItemFile
            // 
            this.exitToolStripMenuItemFile.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.Delete;
            this.exitToolStripMenuItemFile.Name = "exitToolStripMenuItemFile";
            this.exitToolStripMenuItemFile.Size = new System.Drawing.Size(211, 30);
            this.exitToolStripMenuItemFile.Text = "Exit";
            this.exitToolStripMenuItemFile.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 29);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(61, 29);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.About;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(316, 30);
            this.aboutToolStripMenuItem.Text = "About Axure IoT Hub Tester";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // contextMenuStripDevices
            // 
            this.contextMenuStripDevices.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripDevices.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeviceConnect,
            this.refreshToolStripMenuItemDevices});
            this.contextMenuStripDevices.Name = "contextMenuStripDevices";
            this.contextMenuStripDevices.Size = new System.Drawing.Size(163, 64);
            // 
            // toolStripMenuItemDeviceConnect
            // 
            this.toolStripMenuItemDeviceConnect.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.cloud16;
            this.toolStripMenuItemDeviceConnect.Name = "toolStripMenuItemDeviceConnect";
            this.toolStripMenuItemDeviceConnect.Size = new System.Drawing.Size(162, 30);
            this.toolStripMenuItemDeviceConnect.Text = "Connect";
            this.toolStripMenuItemDeviceConnect.ToolTipText = "Connect to the Service Bus Namespaces";
            this.toolStripMenuItemDeviceConnect.Click += new System.EventHandler(this.toolStripMenuItemDeviceConnect_Click);
            // 
            // refreshToolStripMenuItemDevices
            // 
            this.refreshToolStripMenuItemDevices.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.Azure;
            this.refreshToolStripMenuItemDevices.Name = "refreshToolStripMenuItemDevices";
            this.refreshToolStripMenuItemDevices.Size = new System.Drawing.Size(162, 30);
            this.refreshToolStripMenuItemDevices.Text = "Refresh";
            this.refreshToolStripMenuItemDevices.Click += new System.EventHandler(this.refreshToolStripMenuItemDevices_Click);
            // 
            // contextMenuStripMessage
            // 
            this.contextMenuStripMessage.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripMessage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionToolStripMenuItem,
            this.toolStripMenuItemCopyMessage,
            this.toolStripSeparator11,
            this.removeToolStripMenuItem1});
            this.contextMenuStripMessage.Name = "contextMenuStripMessage";
            this.contextMenuStripMessage.Size = new System.Drawing.Size(162, 100);
            // 
            // actionToolStripMenuItem
            // 
            this.actionToolStripMenuItem.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.rest;
            this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
            this.actionToolStripMenuItem.Size = new System.Drawing.Size(161, 30);
            this.actionToolStripMenuItem.Text = "Action";
            this.actionToolStripMenuItem.ToolTipText = "Forward this message to the action (none, blob, xlst, post, put, topic/queue, etc" +
    ".)";
            // 
            // toolStripMenuItemCopyMessage
            // 
            this.toolStripMenuItemCopyMessage.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.deleteMessage1;
            this.toolStripMenuItemCopyMessage.Name = "toolStripMenuItemCopyMessage";
            this.toolStripMenuItemCopyMessage.Size = new System.Drawing.Size(161, 30);
            this.toolStripMenuItemCopyMessage.Text = "Copy";
            this.toolStripMenuItemCopyMessage.ToolTipText = "Copy message and BMP to the clipboard";
            this.toolStripMenuItemCopyMessage.Click += new System.EventHandler(this.toolStripMenuItemCopyMessage_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(158, 6);
            // 
            // removeToolStripMenuItem1
            // 
            this.removeToolStripMenuItem1.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.Delete;
            this.removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
            this.removeToolStripMenuItem1.Size = new System.Drawing.Size(161, 30);
            this.removeToolStripMenuItem1.Text = "Remove";
            this.removeToolStripMenuItem1.ToolTipText = "Remove message from the Subscriber";
            this.removeToolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItemRemoveMessage_Click);
            // 
            // contextMenuStripIoTHubs
            // 
            this.contextMenuStripIoTHubs.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripIoTHubs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuIoTHubConnect});
            this.contextMenuStripIoTHubs.Name = "contextMenuStripIoTHubs";
            this.contextMenuStripIoTHubs.Size = new System.Drawing.Size(163, 34);
            // 
            // toolStripMenuIoTHubConnect
            // 
            this.toolStripMenuIoTHubConnect.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.cloud16;
            this.toolStripMenuIoTHubConnect.Name = "toolStripMenuIoTHubConnect";
            this.toolStripMenuIoTHubConnect.Size = new System.Drawing.Size(162, 30);
            this.toolStripMenuIoTHubConnect.Text = "Connect";
            this.toolStripMenuIoTHubConnect.ToolTipText = "Connect to the IoT Hub Namespace";
            this.toolStripMenuIoTHubConnect.Click += new System.EventHandler(this.toolStripMenuItemIoTHubConnect_Click);
            // 
            // contextMenuStripDevice
            // 
            this.contextMenuStripDevice.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripDevice.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeviceUnconnect,
            this.reconnectToolStripMenuItemDevice,
            this.toolStripSeparator1,
            this.clearToolStripMenuItem,
            this.copySASToolStripMenuItem,
            this.copyConnectionStringToolStripMenuItem,
            this.toolStripSeparator2,
            this.stopSamplingToolStripMenuItemDevice,
            this.loadSamplesToolStripMenuItemDevice});
            this.contextMenuStripDevice.Name = "contextMenuStripDevice";
            this.contextMenuStripDevice.Size = new System.Drawing.Size(281, 226);
            // 
            // toolStripMenuItemDeviceUnconnect
            // 
            this.toolStripMenuItemDeviceUnconnect.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.cloud16;
            this.toolStripMenuItemDeviceUnconnect.Name = "toolStripMenuItemDeviceUnconnect";
            this.toolStripMenuItemDeviceUnconnect.Size = new System.Drawing.Size(280, 30);
            this.toolStripMenuItemDeviceUnconnect.Text = "Unconnect";
            this.toolStripMenuItemDeviceUnconnect.ToolTipText = "Unonnect device from the IoT Hub";
            this.toolStripMenuItemDeviceUnconnect.Click += new System.EventHandler(this.toolStripMenuItemDeviceUnconnect_Click);
            // 
            // reconnectToolStripMenuItemDevice
            // 
            this.reconnectToolStripMenuItemDevice.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.cloud16;
            this.reconnectToolStripMenuItemDevice.Name = "reconnectToolStripMenuItemDevice";
            this.reconnectToolStripMenuItemDevice.Size = new System.Drawing.Size(280, 30);
            this.reconnectToolStripMenuItemDevice.Text = "Re-Connect";
            this.reconnectToolStripMenuItemDevice.Click += new System.EventHandler(this.reconnectToolStripMenuItemDevice_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(277, 6);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.deleteMessage;
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(280, 30);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // copySASToolStripMenuItem
            // 
            this.copySASToolStripMenuItem.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.Paste;
            this.copySASToolStripMenuItem.Name = "copySASToolStripMenuItem";
            this.copySASToolStripMenuItem.Size = new System.Drawing.Size(280, 30);
            this.copySASToolStripMenuItem.Text = "Copy SAS";
            this.copySASToolStripMenuItem.ToolTipText = "Copy the device SAS to the clipboard ";
            this.copySASToolStripMenuItem.Click += new System.EventHandler(this.copySASToolStripMenuItem_Click);
            // 
            // copyConnectionStringToolStripMenuItem
            // 
            this.copyConnectionStringToolStripMenuItem.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.rest;
            this.copyConnectionStringToolStripMenuItem.Name = "copyConnectionStringToolStripMenuItem";
            this.copyConnectionStringToolStripMenuItem.Size = new System.Drawing.Size(280, 30);
            this.copyConnectionStringToolStripMenuItem.Text = "Copy ConnectionString";
            this.copyConnectionStringToolStripMenuItem.ToolTipText = "Copy device connection string to the clipboard";
            this.copyConnectionStringToolStripMenuItem.Click += new System.EventHandler(this.copyConnectionStringToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(277, 6);
            // 
            // stopSamplingToolStripMenuItemDevice
            // 
            this.stopSamplingToolStripMenuItemDevice.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.Delete;
            this.stopSamplingToolStripMenuItemDevice.Name = "stopSamplingToolStripMenuItemDevice";
            this.stopSamplingToolStripMenuItemDevice.Size = new System.Drawing.Size(280, 30);
            this.stopSamplingToolStripMenuItemDevice.Text = "Stop Sampling";
            this.stopSamplingToolStripMenuItemDevice.Click += new System.EventHandler(this.stopSamplingToolStripMenuItemDevice_Click);
            // 
            // loadSamplesToolStripMenuItemDevice
            // 
            this.loadSamplesToolStripMenuItemDevice.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.Add;
            this.loadSamplesToolStripMenuItemDevice.Name = "loadSamplesToolStripMenuItemDevice";
            this.loadSamplesToolStripMenuItemDevice.Size = new System.Drawing.Size(280, 30);
            this.loadSamplesToolStripMenuItemDevice.Text = "Load Samples";
            this.loadSamplesToolStripMenuItemDevice.ToolTipText = "Load a sample of the telemetry data";
            this.loadSamplesToolStripMenuItemDevice.Click += new System.EventHandler(this.loadSamplesToolStripMenuItemDevice_Click);
            // 
            // contextMenuStripSubscribers
            // 
            this.contextMenuStripSubscribers.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripSubscribers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getToolStripMenuItem,
            this.toolStripMenuItemClearAllMessages});
            this.contextMenuStripSubscribers.Name = "contextMenuStripSubscribers";
            this.contextMenuStripSubscribers.Size = new System.Drawing.Size(137, 64);
            // 
            // getToolStripMenuItem
            // 
            this.getToolStripMenuItem.Name = "getToolStripMenuItem";
            this.getToolStripMenuItem.Size = new System.Drawing.Size(136, 30);
            this.getToolStripMenuItem.Text = "Get";
            this.getToolStripMenuItem.Click += new System.EventHandler(this.getToolStripMenuItem_Click);
            // 
            // toolStripMenuItemClearAllMessages
            // 
            this.toolStripMenuItemClearAllMessages.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.deleteMessage;
            this.toolStripMenuItemClearAllMessages.Name = "toolStripMenuItemClearAllMessages";
            this.toolStripMenuItemClearAllMessages.Size = new System.Drawing.Size(136, 30);
            this.toolStripMenuItemClearAllMessages.Text = "Clear";
            this.toolStripMenuItemClearAllMessages.ToolTipText = "Delete received messages";
            this.toolStripMenuItemClearAllMessages.Click += new System.EventHandler(this.toolStripMenuItemClearAllMessages_Click);
            // 
            // contextMenuStripRESTAPI
            // 
            this.contextMenuStripRESTAPI.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripRESTAPI.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copySASToolStripMenuItemRESTAPI});
            this.contextMenuStripRESTAPI.Name = "contextMenuStripRESTAPI";
            this.contextMenuStripRESTAPI.Size = new System.Drawing.Size(177, 34);
            // 
            // copySASToolStripMenuItemRESTAPI
            // 
            this.copySASToolStripMenuItemRESTAPI.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.Paste;
            this.copySASToolStripMenuItemRESTAPI.Name = "copySASToolStripMenuItemRESTAPI";
            this.copySASToolStripMenuItemRESTAPI.Size = new System.Drawing.Size(176, 30);
            this.copySASToolStripMenuItemRESTAPI.Text = "Copy SAS";
            this.copySASToolStripMenuItemRESTAPI.ToolTipText = "Copy SAS to the clipboard";
            this.copySASToolStripMenuItemRESTAPI.Click += new System.EventHandler(this.copySASToolStripMenuItemRESTAPI_Click);
            // 
            // contextMenuStripIoTHub
            // 
            this.contextMenuStripIoTHub.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripIoTHub.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemIoTHubUnconnect});
            this.contextMenuStripIoTHub.Name = "contextMenuStripIoTHub";
            this.contextMenuStripIoTHub.Size = new System.Drawing.Size(182, 34);
            // 
            // toolStripMenuItemIoTHubUnconnect
            // 
            this.toolStripMenuItemIoTHubUnconnect.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.cloud16;
            this.toolStripMenuItemIoTHubUnconnect.Name = "toolStripMenuItemIoTHubUnconnect";
            this.toolStripMenuItemIoTHubUnconnect.Size = new System.Drawing.Size(181, 30);
            this.toolStripMenuItemIoTHubUnconnect.Text = "Unconnect";
            this.toolStripMenuItemIoTHubUnconnect.ToolTipText = "Remove IoT Hub Namespace from this tree.";
            this.toolStripMenuItemIoTHubUnconnect.Click += new System.EventHandler(this.toolStripMenuItemIoTHubUnconnect_Click);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 33);
            this.splitContainerMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerEntities);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.groupBoxLog);
            this.splitContainerMain.Size = new System.Drawing.Size(1153, 895);
            this.splitContainerMain.SplitterDistance = 777;
            this.splitContainerMain.TabIndex = 7;
            // 
            // groupBoxLog
            // 
            this.groupBoxLog.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.groupBoxLog.Controls.Add(this.richTextBoxLog);
            this.groupBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxLog.Location = new System.Drawing.Point(0, 0);
            this.groupBoxLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxLog.Size = new System.Drawing.Size(1153, 114);
            this.groupBoxLog.TabIndex = 0;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "Log";
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLog.Location = new System.Drawing.Point(3, 21);
            this.richTextBoxLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(1147, 91);
            this.richTextBoxLog.TabIndex = 0;
            this.richTextBoxLog.Text = "";
            this.richTextBoxLog.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 928);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Azure IoT Hub Tester";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainerEntities.Panel1.ResumeLayout(false);
            this.splitContainerEntities.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEntities)).EndInit();
            this.splitContainerEntities.ResumeLayout(false);
            this.groupBoxPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAZureIoTHub)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStripDevices.ResumeLayout(false);
            this.contextMenuStripMessage.ResumeLayout(false);
            this.contextMenuStripIoTHubs.ResumeLayout(false);
            this.contextMenuStripDevice.ResumeLayout(false);
            this.contextMenuStripSubscribers.ResumeLayout(false);
            this.contextMenuStripRESTAPI.ResumeLayout(false);
            this.contextMenuStripIoTHub.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.groupBoxLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerEntities;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.TreeView treeViewIoTHubs;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDevices;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeviceConnect;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMessage;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem1;
        private System.Windows.Forms.ImageList imageListIcons;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopyMessage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripIoTHubs;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuIoTHubConnect;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDevice;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeviceUnconnect;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSubscribers;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemClearAllMessages;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySASToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripRESTAPI;
        private System.Windows.Forms.ToolStripMenuItem copySASToolStripMenuItemRESTAPI;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItemDevices;
        private System.Windows.Forms.ToolStripMenuItem getToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reconnectToolStripMenuItemDevice;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private UserControls.Publisher publisher;
        private UserControls.Devices devicesView;
        private System.Windows.Forms.ToolStripMenuItem stopSamplingToolStripMenuItemDevice;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem loadSamplesToolStripMenuItemDevice;
        private System.Windows.Forms.GroupBox groupBoxPanel1;
        private UserControls.Message message;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripIoTHub;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemIoTHubUnconnect;
        private UserControls.RESTClient restClient;
        private System.Windows.Forms.PictureBox pictureBoxAZureIoTHub;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.GroupBox groupBoxLog;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.WebBrowser webBrowserAzureIoTHub;
        private System.Windows.Forms.ToolStripMenuItem clearLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyConnectionStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToAzureIoTHubToolStripMenuItem;
    }
}

