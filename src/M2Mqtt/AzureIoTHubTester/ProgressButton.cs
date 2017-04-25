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
    public class ProgressButton : System.Timers.Timer, IDisposable
    {
        Form control;
        System.Windows.Forms.Button button;
        int counter = 0;
        int endImageIndex = 0;

        public ProgressButton(Form control, System.Windows.Forms.Button button, int baseImageIndex, int endImageIndex)
        {
            this.button = button;
            this.control = control;
            this.endImageIndex = endImageIndex;

            this.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e)
            {
                int index = baseImageIndex + (counter & 7);
                control.InvokeEx(() => button.ImageIndex = index);
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
            control.InvokeEx(() => button.ImageIndex = endImageIndex);
        }
    }   
}