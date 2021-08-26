using System;
using Biblioseca.Service;
using Biblioseca.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Biblioseca.Web.Loan
{
    public partial class Returned : System.Web.UI.Page
    {
        private readonly BookDao bookDao = new BookDao(Global.SessionFactory);
        private readonly PartnerDao partnerDao = new PartnerDao(Global.SessionFactory);
        private readonly LoanDao loanDao = new LoanDao(Global.SessionFactory);
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindPartners();
                this.BindBooks();
            }
            else
            {
                this.BindBooks();
            }                      
           
        }

        private void BindPartners()
        {
            PartnerService partnerService = new PartnerService(this.partnerDao);
            this.partnerList.DataValueField = "Id";
            this.partnerList.DataTextField = "UserName";
            this.partnerList.DataSource = partnerService.GetAll();
            this.partnerList.DataBind();
        }

        protected void BindBooks()
        {
            int partnerId = Convert.ToInt32(this.partnerList.SelectedValue);
            LoanService loanService = new LoanService(this.loanDao, this.bookDao, this.partnerDao);
            IEnumerable<Model.Loan> actualLoans = loanService.GetActualLoansByPartnerID(partnerId);
            List<Model.Book> books = new List<Model.Book>();
            
            if (actualLoans.Any())
            {
                foreach (Model.Loan loan in actualLoans)
                {
                    books.Add(loan.book);                   
                    
                }
                this.bookList.DataValueField = "Id";
                this.bookList.DataTextField = "title";
                this.bookList.DataSource = books;
                this.bookList.DataBind();
            }
            
        }

        protected void OnChangeSelected(object sender, EventArgs e)
        {
           // Response.Redirect(Pages.Loans.Returned);
        }

        protected void ButtonReturnLoan_Click(object sender, EventArgs e)
        {
            int bookId = Convert.ToInt32(this.bookList.SelectedValue);
            int partnerId = Convert.ToInt32(this.partnerList.SelectedValue);

            LoanService loanService = new LoanService(this.loanDao, this.bookDao, this.partnerDao);

            try
            {
                loanService.Returns(bookId, partnerId);

            }
            catch (Exception ex)
            {
                Response.Redirect(string.Format(Pages.Error.BusinessError, ex.Message));
            }

            Response.Redirect(Pages.Loans.List);
        }
    }
}