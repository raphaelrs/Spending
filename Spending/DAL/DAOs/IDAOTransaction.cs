using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.DAOs
{
    public interface IDAOTransaction : IDAOGeneric<Transaction>, IDisposable
    {
        int GetTotal(int walletId);
        List<Transaction> Paginate(int currentPageIndex, int pageSize, int walletId);
    }
}
