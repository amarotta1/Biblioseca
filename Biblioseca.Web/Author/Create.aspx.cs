using System;
using Biblioseca.DataAccess;
using Biblioseca.Service;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Biblioseca.Web.Author
{
    public partial class Create : System.Web.UI.Page
    {
        private readonly AuthorDao authorDao = new AuthorDao(Global.SessionFactory);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                
            }
        }

        protected void ButtonCreateAuthor_Click(object sender, EventArgs e)
        {
            AuthorService authorService = new AuthorService(authorDao);
            try
            {
                authorService.Create(textBoxFirstName.Text, textBoxLastName.Text);
                Response.Redirect(Pages.Author.List);
            }
            catch (Exception ex)
            {
                Response.Redirect(string.Format(Pages.Error.BusinessError, ex.Message));
            }
            
            
        }
    }
}