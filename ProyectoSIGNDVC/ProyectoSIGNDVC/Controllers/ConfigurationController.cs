using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSIGNDVC.Controllers
{
    public class ConfigurationController : Controller
    {
        // GET: Configuration
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegistroUsuario()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult TablaUsuarios()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
    }
}