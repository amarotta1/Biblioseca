using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using Biblioseca.DataAccess;
using Biblioseca.Model;
using System.Collections.Generic;
using System.Linq;

namespace TestBiblioseca
{
    [TestClass]
    public class BookDaoTest
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;
        private BookDao bookDao;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Configuration().Configure().BuildSessionFactory();
            this.session = this.sessionFactory.OpenSession();
            this.transaction = this.session.BeginTransaction();
            CurrentSessionContext.Bind(this.session);
            this.bookDao = new BookDao(this.sessionFactory);
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.transaction.Rollback();
            this.session.Close();
        }

        [TestMethod]
        public void GetAll()
        {
            IEnumerable<Book> books = bookDao.GetAll();

            Assert.IsTrue(books.Any());
        }
        [TestMethod]
        public void GetOne()
        {            
            Book book1 = bookDao.Get(1);

            Assert.IsNotNull(book1);

        }
        [TestMethod]
        public void Delete()
        {
            CategoryDao catDao = new CategoryDao(this.sessionFactory);
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);

            Category cat1 = catDao.Get(1);
            Author author = authorDao.Get(1);

            Book book = new Book {title = "Prueba", category = cat1,
                author = author, isbn = "8956432164895", description ="Libro de prueba" };

            this.session.Save(book);
            this.session.Flush();
            this.session.Clear();

            bookDao.Delete(book);

            Book created = this.session.Get<Book>(book.Id);

            Assert.IsNull(created);

        }
        [TestMethod]
        public void Save()
        {
            CategoryDao catDao = new CategoryDao(this.sessionFactory);
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);
            Category cat1 = catDao.Get(1);
            Author author = authorDao.Get(1);

            Book book = new Book
            {
                title = "Prueba",
                category = cat1,
                author = author,
                isbn = "6556432164895",
                description = "Libro de prueba"
            };

            this.session.Save(book);
            this.session.Flush();
            this.session.Clear();

            Book created = this.session.Get<Book>(book.Id);

            Assert.AreEqual(book.Id, created.Id);

        }

        [TestMethod]
        public void GetByHqlQuery()
        {
            
            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("title", "%Sherlock%");

            Book book = bookDao.GetUniqueByHqlQuery("FROM Book WHERE title LIKE :title", parameters);

            Assert.IsNotNull(book);
            Assert.AreEqual("Sherlock Holmes", book.title);
        }

        [TestMethod]
        public void GetByAuthorHqlQuery()
        {
            AuthorDao authorDao = new AuthorDao(sessionFactory);
            Author author = authorDao.Get(1);
            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("author", author);

            Book book = bookDao.GetUniqueByHqlQuery("FROM Book WHERE author = :author", parameters);

            Assert.IsNotNull(book);
            Assert.AreEqual("Ernesto", book.author.FirstName);
        }

        [TestMethod]
        public void GetByQuery()
        {
            IDictionary<string, object> parameters = new Dictionary<string, object> { { "title", "Sherlock Holmes" } };
            Book book = bookDao.GetUniqueByQuery(parameters);

            Assert.IsNotNull(book);
            Assert.AreEqual("Sherlock Holmes", book.title);
        }


        [TestMethod]
        public void GetByAuthor()
        {
            
            IEnumerable<Book> books = bookDao.GetByAuthorName("Akira");

            Assert.IsNotNull(books);
            foreach (Book book in books)
            {
                Assert.AreEqual("Akira", book.author.FirstName);
            }
            
        }

        [TestMethod]
        public void GetByCategory()
        {

            IEnumerable<Book> books = bookDao.GetByCategoryName("Terror");

            Assert.IsNotNull(books);
            foreach (Book book in books)
            {
                Assert.AreEqual("Terror", book.category.name);
            }

        }

    }
}
