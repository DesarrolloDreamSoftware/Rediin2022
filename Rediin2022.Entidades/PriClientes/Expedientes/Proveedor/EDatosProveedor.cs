using DSMetodNetX.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriClientes
{
	public class EDatosProveedor
	{
		public EProveedor Proveedor { get; set; }
		public List<MEReglaNeg> ReglasNegocio { get; set; }
	}
}
