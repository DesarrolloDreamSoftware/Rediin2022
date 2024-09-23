using DSEntityNetX.Entities.Connection;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriOperacion;

namespace Rediin2022.Comun.PriOperacion;

public class UtilExpedientesAD
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
}
