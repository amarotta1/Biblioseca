using Biblioseca.Model;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace Biblioseca.DataAccess
{
    public class PartnerDao : Dao<Partner>
    {
        public PartnerDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public IEnumerable<Partner> GetByName(string partnerName)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Partner>();

            if (!string.IsNullOrEmpty(partnerName))
            {
                criteria.Add(Restrictions.Like("FirstName", partnerName, MatchMode.Anywhere));
            }

            return criteria.List<Partner>();
        }

        public IEnumerable<Partner> GetByLastName(string partnerLastName)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Partner>();

            if (!string.IsNullOrEmpty(partnerLastName))
            {
                criteria.Add(Restrictions.Like("LastName", partnerLastName, MatchMode.Anywhere));
            }

            return criteria.List<Partner>();
        }

        public Partner GetByUserName(string partnerUserName)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Partner>();

            if (!string.IsNullOrEmpty(partnerUserName))
            {
                criteria.Add(Restrictions.Like("UserName", partnerUserName, MatchMode.Anywhere));
            }

            return criteria.UniqueResult<Partner>(); //Deberia haber uno solo
        }

    }
}