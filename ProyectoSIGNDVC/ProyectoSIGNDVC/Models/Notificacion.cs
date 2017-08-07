using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProyectoSIGNDVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoSIGNDVC
{
    public class Notificacion
    {
        [Key]
        public int NotificacionID { get; set; }
        public String tipo { get; set; }
        public String titulo { get; set; }
        public String descripcion { get; set; }
        public DateTime f_enviado { get; set; }
        public DateTime? f_leido { get; set; }
        public int Fk_Usuario { get; set; }
        public int? Fk_Nomina { get; set; }
        public int? Fk_Pago { get; set; } 
        [ForeignKey("Fk_Usuario")]
        public Usuario Usuario { get; set; }
        [ForeignKey("Fk_Nomina")]
        public Nomina Nomina { get; set; }
        [ForeignKey("Fk_Pago")]
        public Pago Pago { get; set; }


        public static void AddNotificacion(String tipo, String titulo, String descripcion, int tipoId, int usuarioId)
        {
            var notif = new Notificacion()
            {
                tipo = tipo,
                titulo = titulo,
                descripcion = descripcion,
                f_enviado = DateTime.Now,
                f_leido = null,
                Fk_Usuario = usuarioId

            };

            switch (tipo)
            {
                case ("NOMINA"):
                    notif.Fk_Nomina = tipoId;
                break;

                case ("PAGO"):
                    notif.Fk_Pago = tipoId;
                break;

                default:
                    break;
            }

            using (var ctx = new AppDbContext())
            {
                ctx.Notificaciones.Add(notif);
                ctx.SaveChanges();
            }
        }

        public static List<Notificacion> GetAllNotificaciones(int usuarioid)
        {
            using (var ctx = new AppDbContext())
            {
                var query = ( from notif in ctx.Notificaciones
                              where notif.Fk_Usuario == usuarioid
                              orderby notif.f_enviado descending
                              select notif
                    );
                return query.Take(5).ToList();
                //return query.ToList();
            }
        }
   
    }
}