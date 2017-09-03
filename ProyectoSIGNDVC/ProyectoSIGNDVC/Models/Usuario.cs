using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using ProyectoSIGNDVC.Models;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoSIGNDVC
{
    public class Usuario
    {
        [Key]
        public int usuarioID { get; set; }
        public String email { get; set; }
        [Required]
        [Display(Name = "Username")]
        public String usuario { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public String clave { get; set; }
        public int EmpleadoID { get; set; }
        public Empleado Empleado { get; set; }


        public static Usuario GetUsuario(String usuario)
        {
            using (var ctx = new AppDbContext())
            {
                var query = (from user in ctx.Usuarios
                             join emp in ctx.Empleados on user.EmpleadoID equals emp.EmpleadoID
                             join per in ctx.Personas on emp.Fk_Persona equals per.PersonaID
                             where user.usuario == usuario
                             select new { per, user, emp });
                var query2 = query.SingleOrDefault();
                if (query2 == null)
                {
                    return null;
                }
                var usuario2 = query2.user;

                usuario2.Empleado = query2.emp;
                usuario2.Empleado.Persona = query2.per;

                var query3 = (from carga in ctx.Cargas
                              where carga.Fk_Empleado == usuario2.Empleado.EmpleadoID
                              join per in ctx.Personas on carga.Fk_Persona equals per.PersonaID
                              select new { carga, per });
                usuario2.Empleado.Cargas = new List<Carga>();
                query3.ToList();
                

                
                var query4 = (from cargo in ctx.Cargos
                              where cargo.CargoID == usuario2.Empleado.Fk_Cargo
                              select cargo
                    );
                usuario2.Empleado.Cargo = query4.FirstOrDefault();
                return usuario2;
            }
        }

        public static String GetUsuarioNombres(String usuario)
        {
            using (var ctx = new AppDbContext())
            {

                var query = (from user in ctx.Usuarios
                             join emp in ctx.Empleados on user.EmpleadoID equals emp.EmpleadoID
                             join per in ctx.Personas on emp.Fk_Persona equals per.PersonaID
                             where user.usuario == usuario
                             select per);
                var query2=query.SingleOrDefault();
                return  query2.nombre+" "+query2.apellido; 
            }
            
        }
        public static List<Usuario> GetAllUsuarios()
        {
            using (var ctx=new AppDbContext())
            {

                var query = (from user in ctx.Usuarios
                             join emp in ctx.Empleados on user.EmpleadoID equals emp.EmpleadoID
                             join per in ctx.Personas on emp.Fk_Persona equals per.PersonaID
                             select new { per, user, emp });
                var usuarios=new List<Usuario>();
                foreach (var item in query)
                {
                    item.emp.Persona = item.per;
                    item.user.Empleado = item.emp;
                    usuarios.Add(item.user);
                }
                return usuarios;
            }

            return null;
        }

        public static bool CheckCredencialesUsuarios(String usuario,String clave )
        {

            using (var ctx = new AppDbContext())
            {
               return ctx.Usuarios.Any(user => user.usuario==usuario && user.clave==clave);
            }
             
        }

        public static void AddUsuario(Usuario usuario)
        {
            using (var ctx = new AppDbContext())
            {
                ctx.Usuarios.Add(usuario);
                ctx.SaveChanges();
            }
        }

       
        public static void DeleteUsuario(string usuario)
        {
            using (var ctx = new AppDbContext())
            {
                Usuario usuariodelete = ctx.Usuarios.First(x => x.usuario == usuario);
                ctx.Usuarios.Remove(usuariodelete);
                ctx.SaveChanges();
            }
        }

        public static Usuario GetUsuario(int empleadoId) {
            using (var ctx = new AppDbContext())
            {
                var query = ( from user in ctx.Usuarios
                              where user.EmpleadoID == empleadoId
                              select user
                            );
                return query.FirstOrDefault();
            }
        }

        public static bool UsuarioIsDirectorEjecutivo(String usuario)
        {
            if (Usuario.GetUsuario(usuario).Empleado.Cargo.CargoID == 1)
            {
                return true;
            }
            return false;
        }

        public static bool UsuarioIsCoordinadoraAdm(String usuario)
        {
            if (Usuario.GetUsuario(usuario).Empleado.Cargo.CargoID == 7)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Usuario GetUsuarioDirector()
        {
            var usuarios = GetAllUsuarios();
            foreach (var user in usuarios)
            {
                if (user.Empleado.Fk_Cargo == 1)
                { 
                    return user;
                }
            }
                return null;
        }

        public static void EditUsuario(Usuario usuario)
        {
            using (var ctx = new AppDbContext())
            {
                ctx.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }
        //public Empleado Empleado { get; set; }
    }
}