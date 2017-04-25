namespace RKiss.Tools.AzureIoTHubTester.UserControls
{
    partial class RequestResponseMessages
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
            this.splitContainerRestClient = new System.Windows.Forms.SplitContainer();
            this.splitContainerRequest = new System.Windows.Forms.SplitContainer();
            this.groupBoxRequestHeaders = new System.Windows.Forms.GroupBox();
            this.richTextBoxRequestHeaders = new System.Windows.Forms.RichTextBox();
            this.groupBoxRequestPayload = new System.Windows.Forms.GroupBox();
            this.richTextBoxRequestPayload = new System.Windows.Forms.RichTextBox();
            this.groupBoxResponse = new System.Windows.Forms.GroupBox();
            this.richTextBoxResponse = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRestClient)).BeginInit();
            this.splitContainerRestClient.Panel1.SuspendLayout();
            this.splitContainerRestClient.Panel2.SuspendLayout();
            this.splitContainerRestClient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRequest)).BeginInit();
            this.splitContainerRequest.Panel1.SuspendLayout();
            this.splitContainerRequest.Panel2.SuspendLayout();
            this.splitContainerRequest.SuspendLayout();
            this.groupBoxRequestHeaders.SuspendLayout();
            this.groupBoxRequestPayload.SuspendLayout();
            this.groupBoxResponse.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerRestClient
            // 
            this.splitContainerRestClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRestClient.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRestClient.Name = "splitContainerRestClient";
            this.splitContainerRestClient.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRestClient.Panel1
            // 
            this.splitContainerRestClient.Panel1.Controls.Add(this.splitContainerRequest);
            // 
            // splitContainerRestClient.Panel2
            // 
            this.splitContainerRestClient.Panel2.Controls.Add(this.groupBoxResponse);
            this.splitContainerRestClient.Size = new System.Drawing.Size(719, 766);
            this.splitContainerRestClient.SplitterDistance = 381;
            this.splitContainerRestClient.TabIndex = 1;
            // 
            // splitContainerRequest
            // 
            this.splitContainerRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerRequest.Location = new System.Drawing.Point(3, 0);
            this.splitContainerRequest.Name = "splitContainerRequest";
            this.splitContainerRequest.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRequest.Panel1
            // 
            this.splitContainerRequest.Panel1.Controls.Add(this.groupBoxRequestHeaders);
            // 
            // splitContainerRequest.Panel2
            // 
            this.splitContainerRequest.Panel2.Controls.Add(this.groupBoxRequestPayload);
            this.splitContainerRequest.Size = new System.Drawing.Size(713, 373);
            this.splitContainerRequest.SplitterDistance = 87;
            this.splitContainerRequest.TabIndex = 7;
            // 
            // groupBoxRequestHeaders
            // 
            this.groupBoxRequestHeaders.Controls.Add(this.richTextBoxRequestHeaders);
            this.groupBoxRequestHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxRequestHeaders.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxRequestHeaders.Location = new System.Drawing.Point(0, 0);
            this.groupBoxRequestHeaders.Name = "groupBoxRequestHeaders";
            this.groupBoxRequestHeaders.Size = new System.Drawing.Size(713, 87);
            this.groupBoxRequestHeaders.TabIndex = 0;
            this.groupBoxRequestHeaders.TabStop = false;
            this.groupBoxRequestHeaders.Text = "Request headers";
            // 
            // richTextBoxRequestHeaders
            // 
            this.richTextBoxRequestHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxRequestHeaders.Location = new System.Drawing.Point(3, 22);
            this.richTextBoxRequestHeaders.Name = "richTextBoxRequestHeaders";
            this.richTextBoxRequestHeaders.Size = new System.Drawing.Size(707, 62);
            this.richTextBoxRequestHeaders.TabIndex = 6;
            this.richTextBoxRequestHeaders.Text = "";
            this.richTextBoxRequestHeaders.WordWrap = false;
            // 
            // groupBoxRequestPayload
            // 
            this.groupBoxRequestPayload.Controls.Add(this.richTextBoxRequestPayload);
            this.groupBoxRequestPayload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxRequestPayload.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxRequestPayload.Location = new System.Drawing.Point(0, 0);
            this.groupBoxRequestPayload.Name = "groupBoxRequestPayload";
            this.groupBoxRequestPayload.Size = new System.Drawing.Size(713, 282);
            this.groupBoxRequestPayload.TabIndex = 0;
            this.groupBoxRequestPayload.TabStop = false;
            this.groupBoxRequestPayload.Text = "Request payload";
            // 
            // richTextBoxRequestPayload
            // 
            this.richTextBoxRequestPayload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxRequestPayload.Location = new System.Drawing.Point(3, 22);
            this.richTextBoxRequestPayload.Name = "richTextBoxRequestPayload";
            this.richTextBoxRequestPayload.Size = new System.Drawing.Size(707, 257);
            this.richTextBoxRequestPayload.TabIndex = 7;
            this.richTextBoxRequestPayload.Text = "{\n\n}";
            this.richTextBoxRequestPayload.WordWrap = false;
            // 
            // groupBoxResponse
            // 
            this.groupBoxResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxResponse.Controls.Add(this.richTextBoxResponse);
            this.groupBoxResponse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxResponse.Location = new System.Drawing.Point(3, 3);
            this.groupBoxResponse.Name = "groupBoxResponse";
            this.groupBoxResponse.Size = new System.Drawing.Size(713, 362);
            this.groupBoxResponse.TabIndex = 0;
            this.groupBoxResponse.TabStop = false;
            this.groupBoxResponse.Text = "Response";
            // 
            // richTextBoxResponse
            // 
            this.richTextBoxResponse.BackColor = System.Drawing.Color.White;
            this.richTextBoxResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxResponse.Location = new System.Drawing.Point(3, 22);
            this.richTextBoxResponse.Name = "richTextBoxResponse";
            this.richTextBoxResponse.ReadOnly = true;
            this.richTextBoxResponse.Size = new System.Drawing.Size(707, 337);
            this.richTextBoxResponse.TabIndex = 1;
            this.richTextBoxResponse.Text = "";
            this.richTextBoxResponse.WordWrap = false;
            // 
            // RequestResponseMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerRestClient);
            this.Name = "RequestResponseMessages";
            this.Size = new System.Drawing.Size(719, 766);
            this.splitContainerRestClient.Panel1.ResumeLayout(false);
            this.splitContainerRestClient.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRestClient)).EndInit();
            this.splitContainerRestClient.ResumeLayout(false);
            this.splitContainerRequest.Panel1.ResumeLayout(false);
            this.splitContainerRequest.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRequest)).EndInit();
            this.splitContainerRequest.ResumeLayout(false);
            this.groupBoxRequestHeaders.ResumeLayout(false);
            this.groupBoxRequestPayload.ResumeLayout(false);
            this.groupBoxResponse.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerRestClient;
        private System.Windows.Forms.SplitContainer splitContainerRequest;
        private System.Windows.Forms.GroupBox groupBoxRequestHeaders;
        private System.Windows.Forms.RichTextBox richTextBoxRequestHeaders;
        private System.Windows.Forms.GroupBox groupBoxRequestPayload;
        private System.Windows.Forms.RichTextBox richTextBoxRequestPayload;
        private System.Windows.Forms.GroupBox groupBoxResponse;
        private System.Windows.Forms.RichTextBox richTextBoxResponse;
    }
}
