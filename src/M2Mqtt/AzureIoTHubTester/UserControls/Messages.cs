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
    public partial class Message : UserControl
    {
        public Message()
        {
            InitializeComponent();
        }
        public string Title
        {
            set { this.groupBoxMessage.Text = value; }
        }
        public void Clear()
        {
            this.richTextBoxMessage.Clear();
        }
          
        public void ShowMessage(string text)
        {
            this.richTextBoxMessage.Text = text;
        }
           
        public void AppendMessage(string text)
        {
            this.richTextBoxMessage.AppendText(text + "\n");
            this.richTextBoxMessage.SelectionStart = this.richTextBoxMessage.TextLength;
            this.richTextBoxMessage.ScrollToCaret();
        }
        public void AddText(string text, Color color, bool bScroll = true)
        {
            this.richTextBoxMessage.AppendText(text, color, bScroll);
        }
        public void AddText(string text)
        {
            this.richTextBoxMessage.AppendText(text, Color.Black);
        }
        public void ClearMessagePanel()
        {
            this.richTextBoxMessage.Clear();
        }
    }
}
