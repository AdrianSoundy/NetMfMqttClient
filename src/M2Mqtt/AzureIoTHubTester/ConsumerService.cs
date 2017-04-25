//*****************************************************************************
//    Description.....Axure IoT Hub Tester
//                                
//    Author..........Roman Kiss, rkiss@pathcom.com
//    Copyright © 2011 ATZ Consulting Inc. (see included license.rtf file)         
//                        
//    Date Created:    11/11/11
//
//    Date        Modified By     Description
//-----------------------------------------------------------------------------
//    11/11/11   Roman Kiss     Initial Revision
//
//*****************************************************************************
//
#region Namespaces
using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
#endregion

namespace RKiss.Tools.AzureIoTHubTester
{
    [ServiceBehavior(AddressFilterMode=AddressFilterMode.Any)]
    public class VirtualService : IGenericOneWayContract
    {       
        public void ProcessMessage(System.ServiceModel.Channels.Message msg)
        {
            Trace.WriteLine("VirtualService: *** Message has been received ***");
            Trace.WriteLine(msg.ToString());

            ChannelFactory<IGenericOneWayContract> factory = null;

            try
            {
                var config = OperationContext.Current.Host.Extensions.Find<ConfigData>();
                if (config == null)
                    throw new Exception("Fatal error: Missing ServiceConfigData extension object");

                var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
                var se = new ServiceEndpoint(ContractDescription.GetContract(typeof(IGenericOneWayContract)), binding, new EndpointAddress(config.TesterAddress));

                factory = new ChannelFactory<IGenericOneWayContract>(se);
                var channel = factory.CreateChannel();

                using (var msgbuffer = msg.CreateBufferedCopy(int.MaxValue))
                {

                    using (var scope = new OperationContextScope((IContextChannel)channel))
                    {
                        if (msg.Version == MessageVersion.None)
                        {
                            
                        }
                        else
                        {                         
                            OperationContext.Current.OutgoingMessageHeaders.Add(MessageHeader.CreateHeader(ConfigData.XName.LocalName, ConfigData.XName.NamespaceName, config));
                            channel.ProcessMessage(msgbuffer.CreateMessage());
                        }
                        Trace.WriteLine("VirtualService: --- Message has been sent to tester ---");

                        #region check for action
                       
                        #endregion
                    }
                    factory.Close();
                }
            }
            catch (CommunicationException ex)
            {
                if (factory != null)
                {
                    if (factory.State == CommunicationState.Faulted)
                        factory.Abort();
                    else if (factory.State != CommunicationState.Closed)
                        factory.Close();
                    factory = null;
                }
                Trace.WriteLine(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                if (factory != null)
                {
                    if (factory.State == CommunicationState.Faulted)
                        factory.Abort();
                    else if (factory.State != CommunicationState.Closed)
                        factory.Close();
                    factory = null;
                }
                Trace.WriteLine(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }

        }

        public void PostProcessMessage(string messageId, string status)
        {
            throw new NotImplementedException();
        }
    }
}


