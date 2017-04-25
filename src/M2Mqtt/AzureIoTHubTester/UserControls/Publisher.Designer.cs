namespace RKiss.Tools.AzureIoTHubTester.UserControls
{
    partial class Publisher
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonPublish = new System.Windows.Forms.Button();
            this.labelDescription = new System.Windows.Forms.Label();
            this.checkBoxRetain = new System.Windows.Forms.CheckBox();
            this.comboBoxQoS = new System.Windows.Forms.ComboBox();
            this.labelQoS = new System.Windows.Forms.Label();
            this.labelTopic = new System.Windows.Forms.Label();
            this.textBoxTopic = new System.Windows.Forms.TextBox();
            this.richTextBoxPayload = new System.Windows.Forms.RichTextBox();
            this.splitContainerPublisher = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBoxPublisherPanel1 = new System.Windows.Forms.GroupBox();
            this.richTextBoxMessage = new System.Windows.Forms.RichTextBox();
            this.groupBoxPublisherPanel2 = new System.Windows.Forms.GroupBox();
            this.panelPublish = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPublisher)).BeginInit();
            this.splitContainerPublisher.Panel1.SuspendLayout();
            this.splitContainerPublisher.Panel2.SuspendLayout();
            this.splitContainerPublisher.SuspendLayout();
            this.groupBoxPublisherPanel1.SuspendLayout();
            this.groupBoxPublisherPanel2.SuspendLayout();
            this.panelPublish.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPublish
            // 
            this.buttonPublish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPublish.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPublish.Location = new System.Drawing.Point(690, 17);
            this.buttonPublish.Name = "buttonPublish";
            this.buttonPublish.Size = new System.Drawing.Size(99, 64);
            this.buttonPublish.TabIndex = 0;
            this.buttonPublish.Text = "Publish";
            this.buttonPublish.UseVisualStyleBackColor = true;
            this.buttonPublish.Click += new System.EventHandler(this.buttonPublish_Click);
            // 
            // labelDescription
            // 
            this.labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDescription.AutoSize = true;
            this.labelDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescription.ForeColor = System.Drawing.SystemColors.Highlight;
            this.labelDescription.Location = new System.Drawing.Point(259, 61);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(89, 20);
            this.labelDescription.TabIndex = 11;
            this.labelDescription.Text = "Description";
            // 
            // checkBoxRetain
            // 
            this.checkBoxRetain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxRetain.AutoSize = true;
            this.checkBoxRetain.Location = new System.Drawing.Point(146, 57);
            this.checkBoxRetain.Name = "checkBoxRetain";
            this.checkBoxRetain.Size = new System.Drawing.Size(82, 24);
            this.checkBoxRetain.TabIndex = 10;
            this.checkBoxRetain.Text = "Retain";
            this.checkBoxRetain.UseVisualStyleBackColor = true;
            // 
            // comboBoxQoS
            // 
            this.comboBoxQoS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxQoS.FormattingEnabled = true;
            this.comboBoxQoS.Items.AddRange(new object[] {
            "0",
            "1"});
            this.comboBoxQoS.Location = new System.Drawing.Point(64, 55);
            this.comboBoxQoS.Name = "comboBoxQoS";
            this.comboBoxQoS.Size = new System.Drawing.Size(66, 28);
            this.comboBoxQoS.TabIndex = 9;
            this.comboBoxQoS.Text = "0";
            // 
            // labelQoS
            // 
            this.labelQoS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelQoS.AutoSize = true;
            this.labelQoS.Location = new System.Drawing.Point(13, 63);
            this.labelQoS.Name = "labelQoS";
            this.labelQoS.Size = new System.Drawing.Size(45, 20);
            this.labelQoS.TabIndex = 7;
            this.labelQoS.Text = "QoS:";
            // 
            // labelTopic
            // 
            this.labelTopic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTopic.AutoSize = true;
            this.labelTopic.Location = new System.Drawing.Point(8, 21);
            this.labelTopic.Name = "labelTopic";
            this.labelTopic.Size = new System.Drawing.Size(51, 20);
            this.labelTopic.TabIndex = 6;
            this.labelTopic.Text = "Topic:";
            // 
            // textBoxTopic
            // 
            this.textBoxTopic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTopic.Location = new System.Drawing.Point(65, 17);
            this.textBoxTopic.Name = "textBoxTopic";
            this.textBoxTopic.Size = new System.Drawing.Size(609, 26);
            this.textBoxTopic.TabIndex = 8;
            this.textBoxTopic.TextChanged += new System.EventHandler(this.textBoxTopic_TextChanged);
            // 
            // richTextBoxPayload
            // 
            this.richTextBoxPayload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxPayload.Location = new System.Drawing.Point(3, 22);
            this.richTextBoxPayload.Name = "richTextBoxPayload";
            this.richTextBoxPayload.Size = new System.Drawing.Size(790, 234);
            this.richTextBoxPayload.TabIndex = 12;
            this.richTextBoxPayload.Text = "";
            this.richTextBoxPayload.WordWrap = false;
            // 
            // splitContainerPublisher
            // 
            this.splitContainerPublisher.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPublisher.Location = new System.Drawing.Point(0, 0);
            this.splitContainerPublisher.Name = "splitContainerPublisher";
            this.splitContainerPublisher.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerPublisher.Panel1
            // 
            this.splitContainerPublisher.Panel1.Controls.Add(this.groupBox2);
            this.splitContainerPublisher.Panel1.Controls.Add(this.groupBoxPublisherPanel1);
            // 
            // splitContainerPublisher.Panel2
            // 
            this.splitContainerPublisher.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainerPublisher.Panel2.Controls.Add(this.groupBoxPublisherPanel2);
            this.splitContainerPublisher.Panel2.Controls.Add(this.panelPublish);
            this.splitContainerPublisher.Size = new System.Drawing.Size(802, 726);
            this.splitContainerPublisher.SplitterDistance = 355;
            this.splitContainerPublisher.TabIndex = 13;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(444, 225);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(8, 8);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // groupBoxPublisherPanel1
            // 
            this.groupBoxPublisherPanel1.Controls.Add(this.richTextBoxMessage);
            this.groupBoxPublisherPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPublisherPanel1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBoxPublisherPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupBoxPublisherPanel1.Name = "groupBoxPublisherPanel1";
            this.groupBoxPublisherPanel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBoxPublisherPanel1.Size = new System.Drawing.Size(802, 355);
            this.groupBoxPublisherPanel1.TabIndex = 14;
            this.groupBoxPublisherPanel1.TabStop = false;
            this.groupBoxPublisherPanel1.Text = "Subscriber";
            // 
            // richTextBoxMessage
            // 
            this.richTextBoxMessage.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBoxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxMessage.Location = new System.Drawing.Point(3, 22);
            this.richTextBoxMessage.Name = "richTextBoxMessage";
            this.richTextBoxMessage.ReadOnly = true;
            this.richTextBoxMessage.Size = new System.Drawing.Size(796, 330);
            this.richTextBoxMessage.TabIndex = 13;
            this.richTextBoxMessage.Text = "";
            this.richTextBoxMessage.WordWrap = false;
            // 
            // groupBoxPublisherPanel2
            // 
            this.groupBoxPublisherPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPublisherPanel2.Controls.Add(this.richTextBoxPayload);
            this.groupBoxPublisherPanel2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBoxPublisherPanel2.Location = new System.Drawing.Point(3, 3);
            this.groupBoxPublisherPanel2.Name = "groupBoxPublisherPanel2";
            this.groupBoxPublisherPanel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBoxPublisherPanel2.Size = new System.Drawing.Size(796, 259);
            this.groupBoxPublisherPanel2.TabIndex = 13;
            this.groupBoxPublisherPanel2.TabStop = false;
            this.groupBoxPublisherPanel2.Text = "Publisher";
            // 
            // panelPublish
            // 
            this.panelPublish.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPublish.Controls.Add(this.textBoxTopic);
            this.panelPublish.Controls.Add(this.buttonPublish);
            this.panelPublish.Controls.Add(this.comboBoxQoS);
            this.panelPublish.Controls.Add(this.labelQoS);
            this.panelPublish.Controls.Add(this.checkBoxRetain);
            this.panelPublish.Controls.Add(this.labelDescription);
            this.panelPublish.Controls.Add(this.labelTopic);
            this.panelPublish.Location = new System.Drawing.Point(3, 268);
            this.panelPublish.Name = "panelPublish";
            this.panelPublish.Size = new System.Drawing.Size(796, 96);
            this.panelPublish.TabIndex = 12;
            // 
            // Publisher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.splitContainerPublisher);
            this.Name = "Publisher";
            this.Size = new System.Drawing.Size(802, 726);
            this.splitContainerPublisher.Panel1.ResumeLayout(false);
            this.splitContainerPublisher.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPublisher)).EndInit();
            this.splitContainerPublisher.ResumeLayout(false);
            this.groupBoxPublisherPanel1.ResumeLayout(false);
            this.groupBoxPublisherPanel2.ResumeLayout(false);
            this.panelPublish.ResumeLayout(false);
            this.panelPublish.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonPublish;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.CheckBox checkBoxRetain;
        private System.Windows.Forms.ComboBox comboBoxQoS;
        private System.Windows.Forms.Label labelQoS;
        private System.Windows.Forms.Label labelTopic;
        private System.Windows.Forms.TextBox textBoxTopic;
        private System.Windows.Forms.RichTextBox richTextBoxPayload;
        private System.Windows.Forms.SplitContainer splitContainerPublisher;
        private System.Windows.Forms.RichTextBox richTextBoxMessage;
        private System.Windows.Forms.Panel panelPublish;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBoxPublisherPanel1;
        private System.Windows.Forms.GroupBox groupBoxPublisherPanel2;
    }
}
