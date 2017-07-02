using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoSIGNDVC.Models
{
    public class Direccion
    {
        public int DireccionID { get; set; }
        public String nombre { get; set; }
        public String tipo { get; set; }
        public int? Fk_Direccion { get; set; }
        [ForeignKey("Fk_Direccion")]
        public Direccion direccion { get; set; }
    }
}