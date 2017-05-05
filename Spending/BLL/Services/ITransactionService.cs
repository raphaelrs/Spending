using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityModels;

namespace BLL.Services
{
    public interface ITransactionService : IDisposable
    {
        TransactionModel Select(int id, int walletId);
        //List<TransactionModel> List(int walletId);
        TransactionModel Insert(TransactionModel transactionModel);
        TransactionModel Update(TransactionModel transactionModel);
        void Delete(int transactionId, int walletId);

        List<TransactionModel> Paginate(int currentPageIndex, int pageSize, int walletId);

        int SelectCount(int walletId);
    }
}
