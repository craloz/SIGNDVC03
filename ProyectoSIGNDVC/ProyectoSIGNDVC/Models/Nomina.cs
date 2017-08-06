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
            };
        
        
            
        }
    }
}