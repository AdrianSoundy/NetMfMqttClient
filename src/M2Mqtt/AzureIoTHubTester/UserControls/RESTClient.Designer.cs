namespace RKiss.Tools.AzureIoTHubTester.UserControls
{
    partial class RESTClient
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
            this.groupBoxRestClient = new System.Windows.Forms.GroupBox();
            this.checkBoxNext = new System.Windows.Forms.CheckBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonSend = new System.Windows.Forms.Button();
            this.groupBoxMethod = new System.Windows.Forms.GroupBox();
            this.radioButtonPATCH = new System.Windows.Forms.RadioButton();
            this.radioButtonGet = new System.Windows.Forms.RadioButton();
            this.radioButtonDELETE = new System.Windows.Forms.RadioButton();
            this.radioButtonPOST = new System.Windows.Forms.RadioButton();
            this.radioButtonPUT = new System.Windows.Forms.RadioButton();
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.labelUrl = new System.Windows.Forms.Label();
            this.labelLoadingTime = new System.Windows.Forms.Label();
            this.requestResponseMessages = new RKiss.Tools.AzureIoTHubTester.UserControls.RequestResponseMessages();
            this.groupBoxRestClient.SuspendLayout();
            this.groupBoxMethod.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxRestClient
            // 
            this.groupBoxRestClient.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxRestClient.Controls.Add(this.checkBoxNext);
            this.groupBoxRestClient.Controls.Add(this.labelLoadingTime);
            this.groupBoxRestClient.Controls.Add(this.labelStatus);
            this.groupBoxRestClient.Controls.Add(this.requestResponseMessages);
            this.groupBoxRestClient.Controls.Add(this.buttonSend);
            this.groupBoxRestClient.Controls.Add(this.groupBoxMethod);
            this.groupBoxRestClient.Controls.Add(this.textBoxUrl);
            this.groupBoxRestClient.Controls.Add(this.labelUrl);
            this.groupBoxRestClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxRestClient.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBoxRestClient.Location = new System.Drawing.Point(0, 0);
            this.groupBoxRestClient.Name = "groupBoxRestClient";
            this.groupBoxRestClient.Size = new System.Drawing.Size(1202, 783);
            this.groupBoxRestClient.TabIndex = 0;
            this.groupBoxRestClient.TabStop = false;
            this.groupBoxRestClient.Text = "REST Client";
            // 
            // checkBoxNext
            // 
            this.checkBoxNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxNext.AutoSize = true;
            this.checkBoxNext.Location = new System.Drawing.Point(964, 96);
            this.checkBoxNext.Name = "checkBoxNext";
            this.checkBoxNext.Size = new System.Drawing.Size(67, 24);
            this.checkBoxNext.TabIndex = 10;
            this.checkBoxNext.Text = "Next";
            this.checkBoxNext.UseVisualStyleBackColor = true;
            this.checkBoxNext.Visible = false;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelStatus.Location = new System.Drawing.Point(767, 95);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(79, 29);
            this.labelStatus.TabIndex = 9;
            this.labelStatus.Text = "Status";
            this.labelStatus.Visible = false;
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSend.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSend.Location = new System.Drawing.Point(1037, 84);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(147, 54);
            this.buttonSend.TabIndex = 6;
            this.buttonSend.Text = "SEND";
            this.buttonSend.UseVisualStyleBackColor = false;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // groupBoxMethod
            // 
            this.groupBoxMethod.Controls.Add(this.radioButtonPATCH);
            this.groupBoxMethod.Controls.Add(this.radioButtonGet);
            this.groupBoxMethod.Controls.Add(this.radioButtonDELETE);
            this.groupBoxMethod.Controls.Add(this.radioButtonPOST);
            this.groupBoxMethod.Controls.Add(this.radioButtonPUT);
            this.groupBoxMethod.Location = new System.Drawing.Point(6, 71);
            this.groupBoxMethod.Name = "groupBoxMethod";
            this.groupBoxMethod.Size = new System.Drawing.Size(539, 74);
            this.groupBoxMethod.TabIndex = 5;
            this.groupBoxMethod.TabStop = false;
            // 
            // radioButtonPATCH
            // 
            this.radioButtonPATCH.AutoSize = true;
            this.radioButtonPATCH.Location = new System.Drawing.Point(418, 25);
            this.radioButtonPATCH.Name = "radioButtonPATCH";
            this.radioButtonPATCH.Size = new System.Drawing.Size(87, 24);
            this.radioButtonPATCH.TabIndex = 4;
            this.radioButtonPATCH.Text = "PATCH";
            this.radioButtonPATCH.UseVisualStyleBackColor = true;
            this.radioButtonPATCH.CheckedChanged += new System.EventHandler(this.radioButtonPATCH_CheckedChanged);
            // 
            // radioButtonGet
            // 
            this.radioButtonGet.AutoSize = true;
            this.radioButtonGet.Checked = true;
            this.radioButtonGet.Location = new System.Drawing.Point(18, 25);
            this.radioButtonGet.Name = "radioButtonGet";
            this.radioButtonGet.Size = new System.Drawing.Size(67, 24);
            this.radioButtonGet.TabIndex = 4;
            this.radioButtonGet.TabStop = true;
            this.radioButtonGet.Text = "GET";
            this.radioButtonGet.UseVisualStyleBackColor = true;
            this.radioButtonGet.CheckedChanged += new System.EventHandler(this.radioButtonGet_CheckedChanged);
            // 
            // radioButtonDELETE
            // 
            this.radioButtonDELETE.AutoSize = true;
            this.radioButtonDELETE.Location = new System.Drawing.Point(300, 25);
            this.radioButtonDELETE.Name = "radioButtonDELETE";
            this.radioButtonDELETE.Size = new System.Drawing.Size(97, 24);
            this.radioButtonDELETE.TabIndex = 4;
            this.radioButtonDELETE.Text = "DELETE";
            this.radioButtonDELETE.UseVisualStyleBackColor = true;
            this.radioButtonDELETE.CheckedChanged += new System.EventHandler(this.radioButtonDELETE_CheckedChanged);
            // 
            // radioButtonPOST
            // 
            this.radioButtonPOST.AutoSize = true;
            this.radioButtonPOST.Location = new System.Drawing.Point(109, 25);
            this.radioButtonPOST.Name = "radioButtonPOST";
            this.radioButtonPOST.Size = new System.Drawing.Size(76, 24);
            this.radioButtonPOST.TabIndex = 4;
            this.radioButtonPOST.Text = "POST";
            this.radioButtonPOST.UseVisualStyleBackColor = true;
            this.radioButtonPOST.CheckedChanged += new System.EventHandler(this.radioButtonPOST_CheckedChanged);
            // 
            // radioButtonPUT
            // 
            this.radioButtonPUT.AutoSize = true;
            this.radioButtonPUT.Location = new System.Drawing.Point(210, 25);
            this.radioButtonPUT.Name = "radioButtonPUT";
            this.radioButtonPUT.Size = new System.Drawing.Size(65, 24);
            this.radioButtonPUT.TabIndex = 4;
            this.radioButtonPUT.Text = "PUT";
            this.radioButtonPUT.UseVisualStyleBackColor = true;
            this.radioButtonPUT.CheckedChanged += new System.EventHandler(this.radioButtonPUT_CheckedChanged);
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUrl.BackColor = System.Drawing.Color.Azure;
            this.textBoxUrl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUrl.ForeColor = System.Drawing.Color.Blue;
            this.textBoxUrl.Location = new System.Drawing.Point(45, 34);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(1139, 23);
            this.textBoxUrl.TabIndex = 1;
            this.textBoxUrl.Text = "https://rk2016-IoT.azure-devices.net/devices?top=111&api-version=2016-11-14";
            // 
            // labelUrl
            // 
            this.labelUrl.AutoSize = true;
            this.labelUrl.Location = new System.Drawing.Point(6, 37);
            this.labelUrl.Name = "labelUrl";
            this.labelUrl.Size = new System.Drawing.Size(33, 20);
            this.labelUrl.TabIndex = 3;
            this.labelUrl.Text = "Url:";
            // 
            // labelLoadingTime
            // 
            this.labelLoadingTime.AutoSize = true;
            this.labelLoadingTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoadingTime.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelLoadingTime.Location = new System.Drawing.Point(562, 99);
            this.labelLoadingTime.Name = "labelLoadingTime";
            this.labelLoadingTime.Size = new System.Drawing.Size(182, 25);
            this.labelLoadingTime.TabIndex = 9;
            this.labelLoadingTime.Text = "RspTime: 99999ms";
            this.labelLoadingTime.Visible = false;
            // 
            // requestResponseMessages
            // 
            this.requestResponseMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.requestResponseMessages.BackColor = System.Drawing.Color.WhiteSmoke;
            this.requestResponseMessages.Location = new System.Drawing.Point(3, 151);
            this.requestResponseMessages.Name = "requestResponseMessages";
            this.requestResponseMessages.RequestHeaders = "";
            this.requestResponseMessages.RequestPayload = "";
            this.requestResponseMessages.ResponseMessage = "";
            this.requestResponseMessages.Size = new System.Drawing.Size(1193, 626);
            this.requestResponseMessages.TabIndex = 7;
            // 
            // RESTClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.groupBoxRestClient);
            this.Name = "RESTClient";
            this.Size = new System.Drawing.Size(1202, 783);
            this.groupBoxRestClient.ResumeLayout(false);
            this.groupBoxRestClient.PerformLayout();
            this.groupBoxMethod.ResumeLayout(false);
            this.groupBoxMethod.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxRestClient;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.GroupBox groupBoxMethod;
        private System.Windows.Forms.RadioButton radioButtonPATCH;
        private System.Windows.Forms.RadioButton radioButtonGet;
        private System.Windows.Forms.RadioButton radioButtonDELETE;
        private System.Windows.Forms.RadioButton radioButtonPOST;
        private System.Windows.Forms.RadioButton radioButtonPUT;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Label labelUrl;
        private RequestResponseMessages requestResponseMessages;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.CheckBox checkBoxNext;
        private System.Windows.Forms.Label labelLoadingTime;
    }
}
