using Biblioseca.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;

namespace TestBiblioseca
{
    [TestClass]
    public class CategoryTest
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;

        [TestInitialize]
        public void SetUp()
        {
            sessionFactory = new Configuration().Configure().BuildSessionFactory();
            this.session = this.sessionFactory.OpenSession();
            this.transaction = this.session.BeginTransaction();
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.transaction.Rollback();
            this.session.Close();
        }

        [TestMethod]
        public void CreateCategory()
        {
            Category category = new Category
            {
                name = "suspenso"
            };

            this.session.Save(category);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(category.Id > 0);

            Category created = this.session.Get<Category>(category.Id);

            Assert.AreEqual(category.Id, created.Id);
        }
        [TestMethod]
        public void IsDeleted()
        {
            Category category = new Category
            {
                name = "suspenso"
            };

            this.session.Save(category);
            this.session.Flush();
            this.session.Clear();

            category.MarkAsDeleted();
            this.session.Save(category);

            Category created = this.session.Get<Category>(category.Id);

            Assert.IsTrue(created.Deleted);

        }
    }
}
