using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace ProyectoSIGNDVC
{
    public class Empleado : Persona
    {
        private String cargo;
        private int sueldo;
        private DateTime f_ingreso;
        private DateTime f_salida;
    }
}