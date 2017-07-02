using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        //public Empleado Empleado { get; set; }
    }
}