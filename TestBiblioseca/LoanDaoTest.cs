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
    public class LoanDaoTest
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
            LoanDao loanDao = new LoanDao(this.sessionFactory);

            IEnumerable<Loan> authors = loanDao.GetAll();

            Assert.IsTrue(authors.Any());
        }
        [TestMethod]
        public void GetOne()
        {

            LoanDao loanDao = new LoanDao(this.sessionFactory);
            Loan loan = loanDao.Get(12);

            Assert.IsNotNull(loan);

        }
        [TestMethod]
        public void GetByPartnerId()
        {
            PartnerDao partnerDao = new PartnerDao(this.sessionFactory);
            LoanDao loanDao = new LoanDao(this.sessionFactory);

            Loan loan = loanDao.GetAllLoansByPartnerID(partnerDao.Get(1).Id).First();

            Assert.AreEqual(loan.partner.Id, 1);

        }
        

    }
}
