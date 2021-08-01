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
        public List<AccountModel> GetClientAccountsByEmail(string emailAddress)
        {
            string sql = "SELECT * FROM dbo.Accounts INNER JOIN dbo.ClientAccounts ON dbo.Accounts.Id = dbo.ClientAccounts.AccountId WHERE dbo.ClientAccounts.ClientEmail = @EmailAddress;";

            return db.LoadData<AccountModel, dynamic>(sql, new { EmailAddress = emailAddress }, _connectionString);
        }
        public void SaveNewAccount(AccountModel newAccount)
        {
            string sql = "INSERT INTO dbo.Accounts (AccountNumber, AccountType, Balance) VALUES (@AccountNumber, @AccountType, @Balance);";

            db.SaveData(sql, new { newAccount.AccountNumber, newAccount.AccountType, newAccount.Balance }, _connectionString);
        }
        public void SaveNewAccountTransaction(TransactionLogModel transaction)
        {
            string sql = "INSERT INTO dbo.Logs (TransactionNumber, ClientEmail, ToAccountId, TransactionAmount, TransactionType) VALUES (@TransactionNumber, @ClientEmail, @ToAccountId, @TransactionAmount, @TransactionType);";

            db.SaveData(sql, new { transaction.TransactionNumber, transaction.ClientEmail, transaction.ToAccountId, transaction.TransactionAmount, @TransactionType = "NewAccount" }, _connectionString);
        }
        public int GetAccountId(AccountModel account)
        {
            string sql = "SELECT Id FROM dbo.Accounts WHERE AccountNumber = @AccountNumber;";

            return db.LoadData<int, dynamic>(sql, new { account.AccountNumber }, _connectionString).First();
        }
        public void SaveNewAccountLink(string emailAddress, int Id)
        {
            string sql = "INSERT INTO dbo.ClientAccounts (ClientEmail, AccountId) VALUES (@EmailAddress, @AccountId);";

            db.SaveData(sql, new { @EmailAddress = emailAddress, @AccountId = Id }, _connectionString);
        }
    }
}
