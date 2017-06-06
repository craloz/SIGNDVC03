using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoSIGNDVC.Models
{
    public class Nomina
    {
       
        private DateTime fecha_emision;
        private DateTime fecha_aprobacion;
        private DateTime fecha_efectivo;
        private List<Pago> pagos;
    }
}