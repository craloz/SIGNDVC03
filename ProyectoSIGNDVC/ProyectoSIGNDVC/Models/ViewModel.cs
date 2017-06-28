﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoSIGNDVC.Models
{
    public class ViewModel
    {
        public Usuario usuario{get;set;}
        public Direccion direccion {get; set;}
        public Empleado empleado {get; set;}
        public List<Carga> cargas {get; set;}
        public Rol rol { get; set; }
    }

 
}