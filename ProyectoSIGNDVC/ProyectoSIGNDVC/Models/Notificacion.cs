using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
    }
}