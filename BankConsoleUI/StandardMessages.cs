using BankingLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankConsoleUI
{
    public static class StandardMessages
    {
        public static void Logo()
        {
            Console.WriteLine("Add Logo");
        }

        internal static string EnterSelection()
        {
            return "Enter selection: ";
        }

        internal static void AccountMenuText()
        {
            Console.WriteLine();
            Console.WriteLine("   Account Menu");
            Console.WriteLine();
            Console.WriteLine("Please select an action from the following:");
            Console.WriteLine();
            Console.WriteLine("1. View existing accounts");
            Console.WriteLine();
            Console.WriteLine("2. Open a new account");
            Console.WriteLine();
            Console.WriteLine("3. Perform transaction");
            Console.WriteLine();
            Console.WriteLine("4. View existing profile information");
            Console.WriteLine();
            Console.WriteLine("5. Update profile information");
            Console.WriteLine();
            Console.WriteLine("6. View transaction log");
            Console.WriteLine();
            Console.WriteLine("7. Return to Main Menu");
        }

        public static void MainMenuText()
        {
            Console.WriteLine();
            Console.WriteLine("   Main Menu");
            Console.WriteLine();
            Console.WriteLine("Please select an action from the following:");
            Console.WriteLine();
            Console.WriteLine("1. Log in to an existing profile");
            Console.WriteLine();
            Console.WriteLine("2. Sign up for a new profile");
            Console.WriteLine();
            Console.WriteLine("3. Exit program");
            Console.WriteLine();
        }

        public static void AccountTypeMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Which type of account would you like to open?");
            Console.WriteLine();
            Console.WriteLine("1. Checking");
            Console.WriteLine();
            Console.WriteLine("2. Savings");
            Console.WriteLine();
        }

        public static void TransactionMenu()
        {
            Console.WriteLine();
            Console.WriteLine("    Transaction Menu");
            Console.WriteLine();
            Console.WriteLine("Which type of transaction would you like to perform?");
            Console.WriteLine();
            Console.WriteLine("1. Debit");
            Console.WriteLine();
            Console.WriteLine("2. Credit");
            Console.WriteLine();
            Console.WriteLine("3. Transfer funds between accounts");
            Console.WriteLine();
            Console.WriteLine("4. Transfer funds to another user's account");
            Console.WriteLine();
            Console.WriteLine("5. Return to Account Menu");
            Console.WriteLine();
        }
    }
}
