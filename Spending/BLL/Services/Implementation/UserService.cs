using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using DAL.DAOs.Implementation;
using DAL.DAOs;
using EntityModels;

namespace BLL.Services.Implementation
{
    public class UserService : IUserService
    {
        private User ModelToUSer(UserModel userModel)
        {
            return new User()
            {
                Email = userModel.Email,
                Name = userModel.Name,
                Password = userModel.Password
            };
        }
        private UserModel UserToModel(User user)
        {
            return new UserModel()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Password = user.Password
            };
        }

        public UserModel Insert(UserModel userModel)
        {            
            Crypto cripto = new Crypto();

            userModel.Password = cripto.EncryptToString(userModel.Password);

            User user = this.ModelToUSer(userModel);

            try
            {
                using (IDAOUser daoUser = new DAOUser())
                {
                    user.Wallets.Add(new Wallet()
                    {
                        Money = 0
                    });

                    user = daoUser.Add(user);

                    if (user != null)
                    {
                        return this.UserToModel(user);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        public UserModel ValidateUser(string email, string password)
        {
            using (IDAOUser daoUser = new DAOUser())
            {
                User user = daoUser.GetSingle(x => x.Email == email);

                if (user != null)
                {
                    if (user.Password == password)
                    {
                        return this.UserToModel(user);
                    }
                }
            }

            return null;
        }
        
        public UserModel Select(string email)
        {
            using (IDAOUser daoUser = new DAOUser())
            {
                User user = daoUser.GetSingle(x => x.Email == email, "Wallets");

                if (user != null)
                {
                    UserModel model = this.UserToModel(user);
                    model.WalletId = user.Wallets.First().Id;

                    return model;
                }
            }

            return null;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
