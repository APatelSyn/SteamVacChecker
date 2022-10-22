using System;
using System.Collections.Generic;
using System.Text;

namespace SteamVacChecker
{
    public class ProgramUtils
    {
        public static void PrintCentered(string input)
        {
            foreach(string inSpl in input.Split("\n")) {
                Console.WriteLine(string.Format("{0," + ((Console.WindowWidth / 2) + (inSpl.Length / 2)) + "}", inSpl));
            }
        }
    }
}
