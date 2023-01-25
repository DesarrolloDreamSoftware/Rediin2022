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
		public String EstatusNombre { get; set; }
		public String Comentarios { get; set; }

		public Int64 ProveedorId { get; set; }
		public Int64 UsuarioId { get; set; }
		public String NombreORazonSocial { get; set; }
		public String Rfc { get; set; }
		public String CodigoPostal { get; set; }
		public String Calle { get; set; }
		public String NumeroExterior { get; set; }
		public String NumeroInterior { get; set; }
		public Int64 ColoniaId { get; set; }
		public Int64 MunicipioId { get; set; }
		public Int64 EstadoId { get; set; }
		public Int64 PaisId { get; set; }

		public String ContactoNombre { get; set; }
		public String ContactoCorreoElectronico { get; set; }
		public String ContactoTelefono { get; set; }
		public String ContactoCelular { get; set; }

		public Int64 BancoId { get; set; }
		public String Cuenta { get; set; }
		public String CuentaClabe { get; set; }

        public Int64 BancoId2 { get; set; }
        public String Cuenta2 { get; set; }
        public String CuentaClabe2 { get; set; }

        public Int64 BancoId3 { get; set; }
        public String Cuenta3 { get; set; }
        public String CuentaClabe3 { get; set; }

        public String NotarioNombre { get; set; }
		public String NumeroEscritura { get; set; }
		public DateTime FechaEscritura { get; set; }
		public String RepresentanteLegal { get; set; }
		public Int64 IdentificacionId { get; set; }
		public String NumIdentificacion { get; set; }

		public String PoderNotarialNotarioNombre { get; set; }
		public String PoderNotarialNumEscritura { get; set; }
		public DateTime PoderNotarialFechaEscritura { get; set; }
		public String PoderNotarialRepresentanteLegal { get; set; }
		public Int64 PoderNotarialIdentificacionId { get; set; }
		public String PoderNotarialNumIdentificacion { get; set; }

        public String SapSociedadId { get; set; }
        public String SapSociedadGLId { get; set; }
		public String SapGrupoCuentaId { get; set; }
		public String SapOrganizacionCompraId { get; set; }
		public String SapTratamientoId { get; set; }
		public String SapCuentaAsociadaId { get; set; }
		public String SapGrupoTesoreriaId { get; set; }
		public String SapBancoId { get; set; }
		public String SapCondicionPagoId { get; set; }
		public String SapViaPagoId { get; set; }
		public String SapGrupoToleranciaId { get; set; }
    }
}
