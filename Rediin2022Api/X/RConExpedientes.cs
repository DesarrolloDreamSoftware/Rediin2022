using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Entidades;
using DSMetodNetX.Idioma;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriOperacion;
using System;
using System.Collections.Generic;

namespace Rediin2022.AccesoDatos.PriOperacion
{
    [Serializable]
    public class RConExpedientes : MRepositorio
    {
        #region Constructores
        public RConExpedientes(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region ConExpProcOperativo (Enc)
        /// <summary>
        /// Consulta paginada de la entidad ConExpProcOperativo.
        /// </summary>
        public EConExpProcOperativoPag ConExpProcOperativoPag(EConExpProcOperativoFiltro conExpProcOperativoFiltro)
        {
            EConExpProcOperativoPag vConExpProcOperativoPag = new EConExpProcOperativoPag();

            _conexion.AddParamIn(MMetaDatos.establecimientoId, _conexion.UsuarioSesion.EstablecimientoId);
            _conexion.AddParamFilterTL(conExpProcOperativoFiltro);
            _conexion.LoadEntity<EConExpProcOperativoPag>("NTExpEncCP", vConExpProcOperativoPag);
            if (!_mensajes.Ok)
                return vConExpProcOperativoPag;

            base.MProcesaDatPag(conExpProcOperativoFiltro, vConExpProcOperativoPag);

            _conexion.AddParamIn(MMetaDatos.establecimientoId, _conexion.UsuarioSesion.EstablecimientoId);
            _conexion.AddParamFilterPag(conExpProcOperativoFiltro);
            vConExpProcOperativoPag.Pagina = _conexion.LoadEntities<EConExpProcOperativo>("NTExpEncCP");

            return vConExpProcOperativoPag;
        }
        #endregion

        #region ConExpediente (Exp)
        /// <summary>
        /// Consulta paginada de la entidad ConExpediente.
        /// </summary>
        protected EConExpedientePag ConExpedientePag(EConExpedienteFiltro conExpedienteFiltro)
        {
            EConExpedientePag vConExpedientePag = new EConExpedientePag();

            _conexion.AddParamFilterTL(conExpedienteFiltro);
            _conexion.LoadEntity<EConExpedientePag>("NTExpExpCP", vConExpedientePag);
            if (!_mensajes.Ok)
                return vConExpedientePag;

            base.MProcesaDatPag(conExpedienteFiltro, vConExpedientePag);

            _conexion.AddParamFilterPag(conExpedienteFiltro);
            vConExpedientePag.Pagina = _conexion.LoadEntities<EConExpediente>("NTExpExpCP");

            return vConExpedientePag;
        }
        /// <summary>
        /// Permite insertar la entidad ConExpediente.
        /// </summary>
        protected Int64 ConExpedienteInserta(EConExpediente conExpediente)
        {
            _conexion.AddParamEntity(conExpediente, MAccionesBd.Inserta);
            Int64 vResultado = _conexion.ExecuteScalar<Int64>("NTExpExpIAE");
            return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad ConExpediente.
        /// </summary>
        protected Boolean ConExpedienteActualiza(EConExpediente conExpediente)
        {
            _conexion.AddParamEntity(conExpediente, MAccionesBd.Actualiza);
            _conexion.ExecuteScalar("NTExpExpIAE");
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad ConExpediente.
        /// </summary>
        protected Boolean ConExpedienteElimina(EConExpediente conExpediente)
        {
            _conexion.AddParamEntity(conExpediente, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NTExpExpIAE");
        }
        /// <summary>
        /// Acción personalizada CambioEstatusUno.
        /// </summary>
        protected Boolean ConExpedienteCambioEstatusUno(EConExpedienteCambioEstatusUno conExpedienteCambioEstatusUno)
        {
            _conexion.AddParamIn(MMetaDatos.usuarioIdSesion, _conexion.UsuarioSesion.UsuarioId);
            _conexion.AddParamEntity(conExpedienteCambioEstatusUno);
            return _conexion.ExecuteNonQueryRet("NTExpExpCambioEstatusUno");
        }
        /// <summary>
        /// Acción personalizada CambioEstatusDos.
        /// </summary>
        protected Boolean ConExpedienteCambioEstatusDos(EConExpedienteCambioEstatusDos conExpedienteCambioEstatusDos)
        {
            _conexion.AddParamIn(MMetaDatos.usuarioIdSesion, _conexion.UsuarioSesion.UsuarioId);
            _conexion.AddParamEntity(conExpedienteCambioEstatusDos);
            return _conexion.ExecuteNonQueryRet("NTExpExpCambioEstatusDos");
        }
        #endregion

        #region ConExpedienteObjeto (Objs)
        /// <summary>
        /// Consulta paginada de la entidad ConExpedienteObjeto.
        /// </summary>
        public EConExpedienteObjetoPag ConExpedienteObjetoPag(EConExpedienteObjetoFiltro conExpedienteObjetoFiltro)
        {
            EConExpedienteObjetoPag vConExpedienteObjetoPag = new EConExpedienteObjetoPag();

            _conexion.AddParamFilterTL(conExpedienteObjetoFiltro);
            _conexion.LoadEntity<EConExpedienteObjetoPag>("NTExpObjsCP", vConExpedienteObjetoPag);
            if (!_mensajes.Ok)
                return vConExpedienteObjetoPag;

            base.MProcesaDatPag(conExpedienteObjetoFiltro, vConExpedienteObjetoPag);

            _conexion.AddParamFilterPag(conExpedienteObjetoFiltro);
            vConExpedienteObjetoPag.Pagina = _conexion.LoadEntities<EConExpedienteObjeto>("NTExpObjsCP");

            return vConExpedienteObjetoPag;
        }
        /// <summary>
        /// Permite insertar la entidad ConExpedienteObjeto.
        /// </summary>
        protected Int64 ConExpedienteObjetoInserta(EConExpedienteObjeto conExpedienteObjeto)
        {
            _conexion.AddParamEntity(conExpedienteObjeto, MAccionesBd.Inserta);
            Int64 vResultado = _conexion.ExecuteScalar<Int64>("NTExpObjsIAE");
            return vResultado;
        }
        /// <summary>
        /// Permite eliminar la entidad ConExpedienteObjeto.
        /// </summary>
        protected Boolean ConExpedienteObjetoElimina(EConExpedienteObjeto conExpedienteObjeto)
        {
            _conexion.AddParamEntity(conExpedienteObjeto, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NTExpObjsIAE");
        }
        #endregion

        #region ExpedienteEstatu (ExpeEsta)
        /// <summary>
        /// Consulta paginada de la entidad ExpedienteEstatu.
        /// </summary>
        public EExpedienteEstatuPag ExpedienteEstatuPag(EExpedienteEstatuFiltro expedienteEstatuFiltro)
        {
            EExpedienteEstatuPag vExpedienteEstatuPag = new EExpedienteEstatuPag();

            _conexion.AddParamFilterTL(expedienteEstatuFiltro);
            _conexion.LoadEntity<EExpedienteEstatuPag>("NTExpExpeEstaCP", vExpedienteEstatuPag);
            if (!_mensajes.Ok)
                return vExpedienteEstatuPag;

            base.MProcesaDatPag(expedienteEstatuFiltro, vExpedienteEstatuPag);

            _conexion.AddParamFilterPag(expedienteEstatuFiltro);
            vExpedienteEstatuPag.Pagina = _conexion.LoadEntities<EExpedienteEstatu>("NTExpExpeEstaCP");

            return vExpedienteEstatuPag;
        }
        #endregion

        #endregion
    }
}
