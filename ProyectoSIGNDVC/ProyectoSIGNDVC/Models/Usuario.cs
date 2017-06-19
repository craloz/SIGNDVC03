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

        [Required]
        [Display(Name = "Email")]
        public String username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public String clave { get; set; }

    }
}