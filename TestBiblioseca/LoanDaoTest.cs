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
        private LoanDao loanDao;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Configuration().Configure().BuildSessionFactory();
            this.session = this.sessionFactory.OpenSession();
            this.transaction = this.session.BeginTransaction();
            CurrentSessionContext.Bind(this.session);

            this.loanDao = new LoanDao(this.sessionFactory);
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
            IEnumerable<Loan> authors = loanDao.GetAll();

            Assert.IsTrue(authors.Any());
        }
        [TestMethod]
        public void GetOne()
        {

            Loan loan = loanDao.Get(12);
            Assert.IsNotNull(loan);

        }
        [TestMethod]
        public void GetByPartnerId()
        {
            PartnerDao partnerDao = new PartnerDao(this.sessionFactory);
            
            Loan loan = loanDao.GetAllLoansByPartnerID(partnerDao.Get(1).Id).First();

            Assert.AreEqual(loan.partner.Id, 1);

        }
        [TestMethod]
        public void GetByPartnerLastName()
        {
            IEnumerable<Loan> prestamos = loanDao.GetByPartnerLastName("Perez");
            foreach (Loan loan in prestamos)
            {
                Assert.AreEqual("Perez", loan.partner.LastName);
            }
        }
        [TestMethod]
        public void GetActualLoansByPartnerId()
        {
            PartnerDao partnerDao = new PartnerDao(this.sessionFactory);

            IEnumerable<Loan> actualLoans = loanDao.GetActualLoansByPartnerID(partnerDao.Get(1).Id);

            foreach (Loan loan in actualLoans)
            {
                Assert.IsNull(loan.returnedDate);
            }

        }
        [TestMethod]
        public void GetActualLoansByBookId()
        {
            BookDao bookDao = new BookDao(this.sessionFactory);

            IEnumerable<Loan> actualLoans = loanDao.GetActualLoansByBookId(bookDao.Get(1).Id);

            foreach (Loan loan in actualLoans)
            {
                Assert.AreEqual(loan.book.Id,1);
            }

        }

        [TestMethod]
        public void GetUniqueByBookAndPartner()
        {
     
            Loan borrow = loanDao.GetUniqueLoanByBookAndPartner(1, 1);

            Assert.IsNotNull(borrow);
        }



    }
}
