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
        public ActionResult Index(String error)
        {
            try
            {
                var e = error;
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Unexpected", "Error");
            }
           
        }
        [HttpPost]
        [SessionExpire]
        public ActionResult Nomina_Trabajador(FormCollection fc)
        {
            try
            {
                Usuario usuario = Usuario.GetUsuario(fc.Get("usuario"));
                DateTime desde = DateTime.Parse(fc.Get("desde"));
                DateTime hasta = DateTime.Parse(fc.Get("hasta"));
                ViewModel vm = new ViewModel
                {
                    pagos = Pago.GetPagos(usuario, desde, hasta),
                    usuario = usuario,
                    desde = desde,
                    hasta = hasta
                };
                return View(vm);
            }
            catch (Exception)
            {
                return RedirectToAction("Unexpected", "Error");
            }
            
        }

        [SessionExpire]
        public ActionResult Aprobacion_Quincenal()
        {

            try
            {
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Unexpected", "Error");
            }
        }

        [SessionExpire]
        public ActionResult Detalle_Abono()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Unexpected", "Error");
            }
        }
    }

}