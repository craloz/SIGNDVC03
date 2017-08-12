using ProyectoSIGNDVC.Attributes;
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
        [SessionExpire]
        public ActionResult Index()
        {
            return View();
        }

        [SessionExpire]
        public ActionResult Nomina_Trabajador()
        {
            return View();
        }

        [SessionExpire]
        public ActionResult Aprobacion_Quincenal()
        {
            return View();
        }

        [SessionExpire]
        public ActionResult Detalle_Abono()
        {
            return View();
        }
    }
}