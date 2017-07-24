using ProyectoSIGNDVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSIGNDVC.Controllers
{
    public class PagoController : Controller
    {
        // GET: Pago
        public ActionResult TablaPagos()
        {
            return View();
        }

        public ActionResult PagoNomina()
        {
            ViewModel vm = new ViewModel();
            return View();
        }

        public ActionResult AprobarNomina()
        {
            return View();
        }

        public ActionResult Nomina()
        {
            return View();
        }

        public ActionResult ListaNomina()
        {
            return View();
        }

        public ActionResult DetallePago()
        {
            return View();
        }
    }
}