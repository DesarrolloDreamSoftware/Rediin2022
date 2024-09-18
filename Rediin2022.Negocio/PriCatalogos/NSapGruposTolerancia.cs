using DSEntityNetX.Business;
using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Comun;
using DSMetodNetX.Entidades;

using DSMetodNetX.Negocio;
using Rediin2022.AccesoDatos.PriCatalogos;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public new async Task<Boolean> SapGrupoToleranciaInserta(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            //Validacion
            if (!SapGrupoToleranciaValida(sapGrupoTolerancia))
                return false;

            //Persistencia
            return await base.SapGrupoToleranciaInserta(sapGrupoTolerancia);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> SapGrupoToleranciaActualiza(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            //Validacion
            if (!SapGrupoToleranciaValida(sapGrupoTolerancia))
                return false;

            //Persistencia
            return await base.SapGrupoToleranciaActualiza(sapGrupoTolerancia);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new async Task<Boolean> SapGrupoToleranciaElimina(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            //Validacion
            SapGrupoToleranciaReglasNeg().ValidateProperty(sapGrupoTolerancia, e => e.SapGrupoToleranciaId);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.SapGrupoToleranciaElimina(sapGrupoTolerancia);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> SapGrupoToleranciaExporta(ESapGrupoToleranciaFiltro sapGrupoToleranciaFiltro)
        {
            return await base.SapGrupoToleranciaExporta(sapGrupoToleranciaFiltro,
                                                        _archivoExcel);
        }
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapGrupoToleranciaReglas()
        {
            return await Task.Run(() => SapGrupoToleranciaReglasNeg().Rules);
        }
        /// <summary>
        /// Validacion para inserta y actualiza.
        /// </summary>
        private Boolean SapGrupoToleranciaValida(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            Mensajes.Initialize();
            if (!SapGrupoToleranciaReglasNeg().Validate(sapGrupoTolerancia))
                return false;

            //Validaciones adicionales

            return Mensajes.Ok;
        }
        /// <summary>
        /// Crea las reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapGrupoTolerancia> SapGrupoToleranciaReglasNeg()
        {
            if (_sapGrupoToleranciaReglas != null)
                return _sapGrupoToleranciaReglas;

            _sapGrupoToleranciaReglas = Validaciones.CreaReglasNeg<ESapGrupoTolerancia>(Mensajes);
            _sapGrupoToleranciaReglas.AddSL(e => e.SapGrupoToleranciaId, 2, 50);
            _sapGrupoToleranciaReglas.AddSL(e => e.SapGrupoToleranciaNombre, 2, 120);
            _sapGrupoToleranciaReglas.AddSL(e => e.Activo);

            return _sapGrupoToleranciaReglas;
        }
        #endregion

        #endregion
    }
}
