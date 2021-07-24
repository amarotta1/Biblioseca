using System.Collections.Generic;
using NHibernate;

namespace Biblioseca.DataAccess
{
    public abstract class Dao<T> : IDao<T>
    {
        private readonly ISessionFactory sessionFactory;

        protected Dao(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public void Save(T entity)
        {
            this.sessionFactory
                .GetCurrentSession()
                .Save(entity);
        }

        public void Delete(T entity)
        {
            this.sessionFactory
                .GetCurrentSession()
                .Delete(entity);
        }

        public T Get(int id)
        {
            return this.sessionFactory
                .GetCurrentSession()
                .Get<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return this.sessionFactory
                .GetCurrentSession()
                .Query<T>();
        }
    }
}