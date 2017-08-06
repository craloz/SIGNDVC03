using System;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ProyectoSIGNDVC.Models;

namespace ProyectoSIGNDVC
{
    public class Configuracion
    {[Key]
        public int ConfiguracionID { get; set; }
        public float sso_aporte { get; set; }
        public float sso_retencion { get; set; }
        public float rpe_aporte { get; set; }
        public float rpe_retencion { get; set; }
        public float faov_aporte { get; set; }
        public float faov_retencion { get; set; }
        public float inces_aporte { get; set; }
        public float inces_retencion { get; set; }
        public float unid_tributaria { get; set; }
        public float bonoalimentacion { get; set; }
        public DateTime fecha_inicio_config { get; set; }
        public int? fecha_fin_config { get; set; }

        public static void AddConfiguracion(Configuracion cf)
        {
            using (var ctx = new AppDbContext())
            {
                ctx.Configuraciones.Add(cf);
                ctx.SaveChanges();
            };
        }

        public static Configuracion GetLastConfiguracion()
        {
            using (var ctx = new AppDbContext())
            {
                var Query = (from conf in ctx.Configuraciones
                             where conf.fecha_fin_config == null
                             orderby conf.fecha_inicio_config descending
                             select conf).FirstOrDefault();
                return Query;
            };
        }

       
    }
}