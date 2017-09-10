using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSIGNDVC.Models
{
    public class Nomina
    {
        [Key]
        public int NominaID { get; set; }
        public DateTime fecha_emision { get; set; }
        public DateTime? fecha_aprobacion { get; set; }
        public DateTime? fecha_efectivo { get; set; }
        public bool enviado { get; set; } = false;
        public ICollection<Pago> Pagos { get; set; }

        public static int GetLastNominaID()
        {
            using (var ctx = new AppDbContext())
            {


                var Query = (from nomina in ctx.Nominas                             
                             orderby nomina.fecha_emision descending
                             select nomina).FirstOrDefault();

                return Query.NominaID;
            };
        }


        public static void AddNomina(DateTime fechaefectivo)
        {
            
            using (var ctx = new AppDbContext())
            {
                Nomina nom = new Nomina()
                {
                    fecha_emision = DateTime.Now,
                    fecha_efectivo = fechaefectivo
                };
                ctx.Nominas.Add(nom);
                ctx.SaveChanges();
                Notificacion.AddNotificacion("NOMINA", Properties.Resources.TituloNuevaNomina, Properties.Resources.DescripcionNuevaNomina, nom.NominaID, Usuario.GetUsuarioDirector().usuarioID);
            };
        }

        public static List<Nomina> GetAllNominas()
        {

            using (var ctx = new AppDbContext())
            {
                var query = (from nomina in ctx.Nominas
                             orderby nomina.fecha_emision ascending
                             select nomina);
                var nominas = new List<Nomina>();
                nominas = query.ToList();
                return nominas;
            }
        }

        public static Nomina GetNomina(int nominaid)
        {
            using (var ctx = new AppDbContext())
            {
                
                var query = (from nomina in ctx.Nominas
                            where nomina.NominaID == nominaid
                             select nomina);
                return query.FirstOrDefault();
            }
        }

        public static void AprobarNomina(int nominaid)
        {
            using (var ctx = new AppDbContext())
            {
                var nomina = GetNomina(nominaid);
                nomina.fecha_aprobacion = DateTime.Now;
                ctx.Entry(nomina).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public static void CambiarStatusEnviadoNomina (int nominaId)
        {
            using (var ctx = new AppDbContext())
            {
                var nomina = GetNomina(nominaId);
                nomina.enviado = true;
                ctx.Entry(nomina).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }



    }
}