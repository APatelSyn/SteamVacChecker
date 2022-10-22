using System;
using System.Collections.Specialized;
using System.Net;

namespace SteamVacChecker
{
    public class DiscordWebhook : IDisposable
    {
        private readonly WebClient Client;
        private static NameValueCollection DiscordValues = new NameValueCollection();
        public string WebHook { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }

        public DiscordWebhook()
        {
            Client = new WebClient();
        }

        public void SendMessage(string msgSend)
        {
            DiscordValues.Add("username", UserName);
            DiscordValues.Add("avatar_url", ProfilePicture);
            DiscordValues.Add("content", msgSend);

            Client.UploadValues(WebHook, DiscordValues);
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }

    public class HookUtils
    {
        public static void SendWebhook(string PfpURL, string Username, string WebhookURL, string Message)
        {
            using (DiscordWebhook webhook = new DiscordWebhook())
            {
                webhook.ProfilePicture = PfpURL;
                webhook.UserName = Username;
                webhook.WebHook = WebhookURL;
                webhook.SendMessage(Message);
            }
        }
    }
}
 
