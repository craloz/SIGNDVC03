using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace ProyectoSIGNDVC.Models
{
    public class Daemon
    {




        public void CheckNominas()
        {
            while (true)
            {
                using (var ctx = new AppDbContext())
                {                  
                    foreach (var nomina in Nomina.GetAllNominas())
                    {
                        if (nomina.fecha_efectivo <= DateTime.Now && !nomina.enviado /*&& nomina.fecha_aprobacion != null*/)
                        {
                            Correo c = new Correo(Pago.GetAllPagosNomina(nomina.NominaID));
                            c.EnviarCorreoPagos();
                            Nomina.CambiarStatusEnviadoNomina(nomina.NominaID);
                        }
                    }
                }

                Thread.Sleep(1000*5);
            }
        }
    }
}