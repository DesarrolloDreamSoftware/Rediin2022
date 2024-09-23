using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriClientes;

public class EProveedor : MEntidad
{
	public Int64 ProcesoOperativoId { get; set; }
	/// <summary>
	/// ExpedienteId
	/// </summary>
	public Int64 ExpedienteId { get; set; }
	public Int64 ProcesoOperativoEstId { get; set; } = 0L;
	public String EstatusNombre { get; set; }
	public String Comentarios { get; set; }
	/// <summary>
	/// No. usuario en REDIIN
	/// </summary>
	public Int64 UsuarioId { get; set; }

}
