using DSEntityNetX.Business;
using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Comun;
using DSMetodNetX.Entidades;
using DSMetodNetX.Entidades.Idioma;

using DSMetodNetX.Negocio;
using Rediin2022.AccesoDatos.PriCatalogos;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Negocio.PriCatalogos
{
    public class NProcesosOperativos : RProcesosOperativos, INProcesosOperativos
    {
        #region Variables
        private IMReglasNeg<EProcesoOperativo> _procesoOperativoReglas = null;
        private IMReglasNeg<EProcesoOperativoCol> _procesoOperativoColReglas = null;
        private IMReglasNeg<EProcesoOperativoObjeto> _procesoOperativoObjetoReglas = null;
        private IMReglasNeg<EProcesoOperativoEst> _procesoOperativoEstReglas = null;
        private IMReglasNeg<EProcesoOperativoEstSec> _procesoOperativoEstSecReglas = null;
        #endregion

        #region Constructores
        public NProcesosOperativos(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region ProcesoOperativo (ProcesosOperativos)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativo.
        /// </summary>
        public new async Task<EProcesoOperativoPag> ProcesoOperativoPag(EProcesoOperativoFiltro procesoOperativoFiltro)
        {
            EProcesoOperativoPag vProcesoOperativoPag = await base.ProcesoOperativoPag(procesoOperativoFiltro);
            if (vProcesoOperativoPag.Pagina != null)
            {
                foreach (EProcesoOperativo vProcesoOperativo in vProcesoOperativoPag.Pagina)
                {
                    //Permite Elimina
                    vProcesoOperativo.PermiteElimina = ProcesoOperativoPermiteElimina(vProcesoOperativo);
                    //Permite IrAProcesoOperativoEst
                    vProcesoOperativo.PermiteIrAProcesoOperativoEst = ProcesoOperativoPermiteIrAProcesoOperativoEst(vProcesoOperativo);
                }
            }

            return vProcesoOperativoPag;
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativo.
        /// </summary>
        public async Task<EProcesoOperativo> ProcesoOperativoXId(Int64 procesoOperativoId,
                                                                 EProcesoOperativoFiltro procesoOperativoFiltro)
        {
            EProcesoOperativo vProcesoOperativo = await base.ProcesoOperativoXId(procesoOperativoId);
            if (vProcesoOperativo != null)
            {
                //Permite Elimina
                vProcesoOperativo.PermiteElimina = ProcesoOperativoPermiteElimina(vProcesoOperativo);
                //Permite IrAProcesoOperativoEst
                vProcesoOperativo.PermiteIrAProcesoOperativoEst = ProcesoOperativoPermiteIrAProcesoOperativoEst(vProcesoOperativo);
            }

            return vProcesoOperativo;
        }
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new async Task<Int64> ProcesoOperativoInserta(EProcesoOperativo procesoOperativo)
        {
            procesoOperativo.EstablecimientoId = UsuarioSesion.EstablecimientoId;

            //Validacion
            if (!ProcesoOperativoValida(procesoOperativo))
                return 0L;

            //Persistencia
            return await base.ProcesoOperativoInserta(procesoOperativo);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> ProcesoOperativoActualiza(EProcesoOperativo procesoOperativo)
        {
            procesoOperativo.EstablecimientoId = UsuarioSesion.EstablecimientoId;

            //Validacion
            if (!ProcesoOperativoValida(procesoOperativo))
                return false;

            //Persistencia
            return await base.ProcesoOperativoActualiza(procesoOperativo);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new async Task<Boolean> ProcesoOperativoElimina(EProcesoOperativo procesoOperativo)
        {
            //Validacion
            if (!ProcesoOperativoPermiteElimina(procesoOperativo))
            {
                Mensajes.AddError(MMensajesXId.PermiteEliminaErr);
                return false;
            }
            ProcesoOperativoReglasNeg().ValidateProperty(procesoOperativo, e => e.ProcesoOperativoId);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.ProcesoOperativoElimina(procesoOperativo);
        }
        public async Task<List<MEReglaNeg>> ProcesoOperativoReglas()
        {
            return await Task.Run(() => ProcesoOperativoReglasNeg().Rules);
        }
        private Boolean ProcesoOperativoValida(EProcesoOperativo procesoOperativo)
        {
            Mensajes.Initialize();
            if (!ProcesoOperativoReglasNeg().Validate(procesoOperativo))
                return false;

            //Validaciones adicionales

            return Mensajes.Ok;
        }
        private IMReglasNeg<EProcesoOperativo> ProcesoOperativoReglasNeg()
        {
            if (_procesoOperativoReglas != null)
                return _procesoOperativoReglas;

            _procesoOperativoReglas = Validaciones.CreaReglasNeg<EProcesoOperativo>(Mensajes);
            _procesoOperativoReglas.AddSL(e => e.ProcesoOperativoId, 0L, Validaciones._int64Max, false); // Consecutivo
            _procesoOperativoReglas.AddSL(e => e.ProcesoOperativoNombre, 2, 100);
            _procesoOperativoReglas.AddSL(e => e.Orden, (Int16)0, Validaciones._int16Max);
            _procesoOperativoReglas.AddSL(e => e.ControlEstatus);
            _procesoOperativoReglas.AddSL(e => e.EsquemaObjetos, EsquemaObjetos.Ninguno, EsquemaObjetos.ListadoObjetos);
            _procesoOperativoReglas.AddSL(e => e.Activo);

            return _procesoOperativoReglas;
        }
        /// <summary>
        /// Permite indicar si se permite la acción Elimina.
        /// </summary>
        private Boolean ProcesoOperativoPermiteElimina(EProcesoOperativo procesoOperativo)
        {
            return !procesoOperativo.TieneExpedientes;
        }
        /// <summary>
        /// Permite indicar si se permite la acción IrAProcesoOperativoEst.
        /// </summary>
        private Boolean ProcesoOperativoPermiteIrAProcesoOperativoEst(EProcesoOperativo procesoOperativo)
        {
            return procesoOperativo.ControlEstatus; //Mod == 1
        }
        #endregion

        #region ProcesoOperativoCol (ProcesosOperativosCols)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new async Task<Int64> ProcesoOperativoColInserta(EProcesoOperativoCol procesoOperativoCol)
        {
            //Validacion
            if (!ProcesoOperativoColValida(procesoOperativoCol))
                return 0L;

            //Persistencia
            return await base.ProcesoOperativoColInserta(procesoOperativoCol);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> ProcesoOperativoColActualiza(EProcesoOperativoCol procesoOperativoCol)
        {
            //Validacion
            if (!ProcesoOperativoColValida(procesoOperativoCol))
                return false;

            //Persistencia
            return await base.ProcesoOperativoColActualiza(procesoOperativoCol);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new async Task<Boolean> ProcesoOperativoColElimina(EProcesoOperativoCol procesoOperativoCol)
        {
            //Validacion
            ProcesoOperativoColReglasNeg().ValidateProperty(procesoOperativoCol, e => e.ColumnaId);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.ProcesoOperativoColElimina(procesoOperativoCol);
        }
        public async Task<List<MEReglaNeg>> ProcesoOperativoColReglas()
        {
            return await Task.Run(() => ProcesoOperativoColReglasNeg().Rules);
        }
        private Boolean ProcesoOperativoColValida(EProcesoOperativoCol procesoOperativoCol)
        {
            Mensajes.Initialize();
            if (!ProcesoOperativoColReglasNeg().Validate(procesoOperativoCol))
                return false;

            //Validaciones adicionales
            if ((procesoOperativoCol.CapColumnas + procesoOperativoCol.CapColumnasVacias) > 4)
                Mensajes.AddError("La suma de los campos [{0}] y [{1}] debe ser <= 4.",
                        MensajesXId.CapColumnas, MensajesXId.CapColumnasVacias);

            if (procesoOperativoCol.Tipo == TiposColumna.Texto &&
                procesoOperativoCol.ConLongitud > 0 &&
                procesoOperativoCol.ConLongitud > XString.XToInt32(procesoOperativoCol.CapRangoFin))
                Mensajes.AddError("El campo [{0}] debe ser <= al campo [{1}]",
                        MensajesXId.ConLongitud, MensajesXId.CapRangoFin);

            if (procesoOperativoCol.CapCmbProcesoOperativoId > 0)
            {
                if (procesoOperativoCol.CapCmbProcesoOperativoId == procesoOperativoCol.ProcesoOperativoId)
                    Mensajes.AddError("El proceso operativo del combo no puede ser hacia si mismo (seleccione otro proceso operativo en el combo).");
                else
                {
                    if (procesoOperativoCol.CapCmbIdColumnaId <= 0)
                        Mensajes.AddError("Debe elegir una columna de Clave para el combo.");
                    if (procesoOperativoCol.CapCmbTextoColumnaId <= 0)
                        Mensajes.AddError("Debe elegir una columna de Texto para el combo.");
                }
            }

            return Mensajes.Ok;
        }
        private IMReglasNeg<EProcesoOperativoCol> ProcesoOperativoColReglasNeg()
        {
            if (_procesoOperativoColReglas != null)
                return _procesoOperativoColReglas;

            _procesoOperativoColReglas = Validaciones.CreaReglasNeg<EProcesoOperativoCol>(Mensajes);
            _procesoOperativoColReglas.AddSL(e => e.ColumnaId, 0L, Validaciones._int64Max, false); // Consecutivo
            _procesoOperativoColReglas.AddSL(e => e.Etiqueta, 2, 100);
			_procesoOperativoColReglas.AddSL(e => e.Propiedad, 0, 100, false);
			_procesoOperativoColReglas.AddSL(e => e.Tipo, TiposColumna.Ninguno, TiposColumna.Hora);
            _procesoOperativoColReglas.AddSL(e => e.Decimales, (Int16)0, Validaciones._int16Max, false);
            _procesoOperativoColReglas.AddSL(e => e.ConOrden, 0, Validaciones._int32Max, false);
            _procesoOperativoColReglas.AddSL(e => e.ConOrdenar);
            _procesoOperativoColReglas.AddSL(e => e.ConLongitud, 0, Validaciones._int32Max, false);
            _procesoOperativoColReglas.AddSL(e => e.CapTab, 2, 120, false);
            _procesoOperativoColReglas.AddSL(e => e.CapOrden, 0, Validaciones._int32Max);
            _procesoOperativoColReglas.AddSL(e => e.CapColumnas, 0, 4);
            _procesoOperativoColReglas.AddSL(e => e.CapColumnasVacias, 0, 3, false);
            _procesoOperativoColReglas.AddSL(e => e.CapObligatorio);
            _procesoOperativoColReglas.AddSL(e => e.CapRangoIni, 0, 60, false);
            _procesoOperativoColReglas.AddSL(e => e.CapRangoFin, 0, 60, false);
            _procesoOperativoColReglas.AddSL(e => e.CapCmbProcesoOperativoId, 0L, Validaciones._int64Max, false).MessageForRange = MMensajesXId.ValidaSeleccion;
            _procesoOperativoColReglas.AddSL(e => e.CapCmbIdColumnaId, 0L, Validaciones._int64Max, false).MessageForRange = MMensajesXId.ValidaSeleccion;
            _procesoOperativoColReglas.AddSL(e => e.CapCmbTextoColumnaId, 0L, Validaciones._int64Max, false).MessageForRange = MMensajesXId.ValidaSeleccion;
            _procesoOperativoColReglas.AddSL(e => e.ComboId, Combos.Ninguno, Combos.Modelos, false);
            _procesoOperativoColReglas.AddSL(e => e.Activo);

			//Habilitado
			//_procesoOperativoColReglas.AddEnabled(e => e.Propiedad, e => UsuarioSesion.UsuarioId == 1 || UsuarioSesion.UsuarioId == 2);

			return _procesoOperativoColReglas;
        }
        #endregion

        #region ProcesoOperativoObjeto (ProcesosOperativosObjetos)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new async Task<Int64> ProcesoOperativoObjetoInserta(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            //Validacion
            if (!ProcesoOperativoObjetoValida(procesoOperativoObjeto))
                return 0L;

            //Persistencia
            return await base.ProcesoOperativoObjetoInserta(procesoOperativoObjeto);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> ProcesoOperativoObjetoActualiza(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            //Validacion
            if (!ProcesoOperativoObjetoValida(procesoOperativoObjeto))
                return false;

            //Persistencia
            return await base.ProcesoOperativoObjetoActualiza(procesoOperativoObjeto);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new async Task<Boolean> ProcesoOperativoObjetoElimina(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            //Validacion
            ProcesoOperativoObjetoReglasNeg().ValidateProperty(procesoOperativoObjeto, e => e.ProcesoOperativoObjetoId);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.ProcesoOperativoObjetoElimina(procesoOperativoObjeto);
        }
        public async Task<List<MEReglaNeg>> ProcesoOperativoObjetoReglas()
        {
            return await Task.Run(() => ProcesoOperativoObjetoReglasNeg().Rules);
        }
        private Boolean ProcesoOperativoObjetoValida(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            Mensajes.Initialize();
            if (!ProcesoOperativoObjetoReglasNeg().Validate(procesoOperativoObjeto))
                return false;

            //Validaciones adicionales

            return Mensajes.Ok;
        }
        private IMReglasNeg<EProcesoOperativoObjeto> ProcesoOperativoObjetoReglasNeg()
        {
            if (_procesoOperativoObjetoReglas != null)
                return _procesoOperativoObjetoReglas;

            _procesoOperativoObjetoReglas = Validaciones.CreaReglasNeg<EProcesoOperativoObjeto>(Mensajes);
            _procesoOperativoObjetoReglas.AddSL(e => e.ProcesoOperativoObjetoId, 0L, Validaciones._int64Max, false); // Consecutivo
            _procesoOperativoObjetoReglas.AddSL(e => e.ProcesoOperativoObjetoNombre, 2, 150);
            _procesoOperativoObjetoReglas.AddSL(e => e.Cantidad, 0, Validaciones._int32Max);
            _procesoOperativoObjetoReglas.AddSL(e => e.Orden, (Int16)0, Validaciones._int16Max, false);
            _procesoOperativoObjetoReglas.AddSL(e => e.Obligatorio);
            _procesoOperativoObjetoReglas.AddSL(e => e.DiasVencimiento, (Int16)0, Validaciones._int16Max, false);
            _procesoOperativoObjetoReglas.AddSL(e => e.TipoCapturaId, TipoCaptura.Ninguno, TipoCaptura.PersonaExtranjera, false);
            _procesoOperativoObjetoReglas.AddSL(e => e.Activo);

            return _procesoOperativoObjetoReglas;
        }
        #endregion

        #region ProcesoOperativoEst (ProcesosOperativosEst)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new async Task<Int64> ProcesoOperativoEstInserta(EProcesoOperativoEst procesoOperativoEst)
        {
            //Validacion
            if (!ProcesoOperativoEstValida(procesoOperativoEst))
                return 0L;

            //Persistencia
            return await base.ProcesoOperativoEstInserta(procesoOperativoEst);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> ProcesoOperativoEstActualiza(EProcesoOperativoEst procesoOperativoEst)
        {
            //Validacion
            if (!ProcesoOperativoEstValida(procesoOperativoEst))
                return false;

            //Persistencia
            return await base.ProcesoOperativoEstActualiza(procesoOperativoEst);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new async Task<Boolean> ProcesoOperativoEstElimina(EProcesoOperativoEst procesoOperativoEst)
        {
            //Validacion
            ProcesoOperativoEstReglasNeg().ValidateProperty(procesoOperativoEst, e => e.ProcesoOperativoEstId);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.ProcesoOperativoEstElimina(procesoOperativoEst);
        }
        public async Task<List<MEReglaNeg>> ProcesoOperativoEstReglas()
        {
            return await Task.Run(() => ProcesoOperativoEstReglasNeg().Rules);
        }
        private Boolean ProcesoOperativoEstValida(EProcesoOperativoEst procesoOperativoEst)
        {
            Mensajes.Initialize();
            if (!ProcesoOperativoEstReglasNeg().Validate(procesoOperativoEst))
                return false;

            //Validaciones adicionales

            return Mensajes.Ok;
        }
        private IMReglasNeg<EProcesoOperativoEst> ProcesoOperativoEstReglasNeg()
        {
            if (_procesoOperativoEstReglas != null)
                return _procesoOperativoEstReglas;

            _procesoOperativoEstReglas = Validaciones.CreaReglasNeg<EProcesoOperativoEst>(Mensajes);
            _procesoOperativoEstReglas.AddSL(e => e.ProcesoOperativoEstId, 0L, Validaciones._int64Max, false); // Consecutivo
            _procesoOperativoEstReglas.AddSL(e => e.EstatusNombre, 2, 50);
            _procesoOperativoEstReglas.AddSL(e => e.Orden, (Int16)0, Validaciones._int16Max, false);
            _procesoOperativoEstReglas.AddSL(e => e.PermiteModificar);
            _procesoOperativoEstReglas.AddSL(e => e.Activo);

            return _procesoOperativoEstReglas;
        }
        #endregion

        #region ProcesoOperativoEstSec (ProcesosOperativosEstSec)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new async Task<Int64> ProcesoOperativoEstSecInserta(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            //Validacion
            if (!ProcesoOperativoEstSecValida(procesoOperativoEstSec))
                return 0L;

            //Persistencia
            return await base.ProcesoOperativoEstSecInserta(procesoOperativoEstSec);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> ProcesoOperativoEstSecActualiza(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            //Validacion
            if (!ProcesoOperativoEstSecValida(procesoOperativoEstSec))
                return false;

            //Persistencia
            return await base.ProcesoOperativoEstSecActualiza(procesoOperativoEstSec);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new async Task<Boolean> ProcesoOperativoEstSecElimina(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            //Validacion
            ProcesoOperativoEstSecReglasNeg().ValidateProperty(procesoOperativoEstSec, e => e.ProcesoOperativoEstSecId);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.ProcesoOperativoEstSecElimina(procesoOperativoEstSec);
        }
        public async Task<List<MEReglaNeg>> ProcesoOperativoEstSecReglas()
        {
            return await Task.Run(() => ProcesoOperativoEstSecReglasNeg().Rules);
        }
        private Boolean ProcesoOperativoEstSecValida(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            Mensajes.Initialize();
            if (!ProcesoOperativoEstSecReglasNeg().Validate(procesoOperativoEstSec))
                return false;

            //Validaciones adicionales
            if (procesoOperativoEstSec.ProcesoOperativoEstIdSig == procesoOperativoEstSec.ProcesoOperativoEstId)
            {
                Mensajes.AddError("No se puede seleccionar el estatus de origen.");
            }

            return Mensajes.Ok;
        }
        private IMReglasNeg<EProcesoOperativoEstSec> ProcesoOperativoEstSecReglasNeg()
        {
            if (_procesoOperativoEstSecReglas != null)
                return _procesoOperativoEstSecReglas;

            _procesoOperativoEstSecReglas = Validaciones.CreaReglasNeg<EProcesoOperativoEstSec>(Mensajes);
            _procesoOperativoEstSecReglas.AddSL(e => e.ProcesoOperativoEstSecId, 0L, Validaciones._int64Max, false); // Consecutivo
            _procesoOperativoEstSecReglas.AddSL(e => e.ProcesoOperativoEstIdSig, 0L, Validaciones._int64Max).MessageForRange = MMensajesXId.ValidaSeleccion;

            return _procesoOperativoEstSecReglas;
        }
        #endregion

        #endregion
    }
}
