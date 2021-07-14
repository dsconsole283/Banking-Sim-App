
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingLibrary
{
    public class Enums
    {
        public enum AccountTypes
        {
            Checking,
            Savings
        }
        public enum TransactionTypes
        {
            Debit,
            Credit,
            Transfer,
            NewAccount
        }
    }
}
