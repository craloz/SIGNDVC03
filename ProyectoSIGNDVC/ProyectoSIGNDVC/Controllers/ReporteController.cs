using ProyectoSIGNDVC.Attributes;
using ProyectoSIGNDVC.Models;
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
        [HttpPost]
        [SessionExpire]
        public ActionResult Nomina_Trabajador(FormCollection fc)
        {
            Usuario usuario = Usuario.GetUsuario(fc.Get("usuario"));
            DateTime desde = DateTime.Parse(fc.Get("desde"));
            DateTime hasta = DateTime.Parse(fc.Get("hasta"));
            ViewModel vm = new ViewModel {
                pagos = Pago.GetPagos(usuario, desde, hasta),
                usuario = usuario,
                desde = desde,
                hasta = hasta
             };
            return View(vm);
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