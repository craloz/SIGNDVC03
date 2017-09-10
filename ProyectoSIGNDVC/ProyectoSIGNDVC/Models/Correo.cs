using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSIGNDVC.Models
{
    public class Correo
    {
        private String correo { get; set; }
        private String clave { get; set; }
        private String host { get; set; }
        private int puerto { get; set; }
        private String body { get; set; }
        private String emailTo { get; set; }
        private String desde { get; set; }
        private String subject { get; set; }
        private List<Pago> pagos { get; set; }


        public Correo(String body, String emailTo, String desde, String subject)
        {
            this.correo = Properties.Resources.EmailDVC;
            this.clave = Properties.Resources.PasswordDVC;
            this.host = Properties.Resources.Host;
            this.puerto = 587;
            this.emailTo = emailTo;
            this.body = body;
            this.desde = desde;
            this.subject = subject;
        }

        public Correo(List<Pago> pagos)
        {
            this.correo = Properties.Resources.EmailDVC;
            this.clave = Properties.Resources.PasswordDVC;
            this.host = Properties.Resources.Host;
            this.puerto = 587;
            this.pagos = pagos;
        }

        public void EnviarCorreo(String desde, int pagoid)
        {

            PDF pdf = new PDF();
            var message = new MailMessage();
            message.To.Add(new MailAddress(this.emailTo));  // replace with valid value 
            message.From = new MailAddress(this.desde);  // replace with valid value
            message.Subject = this.subject;

            message.Attachments.Add(new Attachment(pdf.generarPDF(pagoid), "test.pdf"));

            message.Body = string.Format(this.body);


            /*var res = new LinkedResource("C:/Users/Carlos/Desktop/Servicio Comunitario Definitivo/SIGNDVC03/ProyectoSIGNDVC/ProyectoSIGNDVC/Content/images/dvclogo.png");
            res.ContentId = Guid.NewGuid().ToString();
            string htmlBody = this.body.Replace("CID", res.ContentId);
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(res);
            message.AlternateViews.Add(alternateView);*/
            //message.Body = body;

            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = this.correo,  // replace with valid value
                    Password = this.clave // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = this.host;
                smtp.Port = this.puerto;
                smtp.EnableSsl = true;
                try
                {
                    smtp.Send(message);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: "+e);

                }
            }
        }

        public void EnviarCorreoPagos()
        {
            foreach (var pago in this.pagos)
            {
                DateTime today = DateTime.Now;
                int day = today.Day;
                int year = today.Year;
                
                Usuario u = Usuario.GetUsuario((Usuario.GetUsuario(pago.Fk_Empleado).usuario));
                Empleado e = Empleado.calcularSalarioByEmp(u.Empleado.EmpleadoID);
                Notificacion.AddNotificacion("PAGO",Properties.Resources.TituloNuevoPago,Properties.Resources.DescripcionNuevoPago,pago.PagoID,u.usuarioID);
                String body = @"<div style='margin-left: 30%; margin-right: 30%; border: 2px solid black !important; padding-left: 10px; padding-right: 10px; '> <div style='overflow: hidden;padding-left: 15px;padding-top: 15px;'><div style='float: left;width: 50%;'> 	<div ><img width='100%' height='60px' src='cid:CID'><p>DIVIDENDO VOLUNTARIO<br> PARA LA COMUNIDAD AC</p></div></div><div style='float: left;width: 50%'> <div style='text-align: right;'><strong>"
                    + u.Empleado.Persona.nombre + " " + u.Empleado.Persona.apellido
                    + "</strong> <p>C.I N#: "+ u.Empleado.Persona.cedula.ToString() + "</p> <p>Codigo: xxxxxxx:</p> </div> </div></div> <p style='background-color: #45454;text-align: center'>Ha Recibido del <span style='font-weight: bold'>DIVIDENDO VOLUNTARIADO PARA LA COMUNIDAD</span>, por concepto de salario correspondiente a la NROQUINCENA Quincena de NOVIEMBRE "
                    + year.ToString() + ".</p> <div style='overflow: hidden;text-align: right;' width='100%'> <div style='float: left; width: 50% !important; ' width='50%' height='100px'> <p>SALARIO</p> <p>RETROACTIVO</p><p style='text-decoration: underline;font-weight: bold'>DEDUCCIONES</p> <p>S.S.O</p> <p>R.P.E<p> <p>F.A.O.V<p> <p>I.N.C.E.S</p> <p>PRESTAMOS</p> <p>I.S.L.R</p> <p style='margin-bottom:0px'>POLIZA HCM</p> <p style='font-weight: bold;margin-top:0px'>TOTAL DEDUCCIONES</p> <p>NETO</p> </div> <div style='float: left; text-align: center; width: 50% !important;' width='50%' height='100px' > <div style='width: 50% !important;margin-left: 10px;'> <p>"
                    + u.Empleado.sueldo.ToString() + "</p> <p>RETROACT</p> <p>0,00</p> <p>"
                    + e.SSO.ToString() + "</p> <p>"
                    + e.RPE.ToString() + "</p> <p>"
                    + e.FAOV.ToString() + "</p> <p>INCES</p> <p>0,00</p> <p style='border-bottom: 1px solid black;margin-bottom:0px'>0,00</p> <p style='margin-top: 0px'>0,00</p> <p style='border-top: 1px solid black;'>RETENCIONES</p> </div> </div> </div> <p style='text-align: center'>Este monto fue abonado en la cuenta del "
                    + u.Empleado.Banco +" N# <span style='font-weight: bold'>"
                    + u.Empleado.N_Cuenta +"</span></p> <p style='text-align: center;font-weight: bold'>Fecha: "+
                    pago.f_pago.ToString() + "</p> </div>";

                /*String body = "<div style='margin-left: 30%; margin-right: 30%; border: 2px solid black !important; padding-left: 10px; padding-right: 10px; '> <div style='overflow: hidden;padding-left: 15px;padding-top: 15px;'><div style='float: left;width: 50%;'> 	<div ><img width='100%' height='60px' src='../Content/images/dvclogo.png'><p>DIVIDENDO VOLUNTARIO<br> PARA LA COMUNIDAD AC</p></div></div><div style='float: left;width: 50%'> <div style='text-align: right;'><strong>"
                    +u.Empleado.Persona.nombre + " " + u.Empleado.Persona.apellido
                    + "</strong> <p>C.I N#: "+ u.Empleado.Persona.cedula.ToString() + "</p> <p>Codigo: xxxxxxx:</p> </div> </div></div> <p style='background-color: #45454;text-align: center'>Ha Recibido del <span style='font-weight: bold'>DIVIDENDO VOLUNTARIADO PARA LA COMUNIDAD</span>, por concepto de salario correspondiente a la NROQUINCENA Quincena de NOVIEMBRE "
                    + year.ToString() + ".</p> <div style='overflow: hidden;text-align: right;' width='100%'> <div style='float: left; width: 50% !important; ' width='50%' height='100px'> <p>SALARIO</p> <p>RETROACTIVO</p><p style='text-decoration: underline;font-weight: bold'>DEDUCCIONES</p> <p>S.S.O</p> <p>R.P.E<p> <p>F.A.O.V<p> <p>I.N.C.E.S</p> <p>PRESTAMOS</p> <p>I.S.L.R</p> <p style='margin-bottom:0px'>POLIZA HCM</p> <p style='font-weight: bold;margin-top:0px'>TOTAL DEDUCCIONES</p> <p>NETO</p> </div> <div style='float: left; text-align: center; width: 50% !important;' width='50%' height='100px' > <div style='width: 50% !important;margin-left: 10px;'> <p>"
                    + u.Empleado.sueldo.ToString() + "</p> <p>RETROACT</p> <p>0,00</p> <p>"
                    + e.SSO.ToString() + "</p> <p>"
                    + e.RPE.ToString() + "</p> <p>"
                    + e.FAOV.ToString() + "</p> <p>INCES</p> <p>0,00</p> <p style='border-bottom: 1px solid black;margin-bottom:0px'>0,00</p> <p style='margin-top: 0px'>0,00</p> <p style='border-top: 1px solid black;'>RETENCIONES</p> </div> </div> </div> <p style='text-align: center'>Este monto fue abonado en la cuenta del BANCO VENEZOLANO DE CREDITO N# <span style='font-weight: bold'>xxxx-xxxx-xxxx</span></p> <p style='text-align: center;font-weight: bold'>Fecha: "+
                    pago.f_pago.ToString() + "</p> </div>";*/

                this.emailTo = u.email;                
                this.body = body;
                this.desde = "carlosenriquelozanoperez@hotmail.com";
                this.subject = Properties.Resources.TituloNuevoPago +" "+ pago.numero_ref.ToString();
                this.EnviarCorreo(Properties.Resources.EmailDVC,pago.PagoID);
            }
        }

    }
}