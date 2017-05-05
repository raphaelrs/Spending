using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityModels;

namespace Spending.Models
{
    public class TransactionListModel
    {
        public List<TransactionModel> ListModel { get; set; }

        public TransactionListModel()
        {
            this.ListModel = new List<TransactionModel>();
        }

        public int PageSize { get; set; }

        public int CurrentPageIndex { get; set; }

        public int Total { get; set; }
    }
}