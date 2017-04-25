///
/// NOTE: this demo uses the information outlined in https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-mqtt-support
/// It uses the M2MQTT library, however a bug with the SSLStream means that the library is not the nuget version and needed to be included.
/// The issue and fix is documented in https://github.com/eclipse/paho.mqtt.m2mqtt/issues/43
/// 
/// https://www.codeproject.com/Articles/1173356/WebControls/ is also a good resource and maybe useful for testing...

using System;
using System.Threading;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.Web;
using System.Security.Cryptography;
using Microsoft.SPOT;
using Microsoft.SPOT.Time;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;




namespace Gadgeteermqtt
{
    public partial class Program
    {

        static readonly string deviceID = "[enter device id]";
        static readonly string iotBrokerAddress = "[enter iothub url]";
        static readonly string SasKey = "[enter sas key]";

        static readonly string telemetryTopic = Fx.Format("devices/{0}/messages/events/", deviceID);
        const string twinReportedPropertiesTopic = "$iothub/twin/PATCH/properties/reported/";
        const string twinDesiredPropertiesTopic = "$iothub/twin/GET/";

        private static bool timeSynchronized;

        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            /*******************************************************************************************
            Modules added in the Program.gadgeteer designer view are used by typing 
            their name followed by a period, e.g.  button.  or  camera.
            
            Many modules generate useful events. Type +=<tab><tab> to add a handler to an event, e.g.:
                button.ButtonPressed +=<tab><tab>
            
            If you want to do something periodically, use a GT.Timer and handle its Tick event, e.g.:
                GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
                timer.Tick +=<tab><tab>
                timer.Start();
            *******************************************************************************************/

            // Use Trace to show messages in Visual Studio's "Output" window during debugging.
            Trace("Program Started");

            if (ethernetENC28.NetworkInterface.Opened)
                ethernetENC28.NetworkInterface.Close();

            ethernetENC28.NetworkInterface.EnableDhcp();
            ethernetENC28.NetworkInterface.EnableDynamicDns();
            ethernetENC28.NetworkUp += ethernetENC28_NetworkUp;
            ethernetENC28.UseThisNetworkInterface();

            TimeService.SystemTimeChanged +=
                new SystemTimeChangedEventHandler(TimeService_SystemTimeChanged);
            TimeService.TimeSyncFailed +=
                new TimeSyncFailedEventHandler(TimeService_TimeSyncFailed);

         
        }

        private static void Client_MqttMsgReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Trace(Fx.Format("Message received on topic: {0}", e.Topic));
            Trace(Fx.Format("The message was: {0}", new string(Encoding.UTF8.GetChars(e.Message))));

