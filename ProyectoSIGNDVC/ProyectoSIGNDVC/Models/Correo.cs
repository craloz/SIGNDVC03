using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

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

        public void EnviarCorreo(String desde)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(this.emailTo));  // replace with valid value 
            message.From = new MailAddress(this.desde);  // replace with valid value
            message.Subject = this.subject;
            message.Body = string.Format(this.body);
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
                Notificacion.AddNotificacion(Properties.Resources.TipoNotificacionPago,Properties.Resources.TituloNuevoPago,Properties.Resources.DescripcionPago,pago.PagoID,u.usuarioID);
                String body = "<div style='margin-left: 30%; margin-right: 30%; border: 2px solid black !important; padding-left: 10px; padding-right: 10px; '> <div style='overflow: hidden;padding-left: 15px;padding-top: 15px;'><div style='float: left;width: 50%;'> 	<div ><img width='100%' height='60px' src='/ProyectoSIGNDVC/ProyectoSIGNDVC/Content/images/dvclogo.png'><p>DIVIDENDO VOLUNTARIO<br> PARA LA COMUNIDAD AC</p></div></div><div style='float: left;width: 50%'> <div style='text-align: right;'><strong>NOMBRETRABAJADOR</strong> <p>C.I N#: CEDULA:</p> <p>Codigo: xxxxxxx:</p> </div> </div></div> <p style='background-color: #45454;text-align: center'>Ha Recibido del <span style='font-weight: bold'>DIVIDENDO VOLUNTARIADO PARA LA COMUNIDAD</span>, por concepto de salario correspondiente a la NROQUINCENA Quincena de NOVIEMBRE ANIO.</p> <div style='overflow: hidden;text-align: right;' width='100%'> <div style='float: left; width: 50% !important; ' width='50%' height='100px'> <p>SALARIO</p> <p>RETROACTIVO</p><p style='text-decoration: underline;font-weight: bold'>DEDUCCIONES</p> <p>S.S.O</p> <p>R.P.E<p> <p>F.A.O.V<p> <p>I.N.C.E.S</p> <p>PRESTAMOS</p> <p>I.S.L.R</p> <p style='margin-bottom:0px'>POLIZA HCM</p> <p style='font-weight: bold;margin-top:0px'>TOTAL DEDUCCIONES</p> <p>NETO</p> </div> <div style='float: left; text-align: center; width: 50% !important;' width='50%' height='100px' > <div style='width: 50% !important;margin-left: 10px;'> <p>SUELDO</p> <p>RETROACT</p> <p>0,00</p> <p>SSO</p> <p>RPE</p> <p>FAOV</p> <p>INCES</p> <p>0,00</p> <p style='border-bottom: 1px solid black;margin-bottom:0px'>0,00</p> <p style='margin-top: 0px'>0,00</p> <p style='border-top: 1px solid black;'>RETENCIONES</p> </div> </div> </div> <p style='text-align: center'>Este monto fue abonado en la cuenta del BANCO VENEZOLANO DE CREDITO N# <span style='font-weight: bold'>xxxx-xxxx-xxxx</span></p> <p style='text-align: center;font-weight: bold'>Fecha: FECHA</p> </div>";
                body = body.Replace("NOMBRETRABAJADOR",u.Empleado.Persona.nombre +" "+u.Empleado.Persona.apellido);
                body = body.Replace("CEDULA", u.Empleado.Persona.cedula.ToString());
                body = body.Replace("FECHA", pago.f_pago.ToString());
                body = body.Replace("SUELDO", u.Empleado.sueldo.ToString());
                body = body.Replace("ANIO", year.ToString());
                body = body.Replace("SSO", e.SSO.ToString());
                body = body.Replace("RPE", e.RPE.ToString());
                body = body.Replace("FAOV", e.FAOV.ToString());
                body = body.Replace("INCES", e.INCES.ToString());
                if (day > 15)
                {
                    body = body.Replace("NROQUINCENA", "2da");
                }
                else
                {
                    body = body.Replace("NROQUINCENA", "1ra");
                }
                

                this.emailTo = u.email;
                this.body = body;
                this.desde = "carlosenriquelozanoperez@hotmail.com";
                this.subject = "Subject";
                //this.EnviarCorreo("carlosenriquelozanoperez@hotmail.com");
            }
        }

    }
}