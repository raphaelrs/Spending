using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace DAL
{
    public interface IDAOGeneric<T> where T : class
    {
        IList<T> GetList(Func<T, bool> where, string navigation = null);
        T GetSingle(Func<T, bool> where, string navigation = null);
        T Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
