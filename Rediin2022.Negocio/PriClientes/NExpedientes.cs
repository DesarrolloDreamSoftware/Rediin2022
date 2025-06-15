using DSEntityNetX.Common.Casting;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Comun;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Rediin2022.AccesoDatos.PriClientes;
using Rediin2022.Comun.PriOperacion;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriClientes.Expedientes;
using Rediin2022.Entidades.PriOperacion;
using Sisegui2020.Entidades.PriSeguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rediin2022.Negocio.PriClientes
{
    public class NExpedientes : RExpedientes, INExpedientes
    {
        #region Constructores
        public NExpedientes(IMConexionEntidad conexionEntidad,
                            INConExpedientes nConExpedientes,
                            INProcesosOperativos nProcesosOperativos)
            : base(conexionEntidad)
        {
            NConExpedientes = nConExpedientes;
            NProcesosOperativos = nProcesosOperativos;
        }
        #endregion

        #region Propiedades
        public INConExpedientes NConExpedientes { get; }
        public INProcesosOperativos NProcesosOperativos { get; }
        #endregion

        #region Funciones

        #region Expediente
        public async Task<Int64> ExpedienteInserta(EExpediente expediente)
        {
            //Validamos
            if(!ExpedienteValida(expediente))
                return 0L;

            List<EProcesoOperativoCol> vColumnas =
                await NProcesosOperativos.ProcesoOperativoColCT(expediente.ProcesoOperativoId);

            if (!ProcOperColValida(vColumnas, expediente))
                return 0L;

            //Proceso
            EConExpediente vConExp = new EConExpediente();
            vConExp.ProcesoOperativoId = expediente.ProcesoOperativoId;
            vConExp.ExpedienteId = 0; //Es insercion
            CargaDatosConExp(vColumnas, expediente.Valores, 0, vConExp.Valores);
           
            return await NConExpedientes.ConExpedienteInserta(vConExp);
        }
        public async Task<Boolean> ExpedienteActualiza(EExpediente expediente)
        {
            //Validamos
            if(!ExpedienteValida(expediente))
                return false;
           
            List<EProcesoOperativoCol> vColumnas =
                await NProcesosOperativos.ProcesoOperativoColCT(expediente.ProcesoOperativoId);

            if (!ProcOperColValida(vColumnas, expediente))
                return false;

            //Proceso
            EConExpediente vConExp = new EConExpediente();
            vConExp.ProcesoOperativoId = expediente.ProcesoOperativoId;
            vConExp.ExpedienteId = expediente.ExpendienteId;
            vConExp.PermiteActualiza = true;
            CargaDatosConExp(vColumnas, expediente.Valores, expediente.ExpendienteId, vConExp.Valores);

            return await NConExpedientes.ConExpedienteActualiza(vConExp);
        }
        public async Task<Boolean> ExpedienteElimina(Int64 expedienteId)
        {
            return await NConExpedientes.ConExpedienteElimina(new EConExpediente()
            {
                ExpedienteId = expedienteId
            });
        }
        protected Boolean ExpedienteValida(EExpediente expediente)
        {
            if (expediente.ProcesoOperativoId <= 0)
                Mensajes.AddError("El [{0}] no es valido.", MensajesXId.ProcesoOperativoId);

            //if (expediente.ExpendienteId <= 0)
            //    Mensajes.AddError("El [{0}] no es valido.", MensajesXId.ExpedienteId);

            if (expediente.Valores == null || expediente.Valores.Count == 0)
                Mensajes.AddError("No especificó los valores.");

            return Mensajes.Ok;
        }
        protected Boolean ProcOperColValida(List<EProcesoOperativoCol> columnas, EExpediente expediente)
        {
            foreach (EProcesoOperativoCol vCol in columnas)
            {
                Boolean vExiste = false;
                foreach (EExpendienteValor vVal in expediente.Valores)
                {
                    if (vVal.ColumnaId == vCol.ColumnaId)
                        vExiste = true;
                }

                if (!vExiste)
                    Mensajes.AddError("La columna [{0}] no fue agregada a Valores.", vCol.ColumnaId);
            }

            return Mensajes.Ok;
        }
        protected void CargaDatosConExp(List<EProcesoOperativoCol> procOperColumnas,
                                        List<EExpendienteValor> expValores,
                                        Int64 expedienteId,
                                        List<EConExpValores> conExpValores)
        {
            foreach (EProcesoOperativoCol vCol in procOperColumnas)
            {
                foreach (EExpendienteValor vVal in expValores)
                {
                    if (vVal.ColumnaId == vCol.ColumnaId)
                    {
                        var vValExp = new EConExpValores()
                        {
                            ExpedienteId = expedienteId,
                            ColumnaId = vCol.ColumnaId
                        };
                        if (String.IsNullOrWhiteSpace(vVal.Valor))
                            vVal.Valor = String.Empty;

                        if (vCol.Tipo == TiposColumna.Texto)
                            vValExp.ValorTexto = vVal.Valor;
                        else if (vCol.Tipo == TiposColumna.Boleano)
                            vValExp.ValorTexto = (vVal.Valor == "1" || vVal.Valor == "true" ? "1" : String.Empty);
                        else if (vCol.Tipo == TiposColumna.Entero || vCol.Tipo == TiposColumna.Importe)
                            vValExp.ValorNumerico = XString.XToDecimal(vVal.Valor);
                        else if (vCol.Tipo == TiposColumna.Fecha || vCol.Tipo == TiposColumna.FechaYHora)
                            vValExp.ValorFecha = XString.XToDateTime(vVal.Valor);
                        else if (vCol.Tipo == TiposColumna.Hora)
                        {
                            DateTime vFecha = XString.XToDateTime(vVal.Valor);
                            if (vFecha == DateTime.MinValue && vVal.Valor.Contains(":"))
                            {
                                String[] vVals = vVal.Valor.Split(":");
                                Int32 vHora = 0;
                                Int32 vMinuto = 0;
                                if (vVals.Length > 0)
                                    vHora = XString.XToInt32(vVals[0]);
                                if (vVals.Length > 1)
                                    vMinuto = XString.XToInt32(vVals[1]);
                                vFecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, vHora, vMinuto, 0);
                            }

                            if (vFecha > DateTime.MinValue)
                                vValExp.ValorFecha = vFecha;
                        }

                        conExpValores.Add(vValExp);
                    }
                }
            }
        }
        #endregion

        #region Objeto        
        public async Task<Int64> ObjetoInserta(EExpedienteObjeto expedienteObjeto)
        {
            if (!ObjetoValida(expedienteObjeto))
                return 0L;

            //Proceso
            EConExpedienteObjeto vConExpedienteObjeto = new EConExpedienteObjeto();
            vConExpedienteObjeto.ExpedienteId = expedienteObjeto.ExpedienteId;
            vConExpedienteObjeto.ProcesoOperativoObjetoId = expedienteObjeto.ProcesoOperativoObjetoId;
            vConExpedienteObjeto.ArchivoNombre = expedienteObjeto.ArchivoNombre;
            vConExpedienteObjeto.Ruta = expedienteObjeto.Ruta;
            vConExpedienteObjeto.Activo = expedienteObjeto.Activo;
            return await NConExpedientes.ConExpedienteObjetoInserta(vConExpedienteObjeto);
        }
        /// <summary>
        /// Sube el documento y modifica su nombre.
        /// Es necesario cargar los campos ExpedienteId, ExpedienteObjetoId, ArchivoNombre y Archivo
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public async Task<Boolean> ObjetoActualiza(EExpedienteObjeto expedienteObjeto)
        {
            if (!ObjetoValida(expedienteObjeto))
                return false;

            //Proceso
            EConExpedienteObjeto vConExpedienteObjeto = new EConExpedienteObjeto();
            vConExpedienteObjeto.ExpedienteId = expedienteObjeto.ExpedienteId;
            vConExpedienteObjeto.ExpedienteObjetoId = expedienteObjeto.ExpedienteObjetoId;
            vConExpedienteObjeto.ProcesoOperativoObjetoId = expedienteObjeto.ProcesoOperativoObjetoId;
            vConExpedienteObjeto.ArchivoNombre = expedienteObjeto.ArchivoNombre;
            vConExpedienteObjeto.Ruta = expedienteObjeto.Ruta;
            vConExpedienteObjeto.Activo = expedienteObjeto.Activo;
            return await NConExpedientes.ConExpedienteObjetoActualiza(vConExpedienteObjeto);
        }

        /// <summary>
        /// Descargar solo el documento
        /// </summary>
        /// <param name="expendienteId"></param>
        /// <param name="archivoNombre"></param>
        /// <returns></returns>
        public async Task<EDocumento> ObjectoDescargaDocto(Int64 expendienteId, String archivoNombre)
        {
            return await Task.FromResult(new EDocumento()); //Solo para el Api
        }
        /// <summary>
        /// Sube el documento sin modificar datos
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public async Task<Boolean> ObjetoSubeDocto(EDocumento documento)
        {
            return await Task.FromResult(true);
        }
        protected Boolean ObjetoValida(EExpedienteObjeto expedienteObjeto)
        {
            //Validacion
            if (expedienteObjeto.ExpedienteId <= 0)
                Mensajes.AddError("El [{0}] no es valido.", MensajesXId.ExpedienteId);

            if (expedienteObjeto.ProcesoOperativoObjetoId <= 0)
                Mensajes.AddError("El [{0}] no es valido.", MensajesXId.ProcesoOperativoObjetoId);

            if (!expedienteObjeto.Eliminar)
            {
                if (String.IsNullOrWhiteSpace(expedienteObjeto.ArchivoNombre))
                    Mensajes.AddError("El [{0}] no es valido.", MensajesXId.ArchivoNombre);

                if (expedienteObjeto.Archivo == null)
                    Mensajes.AddError("No se envio un archivo junto con el expediente.");
            }

            return Mensajes.Ok;
        }
        ///// <summary>
        ///// Listados para cargar los combos con expedientes de procesos operativos que son catalogos
        ///// </summary>
        ///// <param name="expendienteDatCmb"></param>
        ///// <returns></returns>
        //public async Task<List<MEElemento>> ConExpedienteCmb(EExpendienteDatCmb expendienteDatCmb)
        //{
        //    return await NConExpedientes.ConExpedienteCmb(new EProcesoOperativoCol()
        //    {
        //        CapCmbProcesoOperativoId = expendienteDatCmb.CapCmbProcesoOperativoId,
        //        CapCmbIdColumnaId = expendienteDatCmb.CapCmbIdColumnaId,
        //        CapCmbTextoColumnaId = expendienteDatCmb.CapCmbTextoColumnaId
        //    });
        //}
        #endregion

        #endregion
    }
}
