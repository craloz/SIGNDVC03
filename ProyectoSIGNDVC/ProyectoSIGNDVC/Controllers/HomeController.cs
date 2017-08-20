using ProyectoSIGNDVC.Attributes;
using ProyectoSIGNDVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSIGNDVC.Controllers
{
    public class HomeController : Controller
    {
        [SessionExpire]
        public ActionResult Index()
        {
            ViewModel vm = new ViewModel {
                notificaciones = Notificacion.GetAllNotificaciones(Usuario.GetUsuario(Session["usuario"].ToString()).usuarioID)
            };
            return View(vm);
        }

        [SessionExpire]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [SessionExpire]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [SessionExpire]
        public ActionResult Error()
        {
            return View();
        }

    }
}