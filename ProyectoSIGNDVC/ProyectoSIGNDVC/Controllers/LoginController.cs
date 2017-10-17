using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoSIGNDVC.Models;
using ProyectoSIGNDVC.Attributes;

namespace ProyectoSIGNDVC.Controllers
{
    public class LoginController : Controller
    {
        
        public ActionResult Login()
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
        // GET: Login
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {

            try
            {
                if (Usuario.CheckCredencialesUsuarios(fc.Get("usuario"), fc.Get("clave")))
                {
                    Session["usuario"] = fc.Get("usuario");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Usuario o Clave Incorrecta";
                }
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }

           
        }

        // GET: Login
        [IniciarSesion]
        public ActionResult LogOff()
        {

            try
            {
                Session.Clear();
                return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
            
        } 
       
    }
}