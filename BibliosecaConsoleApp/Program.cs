using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;
using Biblioseca.DataAccess;



namespace BibliosecaConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ISessionFactory sessionFactory = new Configuration().Configure().BuildSessionFactory();

            ISession session = sessionFactory.OpenSession();
            //abre una conexion TCP contra el servidor

            AuthorDao authorDao = new AuthorDao(sessionFactory);

            Console.WriteLine(authorDao.Get(1));
            Console.ReadLine();
            
            session.Close();

        }
    }
}
