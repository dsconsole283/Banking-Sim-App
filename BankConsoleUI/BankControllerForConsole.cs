using BankingLibrary;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using static BankingLibrary.Enums;

namespace BankConsoleUI
{
    public class BankControllerForConsole
    {
        public ClientModel currentUser;
        public List<TransactionModel> transactions = new List<TransactionModel>();
        private readonly SQLProcesses sql = new SQLProcesses(GetConnectionString());
        internal void RunMainMenu()
        {
            bool isValidEntry = false;
            int selection;

            do
            {
                Console.Clear();

                StandardMessages.MainMenuText();

                selection = ConsoleHelpers.GetIntFromConsole(StandardMessages.EnterSelection());

                if (selection > 0 && selection < 4)
                {
                    isValidEntry = true;
                }
                else
                {
                    Console.WriteLine("Invalid entry, please try again (press enter).");
                    Console.ReadLine();
                }
            } while (!isValidEntry);

            if (isValidEntry)
            {
                switch (selection)
                {
                    case 1: // Log in
                        {
                            currentUser = UserLogin();

                            Console.WriteLine("Login successful! Press enter to continue.");
                            Console.ReadLine();

                            RunAccountMenu(currentUser);

                            break;
                        }
                    case 2: // Sign up
                        {
                            currentUser = RunSignUp();

                            SaveClientToDB(LoadClientInfoToDBModel(currentUser));


                            Console.WriteLine("Profile successfully created. Press enter to open account menu.");
                            Console.ReadLine();

                            RunAccountMenu(currentUser);

                            break;
                        }
                    case 3: // Exit
                        {
                            Environment.Exit(0);
                            break;
                        }
                }
            }
        }

