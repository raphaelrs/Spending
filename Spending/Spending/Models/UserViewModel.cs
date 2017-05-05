using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using EntityModels;

namespace Spending.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            this.UserModel = new UserModel();
        }

        public UserModel UserModel { get; set; }
        public bool LoginFailed { get; set; }
        public string Error { get; set; }
    }
}