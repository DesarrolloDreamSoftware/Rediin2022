using DSEntityNetX.Common.Casting;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriOperacion;

namespace Rediin2022.Comun.PriOperacion;

public static class UtilExpediente
{
    public static void EstableceValor(EConExpValores valor, TiposColumna tipo, String cadena)
    {
        if (tipo == TiposColumna.Entero || tipo == TiposColumna.Importe)
            valor.ValorNumerico = XObject.ToDecimal(cadena);
        else if (tipo == TiposColumna.Fecha || tipo == TiposColumna.FechaYHora || tipo == TiposColumna.Hora)
            valor.ValorFecha = XObject.ToDateTime(cadena);
        else if (tipo == TiposColumna.Boleano)
            valor.ValorTexto = (cadena == "true" ? "1" : String.Empty);
        else
            valor.ValorTexto = cadena;
    }
    public static void EstableceValor(List<EConExpValores> valores, Int64 columnaId, TiposColumna tipo, string cadena)
    {
        foreach (var vValor in valores)
        {
            if (vValor.ColumnaId == columnaId)
            {
                EstableceValor(vValor, TiposColumna.Entero, cadena);
                break;
            }
        }
    }
    public static Object ObtenValor(List<EProcesoOperativoCol> procesoOperativoCols,
                                    EConExpediente conExpediente,
                                    Int64 columnaId)
    {
        foreach (EProcesoOperativoCol vCol in procesoOperativoCols)
        {
            if (vCol.ColumnaId == columnaId)
                return ObtenValor(conExpediente.Valores, vCol.Tipo, columnaId);
        }

        return null;
    }
    public static Object ObtenValor(List<EConExpValores> valores,
                                    TiposColumna tipo,
                                    Int64 columnaId)
    {
        foreach (EConExpValores vVal in valores)
        {
            if (vVal.ColumnaId == columnaId)
            {
                if (tipo == TiposColumna.Importe ||
                    tipo == TiposColumna.Entero)
                    return vVal.ValorNumerico;
                else if (tipo == TiposColumna.Boleano)
                    return vVal.ValorNumerico == 1;
                else if (tipo == TiposColumna.Fecha ||
                         tipo == TiposColumna.FechaYHora ||
                         tipo == TiposColumna.Hora)
                    return vVal.ValorFecha;
                else
                    return vVal.ValorTexto ?? String.Empty;
            }
        }

        return null;
    }
    public static Object ObtenValor(List<EProcesoOperativoCol> procesoOperativoCols,
                                    EConExpediente conExpediente,
                                    Int64 columnaId,
                                    Type tipoVal)
    {
        foreach (EProcesoOperativoCol vCol in procesoOperativoCols)
        {
            if (vCol.ColumnaId == columnaId)
                return ObtenValor(conExpediente.Valores, vCol.Tipo, columnaId, tipoVal);
        }

        return null;
    }
    public static Object ObtenValor(List<EConExpValores> valores,
                                    TiposColumna tipo,
                                    Int64 columnaId,
                                    Type tipoVal)
    {
        foreach (EConExpValores vVal in valores)
        {
            if (vVal.ColumnaId == columnaId)
            {
                if (tipo == TiposColumna.Importe ||
                tipo == TiposColumna.Entero)
                    return Convert.ChangeType(vVal.ValorNumerico, tipoVal);
                else if (tipo == TiposColumna.Boleano)
                    return vVal.ValorNumerico == 1;
                else if (tipo == TiposColumna.Fecha ||
                         tipo == TiposColumna.FechaYHora ||
                         tipo == TiposColumna.Hora)
                    return vVal.ValorFecha;
                else
                    return vVal.ValorTexto ?? String.Empty;
            }
        }

        return null;
    }
    //public static EProcesoOperativoColMin ObtenRelacion(List<EProcesoOperativoColMin> relaciones, String propiedad)
    //{
    //    return relaciones.FirstOrDefault(e => e.Propiedad == propiedad, new EProcesoOperativoColMin());
    //}
    public static EProcesoOperativoCol ObtenRelacion(List<EProcesoOperativoCol> relaciones, String propiedad)
    {
        return relaciones.FirstOrDefault(e => e.Propiedad == propiedad, new EProcesoOperativoCol());
    }

    public static Type ObtenTipoColumna(TiposColumna tipo)
    {
        if (tipo == TiposColumna.Boleano)
            return typeof(Boolean);
        else if (tipo == TiposColumna.Entero)
            return typeof(Int64);
        else if (tipo == TiposColumna.Fecha || tipo == TiposColumna.Hora || tipo == TiposColumna.FechaYHora)
            return typeof(DateTime);
        else if (tipo == TiposColumna.Importe)
            return typeof(Decimal);
        else
            return typeof(String);
    }
}


