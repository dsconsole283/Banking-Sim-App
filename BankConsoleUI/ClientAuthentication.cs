
using DataAccess;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsoleUI
{
    public class ClientAuthentication
    {
        public static string EmailAddressValidation(SQLProcesses sql)
        {
            string output = "";

            string emailEntry = ConsoleHelpers.GetEmailAddress();
            List<EmailAddressModel> emailAddresses = sql.GetEmailAddresses();

            foreach (var emailAddress in emailAddresses)
            {
                if (emailEntry == emailAddress.EmailAddress)
                {
                    output = emailEntry;
                }
            }
            return output;
        }

        
    }
}
