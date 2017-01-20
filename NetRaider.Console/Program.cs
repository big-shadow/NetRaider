using NetRaider.Core.Controller;
using System;

namespace NetRaider.ConsoleGUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Controller controller = new Controller();
            controller.Communicator.MessageEvent += WriteMessage;

            controller.TestTor();
            controller.TestPing();

            // Pause execution.
            Console.Read();
        }

        public static void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}