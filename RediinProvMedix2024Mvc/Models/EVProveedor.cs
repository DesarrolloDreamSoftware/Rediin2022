using DSEntityNetX.Mvc.Session;
using DSMetodNetX.Entidades;
using Microsoft.AspNetCore.Http;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriOperacion;

namespace RediinProvMedix2022Mvc.Models
{
	public class EVProveedor(HttpContext context)
	{
		public Int64 UsuarioId
		{
			get { return context.Session.GetInt64("UsuarioId"); }
			set { context.Session.SetInt64("UsuarioId", value); }
		}
		public Int64 EstablecimientoId
		{
			get { return context.Session.GetInt64("EstablecimientoId"); }
			set { context.Session.SetInt64("EstablecimientoId", value); }
		}
		public String EstablecimientoNombre
		{
			get { return context.Session.GetString("EstablecimientoNombre"); }
			set { context.Session.SetString("EstablecimientoNombre", value); }
		}
		public String EstablecimientoRfc
		{
			get { return context.Session.GetString("EstablecimientoRfc"); }
			set { context.Session.SetString("EstablecimientoRfc", value); }
		}
		public Int64 ProcOper
		{
			get { return context.Session.GetInt64("ProcOper"); }
			set { context.Session.SetInt64("ProcOper", value); }
		}
		public Int64 EstatusIdCaptura
		{
			get { return context.Session.GetInt64("EstatusIdCaptura"); }
			set { context.Session.SetInt64("EstatusIdCaptura", value); }
		}
		public Int64 EstatusIdRevision
		{
			get { return context.Session.GetInt64("EstatusIdRevision"); }
			set { context.Session.SetInt64("EstatusIdRevision", value); }
		}
		public EProveedorMedix Proveedor
		{
			get { return context.Session.GetEntity<EProveedorMedix>("Proveedor"); }
			set { context.Session.SetEntity("Proveedor", value); }
		}
		public List<MEReglaNeg> ProveedorReglas
		{
			get { return context.Session.GetEntity<List<MEReglaNeg>>("ProveedorReglas"); }
			set { context.Session.SetEntity("ProveedorReglas", value); }
		}
		public EConExpedienteObjetoPag Objetos
		{
			get { return context.Session.GetEntity<EConExpedienteObjetoPag>("Objetos"); }
			set { context.Session.SetEntity("Objetos", value); }
		}
		public Int32 Indice
		{
			get { return context.Session.GetInt32("Indice") ?? -1; }
			set { context.Session.SetInt32("Indice", value); }
		}
	}
}
