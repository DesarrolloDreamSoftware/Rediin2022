using DSEntityNetX.Common.Casting;
using DSMetodNetX.Entidades;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriOperacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022.Negocio.PriClientes
{
    public class NExpedientes : INExpedientes
    {
        #region Constructores
        public NExpedientes(INConExpedientes nConExpedientes,
                            INProcesosOperativos nProcesosOperativos)
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
                            vValExp.ValorTexto = (vVal.Valor == "1" ? "1" : String.Empty);
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

            if (String.IsNullOrWhiteSpace(expedienteObjeto.ArchivoNombre))
                Mensajes.AddError("El [{0}] no es valido.", MensajesXId.ArchivoNombre);

            if (expedienteObjeto.Archivo == null)
                Mensajes.AddError("No se envio un archivo junto con el expediente.");

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
        #endregion
    }
}
