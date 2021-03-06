﻿using ProyectoSIGNDVC.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoSIGNDVC
{
    public class Persona
    {
        [Key]
        public int PersonaID { get; set; }
        public String nombre { get; set; }
        public String apellido { get; set; }
        public int cedula { get; set; }
        public String sexo { get; set; }
        public DateTime fecha_nacimiento { get; set; }

        public static void EditPersona(Persona persona)
        {
            using (var ctx = new AppDbContext())
            {
                ctx.Entry(persona).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}