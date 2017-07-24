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
        public int sso_aporte { get; set; }
        public int sso_retencion { get; set; }
        public int rpe_aporte { get; set; }
        public int rpe_retencion { get; set; }
        public int faov_aporte { get; set; }
        public int faov_retencion { get; set; }
        public int inces_aporte { get; set; }
        public int inces_retencion { get; set; }
        public int unid_tributaria { get; set; }
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