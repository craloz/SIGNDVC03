using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSIGNDVC.Models
{
    public class Nomina
    {
        [Key]
        public int NominaID { get; set; }
        public DateTime fecha_emision { get; set; }
        public DateTime fecha_aprobacion { get; set; }
        public DateTime fecha_efectivo { get; set; }
        public ICollection<Pago> Pagos { get; set; }
    }
}