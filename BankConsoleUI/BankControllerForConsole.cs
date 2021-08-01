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
                            FullClientModel validClient = UserLogin();

                            currentUser = LoadClientInfoToModel(validClient);

                            Console.WriteLine("Login successful! Press enter to continue.");
                            Console.ReadLine();

                            RunAccountMenu(currentUser);

                            break;
                        }
                    case 2: // Sign up
                        {
                            FullClientModel prospectiveClient = RunSignUp();

                            SaveClientToDB(prospectiveClient);

                            currentUser = LoadClientInfoToModel(prospectiveClient);

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

        private ClientModel LoadClientInfoToModel(FullClientModel currentUser)
        {
            ClientModel client = new ClientModel
            {
                EmailAddress = currentUser.EmailAddress,
                SSN = currentUser.SSN,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                BirthDate = currentUser.BirthDate,
                Password = currentUser.Password
            };

            List<DataAccessLibrary.Models.AccountModel> rawAccounts = sql.GetClientAccountsByEmail(currentUser.EmailAddress);

            List<BankingLibrary.AccountModel> formattedAccounts = LoadAccountsToModel(rawAccounts);

            client.Accounts = formattedAccounts;

            return client;
        }
        private void SaveClientToDB(FullClientModel client)
        {
            sql.SaveClientInfoToDB(client);
        }
        private FullClientModel RunSignUp()
        {
            Console.Clear();

            string newEmail = ClientAuthentication.CheckEmailAvailability(sql);

            string newFirstName = ConsoleHelpers.GetStringFromConsole("Please enter your first name: ");
            string newLastName = ConsoleHelpers.GetStringFromConsole("Please enter your last name: ");
            string newSSN = ConsoleHelpers.GetSSNFromUser("Please enter a 9 digit Social Security Number: ");
            string newBirthDate = ConsoleHelpers.GetBirthDateFromConsole();

            FullClientModel newClient = new FullClientModel
            {
                EmailAddress = newEmail,
                FirstName = newFirstName,
                LastName = newLastName,
                SSN = newSSN,
                BirthDate = newBirthDate,
                Accounts = null
            };

            Console.WriteLine("User information accepted. Press enter to create a password.");
            Console.ReadLine();

            Console.Clear();

            ClientAuthentication.CreatePassword(newClient);

            return newClient;
        }

        private FullClientModel UserLogin()
        {
            string emailAddress = ClientAuthentication.EmailAddressValidation(sql);

            ClientAuthentication.LoginWithPassword(sql, emailAddress);

            return sql.GetFullClientDetails(emailAddress);
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
                            DisplayAccounts(currentUser);

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
        private static List<BankingLibrary.AccountModel> LoadAccountsToModel(List<DataAccessLibrary.Models.AccountModel> accounts)
        {
            List<BankingLibrary.AccountModel> output = new List<BankingLibrary.AccountModel>();
            foreach (var account in accounts)
            {
                BankingLibrary.AccountModel formattedAccount = new BankingLibrary.AccountModel
                {
                    AccountNumber = account.AccountNumber,
                    AccountType = account.AccountType,
                    Balance = account.Balance
                };
                output.Add(formattedAccount);
            }
            return output;
        }
        private void DisplayAccounts(ClientModel currentUser)
        {
            if (currentUser.Accounts.Count > 0)
            {
                Console.WriteLine("ACCOUNT NUMBER  |  ACCOUNT TYPE  |  BALANCE");

                foreach (var account in currentUser.Accounts)
                {
                    Console.WriteLine($"{ account.AccountNumber }    |    { account.AccountType }   |    ${Math.Round(account.Balance, 2) }");
                }
            }
            else
            {
                Console.WriteLine("You do not have any active accounts. Please open an account in the Account Menu.");
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

        //    if (currentUser.Accounts.Count < ClientModel.MaxAccounts)
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
        //internal decimal GetInitialDeposit(AccountTypes type)
        //{
        //    decimal amount = 0.00M;
        //    bool isValid = false;
        //    decimal minimumDeposit = 0.00M;

        //    do
        //    {
        //        amount = ConsoleHelpers.GetDecimalFromConsole("How much would you like to initially deposit?: ");

        //        if (type == AccountTypes.Checking)
        //        {
        //            minimumDeposit = CheckingAccount.MinimumDeposit;
        //        }
        //        else if (type == AccountTypes.Savings)
        //        {
        //            minimumDeposit = SavingsAccount.MinimumDeposit;
        //        }

        //        if (amount < minimumDeposit)
        //        {
        //            Console.WriteLine();
        //            Console.WriteLine("Deposit amount does not meet minimum requirements, please try a larger amount.");
        //            Console.WriteLine();

        //            isValid = false;
        //        }
        //        else
        //        {
        //            isValid = true;
        //        }

        //    } while (!isValid);
        //    return amount;
        //}
        //internal AccountTypes GetDesiredAccountType()
        //{
        //    AccountTypes type;
        //    bool isValidSelection = false;

        //    do
        //    {
        //        StandardMessages.AccountTypeMenu();
        //        int selection = ConsoleHelpers.GetIntFromConsole(StandardMessages.EnterSelection());

        //        if (selection == 1)
        //        {
        //            type = AccountTypes.Checking;

        //            isValidSelection = true;
        //        }
        //        else if (selection == 2)
        //        {
        //            type = AccountTypes.Savings;

        //            isValidSelection = true;
        //        }
        //        else
        //        {
        //            Console.WriteLine();
        //            Console.WriteLine("Invalid selection, please try again.");
        //            Console.WriteLine();
        //            type = AccountTypes.Checking;

        //            isValidSelection = false;
        //        }
        //    } while (!isValidSelection);

        //    return type;
        //}
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
