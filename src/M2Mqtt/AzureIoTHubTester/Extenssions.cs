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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using uPLibrary.Networking.M2Mqtt.Messages;
#endregion


namespace RKiss.Tools.AzureIoTHubTester
{
    #region Exception
    public static class ExceptionExtensions
    {
        public static Exception AddData(this Exception exception, string name, string value)
        {
            return exception.AddData<Exception>(name, value);
        }

        public static T AddData<T>(this T exception, string name, string value) where T : Exception
        {
            if (value == null)
                value = string.Empty;

            if (exception.Data.Contains(name))
                exception.Data[name] = value;
            else
                exception.Data.Add(name, value);
            return exception;
        }

        public static string InnerMessage(this Exception exception, string filter = null, string errorText = "")
        {
            
            var ex = exception;
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            if (string.IsNullOrEmpty(filter))
                return ex.Message;
            else
                return ex.Message.IndexOf(filter) < 1 ? ex.Message : errorText;
        }
    }
    #endregion

    public static class MessageExtension
    {
        /// <summary>
        /// Write Message to the string
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string WriteToString(this System.ServiceModel.Channels.Message message)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                Indent = true,
                Encoding = UTF8Encoding.UTF8
            };

            using (XmlDictionaryWriter dwr = XmlDictionaryWriter.CreateDictionaryWriter(XmlDictionaryWriter.Create(sb, settings)))
            {
                dwr.WriteStartDocument();
                message.WriteMessage(dwr);
                dwr.WriteEndDocument();
                dwr.Flush();
            }
            return sb.ToString();
        }

        /// <summary>
        /// deserialize xmlmessage to the object Message
        /// </summary>
        /// <param name="xmlmessage"></param>
        /// <returns></returns>
        public static System.ServiceModel.Channels.Message CreateMessage(string xmlmessage, bool bMessageVersion = true)
        {
            if (!bMessageVersion && !xmlmessage.TrimStart().StartsWith("<") && !xmlmessage.TrimEnd().StartsWith(">"))
                return System.ServiceModel.Channels.Message.CreateMessage(MessageVersion.None, "*", xmlmessage);

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xmlmessage));
            XmlDictionaryReader xdr = XmlDictionaryReader.CreateTextReader(ms, new XmlDictionaryReaderQuotas());
            MessageVersion ver = bMessageVersion ? GetMessageVersion(xmlmessage) : MessageVersion.None;
            return System.ServiceModel.Channels.Message.CreateMessage(xdr, Int32.MaxValue, ver);
        }
        
        /// <summary>
        /// Get MeessageVersion from the message represented by xml text
        /// </summary>
        /// <param name="xmltext"></param>
        /// <returns></returns>
        public static MessageVersion GetMessageVersion(string xmltext)
        {
            if (string.IsNullOrEmpty(xmltext))
                return null;
            var root = XElement.Parse(xmltext);
            return GetMessageVersion(root);
        }

        /// <summary>
        /// Get MessageVersion from the Message represented by XElement
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static MessageVersion GetMessageVersion(XElement root)
        {
            var envelope = root.DescendantsAndSelf().FirstOrDefault(e => e.Name.LocalName == "Envelope");
            if (envelope != null)
            {
                string nsEnv = envelope.Name.NamespaceName;
                var action = envelope.Descendants().FirstOrDefault(e => e.Name.LocalName == "Action");
                if (action != null)
                {
                    string nsAction = action.Name.NamespaceName;
                    if (!string.IsNullOrEmpty(nsEnv) && !string.IsNullOrEmpty(nsAction))
                    {
                        EnvelopeVersion envVersion = null;
                        if (nsEnv == "http://schemas.xmlsoap.org/soap/envelope/")
                            envVersion = EnvelopeVersion.Soap11;
                        else if (nsEnv == "http://www.w3.org/2003/05/soap-envelope")
                            envVersion = EnvelopeVersion.Soap12;
                        else if (nsEnv == "http://schemas.microsoft.com/ws/2005/05/envelope/none")
                            envVersion = EnvelopeVersion.None;

                        if (envVersion != null)
                        {
                            AddressingVersion addrVersion = null;
                            if (nsAction == "http://www.w3.org/2005/08/addressing")
                                addrVersion = AddressingVersion.WSAddressing10;
                            else if (nsAction == "http://schemas.xmlsoap.org/ws/2004/08/addressing")
                                addrVersion = AddressingVersion.WSAddressingAugust2004;
                            else if (nsAction == "http://schemas.microsoft.com/ws/2005/05/addressing/none")
                                addrVersion = AddressingVersion.None;
                            if (addrVersion != null)
                            {
                                return System.ServiceModel.Channels.MessageVersion.CreateVersion(envVersion, addrVersion);
                            }
                        }
                    }
                }
            }
            return System.ServiceModel.Channels.MessageVersion.None;
        }
    }

    public static class BindingExtension
    {
        public static BindingList<T> ToBindingList<T>(this IList<T> source)
        {
            return new BindingList<T>(source);
        }


    }

    // thanks for http://stackoverflow.com/questions/17616239/c-sharp-extend-class-by-adding-properties
    public static class MqttMsgPublishEventArgsExtension
    {
        static readonly ConditionalWeakTable<MqttMsgPublishEventArgs, StringObject> TS = new ConditionalWeakTable<MqttMsgPublishEventArgs, StringObject>();

        public static string GetTimestamp(this MqttMsgPublishEventArgs args) { return TS.GetOrCreateValue(args).Value; }

        public static void SetTimestamp(this MqttMsgPublishEventArgs args, string ts) { TS.GetOrCreateValue(args).Value = ts; }

       
        class StringObject
        {
            public string Value { get; set; }
        }
    }


    // stolen from http://stackoverflow.com/questions/783925/control-invoke-with-input-parameters
    public static class ControlExtensions
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

    // stolen from http://stackoverflow.com/questions/1124597/why-isnt-there-an-xml-serializable-dictionary-in-net and modified by Roman Kiss
    public static class SerializationExtensions
    {
        public static XElement Serialize<T>(this T obj)
        {
            var serializer = new System.Runtime.Serialization.DataContractSerializer(obj.GetType());
            using(var ms = new MemoryStream())
            using (var xwr = new XmlTextWriter(ms, Encoding.UTF8))
            {
                serializer.WriteObject(xwr, obj);
                xwr.Flush();
                ms.Position = 0;
                return XElement.Load(ms);
            }
        }
        public static T Deserialize<T>(this XElement serialized)
        {
            var serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
            using (var reader = new StringReader(serialized.ToString()))
            using (var stm = new XmlTextReader(reader))
            {
                return (T)serializer.ReadObject(stm);
            }
        }

        public static T Copy<T>(this T obj)
        {
            return (T)obj.Serialize<T>().Deserialize<T>();
        }
    }

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color, bool bScroll = true)
        {
            if (text == null)
                return;

            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            //box.SelectionFont = font;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
            if (bScroll)
            {
                box.SelectionStart = box.TextLength;
                box.ScrollToCaret();
            }
        }
    }
    public static class ServiceBusHelper
    {
        public static HttpResponseMessage GetResourceDescription(string addressWithKey)
        {
            addressWithKey = HttpUtility.HtmlDecode(HttpUtility.UrlDecode(addressWithKey));
            string qAddress = GetResourceAddress(addressWithKey);
            string token = GetToken(addressWithKey);

            var httpClient = new HttpClient() { BaseAddress = new Uri(qAddress) };
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
            return httpClient.GetAsync(string.Empty).Result;
        }

        public static HttpResponseMessage SendMessageToSB(string addressWithKey, HttpContent content, dynamic brokerProperties, dynamic userProperties = null)
        {
            addressWithKey = HttpUtility.HtmlDecode(HttpUtility.UrlDecode(addressWithKey));
            string qAddress = GetResourceAddress(addressWithKey);
            string token = GetToken(addressWithKey);

            var httpClient = new HttpClient() { BaseAddress = new Uri(qAddress) };
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);

            // the brokerProperties must be in the JSON formatted text
            if (brokerProperties != null && brokerProperties is string)
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("BrokerProperties", brokerProperties);
            else if (brokerProperties != null && brokerProperties is object)
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("BrokerProperties", JsonConvert.SerializeObject(brokerProperties));          

            if (userProperties != null)
            {
                // NameValueCollection or Dictionary or anonymous type
                if (userProperties as NameValueCollection != null)
                {
                    foreach (string key in (userProperties as NameValueCollection).AllKeys)
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, (userProperties as NameValueCollection)[key]);
                }
                else if (userProperties as Dictionary<string, string> != null)
                {
                    foreach (string key in (userProperties as Dictionary<string, string>).Keys)
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, (userProperties as Dictionary<string, string>)[key]);
                }
                else
                {
                    // anonymous type
                    foreach (var property in userProperties.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        if (property.CanRead)
                            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(property.Name, property.GetValue(userProperties, null));
                    }
                }
            }
            return httpClient.PostAsync("messages", content).Result;
        }

        public static bool IsServiceBusResource(string resourceUri)
        {
            return new Uri(resourceUri).Host.IndexOf(".servicebus.windows.net") > 0;
        }
        public static string GetResourceAddress(string resourceUriWithKeys, string postfix = "")
        {
            return resourceUriWithKeys.Split(new char[] { '?' }, 2)[0].Trim('/') + "/" + postfix;
        }
        public static string GetSASToken(string resourceUriWithKeys)
        {
            var a = resourceUriWithKeys.Split(new char[] { '?' }, 2);
            string qAddress = a[0] + "/";
            var keys = HttpUtility.ParseQueryString(a[1]);
            return GetSASToken(qAddress, keys["skn"], keys["skk"].Replace(' ', '+'));
        }
        public static string GetSASToken(string resourceUri, string keyName, string key)
        {
            var expiry = GetExpiryInSeconds();
            string stringToSign = HttpUtility.UrlEncode(resourceUri) + "\n" + expiry;
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));

            var signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));
            var sasToken = String.Format(CultureInfo.InvariantCulture, "SharedAccessSignature sr={0}&sig={1}&se={2}&skn={3}",
                HttpUtility.UrlEncode(resourceUri), HttpUtility.UrlEncode(signature), expiry, keyName);
            return sasToken;
        }
        
        public static string GetAcsToken(string acsNamespace, string connectionstring, string appliesToAddress)
        {
            string acsAddress = GetAcsAddress(acsNamespace);
            var col = CreateNameValueCollectionFromConnectionString(connectionstring);
            return GetAcsToken(acsAddress, col["SharedSecretIssuer"], col["SharedSecretValue"], appliesToAddress);
        }
        public static string GetAcsToken(string acsAddress, string issuerName, string issuerKey, string appliesToAddress)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(acsAddress + "/WRAPV0.9");
                return client.GetAcsToken(issuerName, issuerKey, appliesToAddress);
            }
        }
        public static string GetAcsToken(this HttpClient client, string connectionstring, string appliesToAddress)
        {
            var col = CreateNameValueCollectionFromConnectionString(connectionstring);
            return GetAcsToken(client, col["SharedSecretIssuer"], col["SharedSecretValue"], appliesToAddress);
        }
        public static string GetAcsToken(this HttpClient client, string issuerName, string issuerKey, string appliesToAddress)
        {
            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("wrap_name", issuerName),
                new KeyValuePair<string, string>("wrap_password", issuerKey),
                new KeyValuePair<string, string>("wrap_scope", new UriBuilder(appliesToAddress) { Scheme = "http", Port = 80 }.ToString())
            };

            var content = new FormUrlEncodedContent(values);
            var response = client.PostAsync("WRAPv0.9/", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                // Extract the SWT token and return it.
                return result
                    .Split('&')
                    .Single(value => value.StartsWith("wrap_access_token=", StringComparison.OrdinalIgnoreCase))
                    .Split('=')[1];
            }
            return null;
        }
        public static string GetAcsAddress(string acsNamespace)
        {
            UriBuilder acsUri = new UriBuilder("https://" + acsNamespace + ".accesscontrol.windows.net");
            return acsUri.ToString();
        }
        public static string GetAcsNamespace(string connectionString, string postfix = "-sb")
        {
            var col = CreateNameValueCollectionFromConnectionString(connectionString);
            if(col != null)
                return new UriBuilder(col["Endpoint"]).Host.Split('.').FirstOrDefault() + postfix;
            else
                return new UriBuilder(connectionString).Host.Split('.').FirstOrDefault() + postfix;
        }  

        public static string GetAddressFromConnectionString(string connectionString)
        {
            var col = CreateNameValueCollectionFromConnectionString(connectionString);
            if(col != null)
            {
                if (col.AllKeys.Contains("Endpoint") && col.AllKeys.Contains("SharedSecretIssuer"))
                {
                    return string.Format("{0}XXX?ssi={1}&ssv={2}", col["endpoint"].Replace("sb://", "https://"), col["SharedSecretIssuer"], col["SharedSecretValue"]);
                }
                else if (col.AllKeys.Contains("Endpoint") && col.AllKeys.Contains("SharedAccessKeyName"))
                {
                    return string.Format("{0}XXX?skn={1}&skk={2}", col["endpoint"].Replace("sb://", "https://"), col["SharedAccessKeyName"], col["SharedAccessKey"]);
                }                
            }
            return null;
        }

        private static string GetToken(string resourceUriWithKeys, string acsPostfix = "-sb")
        {
            var a = resourceUriWithKeys.Split(new char[] { '?' }, 2);
            var keys = HttpUtility.ParseQueryString(a[1]);
            if (string.IsNullOrEmpty(keys["skn"]) == false)
            {
                return GetSASToken(resourceUriWithKeys);
            }
            else
            {
                string acsNamespace = GetAcsNamespace(resourceUriWithKeys, acsPostfix);
                string acsAddress = GetAcsAddress(acsNamespace);
                string acsToken = GetAcsToken(acsAddress, keys["ssi"], keys["ssv"].Replace(' ', '+'), a[0]);
                return string.Format("WRAP access_token=\"{0}\"", HttpUtility.UrlDecode(acsToken));
            }         
        }

        public static string GetExpiryInSeconds()
        {
            TimeSpan sinceEpoch = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return Convert.ToString((int)sinceEpoch.TotalSeconds + 3600);
        }

        // from Microsoft.ServiceBus.Messaging
        public static NameValueCollection CreateNameValueCollectionFromConnectionString(string connectionString)
        {
            Regex KeyRegex = new Regex("(OperationTimeout|Endpoint|RuntimePort|ManagementPort|StsEndpoint|WindowsDomain|WindowsUsername|WindowsPassword|OAuthDomain|OAuthUsername|OAuthPassword|SharedSecretIssuer|SharedSecretValue|SharedAccessKeyName|SharedAccessKey|TransportType)", RegexOptions.IgnoreCase);
            Regex ValueRegex = new Regex(@"([^\s]+)");

            NameValueCollection values = new NameValueCollection();
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                string[] strArray = Regex.Split(";" + connectionString, ";(OperationTimeout|Endpoint|RuntimePort|ManagementPort|StsEndpoint|WindowsDomain|WindowsUsername|WindowsPassword|OAuthDomain|OAuthUsername|OAuthPassword|SharedSecretIssuer|SharedSecretValue|SharedAccessKeyName|SharedAccessKey|TransportType)=", RegexOptions.IgnoreCase);
                if (strArray.Length <= 0)
                {
                    return values;
                }
                if (!string.IsNullOrWhiteSpace(strArray[0]))
                {
                    return null;                      
                }
                if ((strArray.Length % 2) != 1)
                {
                    return null;   
                }
                for (int i = 1; i < strArray.Length; i++)
                {
                    string str2 = strArray[i];
                    if (string.IsNullOrWhiteSpace(str2) || !KeyRegex.IsMatch(str2))
                    {
                        return null;   
                    }
                    string str3 = strArray[i + 1];
                    if (string.IsNullOrWhiteSpace(str3) || !ValueRegex.IsMatch(str3))
                    {
                        return null;   
                    }
                    if (values[str2] != null)
                    {
                        return null;   
                    }
                    values[str2] = str3;
                    i++;
                }
            }
            return values;
        }

        
    }
}