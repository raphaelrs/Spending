using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spending.Models;
using BLL.Services;
using System.Web.Security;
using BLL.Services.Implementation;
using Spending.Authentication;
using EntityModels;
using System.IO;

namespace Spending.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        [System.Web.Http.AllowAnonymous]
        public ActionResult Index()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        [System.Web.Http.AllowAnonymous]
        public ActionResult Index(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (IUserService userService = new UserService())
                {
                    if (userService.Select(model.UserModel.Email) != null)
                    {                       
                        ModelState.AddModelError("UserModel.Email","This email is already registered");
                    }
                    else
                    {
                        UserModel userModel = userService.Insert(model.UserModel);

                        if (userModel != null)
                        {
                            using (IWalletService wallerService = new WalletService())
                            {
                                userModel.WalletId = wallerService.Select(userModel.Id).Id;
                            }

                            UserSession.CurrentUser = userModel;
                            FormsAuthentication.SetAuthCookie(model.UserModel.Name, false /* createPersistentCookie */);
                            return RedirectToAction("Index", "Transaction");
                        }
                        else
                        {
                            model.LoginFailed = true;
                            model.Error = "The system could not add the user, try again later";
                        }
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        [System.Web.Http.AllowAnonymous]
        public ActionResult LogOn(UserModel model)
        {
            ModelState.Remove("Name");

            if (ModelState.IsValid)
            {
                using (IUserService userService = new UserService())
                {
                    UserModel userModel = userService.Select(model.Email);

                    if (userModel != null)
                    {
                        if (AuthenticationService.IsUserAuthenticated(userModel, model.Password))
                        {
                            FormsAuthentication.SetAuthCookie(userModel.Name, false);
                            return Json(new object[] {false});
                        }
                        else
                        {
                            model.IsInvalid = true;
                            return Json(new object[] { true, base.RenderRazorViewToString("SignIn", model) });
                        }
                    }
                    else
                    {
                        model.IsInvalid = true;
                    }
                }
            }

            return Json(new object[] { true, this.RenderRazorViewToString("SignIn", model) });
        }
        
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            UserSession.CurrentUser = null;

            return RedirectToAction("Index", "Home");
        }

    }
}
