using DSEntityNetX.Entities.Connection;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Comun;
using DSMetodNetX.Entidades;
using Rediin2022.Comun.PriOperacion;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriOperacion;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.AccesoDatos.PriOperacion
{
    [Serializable]
    public class RConExpedientes : MRepositorio
    {
        #region Variables
        /// <summary>
        /// Conexión.
        /// </summary>
        private IMConexionEntidad _conexion;
        #endregion

        #region Constructores
        public RConExpedientes(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Funciones

        #region ConExpProcOperativo (Enc)
        /// <summary>
        /// Consulta paginada de la entidad ConExpProcOperativo.
        /// </summary>
        public async Task<EConExpProcOperativoPag> ConExpProcOperativoPag(EConExpProcOperativoFiltro conExpProcOperativoFiltro)
        {
            return await _conexion.EntidadPagAsync<EConExpProcOperativo,
                                                    EConExpProcOperativoPag,
                                                    EConExpProcOperativoFiltro>(conExpProcOperativoFiltro, "NTExpEncCP");
        }
        #endregion

        #region ConExpediente (Exp)
        /// <summary>
        /// Consulta paginada de la entidad ConExpediente.
        /// </summary>
        protected async Task<EConExpedientePag> ConExpedientePag(EConExpedienteFiltro conExpedienteFiltro)
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
            await _conexion.LoadEntityAsync<EConExpedientePag>("NTExpExpCP", vConExpedientePag);
            if (!Mensajes.Ok)
                return vConExpedientePag;

            _conexion.MProcesaDatPag(conExpedienteFiltro, vConExpedientePag);

            if (vConExpedientePag.DatPag.TotalLines <= 0)
                return vConExpedientePag;

            _conexion.AddParamFilterPag(conExpedienteFiltro);

            //Mod
            Int32 vLectura = 0;
            List<EConExpediente> vExpedientes = new List<EConExpediente>();
            vConExpedientePag.Pagina = vExpedientes;
            EConExpediente vExp;
            EConExpValores vExpVal;
            await _conexion.XConnection.LoadObjectAsync(
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
                    _conexion.AddParamIn(_conexion.UsuarioSesion.UsuarioId, MMetaDatos.usuarioIdSesion);
                    _conexion.AddParamIn(conExpedienteFiltro.ProcesoOperativoId);
                    _conexion.AddParamIn(vEx.ProcesoOperativoEstId);
                    vEx.EstatusValidosSig = await _conexion.LoadEntitiesAsync<EEstatusValidoSig>("NTExpExpEstatusValidosSig");
                }
            }

            return vConExpedientePag;
        }
        /// <summary>
        /// Consulta por id de la entidad ConExpediente.
        /// </summary>
        /// <param name="expedienteId"></param>
        /// <returns></returns>
        public async Task<EConExpediente> ConExpedienteXId(Int64 expedienteId)
        {
            _conexion.AddParamIn(expedienteId);

            Int32 vLectura = 0;
            EConExpediente vExp = null;
            EConExpValores vExpVal = null;
            await _conexion.XConnection.LoadObjectAsync(
                _conexion.GetCurrentCmd("NTExpExpCI"),
                _conexion.XConnection.GetXDataReader(),
                (IXDataReader dr) => { vLectura++; return true; },
                (IXDataReader dr) =>
                {
                    if (vLectura == 1)
                    {
                        vExp = new EConExpediente();
                        vExp.ProcesoOperativoId = dr.GetInt64(nameof(EConExpediente.ProcesoOperativoId)); //Adi Adicional a consulta paginada
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

                        if (vExp != null)
                            vExp.Valores.Add(vExpVal);
                    }
                    return true;
                });

            if (vExp != null)
            {
                _conexion.GetCurrentCmd().CommandText = String.Empty;
                _conexion.ClearParameters();

                _conexion.AddParamIn(_conexion.UsuarioSesion.UsuarioId, MMetaDatos.usuarioIdSesion);
                _conexion.AddParamIn(vExp.ProcesoOperativoId);
                _conexion.AddParamIn(vExp.ProcesoOperativoEstId);
                vExp.EstatusValidosSig = await _conexion.LoadEntitiesAsync<EEstatusValidoSig>("NTExpExpEstatusValidosSig");
            }

            return vExp;
        }
        /// <summary>
        /// Permite insertar la entidad ConExpediente.
        /// </summary>
        protected async Task<Int64> ConExpedienteInserta(EConExpediente conExpediente)
        {
            if (!await ActualizaValoresTemp(conExpediente))
                return 0L;

            await _conexion.EntityUpdateAsync(conExpediente, MAccionesBd.Inserta, "NTExpExpIAE");
            return conExpediente.ExpedienteId;
        }
        /// <summary>
        /// Permite actualizar la entidad ConExpediente.
        /// </summary>
        protected async Task<Boolean> ConExpedienteActualiza(EConExpediente conExpediente)
        {
            if (!await ActualizaValoresTemp(conExpediente))
                return false;

            return await _conexion.EntityUpdateAsync(conExpediente, MAccionesBd.Actualiza, "NTExpExpIAE");
        }
        /// <summary>
        /// Permite eliminar la entidad ConExpediente.
        /// </summary>
        protected async Task<Boolean> ConExpedienteElimina(EConExpediente conExpediente)
        {
            return await _conexion.EntityUpdateAsync(conExpediente, MAccionesBd.Elimina, "NTExpExpIAE");
        }
        /// <summary>
        /// Acción personalizada CambioEstatus.
        /// </summary>
        protected async Task<Boolean> ConExpedienteCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
        {
            _conexion.AddParamIn(UsuarioSesion.UsuarioId, MMetaDatos.usuarioIdSesion);
            _conexion.AddParamEntity(conExpedienteCambioEstatus);
            return await _conexion.ExecuteNonQueryRetAsync("NTExpExpCambioEstatus");
        }
        private async Task<Boolean> ActualizaValoresTemp(EConExpediente conExpediente)
        {
            //Eliminamos todos los valores del expediente a trabajar
            _conexion.AddParamEntity(new EConExpValores() { ExpedienteId = conExpediente.ExpedienteId }, MAccionesBd.Elimina);
            await _conexion.ExecuteNonQueryAsync("NTExpExpValoresIAE");
            if (!_conexion.Messages.Ok)
                return false;

            //Insertamos los valores del expediente
            foreach (EConExpValores vVal in conExpediente.Valores)
            {
                if (conExpediente.ExpedienteId > 0 && vVal.ExpedienteId == 0)
                    vVal.ExpedienteId = conExpediente.ExpedienteId;

                _conexion.AddParamEntity(vVal, MAccionesBd.Inserta);
                await _conexion.ExecuteNonQueryAsync("NTExpExpValoresIAE");
                if (!_conexion.Messages.Ok)
                    return false;
            }

            return _conexion.Messages.Ok;
        }
        ///// <summary>
        ///// Consulta para los combos que se capturan.
        ///// </summary>
        //public async Task<List<MEElemento>> ConExpedienteCmb(EProcesoOperativoCol procesoOperativoCol)
        //{
        //    _conexion.AddParamIn(procesoOperativoCol.CapCmbProcesoOperativoId);
        //    _conexion.AddParamIn(procesoOperativoCol.CapCmbIdColumnaId);
        //    _conexion.AddParamIn(procesoOperativoCol.CapCmbTextoColumnaId);

        //    Dictionary<Int64, MEElemento> vElementosDic = new Dictionary<Int64, MEElemento>();
        //    await _conexion.XConnection.LoadObjectAsync(
        //        _conexion.GetCurrentCmd("NTExpExpCmb"),
        //        _conexion.XConnection.GetXDataReader(),
        //        dr =>
        //        {
        //            Int64 vExpediente = dr.GetInt64(nameof(EConExpValores.ExpedienteId));
        //            TiposColumna vTipo = (TiposColumna)dr.GetInt16(nameof(EProcesoOperativoCol.Tipo));

        //            if (!vElementosDic.ContainsKey(vExpediente))
        //                vElementosDic.Add(vExpediente, new MEElemento());

        //            MEElemento vElemento = vElementosDic[vExpediente];

        //            if (dr.GetInt64(nameof(EConExpValores.ColumnaId)) == procesoOperativoCol.CapCmbIdColumnaId)
        //                vElemento.Id = UtilExpedientesAD.ObtenValorXTipoStr(vTipo, dr);
        //            else if (dr.GetInt64(nameof(EConExpValores.ColumnaId)) == procesoOperativoCol.CapCmbTextoColumnaId)
        //                vElemento.Text = UtilExpedientesAD.ObtenValorXTipoStr(vTipo, dr);

        //            return true;
        //        });

        //    return new List<MEElemento>(vElementosDic.Values);
        //}
        #endregion

        #region ConExpedienteObjeto (Objs)
        /// <summary>
        /// Consulta paginada de la entidad ConExpedienteObjeto.
        /// </summary>
        public async Task<EConExpedienteObjetoPag> ConExpedienteObjetoPag(EConExpedienteObjetoFiltro conExpedienteObjetoFiltro)
        {
            return await _conexion.EntidadPagAsync<EConExpedienteObjeto,
                                                    EConExpedienteObjetoPag,
                                                    EConExpedienteObjetoFiltro>(conExpedienteObjetoFiltro, "NTExpObjsCP");
        }
        /// <summary>
        /// Permite insertar la entidad ConExpedienteObjeto.
        /// </summary>
        protected async Task<Int64> ConExpedienteObjetoInserta(EConExpedienteObjeto conExpedienteObjeto)
        {
            await _conexion.EntityUpdateAsync(conExpedienteObjeto, MAccionesBd.Inserta, "NTExpObjsIAE");
            return conExpedienteObjeto.ExpedienteObjetoId;
        }
        /// <summary>
        /// Permite actualizar la entidad ConExpedienteObjeto.
        /// </summary>
        protected async Task<Boolean> ConExpedienteObjetoActualiza(EConExpedienteObjeto conExpedienteObjeto)
        {
            return await _conexion.EntityUpdateAsync(conExpedienteObjeto, MAccionesBd.Actualiza, "NTExpObjsIAE");
        }
        /// <summary>
        /// Permite eliminar la entidad ConExpedienteObjeto.
        /// </summary>
        protected async Task<Boolean> ConExpedienteObjetoElimina(EConExpedienteObjeto conExpedienteObjeto)
        {
            return await _conexion.EntityUpdateAsync(conExpedienteObjeto, MAccionesBd.Elimina, "NTExpObjsIAE");
        }
        /// <summary>
        /// Acción personalizada SelArchivo.
        /// </summary>
        protected async Task<Boolean> ConExpedienteObjetoSelArchivo(EConExpedienteObjetoSelArchivo conExpedienteObjetoSelArchivo)
        {
            _conexion.AddParamIn(UsuarioSesion.UsuarioId, MMetaDatos.usuarioIdSesion);
            _conexion.AddParamEntity(conExpedienteObjetoSelArchivo);
            return await _conexion.ExecuteNonQueryRetAsync("NTExpObjsSelArchivo");
        }
        #endregion

        #region ExpedienteEstatu (ExpeEsta)
        /// <summary>
        /// Consulta paginada de la entidad ExpedienteEstatu.
        /// </summary>
        public async Task<EExpedienteEstatuPag> ExpedienteEstatuPag(EExpedienteEstatuFiltro expedienteEstatuFiltro)
        {
            return await _conexion.EntidadPagAsync<EExpedienteEstatu,
                                                    EExpedienteEstatuPag,
                                                    EExpedienteEstatuFiltro>(expedienteEstatuFiltro, "NTExpExpeEstaCP");
        }
        /// <summary>
        /// Consulta del utlimo estatus del expediente.
        /// </summary>
        /// <param name="expedienteId"></param>
        /// <returns></returns>
        public async Task<EExpedienteEstatu> ExpedienteEstatusUltimo(Int64 expedienteId)
        {
            _conexion.AddParamIn(expedienteId);
            return await _conexion.LoadEntityAsync<EExpedienteEstatu>("NTExpExpeEstaUltimo");
        }
        #endregion

        #endregion
    }
}
