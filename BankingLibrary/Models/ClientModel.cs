using System;
using System.Collections.Generic;
using System.Text;

namespace BankingLibrary
{
    public class ClientModel
    {
        public List<AccountModel> Accounts { get; set; }

        public string EmailAddress { get; set; }

        public static int MaxAccounts = 5;
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static int MinimumPasswordLength = 8;
        public static int MaximumPasswordLength = 15;

        public string SSN { get; set; }

        public string Password { get; set; }
        public string BirthDate { get; set; }
    }
}
