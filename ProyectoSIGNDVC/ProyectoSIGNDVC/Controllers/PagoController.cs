using ProyectoSIGNDVC.Attributes;
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
        [SessionExpire]
        public ActionResult TablaPagos()
        {
            
            ViewModel vm = new ViewModel {pagos = Pago.GetPagos(Usuario.GetUsuario(Session["usuario"].ToString()).usuarioID) };
            return View(vm);
        }

        [SessionExpire]
        public ActionResult PagoNomina()
        {
                       
            var nomina = new Nomina
            {
                fecha_emision = DateTime.Now,
                Pagos = new List<Pago>(),

            };            
            ViewModel vm = new ViewModel { nominaId = nomina.NominaID, usuarios = Usuario.GetAllUsuarios(), empleados = Empleado.calcularSalario() };
            return View(vm);            
        }

        [SessionExpire]
        public ActionResult AprobarNomina(String nominaid)
        {
            //ViewBag.message = "Nomina: "+nominaid+"Aprobada";
            
            Models.Nomina.AprobarNomina(int.Parse(nominaid));
            Notificacion.AddNotificacion("NOMINA", "Nueva Nomina Creada", "Se ha creado una nueva solicitud de nomina", int.Parse(nominaid), 1);
            return RedirectToAction("ListaNomina","Pago");
        }

        [SessionExpire]
        public ActionResult Nomina()
        {
            return View();
        }

        [SessionExpire]
        public ActionResult ListaNomina()
        {
            ViewModel vm = new ViewModel { nominas = Models.Nomina.GetAllNominas() };
            return View(vm);
        }

        [SessionExpire]
        public ActionResult DetallePago()
        {
            return View();
        }

        [SessionExpire]
        [HttpPost]
        public ActionResult GenerarNomina(FormCollection fc)
        {
            
            DateTime fechaefectivo = DateTime.Parse(fc.Get("fechaefectiva"));
            Pago.GenerarNomina(fechaefectivo);
            return RedirectToAction("ListaNomina","Pago");
        }
    }
}