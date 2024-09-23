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
public class NModelos : RModelos, INModelos
{
    #region Variables
    /// <summary>
    /// Reglas de negocio.
    /// </summary>
    private IMReglasNeg<EModelo> _modeloReglas = null;
    #endregion

    #region Constructores
    /// <summary>
    /// Negocio.
    /// </summary>
    public NModelos(IMConexionEntidad conexion)
        : base(conexion)
    {
    }
    #endregion

    #region Funciones

    #region Modelo (Modelos)
    /// <summary>
    /// Esta funcion valida e inserta un registro en la base de datos.
    /// </summary>
    public new async Task<Boolean> ModeloInserta(EModelo modelo)
    {
        //Validacion
        if (!ModeloValida(modelo))
            return false;

        //Persistencia
        return await base.ModeloInserta(modelo);
    }
    /// <summary>
    /// Valida y actualiza un registro en la base de datos.
    /// </summary>
    public new async Task<Boolean> ModeloActualiza(EModelo modelo)
    {
        //Validacion
        if (!ModeloValida(modelo))
            return false;

        //Persistencia
        return await base.ModeloActualiza(modelo);
    }
    /// <summary>
    /// Elimina un registro de la base de datos.
    /// </summary>
    /// <returns></returns>
    public new async Task<Boolean> ModeloElimina(EModelo modelo)
    {
        //Validacion
        ModeloReglasNeg().ValidateProperty(modelo, e => e.ModeloId);
        if (!Mensajes.Ok)
            return false;

        //Persistencia
        return await base.ModeloElimina(modelo);
    }
    /// <summary>
    /// Reglas de negocio.
    /// </summary>
    public Task<List<MEReglaNeg>> ModeloReglas()
    {
        return Task.Run(() => ModeloReglasNeg().Rules);
    }
    /// <summary>
    /// Validacion para inserta y actualiza.
    /// </summary>
    private Boolean ModeloValida(EModelo modelo)
    {
        Mensajes.Initialize();
        if (!ModeloReglasNeg().Validate(modelo))
            return false;

        //Validaciones adicionales

        return Mensajes.Ok;
    }
    /// <summary>
    /// Crea las reglas de negocio.
    /// </summary>
    private IMReglasNeg<EModelo> ModeloReglasNeg()
    {
        if (_modeloReglas != null)
            return _modeloReglas;

        _modeloReglas = Validaciones.CreaReglasNeg<EModelo>(Mensajes);
        _modeloReglas.AddSL(e => e.ModeloId, 0L, Validaciones._int64Max, false); // Consecutivo
        _modeloReglas.AddSL(e => e.ModeloNombre, 2, 120);
        _modeloReglas.AddSL(e => e.TipoCapturaId, TipoCaptura.Ninguno, TipoCaptura.PersonaExtranjera);
        _modeloReglas.AddSL(e => e.Activo);

        return _modeloReglas;
    }
    #endregion

    #endregion
}
