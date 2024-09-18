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
    public class NSapOrganizacionesCompra : RSapOrganizacionesCompra, INSapOrganizacionesCompra
    {
        #region Variables
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapOrganizacionCompra> _sapOrganizacionCompraReglas = null;
        /// <summary>
        /// Objeto para exportacion a excel.
        /// </summary>
        private MArchivoExcel _archivoExcel = null;
        #endregion

        #region Constructores
        /// <summary>
        /// Negocio.
        /// </summary>
        public NSapOrganizacionesCompra(IMConexionEntidad conexion,
                                        MArchivoExcel archivoExcel)
            : base(conexion)
        {
            _archivoExcel = archivoExcel;
        }
        #endregion

        #region Funciones

        #region SapOrganizacionCompra (SapOrganizacionesCompra)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> SapOrganizacionCompraInserta(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            //Validacion
            if (!SapOrganizacionCompraValida(sapOrganizacionCompra))
                return false;

            //Persistencia
            return await base.SapOrganizacionCompraInserta(sapOrganizacionCompra);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> SapOrganizacionCompraActualiza(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            //Validacion
            if (!SapOrganizacionCompraValida(sapOrganizacionCompra))
                return false;

            //Persistencia
            return await base.SapOrganizacionCompraActualiza(sapOrganizacionCompra);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new async Task<Boolean> SapOrganizacionCompraElimina(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            //Validacion
            SapOrganizacionCompraReglasNeg().ValidateProperty(sapOrganizacionCompra, e => e.SapOrganizacionCompraId);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.SapOrganizacionCompraElimina(sapOrganizacionCompra);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> SapOrganizacionCompraExporta(ESapOrganizacionCompraFiltro sapOrganizacionCompraFiltro)
        {
            return await base.SapOrganizacionCompraExporta(sapOrganizacionCompraFiltro,
                                                           _archivoExcel);
        }
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapOrganizacionCompraReglas()
        {
            return await Task.Run(() => SapOrganizacionCompraReglasNeg().Rules);
        }
        /// <summary>
        /// Validacion para inserta y actualiza.
        /// </summary>
        private Boolean SapOrganizacionCompraValida(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            Mensajes.Initialize();
            if (!SapOrganizacionCompraReglasNeg().Validate(sapOrganizacionCompra))
                return false;

            //Validaciones adicionales

            return Mensajes.Ok;
        }
        /// <summary>
        /// Crea las reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapOrganizacionCompra> SapOrganizacionCompraReglasNeg()
        {
            if (_sapOrganizacionCompraReglas != null)
                return _sapOrganizacionCompraReglas;

            _sapOrganizacionCompraReglas = Validaciones.CreaReglasNeg<ESapOrganizacionCompra>(Mensajes);
            _sapOrganizacionCompraReglas.AddSL(e => e.SapOrganizacionCompraId, 2, 50);
            _sapOrganizacionCompraReglas.AddSL(e => e.SapOrganizacionCompraNombre, 2, 120);
            _sapOrganizacionCompraReglas.AddSL(e => e.Activo);

            return _sapOrganizacionCompraReglas;
        }
        #endregion

        #endregion
    }
}
