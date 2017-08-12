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
        public ActionResult Test()
        {
            return View();
        }
        // GET: Configuration
        [SessionExpire]
        [HttpPost]
        public JsonResult Test(FormCollection fc)
        {
            DateTime dt=DateTime.Parse(fc.Get("fechanac"));
            
            return  Json(dt);
            
        }
        [SessionExpire]
        public ActionResult Index()
        {
            ViewModel vm = new ViewModel { direcciones = Direccion.GetAllEstadoDireccion(), cargos = Cargo.GetAllCargo() };
            return View(vm);
        }
        [HttpPost]
        [SessionExpire]
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
        [SessionExpire]
        public ActionResult AgregarUsuario()
        {
            return View();
        }

        [SessionExpire]
        public ActionResult TablaUsuarios()
        {
            ViewModel vm = new ViewModel { usuarios = Usuario.GetAllUsuarios() };
            return View(vm);
        }
        [SessionExpire]
        public ActionResult  Variables()
        {
            ViewModel vm = new ViewModel { configuracion = Configuracion.GetLastConfiguracion() };
            return View(vm);
        }

        [SessionExpire]
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

        [SessionExpire]
        public ActionResult EditarUsuario(String usuario)
        {
            ViewModel vm = new ViewModel {
                direcciones = Direccion.GetAllEstadoDireccion(),
                cargos = Cargo.GetAllCargo(),
                usuario = Usuario.GetUsuario(usuario)
            };
            return View(vm);
        }

        [SessionExpire]
        public ActionResult DeleteUsuario(String usuario)
        {
            Usuario.DeleteUsuario(usuario);
            return new HttpStatusCodeResult(200);
        }

        [SessionExpire]
        public JsonResult GetAllUsuarios()
        {
            return Json(Usuario.GetAllUsuarios(), JsonRequestBehavior.AllowGet);
        }

        [SessionExpire]
        public JsonResult GetUsuario(string usuario)
        {
            return Json(Usuario.GetUsuario(usuario),JsonRequestBehavior.AllowGet);
        }

        [SessionExpire]
        public async System.Threading.Tasks.Task<JsonResult> pruebaAsync()
        {
            var body = "<div style='margin-left: 30%; margin-right: 30%; border: 2px solid black !important; padding-left: 10px; padding-right: 10px; '> <div style='overflow: hidden;padding-left: 15px;padding-top: 15px;'> <div style='float: left;width: 50%;'> <div > <img width='100%' height='60px' src='/ProyectoSIGNDVC/ProyectoSIGNDVC/Content/images/dvclogo.png'> <p>DIVIDENDO VOLUNTARIO<br> PARA LA COMUNIDAD AC</p> </div> </div> <div style='float: left;width: 50%'> <div style='text-align: right;'> <strong>NOMBRE DEL TRABAJADOR</strong> <p>C.I N#: xxxxxxx:</p> <p>Codigo: xxxxxxx:</p> </div> </div> </div> <p style='background-color: #45454;text-align: center'>Ha Recibido del <span style='font-weight: bold'>DIVIDENDO VOLUNTARIADO PARA LA COMUNIDAD</span>, por concepto de salario correspondiente a la 1era. Quincena de NOVIEMBRE 2016.</p> <div style='overflow: hidden;text-align: right;' width='100%'> <div style='float: left; width: 50% !important; ' width='50%' height='100px'> <p>SALARIO</p> <p>RETROACTIVO</p> <p style='text-decoration: underline;font-weight: bold'>DEDUCCIONES</p> <p>S.S.O</p> <pR.P.E<p> <p>F.A.O.V<p> <p>I.N.C.E.S</p> <p>PRESTAMOS</p> <p>I.S.L.R</p> <p style='margin-bottom:0px'>POLIZA HCM</p> <p style='font-weight: bold;margin-top:0px'>TOTAL DEDUCCIONES</p> <p>NETO</p> </div> <div style='float: left; text-align: center; width: 50% !important;' width='50%' height='100px' > <div style='width: 50% !important;margin-left: 10px;'> <p>1.000.000</p> <p>0,00</p> <p>0,00</p> <p>0,00</p> <p>0,00</p> <p>0,00</p> <p>0,00</p> <p>0,00</p> <p style='border-bottom: 1px solid black;margin-bottom:0px'>0,00</p> <p style='margin-top: 0px'>0,00</p> <p style='border-top: 1px solid black;'>0,00</p> </div> </div> </div> <p style='text-align: center'>Este monto fue abonado en la cuenta del BANCO VENEZOLANO DE CREDITO N# <span style='font-weight: bold'>xxxx-xxxx-xxxx</span></p> <p style='text-align: center;font-weight: bold'>Fecha: 11/11/2016</p> </div>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("manuelpimentel16@gmail.com"));  // replace with valid value 
            message.From = new MailAddress("carlosenriquelozanoperez@hotmail.com");  // replace with valid value
            message.Subject = "Your Email Subject TEST";
            message.Body = string.Format(body);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = Properties.Resources.EmailDVC,  // replace with valid value
                    Password = Properties.Resources.PasswordDVC // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
                return Json("Sent",JsonRequestBehavior.AllowGet);
            }
        }
    }
}