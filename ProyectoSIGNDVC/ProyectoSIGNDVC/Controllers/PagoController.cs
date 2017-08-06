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
                       
            var nomina = new Nomina
            {
                fecha_emision = DateTime.Now,
                Pagos = new List<Pago>(),

            };            
            ViewModel vm = new ViewModel { nominaId = nomina.NominaID, usuarios = Usuario.GetAllUsuarios(), empleados = Empleado.calcularSalario() };
            return View(vm);            
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

        [HttpPost]
        public ActionResult GenerarNomina(FormCollection fc)
        {
            
            DateTime fechaefectivo = DateTime.Parse(fc.Get("fechaefectiva"));
            Pago.GenerarNomina(fechaefectivo);
            return View();
        }
    }
}