using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using ProyectoSIGNDVC.Attributes;
using ProyectoSIGNDVC.Models;
using System.Net.Mail;
using System.Threading;
using System.Web.Mvc;

namespace ProyectoSIGNDVC.Models
{
    public class PDF
    {

        private List<Pago> pagos { get; set; }

        public void htmlToPDF()
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream("c:\\my.pdf", FileMode.Create));
            document.Open();
            WebClient wc = new WebClient();
            string htmlText = wc.DownloadString("http://localhost:59500/my.html");
            //Response.Write(htmlText);
            List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StringReader(htmlText), null);
            for (int k = 0; k < htmlarraylist.Count; k++)
            {
                document.Add((IElement)htmlarraylist[k]);
            }

            document.Close();
        }


        public MemoryStream generarPDF(int pagoid)
        {
            //Create a byte array that will eventually hold our final PDF
            Byte[] bytes;
            MemoryStream mss = new MemoryStream();

            //Boilerplate iTextSharp setup here
            //Create a stream that we can write to, in this case a MemoryStream
            using (var ms = new MemoryStream())
            {

                //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                using (var doc = new Document())
                {

                    //Create a writer that's bound to our PDF abstraction and our stream
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {

                        //Open the document for writing
                        doc.Open();

                        Pago pago = Pago.GetPago(pagoid);

                        DateTime today = DateTime.Now;
                        int day = today.Day;
                        int year = today.Year;
                        int month = today.Month;
                        String[] meses = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

                        Usuario u = Usuario.GetUsuario((Usuario.GetUsuario(pago.Fk_Empleado).usuario));
                        Empleado e = Empleado.calcularSalarioByEmp(u.Empleado.EmpleadoID);
                        var body = @"<div style='margin-left: 30%; margin-right: 30%; border: 2px solid black !important; padding-left: 10px; padding-right: 10px; '><div style='overflow: hidden;padding-left: 15px;padding-top: 15px;'><div style='float: left;width: 50%;'><p>DIVIDENDO VOLUNTARIO PARA LA COMUNIDAD AC</p></div><div style='float: left;width: 50%'><div style='text-align: right;'><strong>"
                        + u.Empleado.Persona.nombre + " " + u.Empleado.Persona.apellido
                        + "</strong><p>C.I N#: " + u.Empleado.Persona.cedula.ToString() + "</p><p>Codigo: " + u.Empleado.Codigo + "</p></div></div></div><div><p style='background-color: #45454;text-align: center'>Ha Recibido del <span style='font-weight: bold'>DIVIDENDO VOLUNTARIADO PARA LA COMUNIDAD</span>, por concepto de salario correspondiente a la NROQUINCENA Quincena de "
                        + meses[month - 1] + " " + year.ToString() + ".</p> <div style='overflow: hidden;text-align: right;' > <div style='float: left; width: 50% !important; ' > <p>SALARIO</p> <p>RETROACTIVO</p><p style='text-decoration: underline;font-weight: bold'>DEDUCCIONES</p> <p>S.S.O</p> <p>R.P.E</p> <p>F.A.O.V</p> <p>I.N.C.E.S</p> <p>PRESTAMOS</p> <p>I.S.L.R</p> <p style='margin-bottom:0px'>POLIZA HCM</p> <p style='font-weight: bold;margin-top:0px'>TOTAL DEDUCCIONES</p> <p>NETO</p> </div> <div style='float: left; text-align: center; width: 50% !important;'  > <div style='width: 50% !important;margin-left: 10px;'> <p>"
                        + u.Empleado.sueldo.ToString() + "</p><p>RETROACT</p><p>0,00</p><p>"
                        + e.SSO.ToString() + "</p><p>"
                        + e.RPE.ToString() + "</p><p>"
                        + e.FAOV.ToString() + "</p><p>"
                        + e.INCES + "</p><p>0,00</p><p style='border-bottom: 1px solid black;margin-bottom:0px'>0,00</p><p style='margin-top: 0px'>0,00</p><p style='border-top: 1px solid black;'>RETENCIONES</p></div></div></div><p style='text-align: center'>Este monto fue abonado en la cuenta del "
                        + u.Empleado.Banco + " N# <span style='font-weight: bold'>"
                        + u.Empleado.N_Cuenta + "</span></p><p style='text-align: center;font-weight: bold'>Fecha: " +
                        pago.f_pago.ToString() + "</p></div></div>";
                        
                        //XMLWorker also reads from a TextReader and not directly from a string
                        using (var srHtml = new StringReader(body))
                        {
                            //Parse the HTML
                            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, srHtml);
                        }

                        doc.Close();
                    }
                }

                //After all of the PDF "stuff" above is done and closed but **before** we
                //close the MemoryStream, grab all of the active bytes from the stream
                bytes = ms.ToArray();
                mss = ms;

            }

            //Now we just need to do something with those bytes.
            //Here I'm writing them to disk but if you were in ASP.Net you might Response.BinaryWrite() them.
            //You could also write the bytes to a database in a varbinary() column (but please don't) or you
            //could pass them to another function for further PDF processing.
            var testFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.pdf");
            System.IO.File.WriteAllBytes(testFile, bytes);
            return mss;
        }

         
    }

}