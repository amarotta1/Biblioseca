using Biblioseca.DataAccess;
using Biblioseca.Model;
using Biblioseca.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestBiblioseca
{
    [TestClass]
    public class LoanServiceTest
    {
        private LoanService loanService;
        private Mock<LoanDao> loanDao;
        private Mock<BookDao> bookDao;
        private Mock<PartnerDao> partnerDao;
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Mock<ISessionFactory>();
            this.session = new Mock<ISession>();
            this.loanDao = new Mock<LoanDao>(this.sessionFactory.Object);
            this.bookDao = new Mock<BookDao>(this.sessionFactory.Object);
            this.partnerDao = new Mock<PartnerDao>(this.sessionFactory.Object);
        }

        [TestMethod]
        public void GetAll()
        {            
            
            this.loanDao.Setup(dao => dao.GetAll()).Returns(GetLoans());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.loanDao.Setup(dao => dao.Session).Returns(this.session.Object);

            this.loanService = new LoanService(this.loanDao.Object, this.bookDao.Object, this.partnerDao.Object);

            IEnumerable<Loan> loans = this.loanService.GetAllLoan();

            Assert.IsTrue(loans.Any());
        }

        [TestMethod]
        public void GetUniqueLoan()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            this.partnerDao.Setup(dao => dao.Get(partnerId)).Returns(GetPartner());
            this.loanDao.Setup(dao => dao.GetUniqueLoanByBookAndPartner(bookId, partnerId)).Returns(GetUniqueLoanResult());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.loanDao.Setup(dao => dao.Session).Returns(this.session.Object);

            this.loanService = new LoanService(this.loanDao.Object, this.bookDao.Object, this.partnerDao.Object);

            Loan loan = this.loanService.GetUniqueLoan(bookId, partnerId);

            Assert.IsNotNull(loan);
            Assert.AreEqual(loan.partner.UserName,"JPerez" );
            Assert.AreEqual(loan.book.title,"A title" );
        }

        [TestMethod]
        public void LoanABook()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            this.partnerDao.Setup(dao => dao.Get(partnerId)).Returns(GetPartner());
            this.loanDao.Setup(dao => dao.GetUniqueLoanByBookAndPartner(bookId, partnerId)).Returns(new Loan());
            this.loanDao.Setup(dao => dao.GetActualLoansByPartnerID(partnerId)).Returns(GetOneListLoan());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.loanDao.Setup(dao => dao.Session).Returns(this.session.Object);

            this.loanService = new LoanService(this.loanDao.Object, this.bookDao.Object, this.partnerDao.Object);

            Loan loan = this.loanService.LoanABook(bookId, partnerId);

            Assert.IsNotNull(loan);
        }

        [TestMethod]
        public void LoanABookWhenBooksDontHaveStock()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBookWithOutStock());
            this.partnerDao.Setup(dao => dao.Get(partnerId)).Returns(GetPartner());
            this.loanDao.Setup(dao => dao.GetUniqueLoanByBookAndPartner(bookId, partnerId)).Returns(new Loan());
            this.loanDao.Setup(dao => dao.GetActualLoansByPartnerID(partnerId)).Returns(GetOneListLoan());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.loanDao.Setup(dao => dao.Session).Returns(this.session.Object);

            this.loanService = new LoanService(this.loanDao.Object, this.bookDao.Object, this.partnerDao.Object);

            Assert.ThrowsException<Exception>(() => this.loanService.LoanABook(bookId, partnerId),
                "El libro no tiene stock");
        }

        [TestMethod]
        public void LoanABookWhenPartnerHasMoreThan2()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            this.partnerDao.Setup(dao => dao.Get(partnerId)).Returns(GetPartner());
            this.loanDao.Setup(dao => dao.GetUniqueLoanByBookAndPartner(bookId, partnerId)).Returns(new Loan());
            this.loanDao.Setup(dao => dao.GetActualLoansByPartnerID(partnerId)).Returns(GetLoans());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.loanDao.Setup(dao => dao.Session).Returns(this.session.Object);

            this.loanService = new LoanService(this.loanDao.Object, this.bookDao.Object, this.partnerDao.Object);

            Assert.ThrowsException<Exception>(() => this.loanService.LoanABook(bookId, partnerId),
                "Limite de libros alcanzado");
        }
        [TestMethod]
        public void LoanABookPartnerDoesnotExist()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            this.partnerDao.Setup(dao => dao.Get(partnerId)).Returns(default(Partner));
            this.loanDao.Setup(dao => dao.GetUniqueLoanByBookAndPartner(bookId, partnerId)).Returns(new Loan());
            this.loanDao.Setup(dao => dao.GetActualLoansByPartnerID(partnerId)).Returns(GetOneListLoan());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.loanDao.Setup(dao => dao.Session).Returns(this.session.Object);

            this.loanService = new LoanService(this.loanDao.Object, this.bookDao.Object, this.partnerDao.Object);

            Assert.ThrowsException<Exception>(() => this.loanService.LoanABook(bookId, partnerId),
                $"El objeto no existe");

        }
        [TestMethod]
        public void LoanABookBookDoesnotExist()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(default(Book));
            this.partnerDao.Setup(dao => dao.Get(partnerId)).Returns(GetPartner());
            this.loanDao.Setup(dao => dao.GetUniqueLoanByBookAndPartner(bookId, partnerId)).Returns(new Loan());
            this.loanDao.Setup(dao => dao.GetActualLoansByPartnerID(partnerId)).Returns(GetOneListLoan());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.loanDao.Setup(dao => dao.Session).Returns(this.session.Object);

            this.loanService = new LoanService(this.loanDao.Object, this.bookDao.Object, this.partnerDao.Object);

            Assert.ThrowsException<Exception>(() => this.loanService.LoanABook(bookId, partnerId),
                "El objeto no existe");

        }

        [TestMethod]
        public void ReturnsABook()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            this.partnerDao.Setup(dao => dao.Get(partnerId)).Returns(GetPartner());
            this.loanDao.Setup(dao => dao.GetUniqueLoanByBookAndPartner(bookId, partnerId)).Returns(new Loan());
            this.loanDao.Setup(dao => dao.GetActualLoansByPartnerID(partnerId)).Returns(GetOneListLoan());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.loanDao.Setup(dao => dao.Session).Returns(this.session.Object);

            this.loanService = new LoanService(this.loanDao.Object, this.bookDao.Object, this.partnerDao.Object);

            bool returned = this.loanService.Returns(bookId, partnerId);

            Assert.IsTrue(returned);
        }
        [TestMethod]
        public void Deleted()
        {

            ISessionFactory sessionFactory = new Configuration().Configure().BuildSessionFactory();
            ISession session = sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
            BookDao bookDao = new BookDao(sessionFactory);
            LoanDao loanDao = new LoanDao(sessionFactory);
            PartnerDao partnerDao = new PartnerDao(sessionFactory);
            this.loanService = new LoanService(loanDao, bookDao, partnerDao);
            Loan loan = loanDao.Get(12);
            loanService.Delete(12);

            Assert.IsTrue(loan.Deleted);

            loan.Deleted = false; //para volverlo a poner bien
            loanDao.Save(loan);

        }

        [TestMethod]
        public void GetActualLoansbyBookID()
        {

            ISessionFactory sessionFactory = new Configuration().Configure().BuildSessionFactory();
            ISession session = sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
            BookDao bookDao = new BookDao(sessionFactory);
            LoanDao loanDao = new LoanDao(sessionFactory);
            PartnerDao partnerDao = new PartnerDao(sessionFactory);
            this.loanService = new LoanService(loanDao, bookDao, partnerDao);

            IEnumerable<Loan> loans = loanService.GetActualLoansByBookId(1);

            foreach (Loan loan in loans)
            {
                Assert.AreEqual(loan.book.Id,1);
            }

        }
        [TestMethod]
        public void GetActualLoansbyPartnerID()
        {

            ISessionFactory sessionFactory = new Configuration().Configure().BuildSessionFactory();
            ISession session = sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
            BookDao bookDao = new BookDao(sessionFactory);
            LoanDao loanDao = new LoanDao(sessionFactory);
            PartnerDao partnerDao = new PartnerDao(sessionFactory);
            this.loanService = new LoanService(loanDao, bookDao, partnerDao);

            IEnumerable<Loan> loans = loanService.GetActualLoansByPartnerID(1);

            foreach (Loan loan in loans)
            {
                Assert.AreEqual(loan.partner.Id, 1);
            }

        }
        [TestMethod]
        public void GetllLoansbyPartnerID()
        {

            ISessionFactory sessionFactory = new Configuration().Configure().BuildSessionFactory();
            ISession session = sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
            BookDao bookDao = new BookDao(sessionFactory);
            LoanDao loanDao = new LoanDao(sessionFactory);
            PartnerDao partnerDao = new PartnerDao(sessionFactory);
            this.loanService = new LoanService(loanDao, bookDao, partnerDao);

            IEnumerable<Loan> loans = loanService.GetAllLoansByPartnerID(1);

            foreach (Loan loan in loans)
            {
                Assert.AreEqual(loan.partner.Id, 1);
            }

        }
        private static Loan GetLoan()
        {
            return new Loan
            {
                Id = 1,
                initialDate = DateTime.Now.AddDays(-2),
                finishDate = DateTime.Now,
                returnedDate = DateTime.Now,
                book = new Book
                {
                    Id = 1,
                    stock = 1
                }
            };
        }

        private static Loan GetUniqueLoanResult()
        {
            Loan loan =  new Loan
            {
                Id = 1,
                initialDate = DateTime.Now.AddDays(-2),
                finishDate = DateTime.Now,
                returnedDate = DateTime.Now,
                book = new Book{
                title = "A title",
                description = "A description",
                stock = 10
                },
                partner = new Partner{
                FirstName = "Juan",
                LastName = "Perez",
                UserName = "JPerez"
                }

            };

            return loan;
        }


        private static IEnumerable<Loan> GetLoans()
        {
            List<Loan> loans = new List<Loan>
            {
                new Loan
                {
                    Id = 1
                },
                new Loan
                {
                    Id = 2
                },
                new Loan
                {
                    Id = 3
                }
            };

            return loans;
        }

        private static IEnumerable<Loan> GetOneListLoan()
        {
            List<Loan> loans = new List<Loan>
            {
                new Loan
                {
                    Id = 1,
                    returnedDate = DateTime.Now.AddDays(-1)
                },
            };

            return loans;
        }

        private static Partner GetPartner()
        {
            Partner partner = new Partner()
            {
                FirstName = "Juan",
                LastName = "Perez",
                UserName = "JPerez"
            };

            return partner;
        }

        private static Book GetBook()
        {
            Book book = new Book
            {
                title = "A title",
                description = "A description",               
                stock = 10
            };

            return book;
        }

        private static Book GetBookWithOutStock()
        {
            Book book = new Book
            {
                title = "A title",
                description = "A description",
                stock = 0
            };

            return book;
        }


    }

    
}
