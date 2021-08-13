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
    public class PartnerDaoTest
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;
        private PartnerDao partnerDao;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Configuration().Configure().BuildSessionFactory();
            this.session = this.sessionFactory.OpenSession();
            this.transaction = this.session.BeginTransaction();
            CurrentSessionContext.Bind(this.session);
            this.partnerDao = new PartnerDao(sessionFactory);
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

            IEnumerable<Partner> partner = partnerDao.GetAll();

            Assert.IsTrue(partner.Any());
        }
        [TestMethod]
        public void GetOne()
        {

            Partner partner = partnerDao.Get(1);

            Assert.IsNotNull(partner);

        }
        [TestMethod]
        public void Delete()
        {
            Partner partner = new Partner
            {
                FirstName = "Juan",
                LastName = "Perez",
                UserName = "JuanPerez"
            };

            this.session.Save(partner);
            this.session.Flush();
            this.session.Clear();

            partnerDao.Delete(partner);

            Partner created = this.session.Get<Partner>(partner.Id);

            Assert.IsNull(created);

        }
        [TestMethod]
        public void Save()
        {
            Partner partner = new Partner
            {
                FirstName = "Juan",
                LastName = "Perez",
                UserName = "JuanPerez"
            };


            partnerDao.Save(partner);

            Partner created = this.session.Get<Partner>(partner.Id);

            Assert.AreEqual(partner.Id, created.Id);

        }

        [TestMethod]
        public void GetByHqlQuery()
        {

            Partner partner = new Partner
            {
                FirstName = "Juan",
                LastName = "Perez",
                UserName = "JuanPerez"
            };

            partnerDao.Save(partner);

            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("FirstName", "%Juan%");

            Partner partner1 = partnerDao.GetUniqueByHqlQuery("FROM Partner WHERE FirstName LIKE :FirstName", parameters);

            Assert.IsNotNull(partner1);
            Assert.AreEqual("Juan", partner1.FirstName);
        }

        [TestMethod]
        public void GetByQuery()
        {
            Partner partner = new Partner
            {
                FirstName = "Juan",
                LastName = "Perez",
                UserName = "JuanPerez"
            };

            partnerDao.Save(partner);

            IDictionary<string, object> parameters = new Dictionary<string, object> { { "FirstName", "Juan" } };
            Partner partner1 = partnerDao.GetUniqueByQuery(parameters);

            Assert.IsNotNull(partner1);
            Assert.AreEqual("Juan", partner1.FirstName);
        }
        [TestMethod]
        public void GetByName()
        {
            Partner partner = new Partner
            {
                FirstName = "Juan",
                LastName = "Perez",
                UserName = "JuanPerez"
            };
            partnerDao.Save(partner);

            IEnumerable<Partner> partners = partnerDao.GetByName("Juan");

            foreach (Partner p in partners)
            {
                Assert.AreEqual(p.FirstName,"Juan");
            }

        }
        [TestMethod]
        public void GetByLastName()
        {
            Partner partner = new Partner
            {
                FirstName = "Juan",
                LastName = "Perez",
                UserName = "JuanPerez"
            };
            partnerDao.Save(partner);

            IEnumerable<Partner> partners = partnerDao.GetByLastName("Perez");

            foreach (Partner p in partners)
            {
                Assert.AreEqual(p.LastName, "Perez");
            }
        }
        [TestMethod]
        public void GetByUserName()
        {
            Partner partner = new Partner
            {
                FirstName = "Juan",
                LastName = "Perez",
                UserName = "JuanPerez"
            };
            partnerDao.Save(partner);

            Partner p = partnerDao.GetByUserName("JuanPerez");            
           
            Assert.AreEqual(p.UserName, "JuanPerez");
            
        }
    }
}
