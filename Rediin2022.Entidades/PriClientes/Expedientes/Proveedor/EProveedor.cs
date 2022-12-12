using DSEntityNetX.DataAccess;
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
		public Int64 ProcesoOperativoId { get; set; }
		public Int64 ExpedienteId { get; set; }
        public Int64 ProcesoOperativoEstId { get; set; } = 0L;
        public Int64 UsuarioId { get; set; }
		public String NombreORazonSocial { get; set; }
		public String Rfc { get; set; }
		public String CodigoPostal { get; set; }
		public String Calle { get; set; }
		public String NumeroExterior { get; set; }
		public String NumeroInterior { get; set; }
		public Int64 ColoniaId { get; set; }
		public String Colonia { get; set; } //Verificar si se queda
		public Int64 MunicipioId { get; set; }
        public String Municipio { get; set; } //Verificar si se queda
        public Int64 EstadoId { get; set; }
        public String Estado { get; set; } //Verificar si se queda
        public Int64 PaisId { get; set; }
        public String Pais { get; set; } //Verificar si se queda

        public String ContactoNombre { get; set; }
		public String ContactoCorreoElectronico { get; set; }
		public String ContactoTelefono { get; set; }
		public String ContactoCelular { get; set; }

		public String BancoId { get; set; }
        public String Banco { get; set; } //Verificar si se queda

        public String Cuenta { get; set; }
		public String CuentaClabe { get; set; }

		public String NotarioNombre { get; set; }
		public String NumeroEscritura { get; set; }
		public DateTime FechaEscritura { get; set; }
		public String RepresentanteLegal { get; set; }
		public String IdentificacionId { get; set; }
        public String Identificacion { get; set; } //Verificar si se queda
		public String NumIdentificacion { get; set; }

		public String PoderNotarialNotarioNombre { get; set; }
		public String PoderNotarialNumEscritura { get; set; }
		public DateTime PoderNotarialFechaEscritura { get; set; }
		public String PoderNotarialRepresentanteLegal { get; set; }
		public String PoderNotarialIdentificacionId { get; set; }
		public String PoderNotarialIdentificacion { get; set; } //Verificar si se queda
        public String PoderNotarialNumIdentificacion { get; set; }
	}
}
