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
    public class NSapGruposTesoreria : RSapGruposTesoreria, INSapGruposTesoreria
    {
        #region Variables
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapGrupoTesoreria> _sapGrupoTesoreriaReglas = null;
        /// <summary>
        /// Objeto para exportacion a excel.
        /// </summary>
        private MArchivoExcel _archivoExcel = null;
        #endregion

        #region Constructores
        /// <summary>
        /// Negocio.
        /// </summary>
        public NSapGruposTesoreria(IMConexionEntidad conexion,
                                   MArchivoExcel archivoExcel)
            : base(conexion)
        {
            _archivoExcel = archivoExcel;
        }
        #endregion

        #region Funciones

        #region SapGrupoTesoreria (SapGruposTesoreria)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> SapGrupoTesoreriaInserta(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            //Validacion
            if (!SapGrupoTesoreriaValida(sapGrupoTesoreria))
                return false;

            //Persistencia
            return await base.SapGrupoTesoreriaInserta(sapGrupoTesoreria);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> SapGrupoTesoreriaActualiza(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            //Validacion
            if (!SapGrupoTesoreriaValida(sapGrupoTesoreria))
                return false;

            //Persistencia
            return await base.SapGrupoTesoreriaActualiza(sapGrupoTesoreria);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new async Task<Boolean> SapGrupoTesoreriaElimina(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            //Validacion
            SapGrupoTesoreriaReglasNeg().ValidateProperty(sapGrupoTesoreria, e => e.SapGrupoTesoreriaId);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.SapGrupoTesoreriaElimina(sapGrupoTesoreria);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> SapGrupoTesoreriaExporta(ESapGrupoTesoreriaFiltro sapGrupoTesoreriaFiltro)
        {
            return await base.SapGrupoTesoreriaExporta(sapGrupoTesoreriaFiltro,
                                                       _archivoExcel);
        }
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapGrupoTesoreriaReglas()
        {
            return await Task.Run(() => SapGrupoTesoreriaReglasNeg().Rules);
        }
        /// <summary>
        /// Validacion para inserta y actualiza.
        /// </summary>
        private Boolean SapGrupoTesoreriaValida(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            Mensajes.Initialize();
            if (!SapGrupoTesoreriaReglasNeg().Validate(sapGrupoTesoreria))
                return false;

            //Validaciones adicionales

            return Mensajes.Ok;
        }
        /// <summary>
        /// Crea las reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapGrupoTesoreria> SapGrupoTesoreriaReglasNeg()
        {
            if (_sapGrupoTesoreriaReglas != null)
                return _sapGrupoTesoreriaReglas;

            _sapGrupoTesoreriaReglas = Validaciones.CreaReglasNeg<ESapGrupoTesoreria>(Mensajes);
            _sapGrupoTesoreriaReglas.AddSL(e => e.SapGrupoTesoreriaId, 2, 50);
            _sapGrupoTesoreriaReglas.AddSL(e => e.SapGrupoTesoreriaNombre, 2, 120);
            _sapGrupoTesoreriaReglas.AddSL(e => e.Activo);

            return _sapGrupoTesoreriaReglas;
        }
        #endregion

        #endregion
    }
}
