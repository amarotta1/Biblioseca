using Biblioseca.Model;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace Biblioseca.DataAccess
{
    public class AuthorDao : Dao<Author>
    {
        public AuthorDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public virtual IEnumerable<Author> AuthorAllreadyExist(string name,string lastName)
        {
            ICriteria criteria = this.Session.CreateCriteria<Author>();
            criteria.Add(Restrictions.Eq("FirstName",name)); //greater than
            criteria.Add(Restrictions.Eq("LastName", lastName));
            return criteria.List<Author>();
        }

    }
}