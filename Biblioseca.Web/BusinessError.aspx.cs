using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Biblioseca.Web
{
    public partial class BusinessError : System.Web.UI.Page
    {
        public string businessError;
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox.Text = Request.QueryString.Get("error");
                        
        }
    }
}