using Biblioseca.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using System;

namespace TestBiblioseca
{
    /// <summary>
    /// Descripción resumida de LoanTest
    /// </summary>
    [TestClass]
    public class LoanTest
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;
        Partner partner;
        Book book;

        [TestInitialize]
        public void SetUp()
        {
            sessionFactory = new Configuration().Configure().BuildSessionFactory();
            this.session = this.sessionFactory.OpenSession();
            this.transaction = this.session.BeginTransaction();

            this.partner = session.Get<Partner>(1);
            this.book = session.Get<Book>(1);

        }

        [TestCleanup]
        public void CleanUp()
        {
            this.transaction.Rollback();
            this.session.Close();
        }
              

        [TestMethod]
        public void TestLoan()
        {
            Loan loan = new Loan();

            loan.partner = this.partner;
            loan.book = this.book;
            loan.initialDate = DateTime.Now.Date;
            loan.finishDate = DateTime.Now.Date.AddDays(2);
            //loan.returnedDate = null;
            

            this.session.Save(loan);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(loan.Id > 0);

            Loan created = this.session.Get<Loan>(loan.Id);

            Assert.AreEqual(loan.Id, created.Id);

            Assert.AreEqual(loan.initialDate, created.initialDate);

            Assert.AreEqual(loan.finishDate, created.finishDate);
                        
        }
    }
}
