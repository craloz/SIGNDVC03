using ProyectoSIGNDVC.Models;
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

        [HttpPost]
        public ActionResult RegistroUsuario(FormCollection fc)
        {
            Empleado emp = new Empleado {
                nombre =fc.Get("nombre"),apellido=fc.Get("apellido"),
                fecha =DateTime.Now,
                cedula= int.Parse(fc.Get("cedula")),
                //cargo =fc.Get("cargo"),
                sexo =fc.Get("sexo")[0],
                sueldo =int.Parse(fc.Get("sueldo")),
                f_ingreso =DateTime.Now,
                f_salida=DateTime.Now,
                
            };
            Usuario usu = new Usuario { usuario = fc.Get("usuario"), clave = fc.Get("clave"), email = fc.Get("email"), Empleado = emp };
            using (var ctx = new AppDbContext())
            {

                ctx.Usuarios.Add(usu);
                ctx.SaveChanges();
            }
            //String cl = emp.Usuario.clave;
            //String cl = fc.Get("clave");
            return View();
        }

        [HttpPost]
        public ActionResult AgregarUsuario()
        {
            return View();
        }

        public ActionResult TablaUsuarios()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
    }
}