using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace Biblioseca.Web
{
    public class Global : HttpApplication
    {
        public static readonly ISessionFactory SessionFactory = new Configuration()
                .Configure()
                .BuildSessionFactory();

        private void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            ISession session = SessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
            session.BeginTransaction();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            if (SessionFactory.GetCurrentSession().IsOpen && SessionFactory.GetCurrentSession().GetCurrentTransaction().IsActive)
            {
                SessionFactory.GetCurrentSession().GetCurrentTransaction().Commit();
                SessionFactory.GetCurrentSession().Close();
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            SessionFactory.GetCurrentSession().GetCurrentTransaction().Rollback();
            SessionFactory.GetCurrentSession().Close();
        }
    }
}