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
        public Nomina Nomina { get; set; }
        
        public float SSO { get; set; }
        
        public float RPE { get; set; }
        
        public float FAOV { get; set; }

        public float sueldo { get; set; }

        public float prestamos { get; set; }
        public float retroactivos { get; set; }

        public float INCES { get; set; }
        [NotMapped]
        public float Retenciones { get; set; }
        
        public float SSO_ap { get; set; }
        
        public float RPE_ap { get; set; }
        
        public float FAOV_ap { get; set; }
        
        public float INCES_ap { get; set; }
        [NotMapped]
        public float Aportes { get; set; }
        
        public float BonoAlimentacion { get; set; }
        [NotMapped]
        public float costoCargas { get; set; }
        [NotMapped]
        public float MontoTotal { get; set; }


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
                        Fk_Nomina = idnom,
                        sueldo = emp.sueldo,
                        SSO = emp.SSO,
                        RPE = emp.RPE,
                        FAOV = emp.FAOV,
                        INCES = emp.INCES,
                        SSO_ap = emp.SSO_ap,
                        RPE_ap = emp.RPE_ap,
                        FAOV_ap = emp.FAOV_ap,
                        INCES_ap = emp.INCES_ap,
                        BonoAlimentacion = emp.BonoAlimentacion
                    };
                    ctx.Pagos.Add(pago);
                    ctx.SaveChanges();

                }
            }
        }

        public static void GenerarNomina(DateTime fechaefectiva, List<Empleado> listEm)
        {
            using (var ctx = new AppDbContext())
            {
                Nomina.AddNomina(fechaefectiva);
                int idnom = Nomina.GetLastNominaID();                
                DateTime today = DateTime.Now;
                int year = today.Year;
                int month = today.Month;
                foreach (var emp in listEm)
                {
                    int cont = 1;
                    Pago pago = new Pago()
                    {
                        numero_ref = int.Parse(month.ToString() + year.ToString() + cont.ToString().PadLeft(4, '0')),
                        f_pago = today,
                        monto = emp.MontoTotal,
                        aprobado = false,
                        Fk_Empleado = emp.EmpleadoID,
                        Fk_Nomina = idnom,
                        sueldo = emp.sueldo,
                        SSO = emp.SSO,
                        RPE = emp.RPE,
                        FAOV = emp.FAOV,
                        INCES = emp.INCES,
                        SSO_ap = emp.SSO_ap,
                        RPE_ap = emp.RPE_ap,
                        FAOV_ap = emp.FAOV_ap,
                        INCES_ap = emp.INCES_ap,
                        retroactivos = emp.Retroactivos,
                        prestamos = emp.Prestamos,
                        BonoAlimentacion = emp.BonoAlimentacion
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
                var query = (   from pago in ctx.Pagos
                            join nom in ctx.Nominas on pago.Fk_Nomina equals nom.NominaID
                            where pago.Fk_Empleado == empleadoId && nom.fecha_aprobacion != null && nom.fecha_efectivo <= DateTime.Now
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
                             join emp in ctx.Empleados on pag.Fk_Empleado equals emp.EmpleadoID
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




        /*public static List<Pago> CreatePagos(List<Usuario> usuarios)
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

        }*/

        public static List<Pago> GetPagos(Usuario usuario, DateTime desde, DateTime hasta)
        {
            using (var ctx = new AppDbContext())
            {
                
                var query = (from pago in ctx.Pagos
                             where pago.Fk_Empleado == usuario.EmpleadoID && pago.f_pago <= hasta && pago.f_pago >= desde
                             select pago
                   );
             
                return query.ToList();
            }
        }

    }

    
}