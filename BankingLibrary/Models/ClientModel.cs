using System;
using System.Collections.Generic;
using System.Text;

namespace BankingLibrary
{
    public class ClientModel
    {
        public List<AccountModel> Accounts { get; set; } = new List<AccountModel>();

        public string EmailAddress { get; set; }

        public int MaxAccounts = 5;
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static int MinimumPasswordLength = 8;
        public static int MaximumPasswordLength = 15;

        private string _ssn;

        public string SSN // 012457890
        {
            get { return $"XXX-XX-{_ssn.Substring(5, 4) }"; }
            set { _ssn = value; }
        }

        public string Password { get; set; }
        public string BirthDate { get; set; }
    }
}
