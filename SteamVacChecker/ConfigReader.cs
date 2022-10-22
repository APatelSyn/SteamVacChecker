using Microsoft.Extensions.Configuration;
using System;

namespace SteamVacChecker
{
    public static class ConfigReader
    {
        private static IConfiguration config;
        static ConfigReader()
        {
            config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        public static Func<string> GetSteamAPIKey = () => GetValue("SteamSettings:Key");
        public static Func<string> GetSteamAccounts = () => GetValue("SteamSettings:Accounts");
        public static void SetSteamAccounts(string input)
        {
            SetValue("SteamSettings:Accounts", input);
        }

        public static Func<string> GetWebhookURL = () => GetValue("DiscordSettings:WebhookURL");
        public static Func<string> GetWebhookProfilePictureURL = () => GetValue("DiscordSettings:WebhookProfilePictureURL");
        public static Func<string> GetWebhookUsername = () => GetValue("DiscordSettings:WebhookUsername");

        public static string GetValue(string path)
        {
            return config[path];
        }

        public static void SetValue(string path, string value)
        {
            config.GetSection(path).Value = value;
        }
    }
}
