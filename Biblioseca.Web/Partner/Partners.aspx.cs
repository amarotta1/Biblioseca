using System;
using Biblioseca.DataAccess;
using Biblioseca.Service;


namespace Biblioseca.Web.Partner
{
    public partial class Partners : System.Web.UI.Page
    {
        private readonly PartnerDao pDao = new PartnerDao(Global.SessionFactory);
        protected void Page_Load(object sender, EventArgs e)
        {
            PartnerService pService = new PartnerService(pDao);

            this.GridViewPartners.DataSource = pService.GetAll();
            this.GridViewPartners.DataBind();

        }
    }
}