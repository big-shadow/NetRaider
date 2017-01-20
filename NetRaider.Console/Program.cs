using NetRaider.Configuration;
using System;

namespace NetRaider.ConsoleGUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var test = Config.Item["PrivoxyPort"];
            Console.WriteLine(test);

            test = Config.Item["TorPath"];
            Console.WriteLine(test);

            // Pause execution.
            Console.Read();
        }
    }
}