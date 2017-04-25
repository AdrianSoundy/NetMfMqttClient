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
using System.Drawing;
using System.Windows.Forms;
#endregion

namespace RKiss.Tools.AzureIoTHubTester.UserControls
{
    public partial class RequestResponseMessages : UserControl
    {
        public RequestResponseMessages()
        {
            InitializeComponent();
            this.splitContainerRequest.Panel1MinSize = 60;
            this.splitContainerRequest.Panel2MinSize = this.splitContainerRequest.Panel2.ClientSize.Height * 2 / 3;

        }

        public bool HideRequestPayload
        {
            set
            {
                if (value)
                {
                    this.splitContainerRequest.Panel2Collapsed = true;
                    this.splitContainerRequest.Panel2.Hide();
                    
                }
                else
                {
                    this.splitContainerRequest.Panel2Collapsed = false;
                    this.splitContainerRequest.Panel2MinSize = this.splitContainerRequest.Panel2.ClientSize.Height * 2 / 3;
                    this.splitContainerRequest.Panel2.Show();
                }               
            }
        }
        public string RequestHeaders
        {
            get { return this.richTextBoxRequestHeaders.Text; }
            set { this.richTextBoxRequestHeaders.Text = value; }                
        }
        public string RequestPayload
        {
            get { return this.richTextBoxRequestPayload.Text; }
            set { this.richTextBoxRequestPayload.Text = value; }
        }
        public string ResponseMessage
        {
            get { return this.richTextBoxResponse.Text; }
            set { this.richTextBoxResponse.Text = value; }
        }

        public void AddTextToResponseMessage(string text, Color color, bool bScroll = true)
        {
            this.richTextBoxResponse.AppendText(text, color, bScroll);
        }
        public void AddTextToResponseMessage(string text, bool bScroll = true)
        {
            this.richTextBoxResponse.AppendText(text, Color.Black, bScroll );
        }

        public FixedPanel FixedRequestPanel
        {
            set { this.splitContainerRequest.FixedPanel = value; }
        }
        public FixedPanel FixedClientPanel
        {
            set { this.splitContainerRestClient.FixedPanel = value; }
        }
    }
}
