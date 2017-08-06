using Newtonsoft.Json;
using ProyectoSIGNDVC.Attributes;
using ProyectoSIGNDVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSIGNDVC.Controllers
{
    public class ConfigurationController : Controller
    {
        public ActionResult Test()
        {
            return View();
        }
        // GET: Configuration
        [HttpPost]
        public JsonResult Test(FormCollection fc)
        {
            DateTime dt=DateTime.Parse(fc.Get("fechanac"));
            
            return  Json(dt);
            
        }
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

        //[SessionExpire]
        public ActionResult RegistroUsuario()
        {
            ViewBag.Message = "Your application description page.";
            ViewModel vm = new ViewModel { direcciones=Direccion.GetAllEstadoDireccion(),cargos=Cargo.GetAllCargo() };
            return View(vm);
        }
        //[SessionExpire]
        [HttpPost]
        public ActionResult RegistroUsuario(FormCollection fc)
        {
            int fk_dir = Direccion.InsertDireccion(fc.Get("casa"), "Casa",Direccion.InsertDireccion(fc.Get("calle"), "Calle",Direccion.InsertDireccion(fc.Get("ciudad"), "Ciudad",Direccion.GetDireccionID(fc.Get("estado"), "Estado"))));
            List<Carga> cargas = new List<Carga>();
            for (int i = 2; i <=int.Parse(fc.Get("numfilas")) ; i++)
            {
                cargas.Add
                (
                    new Carga {
                        Persona = new Persona
                        {

                            nombre = fc.Get("nombrecarga" + i.ToString()),
                            apellido = fc.Get("apellidocarga" + i.ToString()),
                            sexo = fc.Get("sexocarga" + i.ToString()),
                            cedula = int.Parse(fc.Get("cedulacarga" + i.ToString())),
                            fecha_nacimiento = DateTime.Parse(fc.Get("fechanaccarga" + i.ToString()))
                        },
                        monto_poliza = int.Parse(fc.Get("montocarga" + i.ToString())),
                        //
                        //
                        //
                        //
                        //monto_poliza = int.Parse(fc.Get("nombrecarga" + i.ToString())),
                        //
                    }
                );
            }
            Usuario usu = new Usuario
            {
                usuario = fc.Get("usuario"),
                clave = fc.Get("clave"),
                email = fc.Get("email"),
                Empleado = new Empleado
                {
                    Cargas = cargas,
                    Persona = new Persona
                    {
                        nombre = fc.Get("nombre"),
                        apellido = fc.Get("apellido"),
                        fecha_nacimiento = DateTime.Parse(fc.Get("fechanac")),
                        cedula = int.Parse(fc.Get("cedula")),
                        sexo = fc.Get("sexo")
                    },
                    sueldo = int.Parse(fc.Get("sueldo")),
                    fecha_ingreso = DateTime.Now,
                    fecha_salida = DateTime.Now,
                    Direccion = new Direccion { nombre = fc.Get("direccion"), tipo = "Test", Fk_Direccion = fk_dir },
                    Fk_Cargo = Cargo.GetCargoID(fc.Get("cargo"))
                }
                   
             };
            
         //   int fk_cargo = Cargo.GetCargoID(fc.Get("cargo"));
            //usu.Empleado.Fk_Cargo = fk_cargo;
           // usu.Empleado.Direccion =  new Direccion { nombre="cualquier verga",tipo="Calle",Fk_Direccion=fk_dir};
            using (var ctx = new AppDbContext())
                {

                    //Agregar A
                    ///Agregar Direccion de tipo Estado
                    ///
                    
                    //int idEstado = ctx.Direcciones
                    //.Where(dir => dir.nombre == fc.Get("direccion"))
                    //.Select(dir => dir.DireccionID)
                    //.DefaultIfEmpty(0)
                    //.Single();
                    
                    ctx.Usuarios.Add(usu);
                    ctx.SaveChanges();
                }


            //String cl = emp.Usuario.clave;
            //String cl = fc.Get("clave");
            ViewModel vm = new ViewModel { direcciones = Direccion.GetAllEstadoDireccion(), cargos = Cargo.GetAllCargo() };
            return View(vm);
        }

        [HttpPost]
        public ActionResult AgregarUsuario()
        {
            return View();
        }

        public ActionResult TablaUsuarios()
        {
            ViewModel vm = new ViewModel { usuarios = Usuario.GetAllUsuarios() };
            return View(vm);
        }

        public ActionResult  Variables()
        {
            ViewModel vm = new ViewModel { configuracion = Configuracion.GetLastConfiguracion() };
            return View(vm);
        }

        [HttpPost]
        public ActionResult Variables(FormCollection fc)
        {
            Configuracion cf = new Configuracion
            {
                faov_retencion = int.Parse(fc.Get("faov_retencion")),
                faov_aporte = int.Parse(fc.Get("faov_aporte")),
                rpe_retencion = int.Parse(fc.Get("rpe_retencion")),
                rpe_aporte = int.Parse(fc.Get("rpe_aporte")),
                sso_retencion = int.Parse(fc.Get("sso_retencion")),
                sso_aporte = int.Parse(fc.Get("sso_aporte")),
                inces_retencion = int.Parse(fc.Get("inces_retencion")),
                inces_aporte = int.Parse(fc.Get("inces_aporte")),
                unid_tributaria = int.Parse(fc.Get("unid_tributaria")),
                fecha_inicio_config = DateTime.Now,

            };
            Configuracion.AddConfiguracion(cf);
            ViewModel vm = new ViewModel { configuracion = Configuracion.GetLastConfiguracion() };
            return View(vm);
        }

        public ActionResult EditarUsuario(String usuario)
        {
            ViewModel vm = new ViewModel {
                direcciones = Direccion.GetAllEstadoDireccion(),
                cargos = Cargo.GetAllCargo(),
                usuario = Usuario.GetUsuario(usuario)
            };
            var prueba = vm.usuario;
            return View(vm);
        }
        public ActionResult DeleteUsuario(String usuario)
        {
            Usuario.DeleteUsuario(usuario);
            return new HttpStatusCodeResult(200);
        }

        public JsonResult GetAllUsuarios()
        {
            return Json(Usuario.GetAllUsuarios(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsuario(string usuario)
        {
            return Json(Usuario.GetUsuario(usuario),JsonRequestBehavior.AllowGet);
        }
    }
}