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
        
        [IniciarSesion]
        [AutorizarRol]
        public ActionResult RegistroUsuario()
        {
            try
            {
                ViewModel vm = new ViewModel { direcciones = Direccion.GetAllEstadoDireccion(), cargos = Cargo.GetAllCargo() };
                return View(vm);
            } catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
            
        }


        [HttpPost]
        [IniciarSesion]
        [AutorizarRol]
        public ActionResult RegistroUsuario(FormCollection fc)
        {
            try
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
                        sueldo = float.Parse(fc.Get("sueldo")),
                        Codigo = fc.Get("codempleado"),
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
            catch (Exception e)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
        }

        [HttpPost]
        [IniciarSesion]
        [AutorizarRol]
        public ActionResult AgregarUsuario()
        {
            try
            {
                return View();
            } catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
            
        }

        [IniciarSesion]
        [AutorizarRol]
        public ActionResult TablaUsuarios()
        {
            try
            {
                ViewModel vm = new ViewModel { usuarios = Usuario.GetAllUsuarios() };
                return View(vm);
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
            
        }
        [IniciarSesion]
        [AutorizarRol]
        public ActionResult  Variables()
        {

            try
            {
                ViewModel vm = new ViewModel { configuracion = Configuracion.GetLastConfiguracion() };
                return View(vm);
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
            
        }

        [IniciarSesion]
        [AutorizarRol]
        [HttpPost]
        public ActionResult Variables(FormCollection fc)
        {

            try
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
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }

        }

        [IniciarSesion]
        [AutorizarRol]
        public ActionResult EditarUsuario(String usuario)
        {
            try
            {
                ViewModel vm = new ViewModel
                {
                    direcciones2 = Direccion.GetAllEstadoDireccion(),
                    cargos = Cargo.GetAllCargo(),
                    usuario = Usuario.GetUsuario(usuario)

                };

                vm.direcciones = Direccion.GetAllDireccionPersona(vm.usuario.Empleado.Fk_Direccion);
                return View(vm);
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
            
        }

        [IniciarSesion]
        [HttpPost]
        public ActionResult EditarUsuario(FormCollection fc)
        {
            try
            {
                Usuario usuario = Usuario.GetUsuario(Usuario.GetUsuario(int.Parse(fc.Get("idUsuario"))).usuario);
                Carga.DeleteCargasUsuario(usuario.usuario);
                usuario.Empleado.Cargas = null;
                Usuario.EditUsuario(usuario);

                var dir = Direccion.GetDireccion(usuario.Empleado.Fk_Direccion);
                dir.nombre = fc.Get("direccion");

                var casa = Direccion.GetDireccion(dir.Fk_Direccion.Value);
                casa.nombre = fc.Get("casa");

                var calle = Direccion.GetDireccion(casa.Fk_Direccion.Value);
                calle.nombre = fc.Get("calle");

                var ciudad = Direccion.GetDireccion(calle.Fk_Direccion.Value);
                ciudad.nombre = fc.Get("ciudad");
                ciudad.Fk_Direccion = Direccion.GetDireccionID(fc.Get("estado"), "Estado");


                Direccion.EditDireccion(casa);
                Direccion.EditDireccion(calle);
                Direccion.EditDireccion(ciudad);
                Direccion.EditDireccion(dir);



                int fk_dir = Direccion.InsertDireccion(fc.Get("casa"), "Casa", Direccion.InsertDireccion(fc.Get("calle"), "Calle", Direccion.InsertDireccion(fc.Get("ciudad"), "Ciudad", Direccion.GetDireccionID(fc.Get("estado"), "Estado"))));


                ///////////////////////////
                //////////////////////////
                List<Carga> cargas = new List<Carga>();
                for (int i = 2; i <= int.Parse(fc.Get("numfilas")); i++)
                {
                    var carga = new Carga
                    {
                        Persona = new Persona
                        {

                            nombre = fc.Get("nombrecarga" + i.ToString()),
                            apellido = fc.Get("apellidocarga" + i.ToString()),
                            sexo = fc.Get("sexocarga" + i.ToString()),
                            cedula = int.Parse(fc.Get("cedulacarga" + i.ToString())),
                            fecha_nacimiento = DateTime.Parse(fc.Get("fechanaccarga" + i.ToString()))
                        },
                        monto_poliza = int.Parse(fc.Get("montocarga" + i.ToString())),
                        Fk_Empleado = usuario.Empleado.EmpleadoID
                    };
                    Carga.AddCarga(carga);
                    cargas.Add(carga);
                };
                //Direccion.GetDireccion("");
                usuario = Usuario.GetUsuario(Usuario.GetUsuario(int.Parse(fc.Get("idUsuario"))).usuario);
                usuario.usuario = fc.Get("usuario");
                usuario.clave = fc.Get("clave");
                usuario.email = fc.Get("email");

                Cargo cargo = Cargo.GetCargo(Cargo.GetCargoID(fc.Get("cargo")));
                usuario.Empleado.Fk_Cargo = cargo.CargoID;
                usuario.Empleado.Cargo = cargo;

                usuario.Empleado.Banco = fc.Get("banco");
                usuario.Empleado.N_Cuenta = fc.Get("cuenta");
                usuario.Empleado.sueldo = float.Parse(fc.Get("sueldo"));
                usuario.Empleado.Codigo = fc.Get("codempleado");
                usuario.Empleado.Persona.nombre = fc.Get("nombre");
                usuario.Empleado.Persona.apellido = fc.Get("apellido");
                usuario.Empleado.Persona.cedula = int.Parse(fc.Get("cedula"));
                usuario.Empleado.Persona.fecha_nacimiento = DateTime.Parse(fc.Get("fechanac"));
                usuario.Empleado.Persona.sexo = fc.Get("sexo");
                usuario.Empleado.Cargas = cargas;
                //
                Usuario.EditUsuario(usuario);
                Empleado.EditEmpleado(usuario.Empleado);
                Persona.EditPersona(usuario.Empleado.Persona);
                return RedirectToAction("TablaUsuarios", "Configuration");
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }

        }

        [IniciarSesion]
        [AutorizarRol]
        public ActionResult DeleteUsuario(String usuario)
        {
            try
            {
                Usuario.DeleteUsuario(usuario);
                return RedirectToAction("TablaUsuarios", "Configuration");
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
            
        }
    }
}