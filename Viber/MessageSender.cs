using Microsoft.AspNetCore.Http;
using StrollTracker.Data;
using System.Net;

namespace StrollTracker.Viber
{
    public class MessageSender
    {
        private string url;
        private string userId = "";
        WebRequest httpRequest;
        private string viberToken;
        private string avatarPath;
        public string UserId { get { return userId; } set { userId = value; } }
        public string AvatarPath
        {
            get { return avatarPath; }
            set { avatarPath = value; }
        }
        public MessageSender(string url, string viberToken)
        {
            this.url = url;
            this.viberToken = viberToken;
            createWebRequest();
        }

        private void createWebRequest()
        {
            httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.Headers.Add("X-Viber-Auth-Token", viberToken);
            httpRequest.ContentType = "application/x-www-form-urlencoded";
        }
        public void SendButton(string buttonText, string message)
        {
            MessageWithButton messageWithButton = new MessageWithButton(UserId);

            string dataList = System.Text.Json.JsonSerializer.Serialize(messageWithButton.CreateTop10StrollButton(buttonText, message));

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(dataList);
            }

            getResponse();
        }
        public void SendMessage(string message)
        {
            MessageWithText messageWithText = new MessageWithText() { AvatarPath = this.AvatarPath };
            string dataList = System.Text.Json.JsonSerializer.Serialize(messageWithText.CreateTextMessage(UserId, message));

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(dataList);
            }

            getResponse();
        }

        private void getResponse()
        {
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            string result;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
        }
    }
}
