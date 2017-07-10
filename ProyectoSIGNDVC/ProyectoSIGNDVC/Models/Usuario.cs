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


        //public Empleado Empleado { get; set; }
    }
}