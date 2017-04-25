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
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Windows.Forms;
#endregion

namespace RKiss.Tools.AzureIoTHubTester.UserControls
{

    public partial class Publisher : UserControl
    {
        public event PublishEventHandler RaisePublishButton;

        public Publisher()
        {
            InitializeComponent();

        }
     
        #region Public API
        public string Title1
        {
            set { this.groupBoxPublisherPanel1.Text = value; }
        }
        public string Title2
        {
            set { this.groupBoxPublisherPanel2.Text = value; }
        }
        public void ClearMessagePanel()
        {
            this.richTextBoxMessage.Clear();
        }
        public void ClearPayloadPanel()
        {
            this.richTextBoxPayload.Clear();
        }
        public void AddText(string text, Color color, bool bScroll = true)
        {
            this.richTextBoxMessage.AppendText(text, color, bScroll);
        }
        public void AddText(string text)
        {
            this.richTextBoxMessage.AppendText(text, Color.Black);
        }
        public string RtfText { get { return this.richTextBoxMessage.Rtf; } }

        public void AppendMessage(string text)
        {
            this.richTextBoxMessage.AppendText(text + "\n");
            this.richTextBoxMessage.SelectionStart = this.richTextBoxMessage.TextLength;
            this.richTextBoxMessage.ScrollToCaret();

        }
        public void ShowMessage(string text)
        {
            this.richTextBoxMessage.Text = text;          
        }
        public bool EnablePublish
        {
            get { return this.panelPublish.Visible; }
            set { this.panelPublish.Visible = value; }
        }
        public string Topic
        {
            get { return this.textBoxTopic.Text; }
            set { this.textBoxTopic.Text = value; }
        }
        public string Description
        {
            get { return this.labelDescription.Text; }
            set { this.labelDescription.Text = value; }
        }
        public int QoS
        {
            get { return this.comboBoxQoS.SelectedIndex; }
            set { this.comboBoxQoS.SelectedIndex = value; }
        }
        public bool Retain
        {
            get { return this.checkBoxRetain.Checked; }
            set { this.checkBoxRetain.Checked = value; }
        }

        public string Payload
        {
            get { return this.richTextBoxPayload.Text; }
            set { this.richTextBoxPayload.Text = value; }
        }

        #endregion

        private void buttonPublish_Click(object sender, EventArgs e)
        {
            if (this.RaisePublishButton != null)
            {
                try
                {
                    PublishEventArgs eventArgs = null;
                    this.InvokeEx(delegate ()
                    {
                        this.buttonPublish.Enabled = false;
                        eventArgs = new PublishEventArgs()
                        {
                            Topic = this.Topic,
                            QoS = Convert.ToByte(this.QoS),
                            Retain = this.Retain,
                            PeriodTime = TimeSpan.FromSeconds(3),
                            Counter = 100
                             
                        };
                        try
                        {
                            eventArgs.Payload = JRaw.Parse(this.Payload).ToString();
                            eventArgs.IsJson = true;
                        }
                        catch
                        {
                            eventArgs.Payload = this.Payload;
                            eventArgs.IsJson = false;
                        }
                    });

                    this.RaisePublishButton(sender, eventArgs);                  
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.InnerMessage(), "Publish on the topic.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.InvokeEx(delegate () { this.buttonPublish.Enabled = true; });
                }
            }
        }

        private void textBoxTopic_TextChanged(object sender, EventArgs e)
        {          
           this.buttonPublish.Enabled = Uri.IsWellFormedUriString(this.textBoxTopic.Text, UriKind.Relative);
        }
    }

    // stolen from http://stackoverflow.com/questions/783925/control-invoke-with-input-parameters
    internal static class ControlExtensions
    {
        public static TResult InvokeEx<TControl, TResult>(this TControl control, Func<TControl, TResult> func) where TControl : Control
        {
            return control.InvokeRequired ? (TResult)control.Invoke(func, control) : func(control);
        }
        public static void InvokeEx<TControl>(this TControl control, Action<TControl> func) where TControl : Control
        {
            control.InvokeEx(c => { func(c); return c; });
        }
        public static void InvokeEx<TControl>(this TControl control, Action action) where TControl : Control
        {
            control.InvokeEx(c => action());
        }
    }
}
