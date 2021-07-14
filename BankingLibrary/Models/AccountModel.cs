using System;
using System.Collections.Generic;
using System.Text;
using static BankingLibrary.Enums;

namespace BankingLibrary
{
    public abstract class AccountModel
    {
        private string _accountNumber;

        public string AccountNumber
        {
            get
            {
                return _accountNumber.Substring(4, 4);
            }
            set { _accountNumber = value; }
        }

        public AccountTypes AccountType { get; set; }

        public decimal MinimumBalance = 0;
        public decimal Balance { get; set; }

        public virtual string GenerateAccountNumber()
        {
            string output = "";

            Random random = new Random();
            output = random.Next(10000000, 99999999).ToString();

            return output;
        }
    }
}