        private FullClientModel LoadClientInfoToDBModel(ClientModel currentUser)
        {
            FullClientModel client = new FullClientModel
            {
                EmailAddress = currentUser.EmailAddress,
                SSN = currentUser.SSN,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                BirthDate = currentUser.BirthDate,
                Password = currentUser.Password
            };

            return client;
        }
        private void SaveClientToDB(FullClientModel client)
        {
            sql.SaveClientInfoToDB(client);
        }
        private ClientModel RunSignUp()
        {
            Console.Clear();

            bool isUniqueEmail = false;
            string newEmail = "";
            List<EmailAddressModel> emailAddresses = sql.GetEmailAddresses();

            do
            {
                string emailEntry = ConsoleHelpers.GetEmailAddress();

                foreach (var emailAddress in emailAddresses)
                {
                    if (emailEntry == emailAddress.EmailAddress)
                    {
                        Console.WriteLine("Email address already in use. Please try again.");
                        Console.ReadLine();

                        isUniqueEmail = false;

                        break;
                    }
                    else
                    {
                        isUniqueEmail = true;
                        newEmail = emailEntry;
                    }
                }
            } while (!isUniqueEmail);

            string newFirstName = ConsoleHelpers.GetNameFromConsole("Please enter your first name: ");
            string newLastName = ConsoleHelpers.GetNameFromConsole("Please enter your last name: ");
            string newSSN = ConsoleHelpers.GetSSNFromUser("Please enter a 9 digit Social Security Number: ");
            string newBirthDate = ConsoleHelpers.GetBirthDateFromConsole();

            ClientModel newClient = new ClientModel
            {
                EmailAddress = newEmail,
                FirstName = newFirstName,
                LastName = newLastName,
                SSN = newSSN,
                BirthDate = newBirthDate
            };

            Console.WriteLine("User information accepted. Press enter to proceed.");
            Console.ReadLine();

            bool isMatch = false;
            do
            {
                string passwordEntry = ConsoleHelpers.GetPasswordFromUser("Please create a password. Password must be between 8 and 15 characters.: ");
                Console.WriteLine();

                string passwordConfirmation = ConsoleHelpers.GetPasswordFromUser("Please re-enter your password.: ");

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

            return newClient;
        }

        private ClientModel UserLogin()
        {
            bool validPassword = false;
            string emailEntry = ClientAuthentication.EmailAddressValidation(sql);

            FullClientModel dbDetails = sql.GetFullClientDetails(emailEntry);

            ClientModel user = new ClientModel
            {
                FirstName = dbDetails.FirstName,
                LastName = dbDetails.LastName,
                EmailAddress = emailEntry,
                Password = dbDetails.Password,
                SSN = dbDetails.SSN,
            };

            do
            {
                string passwordEntry = ConsoleHelpers.GetPasswordFromUser("Please enter your password: ");

                if (passwordEntry == user.Password)
                {
                    validPassword = true;
                }
                else
                {
                    validPassword = false;
                }
            } while (!validPassword);
;
            return user;
        }
        internal void RunAccountMenu(ClientModel currentUser)
        {
            Console.Clear();

            bool isValidSelection = false;
            int selection;

            do
            {
                Console.Clear();

                StandardMessages.AccountMenuText();

                selection = ConsoleHelpers.GetIntFromConsole(StandardMessages.EnterSelection());

                if (selection > 0 && selection < 8)
                {
                    isValidSelection = true;
                }
                else
                {
                    Console.WriteLine("Invalid entry, please try again (press enter).");
                    Console.ReadLine();
                }

            } while (!isValidSelection);

            if (isValidSelection)
            {
                switch (selection)
                {
                    case 1: /// TODO Build accounts view
                        {
                            ReturnToAccountMenu(currentUser);

                            break;
                        }
                    case 2: /// TODO Build open accounts
                        {
                            ReturnToAccountMenu(currentUser);

                            break;
                        }
                    case 3: /// TODO Build transactions
                        {
                            ReturnToAccountMenu(currentUser);

                            break;
                        }
                    case 4: /// TODO Build view existing info
                        {
                            ReturnToAccountMenu(currentUser);

                            break;
                        }
                    case 5: /// TODO Update existing info
                        {
                            ReturnToAccountMenu(currentUser);

                            break;
                        }
                    case 6: /// TODO Build view transaction log
                        {
                            ReturnToAccountMenu(currentUser);

                            break;
                        }
                    case 7: // Return to main menu
                        {
                            RunMainMenu();

                            break;
                        }
                    default:
                        break;
                }
            }
        }
        internal void ReturnToAccountMenu(ClientModel currentUser)
        {
            Console.WriteLine();
            Console.WriteLine("Press enter to return to Account Menu.");
            Console.ReadLine();

            RunAccountMenu(currentUser);
        }
        //internal void OpenAccount(ClientModel currentUser)
        //{
        //    Console.Clear();

        //    AccountTypes type;

        //    if (CanOpenNewAccount(currentUser))
        //    {
        //        type = GetDesiredAccountType();

        //        decimal amount = GetInitialDeposit(type);

        //        if (type == AccountTypes.Checking)
        //        {
        //            TransactionModel transaction = new TransactionModel(TransactionTypes.NewAccount, amount);
        //            transaction.OpenCheckingAccount(currentUser, amount);
        //            transactions.Add(transaction);
        //        }
        //        else if (type == AccountTypes.Savings)
        //        {
        //            TransactionModel transaction = new TransactionModel(TransactionTypes.NewAccount, amount);
        //            transaction.OpenSavingsAccount(currentUser, amount);
        //            transactions.Add(transaction);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine();
        //        Console.WriteLine("Maximum number of accounts reached, please try closing an account before opening a new one (press enter to continue).");
        //        Console.ReadLine();

        //        RunAccountMenu(currentUser);
        //    }
        //}
        internal decimal GetInitialDeposit(AccountTypes type)
        {
            decimal amount = 0.00M;
            bool isValid = false;
            decimal minimumDeposit = 0.00M;

            do
            {
                amount = ConsoleHelpers.GetDecimalFromConsole("How much would you like to initially deposit?: ");

                if (type == AccountTypes.Checking)
                {
                    minimumDeposit = CheckingAccount.MinimumDeposit;
                }
                else if (type == AccountTypes.Savings)
                {
                    minimumDeposit = SavingsAccount.MinimumDeposit;
                }

                if (amount < minimumDeposit)
                {
                    Console.WriteLine();
                    Console.WriteLine("Deposit amount does not meet minimum requirements, please try a larger amount.");
                    Console.WriteLine();

                    isValid = false;
                }
                else
                {
                    isValid = true;
                }

            } while (!isValid);
            return amount;
        }
        internal AccountTypes GetDesiredAccountType()
        {
            AccountTypes type;
            bool isValidSelection = false;

            do
            {
                StandardMessages.AccountTypeMenu();
                int selection = ConsoleHelpers.GetIntFromConsole(StandardMessages.EnterSelection());

                if (selection == 1)
                {
                    type = AccountTypes.Checking;

                    isValidSelection = true;
                }
                else if (selection == 2)
                {
                    type = AccountTypes.Savings;

                    isValidSelection = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid selection, please try again.");
                    Console.WriteLine();
                    type = AccountTypes.Checking;

                    isValidSelection = false;
                }
            } while (!isValidSelection);

            return type;
        }
        internal decimal GetTransactionAmount(TransactionTypes type, BankingLibrary.AccountModel account)
        {
            decimal amount = 0.00M;
            bool isValidAmount = false;
            bool isOverDraft = false;

            do
            {
                amount = ConsoleHelpers.GetDecimalFromConsole($"How much would you like to { type }?: ");

                if (amount > 0)
                {
                    isValidAmount = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Not a valid amount, please try again (press enter).");
                    Console.ReadLine();
                }

                if (isValidAmount && (type == TransactionTypes.Debit || type == TransactionTypes.Transfer))
                {

                    if (account.Balance - amount < 0)
                    {
                        isOverDraft = true;

                        Console.WriteLine();
                        Console.WriteLine("Insufficient funds to complete transaction, please enter a different amount.");
                        Console.WriteLine();
                    }
                    else
                    {
                        isOverDraft = false;
                    }
                }
            } while (!isValidAmount || isOverDraft);

            return amount;
        }
        //internal void PerformDebitTransaction(ClientModel currentUser)
        //{
        //    ConsoleHelpers.DisplayAccountsDetails(currentUser);

        //    BankingLibrary.AccountModel account = ConsoleHelpers.GetAccountSelection("Please select the desired account: ", currentUser);

        //    TransactionTypes type = TransactionTypes.Debit;
        //    decimal amount = GetTransactionAmount(type, account);

        //    TransactionModel transaction = new TransactionModel(type, amount);
        //    transaction.DebitTransaction(account, amount);

        //    transactions.Add(transaction);
        //}
        //internal void PerformCreditTransaction(ClientModel currentUser)
        //{
        //    ConsoleHelpers.DisplayAccountsDetails(currentUser);

        //    BankingLibrary.AccountModel account = ConsoleHelpers.GetAccountSelection("Please select the desired account: ", currentUser);

        //    TransactionTypes type = TransactionTypes.Credit;
        //    decimal amount = GetTransactionAmount(type, account);

        //    TransactionModel transaction = new TransactionModel(type, amount);
        //    transaction.CreditTransaction(account, amount);

        //    transactions.Add(transaction);
        //}
        //internal void PerformTransferTransaction(ClientModel currentUser)
        //{
        //    TransactionTypes transfer = TransactionTypes.Transfer;
        //    bool isAccountConflict = false;
        //    BankingLibrary.AccountModel toAccount, fromAccount;

        //    do
        //    {
        //        ConsoleHelpers.DisplayAccountsDetails(currentUser);

        //        toAccount = ConsoleHelpers.GetAccountSelection("Please select the account you would like to send money to: ", currentUser);

        //        fromAccount = ConsoleHelpers.GetAccountSelection("Please select the account you would like to use to send money: ", currentUser);

        //        if (toAccount == fromAccount)
        //        {
        //            Console.WriteLine();
        //            Console.WriteLine("You selected the same account, please try again (press enter).");
        //            Console.ReadLine();

        //            isAccountConflict = true;
        //        }
        //        else
        //        {
        //            isAccountConflict = false;
        //        }
        //    } while (isAccountConflict);

        //    decimal amount = GetTransactionAmount(transfer, fromAccount);

        //    TransactionModel transaction = new TransactionModel(transfer, toAccount, fromAccount, amount);
        //    transaction.TransferTransaction(toAccount, fromAccount, amount);

        //    transactions.Add(transaction);
        //}
        public static string GetConnectionString(string connectionString = "Default")
        {
            string output = "";

            var builder = new ConfigurationBuilder();

            var config = builder.SetBasePath(@"C:\Users\dscon\source\repos\Banking Sim App\BankConsoleUI").AddJsonFile("appsettings.json").Build();

            output = config.GetConnectionString(connectionString);

            return output;
        }
        private static void DisplayAllClients(SQLProcesses sql)
        {
            var rows = sql.GetAllClients();

            foreach (var row in rows)
            {
                Console.WriteLine($"{ row.Id }: { row.FirstName } { row.LastName }");
            }
        }
    }
}
