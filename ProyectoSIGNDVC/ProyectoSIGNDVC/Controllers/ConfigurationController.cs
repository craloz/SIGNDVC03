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
            ViewModel vm = new ViewModel { direcciones=new List<Direccion> (new Direccion[] { new Direccion { nombre = "Vargas", tipo = "Estado" } }) };
            return View(vm);
        }

        [HttpPost]
        public ActionResult RegistroUsuario(FormCollection fc)
        {

            Usuario usu = new Usuario {
                usuario = fc.Get("usuario"),
                clave = fc.Get("clave"),
                email = fc.Get("email"),
                Empleado = new Empleado
                {
                    Persona = new Persona
                    {
                        nombre = fc.Get("nombre"),
                        apellido = fc.Get("apellido"),
                        fecha_nacimiento = DateTime.Now,
                        cedula = int.Parse(fc.Get("cedula")),
                        sexo = fc.Get("sexo")[0]
                    },
                    sueldo = int.Parse(fc.Get("sueldo")),
                    fecha_ingreso = DateTime.Now,
                    fecha_salida = DateTime.Now,
                }
            };
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