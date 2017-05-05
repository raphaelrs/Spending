using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityModels;

namespace BLL.Services
{
    public interface IWalletService : IDisposable
    {
        void Update(int userId, decimal cash, bool debitar = false);
        WalletModel Select(int userId);
    }
}
