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
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
#endregion

namespace RKiss.Tools.AzureIoTHubTester.UserControls
{

    public partial class RESTClient : UserControl
    {
        List<string> listOfSuccess = new List<string>() { "OK", "NoContent" };
        HttpMethod method { get; set; }

        public event SendEventHandler RaiseSendButton;

        public RESTClient()
        {
            InitializeComponent();
            this.labelLoadingTime.Visible = false;
            this.labelStatus.Text = string.Empty;
            this.labelLoadingTime.Text = string.Empty;
            method = HttpMethod.Get;
            this.requestResponseMessages.HideRequestPayload = true;

        }

        #region Group of Radio Buttons
        private void radioButtonGet_CheckedChanged(object sender, EventArgs e)
        {
            this.method = HttpMethod.Get;
            this.requestResponseMessages.HideRequestPayload = true;
            this.requestResponseMessages.FixedClientPanel = FixedPanel.Panel1;

        }

        private void radioButtonPOST_CheckedChanged(object sender, EventArgs e)
        {
            this.method = HttpMethod.Post;
            this.requestResponseMessages.HideRequestPayload = false;
        }

        private void radioButtonPUT_CheckedChanged(object sender, EventArgs e)
        {
            this.method = HttpMethod.Put;
            this.requestResponseMessages.HideRequestPayload = false;
        }

        private void radioButtonDELETE_CheckedChanged(object sender, EventArgs e)
        {
            this.method = HttpMethod.Delete;
            this.requestResponseMessages.HideRequestPayload = false;
        }

        private void radioButtonPATCH_CheckedChanged(object sender, EventArgs e)
        {
            this.method = new HttpMethod("PATCH");
            this.requestResponseMessages.HideRequestPayload = false;
        }
        #endregion

        public string ErrorStatus
        {
            set
            {
                this.labelStatus.ForeColor = Color.Red;
                this.labelStatus.Visible = true;
                this.labelStatus.Text = string.IsNullOrEmpty(value) ? string.Empty : $"Status: {value}";
            }
        }
        public string Status
        {
            set
            {
                this.labelStatus.ForeColor = Color.Green;
                this.labelStatus.Visible = true;
                this.labelStatus.Text = string.IsNullOrEmpty(value) ? string.Empty : $"Status: {value}";
            }
        }
        public long LoadingTime
        {
            set
            {
                if (value > 0)
                {
                    this.labelLoadingTime.Visible = true;
                    this.labelLoadingTime.Text = $"RspTime: {value}ms";
                }
                else
                    this.labelLoadingTime.Visible = false;
            }
        }
        public string Response { set { this.requestResponseMessages.ResponseMessage = value; } }

        public string ResponseStatus
        {
            set
            {
                this.requestResponseMessages.ResponseMessage = string.Empty;
                if (string.IsNullOrEmpty(value) == false)
                {
                    this.requestResponseMessages.AddTextToResponseMessage(value, Color.Gray);
                    this.requestResponseMessages.AddTextToResponseMessage("\r\n\r\nPayload:\r\n", Color.Gray);
                }
            }
        }

        public string ResponsePayload
        {
            set
            {
                if (string.IsNullOrEmpty(value) == false)
                {
                    this.requestResponseMessages.AddTextToResponseMessage(value, Color.Blue, false);
                    this.requestResponseMessages.AddTextToResponseMessage("\r\n", false);
                }
            }
        }


        public void EnableNext(string token)
        {
            this.checkBoxNext.Tag = token;
            this.checkBoxNext.Visible = !string.IsNullOrEmpty(token);
            this.checkBoxNext.Checked = !string.IsNullOrEmpty(token);
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            this.labelLoadingTime.Visible = false;
            this.labelLoadingTime.Text = string.Empty;
            this.labelStatus.Visible = false;
            this.labelStatus.Text = string.Empty;
            this.requestResponseMessages.ResponseMessage = string.Empty;

            if (this.RaiseSendButton != null)
            {
                try
                {
                    SendEventArgs eventArgs = null;             
                    this.InvokeEx(delegate ()
                    {
                        HttpRequestMessage hrm = new HttpRequestMessage(this.method, this.textBoxUrl.Text);

                        string payload = this.requestResponseMessages.RequestPayload.Replace("$DateTime.UtcNow", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture));

                        if (this.method != HttpMethod.Get)
                            hrm.Content = new StringContent(JRaw.Parse(payload).ToString(), Encoding.UTF8, "application/json");

                        string hdrsText = this.requestResponseMessages.RequestHeaders;
                        if (string.IsNullOrEmpty(hdrsText) == false)
                        {
                            string[] lines = hdrsText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string line in lines)
                            {
                                var parts = line.Split(new char[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries);
                                string hdrname = parts.First().Trim();
                                hrm.Headers.TryAddWithoutValidation(parts.First().Trim(), parts.Last().Trim());
                            }
                            if(hrm.Headers.Contains("x-ms-continuation") == false && this.checkBoxNext.Visible && this.checkBoxNext.Checked && this.checkBoxNext.Tag != null)
                            {
                                hrm.Headers.TryAddWithoutValidation("x-ms-continuation", Convert.ToString(this.checkBoxNext.Tag));
                            }
                        }
                        this.checkBoxNext.Visible = false;
                        this.checkBoxNext.Checked = false;
                        this.checkBoxNext.Tag = null;
                        eventArgs = new SendEventArgs() { Request = hrm };
                    });

                    this.RaiseSendButton(sender, eventArgs);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerMessage(), "Send HttpRequest failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.InvokeEx(delegate () { this.buttonSend.Enabled = true; });
                }
            }
        }

