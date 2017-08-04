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
            /*using (var ctx = new AppDbContext())
            {
                var empleado = (from emp in ctx.Empleados
                                select emp);
                foreach (var empl in empleado.ToList())
                {
                    if (empl.Pagos == null)
                    {
                        empl.Pagos = new List<Pago>();
                    }
                    Pago pago = new Pago
                    {
                        monto = empl.sueldo / 2,
                        aprobado = false,
                        f_pago = DateTime.Now,
                        numero_ref = 1234
                    };
                    empl.Pagos.Add(pago);
                    ctx.Nominas.Add(nomina);
                    nomina.Pagos.Add(pago);
                    ctx.SaveChanges();
                }
                ViewModel vm = new ViewModel {  nominaId = nomina.NominaID, usuarios = Usuario.GetAllUsuarios() };
                return View(vm);
            }*/
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
    }
}