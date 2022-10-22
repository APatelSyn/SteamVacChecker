using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SteamVacChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            string Accounts = ConfigReader.GetSteamAccounts();
            string Key = ConfigReader.GetSteamAPIKey();

            string VACJson = ApiUtils.GetSteamBanInfo(Key, Accounts).Result;
            SteamVacCheckerVAC.Root PR = JsonConvert.DeserializeObject<SteamVacCheckerVAC.Root>(VACJson);

            Console.ForegroundColor = ConsoleColor.Green;

            ProgramUtils.PrintCentered(
                "--------------------------------------\n" +
                "|             VAC CHECKER            |\n" +
                "--------------------------------------\n"
            );

            while(true)
            {
                ProgramUtils.PrintCentered("Checking players...\n");

                foreach (SteamVacCheckerVAC.Player player in PR.players)
                {
                    ProgramUtils.PrintCentered(string.Format("[Checking ID: {0}]", player.SteamId));

                    if (!player.VACBanned)
                    {
                        ProgramUtils.PrintCentered(string.Format("ID {0} not banned.\n", player.SteamId));
                    }
                    else
                    {
                        string SummaryJSON = ApiUtils.GetSteamPlayerSummary(Key, player.SteamId).Result;
                        SteamVacCheckerSummary.Root SumRoot = JsonConvert.DeserializeObject<SteamVacCheckerSummary.Root>(SummaryJSON);
                        string PlayerName = SumRoot.response.players[0].personaname;

                        string ProfilePictureURL = ConfigReader.GetWebhookProfilePictureURL();
                        string Username = ConfigReader.GetWebhookUsername();
                        string WebhookURL = ConfigReader.GetWebhookURL();
                        string Message = string.Format("`{0}` just got VAC banned!\nSteamID: `{1}`", PlayerName, player.SteamId);

                        //HookUtils.SendWebhook(ProfilePictureURL, Username, WebhookURL, Message);

                        ProgramUtils.PrintCentered(Message);

                        List<string> AccountsList = new List<string>(Accounts.Split(","));
                        AccountsList.Remove(player.SteamId);
                        ConfigReader.SetSteamAccounts(string.Join(",", AccountsList));

                        ProgramUtils.PrintCentered("Removed player from list.\n");
                    }
                }

                Thread.Sleep((1000 * 60) * 60);
            }
        }
    }
}
