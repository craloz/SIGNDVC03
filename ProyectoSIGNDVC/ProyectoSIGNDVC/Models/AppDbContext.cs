using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ProyectoSIGNDVC.Models
{
    public class AppDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Carga> Cargas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Nomina> Nominas { get; set; }
        public DbSet<Configuracion> Configuraciones { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
    }
}