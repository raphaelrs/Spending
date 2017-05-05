using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spending.Models;
using EntityModels;
using Spending.Authentication;
using BLL.Services.Implementation;
using BLL.Services;

namespace Spending.Controllers
{
    public class TransactionController : BaseController
    {
        private List<TransactionModel> SelectListModel(int pageIndex = 0)
        {
            int walletId = UserSession.CurrentUser.WalletId;

            using (ITransactionService transactionService = new TransactionService())
            {
                List<TransactionModel> list = transactionService.Paginate(pageIndex, BaseController.pageSize, walletId);

                if (list != null && list.Count > 0)
                {
                    return list;
                }
            }

            return null;
        }

        private int SelectTransactionCount()
        {
            int walletId = UserSession.CurrentUser.WalletId;
            int count = 0;

            using (ITransactionService transactionService = new TransactionService())
            {
                count = transactionService.SelectCount(walletId);
            }

            return count;
        }

        private WalletModel SelectWalletModel()
        {
            using (IWalletService walletService = new WalletService())
            {
                WalletModel model = walletService.Select(UserSession.CurrentUser.Id);
                return model;
            }
        }

        [AjaxAuthorizeAttribute]
        public ActionResult Index()
        {
            TransactionViewModel model = new TransactionViewModel();
            List<TransactionModel> list = this.SelectListModel();

            if (list != null)
            {
                model.WalletModel = this.SelectWalletModel();
                model.TransactionListModel.ListModel.AddRange(list);
                model.TransactionListModel.PageSize = BaseController.pageSize;
                model.TransactionListModel.Total = SelectTransactionCount();
            }

            return View(model);
        }

        [AjaxAuthorizeAttribute]
        [HttpPost]
        public ActionResult Add(TransactionModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int userId = UserSession.CurrentUser.Id;

                    using (IWalletService service = new WalletService())
                    {
                        WalletModel walletModel = service.Select(userId);

                        using (ITransactionService transactionService = new TransactionService())
                        {
                            model.WalletId = walletModel.Id;
                            model = transactionService.Insert(model);
                        }

                        decimal valueToUpdate = decimal.Parse(model.Value);

                        if (model.Type == TransactionType.Debit)
                        {
                            walletModel.Money -= valueToUpdate;
                            service.Update(userId, valueToUpdate, true);
                        }
                        else
                        {
                            walletModel.Money += valueToUpdate;
                            service.Update(userId, valueToUpdate);
                        }

                        return Json(new object[] { false, this.RenderRazorViewToString("Wallet", walletModel) });
                    }
                }
                catch (Exception)
                {                    
                    return Json(new object[] { true, true, this.RenderRazorViewToString("AddTransaction", model) });
                }
            }
            else
            {
                return Json(new object[] { true, false, this.RenderRazorViewToString("AddTransaction", model) });
            }
        }

        [AjaxAuthorizeAttribute]
        [HttpPost]
        public ActionResult Edit(int id, bool isEditing, bool isColoredRow = false)
        {
            ModelState.Clear();

            int walletId = UserSession.CurrentUser.WalletId;

            using (ITransactionService transactionService = new TransactionService())
            {
                TransactionModel model = transactionService.Select(id, walletId);

                if (model != null)
                {
                    model.IsEditing = !isEditing;
                    model.IsColoredRow = isColoredRow;

                    return Json(new object[] { false, this.RenderRazorViewToString("RowControl", model) });
                }
                else
                {
                    return Json(new object[] { true, "Transaction not found or not belong to current user" });
                }
            }
        }

        [AjaxAuthorizeAttribute]
        [HttpPost]
        public ActionResult GetListControl()
        {
            int userId = UserSession.CurrentUser.Id;

            List<TransactionModel> listModel = this.SelectListModel();

            if (listModel != null)
            {
                TransactionListModel model = new TransactionListModel();
                model.PageSize = BaseController.pageSize;
                model.ListModel.AddRange(listModel);
                model.Total = SelectTransactionCount();

                return Json(new object[] { false, this.RenderRazorViewToString("TransactionListControl", model) });
            }

            return Json(new object[] { true });
        }

        [AjaxAuthorizeAttribute]
        [HttpPost]
        public ActionResult Save(TransactionModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int walletId = UserSession.CurrentUser.WalletId;

                    using (ITransactionService transactionService = new TransactionService())
                    {
                        model.WalletId = walletId;
                        model = transactionService.Update(model);

                        if (model != null)
                        {
                            WalletModel walletModel = null;

                            using (IWalletService walletService = new WalletService())
                            {
                                walletService.Update(UserSession.CurrentUser.Id, model.valueToTransact, model.ToDebit);
                                walletModel = walletService.Select(UserSession.CurrentUser.Id);
                            }

                            return Json(new object[] { false, this.RenderRazorViewToString("RowControl", model), this.RenderRazorViewToString("Wallet", walletModel) });
                        }
                        else
                        {
                            return Json(new object[] { true, false, "Transaction not found" });
                        }
                    }
                }
                catch (Exception)
                {
                    return Json(new object[] { true, false, "Something wrong just happened" });
                }
            }
            else
            {
                return Json(new object[] { true, true, this.RenderRazorViewToString("RowControl", model) });
            }
        }

        [AjaxAuthorizeAttribute]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            ModelState.Clear();

            int walletId = UserSession.CurrentUser.WalletId;

            using (ITransactionService transactionService = new TransactionService())
            {
                TransactionModel model = transactionService.Select(id, walletId);

                if (model != null)
                {
                    transactionService.Delete(model.Id, walletId);

                    WalletModel walletModel = null;

                    using (IWalletService walletService = new WalletService())
                    {
                        if (model.Type == TransactionType.Debit)
                        {
                            walletService.Update(UserSession.CurrentUser.Id, decimal.Parse(model.Value));
                        }
                        else
                        {
                            walletService.Update(UserSession.CurrentUser.Id, decimal.Parse(model.Value), true);
                        }

                        walletModel = walletService.Select(UserSession.CurrentUser.Id);
                    }

                    return Json(new object[] { false, this.RenderRazorViewToString("Wallet", walletModel) });
                }
                else
                {
                    return Json(new object[] { true, "Transaction not found or not belong to current user" });
                }
            }
        }

        public ActionResult Paginate(int pagina = 0)
        {
            int walletId = UserSession.CurrentUser.WalletId;
            TransactionListModel model = new TransactionListModel();
            List<TransactionModel> listModel = this.SelectListModel(pagina);

            if (listModel != null)
            {
                model.ListModel.AddRange(listModel);
                model.PageSize = BaseController.pageSize;
                model.Total = SelectTransactionCount();
                model.CurrentPageIndex = pagina;
            }

            return PartialView("TransactionListControl", model);
        }
    }
}
