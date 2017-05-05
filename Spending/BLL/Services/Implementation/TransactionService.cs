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
    public class TransactionService :ITransactionService
    {
        private TransactionModel TransactionDatabaseError()
        {
            return new TransactionModel()
            {
                Error = "database error, try again later."
            };
        }

        private TransactionModel TransactionToModel(Transaction transaction)
        {
            return new TransactionModel()
            {
                Date = transaction.Date,
                Id = transaction.Id,
                Subject = transaction.Subject,
                Time = transaction.Time,
                Type = transaction.Type ? TransactionType.Debit : TransactionType.Credit,
                Value = transaction.Value.ToString(),
                WalletId = transaction.WalletId
            };
        }

        private Transaction ModelToTransaction(TransactionModel model)
        {
            return new Transaction()
            {
                Date = model.Date.GetValueOrDefault(),
                Subject = model.Subject,
                Time = model.Time.GetValueOrDefault(),
                Type = model.Type == TransactionType.Credit ? false : true,
                Value = decimal.Parse(model.Value)
            };
        }

        public TransactionModel Select(int id, int walletId)
        {
            try
            {
                using (IDAOTransaction daoTransaction = new DAOTransaction())
                {
                    Transaction transaction = daoTransaction.GetSingle(x => x.Id == id && x.WalletId == walletId);
                    //Transaction transaction = daoTransaction.Select(id, walletId);

                    if (transaction != null)
                    {
                        return this.TransactionToModel(transaction);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }
        
        public List<TransactionModel> Paginate(int currentPageIndex, int pageSize, int walletId)
        {
            List<TransactionModel> listModel = null;

            try
            {
                using (IDAOTransaction daoTransaction = new DAOTransaction())
                {
                    var result = daoTransaction.GetList(x => x.WalletId == walletId);

                    List<Transaction> transactions = daoTransaction.Paginate(currentPageIndex, pageSize, walletId);

                    if (transactions != null)
                    {
                        listModel = new List<TransactionModel>();
                        

                        foreach (var item in transactions)
                        {
                            listModel.Add(TransactionToModel(item));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return listModel;
        }
                
        public int SelectCount(int walletId)
        {
            int count = 0;

            try
            {
                using (IDAOTransaction daoTransaction = new DAOTransaction())
                {
                    count = daoTransaction.GetTotal(walletId);

                }
            }
            catch (Exception)
            {
                throw;
            }

            return count;
        }

        public TransactionModel Insert(TransactionModel transactionModel)
        {
            try
            {
                using (IDAOTransaction daoTransaction = new DAOTransaction())
                {
                    Transaction transaction = this.ModelToTransaction(transactionModel);
                    transaction.WalletId = transactionModel.WalletId;
                    transaction = daoTransaction.Add(transaction);
                    return this.TransactionToModel(transaction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TransactionModel Update(TransactionModel transactionModel)
        {
            try
            {
                using (IDAOTransaction daoTransactionModel = new DAOTransaction())
                {
                    Transaction transaction = daoTransactionModel.GetSingle(x => x.Id == transactionModel.Id && x.WalletId == transactionModel.WalletId);
                    //Transaction transaction = daoTransactionModel.GetSingle(transactionModel.Id, transactionModel.WalletId);

                    if (transaction != null)
                    {
                        int transactionId = transaction.Id;

                        decimal oldValue = transaction.Value;
                        TransactionType oldtype = transaction.Type ? TransactionType.Debit : TransactionType.Credit;

                        transaction = this.ModelToTransaction(transactionModel);
                        transaction.Id = transactionId;
                        transaction.WalletId = transactionModel.WalletId;

                        daoTransactionModel.Update(transaction);

                        transactionModel = this.TransactionToModel(transaction);

                        this.ColectValueToWallet(transactionModel, oldValue, decimal.Parse(transactionModel.Value), oldtype, transactionModel.Type);

                        return transactionModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int transactionId, int walletId)
        {
            using (IDAOTransaction daoTransaction = new DAOTransaction())
            {
                Transaction transaction = daoTransaction.GetSingle(x => x.Id == transactionId && x.WalletId == walletId);
                daoTransaction.Remove(transaction);
            }
        }

        private void ColectValueToWallet(TransactionModel model, decimal oldvalue, decimal newValue, TransactionType oldType, TransactionType newType)
        {
            bool credit = false;
            decimal result = this.Calculate(oldvalue, newValue, oldType, newType, ref credit);

            model.ToDebit = !credit;
            model.valueToTransact = result;
        }
        
        private decimal Calculate(decimal oldvalue, decimal newValue, TransactionType oldType, TransactionType newType, ref bool credit)
        {
            decimal result = 0;

            if (oldType != newType)
            {
                if (oldType == TransactionType.Debit)
                {
                    credit = true;
                    result = oldvalue + newValue;
                }
                else
                {
                    credit = false;
                    result = oldvalue + newValue;
                }
            }
            else
            {
                if (oldType == TransactionType.Debit)
                {
                    if (oldvalue > newValue)
                    {
                        credit = true;
                        result = oldvalue - newValue;
                    }
                    else if (oldvalue < newValue)
                    {
                        credit = false;
                        result = newValue - oldvalue;
                    }
                }
                else
                {
                    if (oldvalue > newValue)
                    {
                        credit = false;
                        result = oldvalue - newValue;
                    }
                    else if (oldvalue < newValue)
                    {
                        credit = true;
                        result = newValue - oldvalue;
                    }
                }
            }

            return result;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
