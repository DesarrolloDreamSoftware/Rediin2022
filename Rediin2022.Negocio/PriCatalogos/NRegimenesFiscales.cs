using DSEntityNetX.Common.Casting;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Comun;
using DSMetodNetX.Entidades;
using DSMetodNetX.Entidades.Idioma;
using Rediin2022.AccesoDatos.PriCatalogos;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Negocio.PriCatalogos;

/// <summary>
/// Negocio.
/// </summary>
public class NRegimenesFiscales : RRegimenesFiscales, INRegimenesFiscales
{
    #region Variables
    /// <summary>
    /// Reglas de negocio.
    /// </summary>
    private IMReglasNeg<ERegimenFiscal> _regimenFiscalReglas = null;
    #endregion

    #region Constructores
    /// <summary>
    /// Negocio.
    /// </summary>
    public NRegimenesFiscales(IMConexionEntidad conexion)
        : base(conexion)
    {
    }
    #endregion

    #region Funciones

    #region RegimenFiscal (RegimenesFiscales)
    /// <summary>
    /// Esta funcion valida e inserta un registro en la base de datos.
    /// </summary>
    public new async Task<Boolean> RegimenFiscalInserta(ERegimenFiscal regimenFiscal)
    {
        //Validacion
        if (!RegimenFiscalValida(regimenFiscal))
            return false;

        //Persistencia
        return await base.RegimenFiscalInserta(regimenFiscal);
    }
    /// <summary>
    /// Valida y actualiza un registro en la base de datos.
    /// </summary>
    public new async Task<Boolean> RegimenFiscalActualiza(ERegimenFiscal regimenFiscal)
    {
        //Validacion
        if (!RegimenFiscalValida(regimenFiscal))
            return false;

        //Persistencia
        return await base.RegimenFiscalActualiza(regimenFiscal);
    }
    /// <summary>
    /// Elimina un registro de la base de datos.
    /// </summary>
    /// <returns></returns>
    public new async Task<Boolean> RegimenFiscalElimina(ERegimenFiscal regimenFiscal)
    {
        //Validacion
        RegimenFiscalReglasNeg().ValidateProperty(regimenFiscal, e => e.RegimenFiscaId);
        if (!Mensajes.Ok)
            return false;

        //Persistencia
        return await base.RegimenFiscalElimina(regimenFiscal);
    }
    /// <summary>
    /// Reglas de negocio.
    /// </summary>
    public Task<List<MEReglaNeg>> RegimenFiscalReglas()
    {
        return Task.Run(() => RegimenFiscalReglasNeg().Rules);
    }
    /// <summary>
    /// Validacion para inserta y actualiza.
    /// </summary>
    private Boolean RegimenFiscalValida(ERegimenFiscal regimenFiscal)
    {
        Mensajes.Initialize();
        if (!RegimenFiscalReglasNeg().Validate(regimenFiscal))
            return false;

        //Validaciones adicionales

        return Mensajes.Ok;
    }
    /// <summary>
    /// Crea las reglas de negocio.
    /// </summary>
    private IMReglasNeg<ERegimenFiscal> RegimenFiscalReglasNeg()
    {
        if (_regimenFiscalReglas != null)
            return _regimenFiscalReglas;

        _regimenFiscalReglas = Validaciones.CreaReglasNeg<ERegimenFiscal>(Mensajes);
        _regimenFiscalReglas.AddSL(e => e.RegimenFiscaId, 0L, Validaciones._int64Max, false); //Consecutivo
        _regimenFiscalReglas.AddSL(e => e.RegimenFiscalClave, 2, 10);
        _regimenFiscalReglas.AddSL(e => e.RegimenFiscalNombre, 2, 120);
        _regimenFiscalReglas.AddSL(e => e.Activo);

        return _regimenFiscalReglas;
    }
    #endregion

    #endregion
}
