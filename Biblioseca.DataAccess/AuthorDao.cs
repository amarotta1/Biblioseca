using Biblioseca.Model;
using NHibernate;

namespace Biblioseca.DataAccess
{
    public class AuthorDao : Dao<Author>
    {
        public AuthorDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

    }
}