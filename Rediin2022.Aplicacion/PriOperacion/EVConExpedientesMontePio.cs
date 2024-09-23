using System;

namespace Rediin2022.Aplicacion.PriOperacion;

public class EVConExpedientesMontePio
{
    public Int64 ProcesoOperativoIdProveedor { get; set; }
    //JRD QUITAR public Dictionary<Int64, List<MEElemento>> CombosProveedores { get; set; }
    //public Int64 ParamProveedorColumnaIdPais { get; set; }
    //public Int64 ParamProveedorColumnaIdEstado { get; set; }
    //public Int64 ParamProveedorColumnaIdMunicipio { get; set; }
    //public Int64 ParamProveedorColumnaIdColonia { get; set; }
    public Int64 ParamEstIdCaptura { get; set; }
    public Int64 ParamEstIdAutorizado { get; set; }
    public Int64 ParamColumnaIdNombre { get; set; }
    public Int64 ParamColumnaIdCorreo { get; set; }
    public Int64 ParamPerfilIdNvoUsr { get; set; }
    public Int64 ColumnaIdUsuario { get; set; }
    public String ParamUrlRediinProveedores { get; set; }
}
