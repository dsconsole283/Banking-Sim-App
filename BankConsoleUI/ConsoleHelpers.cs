using BankingLibrary;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BankConsoleUI
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
        internal static string GetPasswordFromUser(this string message)
        {
            string output = "";
            bool isValidPassword = false;

            do
            {
                Console.WriteLine();
                Console.Write(message);
                string input = Console.ReadLine();

                if (input.Length > ClientModel.MaximumPasswordLength || input.Length < ClientModel.MinimumPasswordLength || string.IsNullOrWhiteSpace(input))
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

        internal static string GetBirthDateFromConsole()
        {
            string output;
            string month = "";
            string day = "";
            string year = "";
            int monthEntry;

            bool isValidMonth = false;
            do
            {
                monthEntry = GetIntFromConsole("Please enter your birth month (MM): ");

                if (monthEntry < 01 || monthEntry > 12)
                {
                    Console.WriteLine("Invalid month selection, try again.");
                    Console.ReadLine();

                    isValidMonth = false;
                }
                else
                {
                    isValidMonth = true;
                    month = monthEntry.ToString();
                }
            } while (!isValidMonth);

            bool isValidDay = false;
            do
            {
                int dayEntry = GetIntFromConsole("Please enter the day of the month on which you were born (DD): ");

                if ((monthEntry == 01 || monthEntry == 03 || monthEntry == 05 || monthEntry == 07 || monthEntry == 10 || monthEntry == 12) && (dayEntry < 01 || dayEntry > 31))
                {
                    Console.WriteLine("Invalid day selection, try again.");
                    Console.ReadLine();

                    isValidDay = false;
                }
                else if ((monthEntry == 04 || monthEntry == 06 || monthEntry == 09 || monthEntry == 11) && (dayEntry < 01 || dayEntry > 30))
                {
                    Console.WriteLine("Invalid day selection, try again.");
                    Console.ReadLine();

                    isValidDay = false;
                }
                else if (monthEntry == 02 && (dayEntry < 0 || dayEntry > 29))
                {
                    Console.WriteLine("Invalid day selection, try again.");
                    Console.ReadLine();

                    isValidDay = false;
                }
                else
                {
                    day = dayEntry.ToString();
                    isValidDay = true;
                }
            } while (!isValidDay);

            bool isValidYear = false;
            do
            {
                int yearEntry = GetIntFromConsole("Please enter your birth year (YYYY): ");

                if (yearEntry + 18 > (int)DateTime.Now.Year)
                {
                    Console.WriteLine("Must be 18 or older to sign up. Please go away little kid.");
                    isValidYear = false;
                }
                else if (yearEntry < 1900)
                {
                    Console.WriteLine("Invalid year, try again.");
                    isValidYear = false;
                }
                else
                {
                    year = yearEntry.ToString();
                    isValidYear = true;
                }
            } while (!isValidYear);

            output = $"{ month }-{ day }-{ year }";
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
        public static string GetEmailAddress()
        {
            bool isValidEntry = false;
            string output = "";
            do
            {
                Console.Clear();

                string input = GetStringFromConsole("Please enter your email address: ");
                if (string.IsNullOrWhiteSpace(input))
                {
                    isValidEntry = false;
                }
                else
                {
                    output = input;

                    isValidEntry = true;
                }
            } while (!isValidEntry);

            return output;
        }
    }
}
