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

            AuthorDao authorDao = new AuthorDao(sessionFactory);

            LoanDao lDao = new LoanDao(sessionFactory);
            PartnerDao pDao = new PartnerDao(sessionFactory);
            BookDao bDao = new BookDao(sessionFactory);

            LoanService ls = new LoanService(lDao,bDao,pDao);

            IEnumerable<Author> autores = authorDao.GetAll();

            foreach (Author a in autores)
            {
                Console.WriteLine(a.Id);
            }

            LoanService loanService = new LoanService(lDao, bDao, pDao);
            IEnumerable<Loan> actualLoans = loanService.GetActualLoansByPartnerID(1);

            if (actualLoans.Any())
            {
                foreach (Loan loan in actualLoans)
                {
                    if (loan.book != null)
                    {
                        //books.Add(loan.book);
                    }

                }




                Console.ReadKey();
           
            // Book b = bDao.Get(1);
            //b.IncreaseStock();
           // bDao.Save(b);
          

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
