using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess;
using Biblioseca.Service;
using System;

namespace Biblioseca.Web
{
    public partial class Categorys : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CategoryDao categoryDao = new CategoryDao(Global.SessionFactory);
            CategoryService categoryService = new CategoryService(categoryDao);

            this.GridViewCategory.DataSource = categoryService.GetAll();
            this.GridViewCategory.DataBind();

        }
    }
}