using System;

namespace Rediin2022.Entidades.PriClientes
{
	[Serializable]
	public class EProveedorMedix : EProveedor
	{
		/// <summary>
		/// No. de proveedor
		/// </summary>
		public Int64 ProveedorId { get; set; } //1
		/// <summary>
		/// Nombre / Razón Social
		/// </summary>
		public String NombreORazonSocial { get; set; } //1
		/// <summary>
		/// Modelo
		/// </summary>
		public Int64 ModeloId { get; set; } //0
		/// <summary>
		/// RFC
		/// </summary>
		public String Rfc { get; set; } //1
		/// <summary>
		/// Régimen Fiscal
		/// </summary>
		public Int64 RegimenFiscalId { get; set; } //0
		/// <summary>
		/// País
		/// </summary>
		public Int64 PaisId { get; set; } //1
		/// <summary>
		/// Estado
		/// </summary>
		public Int64 EstadoId { get; set; } //1
		/// <summary>
		/// Alcaldía o Municipio
		/// </summary>
		public String Municipio { get; set; } //0
		/// <summary>
		/// Colonia
		/// </summary>
		public string Colonia { get; set; } //0
		/// <summary>
		/// Calle
		/// </summary>
		public String Calle { get; set; } //1
		/// <summary>
		/// Número
		/// </summary>
		public String Numero { get; set; } //0
		/// <summary>
		/// Código Postal
		/// </summary>
		public String CodigoPostal { get; set; } //1
		/// <summary>
		/// CURP
		/// </summary>
		public string Curp { get; set; } //0

		//public String NumeroExterior { get; set; }
		//public String NumeroInterior { get; set; }
		//public Int64 ColoniaId { get; set; } //1
		//public Int64 MunicipioId { get; set; } //1


		/// <summary>
		/// Nombre de vendedor
		/// </summary>
		public string VendedorNombre { get; set; } //0
		/// <summary>
		/// Teléfono
		/// </summary>
		public String Telefono { get; set; } //0
		/// <summary>
		/// Celular
		/// </summary>
		public String Celular { get; set; } //0
		/// <summary>
		/// Correo eectrónico 1
		/// </summary>
		public string CorreoElectronico1 { get; set; } //0
		/// <summary>
		/// Correo electrónico 2
		/// </summary>
		public string CorreoElectronico2 { get; set; } //0
		/// <summary>
		/// Corres electrónico 3
		/// </summary>
		public string CorreoElectronico3 { get; set; } //0

		//public String ContactoNombre { get; set; }
		//public String ContactoCorreoElectronico { get; set; }
		//public String ContactoTelefono { get; set; }
		//public String ContactoCelular { get; set; }

		/// <summary>
		/// País Banco 1
		/// </summary>
		public String PaisIdBanco1 { get; set; } //0
		/// <summary>
		/// Banco 1
		/// </summary>
		public Int64 BancoId1 { get; set; } //0
		/// <summary>
		/// Cuenta 1
		/// </summary>
		public String Cuenta1 { get; set; } //0
		/// <summary>
		/// CLABE 1
		/// </summary>
		public String CuentaClabe1 { get; set; } //0

		//public Int64 BancoId { get; set; } //1
		//public String Cuenta { get; set; } //1
		//public String CuentaClabe { get; set; } //1

		/// <summary>
		/// Pais Banco 2
		/// </summary>
		public String PaisIdBanco2 { get; set; } //0
		/// <summary>
		/// Banco 2
		/// </summary>
		public Int64 BancoId2 { get; set; } //1
		/// <summary>
		/// Cuenta 2
		/// </summary>
		public String Cuenta2 { get; set; } //1
		/// <summary>
		/// CLABE 2
		/// </summary>
		public String CuentaClabe2 { get; set; } //1


		/// <summary>
		/// Pais Banco 3
		/// </summary>
		public String PaisIdBanco3 { get; set; } //0
		/// <summary>
		/// Banco 3
		/// </summary>
		public Int64 BancoId3 { get; set; } //1
		/// <summary>
		/// Cuenta 3
		/// </summary>
		public String Cuenta3 { get; set; } //1
		/// <summary>
		/// CLABE 3
		/// </summary>
		public String CuentaClabe3 { get; set; } //1

		/// <summary>
		/// Nombre del Notario
		/// </summary>
		public String NotarioNombre { get; set; } //1
		/// <summary>
		/// Número de escritura
		/// </summary>
		public String NumeroEscritura { get; set; } //1
		/// <summary>
		/// Fecha de escritura
		/// </summary>
		public DateTime FechaEscritura { get; set; } //1
		/// <summary>
		/// Representante Legal
		/// </summary>
		public String RepresentanteLegal { get; set; } //1
		/// <summary>
		/// Tipo de identificación
		/// </summary>
		public Int64 IdentificacionId { get; set; } //1
		/// <summary>
		/// Número de Identificación
		/// </summary>
		public String NumIdentificacion { get; set; } //1


		/// <summary>
		/// Nombre del Notario (Poder Notarial)
		/// </summary>
		public String PoderNotarialNotarioNombre { get; set; } //1
		/// <summary>
		/// Número de escritura (Poder Notarial)
		/// </summary>
		public String PoderNotarialNumEscritura { get; set; } //1
		/// <summary>
		/// Fecha de escritura (Poder Notarial)
		/// </summary>
		public DateTime PoderNotarialFechaEscritura { get; set; } //1
		/// <summary>
		/// Representante Legal (Poder Notarial)
		/// </summary>
		public String PoderNotarialRepresentanteLegal { get; set; } //1
		/// <summary>
		/// Tipo de identificación (Poder Notarial)
		/// </summary>
		public Int64 PoderNotarialIdentificacionId { get; set; } //1
		/// <summary>
		/// Número de identificación(Poder Notarial)
		/// </summary>
		public String PoderNotarialNumIdentificacion { get; set; } //1


		/// <summary>
		/// Sociedad
		/// </summary>
		public String SapSociedadId { get; set; } //1
		/// <summary>
		/// Organización de compra
		/// </summary>
		public String SapOrganizacionCompraId { get; set; } //1
		/// <summary>
		/// Moneda
		/// </summary>
		public string MonedaId { get; set; } //0
		/// <summary>
		/// Condición de pago
		/// </summary>
		public String SapCondicionPagoId { get; set; } //1
		/// <summary>
		/// Incoterm
		/// </summary>
		public Int64 IncotermId { get; set; } //0
		/// <summary>
		/// Destino
		/// </summary>
		public string Destino { get; set; } //0
		/// <summary>
		/// Busqueda 1
		/// </summary>
		public string Busqueda1 { get; set; } //0
		/// <summary>
		/// Busqueda 2
		/// </summary>
		public string Busqueda2 { get; set; } //0
		/// <summary>
		/// Número anterior de proveedor
		/// </summary>
		public Int64 ProveedorIdAnt { get; set; } //0

		//public String SapSociedadGLId { get; set; }
		//public String SapGrupoCuentaId { get; set; }
		//public String SapTratamientoId { get; set; }
		//public String SapCuentaAsociadaId { get; set; }
		//public String SapGrupoTesoreriaId { get; set; }
		//public String SapBancoId { get; set; }
		//public String SapViaPagoId { get; set; }
		//public String SapGrupoToleranciaId { get; set; }











	}
}
