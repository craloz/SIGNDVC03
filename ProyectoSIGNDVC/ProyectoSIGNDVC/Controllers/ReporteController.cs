using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSIGNDVC.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nomina_Trabajador()
        {
            return View();
        }

        public ActionResult Aprobacion_Quincenal()
        {
            return View();
        }

        public ActionResult Detalle_Abono()
        {
            return View();
        }
    }
}