
using DataAccess;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsoleUI
{
    public class ClientAuthentication
    {
        public static string CheckEmailAvailability(SQLProcesses sql)
        {
            bool isUniqueEmail = false;
            string newEmail = "";
            string emailEntry = "";

            do
            {
                emailEntry = ConsoleHelpers.GetStringFromConsole("Please enter an email address. This address will become your username for logging-in.: ");
                EmailAddressModel emailAddress = sql.GetEmailAddress(emailEntry);

                if (emailAddress == null)
                {
                    isUniqueEmail = true;
                    newEmail = emailEntry;
                }
                else
                {
                    Console.WriteLine("Email address already in use. Please try again.");

                    isUniqueEmail = false;
                }
            } while (!isUniqueEmail);

            return newEmail;
        }
        internal static void CreatePassword(FullClientModel newClient)
        {
            bool isMatch = false;
            do
            {
                string passwordEntry = ConsoleHelpers.GetInitialPasswordFromUser("Please create a password. Password must be between 8 and 15 characters.: ");
                Console.WriteLine();

                string passwordConfirmation = ConsoleHelpers.GetInitialPasswordFromUser("Please re-enter your password.: ");

                if (passwordEntry != passwordConfirmation)
                {
                    Console.WriteLine("Passwords do not match. Please try again.");
                    Console.ReadLine();

                    isMatch = false;
                }
                else
                {
                    Console.WriteLine("Password created successfully.");

                    newClient.Password = passwordEntry;

                    isMatch = true;
                }
            } while (!isMatch);
        }

        internal static string EmailAddressValidation(SQLProcesses sql)
        {
            bool isValidEntry = false;
            string verifiedEmail = "";

            do
            {
                string emailEntry = ConsoleHelpers.GetStringFromConsole("Please enter your email address: ");
                EmailAddressModel email = sql.GetEmailAddress(emailEntry);

                if (email == null)
                {
                    Console.WriteLine("No such email address found, please try again.");
                    isValidEntry = false;
                }
                else
                {
                    verifiedEmail = emailEntry;
                    isValidEntry = true;
                }
            } while (!isValidEntry);

            return verifiedEmail;
        }
        internal static void LoginWithPassword(SQLProcesses sql, string emailAddress)
        {
            string passwordFromDB = sql.GetPasswordViaEmail(emailAddress);
            bool isValidPassword = false;
            do
            {
                string passwordEntry = ConsoleHelpers.GetStringFromConsole("Please enter your password: ");
                if (passwordEntry == passwordFromDB)
                {
                    isValidPassword = true;
                }
                else
                {
                    Console.WriteLine("Incorrect password, try again.");
                    isValidPassword = false;
                }
            } while (!isValidPassword);
        }
    }
}
