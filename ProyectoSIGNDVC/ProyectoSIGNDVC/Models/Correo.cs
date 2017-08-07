﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ProyectoSIGNDVC.Models
{
    public class Correo
    {
        private String correo { get; set; }
        private String clave { get; set; }
        private String host { get; set; }
        private int puerto { get; set; }
        private static Correo instance;


        private Correo()
        {
            this.correo = Properties.Resources.EmailDVC;
            this.clave = Properties.Resources.PasswordDVC;
            this.host = Properties.Resources.Host;
            this.puerto = int.Parse(Properties.Resources.Puerto);
        }

        public static Correo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Correo();
                }
                return instance;
            }
        }

        public async System.Threading.Tasks.Task EnviarCorreoAsync(String body, String para, String desde)
        {
            //body = "<div style='margin-left: 30%; margin-right: 30%; border: 2px solid black !important; padding-left: 10px; padding-right: 10px; '> <div style='overflow: hidden;padding-left: 15px;padding-top: 15px;'> <div style='float: left;width: 50%;'> <div > <img width='100%' height='60px' src='/ProyectoSIGNDVC/ProyectoSIGNDVC/Content/images/dvclogo.png'> <p>DIVIDENDO VOLUNTARIO<br> PARA LA COMUNIDAD AC</p> </div> </div> <div style='float: left;width: 50%'> <div style='text-align: right;'> <strong>NOMBRE DEL TRABAJADOR</strong> <p>C.I N#: xxxxxxx:</p> <p>Codigo: xxxxxxx:</p> </div> </div> </div> <p style='background-color: #45454;text-align: center'>Ha Recibido del <span style='font-weight: bold'>DIVIDENDO VOLUNTARIADO PARA LA COMUNIDAD</span>, por concepto de salario correspondiente a la 1era. Quincena de NOVIEMBRE 2016.</p> <div style='overflow: hidden;text-align: right;' width='100%'> <div style='float: left; width: 50% !important; ' width='50%' height='100px'> <p>SALARIO</p> <p>RETROACTIVO</p> <p style='text-decoration: underline;font-weight: bold'>DEDUCCIONES</p> <p>S.S.O</p> <pR.P.E<p> <p>F.A.O.V<p> <p>I.N.C.E.S</p> <p>PRESTAMOS</p> <p>I.S.L.R</p> <p style='margin-bottom:0px'>POLIZA HCM</p> <p style='font-weight: bold;margin-top:0px'>TOTAL DEDUCCIONES</p> <p>NETO</p> </div> <div style='float: left; text-align: center; width: 50% !important;' width='50%' height='100px' > <div style='width: 50% !important;margin-left: 10px;'> <p>1.000.000</p> <p>0,00</p> <p>0,00</p> <p>0,00</p> <p>0,00</p> <p>0,00</p> <p>0,00</p> <p>0,00</p> <p style='border-bottom: 1px solid black;margin-bottom:0px'>0,00</p> <p style='margin-top: 0px'>0,00</p> <p style='border-top: 1px solid black;'>0,00</p> </div> </div> </div> <p style='text-align: center'>Este monto fue abonado en la cuenta del BANCO VENEZOLANO DE CREDITO N# <span style='font-weight: bold'>xxxx-xxxx-xxxx</span></p> <p style='text-align: center;font-weight: bold'>Fecha: 11/11/2016</p> </div>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(para));  // replace with valid value 
            message.From = new MailAddress("carlosenriquelozanoperez@hotmail.com");  // replace with valid value
            message.Subject = "Your Email Subject TEST";
            message.Body = string.Format(body);
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
                await smtp.SendMailAsync(message);
            }
        }


    }
}