using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace ProyectoSIGNDVC.Models
{
    public class PDF
    {

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
    }

}