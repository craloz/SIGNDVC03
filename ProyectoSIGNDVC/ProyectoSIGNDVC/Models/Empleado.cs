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
    public class Empleado 
    {
        [Key]
        public int EmpleadoID { get; set; }
        public int sueldo { get; set; }
        [Column(TypeName = "Date")]
        public DateTime fecha_ingreso { get; set; }
        public DateTime fecha_salida { get; set; }
        public int Fk_Direccion { get; set; }
        [ForeignKey("Fk_Direccion")]
        public Direccion Direccion { get; set; }
        public int Fk_Persona { get; set; }
        [ForeignKey("Fk_Persona")]
        public Persona Persona { get; set; }
        public int Fk_Cargo { get; set; }
        [ForeignKey("Fk_Cargo")]
        public Cargo Cargo { get; set; }
        //public int Fk_Usuario { get; set; }
        //[ForeignKey("Fk_Usuario")]
        //public Usuario Usuario { get; set; }
    }
}