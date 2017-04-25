namespace RKiss.Tools.AzureIoTHubTester.UserControls
{
    partial class Devices
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
            this.devicesGridView = new System.Windows.Forms.DataGridView();
            this.groupBoxDataGrid = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.devicesGridView)).BeginInit();
            this.groupBoxDataGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // devicesGridView
            // 
            this.devicesGridView.AllowUserToAddRows = false;
            this.devicesGridView.AllowUserToDeleteRows = false;
            this.devicesGridView.AllowUserToOrderColumns = true;
            this.devicesGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.devicesGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.devicesGridView.ColumnHeadersHeight = 34;
            this.devicesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.devicesGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.devicesGridView.Location = new System.Drawing.Point(3, 22);
            this.devicesGridView.MultiSelect = false;
            this.devicesGridView.Name = "devicesGridView";
            this.devicesGridView.ReadOnly = true;
            this.devicesGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.devicesGridView.RowTemplate.Height = 28;
            this.devicesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.devicesGridView.Size = new System.Drawing.Size(633, 304);
            this.devicesGridView.TabIndex = 3;
            // 
            // groupBoxDataGrid
            // 
            this.groupBoxDataGrid.Controls.Add(this.devicesGridView);
            this.groupBoxDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDataGrid.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBoxDataGrid.Location = new System.Drawing.Point(0, 0);
            this.groupBoxDataGrid.Name = "groupBoxDataGrid";
            this.groupBoxDataGrid.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBoxDataGrid.Size = new System.Drawing.Size(639, 329);
            this.groupBoxDataGrid.TabIndex = 4;
            this.groupBoxDataGrid.TabStop = false;
            this.groupBoxDataGrid.Text = "Registered Devices";
            // 
            // Devices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxDataGrid);
            this.Name = "Devices";
            this.Size = new System.Drawing.Size(639, 329);
            ((System.ComponentModel.ISupportInitialize)(this.devicesGridView)).EndInit();
            this.groupBoxDataGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView devicesGridView;
        private System.Windows.Forms.GroupBox groupBoxDataGrid;
    }
}
