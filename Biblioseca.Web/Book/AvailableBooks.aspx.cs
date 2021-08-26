using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess;
using Biblioseca.Service;


namespace Biblioseca.Web
{
    public partial class AvailableBooks : System.Web.UI.Page
    {
        private readonly BookDao bookDao = new BookDao(Global.SessionFactory);
        protected void Page_Load(object sender, EventArgs e)
        {
            BookService bookService = new BookService(bookDao);

            this.GridViewAvailableBooks.DataSource = bookService.GetAllAvailableBooks();
            this.GridViewAvailableBooks.DataBind();
        }
    }
}