using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSIGNDVC.DataAccess
{
    interface IDAO
    {
        /// <summary>
        /// Agregar, Realiza una insercion en BD
        /// </summary>
        /// <param name="e"> recibe una Entidad</param>
        /// <returns>retorna la Clave primaria de la entidad que agrego.</returns>
        int Agregar(Entidad e);
        /// <summary>
        /// Modificar, Reliza una actualizacion a un registro en BD
        /// </summary>
        /// <param name="e">Recibe la entidad que va a actualizar</param>
        /// <returns>Retorna la entidad Actualizada</returns>
        Entidad Modificar(Entidad e);
        /// <summary>
        /// Consultar, devuelve un registro unico de la BD
        /// </summary>
        /// <param name="id">Recibe el Id de la entidad que va a retornar</param>
        /// <returns>Retorna la entidad encontrada en Bd</returns>
        Entidad Consultar(int id);
        /// <summary>
        /// Consultar todos los registros de una tabla en BD
        /// </summary>
        /// <returns>Devuelve un Map, indicando todos los registros de una tabla de BD</returns>
        Dictionary<int, Entidad> ConsultarTodos();
    }
}
