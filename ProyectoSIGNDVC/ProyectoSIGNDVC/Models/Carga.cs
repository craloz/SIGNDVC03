using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ProyectoSIGNDVC.Models;

namespace ProyectoSIGNDVC
{
    public class Carga
    {
        [Key]
        public int CargaID { get; set; }
        public int monto_poliza { get; set; }
        public int Fk_Persona { get; set; }
        [ForeignKey("Fk_Persona")]
        public Persona Persona { get; set; }
        public int Fk_Empleado { get; set; }
        [ForeignKey("Fk_Empleado")]
        public Empleado Empleado { get; set; }



        public static void DeleteCargasUsuario(String usuario)
        {
            using (var ctx = new AppDbContext())
            {
                Usuario user = Usuario.GetUsuario(usuario);
                var query = (from car in ctx.Cargas
                             where car.Fk_Empleado == user.Empleado.EmpleadoID
                             select car
                            );
                foreach (var carga in query.ToList())
                {
                    ctx.Cargas.Remove(carga);
                    
                }
                ctx.SaveChanges();
            }
        }

        public static void AddCarga(Carga carga)
        {
            using (var ctx = new AppDbContext())
            {
                ctx.Cargas.Add(carga);
                ctx.SaveChanges();
            }
        }
    }
}