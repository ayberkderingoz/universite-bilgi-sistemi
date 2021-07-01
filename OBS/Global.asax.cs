using OBS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OBS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SetIsLoggedFalse();
        }
        private ogrencisistemiEntities context;
        public void SetIsLoggedFalse()
        {
            context = new ogrencisistemiEntities();
            //login logged = context.login.FirstOrDefault(x => x.islogged == true);
            login logged = context.login.SingleOrDefault(x => x.login1 == true);
            if (logged != null)
            {
                logged.login1 = false;
            }
            context.SaveChanges();
        }
    }
}
