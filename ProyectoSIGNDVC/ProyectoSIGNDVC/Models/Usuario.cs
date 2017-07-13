using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using ProyectoSIGNDVC.Models;
using System.Linq;

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
                var usuario2 = query2.user;
                usuario2.Empleado = query2.emp;
                usuario2.Empleado.Persona = query2.per;
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
        //public Empleado Empleado { get; set; }
    }
}