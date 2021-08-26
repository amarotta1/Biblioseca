using System;
using Biblioseca.Model;
using Biblioseca.Service;
using Biblioseca.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Biblioseca.Web.Loan
{
    public partial class Create : System.Web.UI.Page
    {

        private readonly BookDao bookDao = new BookDao(Global.SessionFactory);
        private readonly PartnerDao partnerDao = new PartnerDao(Global.SessionFactory);
        private readonly LoanDao loanDao = new LoanDao(Global.SessionFactory);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindBooks();
                this.BindPartners();               
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

        private void BindBooks()
        {
            BookService bookService = new BookService(this.bookDao);
            this.bookList.DataValueField = "Id";
            this.bookList.DataTextField = "title";
            this.bookList.DataSource = bookService.GetAllAvailableBooks();
            this.bookList.DataBind();
        }

        protected void ButtonCreateLoan_Click(object sender, EventArgs e)
        {
            int bookId = Convert.ToInt32(this.bookList.SelectedValue);
            int partnerId = Convert.ToInt32(this.partnerList.SelectedValue);

            LoanService loanService = new LoanService(this.loanDao, this.bookDao, this.partnerDao);

            try
            {
                loanService.LoanABook(bookId,partnerId);
                
            }
            catch (Exception ex)
            {
                Response.Redirect(string.Format(Pages.Error.BusinessError, ex.Message));
            }

            Response.Redirect(Pages.Loans.List);
        }

    }
}