using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spending.Models;
using EntityModels;
using BLL.Services;

namespace Spending.Authentication
{
    public class AuthenticationService
    {
        /// <summary>
        /// Verifica se o usuário foi autenticado.
        /// </summary>
        /// <param name="usuario">Usuário.</param>
        /// <param name="senha">Senha.</param>
        /// <returns></returns>
        public static bool IsUserAuthenticated(UserModel userModel, string password)
        {
            Crypto cripto = new Crypto();

            var EncryptedPassword = cripto.EncryptToString(password);

            var IsValidUser = userModel.Password.Equals(EncryptedPassword);

            if (IsValidUser)
            {
                UserSession.CurrentUser = userModel;
            }

            return IsValidUser;
        }

        /// <summary>
        /// Faz logout do sistema.
        /// </summary>
        public static void LogOut()
        {
            UserSession.CurrentUser = null;
        }
    }
}