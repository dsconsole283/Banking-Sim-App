using System;
using System.Collections.Generic;
using System.Text;
using static BankingLibrary.Enums;

namespace BankingLibrary
{
    public class CheckingAccount : AccountModel
    {
        public string DebitCardNumber { get; set; }
        public static decimal MinimumDeposit = 500.00M;
        public CheckingAccount(decimal initialDeposit)
        {
            Balance = initialDeposit;
            AccountType = AccountTypes.Checking;
            AccountNumber = GenerateAccountNumber();
        }
    }
}
