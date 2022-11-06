using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Rediin2022.Entidades.Idioma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022.Negocio
{
    public class Validaciones : MValidaciones
    {
        #region Funciones
        /// <summary>
        /// Crea un objeto de negocio para facilitar el agregar las reglas.
        /// </summary>
        /// <typeparam name="tEntidad"></typeparam>
        /// <param name="mensajes"></param>
        /// <returns></returns>
        public static MReglasNeg<tEntidad> CreaReglasNeg<tEntidad>(IMMensajes mensajes)
        {
            return new MReglasNeg<tEntidad>(mensajes, MensajesXId.ResourceManager);
        }
        #endregion
    }
}
