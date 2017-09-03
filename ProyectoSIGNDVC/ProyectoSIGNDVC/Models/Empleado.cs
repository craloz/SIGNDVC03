using System;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProyectoSIGNDVC.Models;
using System.Collections.Generic;

namespace ProyectoSIGNDVC
{
    public class Empleado 
    {
        [Key]
        public int EmpleadoID { get; set; }
        public String Codigo { get; set; }
        public float sueldo { get; set; }
        [Column(TypeName = "Date")]
        public DateTime fecha_ingreso { get; set; }
        public DateTime fecha_salida { get; set; }
        public String Banco { get; set; }
        public String N_Cuenta { get; set; }
        public int Fk_Direccion { get; set; }
        [ForeignKey("Fk_Direccion")]
        public Direccion Direccion { get; set; }
        public int Fk_Persona { get; set; }
        [ForeignKey("Fk_Persona")]
        public Persona Persona { get; set; }
        public int Fk_Cargo { get; set; }
        [ForeignKey("Fk_Cargo")]
        public Cargo Cargo { get; set; }
        public ICollection<Carga> Cargas { get; set; }
        public ICollection<Pago> Pagos { get; set; }
        [NotMapped]
        public float SSO { get; set; }
        [NotMapped]
        public float RPE { get; set; }
        [NotMapped]
        public float FAOV { get; set; }
        [NotMapped]
        public float INCES { get; set; }
        [NotMapped]
        public float Retenciones { get; set; }
        [NotMapped]
        public float SSO_ap { get; set; }
        [NotMapped]
        public float RPE_ap { get; set; }
        [NotMapped]
        public float FAOV_ap { get; set; }
        [NotMapped]
        public float INCES_ap { get; set; }
        [NotMapped]
        public float Aportes { get; set; }
        [NotMapped]
        public float BonoAlimentacion { get; set; }
        [NotMapped]
        public float costoCargas { get; set; }
        [NotMapped]
        public float MontoTotal { get; set; }







        public static float getCostoCargas(int idEmpleado)
        {
            using (var ctx = new AppDbContext())
            {
                float total = 0;
                List<Carga> listCarga = new List<Carga>();
                var query3 = (from carga in ctx.Cargas
                              where carga.Fk_Empleado == idEmpleado
                              join per in ctx.Personas on carga.Fk_Persona equals per.PersonaID
                              select new { carga, per });
                
                foreach (var item in query3.ToList())
                {                    
                    listCarga.Add(item.carga);
                }
                foreach (var carga in listCarga)
                {
                    total = carga.monto_poliza + total;
                }


                return total;
            }
            
        }

        public static int calcularLunes()
        {
            DateTime thisMonth= DateTime.Now;
            int mondays = 0;
            int day = thisMonth.Day;
            int month = thisMonth.Month;
            int year = thisMonth.Year;
            int daysThisMonth = DateTime.DaysInMonth(year, month);
            DateTime beginingOfThisMonth = new DateTime(year, month, 1);
            for (int i = 0; i < daysThisMonth; i++)
                if (beginingOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Monday)
                    mondays++;
            return mondays;
        }

        public static float calcularSalarioIntegral(float sueldoBase)
        {
            float alicBonoVac = (sueldoBase * (15 / 12 / 30));
            float alicUtil = (sueldoBase * (60 / 360));
            return sueldoBase + alicBonoVac + alicUtil;
        }

