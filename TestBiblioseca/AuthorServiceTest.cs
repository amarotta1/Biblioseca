using Biblioseca.DataAccess;
using Biblioseca.Model;
using Biblioseca.Service;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
namespace TestBiblioseca
{
    [TestClass]
    public class AuthorServiceTests
    {
        private AuthorService authorService;
        private Mock<AuthorDao> authorDao;
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;


        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Mock<ISessionFactory>();
            this.session = new Mock<ISession>();
            this.authorDao = new Mock<AuthorDao>(this.sessionFactory.Object);
        }

        [TestMethod]
        public void GetAll()
        {
            
            this.authorDao.Setup(dao => dao.GetAll()).Returns(GetAuthors());

            this.authorService = new AuthorService(this.authorDao.Object);
            IEnumerable<Author> authors = this.authorService.GetAll();

            Assert.IsTrue(authors.Any());

        }

        [TestMethod]
        public void GetOneByID()
        {
            const int authorId = 1;
            this.authorDao.Setup(dao => dao.Get(authorId)).Returns(GetAuthor());
            this.authorService = new AuthorService(this.authorDao.Object);
            Author author = authorService.GetOnebyId(authorId);
            Assert.IsNotNull(author);
            Assert.IsInstanceOfType(author, typeof(Author));

        }
        [TestMethod]
        public void AlsoCreate()
        {
            const string name = "Juan";
            const string last = "Perez";
            
            this.authorDao.Setup(dao => dao.AuthorAllreadyExist(name,last)).Returns(GetAuthors());
            this.authorService = new AuthorService(this.authorDao.Object);

            Assert.ThrowsException<Exception>(() => this.authorService.Create(name,last),
                "El autor ya existe");

        }
        [TestMethod]
        public void Deleted()
        {

            ISessionFactory sessionFactory = new Configuration().Configure().BuildSessionFactory();
            ISession session = sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            AuthorDao authorDao = new AuthorDao(sessionFactory);
            this.authorService = new AuthorService(authorDao);
            Author a = authorDao.Get(1);
            authorService.Delete(1);

            Assert.IsTrue(a.Deleted);

            a.Deleted = false; //para volverlo a poner bien
            authorDao.Save(a);

        }

        [TestMethod]
        public void Update()
        {
            ISessionFactory sessionFactory = new Configuration().Configure().BuildSessionFactory();
            ISession session = sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            AuthorDao authorDao = new AuthorDao(sessionFactory);
            this.authorService = new AuthorService(authorDao);

            authorService.Update(1,"Ernesto","Perez");
            Author a = authorDao.Get(1);
            Assert.AreEqual(a.LastName,"Perez");

            a.LastName = "Lopez";
            authorDao.Save(a);
        }


        private static Author GetAuthor()
        {
            Author author = new Author
            {
                FirstName = "Juan",
                LastName = "Perez"
            };
            return author;
        }

       
        public static IEnumerable<Author> GetAuthors()
        {
            List<Author> autores = new List<Author>
            {
                new Author
                {
                    FirstName = "Juan",
                    LastName = "Perez"
                },
                new Author
                {
                    FirstName = "Martin",
                    LastName = "Paez"
                },
                new Author
                {
                    FirstName = "Julian",
                    LastName = "Cerro"
                }

            };
            return autores;

        }


    }
}