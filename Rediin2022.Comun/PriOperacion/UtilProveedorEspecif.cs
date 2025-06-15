using DSEntityNetX.Common.Casting;
using DSEntityNetX.Common.Rules;
using DSEntityNetX.Entities.Rules;
using DSMetodNetX.Entidades;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriOperacion;
using Sisegui2020.Entidades.PriSeguridad;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;

namespace Rediin2022.Comun.PriOperacion;

public class UtilProveedorEspecif
{
    public static async Task<Int64> ParamSistemaInt64(INParametrosSistema nParametrosSistema, ProveedorParametrosSistema nombreParametro)
    {
        return XObject.ToInt64((await nParametrosSistema.ParametroSistemaXId(nombreParametro.XDescription(), TiposParametrosSistema.General, 0))?.Valor ?? "0");
    }
    public static void SeparaNombreUsuario(string proveedor, EUsuario usuario)
    {
        String[] vNombresArray = proveedor.Split(" ");
        List<String> vNombres = new List<string>();
        foreach(String vPal in vNombresArray)
        {
            if (vPal.Length > 2)
                vNombres.Add(vPal);
        }

        if (vNombres.Count >= 3)
        {
            usuario.ApellidoMaterno = vNombres[vNombres.Count - 1];
            usuario.ApellidoPaterno = vNombres[vNombres.Count - 2];
            usuario.Nombre = String.Empty;
            for (int i = 0; i < vNombres.Count - 2; i++)
                usuario.Nombre += (i > 0 ? " " : String.Empty) + vNombres[i];

            usuario.Usuario = $"{usuario.Nombre[0]}{usuario.ApellidoPaterno}".ToLower();
        }
        else if (vNombres.Count >= 2)
        {
            usuario.ApellidoMaterno = "S/N.";
            usuario.ApellidoPaterno = vNombres[vNombres.Count - 1];
            usuario.Nombre = String.Empty;
            for (int i = 0; i < vNombres.Count - 1; i++)
                usuario.Nombre += (i > 0 ? " " : String.Empty) + vNombres[i];

            usuario.Usuario = $"{usuario.Nombre[0]}{usuario.ApellidoPaterno}".ToLower();
        }
        else
        {
            usuario.ApellidoPaterno = "S/N.";
            usuario.ApellidoMaterno = "S/N.";
            usuario.Nombre = proveedor;
            usuario.Usuario = $"{usuario.Nombre.Trim().Replace(" ", "")}".ToLower();
        }
    }


    public static void CargaEntidadProveedor(List<EProcesoOperativoCol> colMD,
                                             EConExpediente expediente,
                                             object proveedor)
    {
        Type vProveedorTipo = proveedor.GetType();
        foreach (EProcesoOperativoCol vPOC in colMD)
        {
            PropertyInfo vPI = vProveedorTipo.GetProperty(vPOC.Propiedad);
            if (vPI != null)
                vPI.SetValue(proveedor, UtilExpediente.ObtenValor(colMD, expediente, vPOC.ColumnaId, vPI.PropertyType));
        }
    }

    public static void CargaReglasNegocioProveedor(List<EProcesoOperativoCol> colMD,
                                                   List<MEReglaNeg> reglasNegocio)
    {
        foreach (EProcesoOperativoCol vPOC in colMD)
        {
            IXBusinessRule vRegla =
                XUtilRules.CreateRule<MEReglaNeg>(UtilExpediente.ObtenTipoColumna(vPOC.Tipo));

            vRegla.Property = vPOC.Propiedad;
            XUtilRules.LoadRule(vRegla,
                                vPOC.Etiqueta,
                                vPOC.CapRangoIni,
                                vPOC.CapRangoFin,
                                vPOC.CapObligatorio,
                                (vPOC.Tipo == TiposColumna.Importe ? vPOC.Decimales : 0));

            reglasNegocio.Add((MEReglaNeg)vRegla);
        }
    }

    public static void CargaProveedor(EProveedor vProveedor,
                                      EConExpediente vExpediente,
                                      Int64 procesoOperativoIdProveedor,
                                      Int64 usuarioId,
                                      string comentarios)
    {
        vProveedor.ExpedienteId = vExpediente.ExpedienteId;
        vProveedor.ProcesoOperativoId = procesoOperativoIdProveedor;
        vProveedor.ProcesoOperativoEstId = vExpediente.ProcesoOperativoEstId;
        vProveedor.EstatusNombre = vExpediente.EstatusNombre;
        vProveedor.UsuarioId = usuarioId;
        vProveedor.Comentarios = comentarios;
    }

    public static void CargaExpedienteValores(Object proveedor,
                                              List<EProcesoOperativoColMin> colMD,
                                              List<EExpendienteValor> expedienteValores)
    {
        PropertyInfo[] vPIEnt = proveedor.GetType().GetProperties();
        foreach (EProcesoOperativoColMin vPOC in colMD)
        {
            PropertyInfo vPI = vPIEnt.FirstOrDefault(e => e.Name == vPOC.Propiedad, null);
            if (vPI != null)
            {
                String vValor = String.Empty;
                Object vValorOrg = vPI.GetValue(proveedor);

                if (vValorOrg == null)
                    vValor = String.Empty;
                else if (vValorOrg is DateTime)
                    vValor = String.Format("{0:dd/MM/yyyy HH:mm:ss}", vValorOrg);
                else if (vValorOrg is String)
                    vValor = (String)vValorOrg;
                else
                    vValor = vValorOrg.ToString();

                expedienteValores.Add(new EExpendienteValor()
                {
                    ColumnaId = vPOC.ColumnaId,
                    Valor = vValor
                }); ;
            }
        }
    }
}
