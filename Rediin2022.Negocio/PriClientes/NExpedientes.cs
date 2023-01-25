using DSEntityNetX.Common.Casting;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Rediin2022.AccesoDatos.PriClientes;
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
using System.Threading.Tasks;

namespace Rediin2022.Negocio.PriClientes
{
    public class NExpedientes : RExpendientes, INExpedientes
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
        public IMMensajes Mensajes
        {
            get { return NProcesosOperativos.Mensajes; }
        }
        #endregion

        #region Funciones

        #region Funciones para el cliente
        public Int64 ExpedienteInserta(EExpediente expediente)
        {
            //Validamos
            if (expediente.ProcesoOperativoId <= 0)
                Mensajes.AddError("El [{0}] no es valido.", MensajesXId.ProcesoOperativoId);

            //if (expediente.ExpendienteId <= 0)
            //    Mensajes.AddError("El [{0}] no es valido.", MensajesXId.ExpedienteId);

            if (expediente.Valores == null || expediente.Valores.Count == 0)
                Mensajes.AddError("No especificó los valores.");

            if (!Mensajes.Ok)
                return 0L;

            List<EProcesoOperativoCol> vColumnas =
                NProcesosOperativos.ProcesoOperativoColCT(expediente.ProcesoOperativoId);

            foreach (EProcesoOperativoCol vCol in vColumnas)
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

            if (!Mensajes.Ok)
                return 0L;

            //Proceso
            EConExpediente vConExp = new EConExpediente();
            vConExp.ProcesoOperativoId = expediente.ProcesoOperativoId;
            vConExp.ExpedienteId = 0; //Es insercion
            foreach (EProcesoOperativoCol vCol in vColumnas)
            {
                foreach (EExpendienteValor vVal in expediente.Valores)
                {
                    if (vVal.ColumnaId == vCol.ColumnaId)
                    {
                        var vValExp = new EConExpValores()
                        {
                            ExpedienteId = 0,
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

                        vConExp.Valores.Add(vValExp);
                    }
                }
            }

            return NConExpedientes.ConExpedienteInserta(vConExp);
        }
        public Boolean ExpedienteActualiza(EExpediente expediente)
        {
            //Validamos
            if (expediente.ProcesoOperativoId <= 0)
                Mensajes.AddError("El [{0}] no es valido.", MensajesXId.ProcesoOperativoId);

            //if (expediente.ExpendienteId <= 0)
            //    Mensajes.AddError("El [{0}] no es valido.", MensajesXId.ExpedienteId);

            if (expediente.Valores == null || expediente.Valores.Count == 0)
                Mensajes.AddError("No especificó los valores.");

            if (!Mensajes.Ok)
                return false;

            List<EProcesoOperativoCol> vColumnas =
                NProcesosOperativos.ProcesoOperativoColCT(expediente.ProcesoOperativoId);

            foreach (EProcesoOperativoCol vCol in vColumnas)
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

            if (!Mensajes.Ok)
                return false;

            //Proceso
            EConExpediente vConExp = new EConExpediente();
            vConExp.ProcesoOperativoId = expediente.ProcesoOperativoId;
            vConExp.ExpedienteId = expediente.ExpendienteId;
            vConExp.PermiteActualiza = true;
            foreach (EProcesoOperativoCol vCol in vColumnas)
            {
                foreach (EExpendienteValor vVal in expediente.Valores)
                {
                    if (vVal.ColumnaId == vCol.ColumnaId)
                    {
                        var vValExp = new EConExpValores()
                        {
                            ExpedienteId = expediente.ExpendienteId,
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

                        vConExp.Valores.Add(vValExp);
                    }
                }
            }

            return NConExpedientes.ConExpedienteActualiza(vConExp);
        }
        public Boolean ExpedienteElimina(Int64 expedienteId)
        {
            return NConExpedientes.ConExpedienteElimina(new EConExpediente()
            {
                ExpedienteId = expedienteId
            });
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
        public Int64 ObjetoInserta(EExpedienteObjeto expedienteObjeto)
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
            return NConExpedientes.ConExpedienteObjetoInserta(vConExpedienteObjeto);
        }
        /// <summary>
        /// Sube el documento y modifica su nombre.
		/// Es necesario cargar los campos ExpedienteId, ExpedienteObjetoId, ArchivoNombre y Archivo
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public Boolean ObjetoActualiza(EExpedienteObjeto expedienteObjeto)
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
            return NConExpedientes.ConExpedienteObjetoActualiza(vConExpedienteObjeto);
        }

        /// <summary>
        /// Descargar solo el documento
		/// </summary>
		/// <param name="expendienteId"></param>
		/// <param name="archivoNombre"></param>
		/// <returns></returns>
		public EDocumento ObjectoDescargaDocto(Int64 expendienteId, String archivoNombre)
        {
            return new EDocumento(); //Solo para el Api
        }
        /// <summary>
        /// Sube el documento sin modificar datos
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public Boolean ObjetoSubeDocto(EDocumento documento)
        {
            return true;
        }
        /// <summary>
        /// Listados para cargar los combos con expedientes de procesos operativos que son catalogos
        /// </summary>
        /// <param name="expendienteDatCmb"></param>
        /// <returns></returns>
        public List<MEElemento> ConExpedienteCmb(EExpendienteDatCmb expendienteDatCmb)
        {
            return NConExpedientes.ConExpedienteCmb(new EProcesoOperativoCol()
            {
                CapCmbProcesoOperativoId = expendienteDatCmb.CapCmbProcesoOperativoId,
                CapCmbIdColumnaId = expendienteDatCmb.CapCmbIdColumnaId,
                CapCmbTextoColumnaId = expendienteDatCmb.CapCmbTextoColumnaId
            });
        }
        #endregion

        //No config Proveedor
        #region Funciones especificas para un proc operativo
        /// <summary>
        /// Regresa los datos del proveedor segun el usuario autentificado 
        /// para el proceso operativo especifico de proveedores.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public EDatosProveedor ProveedorXUsuario(Int64 procesoOperativoIdProveedor,
                                                 Int64 usuarioId)
        {
            EDatosProveedor vDP = new EDatosProveedor();

            //Obtenemos la relacion de las columnas con sus propiedades
            List<ERelacionProcOper> vRelaciones = base.RelacionProcesoOperativo(procesoOperativoIdProveedor);
            if (vRelaciones == null || vRelaciones.Count == 0)
                return vDP;

            //Obtenemos la columna de UsuarioId.
            Int64 vColUsuarioId = UtilExpediente.ObtenRelacion(vRelaciones, "UsuarioId").ColumnaId;
            if (vColUsuarioId == 0)
                return vDP;

            //Obtenemos el id de expediente del usuario.
            Int64 vExpendienteId = base.ProveedorExpedienteId(usuarioId,
                                                              procesoOperativoIdProveedor,
                                                              vColUsuarioId);
            if (vExpendienteId <= 0)
                return vDP;

            //Obtenemos los datos del expediente.
            EConExpediente vExpediente = NConExpedientes.ConExpedienteXId(vExpendienteId);
            if (vExpediente == null)
                return vDP;

            //Obtenemos los metadatos de las columnas
            List<EProcesoOperativoCol> vColMD =
                NProcesosOperativos.ProcesoOperativoColCT(procesoOperativoIdProveedor);

            //Cargamos las propiedades
            vDP.Proveedor = new EProveedor();
            foreach (ERelacionProcOper vRelacion in vRelaciones)
            {
                PropertyInfo vPI = vDP.Proveedor.GetType().GetProperty(vRelacion.Propiedad);
                if (vPI != null)
                    vPI.SetValue(vDP.Proveedor, UtilExpediente.ObtenValor(vColMD, vExpediente, vRelacion.ColumnaId, vPI.PropertyType));
            }

            //Cargamos el ultimo comentario
            EExpedienteEstatu vEstatusUlt = NConExpedientes.ExpedienteEstatusUltimo(vExpendienteId);
            vDP.Proveedor.Comentarios = vEstatusUlt.Comentarios;

            //Creamos las reglas de negocio
            vDP.ReglasNegocio = new List<MEReglaNeg>();
            vDP.Proveedor.ExpedienteId = vExpendienteId;
            vDP.Proveedor.ProcesoOperativoId = procesoOperativoIdProveedor;
            vDP.Proveedor.ProcesoOperativoEstId = vExpediente.ProcesoOperativoEstId;
            vDP.Proveedor.EstatusNombre = vExpediente.EstatusNombre;
            vDP.Proveedor.UsuarioId = usuarioId;
            foreach (EProcesoOperativoCol vPOC in vColMD)
            {
                vDP.ReglasNegocio.Add(new MEReglaNeg()
                {
                    Property = vRelaciones.FirstOrDefault(e => e.ColumnaId == vPOC.ColumnaId, new ERelacionProcOper()).Propiedad,
                    Label = vPOC.Etiqueta,
                    Required = vPOC.CapObligatorio,
                    RangeMin = vPOC.CapRangoIni,
                    RangeMax = vPOC.CapRangoFin,
                    Decimals = (vPOC.Tipo == TiposColumna.Importe ? vPOC.Decimales : 0)
                });
            }

            return vDP;
        }
        /// <summary>
        /// Actualiza el proveedor.
        /// </summary>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        public Boolean ProveedorActualiza(EProveedor proveedor)
        {
            EExpediente vExp = new EExpediente();
            vExp.ProcesoOperativoId = proveedor.ProcesoOperativoId;
            vExp.ExpendienteId = proveedor.ExpedienteId;

            List<ERelacionProcOper> vRelPO =
                RelacionProcesoOperativo(proveedor.ProcesoOperativoId);

            PropertyInfo[] vPIEnt = proveedor.GetType().GetProperties();
            foreach (ERelacionProcOper vRel in vRelPO)
            {
                PropertyInfo vPI = vPIEnt.FirstOrDefault(e => e.Name == vRel.Propiedad, null);
                if (vPI != null)
                {
                    String vValor = String.Empty;
                    Object vValorOrg = vPI.GetValue(proveedor);

                    if (vValorOrg == null)
                        vValor = String.Empty;
                    else if (vValorOrg is DateTime)
                        vValor = String.Format("{0:dd/MM/yyyy HH:mm:ss}", vValorOrg);
                    else if (vValorOrg is String)
                        vValor = (String)vValorOrg;
                    else
                        vValor = vValorOrg.ToString();

                    vExp.Valores.Add(new EExpendienteValor()
                    {
                        ColumnaId = vRel.ColumnaId,
                        Valor = vValor
                    }); ;
                }
            }

            return ExpedienteActualiza(vExp);
        }
        /// <summary>
        /// Pasa el expediente al siguiente estatus.
        /// </summary>
        /// <param name="expedienteId"></param>
        /// <returns></returns>
        public Boolean ProveedorCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
        {
            return NConExpedientes.ConExpedienteCambioEstatus(conExpedienteCambioEstatus);
        }
        #endregion
        //No config Proveedor

        #endregion
    }
}
