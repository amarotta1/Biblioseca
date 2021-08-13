using Biblioseca.Model;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;


namespace Biblioseca.DataAccess
{
    public class LoanDao : Dao<Loan>
    {
        public LoanDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public IEnumerable<Loan> GetByPartnerLastName(string partnerLastName)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Loan>();

            if (!string.IsNullOrEmpty(partnerLastName))
            {
                criteria.CreateCriteria("partner")
                   .Add(Restrictions.Like("LastName", partnerLastName, MatchMode.Anywhere));
            }

            return criteria.List<Loan>();
        }

        public IEnumerable<Loan> GetAllLoansByPartnerID(int partnerId)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Loan>();

            criteria.CreateCriteria("partner")
                .Add(Restrictions.Eq("Id", partnerId));

            return criteria.List<Loan>();
        }
        public virtual IEnumerable<Loan> GetActualLoansByPartnerID(int partnerId)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Loan>();
                        
            criteria.CreateCriteria("partner")
                .Add(Restrictions.Eq("Id", partnerId));
            criteria.Add(Restrictions.Eq("returnedDate", null));

            return criteria.List<Loan>();
        }

        public virtual IEnumerable<Loan> GetActualLoansByBookId(int bookId)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Loan>();
          
            criteria.CreateCriteria("book")
                .Add(Restrictions.Eq("Id", bookId));
            criteria.Add(Restrictions.Eq("returnedDate",null));

            return criteria.List<Loan>();
        }

        public virtual Loan GetUniqueLoanByBookAndPartner(int bookID, int partnerID)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Loan>();
            criteria.CreateCriteria("book")
                .Add(Restrictions.Eq("Id", bookID));
            criteria.CreateCriteria("partner")
                .Add(Restrictions.Eq("Id", partnerID));
            criteria.Add(Restrictions.Eq("returnedDate", null));

            return criteria.UniqueResult<Loan>();
        }

    }
}