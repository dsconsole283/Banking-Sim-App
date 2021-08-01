using System;
using System.Collections.Generic;
using System.Text;
using static BankingLibrary.Enums;

namespace BankingLibrary
{
    public class AccountModel
    {
        public string AccountNumber { get; set; }

        public string AccountType { get; set; }

        public decimal MinimumBalance = 0;
        public decimal Balance { get; set; }
        public static decimal MinimumDeposit { get; set; }

        public virtual string GenerateAccountNumber()
        {
            string output = "";

            Random random = new Random();
            output = random.Next(10000000, 99999999).ToString();

            return output;
        }
    }
}
