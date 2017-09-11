using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoSIGNDVC.Models
{
    public class ViewModel
    {
        //public Usuario usuario{get;set;}
        public List<Direccion> direcciones { get; set; }
        public List<Direccion> direcciones2 { get; set; }
        public List<Cargo> cargos { get; set; }
        public List<Usuario> usuarios { get; set; }
        public List<Empleado> empleados { get; set; }
        public int index { get; set; }
        public int nominaId { get; set; }
        public Configuracion configuracion { get; set; }
        public List<Pago> pagos { get; set; }
        public Usuario usuario { get; set; }
        public List<Nomina> nominas { get; set; }
        public List<Notificacion> notificaciones { get; set; }
        public float totalNomina { get; set; }
        public Pago pago { get; set; }
        public string mes {get;set;}

        public Empleado empleado { get; set; }

        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }
        //public Empleado empleado {get; set;}
        //public List<Carga> cargas {get; set;}
        //public Rol rol { get; set; }
    }
}