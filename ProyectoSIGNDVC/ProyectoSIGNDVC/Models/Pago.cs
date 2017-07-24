using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProyectoSIGNDVC
{
    public class Pago
    {
        [Key]
        public int PagoID { get; set; }
        public int numero_ref { get; set; }
        public DateTime f_pago { get; set; }
        public float monto { get; set; }
        public Boolean aprobado { get; set; }
        public static List<Pago> CreatePagos(List<Usuario> usuarios)
        {
            List<Pago> listaPagos = new List<Pago>();
            foreach (var usuario in usuarios)
            {
                listaPagos.Add(new Pago {
                    numero_ref = 12345,
                    monto = usuario.Empleado.sueldo / 2,
                    f_pago = DateTime.Now,
                    aprobado = false

                });
            }
            return listaPagos;
        }
    }

    

   
}