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
    public class CategoryDaoTest
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
            CategoryDao categoryDao = new CategoryDao(this.sessionFactory);

            IEnumerable<Category> authors = categoryDao.GetAll();

            Assert.IsTrue(authors.Any());
        }
        [TestMethod]
        public void GetOne()
        {

            CategoryDao categoryDao = new CategoryDao(this.sessionFactory);
            Category cat1 = categoryDao.Get(1);

            Assert.IsNotNull(cat1);

        }
        [TestMethod]
        public void Delete()
        {
            Category cat = new Category {name = "Accion" };

            this.session.Save(cat);
            this.session.Flush();
            this.session.Clear();

            CategoryDao categoryDao = new CategoryDao(this.sessionFactory);
            categoryDao.Delete(cat);

            Author created = this.session.Get<Author>(cat.Id);

            Assert.IsNull(created);

        }
        [TestMethod]
        public void Save()
        {
            Category cat = new Category { name = "Accion" };

            CategoryDao categoryDao = new CategoryDao(this.sessionFactory);
            categoryDao.Save(cat);

            Category created = this.session.Get<Category>(cat.Id);

            Assert.AreEqual(cat.Id, created.Id);

        }

        [TestMethod]
        public void GetByHqlQuery()
        {
            CategoryDao categoryDao = new CategoryDao(this.sessionFactory);

            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("name", "%Terror%");

            Category cat = categoryDao.GetUniqueByHqlQuery("FROM Category WHERE name LIKE :name", parameters);

            Assert.IsNotNull(cat);
            Assert.AreEqual("Terror", cat.name);
        }

        [TestMethod]
        public void GetByQuery()
        {
            CategoryDao categoryDao = new CategoryDao(this.sessionFactory);

            IDictionary<string, object> parameters = new Dictionary<string, object> { { "name", "Terror" } };
            Category cat = categoryDao.GetUniqueByQuery(parameters);

            Assert.IsNotNull(cat);
            Assert.AreEqual("Terror", cat.name);
        }
    }
}
