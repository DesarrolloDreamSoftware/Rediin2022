using DSMetodNetX.Entidades;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriClientes
{
    public class EDatosProveedor
    {
        /// <summary>
        /// JSON Que es el proveedor hay que deserealizar
        /// </summary>
        public string Proveedor { get; set; } = string.Empty;
        public List<MEReglaNeg> ReglasNegocio { get; set; }
    }
}
