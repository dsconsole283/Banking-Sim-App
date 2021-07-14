using BankingLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NADBankConsoleUI
{
    public static class ConsoleHelpers
    {
        internal static string GetStringFromConsole(this string message)
        {
            string output = "";
            bool isValid = false;

            do
            {
                Console.WriteLine();
                Console.Write(message);
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine();
                    Console.WriteLine("Cannot be blank, please try again.");
                    Console.WriteLine();

                    isValid = false;
                }
                else
                {
                    output = input;

                    isValid = true;
                }
            } while (!isValid);

            return output;
        }

        internal static int GetIntFromConsole(this string message)
        {
            bool isValidEntry = false;
            int output = 0;
            string input;

            do
            {
                Console.WriteLine();
                Console.Write(message);
                input = Console.ReadLine();

                isValidEntry = int.TryParse(input, out output);

                if (isValidEntry)
                {
                    return output;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid entry, please try again.");
                    Console.WriteLine();
                }
            } while (!isValidEntry);

            return output;
        }
        internal static decimal GetDecimalFromConsole(this string message)
        {
            bool isValidEntry = false;
            decimal output = 0;
            string input;

            do
            {
                Console.WriteLine();
                Console.Write(message);
                input = Console.ReadLine();

                isValidEntry = decimal.TryParse(input, out output);

                if (isValidEntry)
                {
                    return output;
                }
                else
                {
                    isValidEntry = false;

                    Console.WriteLine();
                    Console.WriteLine("Invalid amount, please try again.");
                    Console.WriteLine();
                }
            } while (!isValidEntry);

            return output;
        }
        internal static string GetPasswordFromUser(this string message, ClientModel client)
        {
            string output = "";
            bool isValidPassword = false;

            do
            {
                Console.WriteLine();
                Console.WriteLine(message);
                string input = Console.ReadLine();

                if (input.Length < client.MinimumPasswordLength || string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid entry, please try again.");
                    Console.WriteLine();
                    isValidPassword = false;
                }
                else
                {
                    output = input;
                    isValidPassword = true;
                }
            } while (!isValidPassword);

            return output;
        }
        public static string GetSSNFromUser(this string message)
        {
            string output = "";
            bool isValidSSN = false;

            do
            {
                Console.WriteLine();
                Console.WriteLine(message);
                string input = Console.ReadLine();

                string pattern = @"^[0-9]{9}$";
                Regex regex = new Regex(pattern);

                isValidSSN = regex.IsMatch(input);

                if (isValidSSN)
                {
                    isValidSSN = true;
                    output = input;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid entry, please try again.");
                    Console.WriteLine();

                    isValidSSN = false;
                }
            } while (!isValidSSN);

            return output;
        }

        internal static void GetUserInfo(ClientModel currentUser)
        {
            currentUser.FirstName = GetStringFromConsole("Please enter your first name: ");
            Console.WriteLine();

            currentUser.LastName = GetStringFromConsole("Please enter your last name: ");
            Console.WriteLine();

            currentUser.SSN = GetSSNFromUser("Please enter your 9-digit Social Security Number: ");
            Console.WriteLine();
        }

        internal static void UpdatePersonalInfo(ClientModel currentUser)
        {
            Console.Clear();

            Console.WriteLine("Please enter your personal information below to update your profile.");

            GetUserInfo(currentUser);
        }

        internal static void GetInitialPersonalInfo(ClientModel client)
        {
            Console.Clear();

            Console.WriteLine("WELCOME to NICK'S A DEMOCRAT BANK!!");
            Console.WriteLine();

            Console.WriteLine("Please enter your personal information below. Once completed, you will be able to open new accounts. Thank you!");
            Console.WriteLine();
            Console.WriteLine();

            GetUserInfo(client);
        }

        public static void DisplayUserInformation(ClientModel currentUser)
        {
            Console.Clear();

            Console.WriteLine("Current Information On File");
            Console.WriteLine();

            Console.WriteLine($"FIRST NAME:  { currentUser.FirstName.ToUpper() }");
            Console.WriteLine();

            Console.WriteLine($"LAST NAME:  { currentUser.LastName.ToUpper() }");
            Console.WriteLine();

            Console.WriteLine($"SOCIAL SECURITY NUMBER:  { currentUser.SSN }");
            Console.WriteLine();
        }

        public static void DisplayAccountsDetails(ClientModel currentUser)
        {
            Console.Clear();

            Console.WriteLine();

            if (currentUser.Accounts.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("You do not have any account information to display");
            }
            else
            {
                int i = 0;

                foreach (var account in currentUser.Accounts)
                {
                    Console.WriteLine($"{ i + 1 }. { account.AccountType } ({ account.AccountNumber }): ${ account.Balance }");
                    i++;
                }
            }
        }
        public static void DisplayTransactionLog(List<TransactionModel> transactions)
        {
            Console.Clear();

            Console.WriteLine("   Transactions");
            Console.WriteLine();

            foreach (var transaction in transactions)
            {

                Console.WriteLine($"{ transaction.TransactionNumber.ToString() }  |  { transaction.TransactionType.ToString() }  |  { transaction.Amount }");

            }
        }
        internal static AccountModel GetAccountSelection(this string message, ClientModel currentUser)
        {
            int selection;
            bool isValidSelection = false;
            AccountModel account;

            do
            {
                Console.WriteLine();
                Console.WriteLine(message);
                selection = ConsoleHelpers.GetIntFromConsole(null);

                if (selection < 1 || selection > currentUser.Accounts.Count)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid selection, please try again.");

                    isValidSelection = false;
                }
                else
                {
                    isValidSelection = true;
                }

            } while (!isValidSelection);

            account = currentUser.Accounts[selection - 1];

            return account;
        }
    }
}
