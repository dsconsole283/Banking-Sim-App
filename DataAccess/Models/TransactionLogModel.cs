using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class TransactionLogModel
    {
        public string TransactionNumber { get; set; }
        public string ClientEmail { get; set; }
        public int ToAccountId { get; set; }
        public int FromAccountId { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal ToAccountPreviousBalance { get; set; }
        public decimal FromAccountPreviousBalance { get; set; }
        public string TransactionType { get; set; }
    }
}
