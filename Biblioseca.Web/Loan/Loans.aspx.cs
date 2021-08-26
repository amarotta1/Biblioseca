using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess;
using Biblioseca.Service;
using System.Linq;


namespace Biblioseca.Web
{
    public partial class Loans : System.Web.UI.Page
    {
        private readonly LoanDao loanDao = new LoanDao(Global.SessionFactory);
        private readonly BookDao bookDao = new BookDao(Global.SessionFactory);
        private readonly PartnerDao pDao = new PartnerDao(Global.SessionFactory);
        protected void Page_Load(object sender, EventArgs e)
        {
            LoanService loanService = new LoanService(loanDao,bookDao,pDao);
            this.GridViewLoans.DataSource = loanService.GetAllLoan().OrderByDescending(loan =>loan.partner.FirstName);
            this.GridViewLoans.DataBind();
            this.Create.NavigateUrl = Pages.Loans.Create;
            this.Create.DataBind();
        }
             
    }
}