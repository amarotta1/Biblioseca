using Biblioseca.Model;
using NHibernate;

namespace Biblioseca.DataAccess
{
    public class PartnerDao : Dao<Partner>
    {
        public PartnerDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

    }
}