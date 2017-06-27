using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyectoSIGNDVC
{
    public class Persona
    {
        [Key]
        public int ID { get; set; }
        public String nombre { get; set; }
        public String apellido { get; set; }
        public int cedula { get; set; }
        public Char sexo { get; set; }
        public String direccion { get; set; }
        public DateTime fecha { get; set; }
    }
}