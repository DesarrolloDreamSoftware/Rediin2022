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
public class NIncoterms : RIncoterms, INIncoterms
{
    #region Variables
    /// <summary>
    /// Reglas de negocio.
    /// </summary>
    private IMReglasNeg<EIncoterm> _incotermReglas = null;
    #endregion

    #region Constructores
    /// <summary>
    /// Negocio.
    /// </summary>
    public NIncoterms(IMConexionEntidad conexion)
        : base(conexion)
    {
    }
    #endregion

    #region Funciones

    #region Incoterm (Incoterms)
    /// <summary>
    /// Esta funcion valida e inserta un registro en la base de datos.
    /// </summary>
    public new async Task<Int64> IncotermInserta(EIncoterm incoterm)
    {
        //Validacion
        if (!IncotermValida(incoterm))
            return 0L;

        //Persistencia
        return await base.IncotermInserta(incoterm);
    }
    /// <summary>
    /// Valida y actualiza un registro en la base de datos.
    /// </summary>
    public new async Task<Boolean> IncotermActualiza(EIncoterm incoterm)
    {
        //Validacion
        if (!IncotermValida(incoterm))
            return false;

        //Persistencia
        return await base.IncotermActualiza(incoterm);
    }
    /// <summary>
    /// Elimina un registro de la base de datos.
    /// </summary>
    /// <returns></returns>
    public new async Task<Boolean> IncotermElimina(EIncoterm incoterm)
    {
        //Validacion
        IncotermReglasNeg().ValidateProperty(incoterm, e => e.IncotermId);
        if (!Mensajes.Ok)
            return false;

        //Persistencia
        return await base.IncotermElimina(incoterm);
    }
    /// <summary>
    /// Reglas de negocio.
    /// </summary>
    public Task<List<MEReglaNeg>> IncotermReglas()
    {
        return Task.Run(() => IncotermReglasNeg().Rules);
    }
    /// <summary>
    /// Validacion para inserta y actualiza.
    /// </summary>
    private Boolean IncotermValida(EIncoterm incoterm)
    {
        Mensajes.Initialize();
        if (!IncotermReglasNeg().Validate(incoterm))
            return false;

        //Validaciones adicionales

        return Mensajes.Ok;
    }
    /// <summary>
    /// Crea las reglas de negocio.
    /// </summary>
    private IMReglasNeg<EIncoterm> IncotermReglasNeg()
    {
        if (_incotermReglas != null)
            return _incotermReglas;

        _incotermReglas = Validaciones.CreaReglasNeg<EIncoterm>(Mensajes);
        _incotermReglas.AddSL(e => e.IncotermId, 0L, Validaciones._int64Max, false); // Consecutivo
        _incotermReglas.AddSL(e => e.IncotermClave, 2, 10);
        _incotermReglas.AddSL(e => e.IncotermNombre, 2, 120);
        _incotermReglas.AddSL(e => e.Activo);

        return _incotermReglas;
    }
    #endregion

    #endregion
}
