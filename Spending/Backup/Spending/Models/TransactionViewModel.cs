using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityModels;

namespace Spending.Models
{
    public class TransactionViewModel
    {
        public TransactionModel TransactionModel { get; set; }
        public WalletModel WalletModel { get; set; }
        public TransactionListModel TransactionListModel { get; set; }

        public TransactionViewModel()
        {
            this.TransactionModel = new TransactionModel();
            this.WalletModel = new WalletModel();
            this.TransactionListModel = new TransactionListModel();
        }
    }
}