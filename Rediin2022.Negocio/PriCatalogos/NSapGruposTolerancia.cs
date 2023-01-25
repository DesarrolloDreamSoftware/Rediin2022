using DSEntityNetX.Business;
using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Entidades;
using DSMetodNetX.Idioma;
using DSMetodNetX.Negocio;
using Rediin2022.AccesoDatos.PriCatalogos;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.Negocio.PriCatalogos
{
    /// <summary>
    /// Negocio.
    /// </summary>
    public class NSapGruposTolerancia : RSapGruposTolerancia, INSapGruposTolerancia
    {
        #region Variables
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapGrupoTolerancia> _sapGrupoToleranciaReglas = null;
        /// <summary>
        /// Objeto para exportacion a excel.
        /// </summary>
        private MArchivoExcel _archivoExcel = null;
        #endregion

        #region Constructores
        /// <summary>
        /// Negocio.
        /// </summary>
        public NSapGruposTolerancia(IMConexionEntidad conexion,
                                    MArchivoExcel archivoExcel)
            : base(conexion)
        {
            _archivoExcel = archivoExcel;
        }
        #endregion

        #region Funciones

        #region SapGrupoTolerancia (SapGruposTolerancia)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new Boolean SapGrupoToleranciaInserta(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            //Validacion
            if (!SapGrupoToleranciaValida(sapGrupoTolerancia))
                return false;

            //Persistencia
            return base.SapGrupoToleranciaInserta(sapGrupoTolerancia);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new Boolean SapGrupoToleranciaActualiza(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            //Validacion
            if (!SapGrupoToleranciaValida(sapGrupoTolerancia))
                return false;

            //Persistencia
            return base.SapGrupoToleranciaActualiza(sapGrupoTolerancia);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new Boolean SapGrupoToleranciaElimina(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            //Validacion
            SapGrupoToleranciaReglasNeg().ValidateProperty(sapGrupoTolerancia, e => e.SapGrupoToleranciaId);
            if (!_mensajes.Ok)
                return false;

            //Persistencia
            return base.SapGrupoToleranciaElimina(sapGrupoTolerancia);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapGrupoToleranciaExporta(ESapGrupoToleranciaFiltro sapGrupoToleranciaFiltro)
        {
            return base.SapGrupoToleranciaExporta(sapGrupoToleranciaFiltro,
                                                  _archivoExcel);
        }
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public List<MEReglaNeg> SapGrupoToleranciaReglas()
        {
            return SapGrupoToleranciaReglasNeg().Rules;
        }
        /// <summary>
        /// Validacion para inserta y actualiza.
        /// </summary>
        private Boolean SapGrupoToleranciaValida(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            _mensajes.Initialize();
            if (!SapGrupoToleranciaReglasNeg().Validate(sapGrupoTolerancia))
                return false;

            //Validaciones adicionales

            return _mensajes.Ok;
        }
        /// <summary>
        /// Crea las reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapGrupoTolerancia> SapGrupoToleranciaReglasNeg()
        {
            if (_sapGrupoToleranciaReglas != null)
                return _sapGrupoToleranciaReglas;

            _sapGrupoToleranciaReglas = Validaciones.CreaReglasNeg<ESapGrupoTolerancia>(_mensajes);
            _sapGrupoToleranciaReglas.AddSL(e => e.SapGrupoToleranciaId, 2, 50);
            _sapGrupoToleranciaReglas.AddSL(e => e.SapGrupoToleranciaNombre, 2, 120);
            _sapGrupoToleranciaReglas.AddSL(e => e.Activo);

            return _sapGrupoToleranciaReglas;
        }
        #endregion

        #endregion
    }
}
