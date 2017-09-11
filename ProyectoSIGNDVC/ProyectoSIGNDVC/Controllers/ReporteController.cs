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
        [IniciarSesion]
        [AutorizarRol]
        public ActionResult Index(String error)
        {
            try
            {
                ViewModel vm = new ViewModel {
                    usuarios = Usuario.GetAllUsuarios()
                };
                var e = error;
                return View(vm);
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
           
        }
        [HttpPost]
        [IniciarSesion]
        [AutorizarRol]
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
                return RedirectToAction("UnexpectedError", "Error");
            }
            
        }

        [IniciarSesion]
        [AutorizarRol]
        public ActionResult Aprobacion_Quincenal()
        {

            try
            {
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
        }

        [IniciarSesion]
        [AutorizarRol]
        public ActionResult Detalle_Abono()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("UnexpectedError", "Error");
            }
        }
    }

}