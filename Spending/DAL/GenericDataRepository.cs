using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Objects;
using System.Data;

namespace DAL
{
    public class DAOGeneric<T> : IDAOGeneric<T> where T : class
    {
        public IList<T> GetList(Func<T, bool> where, string navigation = null)
        {
            List<T> list;
            using (var context = new SpendingEntities())
            {
                var set = context.CreateObjectSet<T>();

                ObjectQuery<T> query = set;

                if (navigation != null)
                {
                    query = query.Include(navigation); 
                }

                list = query.Where(where).ToList<T>();
            }
            return list;
        }

        public T GetSingle(Func<T, bool> where, string navigation)
        {
            T item = null;
            using (var context = new SpendingEntities())
            {
                var set = context.CreateObjectSet<T>();
                ObjectQuery<T> query = set;

                if (navigation != null)
                {
                    query = query.Include(navigation);
                }

                item = query.FirstOrDefault(where);
            }
            return item;
        }

        public void Remove(T entity)
        {
            Work(entity, EntityState.Deleted);
        }

        public void Update(T entity)
        {
            Work(entity, EntityState.Modified);
        }

        public T Add(T entity)
        {
            using (var context = new SpendingEntities())
            {
                var set = context.CreateObjectSet<T>();
                set.AddObject(entity);
                context.ObjectStateManager.ChangeObjectState(entity, EntityState.Added);

                context.SaveChanges();
            }

            return entity;
        }

        private void Work(T entity, EntityState state)
        {
            using (var context = new SpendingEntities())
            {
                var set = context.CreateObjectSet<T>();
                set.Attach(entity);
                context.ObjectStateManager.ChangeObjectState(entity, state);

                context.SaveChanges();
            }
        }
    }
}
