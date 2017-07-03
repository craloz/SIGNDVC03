using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoSIGNDVC.DataAccess
{
    public class Entidad
    {
        public int _id { get; set; }

        public Entidad() { }

        public Entidad(int id) { this._id = id; }
    }
}