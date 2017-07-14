using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoSIGNDVC
{
    public class Carga : Persona
    {
        [Key]
        public int CargaID{ get; set; }
        public int monto_poliza { get; set; }
        


    }
}