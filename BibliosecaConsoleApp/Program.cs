using System;
using NHibernate;
using NHibernate.Cfg;
using Biblioseca.DataAccess;
using NHibernate.Context;
using System.Collections.Generic;
using Biblioseca.Model;
using System.Linq;
using Biblioseca.Service;

namespace BibliosecaConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ISessionFactory sessionFactory = new Configuration().Configure().BuildSessionFactory();

            ISession session = sessionFactory.OpenSession();
            //abre una conexion TCP contra el servidor
            //ITransaction transaction = session.BeginTransaction();
            CurrentSessionContext.Bind(session);

            LoanDao loanDao = new LoanDao(sessionFactory);

            IEnumerable<Loan> list = loanDao.GetActualLoansByBookId(155);
            Console.WriteLine(list.Count());

            BookDao bookDao = new BookDao(sessionFactory);
            BookService bookService = new BookService(bookDao);

            Console.WriteLine(bookService.ISBNVerification(bookDao.Get(1).isbn));

            AuthorDao authorDao = new AuthorDao(sessionFactory);

            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("FirstName", "%Juan%");

            Author author1 = authorDao.GetUniqueByHqlQuery("FROM Author WHERE FirstName LIKE :FirstName", parameters);
            Console.WriteLine(author1.ToString());

            Console.WriteLine(loanDao.GetActualLoansByPartnerID(1).Count() >=2);
            Console.WriteLine(GetLoans().Count() >= 2);
            PartnerDao partnerDao = new PartnerDao(sessionFactory);

            LoanService loanService = new LoanService(loanDao,bookDao,partnerDao);
            loanService.LoanABook(1, 4568745);


            Console.ReadKey();

            

        }

        private static IEnumerable<Loan> GetLoans()
        {
            List<Loan> loans = new List<Loan>
            {
                new Loan
                {
                    Id = 1
                },
                new Loan
                {
                    Id = 2
                },
                new Loan
                {
                    Id = 3
                }
            };

            return loans;
        }
    }
}
