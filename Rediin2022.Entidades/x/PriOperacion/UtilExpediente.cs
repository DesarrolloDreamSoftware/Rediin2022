using DSEntityNetX.Common.Casting;

using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriOperacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriOperacion;

public static class UtilExpediente
{
    public static String ObtenValorXTipoStr(TiposColumna tipo, IXDataReader dr)
    {
        if (tipo == TiposColumna.Boleano || tipo == TiposColumna.Entero || tipo == TiposColumna.Importe)
        {
            Decimal vValorDec = 0;
            if (!dr.IsNull(nameof(EConExpValores.ValorNumerico)))
                vValorDec = dr.GetDecimal(nameof(EConExpValores.ValorNumerico));

            if (tipo == TiposColumna.Boleano)
                return (vValorDec > 0).ToString();
            else if (tipo == TiposColumna.Entero)
                return String.Format("{0:0}", vValorDec);
            else
                return vValorDec.ToString();
        }
        else if (tipo == TiposColumna.Fecha || tipo == TiposColumna.FechaYHora || tipo == TiposColumna.Hora)
        {
            DateTime vValorFec = DateTime.MinValue;
            if (!dr.IsNull(nameof(EConExpValores.ValorFecha)))
                vValorFec = dr.GetDateTime(nameof(EConExpValores.ValorFecha));

            if (tipo == TiposColumna.Fecha)
                return String.Format("{0:dd/MM/yyyy}", vValorFec);
            else if (tipo == TiposColumna.FechaYHora)
                return String.Format("{0:dd/MM/yyyy HH:mm}", vValorFec);
            else
                return String.Format("{0:HH:mm}", vValorFec);
        }
        else
        {
            String vValorTxt = String.Empty;
            if (!dr.IsNull(nameof(EConExpValores.ValorTexto)))
                vValorTxt = dr.GetString(nameof(EConExpValores.ValorTexto));

            return vValorTxt;
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
    public static ERelacionProcOper ObtenRelacion(List<ERelacionProcOper> relaciones, String propiedad)
    {
        //JRD PENDIENTE 17/9/2024
        return null;
        //return relaciones.FirstOrDefault(e => e.Propiedad == propiedad, new ERelacionProcOper());
    }
}


