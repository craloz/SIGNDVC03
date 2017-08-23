using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoSIGNDVC.Models
{
    public class Cargo
    {
        public int CargoID { get; set; }
        public String nombre { get; set; }

        public static List<Cargo> GetAllCargo()
        {
            using (var ctx = new AppDbContext())
            {

                var Query = from car in ctx.Cargos
                            select car;
                return Query.ToList();
            }
            return null;
        }

        public static int GetCargoID(String nombre)       
            {
                using (var ctx = new AppDbContext())
                {
                    var Query = (from car in ctx.Cargos
                                 where car.nombre ==nombre
                                 select car.CargoID).FirstOrDefault();
                    return Query;
                }
                return 0;
            }  
    }  
}