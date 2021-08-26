using System;
using Biblioseca.DataAccess;
using Biblioseca.Service;
using Biblioseca.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Biblioseca.Web.Author
{
    public partial class Edit : System.Web.UI.Page
    {
        private readonly AuthorDao authorDao = new AuthorDao(Global.SessionFactory);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                int authorId = Convert.ToInt32(Request.QueryString.Get("id"));
                AuthorService authorService = new AuthorService(this.authorDao);
                Model.Author autor;
                try
                {
                    autor = authorService.GetOnebyId(authorId);
                    this.textBoxFirstName.Text = autor.FirstName;
                    this.textBoxLastName.Text = autor.LastName;
                }
                catch (Exception ex)
                {
                    Response.Redirect(string.Format(Pages.Error.BusinessError, ex.Message));
                }               

            }
            
        }

        protected void ButtonEditAuthor_Click(object sender, EventArgs e)
        {
            
            AuthorService authorService = new AuthorService(this.authorDao);
            int authorId = Convert.ToInt32(Request.QueryString.Get("id"));

            try
            {
                authorService.Update(authorId, this.textBoxFirstName.Text, this.textBoxLastName.Text);
                Response.Redirect(Pages.Author.List);
            }
            catch (Exception ex)
            {

                Response.Redirect(string.Format(Pages.Error.BusinessError, ex.Message));
            }
         
            
        }
    }
}