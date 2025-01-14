﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class AuthorDaoTest
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Configuration().Configure().BuildSessionFactory();
            this.session = this.sessionFactory.OpenSession();
            this.transaction = this.session.BeginTransaction();
            CurrentSessionContext.Bind(this.session);
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
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);

            IEnumerable<Author> authors = authorDao.GetAll();

            Assert.IsTrue(authors.Any());
        }
        [TestMethod]
        public void GetOne()
        {
            
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);
            Author author1 = authorDao.Get(1);

            Assert.IsNotNull(author1);

        }
        [TestMethod]
        public void GetOneThatWasDeleted()
        {
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);
            Author author1 = authorDao.Get(1);
            author1.MarkAsDeleted();

            authorDao.Save(author1);

            Author author2 = authorDao.Get(1);

            Assert.IsNull(author2);

        }
        [TestMethod]
        public void Delete()
        {
            Author author = new Author
            {
                FirstName = "Juan",
                LastName = "Perez"
            };

            this.session.Save(author);
            this.session.Flush();
            this.session.Clear();

            AuthorDao authorDao = new AuthorDao(this.sessionFactory);
            authorDao.Delete(author);

            Author created = this.session.Get<Author>(author.Id);

            Assert.IsNull(created);

        }
        [TestMethod]
        public void Save()
        {
            Author author = new Author
            {
                FirstName = "Juan",
                LastName = "Perez"
            };

            AuthorDao authorDao = new AuthorDao(this.sessionFactory);
            authorDao.Save(author);

            Author created = this.session.Get<Author>(author.Id);

            Assert.AreEqual(author.Id, created.Id);

        }

        [TestMethod]
        public void GetByHqlQuery()
        {
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);

            Author author = new Author
            {
                FirstName = "Akira",
                LastName = "Toriyama"
            };

            authorDao.Save(author);

            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("FirstName", "%Akira%");

            Author author1 = authorDao.GetUniqueByHqlQuery("FROM Author WHERE FirstName LIKE :FirstName", parameters);

            Assert.IsNotNull(author1);
            Assert.AreEqual("Akira", author1.FirstName);
        }

        [TestMethod]
        public void GetByQuery()
        {
            AuthorDao authorDao = new AuthorDao(this.sessionFactory);

            Author author = new Author
            {
                FirstName = "Akira",
                LastName = "Toriyama"
            };

            authorDao.Save(author);

            IDictionary<string, object> parameters = new Dictionary<string, object> { { "FirstName", "Akira" } };
            Author author1 = authorDao.GetUniqueByQuery(parameters);

            Assert.IsNotNull(author1);
            Assert.AreEqual("Akira", author1.FirstName);
        }
    }
}
