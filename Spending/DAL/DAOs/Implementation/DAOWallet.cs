using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.DAOs.Implementation
{
    public class DAOWallet : DAOGeneric<Wallet>, IDAOWallet
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
