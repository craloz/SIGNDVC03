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
        public float Retroactivos{ get; set; }
        [NotMapped]
        public float Prestamos { get; set; }
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

        public static List<Empleado> GetEmpleados()
        {
            using (var ctx = new AppDbContext())
            {
                var query = (from emp in ctx.Empleados                             
                             select emp
                    );
                return query.ToList();
            }

        }


        public static Empleado GetEmpleado(int idempleado)
        {
            using (var ctx = new AppDbContext())
            {
                var query = (from emp in ctx.Empleados
                             where emp.EmpleadoID == idempleado
                             join per in ctx.Personas on emp.Fk_Persona equals per.PersonaID
                             select new { emp, per });
                var query2 = query.SingleOrDefault();
                if (query2 == null)
                {
                    return null;
                }
                Empleado emple = new Empleado();
                emple = query2.emp;
                emple.Persona = query2.per;

                return emple;


            }

        }


        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }


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
            int testera = mondays / 2;
            return mondays/2;
        }

        public static float calcularSalarioIntegral(float sueldoBase, DateTime date)
        {
            float vacation_days = (float)(15.0+CalculateAge(date));
            float i = (float)(vacation_days / 12.0 / 30.0);
            float alicBonoVac = (sueldoBase * i);
            i = (float)(60.0 / 360.0);
            float alicUtil = (sueldoBase * i);
            var testera = sueldoBase + alicBonoVac + alicUtil;
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
                    em.FAOV = ( calcularSalarioIntegral(empl.emp.sueldo, empl.emp.fecha_ingreso) * (conf.faov_retencion/100)/2 );                    
                    em.INCES = (((empl.emp.sueldo * 60 * 12) / 360) * (conf.inces_retencion / 100));
                    em.SSO_ap = ((((empl.emp.sueldo * 12) / 52) * (conf.sso_aporte / 100)) * calcularLunes());
                    em.RPE_ap = ((((empl.emp.sueldo * 12) / 52) * (conf.rpe_aporte / 100)) * calcularLunes());
                    em.FAOV_ap = (calcularSalarioIntegral(empl.emp.sueldo, empl.emp.fecha_ingreso) * (conf.faov_aporte / 100));
                    em.INCES_ap = (((empl.emp.sueldo * (60 / 360)) * 12) * (conf.inces_aporte / 100));
                    if (day > 15)
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
                    em.MontoTotal = ((em.sueldo / 2) - (em.Retenciones) - (em.costoCargas) + (em.BonoAlimentacion));
                    listEmp.Add(em);

                }
                return listEmp;
            }
            
            
        }

        public static List<Empleado> calcularSalario(int retroactivo, int prestamo)
        {
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
                    em.SSO = ((((empl.emp.sueldo * 12) / 52) * (conf.sso_retencion / 100)) * calcularLunes());
                    em.RPE = ((((empl.emp.sueldo * 12) / 52) * (conf.rpe_retencion / 100)) * calcularLunes());
                    em.FAOV = (calcularSalarioIntegral(empl.emp.sueldo, empl.emp.fecha_ingreso) * (conf.faov_retencion / 100)/2);
                    em.INCES = (((empl.emp.sueldo * 60 * 12) / 360) * (conf.inces_retencion / 100));
                    em.SSO_ap = ((((empl.emp.sueldo * 12) / 52) * (conf.sso_aporte / 100)) * calcularLunes());
                    em.RPE_ap = ((((empl.emp.sueldo * 12) / 52) * (conf.rpe_aporte / 100)) * calcularLunes());
                    em.FAOV_ap = (calcularSalarioIntegral(empl.emp.sueldo, empl.emp.fecha_ingreso) * (conf.faov_aporte / 100));
                    em.INCES_ap = (((empl.emp.sueldo * (60 / 360)) * 12) * (conf.inces_aporte / 100));
                    if (day > 15)
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
                    em.MontoTotal = ((em.sueldo / 2) - (em.Retenciones) - (em.costoCargas) + (em.BonoAlimentacion) - retroactivo - prestamo);
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
                    em.FAOV = (calcularSalarioIntegral(empl.emp.sueldo, empl.emp.fecha_ingreso) * (conf.faov_retencion / 100)/2);
                    em.INCES = (((empl.emp.sueldo * 60 * 12) / 360) * (conf.inces_retencion / 100));
                    em.SSO_ap = ((((empl.emp.sueldo * 12) / 52) * (conf.sso_aporte / 100)) * calcularLunes());
                    em.RPE_ap = ((((empl.emp.sueldo * 12) / 52) * (conf.rpe_aporte / 100)) * calcularLunes());
                    em.FAOV_ap = (calcularSalarioIntegral(empl.emp.sueldo, empl.emp.fecha_ingreso) * (conf.faov_aporte / 100));
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
                    em.MontoTotal = ((em.sueldo / 2) - (em.Retenciones ) - (em.costoCargas) + (em.BonoAlimentacion));
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
                    em.Banco = empl.emp.Banco;
                    em.N_Cuenta = empl.emp.N_Cuenta;
                    em.sueldo = empl.emp.sueldo;
                    em.SSO = ((((empl.emp.sueldo * 12) / 52) * (conf.sso_retencion / 100)) * calcularLunes());
                    em.RPE = ((((empl.emp.sueldo * 12) / 52) * (conf.rpe_retencion / 100)) * calcularLunes());
                    em.FAOV = (calcularSalarioIntegral(empl.emp.sueldo, empl.emp.fecha_ingreso) * (conf.faov_retencion / 100)/2);
                    em.INCES = (((empl.emp.sueldo * 60 * 12) / 360) * (conf.inces_retencion / 100));
                    em.SSO_ap = ((((empl.emp.sueldo * 12) / 52) * (conf.sso_aporte / 100)) * calcularLunes());
                    em.RPE_ap = ((((empl.emp.sueldo * 12) / 52) * (conf.rpe_aporte / 100)) * calcularLunes());
                    em.FAOV_ap = (calcularSalarioIntegral(empl.emp.sueldo, empl.emp.fecha_ingreso) * (conf.faov_aporte / 100));
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
                    em.MontoTotal = ((em.sueldo / 2) - (em.Retenciones ) - (em.costoCargas) + (em.BonoAlimentacion));
                    

                 }
                
                return em;
            }


        }



        public static Empleado calcularSalarioByEmp(int idEmpleado, float retroactivo, float prestamo)
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
                foreach (var empl in empleado.ToList())
                {
                    em.EmpleadoID = empl.emp.EmpleadoID;
                    em.Persona = empl.per;
                    em.sueldo = empl.emp.sueldo;
                    em.Retroactivos = retroactivo;
                    em.Prestamos = prestamo;
                    em.SSO = ((((empl.emp.sueldo * 12) / 52) * (conf.sso_retencion / 100)) * calcularLunes());
                    em.RPE = ((((empl.emp.sueldo * 12) / 52) * (conf.rpe_retencion / 100)) * calcularLunes());
                    em.FAOV = (calcularSalarioIntegral(empl.emp.sueldo, empl.emp.fecha_ingreso) * (conf.faov_retencion / 100)/2);
                    em.INCES = (((empl.emp.sueldo * (60 / 360)) * 12) * (conf.inces_retencion / 100));
                    em.SSO_ap = ((((empl.emp.sueldo * 12) / 52) * (conf.sso_aporte / 100)) * calcularLunes());
                    em.RPE_ap = ((((empl.emp.sueldo * 12) / 52) * (conf.rpe_aporte / 100)) * calcularLunes());
                    em.FAOV_ap = (calcularSalarioIntegral(empl.emp.sueldo, empl.emp.fecha_ingreso) * (conf.faov_aporte / 100));
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
                    em.MontoTotal = ((em.sueldo / 2) - (em.Retenciones) - (em.costoCargas) + (em.BonoAlimentacion) + retroactivo - prestamo);


                }

                return em;
            }


        }

        public static void EditEmpleado(Empleado empleado)
        {
            using (var ctx = new AppDbContext())
            {
                ctx.Empleados.Attach(empleado);
                var entry = ctx.Entry(empleado);
                entry.Property(e => e.sueldo).IsModified = true;
                //ctx.Entry(empleado).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }



        //public int Fk_Usuario { get; set; }
        //[ForeignKey("Fk_Usuario")]
        //public Usuario Usuario { get; set; }
    }



}