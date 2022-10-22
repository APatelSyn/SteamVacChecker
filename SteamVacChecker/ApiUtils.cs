using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SteamVacChecker
{
    class ApiUtils
    {
        public static async Task<string> GetSteamBanInfo(string Key, string Accounts)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(string.Format("http://api.steampowered.com/ISteamUser/GetPlayerBans/v1/?key={0}&steamids={1}", Key, Accounts)),
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public static async Task<string> GetSteamPlayerSummary(string Key, string Account)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(string.Format("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={0}&steamids={1}", Key, Account)),
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
