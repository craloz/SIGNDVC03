using Newtonsoft.Json;
using ProyectoSIGNDVC.Attributes;
using ProyectoSIGNDVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSIGNDVC.Controllers
{
    public class ConfigurationController : Controller
    {
        
        [SessionExpire]
        [AutorizarRol]
        public ActionResult RegistroUsuario()
        {
            ViewModel vm = new ViewModel { direcciones=Direccion.GetAllEstadoDireccion(),cargos=Cargo.GetAllCargo() };
            return View(vm);
        }
        [HttpPost]
        [SessionExpire]
        [AutorizarRol]
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
                    Codigo = "E-"+fc.Get("codempleado"),
                    Banco = fc.Get("banco"),
                    N_Cuenta = fc.Get("cuenta"),
                    fecha_ingreso = DateTime.Now,
                    fecha_salida = DateTime.Now,
                    Direccion = new Direccion { nombre = fc.Get("direccion"), tipo = "Direccion", Fk_Direccion = fk_dir },
                    Fk_Cargo = Cargo.GetCargoID(fc.Get("cargo"))
                }
                   
             };
            using (var ctx = new AppDbContext())
                {   
                    ctx.Usuarios.Add(usu);
                    ctx.SaveChanges();
                }
            ViewModel vm = new ViewModel { direcciones = Direccion.GetAllEstadoDireccion(), cargos = Cargo.GetAllCargo() };
            return RedirectToAction("TablaUsuarios","Configuration");
        }

        [HttpPost]
        [SessionExpire]
        [AutorizarRol]
        public ActionResult AgregarUsuario()
        {
            return View();
        }

        [SessionExpire]
        [AutorizarRol]
        public ActionResult TablaUsuarios()
        {
            ViewModel vm = new ViewModel { usuarios = Usuario.GetAllUsuarios() };
            return View(vm);
        }
        [SessionExpire]
        [AutorizarRol]
        public ActionResult  Variables()
        {
            ViewModel vm = new ViewModel { configuracion = Configuracion.GetLastConfiguracion() };
            return View(vm);
        }

        [SessionExpire]
        [AutorizarRol]
        [HttpPost]
        public ActionResult Variables(FormCollection fc)
        {
            Configuracion cf = new Configuracion
            {
                faov_retencion = float.Parse(fc.Get("faov_retencion")),
                faov_aporte = float.Parse(fc.Get("faov_aporte")),
                rpe_retencion = float.Parse(fc.Get("rpe_retencion")),
                rpe_aporte = float.Parse(fc.Get("rpe_aporte")),
                sso_retencion = float.Parse(fc.Get("sso_retencion")),
                sso_aporte = float.Parse(fc.Get("sso_aporte")),
                inces_retencion = float.Parse(fc.Get("inces_retencion")),
                inces_aporte = float.Parse(fc.Get("inces_aporte")),
                unid_tributaria = float.Parse(fc.Get("unid_tributaria")),
                bonoalimentacion = float.Parse(fc.Get("bonoalimentacion")),
                fecha_inicio_config = DateTime.Now,

            };
            Configuracion.AddConfiguracion(cf);
            ViewModel vm = new ViewModel { configuracion = Configuracion.GetLastConfiguracion() };
            return View(vm);
        }

        
        public ActionResult EditarUsuario(String usuario)
        {
            ViewModel vm = new ViewModel {
                direcciones2 = Direccion.GetAllEstadoDireccion(),
                cargos = Cargo.GetAllCargo(),
                usuario = Usuario.GetUsuario(usuario)
                
            };

            vm.direcciones = Direccion.GetAllDireccionPersona(vm.usuario.Empleado.Fk_Direccion);
            return View(vm);
        }
        [HttpPost]
        public ActionResult EditarUsuario(FormCollection fc)
        {
            return RedirectToAction("TablaUsuarios", "Configuration");
        }

        [SessionExpire]
        [AutorizarRol]
        public ActionResult DeleteUsuario(String usuario)
        {
            Usuario.DeleteUsuario(usuario);
            return RedirectToAction("TablaUsuarios","Configuration");
        }
    }
}