        public static List<Empleado> calcularSalario() {
            using (var ctx = new AppDbContext())
            {
                DateTime today = DateTime.Now;                
                int day = today.Day;                
                List<Empleado> listEmp = new List<Empleado>();
                Configuracion conf = new Configuracion();
                conf = Configuracion.GetLastConfiguracion();
                var empleado = (from emp in ctx.Empleados
                                join per in ctx.Personas on emp.Fk_Persona equals per.PersonaID
                                select new { per, emp });
                foreach (var empl in empleado.ToList())
                {
                    Empleado em = new Empleado();
                    em.EmpleadoID = empl.emp.EmpleadoID;
                    em.Persona = empl.per;
                    em.sueldo = empl.emp.sueldo;
                    em.SSO =( (((empl.emp.sueldo*12)/ 52) * (conf.sso_retencion/100)) * calcularLunes());
                    em.RPE = ((((empl.emp.sueldo * 12) / 52) * (conf.rpe_retencion / 100)) * calcularLunes());
                    em.FAOV = ( calcularSalarioIntegral(empl.emp.sueldo) * (conf.faov_retencion/100) );
                    em.INCES = (((empl.emp.sueldo * (60 / 360))*12)*(conf.inces_retencion/100));
                    em.SSO_ap = ((((empl.emp.sueldo * 12) / 52) * (conf.sso_aporte / 100)) * calcularLunes());
                    em.RPE_ap = ((((empl.emp.sueldo * 12) / 52) * (conf.rpe_aporte / 100)) * calcularLunes());
                    em.FAOV_ap = (calcularSalarioIntegral(empl.emp.sueldo) * (conf.faov_aporte / 100));
                    em.INCES_ap = (((empl.emp.sueldo * (60 / 360)) * 12) * (conf.inces_aporte / 100));
                    if (day > 25)
                    {
                        em.BonoAlimentacion = (conf.bonoalimentacion * conf.unid_tributaria * 30);
                    }
                    else
                    {
                        em.BonoAlimentacion = 0;
                    }                    
                    em.Retenciones = em.SSO + em.RPE + em.FAOV + em.INCES;
                    em.Aportes = em.SSO_ap + em.RPE_ap + em.FAOV_ap + em.INCES_ap;
                    em.costoCargas = (float) ((getCostoCargas(empl.emp.EmpleadoID) * (0.3))/12);
                    em.MontoTotal = ((em.sueldo / 2) - (em.Retenciones / 2) - (em.costoCargas) + (em.BonoAlimentacion));
                    listEmp.Add(em);

                }
                return listEmp;
            }
            
            
        }


        public static List<Empleado> calcularSalarioByNomina(int idNomina)
        {
            using (var ctx = new AppDbContext())
            {
                DateTime today = DateTime.Now;
                int day = today.Day;
                List<Empleado> listEmp = new List<Empleado>();
                Configuracion conf = new Configuracion();
                conf = Configuracion.GetLastConfiguracion();
                var empleado = (from pag in ctx.Pagos
                                join emp in ctx.Empleados on pag.Fk_Empleado equals emp.EmpleadoID
                                join per in ctx.Personas on emp.Fk_Persona equals per.PersonaID
                                where pag.Fk_Nomina == idNomina
                                select new { per, emp });
                foreach (var empl in empleado.ToList())
                {
                    Empleado em = new Empleado();
                    em.EmpleadoID = empl.emp.EmpleadoID;
                    em.Persona = empl.per;
                    em.sueldo = empl.emp.sueldo;
                    em.SSO = ((((empl.emp.sueldo * 12) / 52) * (conf.sso_retencion / 100)) * calcularLunes());
                    em.RPE = ((((empl.emp.sueldo * 12) / 52) * (conf.rpe_retencion / 100)) * calcularLunes());
                    em.FAOV = (calcularSalarioIntegral(empl.emp.sueldo) * (conf.faov_retencion / 100));
                    em.INCES = (((empl.emp.sueldo * (60 / 360)) * 12) * (conf.inces_retencion / 100));
                    em.SSO_ap = ((((empl.emp.sueldo * 12) / 52) * (conf.sso_aporte / 100)) * calcularLunes());
                    em.RPE_ap = ((((empl.emp.sueldo * 12) / 52) * (conf.rpe_aporte / 100)) * calcularLunes());
                    em.FAOV_ap = (calcularSalarioIntegral(empl.emp.sueldo) * (conf.faov_aporte / 100));
                    em.INCES_ap = (((empl.emp.sueldo * (60 / 360)) * 12) * (conf.inces_aporte / 100));
                    if (day > 25)
                    {
                        em.BonoAlimentacion = (conf.bonoalimentacion * conf.unid_tributaria * 30);
                    }
                    else
                    {
                        em.BonoAlimentacion = 0;
                    }
                    em.Retenciones = em.SSO + em.RPE + em.FAOV + em.INCES;
                    em.Aportes = em.SSO_ap + em.RPE_ap + em.FAOV_ap + em.INCES_ap;
                    em.costoCargas = (float)((getCostoCargas(empl.emp.EmpleadoID) * (0.3)) / 12);
                    em.MontoTotal = ((em.sueldo / 2) - (em.Retenciones / 2) - (em.costoCargas) + (em.BonoAlimentacion));
                    listEmp.Add(em);

                }
                return listEmp;
            }


        }


