using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess;
using Biblioseca.Service;


namespace Biblioseca.Web
{
    public partial class Categorys : System.Web.UI.Page
    {
        private readonly CategoryDao categoryDao = new CategoryDao(Global.SessionFactory);
        protected void Page_Load(object sender, EventArgs e)
        {            
            CategoryService categoryService = new CategoryService(categoryDao);

            this.GridViewCategory.DataSource = categoryService.GetAll();
            this.GridViewCategory.DataBind();

        }
    }
}