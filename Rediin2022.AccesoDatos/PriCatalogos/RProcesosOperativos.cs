using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Comun;
using DSMetodNetX.Entidades;

using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Rediin2022.AccesoDatos.PriCatalogos
{
    [Serializable]
    public class RProcesosOperativos : MRepositorio
    {
        #region Variables
        /// <summary>
        /// Conexión.
        /// </summary>
        private IMConexionEntidad _conexion;
        #endregion

        #region Constructores
        public RProcesosOperativos(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Funciones

        #region ProcesoOperativo (ProcesosOperativos)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativo.
        /// </summary>
        protected async Task<EProcesoOperativoPag> ProcesoOperativoPag(EProcesoOperativoFiltro procesoOperativoFiltro)
        {
            return await _conexion.EntidadPagAsync<EProcesoOperativo,
                                                    EProcesoOperativoPag,
                                                    EProcesoOperativoFiltro>(procesoOperativoFiltro, "NCProcesosOperativosCP");

            //           EProcesoOperativoPag vProcesoOperativoPag = new EProcesoOperativoPag();

            //           _conexion.AddParamIn(MMetaDatos.establecimientoId, _conexion.UsuarioSesion.EstablecimientoId);
            //           _conexion.AddParamFilterTL(procesoOperativoFiltro);
            //await _conexion.LoadEntityAsync<EProcesoOperativoPag>("NCProcesosOperativosCP", vProcesoOperativoPag);
            //if (!Mensajes.Ok)
            //    return vProcesoOperativoPag;

            //base.MProcesaDatPag(procesoOperativoFiltro, vProcesoOperativoPag);

            //_conexion.AddParamIn(MMetaDatos.establecimientoId, _conexion.UsuarioSesion.EstablecimientoId);
            //_conexion.AddParamFilterPag(procesoOperativoFiltro);
            //vProcesoOperativoPag.Pagina = await _conexion.LoadEntitiesAsync<EProcesoOperativo>("NCProcesosOperativosCP");

            //return vProcesoOperativoPag;
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativo.
        /// </summary>
        protected async Task<EProcesoOperativo> ProcesoOperativoXId(Int64 procesoOperativoId)
        {
            _conexion.AddParamIn(procesoOperativoId);
            return await _conexion.LoadEntityAsync<EProcesoOperativo>("NCProcesosOperativosCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativo.
        /// </summary>
        public async Task<List<MEElemento>> ProcesoOperativoCmb()
        {
            return await _conexion.EntidadCmbAsync("NCProcesosOperativosCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativo.
        /// </summary>
        protected async Task<Int64> ProcesoOperativoInserta(EProcesoOperativo procesoOperativo)
        {
            await _conexion.EntityUpdateAsync(procesoOperativo, MAccionesBd.Inserta, "NCProcesosOperativosIAE");
            return procesoOperativo.ProcesoOperativoId;

            //           _conexion.AddParamEntity(procesoOperativo, MAccionesBd.Inserta);
            //Int64 vResultado = await _conexion.ExecuteScalarValAsync("NCProcesosOperativosIAE",
            //                                              MensajesXId.ProcesoOperativoNombre,
            //                                              MensajesXId.Orden);
            //return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativo.
        /// </summary>
        protected async Task<Boolean> ProcesoOperativoActualiza(EProcesoOperativo procesoOperativo)
        {
            return await _conexion.EntityUpdateAsync(procesoOperativo, MAccionesBd.Actualiza, "NCProcesosOperativosIAE");

            //           _conexion.AddParamEntity(procesoOperativo, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCProcesosOperativosIAE",
            //                           MensajesXId.ProcesoOperativoNombre,
            //                           MensajesXId.Orden);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativo.
        /// </summary>
        protected async Task<Boolean> ProcesoOperativoElimina(EProcesoOperativo procesoOperativo)
        {
            return await _conexion.EntityUpdateAsync(procesoOperativo, MAccionesBd.Elimina, "NCProcesosOperativosIAE");

            //_conexion.AddParamEntity(procesoOperativo, MAccionesBd.Elimina);
            ////Mod
            //Int64 vResultado = await _conexion.ExecuteScalarValAsync("NCProcesosOperativosIAE");
            //if (vResultado == -101)
            //    Mensajes.AddError("El proceso operativo ya contiene expedientes, no se puede eliminar");

            //return Mensajes.Ok;
        }
        #endregion

        #region ProcesoOperativoCol (ProcesosOperativosCols)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoCol.
        /// </summary>
        public async Task<EProcesoOperativoColPag> ProcesoOperativoColPag(EProcesoOperativoColFiltro procesoOperativoColFiltro)
        {
            return await _conexion.EntidadPagAsync<EProcesoOperativoCol,
                                                    EProcesoOperativoColPag,
                                                    EProcesoOperativoColFiltro>(procesoOperativoColFiltro, "NCProcesosOperativosColsCP");

            //EProcesoOperativoColPag vProcesoOperativoColPag = new EProcesoOperativoColPag();

            //_conexion.AddParamFilterTL(procesoOperativoColFiltro);
            //await _conexion.LoadEntityAsync<EProcesoOperativoColPag>("NCProcesosOperativosColsCP", vProcesoOperativoColPag);
            //if (!Mensajes.Ok)
            //    return vProcesoOperativoColPag;

            //base.MProcesaDatPag(procesoOperativoColFiltro, vProcesoOperativoColPag);

            //_conexion.AddParamFilterPag(procesoOperativoColFiltro);
            //vProcesoOperativoColPag.Pagina = await _conexion.LoadEntitiesAsync<EProcesoOperativoCol>("NCProcesosOperativosColsCP");

            //return vProcesoOperativoColPag;
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoCol.
        /// </summary>
        public async Task<EProcesoOperativoCol> ProcesoOperativoColXId(Int64 procesoOperativoId,
                                                                       Int64 columnaId)
        {
            _conexion.AddParamIn(procesoOperativoId);
            _conexion.AddParamIn(columnaId);
            return await _conexion.LoadEntityAsync<EProcesoOperativoCol>("NCProcesosOperativosColsCI");
        }
        /// <summary>
        /// Consulta adicional de la entidad ProcesoOperativoCol.
        /// </summary>
        public async Task<List<EProcesoOperativoCol>> ProcesoOperativoColCT(Int64 procesoOperativoId)
        {
            _conexion.AddParamIn(procesoOperativoId);
            return await _conexion.LoadEntitiesAsync<EProcesoOperativoCol>("NCProcesosOperativosColsCT");
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoCol.
        /// </summary>
        public async Task<List<MEElemento>> ProcesoOperativoColCmb(Int64 procesoOperativoId)
        {
            _conexion.AddParamInOpt(procesoOperativoId);
            return await _conexion.EntidadCmbAsync("NCProcesosOperativosColsCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoCol.
        /// </summary>
        protected async Task<Int64> ProcesoOperativoColInserta(EProcesoOperativoCol procesoOperativoCol)
        {
            await _conexion.EntityUpdateAsync(procesoOperativoCol, MAccionesBd.Inserta, "NCProcesosOperativosColsIAE");
            return procesoOperativoCol.ColumnaId;

            //_conexion.AddParamEntity(procesoOperativoCol, MAccionesBd.Inserta);
            //Int64 vResultado = await _conexion.ExecuteScalarValAsync("NCProcesosOperativosColsIAE",
            //                                              MensajesXId.Etiqueta);
            //return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoCol.
        /// </summary>
        protected async Task<Boolean> ProcesoOperativoColActualiza(EProcesoOperativoCol procesoOperativoCol)
        {
            return await _conexion.EntityUpdateAsync(procesoOperativoCol, MAccionesBd.Actualiza, "NCProcesosOperativosColsIAE");

            //_conexion.AddParamEntity(procesoOperativoCol, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCProcesosOperativosColsIAE",
            //                           MensajesXId.Etiqueta);
            //return Mensajes.Ok;

        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoCol.
        /// </summary>
        protected async Task<Boolean> ProcesoOperativoColElimina(EProcesoOperativoCol procesoOperativoCol)
        {
            return await _conexion.EntityUpdateAsync(procesoOperativoCol, MAccionesBd.Elimina, "NCProcesosOperativosColsIAE",
                vResultado =>
                {
                    if (vResultado == -101)
                        Mensajes.AddError("Ya existen expedientes para el proceso operativo, no se puede eliminar la columna");
                });

            //_conexion.AddParamEntity(procesoOperativoCol, MAccionesBd.Elimina);
            //Int64 vResultado = await _conexion.ExecuteScalarValAsync("NCProcesosOperativosColsIAE");
            //if (vResultado == -101)
            //    Mensajes.AddError("Ya existen expedientes para el proceso operativo, no se puede eliminar la columna");

            //return Mensajes.Ok;
        }
        #endregion

        #region ProcesoOperativoObjeto (ProcesosOperativosObjetos)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoObjeto.
        /// </summary>
        public async Task<EProcesoOperativoObjetoPag> ProcesoOperativoObjetoPag(EProcesoOperativoObjetoFiltro procesoOperativoObjetoFiltro)
        {
            return await _conexion.EntidadPagAsync<EProcesoOperativoObjeto,
                                                    EProcesoOperativoObjetoPag,
                                                    EProcesoOperativoObjetoFiltro>(procesoOperativoObjetoFiltro, "NCProcesosOperativosObjetosCP");

            //EProcesoOperativoObjetoPag vProcesoOperativoObjetoPag = new EProcesoOperativoObjetoPag();

            //_conexion.AddParamFilterTL(procesoOperativoObjetoFiltro);
            //await _conexion.LoadEntityAsync<EProcesoOperativoObjetoPag>("NCProcesosOperativosObjetosCP", vProcesoOperativoObjetoPag);
            //if (!Mensajes.Ok)
            //    return vProcesoOperativoObjetoPag;

            //base.MProcesaDatPag(procesoOperativoObjetoFiltro, vProcesoOperativoObjetoPag);

            //_conexion.AddParamFilterPag(procesoOperativoObjetoFiltro);
            //vProcesoOperativoObjetoPag.Pagina = await _conexion.LoadEntitiesAsync<EProcesoOperativoObjeto>("NCProcesosOperativosObjetosCP");

            //return vProcesoOperativoObjetoPag;
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoObjeto.
        /// </summary>
        public async Task<EProcesoOperativoObjeto> ProcesoOperativoObjetoXId(Int64 procesoOperativoObjetoId)
        {
            _conexion.AddParamIn(procesoOperativoObjetoId);
            return await _conexion.LoadEntityAsync<EProcesoOperativoObjeto>("NCProcesosOperativosObjetosCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoObjeto.
        /// </summary>
        public async Task<List<MEElemento>> ProcesoOperativoObjetoCmb(Int64 procesoOperativoId)
        {
            _conexion.AddParamInOpt(procesoOperativoId);
            return await _conexion.EntidadCmbAsync("NCProcesosOperativosObjetosCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoObjeto.
        /// </summary>
        protected async Task<Int64> ProcesoOperativoObjetoInserta(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            await _conexion.EntityUpdateAsync(procesoOperativoObjeto, MAccionesBd.Inserta, "NCProcesosOperativosObjetosIAE");
            return procesoOperativoObjeto.ProcesoOperativoObjetoId;

            //_conexion.AddParamEntity(procesoOperativoObjeto, MAccionesBd.Inserta);
            //Int64 vResultado = await _conexion.ExecuteScalarValAsync("NCProcesosOperativosObjetosIAE",
            //                                              MensajesXId.ProcesoOperativoObjetoNombre);
            //return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoObjeto.
        /// </summary>
        protected async Task<Boolean> ProcesoOperativoObjetoActualiza(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            return await _conexion.EntityUpdateAsync(procesoOperativoObjeto, MAccionesBd.Actualiza, "NCProcesosOperativosObjetosIAE");

            //_conexion.AddParamEntity(procesoOperativoObjeto, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCProcesosOperativosObjetosIAE",
            //                           MensajesXId.ProcesoOperativoObjetoNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoObjeto.
        /// </summary>
        protected async Task<Boolean> ProcesoOperativoObjetoElimina(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            return await _conexion.EntityUpdateAsync(procesoOperativoObjeto, MAccionesBd.Elimina, "NCProcesosOperativosObjetosIAE",
                vResultado =>
                {
                    if (vResultado == -101)
                        Mensajes.AddError("El objeto ya se esta usando en un expediente, no se puede eliminar.");
                });

            //_conexion.AddParamEntity(procesoOperativoObjeto, MAccionesBd.Elimina);
            ////Mod
            //Int64 vResultado = await _conexion.ExecuteScalarValAsync("NCProcesosOperativosObjetosIAE");
            //if (vResultado == -101)
            //    Mensajes.AddError("El objeto ya se esta usando en un expediente, no se puede eliminar.");

            //return Mensajes.Ok;
        }
        #endregion

        #region ProcesoOperativoEst (ProcesosOperativosEst)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoEst.
        /// </summary>
        public async Task<EProcesoOperativoEstPag> ProcesoOperativoEstPag(EProcesoOperativoEstFiltro procesoOperativoEstFiltro)
        {
            return await _conexion.EntidadPagAsync<EProcesoOperativoEst,
                                                    EProcesoOperativoEstPag,
                                                    EProcesoOperativoEstFiltro>(procesoOperativoEstFiltro, "NCProcesosOperativosEstCP");

            //EProcesoOperativoEstPag vProcesoOperativoEstPag = new EProcesoOperativoEstPag();

            //_conexion.AddParamFilterTL(procesoOperativoEstFiltro);
            //await _conexion.LoadEntityAsync<EProcesoOperativoEstPag>("NCProcesosOperativosEstCP", vProcesoOperativoEstPag);
            //if (!Mensajes.Ok)
            //    return vProcesoOperativoEstPag;

            //base.MProcesaDatPag(procesoOperativoEstFiltro, vProcesoOperativoEstPag);

            //_conexion.AddParamFilterPag(procesoOperativoEstFiltro);
            //vProcesoOperativoEstPag.Pagina = await _conexion.LoadEntitiesAsync<EProcesoOperativoEst>("NCProcesosOperativosEstCP");

            //return vProcesoOperativoEstPag;
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoEst.
        /// </summary>
        public async Task<EProcesoOperativoEst> ProcesoOperativoEstXId(Int64 procesoOperativoEstId)
        {
            _conexion.AddParamIn(procesoOperativoEstId);
            return await _conexion.LoadEntityAsync<EProcesoOperativoEst>("NCProcesosOperativosEstCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoEst.
        /// </summary>
        public async Task<List<MEElemento>> ProcesoOperativoEstCmb(Int64 procesoOperativoId)
        {
            _conexion.AddParamInOpt(procesoOperativoId);
            return await _conexion.EntidadCmbAsync("NCProcesosOperativosEstCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoEst.
        /// </summary>
        protected async Task<Int64> ProcesoOperativoEstInserta(EProcesoOperativoEst procesoOperativoEst)
        {
            await _conexion.EntityUpdateAsync(procesoOperativoEst, MAccionesBd.Inserta, "NCProcesosOperativosEstIAE");
            return procesoOperativoEst.ProcesoOperativoEstId;

            //_conexion.AddParamEntity(procesoOperativoEst, MAccionesBd.Inserta);
            //Int64 vResultado = await _conexion.ExecuteScalarValAsync("NCProcesosOperativosEstIAE",
            //                                              MensajesXId.EstatusNombre);
            //return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoEst.
        /// </summary>
        protected async Task<Boolean> ProcesoOperativoEstActualiza(EProcesoOperativoEst procesoOperativoEst)
        {
            return await _conexion.EntityUpdateAsync(procesoOperativoEst, MAccionesBd.Actualiza, "NCProcesosOperativosEstIAE");

            //_conexion.AddParamEntity(procesoOperativoEst, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCProcesosOperativosEstIAE",
            //                           MensajesXId.EstatusNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoEst.
        /// </summary>
        protected async Task<Boolean> ProcesoOperativoEstElimina(EProcesoOperativoEst procesoOperativoEst)
        {
            return await _conexion.EntityUpdateAsync(procesoOperativoEst, MAccionesBd.Elimina, "NCProcesosOperativosEstIAE");

            //_conexion.AddParamEntity(procesoOperativoEst, MAccionesBd.Elimina);
            //return await _conexion.ExecuteNonQueryRetAsync("NCProcesosOperativosEstIAE");
        }
        #endregion

        #region ProcesoOperativoEstSec (ProcesosOperativosEstSec)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoEstSec.
        /// </summary>
        public async Task<EProcesoOperativoEstSecPag> ProcesoOperativoEstSecPag(EProcesoOperativoEstSecFiltro procesoOperativoEstSecFiltro)
        {
            return await _conexion.EntidadPagAsync<EProcesoOperativoEstSec,
                                                    EProcesoOperativoEstSecPag,
                                                    EProcesoOperativoEstSecFiltro>(procesoOperativoEstSecFiltro, "NCProcesosOperativosEstSecCP");

            //EProcesoOperativoEstSecPag vProcesoOperativoEstSecPag = new EProcesoOperativoEstSecPag();

            //_conexion.AddParamFilterTL(procesoOperativoEstSecFiltro);
            //await _conexion.LoadEntityAsync<EProcesoOperativoEstSecPag>("NCProcesosOperativosEstSecCP", vProcesoOperativoEstSecPag);
            //if (!Mensajes.Ok)
            //    return vProcesoOperativoEstSecPag;

            //base.MProcesaDatPag(procesoOperativoEstSecFiltro, vProcesoOperativoEstSecPag);

            //_conexion.AddParamFilterPag(procesoOperativoEstSecFiltro);
            //vProcesoOperativoEstSecPag.Pagina = await _conexion.LoadEntitiesAsync<EProcesoOperativoEstSec>("NCProcesosOperativosEstSecCP");

            //return vProcesoOperativoEstSecPag;
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoEstSec.
        /// </summary>
        public async Task<EProcesoOperativoEstSec> ProcesoOperativoEstSecXId(Int64 procesoOperativoEstSecId)
        {
            _conexion.AddParamIn(procesoOperativoEstSecId);
            return await _conexion.LoadEntityAsync<EProcesoOperativoEstSec>("NCProcesosOperativosEstSecCI");
        }
        /// <summary>
        /// Consulta adicional de la entidad ProcesoOperativoEstSec.
        /// </summary>
        public async Task<List<EProcesoOperativoEstSec>> ProcesoOperativoEstSecCTXIdPadre(Int64 procesoOperativoEstId)
        {
            _conexion.AddParamIn(procesoOperativoEstId);
            return await _conexion.LoadEntitiesAsync<EProcesoOperativoEstSec>("NCProcesosOperativosEstSecCTXIdPadre");
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoEstSec.
        /// </summary>
        protected async Task<Int64> ProcesoOperativoEstSecInserta(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            await _conexion.EntityUpdateAsync(procesoOperativoEstSec, MAccionesBd.Inserta, "NCProcesosOperativosEstSecIAE",
                vResultado =>
                {
                    //Adi
                    if (vResultado == -101)
                        Mensajes.AddError("Solo hasta dos estatus se permiten indicar para la secuencia.");
                });
            return procesoOperativoEstSec.ProcesoOperativoEstSecId;

            //_conexion.AddParamEntity(procesoOperativoEstSec, MAccionesBd.Inserta);
            //Int64 vResultado = await _conexion.ExecuteScalarValAsync("NCProcesosOperativosEstSecIAE",
            //                                              MensajesXId.ProcesoOperativoEstIdSig);

            ////Adi
            //if (vResultado == -101)
            //    Mensajes.AddError("Solo hasta dos estatus se permiten indicar para la secuencia.");

            //return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoEstSec.
        /// </summary>
        protected async Task<Boolean> ProcesoOperativoEstSecActualiza(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            return await _conexion.EntityUpdateAsync(procesoOperativoEstSec, MAccionesBd.Actualiza, "NCProcesosOperativosEstSecIAE");

            //_conexion.AddParamEntity(procesoOperativoEstSec, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCProcesosOperativosEstSecIAE",
            //                           MensajesXId.ProcesoOperativoEstIdSig);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoEstSec.
        /// </summary>
        protected async Task<Boolean> ProcesoOperativoEstSecElimina(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            return await _conexion.EntityUpdateAsync(procesoOperativoEstSec, MAccionesBd.Elimina, "NCProcesosOperativosEstSecIAE");

            //_conexion.AddParamEntity(procesoOperativoEstSec, MAccionesBd.Elimina);
            //return await _conexion.ExecuteNonQueryRetAsync("NCProcesosOperativosEstSecIAE");
        }
        #endregion

        #endregion
    }
}