        public static Empleado calcularSalarioByEmp(int idEmpleado)
        {
            using (var ctx = new AppDbContext())
            {
                DateTime today = DateTime.Now;
                int day = today.Day;                
                Configuracion conf = new Configuracion();
                conf = Configuracion.GetLastConfiguracion();
                Empleado em = new Empleado();
                var empleado = (from emp in ctx.Empleados
                                where emp.EmpleadoID == idEmpleado
                                join per in ctx.Personas on emp.Fk_Persona equals per.PersonaID
                                select new { per, emp });
                foreach (var empl in empleado.ToList()) { 
                
                    
                    em.EmpleadoID = empl.emp.EmpleadoID;
                    em.Persona = empl.per;
                    em.sueldo = empl.emp.sueldo;
                    em.SSO = ((((empl.emp.sueldo * 12) / 52) * (conf.sso_retencion / 100)) * calcularLunes());
                    em.RPE = ((((empl.emp.sueldo * 12) / 52) * (conf.rpe_retencion / 100)) * calcularLunes());
                    em.FAOV = (calcularSalarioIntegral(empl.emp.sueldo) * (conf.faov_retencion / 100));
                    em.INCES = (((empl.emp.sueldo * (60 / 360)) * 12) * (conf.inces_retencion / 100));
                    em.SSO_ap = ((((empl.emp.sueldo * 12) / 52) * (conf.sso_aporte / 100)) * calcularLunes());
                    em.RPE_ap = ((((empl.emp.sueldo * 12) / 52) * (conf.rpe_aporte / 100)) * calcularLunes());
                    em.FAOV_ap = (calcularSalarioIntegral(empl.emp.sueldo) * (conf.faov_aporte / 100));
                    em.INCES_ap = (((empl.emp.sueldo * (60 / 360)) * 12) * (conf.inces_aporte / 100));
                    if (day > 25)
                    {
                        em.BonoAlimentacion = (conf.bonoalimentacion * conf.unid_tributaria * 30);
                    }
                    else
                    {
                        em.BonoAlimentacion = 0;
                    }
                    em.Retenciones = em.SSO + em.RPE + em.FAOV + em.INCES;
                    em.Aportes = em.SSO_ap + em.RPE_ap + em.FAOV_ap + em.INCES_ap;
                    em.costoCargas = (float)((getCostoCargas(empl.emp.EmpleadoID) * (0.3)) / 12);
                    em.MontoTotal = ((em.sueldo / 2) - (em.Retenciones / 2) - (em.costoCargas) + (em.BonoAlimentacion));
                    

                 }
                
                return em;
            }


        }

        public static void EditEmpleado(Empleado empleado)
        {
            using (var ctx = new AppDbContext())
            {
                ctx.Entry(empleado).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }



        //public int Fk_Usuario { get; set; }
        //[ForeignKey("Fk_Usuario")]
        //public Usuario Usuario { get; set; }
    }



}