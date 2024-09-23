using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
