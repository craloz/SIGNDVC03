using Newtonsoft.Json;
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
            ViewModel vm = new ViewModel { direcciones = Direccion.GetAllEstadoDireccion(), cargos = Cargo.GetAllCargo() };
            return View(vm);
        }
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            int  fk_dir = Direccion.InsertDireccion(fc.Get("casa"), "Casa",
                        Direccion.InsertDireccion(fc.Get("calle"), "Calle",
                        Direccion.InsertDireccion(fc.Get("ciudad"), "Ciudad", Direccion.GetDireccionID(fc.Get("estado"), "Estado"))));
            return View();
        }
        public ActionResult RegistroUsuario()
        {
            ViewBag.Message = "Your application description page.";
            ViewModel vm = new ViewModel { direcciones=Direccion.GetAllEstadoDireccion(),cargos=Cargo.GetAllCargo() };
            return View(vm);
        }

        [HttpPost]
        public ActionResult RegistroUsuario(FormCollection fc)
        {
            
                Usuario usu = new Usuario
                {
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

                    //Agregar A
                    ///Agregar Direccion de tipo Estado
                    ///
                    int fk_dir=Direccion.InsertDireccion(fc.Get("casa"),"Casa",
                        Direccion.InsertDireccion(fc.Get("calle"),"Calle",
                        Direccion.InsertDireccion(fc.Get("ciudad"),"Ciudad",Direccion.GetDireccionID(fc.Get("estado"),"Estado"))));
                    int fk_cargo = Cargo.GetCargoID(fc.Get("cargo"));
                    //int idEstado = ctx.Direcciones
                    //.Where(dir => dir.nombre == fc.Get("direccion"))
                    //.Select(dir => dir.DireccionID)
                    //.DefaultIfEmpty(0)
                    //.Single();
                    usu.Empleado.Fk_Cargo=fk_cargo;
                    usu.Empleado.Fk_Direccion = fk_dir;
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