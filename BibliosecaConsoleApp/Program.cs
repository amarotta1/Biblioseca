using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;

namespace BibliosecaConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ISessionFactory sessionFactory = new Configuration().Configure().BuildSessionFactory();

            ISession session = sessionFactory.OpenSession();
            //abre una conexion TCP contra el servidor

            /* LIBROS
            Book b1 = new Book();
            Book b2 = new Book();
            Book b3 = new Book();
            Book b4 = new Book();

            b1.author = session.Get<Author>(1);
            b1.category = session.Get<Category>(1);
            b1.description = "Es un libro sobre el vampiro más famoso del mundo";
            b1.title = "Dracula";
            b1.isbn = 1234;

            b2.author = session.Get<Author>(2);
            b2.category = session.Get<Category>(1);
            b2.description = "Este misterio te atrapara y asustara mucho";
            b2.title = "El misterio de Salem";
            b2.isbn = 9783;

            b3.author = session.Get<Author>(3);
            b3.category = session.Get<Category>(2);
            b3.description = "Descubriras casos con el detective mas inteligente de todos";
            b3.title = "Sherlock Holmes";
            b3.isbn = 7883;

            b4.author = session.Get<Author>(7);
            b4.category = session.Get<Category>(3);
            b4.description = "Vamos a buscar las esferas del dragon";
            b4.title = "Dragon Ball";
            b4.isbn = 8699;


            session.Save(b1); session.Save(b2); session.Save(b3); session.Save(b4);
            var allbooks = session.QueryOver<Book>().List();
            Console.WriteLine("Todos los libros son: ");

            foreach (var item in allbooks)
            {
                Console.WriteLine($"Id: {item.Id} - Titulo: {item.title } - Autor: {item.author.FirstName} {item.author.LastName} ");
            }
            
            */

            /* PARTNERS
            Partner p1 = new Partner();
            Partner p2 = new Partner();
            Partner p3 = new Partner();
            Partner p4 = new Partner();

            p1.FirstName = "Joaquin";
            p1.LastName = "Reina";
            p1.UserName = "Jreina";

            p2.FirstName = "Santiago";
            p2.LastName = "Guiña";
            p2.UserName = "SGuiña";

            p3.FirstName = "Francisco";
            p3.LastName = "Verdaguer";
            p3.UserName = "polloV";

            p4.FirstName = "Agustin";
            p4.LastName = "Lima";
            p4.UserName = "ALima";

            session.Save(p1); session.Save(p2); session.Save(p3); session.Save(p4);

            */

            /* LOANS  --> ver tema que no existe la fecha nula para el returned
            Loan l1 = new Loan();
            Loan l2 = new Loan();
            Loan l3 = new Loan();
            Loan l4 = new Loan();

            l1.book = session.Get<Book>(1);
            l1.partner = session.Get<Partner>(1);
            l1.initialDate = DateTime.Now;
            l1.finishDate = DateTime.Now.AddDays(3);

            l2.book = session.Get<Book>(2);
            l2.partner = session.Get<Partner>(2);
            l2.initialDate = DateTime.Now.AddDays(-1);//Simular que lo pidio ayer
            l2.finishDate = DateTime.Now.AddDays(2);

            l3.book = session.Get<Book>(3);
            l3.partner = session.Get<Partner>(3);
            l3.initialDate = DateTime.Now.AddDays(-4);//Simular que se atraso
            l3.finishDate = DateTime.Now.AddDays(-1);

            l4.book = session.Get<Book>(4);
            l4.partner = session.Get<Partner>(4);
            l4.initialDate = DateTime.Now.AddDays(-5);//Simular que lo devuelve a tiempo
            l4.finishDate = DateTime.Now.AddDays(-2);
            l4.returnedDate = DateTime.Now.AddDays(-3);

            session.Save(l1); session.Save(l2); session.Save(l3); session.Save(l4);
            */

            Penalty penalty = new Penalty();

            penalty.loan = session.Get<Loan>(3);
            penalty.initialDate = DateTime.Now;
            penalty.finishDate = DateTime.Now.AddDays(5);

            session.Save(penalty);
            Console.ReadKey();
        }
    }
}
