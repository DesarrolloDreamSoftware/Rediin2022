using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriClientes
{
	[Serializable]
	public class EProveedorDocumentacion : MEntidad
	{
		public String Documento { get; set; }
		public String Archivo { get; set; }
		public DateTime FechaCarga { get; set; }
	}
}
