namespace RKiss.Tools.AzureIoTHubTester.Dialogs
{
    partial class NamespaceDialog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NamespaceDialog));
            this.dataGridViewNamespaces = new System.Windows.Forms.DataGridView();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.imageListDialog = new System.Windows.Forms.ImageList(this.components);
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.ColumnStatus = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnNamespace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnConnectionString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNamespaces)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewNamespaces
            // 
            this.dataGridViewNamespaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewNamespaces.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewNamespaces.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewNamespaces.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewNamespaces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNamespaces.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnStatus,
            this.ColumnNamespace,
            this.ColumnConnectionString});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewNamespaces.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewNamespaces.Location = new System.Drawing.Point(4, 1);
            this.dataGridViewNamespaces.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridViewNamespaces.MultiSelect = false;
            this.dataGridViewNamespaces.Name = "dataGridViewNamespaces";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewNamespaces.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewNamespaces.RowHeadersWidth = 25;
            this.dataGridViewNamespaces.Size = new System.Drawing.Size(844, 320);
            this.dataGridViewNamespaces.TabIndex = 1;
            this.dataGridViewNamespaces.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewNamespaces_CellValueChanged);
            this.dataGridViewNamespaces.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewNamespaces_RowsAdded);
            this.dataGridViewNamespaces.SelectionChanged += new System.EventHandler(this.dataGridViewNamespaces_SelectionChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(522, 331);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(86, 35);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(742, 331);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(86, 35);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // imageListDialog
            // 
            this.imageListDialog.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListDialog.ImageStream")));
            this.imageListDialog.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListDialog.Images.SetKeyName(0, "cloud16.ico");
            this.imageListDialog.Images.SetKeyName(1, "Rotate1.ico");
            this.imageListDialog.Images.SetKeyName(2, "Rotate2.ico");
            this.imageListDialog.Images.SetKeyName(3, "Rotate3.ico");
            this.imageListDialog.Images.SetKeyName(4, "Rotate4.ico");
            this.imageListDialog.Images.SetKeyName(5, "Rotate5.ico");
            this.imageListDialog.Images.SetKeyName(6, "Rotate6.ico");
            this.imageListDialog.Images.SetKeyName(7, "Rotate7.ico");
            this.imageListDialog.Images.SetKeyName(8, "Rotate8.ico");
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.ImageIndex = 0;
            this.buttonRefresh.ImageList = this.imageListDialog;
            this.buttonRefresh.Location = new System.Drawing.Point(616, 331);
            this.buttonRefresh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(117, 35);
            this.buttonRefresh.TabIndex = 4;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // ColumnStatus
            // 
            this.ColumnStatus.Frozen = true;
            this.ColumnStatus.HeaderText = "Stat";
            this.ColumnStatus.Image = global::RKiss.Tools.AzureIoTHubTester.Properties.Resources.cloud16;
            this.ColumnStatus.Name = "ColumnStatus";
            this.ColumnStatus.ReadOnly = true;
            this.ColumnStatus.Width = 32;
            // 
            // ColumnNamespace
            // 
            this.ColumnNamespace.HeaderText = "Namespace";
            this.ColumnNamespace.Name = "ColumnNamespace";
            this.ColumnNamespace.Width = 150;
            // 
            // ColumnConnectionString
            // 
            this.ColumnConnectionString.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnConnectionString.FillWeight = 21.2766F;
            this.ColumnConnectionString.HeaderText = "ConnectionString";
            this.ColumnConnectionString.Name = "ColumnConnectionString";
            this.ColumnConnectionString.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnConnectionString.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NamespaceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 371);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.dataGridViewNamespaces);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1564, 739);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(859, 278);
            this.Name = "NamespaceDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NamespaceDialog";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNamespaces)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewNamespaces;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ImageList imageListDialog;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.DataGridViewImageColumn ColumnStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNamespace;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnConnectionString;
    }
}