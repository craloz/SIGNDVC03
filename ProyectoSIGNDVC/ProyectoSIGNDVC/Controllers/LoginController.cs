using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoSIGNDVC.Models;

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
        public ActionResult Login(FormCollection fc)
        {

            if (Usuario.CheckCredencialesUsuarios(fc.Get("usuario"), fc.Get("clave")))
            {
                //ViewBag.Message = "Correcto Redireccionando";
                Session["usuario"] = fc.Get("usuario");
                return RedirectToAction("Index", "Home");
                //Session["UserName"] = obj.UserName.ToString();
               // return RedirectToAction("UserDashBoard");
            }
            else
            {
                ViewBag.Message = "Usuario o Clave Incorrecta";
            }
                return View();
        }


        public ActionResult Prueba()
        {
            
            return View();
        }
        // GET: Login
        public ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("Login", "Login");
        } 
       
    }
}