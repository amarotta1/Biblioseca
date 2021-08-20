using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Biblioseca.DataAccess;
using Biblioseca.Model;


namespace Biblioseca.Service
{
    public class BookService
    {
        private readonly BookDao bookDao;
        public BookService(BookDao bookDao)
        {
            this.bookDao = bookDao;
        }

        public bool IsAvailable(int bookId)
        {
            CheckService.BusinessLogic(bookId <= 0, "Book.Id debe ser mayor que 0.");

            Book book = this.bookDao.Get(bookId);
            CheckService.Exists(book);
            //Retorna un bool true si es mayor a cero
            return book.stock > 0;
        }

        public IEnumerable<Book> GetAllAvailableBooks()
        {
            return bookDao.GetAllAvailableBooks(); //Donde el stock sea mayor  a cero y no deleted

        }

        public Book GetBook(int bookId)
        {
            CheckService.BusinessLogic(bookId <= 0, "Book.Id debe ser mayor que 0.");

            Book book = this.bookDao.Get(bookId);
            CheckService.Exists(book);

            return book;
        }

        public Book Create(string title, string description, string isbn, Author author, Category cat, int stock = 0)
        {
            CheckService.BusinessLogic(!ISBNVerification(isbn), "El ISBN no es valido");

            Book book = new Book();
            book.title = title;
            book.description = description;
            book.isbn = isbn;
            book.author = author;
            book.category = cat;
            book.stock = stock;
            bookDao.Save(book);
            return book;
        }

        public bool ISBNVerification(string ISBN)
        {
            return Regex.IsMatch(ISBN, @"^\d{13}$"); //Expresion regular 13 numeros cualquiera
        }

        public bool Delete(int bookId)
        {
            CheckService.BusinessLogic(bookId <= 0, "El id del libro debe ser mayor a cero");
            Book book = bookDao.Get(bookId);
            CheckService.Exists(book);
            book.MarkAsDeleted();
            bookDao.Save(book);
            return true;

        }

        public void Update(int bookId, string title, string description, string isbn, Author author, Category cat, int stock = 0)
        {
            CheckService.BusinessLogic(bookId <= 0, "El id del libro debe ser mayor a cero");
            Book book = bookDao.Get(bookId);
            CheckService.Exists(book);

            CheckService.BusinessLogic(!ISBNVerification(isbn), "El ISBN no es valido");

            book.title = title;
            book.description = description;
            book.isbn = isbn;
            book.author = author;
            book.category = cat;
            book.stock = stock;
            bookDao.Save(book);
            
        }

    }
}
