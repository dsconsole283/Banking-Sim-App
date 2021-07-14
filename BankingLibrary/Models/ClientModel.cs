using System;
using System.Collections.Generic;
using System.Text;

namespace BankingLibrary
{
    public class ClientModel
    {
        public List<AccountModel> Accounts { get; set; } = new List<AccountModel>();

        public int MaxAccounts = 5;
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int MinimumPasswordLength = 8;

        private string _ssn;

        public string SSN // 012457890
        {
            get { return $"XXX-XX-{_ssn.Substring(5, 4) }"; }
            set { _ssn = value; }
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = $"{ FirstName.Substring(0, 1) }{ LastName }"; }
        }
        private string _password;

        public string Password
        {
            get
            {
                return PasswordView(); ;
            }
            set
            {
                _password = value;
            }
        }

        public string PasswordView()
        {
            string output = "";
            for (int i = 0; i < _password.Length; i++)
            {
                output += "x";
            }

            return output;
        }
    }
}
