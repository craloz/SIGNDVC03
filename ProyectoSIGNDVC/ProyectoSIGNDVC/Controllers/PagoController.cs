using ProyectoSIGNDVC.Attributes;
using ProyectoSIGNDVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSIGNDVC.Controllers
{
    public class PagoController : Controller
    {
        // GET: Pago
        [SessionExpire]
        public ActionResult TablaPagos()
        {
            
            ViewModel vm = new ViewModel {pagos = Pago.GetPagos(Usuario.GetUsuario(Session["usuario"].ToString()).usuarioID) };
            return View(vm);
        }

        [SessionExpire]
        public ActionResult PagoNomina()
        {
                       
            var nomina = new Nomina
            {
                fecha_emision = DateTime.Now,
                Pagos = new List<Pago>(),

            };            
            ViewModel vm = new ViewModel { nominaId = nomina.NominaID, usuarios = Usuario.GetAllUsuarios(), empleados = Empleado.calcularSalario() };
            return View(vm);            
        }

        [SessionExpire]
        public ActionResult AprobarNomina(String nominaid)
        {
            //ViewBag.message = "Nomina: "+nominaid+"Aprobada";
            
            Models.Nomina.AprobarNomina(int.Parse(nominaid));
           /* Alpha oAlpha = new Alpha();
            Thread oThread = new Thread(new ThreadStart(oAlpha.Beta));
            oThread.Start();*/
            //Correo c = new Correo(Pago.GetAllPagosNomina(int.Parse(nominaid)));
            //Thread thread = new Thread(new ThreadStart(c.EnviarCorreoPagosAsync));
            //thread.Start();
            Notificacion.AddNotificacion("NOMINA", "Nueva Nomina Creada", "Se ha creado una nueva solicitud de nomina", int.Parse(nominaid), 1);
            return RedirectToAction("ListaNomina","Pago");
        }

        [SessionExpire]
        public ActionResult AprobarPago(String pagoid)
        {
            return RedirectToAction("TablaPagos", "Pago");
        }

        [SessionExpire]
        public ActionResult Nomina()
        {
            return View();
        }

        [SessionExpire]
        public ActionResult ListaNomina()
        {
            ViewModel vm = new ViewModel { nominas = Models.Nomina.GetAllNominas() };
            return View(vm);
        }

        [SessionExpire]
        public ActionResult DetallePago()
        {
            return View();
        }

        [SessionExpire]
        [HttpPost]
        public ActionResult GenerarNomina(FormCollection fc)
        {
            
            DateTime fechaefectivo = DateTime.Parse(fc.Get("fechaefectiva"));
            Pago.GenerarNomina(fechaefectivo);
            return RedirectToAction("ListaNomina","Pago");
        }

        public JsonResult Prueba()
        {

            /*
            Correo c = new Correo(Pago.GetAllPagosNomina(int.Parse(nominaid)));
            Thread thread = new Thread(new ThreadStart(c.EnviarCorreoPagos));
            thread.Start()*/

            //Correo c = new Correo("Body", "carlos.elp94@gmail.com", "carlosenriquelozanoperez@hotmail.com", "Asunto");
            //c.EnviarCorreoAsync("carlosenriquelozanoperez@hotmail.com");
            //return Json("hola", JsonRequestBehavior.AllowGet);



            //////////////////////////////////////////////////////////////////////////////////////
            /*string to = "carlos.elp94@gmail.com";
            string from = "carlosenriquelozanoperez@contoso.com";
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Using the new SMTP client.";
            message.Body = @"Using this new feature, you can send an e-mail message from an application very easily.";
            SmtpClient client = new SmtpClient(server);
            // Credentials are necessary if the server requires the client 
            // to authenticate before it will send e-mail on the client's behalf.
            client.UseDefaultCredentials = true;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                            ex.ToString());
            }*/


            var message = new MailMessage();
            message.To.Add(new MailAddress("carlos.elp94@gmail.com"));  // replace with valid value 
            message.From = new MailAddress("carlosenriquelozanoperez@hotmail.com");  // replace with valid value
            message.Subject = "Your Email Subject TEST";
            message.Body = string.Format("Body");
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "carlosenriquelozanoperez@hotmail.com",  // replace with valid value
                    Password = "tibiapl" // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = Properties.Resources.Host;
                smtp.Port = 587;
                smtp.EnableSsl = true;
                try { 
                smtp.Send(message);
                }catch(Exception e)
                {

                }
            }

            return Json("Hola",JsonRequestBehavior.AllowGet);
            /////////////////////////////////////////////////////////////////////////////////////
        }
    }
}