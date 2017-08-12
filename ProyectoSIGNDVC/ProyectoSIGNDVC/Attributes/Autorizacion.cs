using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProyectoSIGNDVC.Attributes
{
    public class AutorizacionDirectorAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)       
        {
            
            var usuario = httpContext.Session["usuario"];
            
            if (usuario == null)
            {
                return false;
            }
            else
            {
                var temp = Usuario.GetUsuario(usuario.ToString());
                if (temp != null && temp.Empleado.Cargo.nombre == "Director Ejecutivo")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }
    }

    public class AutorizacionAdministradorAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            var usuario = httpContext.Session["usuario"];

            if (usuario == null)
            {
                return false;
            }
            else
            {
                var temp = Usuario.GetUsuario(usuario.ToString());
                if (temp != null && temp.Empleado.Cargo.nombre == "Asistente de Administración")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }
    }

    public class Auth: AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            var usuario = filterContext.HttpContext.Session["usuario"];
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "xxx", action = "xxx"}));
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