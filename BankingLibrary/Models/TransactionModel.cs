using System;
using System.Collections.Generic;
using System.Text;
using static BankingLibrary.Enums;

namespace BankingLibrary
{
    public class TransactionModel
    {
        public AccountModel ToAccount { get; set; }
        public AccountModel FromAccount { get; set; }
        public AccountModel Account { get; set; }

        public string TransactionNumber { get; set; }
        public decimal Amount { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public TransactionModel(TransactionTypes transactionType, AccountModel account, decimal amount)
        {
            TransactionType = transactionType;
            Account = account;
            Amount = amount;
        }
        public TransactionModel(TransactionTypes transactionType, AccountModel toAccount, AccountModel fromAccount, decimal amount)
        {
            TransactionType = transactionType;
            ToAccount = toAccount;
            FromAccount = fromAccount;
            Amount = amount;
        }
        public TransactionModel(TransactionTypes transactionType, decimal amount)
        {
            TransactionType = transactionType;
            Amount = amount;
        }
        public static string GenerateTransactionNumber(AccountModel account)
        {
            return $"{ account.AccountNumber }" +
                $"{ DateTime.Now.Month.ToString() }" +
                $"{ DateTime.Now.Day.ToString() }" +
                $"{ DateTime.Now.Year.ToString().Substring(2, 1) }" +
                $"{ DateTime.Now.Hour.ToString() }" +
                $"{ DateTime.Now.Minute.ToString() }" +
                $"{ DateTime.Now.Second.ToString() }";
        }
        public void DebitTransaction(AccountModel account, decimal amount)
        {
            account.Balance -= amount;

            TransactionNumber = GenerateTransactionNumber(account);

            Console.WriteLine();
            Console.WriteLine($"Debit transaction of ${ amount } from { account.AccountType } account({ account.AccountNumber }) completed successfully. New balance: ${ account.Balance }");
        }
        public void CreditTransaction(AccountModel account, decimal amount)
        {
            account.Balance += amount;

            TransactionNumber = GenerateTransactionNumber(account);

            Console.WriteLine();
            Console.WriteLine($"Credit transaction of ${ amount } from { account.AccountType } account({ account.AccountNumber }) completed successfully. New balance: ${ account.Balance }");
        }
        public void TransferTransaction(AccountModel toAccount, AccountModel fromAccount, decimal amount)
        {
            TransactionNumber = GenerateTransactionNumber(fromAccount);

            DebitTransaction(fromAccount, amount);
            CreditTransaction(toAccount, amount);
        }
    }
}
