using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoSIGNDVC
{
    public class Rol
    {
        [Key]
        public int RolID{get;set;}
        public String nombre { get; set; }
    }
}