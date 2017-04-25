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
using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
#endregion

namespace RKiss.Tools.AzureIoTHubTester
{
    public class TesterService : IGenericOneWayContract
    {   
        public void ProcessMessage(Message message)
        {
            Trace.WriteLine("TesterService: === Message has been received ===");
            Trace.WriteLine(message.ToString());

            Form1 form = OperationContext.Current.Host.Extensions.Find<Form1>();
            string action = OperationContext.Current.IncomingMessageHeaders.Action;
            
            int indexConfig = message.Headers.FindHeader(ConfigData.XName.LocalName, ConfigData.XName.NamespaceName);
                        
            try
            {
                var config = message.Headers.GetHeader<ConfigData>(indexConfig);
                message.Headers.RemoveAt(indexConfig);

                //var payload = JsonConvert.DeserializeObject<uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs>(message.GetBody<string>());
                var payload = JsonConvert.DeserializeObject<MqttMsgEventArgs>(message.GetBody<string>());
                form.AddMessageToTreview(payload, config);                
            }
            catch (Exception ex)
            {
                form.ShowText(ex.Message);
                throw ex;
            }                       
        }

        public void PostProcessMessage(string messageId, string status)
        {
            Trace.WriteLine("TesterService: === PostMessage has been received ===");

            Form1 form = OperationContext.Current.Host.Extensions.Find<Form1>();
            string action = OperationContext.Current.IncomingMessageHeaders.Action;
            int indexConfig = OperationContext.Current.IncomingMessageHeaders.FindHeader(ConfigData.XName.LocalName, ConfigData.XName.NamespaceName);

            try
            {
                var config = OperationContext.Current.IncomingMessageHeaders.GetHeader<ConfigData>(indexConfig);
                form.UpdateStatusForMessageNode(messageId, status, config);
                                
            }
            catch (Exception ex)
            {
                form.ShowText(ex.Message);
                throw ex;
            }
        }
    }
}