using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;

namespace TestBiblioseca
{
    [TestClass]
    public class PenaltyTest
    {

        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;
        Loan loan;

        [TestInitialize]
        public void SetUp()
        {
            sessionFactory = new Configuration().Configure().BuildSessionFactory();
            this.session = this.sessionFactory.OpenSession();
            this.transaction = this.session.BeginTransaction();
            this.loan = session.Get<Loan>(1);
        }
        [TestMethod]
        public void TestCreatedPenalty()
        {
            Penalty penalty = new Penalty();

            penalty.loan = this.loan;
            penalty.initialDate = System.DateTime.Now.Date;
            penalty.finishDate = System.DateTime.Now.Date.AddDays(10);

            this.session.Save(penalty);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(penalty.Id > 0);

            Penalty created = this.session.Get<Penalty>(penalty.Id);

            Assert.AreEqual(penalty.Id, created.Id);

            Assert.AreEqual(penalty.initialDate, created.initialDate);

            Assert.AreEqual(penalty.finishDate, created.finishDate);
        }
        [TestMethod]
        public void TestPartnerPenalty()
        {
            Penalty penalty = new Penalty();

            penalty.loan = this.loan;
            penalty.initialDate = System.DateTime.Now.Date;
            penalty.finishDate = System.DateTime.Now.Date.AddDays(10);

            this.session.Save(penalty);
            this.session.Flush();
            this.session.Clear();

            Assert.AreEqual(penalty.loan.partner, loan.partner);

        }
    }
}
