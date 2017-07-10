using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProyectoSIGNDVC.Attributes
{
    public class Autorizacion : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)       
        {
            var usuario = httpContext.Session["usuario"];

            if (usuario == null)
            { 
                return false;
            }
            return true;
        }



    }

    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //HttpContext ctx = HttpContext.Current;
            // check  sessions here
            if (HttpContext.Current.Session["usuario"] == null)
            {
                filterContext.Result = new RedirectResult("~/Login/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}