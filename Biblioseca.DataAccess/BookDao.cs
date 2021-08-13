using Biblioseca.Model;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace Biblioseca.DataAccess
{
    public class BookDao : Dao<Book>
    {
        public BookDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public IEnumerable<Book> GetByAuthorName(string authorName)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Book>();

            if (!string.IsNullOrEmpty(authorName))
            {
                criteria.CreateCriteria("author")
                   .Add(Restrictions.Like("FirstName", authorName, MatchMode.Anywhere));
            }

            return criteria.List<Book>();
        }

        public IEnumerable<Book> GetByCategoryName(string categoryName)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Book>();

            if (!string.IsNullOrEmpty(categoryName))
            {
                criteria.CreateCriteria("category")
                   .Add(Restrictions.Like("name", categoryName, MatchMode.Anywhere));
            }

            return criteria.List<Book>();
        }

        public virtual IEnumerable<Book> GetAllAvailableBooks()
        {
            ICriteria criteria = this.Session.CreateCriteria<Book>();
            criteria.Add(Restrictions.Gt("stock",0)); //greater than
            return criteria.List<Book>();
        }


    }
}