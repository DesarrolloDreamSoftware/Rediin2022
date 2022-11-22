using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriClientes
{
	[Serializable]
	public class EProveedor : MEntidad
	{
		public String NombreORazonSocial { get; set; }
		public String Rfc { get; set; }
		public Int32 CodigoPostal { get; set; }
		public String Calle { get; set; }
		public String NumeroExterior { get; set; }
		public String NumeroInterior { get; set; }
		public String ColoniaId { get; set; }
		public String MunicipioId { get; set; }
		public String EstadoId { get; set; }
		public String PaisId { get; set; }

		public String ContactoNombre { get; set; }
		public String ContactoCorreoElectronico { get; set; }
		public String ContactoTelefono { get; set; }
		public String ContactoCelular { get; set; }

		public String BancoId { get; set; }
		public String Cuenta { get; set; }
		public String CuentaClabe { get; set; }

		public String NotarioNombre { get; set; }
		public Int64 NumeroEscritura { get; set; }
		public DateTime FechaEscritura { get; set; }
		public String RepresentanteLegal { get; set; }
		public String IdentificacionId { get; set; }
		public String NumIdentificacion { get; set; }

		public String PoderNotarialNotarioNombre { get; set; }
		public String PoderNotarialNumEscritura { get; set; }
		public String PoderNotarialFechaEscritura { get; set; }
		public String PoderNotarialRepresentanteLegal { get; set; }
		public String PoderNotarialIdentificacionId { get; set; }
		public String PoderNotarialNumIdentificacion { get; set; }
	}
}
