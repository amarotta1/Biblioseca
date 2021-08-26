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

        public virtual void Save(T entity)
        {
            this.Session
                .SaveOrUpdate(entity);

            ITransaction transaction = Session.GetCurrentTransaction();

            if (transaction == null)
            {
                transaction = Session.BeginTransaction();
                transaction.Commit();
            }            
            
            //Session.GetCurrentTransaction().Commit();
            
        }

        public void Delete(T entity)
        {
            this.sessionFactory
                .GetCurrentSession()
                .Delete(entity);
        }

        public virtual T Get(int id)
        {
            ICriteria criteria = this.Session.CreateCriteria(typeof(T));
            criteria.Add(Restrictions.Eq("Id", id));
            criteria.Add(Restrictions.Eq("Deleted", false));            

            return criteria.UniqueResult<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            ICriteria criteria = this.Session.CreateCriteria(typeof(T));
            criteria.Add(Restrictions.Eq("Deleted",false));

            return criteria.List<T>();
        }

        public virtual T GetUniqueByHqlQuery(string queryString, IDictionary<string, object> parameters)
        {
            IQuery query = this.Session
                .CreateQuery(queryString);

            foreach (KeyValuePair<string, object> keyValue in parameters)
            {
                query.SetParameter(keyValue.Key, keyValue.Value);
            }

            // return query.UniqueResult<T>(); Si la Query trae mas de 1 resultado falla

            return query.List<T>()[0];
        }

        public virtual T GetUniqueByQuery(IDictionary<string, object> parameters)
        {
            ICriteria criteria = this.Session.CreateCriteria(typeof(T));

            foreach (KeyValuePair<string, object> keyValue in parameters)
            {
                criteria.Add(Restrictions.Eq(keyValue.Key, keyValue.Value));
            }
            // return criteria.UniqueResult<T>();
            return criteria.List<T>()[0];
        }
 
    }
}