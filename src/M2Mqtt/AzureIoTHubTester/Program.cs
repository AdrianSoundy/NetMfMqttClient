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
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows.Forms;
using System.Linq;
using System.Configuration;
#endregion

namespace RKiss.Tools.AzureIoTHubTester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            ServiceHost host = null;

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Form1 form = new Form1();

                try
                {
                    string uriAddressString = null;

                    string ports = ConfigurationManager.AppSettings["rangeOfPorts"];
                    int[] rangeOfPorts = string.IsNullOrEmpty(ports) ?
                        new int[] { 10100, 10101, 10102, 10103, 10104, 10105, 10106, 10107, 10108, 10109 } :
                        ports.Split(',').Select(n => Convert.ToInt32(n)).ToArray();

                    var usedPorts = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners();
                    for (int ii = 0; ii < rangeOfPorts.Length; ii++)
                    {
                        if (usedPorts.FirstOrDefault(p => p.Port == rangeOfPorts[ii]) == null)
                        {
                            uriAddressString = string.Format(@"http://localhost:{0}/sb", rangeOfPorts[ii]);
                            break;
                        }
                    };

                    if (string.IsNullOrEmpty(uriAddressString))
                        throw new Exception("Not available port in the range 10100-10109"); 

                    // interprocess communications
                    var endpointAddress = new EndpointAddress("net.pipe://localhost/AzureIoTHubTester_" + Process.GetCurrentProcess().Id);
                    var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
                    var se = new ServiceEndpoint(ContractDescription.GetContract(typeof(IGenericOneWayContract)), binding, endpointAddress);              

                    host = new ServiceHost(typeof(TesterService));
                    host.AddServiceEndpoint(se);

                    host.Extensions.Add(form);
                    host.Open();

                }
                catch (Exception ex)
                {
                    form.ShowText(ex.Message);
                }


               // form.Text = string.Format("[{0}] Axure IoT Hub Tester", (form.Tag as Uri).Port);
                Application.Run(form);

            }
            finally
            {
                if (host != null && host.State == CommunicationState.Faulted)
                    host.Abort();
                else if (host != null && host.State == CommunicationState.Opened)
                    host.Close();
            }

        }
    }
}
