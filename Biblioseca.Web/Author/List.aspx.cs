using System;
using Biblioseca.DataAccess;
using Biblioseca.Service;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Biblioseca.Web.Author
{
    public partial class List : System.Web.UI.Page
    {
        private readonly AuthorDao authorDao = new AuthorDao(Global.SessionFactory);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                AuthorService authorService = new AuthorService(authorDao);

                this.GridViewAuthors.DataSource = authorService.GetAll();
                this.GridViewAuthors.DataBind();

            }

        }
        protected void GridViewAuthors_Editing(object sender, GridViewEditEventArgs e)
        {
            int authorId = Convert.ToInt32(this.GridViewAuthors.DataKeys[e.NewEditIndex]?.Values?[0]);
            Response.Redirect(string.Format(Pages.Author.Edit, authorId));
        }

        protected void GridViewAuthors_Deleting(object sender, GridViewDeleteEventArgs e)
        {
            int authorId = Convert.ToInt32(this.GridViewAuthors.DataKeys[e.RowIndex]?.Values?[0]);
            AuthorService authorService = new AuthorService(authorDao);
            try
            {
                authorService.Delete(authorId);
                Response.Redirect(Pages.Author.List);
            }
            catch (Exception ex)
            {
                Response.Redirect(string.Format(Pages.Error.BusinessError, ex.Message));
            }
            
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.Author.Create);
        }
    }
}