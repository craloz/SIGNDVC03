using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProyectoSIGNDVC.Models;

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
        public DateTime f_leido { get; set; }
        public int Fk_Usuario { get; set; }
        public int? Fk_Nomina { get; set; }
        public int? Fk_Pago { get; set; } 
        [ForeignKey("Fk_Usuario")]
        public Usuario Usuario { get; set; }
        [ForeignKey("Fk_Nomina")]
        public Nomina Nomina { get; set; }
        [ForeignKey("Fk_Pago")]
        public Pago Pago { get; set; }
   
    }
}