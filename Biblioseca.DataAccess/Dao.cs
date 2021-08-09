using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace Biblioseca.DataAccess
{
    public abstract class Dao<T> : IDao<T>
    {
        private readonly ISessionFactory sessionFactory;

        protected Dao(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public virtual ISession Session
        {
            get { return this.sessionFactory.GetCurrentSession(); }
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

        public T GetUniqueByHqlQuery(string queryString, IDictionary<string, object> parameters)
        {
            IQuery query = this.Session
                .CreateQuery(queryString);

            foreach (KeyValuePair<string, object> keyValue in parameters)
            {
                query.SetParameter(keyValue.Key, keyValue.Value);
            }

            return query.UniqueResult<T>();
        }

        public T GetUniqueByQuery(IDictionary<string, object> parameters)
        {
            ICriteria criteria = this.Session.CreateCriteria(typeof(T));

            foreach (KeyValuePair<string, object> keyValue in parameters)
            {
                criteria.Add(Restrictions.Eq(keyValue.Key, keyValue.Value));
            }

            return criteria.UniqueResult<T>();
        }
 
    }
}