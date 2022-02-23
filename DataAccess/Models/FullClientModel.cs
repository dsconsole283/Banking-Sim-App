using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class FullClientModel
    {
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string BirthDate { get; set; }
        public string Password { get; set; }
        public List<AccountModel> Accounts { get; set; } = new List<AccountModel>();
    }
}
