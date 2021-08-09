using Biblioseca.Model;
using NHibernate;

namespace Biblioseca.DataAccess
{
    public class CategoryDao : Dao<Category>
    {
        public CategoryDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

    }
}