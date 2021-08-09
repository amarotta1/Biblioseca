using System;
using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess;
using Biblioseca.Model;



namespace Biblioseca.Service
{
    public class LoanService
    {
        private readonly LoanDao loanDao;
        private readonly BookDao bookDao;
        private readonly PartnerDao partnerDao;
        
        public LoanService(LoanDao borrowDao, BookDao bookDao, PartnerDao partnerDao)
        {
            this.loanDao = borrowDao;
            this.bookDao = bookDao;
            this.partnerDao = partnerDao;
        }

        public Loan LoanABook(int bookId, int partnerId)
        {
            Book book = bookDao.Get(bookId);
            CheckService.Exists(book);

            Partner partner = partnerDao.Get(partnerId);
            CheckService.Exists(partner);
            
            CheckService.BusinessLogic( loanDao.GetActualLoansByPartnerID(partner.Id).Count()> 2, "Limite de libros alcanzado ");

            CheckService.BusinessLogic(book.stock <= 0, "El libro no tiene stock"); //podria usar el BookService
         
            /*
            IEnumerable<Loan> loans = loanDao.GetActualLoansByBookId(bookId);
            CheckService.BusinessLogic(loans.Any(), "El libro ya fue prestado. ");*/

            Loan loan = new Loan
            {
                book = book,
                partner = partner,
                initialDate = DateTime.Now.Date,
                finishDate = DateTime.Now.Date.AddDays(2),
            };

            book.DecreaseStock();

            loanDao.Save(loan);
            bookDao.Save(book);

            return loan;
        }

        public bool Returns(int bookID, int partnerID)
        {
            CheckService.BusinessLogic(bookID <= 0, "El ID de Libro debe ser mayor a cero ");
            CheckService.BusinessLogic(partnerID <= 0, "El ID del socio debe ser mayor a cero ");

            Book book = bookDao.Get(bookID);
            CheckService.Exists(book);

            Partner partner = partnerDao.Get(partnerID);
            CheckService.Exists(partner);

            Loan loan = loanDao.GetUniqueLoan(bookID,partnerID);
            CheckService.Exists(loan);

            loan.Returned();

            book.IncreaseStock();

            loanDao.Save(loan);

            return true;

        }
    }
}
