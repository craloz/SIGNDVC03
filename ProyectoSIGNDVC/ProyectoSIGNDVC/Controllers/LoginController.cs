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
        public ActionResult Login(Usuario usuarioLogin)
        {
            
            ViewBag.Message = usuarioLogin.usuario;
            if (ModelState.IsValid)
            {
                using (var ctx = new AppDbContext())
                {
                    System.Console.WriteLine("aquiiiiii");
                    ctx.Usuarios.Add(usuarioLogin);
                    ctx.SaveChanges();
                }
                ModelState.Clear();
            }
                return View();
        }


        public ActionResult Prueba()
        {
            
            return View();
        }
        // GET: Login
       
    }
}