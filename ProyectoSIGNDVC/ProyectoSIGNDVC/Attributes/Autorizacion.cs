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

    public class AutorizarDirector : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var session = HttpContext.Current.Session["usuario"];
            if (session != null)
            {
                if (!Usuario.UsuarioIsDirectorEjecutivo(session.ToString()))
                {
                    filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Error", action = "Unauthorized" }));
                }
                
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Error" , action = "Unauthorized" }));
            }
        }
    }

    public class AutorizarCoordinadoraAdm : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var session = HttpContext.Current.Session["usuario"];
            if (session != null)
            {
                if (!Usuario.UsuarioIsCoordinadoraAdm(session.ToString()))
                {
                    filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Error", action = "Unauthorized" }));
                }

            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Error", action = "Unauthorized" }));
            }
        }
    }

    public class AutorizarRol : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var session = HttpContext.Current.Session["usuario"];
            if (session != null)
            {
                var i = !Usuario.UsuarioIsCoordinadoraAdm(session.ToString());
                var j = !Usuario.UsuarioIsDirectorEjecutivo(session.ToString());
                if (Usuario.UsuarioIsCoordinadoraAdm(session.ToString()) || Usuario.UsuarioIsDirectorEjecutivo(session.ToString()))
                {

                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Error", action = "Unauthorized" }));
                }

            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Error", action = "Unauthorized" }));
            }
        }
    }



    public class IniciarSesion : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["usuario"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Login" }));
            }
        }
    }



}