            if (e.Topic.StartsWith("$iothub/twin/PATCH/properties/desired/"))
            {
                Trace("and received desired properties.");
            }
            else if (e.Topic.StartsWith("$iothub/twin/"))
            {
                
                if (e.Topic.IndexOf("res/400/") > 0 || e.Topic.IndexOf("res/404/") > 0 || e.Topic.IndexOf("res/500/") > 0)
                    Trace("and was in the error queue.");
                else
                    Trace("and was in the success queue.");
            }
            else if (e.Topic.StartsWith("$iothub/methods/POST/"))
            {
                Trace("and was a method.");
            }
            else if (e.Topic.StartsWith(Fx.Format("devices/{0}/messages/devicebound/", deviceID)))
            {
                Trace("and was a message for the device.");
            }
            else if (e.Topic.StartsWith("$iothub/clientproxy/"))
            {
                Trace("and the device has been disconnected.");
            }
            else if (e.Topic.StartsWith("$iothub/logmessage/Info"))
            {
                Trace("and was in the log message queue.");
            }
            else if (e.Topic.StartsWith("$iothub/logmessage/HighlightInfo"))
            {
                Trace("and was in the Highlight info queue.");
            }
            else if (e.Topic.StartsWith("$iothub/logmessage/Error"))
            {
                Trace("and was in the logmessage error queue.");
            }
            else if (e.Topic.StartsWith("$iothub/logmessage/Warning"))
            {
                Trace("and was in the logmessage warning queue.");
            }
        }

        private static void Client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Trace(Fx.Format("Response from publish with message id: {0}", e.MessageId.ToString()));
        }

        private static void Client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Trace(Fx.Format("Response from subscribe with message id: {0}", e.MessageId.ToString()));
        }
        private static void client_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            Trace(Fx.Format("Response from unsubscribe with message id: {0}", e.MessageId.ToString()));
        }

        private static void Client_ConnectionClosed(object sender, EventArgs e)
        {
            Trace("Connection closed");
        }



        void ethernetENC28_NetworkUp(GTM.Module.NetworkModule sender, GTM.Module.NetworkModule.NetworkState state)
        {
            TimeServiceSettings settings = new TimeServiceSettings();
            settings.ForceSyncAtWakeUp = true;
            settings.RefreshTime = 1800;    // in seconds.

            IPAddress[] address = Dns.GetHostEntry("uk.pool.ntp.org").AddressList;
            if (address != null && address.Length > 0)
                settings.PrimaryServer = address[0].GetAddressBytes();
            if (address != null && address.Length > 1)
                settings.AlternateServer = address[1].GetAddressBytes();
 
            TimeService.Settings = settings;

            //Set to UTC!
            TimeService.SetTimeZoneOffset(0);
 
            TimeService.Start();            
        }



        private static readonly long UtcReference = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).Ticks;

        static string GetSharedAccessSignature(string keyName, string sharedAccessKey, string resource, TimeSpan tokenTimeToLive)
        {
            // http://msdn.microsoft.com/en-us/library/azure/dn170477.aspx
            // the canonical Uri scheme is http because the token is not amqp specific
            // signature is computed from joined encoded request Uri string and expiry string

            // needed in .Net Micro Framework to use standard RFC4648 Base64 encoding alphabet
            System.Convert.UseRFC4648Encoding = true;

            string expiry = ((long)(DateTime.UtcNow - new DateTime(UtcReference, DateTimeKind.Utc) + tokenTimeToLive).TotalSeconds()).ToString();
            string encodedUri = HttpUtility.UrlEncode(resource);

            byte[] hmac = SHA.computeHMAC_SHA256(Convert.FromBase64String(sharedAccessKey), Encoding.UTF8.GetBytes(encodedUri + "\n" + expiry));
            string sig = Convert.ToBase64String(hmac);

            if (keyName != null)
            {
                return Fx.Format(
                "SharedAccessSignature sr={0}&sig={1}&se={2}&skn={3}",
                encodedUri,
                HttpUtility.UrlEncode(sig),
                HttpUtility.UrlEncode(expiry),
                HttpUtility.UrlEncode(keyName));
            }
            else
            {
                return Fx.Format(
                    "SharedAccessSignature sr={0}&sig={1}&se={2}",
                    encodedUri,
                    HttpUtility.UrlEncode(sig),
                    HttpUtility.UrlEncode(expiry));
            }
        }


        private static void TimeService_TimeSyncFailed(object sender, TimeSyncFailedEventArgs e)
        {
            Trace(Fx.Format("Error synchronizing system time with NTP server: ", e.ErrorCode));
        }

        private static void TimeService_SystemTimeChanged(object sender, SystemTimeChangedEventArgs e)
        {
            Trace("Network time received.");
            if (!timeSynchronized)
            {
                timeSynchronized = true;
                DoMqttStuff();
            }
        }

        private static void DoMqttStuff()
        {

            //Create MQTT Client with default port 8883 using TLS protocol
            MqttClient mqttc = new MqttClient(iotBrokerAddress, 8883, true, null, null, MqttSslProtocols.TLSv1_0);


            // event when connection has been dropped
            mqttc.ConnectionClosed += Client_ConnectionClosed;

            // handler for received messages on the subscribed topics
            mqttc.MqttMsgPublishReceived += Client_MqttMsgReceived;

            // handler for publisher
            mqttc.MqttMsgPublished += Client_MqttMsgPublished;

            // handler for subscriber 
            mqttc.MqttMsgSubscribed += Client_MqttMsgSubscribed;

            // handler for unsubscriber
            mqttc.MqttMsgUnsubscribed += client_MqttMsgUnsubscribed;


            byte code = mqttc.Connect(
                deviceID,
                Fx.Format("{0}/{1}/api-version=2016-11-14", iotBrokerAddress, deviceID),
                GetSharedAccessSignature(null, SasKey, Fx.Format("{0}/devices/{1}", iotBrokerAddress, deviceID), new TimeSpan(24, 0, 0)), 
                false, 
                MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, 
                false, "$iothub/twin/GET/?$rid=999", 
                "Disconnected", 
                false, 
                60
                );

            if (mqttc.IsConnected)
            {
            Trace("subscribing to topics");
            mqttc.Subscribe(
                new[] { 
                "$iothub/methods/POST/#", 
                Fx.Format("devices/{0}/messages/devicebound/#", deviceID), 
                "$iothub/twin/PATCH/properties/desired/#", 
                "$iothub/twin/res/#" 
                },
                new[] { 
                    MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, 
                    MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, 
                    MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, 
                    MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE 
                }
                );


                Trace("Sending twin properties");
                mqttc.Publish(Fx.Format("{0}?$rid={1}", twinReportedPropertiesTopic, Guid.NewGuid()), Encoding.UTF8.GetBytes("{ \"Firmware\": \"NetMF\"}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);


                Trace("Getting twin properties");
                mqttc.Publish(Fx.Format("{0}?$rid={1}", twinDesiredPropertiesTopic, Guid.NewGuid()), Encoding.UTF8.GetBytes(""), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);


                Trace("[MQTT Client] Start to send telemetry");
            }

            int temp = 10;
            while (mqttc.IsConnected)
            {

                // get temperature value... 
                temp = temp + 1;
                // ...publish it to the broker 



                //Publish telemetry data using AT LEAST ONCE QOS Level
                mqttc.Publish(telemetryTopic, Encoding.UTF8.GetBytes("{ Temperature: " + temp + "}"), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);

                Trace(Fx.Format("{0} [MQTT Client] Sent telemetry { Temperature: {1} }", DateTime.UtcNow.ToString(), temp.ToString()));

                Thread.Sleep(1000 * 60);
                if (temp > 30) temp = 10;

            }

            Trace(Fx.Format("{0} [MQTT Client]" + " is Disconnected", DateTime.UtcNow.ToString()));
        }


        [Conditional("DEBUG")]
        static void Trace(string message)
        {
            if (Debugger.IsAttached)
                Debug.Print(message);
        }

    }
}
