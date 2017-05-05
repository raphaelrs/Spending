using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityModels;
using DAL.DAOs;
using DAL;
using DAL.DAOs.Implementation;

namespace BLL.Services.Implementation
{
    public class WalletService : IWalletService
    {
        private WalletModel WalletToModel(Wallet wallet)
        {
            return new WalletModel()
            {
                Id = wallet.Id,
                Money = wallet.Money
            };
        }
        private Wallet ModelToWallet(WalletModel model)
        {
            return new Wallet()
            {
                Money = model.Money
            };
        }

        public WalletModel Select(int userId)
        {
            using (IDAOWallet daoUser = new DAOWallet())
            {
                Wallet wallet = daoUser.GetSingle(x => x.UserId == userId);

                if (wallet != null)
                {
                    return this.WalletToModel(wallet);
                }
            }

            return null;
        }

        public void Update(int userId, decimal cash, bool debitar = false)
        {
            try
            {
                using (IDAOWallet daoWallet = new DAOWallet())
                {
                    Wallet wallet = daoWallet.GetSingle(x => x.UserId == userId);

                    if (debitar)
                    {
                        wallet.Money -= cash;
                    }
                    else
                    {
                        wallet.Money += cash;
                    }

                    daoWallet.Update(wallet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