        /// <summary>
        /// Set request/response properties
        /// </summary>
        /// <param name="jsonText"></param>
        /// <param name="sas"></param>
        public void SetState(string jsonText, string azureiothubNamespace, string sas)
        {
            try
            {
                string address = $"https://{azureiothubNamespace}.azure-devices.net";
                var reqSchema = new { category = "", name = "", method = "", url = "", headers = "", payload = new JObject(), description = "", _rspLabel = "", _rspStatus = "", _rspTime = 0L, _rspPayload = ""};
                var request = JsonConvert.DeserializeAnonymousType(jsonText, reqSchema);

                this.labelStatus.Visible = false;
                this.textBoxUrl.Text = request.url.StartsWith("http") ? request.url : address.TrimEnd('/') + request.url;
                if (request.method == "GET")
                    this.radioButtonGet.Checked = true;
                else if (request.method == "PUT")
                    this.radioButtonPUT.Checked = true;
                else if (request.method == "POST")
                    this.radioButtonPOST.Checked = true;
                else if (request.method == "DELETE")
                    this.radioButtonDELETE.Checked = true;
                else if (request.method == "PATCH")
                    this.radioButtonPATCH.Checked = true;
                this.requestResponseMessages.RequestHeaders = string.IsNullOrEmpty(request.headers) ? string.Empty : request.headers.Replace("|", "\r\n");
                this.requestResponseMessages.RequestPayload = request.payload == null ? "{}" : request.payload.ToString();

                this.SetResponse(request._rspLabel, request._rspStatus, request._rspTime, request._rspPayload);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerMessage(), "RestClient: SetState");
            }
        }

        /// <summary>
        /// set response properties
        /// </summary>
        /// <param name="jsonText"></param>
        /// <param name="sas"></param>
        public void SetResponse(string label, string status, long time, string jsonText)
        {          
            this.LoadingTime = time;
            if (listOfSuccess.Contains(label))
                this.Status = label ?? string.Empty;
            else
                this.ErrorStatus = label ?? string.Empty;
            this.ResponseStatus = status ?? string.Empty;
            try
            {
                this.ResponsePayload = JRaw.Parse(jsonText).ToString(Newtonsoft.Json.Formatting.Indented);
            }
            catch
            {
                this.ResponsePayload = jsonText;
            }
        }

        // serialize request/response properties
        public string GetState(bool bIncludeResponse = true)
        {
            return string.Empty;
        }

        public string UpdateState(string jsonText)
        {
            try
            {
                dynamic state = JsonConvert.DeserializeObject(jsonText);
                state.url = this.textBoxUrl.Text.Split(new string[] { "azure-devices.net" }, StringSplitOptions.None).LastOrDefault();
                state._rspLabel = this.labelStatus.Text.Replace("Status: ", "");
                state._rspTime = string.IsNullOrEmpty(this.labelLoadingTime.Text) ? 0 : Convert.ToInt64(this.labelLoadingTime.Text.Replace("RspTime: ", "").Replace("ms", "").Trim());
                state.method = this.method.ToString(); 
                state.payload = JsonConvert.DeserializeObject(this.requestResponseMessages.RequestPayload);

                var arrayOfHeaders = this.requestResponseMessages.RequestHeaders.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                state.headers = string.Join(" | ", arrayOfHeaders);

                var parts = this.requestResponseMessages.ResponseMessage.Split(new string[] { "\nPayload:\n", }, 2, StringSplitOptions.RemoveEmptyEntries);
                state._rspStatus = parts.Count() > 0 ? parts.FirstOrDefault().Trim(new char[] { '\n', ' ' }) : string.Empty;
                state._rspPayload = parts.Count() == 2 ? parts.LastOrDefault().Trim(new char[] { '\n', ' ' }) : String.Empty;

                string updatedState = JsonConvert.SerializeObject(state);
                return updatedState;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerMessage(), "RestClient: UpdateState");
                return jsonText;
            }
        }

       
    }
}
