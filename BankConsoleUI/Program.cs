using BankingLibrary;
using System;

namespace NADBankConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            StandardMessages.Logo();

            Console.ReadLine();

            BankControllerForConsole controller = new BankControllerForConsole();
            controller.RunMainMenu();

            Console.ReadLine();
        }
    }
}
