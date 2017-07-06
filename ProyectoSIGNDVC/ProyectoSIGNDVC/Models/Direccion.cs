using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace ProyectoSIGNDVC.Models
{
    public class Direccion
    {
        public int DireccionID { get; set; }
        public String nombre { get; set; }
        public String tipo { get; set; }
        public int? Fk_Direccion { get; set; }
        [ForeignKey("Fk_Direccion")]
        public Direccion direccion { get; set; }

        public static int GetDireccionID(String nombre, String tipo)
        {
            using (var ctx = new AppDbContext())
            {
                var Query = (from dir in ctx.Direcciones
                            where dir.tipo == tipo && dir.nombre==nombre
                            select dir.DireccionID).SingleOrDefault();
                return Query;
            }
            return 0;
        }

        public static List<Direccion> GetAllEstadoDireccion()
        {
            using (var ctx = new AppDbContext())
            {

                var Query = from dir in ctx.Direcciones
                               where dir.tipo == "Estado"
                               select dir;
                return Query.ToList();
            }
            return null;
        }

     

        public static int InsertDireccion(String nombre, String tipo,int fk_dir)
        {
            using (var ctx = new AppDbContext())
            {
                Direccion dir = new Direccion { nombre = nombre, tipo = tipo, Fk_Direccion = fk_dir };
                ctx.Direcciones.Add(dir);
                ctx.SaveChanges();
                return dir.DireccionID;
            }
            return 0;
        }
    }
}