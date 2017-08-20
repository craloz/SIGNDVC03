using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using ProyectoSIGNDVC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using iTextSharp.text;

using iTextSharp.text.pdf;

namespace ProyectoSIGNDVC
{
    public class Pago
    {
        [Key]
        public int PagoID { get; set; }
        public int numero_ref { get; set; }
        public DateTime f_pago { get; set; }
        public float monto { get; set; }
        public Boolean aprobado { get; set; }
        public int Fk_Empleado { get; set; }
        [ForeignKey("Fk_Empleado")]
        public Empleado Empleado { get; set; }
        public int Fk_Nomina { get; set; }
        [ForeignKey("Fk_Nomina")]
        public Persona Persona { get; set; }


        public static List<Pago> CreatePagos(List<Usuario> usuarios)
        {
            List<Pago> listaPagos = new List<Pago>();
            foreach (var usuario in usuarios)
            {
                listaPagos.Add(new Pago {
                    numero_ref = 12345,
                    monto = usuario.Empleado.sueldo / 2,
                    f_pago = DateTime.Now,
                    aprobado = false

                });
            }
            return listaPagos;
        }


        public static void GenerarNomina(DateTime fechaefectiva)
        {
            using (var ctx = new AppDbContext())
            {
                Nomina.AddNomina(fechaefectiva);                
                int idnom = Nomina.GetLastNominaID();
                List<Empleado> listEmp = Empleado.calcularSalario();
                DateTime today = DateTime.Now;
                int year = today.Year;
                int month = today.Month;
                foreach (var emp in listEmp)
                {
                    int cont = 1;
                    Pago pago = new Pago()
                    {
                        numero_ref = int.Parse(month.ToString() + year.ToString() + cont.ToString().PadLeft(4, '0')),
                        f_pago = today,
                        monto = emp.MontoTotal,
                        aprobado = false,
                        Fk_Empleado = emp.EmpleadoID,
                        Fk_Nomina = idnom
                    };
                    ctx.Pagos.Add(pago);
                    ctx.SaveChanges();

                }
            }
        }

        public static List<Pago> GetPagos(int empleadoId)
        {
            using(var ctx = new AppDbContext())
            {
                var query = ( from pago in ctx.Pagos
                              where pago.Fk_Empleado == empleadoId
                              select pago
                    );
                return query.ToList();
            }
                
        }

        public static List<Pago> GetAllPagosNomina(int nominaid)
        {
            using (var ctx = new AppDbContext())
            {
                var query = (from pag in ctx.Pagos
                             where pag.Fk_Nomina == nominaid
                             select pag
                            );
                return query.ToList();
            }
        }

        public static Pago GetPago(int pagoId)
        {
            using (var ctx = new AppDbContext())
            {
                var query = (from pag in ctx.Pagos
                             where pag.PagoID == pagoId
                             select pag
                    );
                return query.FirstOrDefault();
            }

        }

    }

    

   
}