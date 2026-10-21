using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LinqToDB;
using LinqToDB.Data;
using System.Web.Security;
using System.Security.Principal;



namespace Progra6
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticationRequest()
        {
            HttpCookie httpcookie = Request.Cookies[FormsAuthentication.FormsCookieName]; 
            
            if (httpcookie == null)
            {
                FormsAuthenticationTicket authenticationTicket;
                try
                {
                    authenticationTicket = FormsAuthentication.Decrypt(httpcookie.Value);
                }
                catch
                {
                    return;
                }

                if (authenticationTicket == null)
                    return;

                    string[] roles = new string[]
                    {
                    authenticationTicket.UserData
                    };

                    HttpContext.Current.User = new GenericPrincipal(new
                        FormsIdentity(authenticationTicket), roles);
                


            }

        }


    }
}
