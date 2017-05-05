using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using EntityModels;

namespace BLL.Services
{
    public interface IUserService : IDisposable
    {
        UserModel Insert(UserModel userModel);
        UserModel Select(string email);
        UserModel ValidateUser(string email, string password);
    }
}
