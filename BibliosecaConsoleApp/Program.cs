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

            Author author = new Author();
            author.FirstName = "Ale";
            author.LastName = "Marotta";

            session.Save(author);
            Console.WriteLine("El autor añadido tiene id: " + author.Id);
            var allAuthors = session.QueryOver<Author>().List();
            Console.WriteLine("Todos los autores son: ");
            foreach (var item in allAuthors)
            {
                Console.WriteLine($"Id: {item.Id} - Nombre: {item.FirstName } - Apellido: {item.LastName}");
            }
            Console.ReadKey();
        }
    }
}
