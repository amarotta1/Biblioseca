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

        public bool ISBNVerification(string ISBN)
        {
            return Regex.IsMatch(ISBN, @"^\d{13}$"); //Expresion regular 13 numeros cualquiera
        }

        
    }
}
