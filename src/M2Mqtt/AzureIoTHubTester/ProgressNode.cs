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
using System.Windows.Forms;
#endregion

namespace RKiss.Tools.AzureIoTHubTester
{
    public class ProgressNode : System.Timers.Timer, IDisposable
    {
        Form control;
        System.Windows.Forms.TreeNode node;
        int counter = 0;

        public int EndImageIndex { get; set; }

        public ProgressNode(Form control, System.Windows.Forms.TreeNode selectedNode, int baseImageIndex, int endImageIndex)
        {
            this.node = selectedNode;
            this.control = control;
            this.EndImageIndex = endImageIndex;

            this.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e)
            {
                int index = baseImageIndex + (counter & 7);
                control.InvokeEx(() => node.SelectedImageIndex = node.ImageIndex = index);
                counter++;
            };

            this.Interval = 300;
            this.Start();
            this.Enabled = true;

        }
        void IDisposable.Dispose()
        {
            this.Stop();
            this.Enabled = false;
            control.InvokeEx(() => node.SelectedImageIndex = node.ImageIndex = this.EndImageIndex);
        }
    }   
}