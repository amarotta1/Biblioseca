
using Biblioseca.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;

namespace TestBiblioseca
{
    [TestClass]
    public class AuthorTests
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
        public void CreateAuthor()
        {
            Author author = new Author
            {
                FirstName = "Juan",
                LastName = "Perez"
            };

            this.session.Save(author);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(author.Id > 0);

            Author created = this.session.Get<Author>(author.Id);
            
            Assert.AreEqual(author.Id, created.Id);
        }
        [TestMethod]
        public void IsDeleted()
        {
            Author author = new Author
            {
                FirstName = "Juan",
                LastName = "Perez"
            };
            this.session.Save(author);
            this.session.Flush();
            this.session.Clear();

            author.MarkAsDeleted();
            this.session.Save(author);
            Author created = this.session.Get<Author>(author.Id);
            Assert.IsTrue(created.Deleted);
        }
    }
}