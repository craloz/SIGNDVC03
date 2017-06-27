using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyectoSIGNDVC
{
    public class Empleado : Persona
    {
        [Key]
        public int EmpleadoID { get; set; }
        public String cargo { get; set; }
        public int sueldo { get; set; }
        public DateTime f_ingreso { get; set; }
        public DateTime f_salida { get; set; }
        
        public Usuario Usuario { get; set; }
    }
}