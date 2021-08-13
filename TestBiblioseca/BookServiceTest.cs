﻿using Biblioseca.DataAccess;
using Biblioseca.Model;
using Biblioseca.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;
using System;
using System.Collections.Generic;


namespace TestBiblioseca
{
    [TestClass]
    public class BookServiceTest
    {
        private BookService bookService;
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
        public void IsAvailable()
        {
            const int bookId = 1;
            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());

            this.bookService = new BookService(this.bookDao.Object);

            bool available = bookService.IsAvailable(bookId);

            Assert.IsTrue(available);

        }
        [TestMethod]
        public void IsNotAvailable()
        {
            const int bookId = 1;
            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBookNoStock());

            this.bookService = new BookService(this.bookDao.Object);

            bool available = bookService.IsAvailable(bookId);

            Assert.IsFalse(available);

        }

        [TestMethod]
        public void GetAllAvailableBooks()
        {
            this.bookDao.Setup(dao => dao.GetAllAvailableBooks()).Returns(GetBooks());

            this.bookService = new BookService(this.bookDao.Object);

            IEnumerable<Book> books = bookService.GetAllAvailableBooks();

            Assert.IsNotNull(books);

        }
        [TestMethod]
        public void Get()
        {
            const int bookId = 1;
            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());

            this.bookService = new BookService(this.bookDao.Object);

            Book book = bookService.GetBook(bookId);
            Assert.IsNotNull(book);
            Assert.IsInstanceOfType(book,typeof(Book));

        }
        [TestMethod]
        public void GetDoesNotExist()
        {
            const int bookId = 1;
            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(default(Book));

            this.bookService = new BookService(this.bookDao.Object);

            ;
            Assert.ThrowsException<Exception>(() => this.bookService.GetBook(bookId),
                "El objeto no existe");

        }

        [TestMethod]
        public void ISBNVerification()
        {
            BookDao bookDao = new BookDao(this.sessionFactory.Object);
            BookService bookService = new BookService(bookDao);

            bool verification = bookService.ISBNVerification("8574930275834");
            Assert.IsTrue(verification);

        }

        [TestMethod]
        public void ISBNNotVerification()
        {
            BookDao bookDao = new BookDao(this.sessionFactory.Object);
            BookService bookService = new BookService(bookDao);

            bool verification = bookService.ISBNVerification("8930275834");
            Assert.IsFalse(verification);

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

        private static Book GetBookNoStock()
        {
            Book book = new Book
            {
                title = "A title",
                description = "A description",
                stock = 0
            };

            return book;
        }

        private static IEnumerable<Book> GetBooks()
        {
            List<Book> books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    stock = 10
                },
                new Book
                {
                    Id = 2,
                    stock = 10
                },
                new Book
                {
                    Id = 3,
                    stock = 10
                }
            };

            return books;
        }
    }   
}
