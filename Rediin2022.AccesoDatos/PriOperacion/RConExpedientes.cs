using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Entidades;
using DSMetodNetX.Idioma;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriOperacion;
using System;
using System.Collections.Generic;
using System.Net.Http;

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
            //Adi
            conExpedienteFiltro.FilBusquedaActiva = !String.IsNullOrWhiteSpace(conExpedienteFiltro.FilBusquedaTexto);
            if (conExpedienteFiltro.FilBusquedaActiva)
            {
                DateTime vFecha;
                conExpedienteFiltro.FilBuscaFec = DateTime.TryParse(conExpedienteFiltro.FilBusquedaTexto, out vFecha);
                if (vFecha < new DateTime(1800, 1, 1))
                    conExpedienteFiltro.FilBuscaFec = false;
                if (conExpedienteFiltro.FilBuscaFec)
                    conExpedienteFiltro.FilBusquedaFecha = vFecha;


                Decimal vNumero;
                conExpedienteFiltro.FilBuscaNum = Decimal.TryParse(conExpedienteFiltro.FilBusquedaTexto, out vNumero);
                if (conExpedienteFiltro.FilBuscaNum)
                    conExpedienteFiltro.FilBusquedaNumero = vNumero;
            }

            EConExpedientePag vConExpedientePag = new EConExpedientePag();

            _conexion.AddParamFilterTL(conExpedienteFiltro);
            _conexion.LoadEntity<EConExpedientePag>("NTExpExpCP", vConExpedientePag);
            if (!_mensajes.Ok)
                return vConExpedientePag;

            base.MProcesaDatPag(conExpedienteFiltro, vConExpedientePag);

            if (vConExpedientePag.DatPag.TotalLines <= 0)
                return vConExpedientePag;

            _conexion.AddParamFilterPag(conExpedienteFiltro);
            //vConExpedientePag.Pagina = _conexion.LoadEntities<EConExpediente>("NTExpExpCP");
            //Mod
            Int32 vLectura = 0;
            List<EConExpediente> vExpedientes = new List<EConExpediente>();
            vConExpedientePag.Pagina = vExpedientes;
            EConExpediente vExp;
            EConExpValores vExpVal;
            _conexion.XConnection.LoadObject(
                _conexion.GetCurrentCmd("NTExpExpCP"),
                _conexion.XConnection.GetXDataReader(),
                (IXDataReader dr) => { vLectura++; return true; },
                (IXDataReader dr) =>
                {
                    if (vLectura == 1)
                    {
                        vExp = new EConExpediente();
                        vExp.ExpedienteId = dr.GetInt64(nameof(EConExpediente.ExpedienteId));
                        if (!dr.IsNull(nameof(EConExpediente.ProcesoOperativoEstId)))
                            vExp.ProcesoOperativoEstId = dr.GetInt64(nameof(EConExpediente.ProcesoOperativoEstId));
                        else
                            vExp.ProcesoOperativoEstId = 0;
                        vExp.UsuarioIdCreador = dr.GetInt64(nameof(EConExpediente.UsuarioIdCreador));
                        vExp.FechaCreacion = dr.GetDateTime(nameof(EConExpediente.FechaCreacion));
                        vExp.UsuarioIdUltMod = dr.GetInt64(nameof(EConExpediente.UsuarioIdUltMod));
                        vExp.FechaUltMod = dr.GetDateTime(nameof(EConExpediente.FechaUltMod));
                        if (!dr.IsNull(nameof(EConExpediente.EstatusNombre)))
                            vExp.EstatusNombre = dr.GetString(nameof(EConExpediente.EstatusNombre));
                        else
                            vExp.EstatusNombre = String.Empty;
                        if (!dr.IsNull(nameof(EConExpediente.PermiteModificar)))
                            vExp.PermiteModificar = dr.GetBoolean(nameof(EConExpediente.PermiteModificar));
                        else
                            vExp.PermiteModificar = true;
                        vExpedientes.Add(vExp);
                    }
                    else if (vLectura == 2)
                    {
                        vExpVal = new EConExpValores();
                        vExpVal.ExpedienteId = dr.GetInt64(nameof(EConExpValores.ExpedienteId));
                        vExpVal.ColumnaId = dr.GetInt64(nameof(EConExpValores.ColumnaId));
                        if (!dr.IsNull(nameof(EConExpValores.ValorTexto)))
                            vExpVal.ValorTexto = dr.GetString(nameof(EConExpValores.ValorTexto));
                        if (!dr.IsNull(nameof(EConExpValores.ValorNumerico)))
                            vExpVal.ValorNumerico = dr.GetDecimal(nameof(EConExpValores.ValorNumerico));
                        if (!dr.IsNull(nameof(EConExpValores.ValorFecha)))
                            vExpVal.ValorFecha = dr.GetDateTime(nameof(EConExpValores.ValorFecha));
                        foreach (EConExpediente vE in vExpedientes)
                        {
                            if (vE.ExpedienteId == vExpVal.ExpedienteId)
                            {
                                vE.Valores.Add(vExpVal);
                                break;
                            }
                        }
                    }
                    return true;
                });

            if (vConExpedientePag.Pagina != null)
            {
                _conexion.GetCurrentCmd().CommandText = String.Empty;
                _conexion.ClearParameters();
                foreach (EConExpediente vEx in vConExpedientePag.Pagina)
                {
                    _conexion.AddParamIn(MMetaDatos.usuarioIdSesion, _conexion.UsuarioSesion.UsuarioId);
                    _conexion.AddParamIn(nameof(vEx.ProcesoOperativoId), conExpedienteFiltro.ProcesoOperativoId);
                    _conexion.AddParamIn(nameof(vEx.ProcesoOperativoEstId), vEx.ProcesoOperativoEstId);
                    vEx.EstatusValidosSig = _conexion.LoadEntities<EEstatusValidoSig>("NTExpExpEstatusValidosSig");
                }
            }

            return vConExpedientePag;
        }
        /// <summary>
        /// Permite insertar la entidad ConExpediente.
        /// </summary>
        protected Int64 ConExpedienteInserta(EConExpediente conExpediente)
        {
            if (!ActualizaValoresTemp(conExpediente))
                return 0L;

            //Actualizamos en transaccion
            _conexion.AddParamEntity(conExpediente, MAccionesBd.Inserta);
            Int64 vResultado = _conexion.ExecuteScalar<Int64>("NTExpExpIAE");
            return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad ConExpediente.
        /// </summary>
        protected Boolean ConExpedienteActualiza(EConExpediente conExpediente)
        {
            if (!ActualizaValoresTemp(conExpediente))
                return false;

            //Actualizamos en transaccion
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
        /// Acci�n personalizada CambioEstatus.
        /// </summary>
        protected Boolean ConExpedienteCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
        {
            _conexion.AddParamIn(MMetaDatos.usuarioIdSesion, _conexion.UsuarioSesion.UsuarioId);
            _conexion.AddParamEntity(conExpedienteCambioEstatus);
            return _conexion.ExecuteNonQueryRet("NTExpExpCambioEstatus");
        }
        private Boolean ActualizaValoresTemp(EConExpediente conExpediente)
        {
            //Eliminamos todos los valores del expediente a trabajar
            _conexion.AddParamEntity(new EConExpValores() { ExpedienteId = conExpediente.ExpedienteId }, MAccionesBd.Elimina);
            _conexion.ExecuteNonQuery("NTExpExpValoresIAE");
            if (!_conexion.Messages.Ok)
                return false;

            //Insertamos los valores del expediente
            foreach (EConExpValores vVal in conExpediente.Valores)
            {
                _conexion.AddParamEntity(vVal, MAccionesBd.Inserta);
                _conexion.ExecuteNonQuery("NTExpExpValoresIAE");
                if (!_conexion.Messages.Ok)
                    return false;
            }

            return _conexion.Messages.Ok;
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
            Int64 vResultado = _conexion.ExecuteScalarVal("NTExpObjsIAE",
                                                          MensajesXId.ArchivoNombre);
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
        /// <summary>
        /// Acci�n personalizada SelArchivo.
        /// </summary>
        protected Boolean ConExpedienteObjetoSelArchivo(EConExpedienteObjetoSelArchivo conExpedienteObjetoSelArchivo)
        {
            _conexion.AddParamIn(MMetaDatos.usuarioIdSesion, _conexion.UsuarioSesion.UsuarioId);
            _conexion.AddParamEntity(conExpedienteObjetoSelArchivo);
            return _conexion.ExecuteNonQueryRet("NTExpObjsSelArchivo");
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
