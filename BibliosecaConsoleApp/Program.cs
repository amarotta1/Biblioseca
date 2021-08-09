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

            Console.ReadKey();


        }
    }
}
