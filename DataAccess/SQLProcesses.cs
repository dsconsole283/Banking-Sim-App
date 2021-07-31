using DataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SQLProcesses
    {
        private readonly string _connectionString;
        SQLDataAccess db = new SQLDataAccess();

        public SQLProcesses(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<BasicClientModel> GetAllClients()
        {
            string sql = "SELECT * FROM dbo.Clients;";

            return db.LoadData<BasicClientModel, dynamic>(sql, new { }, _connectionString);
        }
        public EmailAddressModel GetEmailAddress(string emailAddress)
        {
            string sql = "SELECT EmailAddress FROM dbo.Clients WHERE EmailAddress = @EmailAddress;";

            return db.LoadData<EmailAddressModel, dynamic>(sql, new { @EmailAddress = emailAddress }, _connectionString).FirstOrDefault();
        }
        public FullClientModel GetFullClientDetails(string emailAddress)
        {
            string sql = "EXECUTE dbo.spRetrieveDetailsById @EmailAddress";

            FullClientModel client = new FullClientModel();
                
            client = db.LoadData<FullClientModel, dynamic>(sql, new { EmailAddress = emailAddress }, _connectionString).First();

            return client;
        }
        public void SaveClientInfoToDB(FullClientModel client)
        {
            string sql = "EXECUTE dbo.spStoreNewCLient @EmailAddress, @SSN, @FirstName, @LastName, @BirthDate, @Password;";

            db.SaveData(sql, new { client.EmailAddress, client.SSN, client.FirstName, client.LastName, client.BirthDate, client.Password }, _connectionString);
        }
        public string GetPasswordViaEmail(string emailAddress)
        {
            string sql = "SELECT Password FROM dbo.Clients WHERE EmailAddress = @EmailAddress;";

            return db.LoadData<string, dynamic>(sql, new { EmailAddress = emailAddress }, _connectionString).FirstOrDefault();
        }
    }
}
