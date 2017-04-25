//*****************************************************************************
//    Description.....Azure IoT Hub Tester
//                                
//    Author..........Roman Kiss, rkiss@pathcom.com
//    Copyright © 2011 ATZ Consulting Inc. (see included license.rtf file)         
//                        
//    Date Created:    12/12/16
//
//    Date        Modified By     Description
//-----------------------------------------------------------------------------
//    12/12/16   Roman Kiss     Initial Revision
//
//*****************************************************************************
//
#region Namespaces
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Xml.Linq;
using System.Linq;
using System.Xml.Xsl;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Threading.Tasks;

#endregion


namespace RKiss.Tools.AzureIoTHubTester
{

    public class NodeDefaults
    {
        public static string IoTHubs = "AzureIoTHubs";
        public static string IoTHub = "AzureIoTHub";
        public static string Devices = "Devices";
        public static string Device = "Device";
        public static string Twin = "twin";
        public static string Methods = "methods";
        public static string Messages = "messages";       
        public static string Message = "Message";
        public static string RESTAPI = "REST-API";
        public static string CategoryRESTAPI = "CategoryRESTAPI";
        public static string HttpRequest = "HttpRequest";

    }


    public class Templates
    {
        public static string FireMessage = @"
            <s:Envelope xmlns:s='http://www.w3.org/2003/05/soap-envelope' xmlns:a='http://www.w3.org/2005/08/addressing'>
                <s:Header>
                    <a:Action s:mustUnderstand='1' xmlns:a='http://www.w3.org/2005/08/addressing' xmlns:s='http://www.w3.org/2003/05/soap-envelope'>urn:rkiss.sb/tester/2011/11/INotify/Message</a:Action>
                </s:Header>
                <s:Body>
                    <Message xmlns='urn:rkiss.sb/tester/2011/11'>
                        <msg>Hello Service Bus</msg>
                    </Message>
                </s:Body>
            </s:Envelope>";

        public static string TelemetrySample = " { \"time\":\"$DateTime.UtcNow\", \"counter\":\"$Counter\", \"deviceId\":\"$DeviceID\", \"windSpeed\": \"$Sensor.WindSpeed\", \"temperature\":\"$Sensor.Temp\", \"humidity\":\"$Sensor.Humidity\" }";
    }

    #region ServiceHelper
    public static class ServiceHelper
    {
        public static async Task<string> GetDevicesAsync(string connectionString)
        {
            string iothubNamespace = SharedAccessSignatureBuilder.GetHostNameNamespaceFromConnectionString(connectionString);
            string sas = SharedAccessSignatureBuilder.GetSASTokenFromConnectionString(connectionString);
            string address = $"https://{iothubNamespace}.azure-devices.net/devices?top=100&api-version=2016-11-14";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", sas);
                client.DefaultRequestHeaders.Add("accept", "application/json");
                var response = client.GetAsync(address).Result;
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }

    public sealed class SharedAccessSignatureBuilder
    {
        public static string GetHostNameNamespaceFromConnectionString(string connectionString)
        {
            return GetPartsFromConnectionString(connectionString)["HostName"].Split('.').FirstOrDefault();
        }
        public static string GetSASTokenFromConnectionString(string connectionString, uint hours = 24)
        {
            var parts = GetPartsFromConnectionString(connectionString);
            return GetSASToken(parts["HostName"], parts["SharedAccessKey"], parts["SharedAccessKeyName"], hours);
        }
        public static string GetSASToken(string resourceUri, string key, string keyName = null, uint hours = 24)
        {
            var expiry = GetExpiry(hours);
            string stringToSign = System.Web.HttpUtility.UrlEncode(resourceUri) + "\n" + expiry;
            HMACSHA256 hmac = new HMACSHA256(Convert.FromBase64String(key));

            var signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));
            var sasToken = keyName == null ?
                String.Format(CultureInfo.InvariantCulture, "SharedAccessSignature sr={0}&sig={1}&se={2}", HttpUtility.UrlEncode(resourceUri), HttpUtility.UrlEncode(signature), expiry) :
                String.Format(CultureInfo.InvariantCulture, "SharedAccessSignature sr={0}&sig={1}&se={2}&skn={3}", HttpUtility.UrlEncode(resourceUri), HttpUtility.UrlEncode(signature), expiry, keyName);
            return sasToken;
        }

        #region Helpers
        public static Dictionary<string, string> GetPartsFromConnectionString(string connectionString)
        {
            return connectionString.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Split(new[] { '=' }, 2)).ToDictionary(x => x[0].Trim(), x => x[1].Trim());
        }
      
        // default expiring = 24 hours
        private static string GetExpiry(uint hours = 24)
        {
            TimeSpan sinceEpoch = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return Convert.ToString((int)sinceEpoch.TotalSeconds + 3600 * hours);
        }

        public static string CreateSHA256Key(string secret)
        {
            using (var provider = new SHA256CryptoServiceProvider())
            {
                byte[] keyArray = provider.ComputeHash(UTF8Encoding.UTF8.GetBytes(secret));
                provider.Clear();
                return Convert.ToBase64String(keyArray);
            }
        }

        public static string CreateRNGKey(int keySize = 32)
        {
            byte[] keyArray = new byte[keySize];
            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetNonZeroBytes(keyArray);
            }
            return Convert.ToBase64String(keyArray);
        }
        #endregion
    }
    #endregion
}