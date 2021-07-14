using System;
using System.Collections.Generic;
using System.Text;
using static BankingLibrary.Enums;

namespace BankingLibrary
{
    public class SavingsAccount : AccountModel
    {
        public static decimal MinimumDeposit = 50.00M;
        public override string GenerateAccountNumber()
        {
            return $"{ base.GenerateAccountNumber().Substring(2, 6) }SA";
        }

        public SavingsAccount(decimal initialDeposit)
        {
            Balance = initialDeposit;
            AccountType = AccountTypes.Savings;
            AccountNumber = GenerateAccountNumber();
        }
    }
}
