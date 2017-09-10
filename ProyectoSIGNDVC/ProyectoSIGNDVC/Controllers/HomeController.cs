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
            try
            {
                ViewModel vm = new ViewModel
                {

                    notificaciones = Notificacion.GetAllNotificaciones(Usuario.GetUsuario(Session["usuario"].ToString()).usuarioID)
                };
                return View(vm);
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
            
        }

        [SessionExpire]
        public ActionResult About()
        {

            try
            {
                ViewBag.Message = "Your application description page.";

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
           
        }

        [SessionExpire]
        public ActionResult Contact()
        {
            try
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
            
        }

        [SessionExpire]
        public ActionResult Error()
        {

            try
            {
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
            
        }

    }
}