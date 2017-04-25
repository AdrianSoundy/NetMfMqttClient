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
using System.Threading;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
#endregion

namespace RKiss.Tools.AzureIoTHubTester.Dialogs
{
    public partial class NamespaceDialog : Form
    {
       
        public string SelectedNamespace
        {
            get { return Convert.ToString(this.dataGridViewNamespaces.SelectedRows[0].Cells[2].Value); }
        }
        public NamespaceDialog(List<IoTHubConnection> iothubNamespaces)
        {
            InitializeComponent();     

            if (iothubNamespaces != null)
            {
                foreach (var item in iothubNamespaces)
                {
                    this.dataGridViewNamespaces.Rows.Add(this.imageListDialog.Images[0], item.Namespace, item.ConnectionString);
                }
                this.dataGridViewNamespaces.Rows[iothubNamespaces.Count].Cells[this.ColumnStatus.Name].Value = this.imageListDialog.Images[0];
                this.RefreshWorker_Namespace();
                this.dataGridViewNamespaces.Rows[0].Selected = true;

            }
            else
            {
                this.dataGridViewNamespaces.Rows[0].Cells[this.ColumnStatus.Name].Value = this.imageListDialog.Images[0];
            }
        }

        public List<IoTHubConnection> IoTHubNamespaces
        {
            get
            {
                List<IoTHubConnection> iothubNamespaces = new List<IoTHubConnection>();
                foreach (DataGridViewRow row in this.dataGridViewNamespaces.Rows)
                {
                    if (row.IsNewRow == false)
                    {
                        iothubNamespaces.Add(new IoTHubConnection
                        {
                            Namespace = row.Cells[this.ColumnNamespace.Name].Value as string,
                            ConnectionString = row.Cells[this.ColumnConnectionString.Name].Value as string,
                        });
                        row.Cells[this.ColumnStatus.Name].ErrorText = string.Empty;
                    }
                }
                return iothubNamespaces;
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshWorker_Namespace();
        }

        private void RefreshWorker_Namespace()
        {
            ThreadPool.QueueUserWorkItem(delegate(object state)
            {
                List<IoTHubConnection> sbNamespaces = null;
                this.InvokeEx(delegate()
                {                    
                    sbNamespaces = this.IoTHubNamespaces;
                    this.dataGridViewNamespaces.Enabled = false;
                    this.buttonOK.Enabled = false;
                    this.buttonRefresh.Enabled = false;
                    this.buttonRefresh.Text = "Loading";
                });

                using (var progress = new ProgressButton(this, this.buttonRefresh, 1, 0))
                {
                    int rowIndex = 0;
                    
                    foreach (IoTHubConnection item in sbNamespaces)
                    {
                        string errorText = string.Empty;
                        try
                        {
                            if (!string.IsNullOrEmpty(item.Namespace) && !string.IsNullOrEmpty(item.ConnectionString))
                            {
                                string iothubNamespace = SharedAccessSignatureBuilder.GetHostNameNamespaceFromConnectionString(item.ConnectionString);
                                if (string.Compare(iothubNamespace, item.Namespace, true) != 0)
                                    throw new Exception("Wrong namespace name");

                                var devices = ServiceHelper.GetDevicesAsync(item.ConnectionString).Result;
                            }
                        }
                        catch(Exception ex)
                        {
                            errorText = ex.InnerMessage();
                        }
                        this.InvokeEx(delegate ()
                        {
                            this.dataGridViewNamespaces.Rows[rowIndex].Cells[this.ColumnStatus.Name].ErrorText = errorText == null ? string.Empty : errorText;

                        });
                        rowIndex++;
                    }
                }

                this.InvokeEx(delegate()
                {                                     
                    this.dataGridViewNamespaces.Enabled = true;
                    this.buttonRefresh.Text = "Refresh";
                    this.buttonRefresh.Enabled = true;
                    if (this.dataGridViewNamespaces.SelectedRows.Count > 0)
                    {
                        string errText = this.dataGridViewNamespaces.Rows[this.dataGridViewNamespaces.SelectedRows[0].Index].Cells[this.ColumnStatus.Name].ErrorText;
                        this.buttonOK.Enabled = string.IsNullOrEmpty(errText) && this.dataGridViewNamespaces.SelectedRows[0].Index != this.dataGridViewNamespaces.Rows.Count - 1;
                    }
                });
            });
        } 
 
      

        private void dataGridViewNamespaces_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.buttonOK.Enabled = false;
            this.dataGridViewNamespaces.Rows[e.RowIndex].Cells[this.ColumnStatus.Name].Value = this.imageListDialog.Images[0];
        }

        private void dataGridViewNamespaces_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.buttonOK.Enabled = false;
        }

        private void dataGridViewNamespaces_SelectionChanged(object sender, EventArgs e)
        {
            this.buttonOK.Enabled = false;
            var selRows = this.dataGridViewNamespaces.SelectedRows;
            if(selRows != null && selRows.Count > 0)
            {
                this.buttonOK.Enabled = selRows[0].Cells[ColumnStatus.Name].Value != null && 
                    selRows[0].Cells[ColumnNamespace.Name].Value != null && 
                    selRows[0].Cells[ColumnConnectionString.Name].Value != null && 
                    string.IsNullOrEmpty(selRows[0].Cells[ColumnStatus.Name].ErrorText);
            } 
        }
    }
    
}

//this.dataGridViewNamespaces.Rows[this.dataGridViewNamespaces.SelectedCells[0].RowIndex].Selected = true;
//this.buttonOK.Enabled = bOK && this.dataGridViewNamespaces.SelectedRows[0].Index != this.dataGridViewNamespaces.Rows.Count - 1;
