using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoSIGNDVC
{
    public class Carga
    {
        [Key]
        public int CargaID { get; set; }
        public int monto_poliza { get; set; }
        public int Fk_Persona { get; set; }
        [ForeignKey("Fk_Persona")]
        public Persona Persona { get; set; }
        public int Fk_Empleado { get; set; }
        [ForeignKey("Fk_Empleado")]
        public Empleado Empleado { get; set; }




    }
}