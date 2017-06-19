using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSIGNDVC.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }
        // GET: Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario usuarioLogin)
        {
            ViewBag.Message = "Your application description page.";
            return View(usuarioLogin);
        }


        public ActionResult Prueba()
        {
            
            return View();
        }
        // GET: Login
       
    }
}