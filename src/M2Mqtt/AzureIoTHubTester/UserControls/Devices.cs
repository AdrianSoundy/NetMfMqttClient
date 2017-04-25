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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
#endregion

namespace RKiss.Tools.AzureIoTHubTester.UserControls
{
    public partial class Devices : UserControl
    {
        public Devices()
        {
            InitializeComponent();

            this.devicesGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.75F, FontStyle.Bold );
        }

        public void Update(string jsontext)
        {
            var deviceInfo = new[] { new { deviceId = "", connectionState = "", status = "", connectionStateUpdatedTime = "", lastActivityTime = "", cloudToDeviceMessageCount = 0 } };
            var listOfRegisteredDevices = JsonConvert.DeserializeAnonymousType(jsontext, deviceInfo).OrderBy(i => i.deviceId).ToList().ToBindingList();
            this.devicesGridView.DataSource = listOfRegisteredDevices;
            var row = this.devicesGridView.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => (string)r.Cells["connectionState"].Value == "Disconnected");
            if (row != null)
                row.Selected = true;

        }
        public string SelectedDevice
        {
            get { return this.devicesGridView.SelectedRows.Count == 0 ? "" : (string)this.devicesGridView.SelectedRows[0].Cells["deviceId"].Value; }
        }
    }
}
