using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spending.Models;
using EntityModels;

namespace Spending.Authentication
{
    public class UserSession
    {
        public static UserModel CurrentUser
        {
            get
            {
                return (UserModel)HttpContext.Current.Session["CurrentUser"];
            }
            set
            {
                if (value == null)
                {
                    if (HttpContext.Current.Session != null)
                    {                     
                        HttpContext.Current.Session.Remove("CurrentUser");   
                    }
                }
                else
                {
                    HttpContext.Current.Session.Add("CurrentUser", value);
                }
            }
        }
    }
}