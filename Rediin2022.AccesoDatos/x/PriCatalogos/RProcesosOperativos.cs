using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Entidades;
using DSMetodNetX.Idioma;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Net;

namespace Rediin2022.AccesoDatos.PriCatalogos
{
    [Serializable]
    public class RProcesosOperativos : MRepositorio
    {
        #region Constructores
        public RProcesosOperativos(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region ProcesoOperativo (ProcesosOperativos)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativo.
        /// </summary>
        protected EProcesoOperativoPag ProcesoOperativoPag(EProcesoOperativoFiltro procesoOperativoFiltro)
        {
            EProcesoOperativoPag vProcesoOperativoPag = new EProcesoOperativoPag();

            _conexion.AddParamIn(MMetaDatos.establecimientoId, _conexion.UsuarioSesion.EstablecimientoId);
            _conexion.AddParamFilterTL(procesoOperativoFiltro);
            _conexion.LoadEntity<EProcesoOperativoPag>("NCProcesosOperativosCP", vProcesoOperativoPag);
            if (!_mensajes.Ok)
                return vProcesoOperativoPag;

            base.MProcesaDatPag(procesoOperativoFiltro, vProcesoOperativoPag);

            _conexion.AddParamIn(MMetaDatos.establecimientoId, _conexion.UsuarioSesion.EstablecimientoId);
            _conexion.AddParamFilterPag(procesoOperativoFiltro);
            vProcesoOperativoPag.Pagina = _conexion.LoadEntities<EProcesoOperativo>("NCProcesosOperativosCP");

            return vProcesoOperativoPag;
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativo.
        /// </summary>
        protected EProcesoOperativo ProcesoOperativoXId(Int64 procesoOperativoId)
        {
            _conexion.AddParamIn(nameof(procesoOperativoId), procesoOperativoId);
            return _conexion.LoadEntity<EProcesoOperativo>("NCProcesosOperativosCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativo.
        /// </summary>
        public List<MEElemento> ProcesoOperativoCmb()
        {
            return _conexion.LoadCmb<MEElemento>("NCProcesosOperativosCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativo.
        /// </summary>
        protected Int64 ProcesoOperativoInserta(EProcesoOperativo procesoOperativo)
        {
            _conexion.AddParamEntity(procesoOperativo, MAccionesBd.Inserta);
            Int64 vResultado = _conexion.ExecuteScalarVal("NCProcesosOperativosIAE",
                                                          MensajesXId.ProcesoOperativoNombre,
                                                          MensajesXId.Orden);
            return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativo.
        /// </summary>
        protected Boolean ProcesoOperativoActualiza(EProcesoOperativo procesoOperativo)
        {
            _conexion.AddParamEntity(procesoOperativo, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCProcesosOperativosIAE",
                                       MensajesXId.ProcesoOperativoNombre,
                                       MensajesXId.Orden);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativo.
        /// </summary>
        protected Boolean ProcesoOperativoElimina(EProcesoOperativo procesoOperativo)
        {
            _conexion.AddParamEntity(procesoOperativo, MAccionesBd.Elimina);
            //Mod
            Int64 vResultado = _conexion.ExecuteScalarVal("NCProcesosOperativosIAE");
            if (vResultado == -101)
                _mensajes.AddError("El proceso operativo ya contiene expedientes, no se puede eliminar");

            return _mensajes.Ok;
        }
        #endregion

        #region ProcesoOperativoCol (ProcesosOperativosCols)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoCol.
        /// </summary>
        public EProcesoOperativoColPag ProcesoOperativoColPag(EProcesoOperativoColFiltro procesoOperativoColFiltro)
        {
            EProcesoOperativoColPag vProcesoOperativoColPag = new EProcesoOperativoColPag();

            _conexion.AddParamFilterTL(procesoOperativoColFiltro);
            _conexion.LoadEntity<EProcesoOperativoColPag>("NCProcesosOperativosColsCP", vProcesoOperativoColPag);
            if (!_mensajes.Ok)
                return vProcesoOperativoColPag;

            base.MProcesaDatPag(procesoOperativoColFiltro, vProcesoOperativoColPag);

            _conexion.AddParamFilterPag(procesoOperativoColFiltro);
            vProcesoOperativoColPag.Pagina = _conexion.LoadEntities<EProcesoOperativoCol>("NCProcesosOperativosColsCP");

            return vProcesoOperativoColPag;
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoCol.
        /// </summary>
        public EProcesoOperativoCol ProcesoOperativoColXId(Int64 procesoOperativoId,
                                                           Int64 columnaId)
        {
            _conexion.AddParamIn(nameof(procesoOperativoId), procesoOperativoId);
            _conexion.AddParamIn(nameof(columnaId), columnaId);
            return _conexion.LoadEntity<EProcesoOperativoCol>("NCProcesosOperativosColsCI");
        }
        /// <summary>
        /// Consulta adicional de la entidad ProcesoOperativoCol.
        /// </summary>
        public List<EProcesoOperativoCol> ProcesoOperativoColCT(Int64 procesoOperativoId)
        {
            _conexion.AddParamIn(nameof(procesoOperativoId), procesoOperativoId);
            return _conexion.LoadEntities<EProcesoOperativoCol>("NCProcesosOperativosColsCT");
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoCol.
        /// </summary>
        public List<MEElemento> ProcesoOperativoColCmb(Int64 procesoOperativoId)
        {
            _conexion.AddParamInOpt(nameof(procesoOperativoId), procesoOperativoId);
            return _conexion.LoadCmb<MEElemento>("NCProcesosOperativosColsCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoCol.
        /// </summary>
        protected Int64 ProcesoOperativoColInserta(EProcesoOperativoCol procesoOperativoCol)
        {
            _conexion.AddParamEntity(procesoOperativoCol, MAccionesBd.Inserta);
            Int64 vResultado = _conexion.ExecuteScalarVal("NCProcesosOperativosColsIAE",
                                                          MensajesXId.Etiqueta);
            return vResultado;

        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoCol.
        /// </summary>
        protected Boolean ProcesoOperativoColActualiza(EProcesoOperativoCol procesoOperativoCol)
        {
            _conexion.AddParamEntity(procesoOperativoCol, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCProcesosOperativosColsIAE",
                                       MensajesXId.Etiqueta);
            return _mensajes.Ok;

        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoCol.
        /// </summary>
        protected Boolean ProcesoOperativoColElimina(EProcesoOperativoCol procesoOperativoCol)
        {
            _conexion.AddParamEntity(procesoOperativoCol, MAccionesBd.Elimina);
            Int64 vResultado = _conexion.ExecuteScalarVal("NCProcesosOperativosColsIAE");
            if (vResultado == -101)
                _mensajes.AddError("Ya existen expedientes para el proceso operativo, no se puede eliminar la columna");

            return _mensajes.Ok;
        }
        #endregion

        #region ProcesoOperativoObjeto (ProcesosOperativosObjetos)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoObjeto.
        /// </summary>
        public EProcesoOperativoObjetoPag ProcesoOperativoObjetoPag(EProcesoOperativoObjetoFiltro procesoOperativoObjetoFiltro)
        {
            EProcesoOperativoObjetoPag vProcesoOperativoObjetoPag = new EProcesoOperativoObjetoPag();

            _conexion.AddParamFilterTL(procesoOperativoObjetoFiltro);
            _conexion.LoadEntity<EProcesoOperativoObjetoPag>("NCProcesosOperativosObjetosCP", vProcesoOperativoObjetoPag);
            if (!_mensajes.Ok)
                return vProcesoOperativoObjetoPag;

            base.MProcesaDatPag(procesoOperativoObjetoFiltro, vProcesoOperativoObjetoPag);

            _conexion.AddParamFilterPag(procesoOperativoObjetoFiltro);
            vProcesoOperativoObjetoPag.Pagina = _conexion.LoadEntities<EProcesoOperativoObjeto>("NCProcesosOperativosObjetosCP");

            return vProcesoOperativoObjetoPag;
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoObjeto.
        /// </summary>
        public EProcesoOperativoObjeto ProcesoOperativoObjetoXId(Int64 procesoOperativoObjetoId)
        {
            _conexion.AddParamIn(nameof(procesoOperativoObjetoId), procesoOperativoObjetoId);
            return _conexion.LoadEntity<EProcesoOperativoObjeto>("NCProcesosOperativosObjetosCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoObjeto.
        /// </summary>
        public List<MEElemento> ProcesoOperativoObjetoCmb(Int64 procesoOperativoId)
        {
            _conexion.AddParamInOpt(nameof(procesoOperativoId), procesoOperativoId);
            return _conexion.LoadCmb<MEElemento>("NCProcesosOperativosObjetosCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoObjeto.
        /// </summary>
        protected Int64 ProcesoOperativoObjetoInserta(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            _conexion.AddParamEntity(procesoOperativoObjeto, MAccionesBd.Inserta);
            Int64 vResultado = _conexion.ExecuteScalarVal("NCProcesosOperativosObjetosIAE",
                                                          MensajesXId.ProcesoOperativoObjetoNombre);
            return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoObjeto.
        /// </summary>
        protected Boolean ProcesoOperativoObjetoActualiza(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            _conexion.AddParamEntity(procesoOperativoObjeto, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCProcesosOperativosObjetosIAE",
                                       MensajesXId.ProcesoOperativoObjetoNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoObjeto.
        /// </summary>
        protected Boolean ProcesoOperativoObjetoElimina(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            _conexion.AddParamEntity(procesoOperativoObjeto, MAccionesBd.Elimina);
            //Mod
            Int64 vResultado = _conexion.ExecuteScalarVal("NCProcesosOperativosObjetosIAE");
            if (vResultado == -101)
                _mensajes.AddError("El objeto ya se esta usando en un expediente, no se puede eliminar.");

            return _mensajes.Ok;
        }
        #endregion

        #region ProcesoOperativoEst (ProcesosOperativosEst)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoEst.
        /// </summary>
        public EProcesoOperativoEstPag ProcesoOperativoEstPag(EProcesoOperativoEstFiltro procesoOperativoEstFiltro)
        {
            EProcesoOperativoEstPag vProcesoOperativoEstPag = new EProcesoOperativoEstPag();

            _conexion.AddParamFilterTL(procesoOperativoEstFiltro);
            _conexion.LoadEntity<EProcesoOperativoEstPag>("NCProcesosOperativosEstCP", vProcesoOperativoEstPag);
            if (!_mensajes.Ok)
                return vProcesoOperativoEstPag;

            base.MProcesaDatPag(procesoOperativoEstFiltro, vProcesoOperativoEstPag);

            _conexion.AddParamFilterPag(procesoOperativoEstFiltro);
            vProcesoOperativoEstPag.Pagina = _conexion.LoadEntities<EProcesoOperativoEst>("NCProcesosOperativosEstCP");

            return vProcesoOperativoEstPag;
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoEst.
        /// </summary>
        public EProcesoOperativoEst ProcesoOperativoEstXId(Int64 procesoOperativoEstId)
        {
            _conexion.AddParamIn(nameof(procesoOperativoEstId), procesoOperativoEstId);
            return _conexion.LoadEntity<EProcesoOperativoEst>("NCProcesosOperativosEstCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoEst.
        /// </summary>
        public List<MEElemento> ProcesoOperativoEstCmb(Int64 procesoOperativoId)
        {
            _conexion.AddParamInOpt(nameof(procesoOperativoId), procesoOperativoId);
            return _conexion.LoadCmb<MEElemento>("NCProcesosOperativosEstCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoEst.
        /// </summary>
        protected Int64 ProcesoOperativoEstInserta(EProcesoOperativoEst procesoOperativoEst)
        {
            _conexion.AddParamEntity(procesoOperativoEst, MAccionesBd.Inserta);
            Int64 vResultado = _conexion.ExecuteScalarVal("NCProcesosOperativosEstIAE",
                                                          MensajesXId.EstatusNombre);
            return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoEst.
        /// </summary>
        protected Boolean ProcesoOperativoEstActualiza(EProcesoOperativoEst procesoOperativoEst)
        {
            _conexion.AddParamEntity(procesoOperativoEst, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCProcesosOperativosEstIAE",
                                       MensajesXId.EstatusNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoEst.
        /// </summary>
        protected Boolean ProcesoOperativoEstElimina(EProcesoOperativoEst procesoOperativoEst)
        {
            _conexion.AddParamEntity(procesoOperativoEst, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NCProcesosOperativosEstIAE");
        }
        #endregion

        #region ProcesoOperativoEstSec (ProcesosOperativosEstSec)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoEstSec.
        /// </summary>
        public EProcesoOperativoEstSecPag ProcesoOperativoEstSecPag(EProcesoOperativoEstSecFiltro procesoOperativoEstSecFiltro)
        {
            EProcesoOperativoEstSecPag vProcesoOperativoEstSecPag = new EProcesoOperativoEstSecPag();

            _conexion.AddParamFilterTL(procesoOperativoEstSecFiltro);
            _conexion.LoadEntity<EProcesoOperativoEstSecPag>("NCProcesosOperativosEstSecCP", vProcesoOperativoEstSecPag);
            if (!_mensajes.Ok)
                return vProcesoOperativoEstSecPag;

            base.MProcesaDatPag(procesoOperativoEstSecFiltro, vProcesoOperativoEstSecPag);

            _conexion.AddParamFilterPag(procesoOperativoEstSecFiltro);
            vProcesoOperativoEstSecPag.Pagina = _conexion.LoadEntities<EProcesoOperativoEstSec>("NCProcesosOperativosEstSecCP");

            return vProcesoOperativoEstSecPag;
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoEstSec.
        /// </summary>
        public EProcesoOperativoEstSec ProcesoOperativoEstSecXId(Int64 procesoOperativoEstSecId)
        {
            _conexion.AddParamIn(nameof(procesoOperativoEstSecId), procesoOperativoEstSecId);
            return _conexion.LoadEntity<EProcesoOperativoEstSec>("NCProcesosOperativosEstSecCI");
        }
        /// <summary>
        /// Consulta adicional de la entidad ProcesoOperativoEstSec.
        /// </summary>
        public List<EProcesoOperativoEstSec> ProcesoOperativoEstSecCTXIdPadre(Int64 procesoOperativoEstId)
        {
            _conexion.AddParamIn(nameof(procesoOperativoEstId), procesoOperativoEstId);
            return _conexion.LoadEntities<EProcesoOperativoEstSec>("NCProcesosOperativosEstSecCTXIdPadre");
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoEstSec.
        /// </summary>
        protected Int64 ProcesoOperativoEstSecInserta(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            _conexion.AddParamEntity(procesoOperativoEstSec, MAccionesBd.Inserta);
            Int64 vResultado = _conexion.ExecuteScalarVal("NCProcesosOperativosEstSecIAE",
                                                          MensajesXId.ProcesoOperativoEstIdSig);

            //Adi
            if (vResultado == -101)
                _mensajes.AddError("Solo hasta dos estatus se permiten indicar para la secuencia.");

            return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoEstSec.
        /// </summary>
        protected Boolean ProcesoOperativoEstSecActualiza(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            _conexion.AddParamEntity(procesoOperativoEstSec, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCProcesosOperativosEstSecIAE",
                                       MensajesXId.ProcesoOperativoEstIdSig);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoEstSec.
        /// </summary>
        protected Boolean ProcesoOperativoEstSecElimina(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            _conexion.AddParamEntity(procesoOperativoEstSec, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NCProcesosOperativosEstSecIAE");
        }
        #endregion

        #endregion
    }
}
