using Biblioseca.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;

namespace TestBiblioseca
{
    [TestClass]
    public class PartnerTests
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
        public void CreatePartner()
        {
            Partner par = new Partner
            {
                FirstName = "Juan",
                LastName = "Pedro",
                UserName = "JPedro"
            };

            this.session.Save(par);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(par.Id > 0);

            Partner created = this.session.Get<Partner>(par.Id);

            Assert.AreEqual(par.Id, created.Id);
        }
        [TestMethod]
        public void TestPropertys()
        {
            Partner par = new Partner
            {
                FirstName = "Juan",
                LastName = "Pedro",
                UserName = "JPedro"
            };

            this.session.Save(par);
            this.session.Flush();
            this.session.Clear();

            Partner created = this.session.Get<Partner>(par.Id);

            Assert.AreEqual(par.FirstName, created.FirstName);
            Assert.AreEqual(par.LastName, created.LastName);
            Assert.AreEqual(par.UserName, created.UserName);

        }
        [TestMethod]
        public void IsDeleted()
        {
            Partner par = new Partner
            {
                FirstName = "Juan",
                LastName = "Pedro",
                UserName = "JPedro"
            };

            this.session.Save(par);
            this.session.Flush();
            this.session.Clear();

            par.MarkAsDeleted();
            this.session.Save(par);
            Partner created = this.session.Get<Partner>(par.Id);
            
            Assert.IsTrue(created.Deleted);

        }
    }
}