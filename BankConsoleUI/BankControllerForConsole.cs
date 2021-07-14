using BankingLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using static BankingLibrary.Enums;

namespace NADBankConsoleUI
{
    public class BankControllerForConsole
    {
        public List<ClientModel> users = new List<ClientModel>();
        public ClientModel currentUser;
        public List<TransactionModel> transactions = new List<TransactionModel>();
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
                            Console.WriteLine("Please pardon our dust!");
                            Console.ReadLine();

                            RunMainMenu();

                            break;
                        }
                    case 2: // Sign up
                        {
                            ClientModel client = new ClientModel();

                            ConsoleHelpers.GetInitialPersonalInfo(client);

                            users.Add(client);
                            currentUser = client;

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
                    case 1: // View accounts
                        {
                            ConsoleHelpers.DisplayAccountsDetails(currentUser);

                            ReturnToAccountMenu(currentUser);

                            break;
                        }
                    case 2: // Open an account
                        {
                            OpenAccount(currentUser);

                            ReturnToAccountMenu(currentUser);

                            break;
                        }
                    case 3: // Perform transaction
                        {
                            RunTransactions(currentUser);

                            ReturnToAccountMenu(currentUser);

                            break;
                        }
                    case 4: // View existing info
                        {
                            ConsoleHelpers.DisplayUserInformation(currentUser);

                            ReturnToAccountMenu(currentUser);

                            break;
                        }
                    case 5: // Update existing info
                        {
                            ConsoleHelpers.UpdatePersonalInfo(currentUser);

                            Console.WriteLine();

                            ConsoleHelpers.DisplayUserInformation(currentUser);

                            ReturnToAccountMenu(currentUser);

                            break;
                        }
                    case 6: // View transaction log
                        {
                            ConsoleHelpers.DisplayTransactionLog(transactions);

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

        internal void OpenAccount(ClientModel currentUser)
        {
            Console.Clear();

            AccountTypes type;

            if (CanOpenNewAccount(currentUser))
            {
                type = GetDesiredAccountType();

                decimal amount = GetInitialDeposit(type);

                if (type == AccountTypes.Checking)
                {
                    TransactionModel transaction = new TransactionModel(TransactionTypes.NewAccount, amount);
                    transaction.OpenCheckingAccount(currentUser, amount);
                    transactions.Add(transaction);
                }
                else if (type == AccountTypes.Savings)
                {
                    TransactionModel transaction = new TransactionModel(TransactionTypes.NewAccount, amount);
                    transaction.OpenSavingsAccount(currentUser, amount);
                    transactions.Add(transaction);
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Maximum number of accounts reached, please try closing an account before opening a new one (press enter to continue).");
                Console.ReadLine();

                RunAccountMenu(currentUser);
            }
        }

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

        internal bool CanOpenNewAccount(ClientModel client)
        {
            int numberOfOpenAccounts = client.Accounts.Count;

            if (numberOfOpenAccounts < client.MaxAccounts)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void RunTransactions(ClientModel currentUser)
        {
            Console.Clear();

            bool isValidSelection = false;
            int selection;

            do
            {
                StandardMessages.TransactionMenu();
                selection = ConsoleHelpers.GetIntFromConsole(StandardMessages.EnterSelection());

                if (selection > 0 && selection < 6)
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
                    case 1: // Debit
                        {
                            PerformDebitTransaction(currentUser);
                            break;
                        }
                    case 2: // Credit
                        {
                            PerformCreditTransaction(currentUser);
                            break;
                        }
                    case 3: // Transfer between current user
                        {
                            PerformTransferTransaction(currentUser);
                            break;
                        }
                    case 4: // Transfer to other user
                        {
                            Console.WriteLine("Under construction");
                            break;
                        }
                    case 5: // Return to Account Menu
                        {
                            ReturnToAccountMenu(currentUser);

                            break;
                        }
                }
            }
        }
        internal decimal GetTransactionAmount(TransactionTypes type, AccountModel account)
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
        internal void PerformDebitTransaction(ClientModel currentUser)
        {
            ConsoleHelpers.DisplayAccountsDetails(currentUser);

            AccountModel account = ConsoleHelpers.GetAccountSelection("Please select the desired account: ", currentUser);

            TransactionTypes type = TransactionTypes.Debit;
            decimal amount = GetTransactionAmount(type, account);

            TransactionModel transaction = new TransactionModel(type, amount);
            transaction.DebitTransaction(account, amount);

            transactions.Add(transaction);
        }

        internal void PerformCreditTransaction(ClientModel currentUser)
        {
            ConsoleHelpers.DisplayAccountsDetails(currentUser);

            AccountModel account = ConsoleHelpers.GetAccountSelection("Please select the desired account: ", currentUser);

            TransactionTypes type = TransactionTypes.Credit;
            decimal amount = GetTransactionAmount(type, account);

            TransactionModel transaction = new TransactionModel(type, amount);
            transaction.CreditTransaction(account, amount);

            transactions.Add(transaction);
        }
        internal void PerformTransferTransaction(ClientModel currentUser)
        {
            TransactionTypes transfer = TransactionTypes.Transfer;
            bool isAccountConflict = false;
            AccountModel toAccount, fromAccount;

            do
            {
                ConsoleHelpers.DisplayAccountsDetails(currentUser);

                toAccount = ConsoleHelpers.GetAccountSelection("Please select the account you would like to send money to: ", currentUser);

                fromAccount = ConsoleHelpers.GetAccountSelection("Please select the account you would like to use to send money: ", currentUser);

                if (toAccount == fromAccount)
                {
                    Console.WriteLine();
                    Console.WriteLine("You selected the same account, please try again (press enter).");
                    Console.ReadLine();

                    isAccountConflict = true;
                }
                else
                {
                    isAccountConflict = false;
                }
            } while (isAccountConflict);

            decimal amount = GetTransactionAmount(transfer, fromAccount);

            TransactionModel transaction = new TransactionModel(transfer, toAccount, fromAccount, amount);
            transaction.TransferTransaction(toAccount, fromAccount, amount);

            transactions.Add(transaction);
        }
    }
}
