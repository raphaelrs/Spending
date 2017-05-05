using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.DAOs.Implementation
{
    public class DAOTransaction : DAOGeneric<Transaction>, IDAOTransaction
    {        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<Transaction> Paginate(int currentPageIndex, int pageSize, int walletId)
        {
            List<Transaction> transactions = null;

            using (SpendingEntities context = new SpendingEntities())
            {
                try
                {
                    transactions = context.Transactions.Where(x => x.WalletId == walletId)
                                                       .OrderByDescending(x => x.Date)
                                                       .ThenByDescending(x => x.Time)
                                                       .Skip(currentPageIndex * pageSize)
                                                       .Take(pageSize)
                                                       .ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return transactions;
        }


        public int GetTotal(int walletId)
        {
            using (SpendingEntities context = new SpendingEntities())
            {
                try
                {
                    int total  = context.Transactions.Where(x => x.WalletId == walletId).Count();
                    return total;
                }
                catch (ArgumentNullException e)
                {
                    return 0;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
