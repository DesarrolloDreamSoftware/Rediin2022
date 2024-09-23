using DSEntityNetX.Mvc.Session;

namespace RediinProvMedix2022Mvc.Models
{
	public class EVSeguridad(HttpContext context)
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
	}
}
