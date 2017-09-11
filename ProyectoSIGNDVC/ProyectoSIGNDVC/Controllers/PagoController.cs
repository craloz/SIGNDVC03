using iTextSharp.text;
using iTextSharp.text.pdf;
using ProyectoSIGNDVC.Attributes;
using ProyectoSIGNDVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

            try
            {
                ViewModel vm = new ViewModel { pagos = Pago.GetPagos(Usuario.GetUsuario(Session["usuario"].ToString()).usuarioID) };
                return View(vm);
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }

        }

        [SessionExpire]
        public ActionResult PagoNomina()
        {

            try
            {
                var nomina = new Nomina
                {
                    fecha_emision = DateTime.Now,
                    Pagos = new List<Pago>(),

                };
                float total = 0;
                List<Empleado> listemp = Empleado.calcularSalario();
                foreach (var emp in listemp)
                {
                    total += emp.MontoTotal;
                }
                ViewModel vm = new ViewModel { nominaId = nomina.NominaID, usuarios = Usuario.GetAllUsuarios(), empleados = listemp, totalNomina = total };
                return View(vm);
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
            
        }

        [SessionExpire]
        public ActionResult AprobarNomina(String nominaid)
        {

            try
            {
                Models.Nomina.AprobarNomina(int.Parse(nominaid));
                return RedirectToAction("ListaNomina", "Pago");
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }

        }

        [SessionExpire]
        public ActionResult AprobarPago(String pagoid)
        {
            try
            {
                return RedirectToAction("TablaPagos", "Pago");
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
            
        }

        [SessionExpire]
        public ActionResult Nomina()
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

        [SessionExpire]
        public ActionResult ListaNomina()
        {
            try
            {
                ViewModel vm = new ViewModel { nominas = Models.Nomina.GetAllNominas() };
                return View(vm);
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }

        }

        [SessionExpire]
        public ActionResult VerNomina( int nominaid)
        {

            try
            {
                float total = 0;
                List<Empleado> listemp = Empleado.calcularSalarioByNomina(nominaid);
                List<Pago> listPago = Pago.GetAllPagosNomina(nominaid);
                ViewModel vm = new ViewModel { pagos = listPago, empleados = listemp, totalNomina = listPago[0].monto };
                return View(vm);
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
            
        }


        [SessionExpire]
        public ActionResult DetallePago()
        {

            try
            {
                return RedirectToAction("VerNomina", "Pago");
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }

            
        }

        [SessionExpire]
        [HttpPost]
        public ActionResult GenerarNomina(FormCollection fc)
        {

            try
            {
                DateTime fechaefectivo = DateTime.Parse(fc.Get("fechaefectiva"));
                Pago.GenerarNomina(fechaefectivo);

                return RedirectToAction("ListaNomina", "Pago");
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }

           
        }

        [HttpPost]
        public ActionResult EditarPagoByEmp(FormCollection fc)
        {

            try
            {
                return RedirectToAction("PagoNomina", "Pago");
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
        }


        [SessionExpire]
        public ActionResult EditarPago()
        {
            try
            {
                List<Empleado> listemp = Empleado.calcularSalario();

                ViewModel vm = new ViewModel{empleados = listemp};
                return View(vm);
                
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }


        }




        [AutorizarCoordinadoraAdm]
        public ActionResult Prueba()
        {
            try
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
                        UserName = Properties.Resources.EmailDVC,  // replace with valid value
                        Password = Properties.Resources.PasswordDVC // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = Properties.Resources.EmailDVC;
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    try
                    {
                        smtp.Send(message);
                        var i = 1;
                    }
                    catch (Exception e)
                    {

                    }
                }

                return Json("Hola", JsonRequestBehavior.AllowGet);
                /////////////////////////////////////////////////////////////////////////////////////
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }

            
        }

        public ActionResult VerPago(String pago)
        {
            Pago pag = new Pago();
            pag = Pago.GetPago(int.Parse(pago));

            PDF pd = new PDF();
            Byte[] pdf = pd.generarPDF(int.Parse(pago)).ToArray();
            ViewModel vm = new ViewModel {
                pago = pag,
                empleado = Empleado.calcularSalarioByEmp(pag.Fk_Empleado)
            };
            return View(vm);
        }

        public ActionResult DownloadPdf(String pago)
        {
            PDF pdf = new PDF();
            MemoryStream stream = pdf.generarPDF(int.Parse(pago));
            return File(stream, "application/pdf", "DownloadName.pdf");
        }

        public FileStreamResult VerPagoPdf(String pago)
        {
            MemoryStream workStream = new MemoryStream();

            Pago p = Pago.GetPago(int.Parse(pago));
            Usuario u = Usuario.GetUsuario(Usuario.GetUsuario(p.Fk_Empleado).usuario);

            /*
            Document document = new Document();
            PdfWriter.GetInstance(document, workStream).CloseStream = false;
            string imageFilePath = Server.MapPath("/Content/images/dvclogo.png");
            Image dvcLogo = Image.GetInstance(imageFilePath);
            dvcLogo.ScaleToFit(140f, 120f);
            dvcLogo.SpacingBefore = 10f;
            dvcLogo.SpacingAfter = 1f;
            dvcLogo.Alignment = Element.ALIGN_CENTER;

            document.Open();
            document.Add(dvcLogo);
            document.Add(new Paragraph("Usuario: "+u.usuario));
            document.Add(new Paragraph("Nombre: " + u.Empleado.Persona.nombre +" "+u.Empleado.Persona.apellido +" CI:"+ u.Empleado.Persona.cedula ));
            document.Close();

            */
            PDF pdf = new PDF();
            //byte[] byteInfo = workStream.ToArray();
            byte[] byteInfo = pdf.generarPDF(int.Parse(pago)).ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;
        
            return new FileStreamResult(workStream, "application/pdf");

        }
    }


}