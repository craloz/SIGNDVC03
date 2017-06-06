using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Data.Entity;

namespace ProyectoSIGNDVC
{
    public class Pago
    {
        public int ID { get; set; }
        private int numero_ref;
        private DateTime f_pago;
        private int monto;
        private Boolean aprobado;     
    }

    public class PagoDBContext : DbContext
    {
        public DbSet<Pago> Pagos { get; set; }
    }
}