using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace ProyectoSIGNDVC
{
    public class Pago
    {
        [Key]
        public int PagoID { get; set; }
        public int numero_ref { get; set; }
        public DateTime f_pago { get; set; }
        public int monto { get; set; }
        public Boolean aprobado { get; set; }
    }

   
}