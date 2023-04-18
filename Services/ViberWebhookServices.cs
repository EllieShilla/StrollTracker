using Microsoft.AspNetCore.Http;
using StrollTracker.Data;
using StrollTracker.Viber;
using System.Net;
using System;

namespace StrollTracker.Services
{
    public class ViberWebhookServices
    {
        WebRequest? httpRequest;
        string urlForSetMessage = "";
        string webhookPath = "";
        string viberToken = "";

        public string UrlForSetMessage
        {
            get { return urlForSetMessage; }
            set { urlForSetMessage = value; }
        }
        public string WebhookPath
        {
            get { return webhookPath; }
            set { webhookPath = value; }
        }

        public string ViberToken
        {
            get { return viberToken; }
            set { viberToken = value; }
        }
        public string Send()
        {
            createWebRequest();
            return sendMessage();
        }

        /// <summary>
        /// Create webhook objectfor for sending to Viber.
        /// </summary>
        private ViberWebhook createData()
        {
            return new ViberWebhook()
            {
                url = UrlForSetMessage,
                event_types = new string[]
                {
                    "delivered",
                    "seen",
                    "failed",
                    "subscribed",
                    "unsubscribed",
                    "conversation_started"
                }
            };
        }

        private void createWebRequest()
        {
            httpRequest = (HttpWebRequest)WebRequest.Create(WebhookPath);
            httpRequest.Method = "POST";
            httpRequest.Headers.Add("X-Viber-Auth-Token", ViberToken);
        }

        /// <summary>
        /// Send webhook to Viber.
        /// </summary>
        private string sendMessage()
        {
            string dataList = System.Text.Json.JsonSerializer.Serialize(createData());

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(dataList);
            }
            return getResponse();
        }

        /// <summary>
        /// Get response from Viber.
        /// </summary>
        private string getResponse()
        {
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            string result;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }
    }
}
