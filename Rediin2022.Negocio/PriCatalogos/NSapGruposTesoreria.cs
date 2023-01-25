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
        public new Boolean SapGrupoTesoreriaInserta(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            //Validacion
            if (!SapGrupoTesoreriaValida(sapGrupoTesoreria))
                return false;

            //Persistencia
            return base.SapGrupoTesoreriaInserta(sapGrupoTesoreria);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new Boolean SapGrupoTesoreriaActualiza(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            //Validacion
            if (!SapGrupoTesoreriaValida(sapGrupoTesoreria))
                return false;

            //Persistencia
            return base.SapGrupoTesoreriaActualiza(sapGrupoTesoreria);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new Boolean SapGrupoTesoreriaElimina(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            //Validacion
            SapGrupoTesoreriaReglasNeg().ValidateProperty(sapGrupoTesoreria, e => e.SapGrupoTesoreriaId);
            if (!_mensajes.Ok)
                return false;

            //Persistencia
            return base.SapGrupoTesoreriaElimina(sapGrupoTesoreria);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapGrupoTesoreriaExporta(ESapGrupoTesoreriaFiltro sapGrupoTesoreriaFiltro)
        {
            return base.SapGrupoTesoreriaExporta(sapGrupoTesoreriaFiltro,
                                                 _archivoExcel);
        }
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public List<MEReglaNeg> SapGrupoTesoreriaReglas()
        {
            return SapGrupoTesoreriaReglasNeg().Rules;
        }
        /// <summary>
        /// Validacion para inserta y actualiza.
        /// </summary>
        private Boolean SapGrupoTesoreriaValida(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            _mensajes.Initialize();
            if (!SapGrupoTesoreriaReglasNeg().Validate(sapGrupoTesoreria))
                return false;

            //Validaciones adicionales

            return _mensajes.Ok;
        }
        /// <summary>
        /// Crea las reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapGrupoTesoreria> SapGrupoTesoreriaReglasNeg()
        {
            if (_sapGrupoTesoreriaReglas != null)
                return _sapGrupoTesoreriaReglas;

            _sapGrupoTesoreriaReglas = Validaciones.CreaReglasNeg<ESapGrupoTesoreria>(_mensajes);
            _sapGrupoTesoreriaReglas.AddSL(e => e.SapGrupoTesoreriaId, 2, 50);
            _sapGrupoTesoreriaReglas.AddSL(e => e.SapGrupoTesoreriaNombre, 2, 120);
            _sapGrupoTesoreriaReglas.AddSL(e => e.Activo);

            return _sapGrupoTesoreriaReglas;
        }
        #endregion

        #endregion
    }
}
