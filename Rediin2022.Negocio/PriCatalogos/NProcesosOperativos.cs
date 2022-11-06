using DSEntityNetX.Business;
using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Entidades;
using DSMetodNetX.Idioma;
using DSMetodNetX.Negocio;
using Rediin2022.AccesoDatos.PriCatalogos;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

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
        public new EProcesoOperativoPag ProcesoOperativoPag(EProcesoOperativoFiltro procesoOperativoFiltro)
        {
            EProcesoOperativoPag vProcesoOperativoPag = base.ProcesoOperativoPag(procesoOperativoFiltro);
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
        public EProcesoOperativo ProcesoOperativoXId(Int64 procesoOperativoId,
                                                     EProcesoOperativoFiltro procesoOperativoFiltro)
        {
            EProcesoOperativo vProcesoOperativo = base.ProcesoOperativoXId(procesoOperativoId);
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
        public new Int64 ProcesoOperativoInserta(EProcesoOperativo procesoOperativo)
        {
            procesoOperativo.EstablecimientoId = _conexion.UsuarioSesion.EstablecimientoId;

            //Validacion
            if (!ProcesoOperativoValida(procesoOperativo))
                return 0L;

            //Persistencia
            return base.ProcesoOperativoInserta(procesoOperativo);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new Boolean ProcesoOperativoActualiza(EProcesoOperativo procesoOperativo)
        {
            procesoOperativo.EstablecimientoId = _conexion.UsuarioSesion.EstablecimientoId;

            //Validacion
            if (!ProcesoOperativoValida(procesoOperativo))
                return false;

            //Persistencia
            return base.ProcesoOperativoActualiza(procesoOperativo);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new Boolean ProcesoOperativoElimina(EProcesoOperativo procesoOperativo)
        {
            //Validacion
            if (!ProcesoOperativoPermiteElimina(procesoOperativo))
            {
                _mensajes.AddError(MMensajesXId.PermiteEliminaErr);
                return false;
            }
            ProcesoOperativoReglasNeg().ValidateProperty(procesoOperativo, e => e.ProcesoOperativoId);
            if (!_mensajes.Ok)
                return false;

            //Persistencia
            return base.ProcesoOperativoElimina(procesoOperativo);
        }
        public List<MEReglaNeg> ProcesoOperativoReglas()
        {
            return ProcesoOperativoReglasNeg().Rules;
        }
        private Boolean ProcesoOperativoValida(EProcesoOperativo procesoOperativo)
        {
            _mensajes.Initialize();
            if (!ProcesoOperativoReglasNeg().Validate(procesoOperativo))
                return false;

            //Validaciones adicionales

            return _mensajes.Ok;
        }
        private IMReglasNeg<EProcesoOperativo> ProcesoOperativoReglasNeg()
        {
            if (_procesoOperativoReglas != null)
                return _procesoOperativoReglas;

            _procesoOperativoReglas = Validaciones.CreaReglasNeg<EProcesoOperativo>(_mensajes);
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
        public new Int64 ProcesoOperativoColInserta(EProcesoOperativoCol procesoOperativoCol)
        {
            //Validacion
            if (!ProcesoOperativoColValida(procesoOperativoCol))
                return 0L;

            //Persistencia
            return base.ProcesoOperativoColInserta(procesoOperativoCol);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new Boolean ProcesoOperativoColActualiza(EProcesoOperativoCol procesoOperativoCol)
        {
            //Validacion
            if (!ProcesoOperativoColValida(procesoOperativoCol))
                return false;

            //Persistencia
            return base.ProcesoOperativoColActualiza(procesoOperativoCol);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new Boolean ProcesoOperativoColElimina(EProcesoOperativoCol procesoOperativoCol)
        {
            //Validacion
            ProcesoOperativoColReglasNeg().ValidateProperty(procesoOperativoCol, e => e.ColumnaId);
            if (!_mensajes.Ok)
                return false;

            //Persistencia
            return base.ProcesoOperativoColElimina(procesoOperativoCol);
        }
        public List<MEReglaNeg> ProcesoOperativoColReglas()
        {
            return ProcesoOperativoColReglasNeg().Rules;
        }
        private Boolean ProcesoOperativoColValida(EProcesoOperativoCol procesoOperativoCol)
        {
            _mensajes.Initialize();
            if (!ProcesoOperativoColReglasNeg().Validate(procesoOperativoCol))
                return false;

            //Validaciones adicionales
            if ((procesoOperativoCol.CapColumnas + procesoOperativoCol.CapColumnasVacias) > 4)
                _mensajes.AddError("La suma de los campos [{0}] y [{1}] debe ser <= 4.",
                        MensajesXId.CapColumnas, MensajesXId.CapColumnasVacias);

            if (procesoOperativoCol.Tipo == TiposColumna.Texto &&
                procesoOperativoCol.ConLongitud > 0 &&
                procesoOperativoCol.ConLongitud > XString.XToInt32(procesoOperativoCol.CapRangoFin))
                _mensajes.AddError("El campo [{0}] debe ser <= al campo [{1}]",
                        MensajesXId.ConLongitud, MensajesXId.CapRangoFin);

            return _mensajes.Ok;
        }
        private IMReglasNeg<EProcesoOperativoCol> ProcesoOperativoColReglasNeg()
        {
            if (_procesoOperativoColReglas != null)
                return _procesoOperativoColReglas;

            _procesoOperativoColReglas = Validaciones.CreaReglasNeg<EProcesoOperativoCol>(_mensajes);
            _procesoOperativoColReglas.AddSL(e => e.ColumnaId, 0L, Validaciones._int64Max, false); // Consecutivo
            _procesoOperativoColReglas.AddSL(e => e.Etiqueta, 2, 100);
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
            _procesoOperativoColReglas.AddSL(e => e.Activo);

            return _procesoOperativoColReglas;
        }
        #endregion

        #region ProcesoOperativoObjeto (ProcesosOperativosObjetos)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new Int64 ProcesoOperativoObjetoInserta(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            //Validacion
            if (!ProcesoOperativoObjetoValida(procesoOperativoObjeto))
                return 0L;

            //Persistencia
            return base.ProcesoOperativoObjetoInserta(procesoOperativoObjeto);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new Boolean ProcesoOperativoObjetoActualiza(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            //Validacion
            if (!ProcesoOperativoObjetoValida(procesoOperativoObjeto))
                return false;

            //Persistencia
            return base.ProcesoOperativoObjetoActualiza(procesoOperativoObjeto);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new Boolean ProcesoOperativoObjetoElimina(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            //Validacion
            ProcesoOperativoObjetoReglasNeg().ValidateProperty(procesoOperativoObjeto, e => e.ProcesoOperativoObjetoId);
            if (!_mensajes.Ok)
                return false;

            //Persistencia
            return base.ProcesoOperativoObjetoElimina(procesoOperativoObjeto);
        }
        public List<MEReglaNeg> ProcesoOperativoObjetoReglas()
        {
            return ProcesoOperativoObjetoReglasNeg().Rules;
        }
        private Boolean ProcesoOperativoObjetoValida(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            _mensajes.Initialize();
            if (!ProcesoOperativoObjetoReglasNeg().Validate(procesoOperativoObjeto))
                return false;

            //Validaciones adicionales

            return _mensajes.Ok;
        }
        private IMReglasNeg<EProcesoOperativoObjeto> ProcesoOperativoObjetoReglasNeg()
        {
            if (_procesoOperativoObjetoReglas != null)
                return _procesoOperativoObjetoReglas;

            _procesoOperativoObjetoReglas = Validaciones.CreaReglasNeg<EProcesoOperativoObjeto>(_mensajes);
            _procesoOperativoObjetoReglas.AddSL(e => e.ProcesoOperativoObjetoId, 0L, Validaciones._int64Max, false); // Consecutivo
            _procesoOperativoObjetoReglas.AddSL(e => e.ProcesoOperativoObjetoNombre, 2, 150);
            _procesoOperativoObjetoReglas.AddSL(e => e.Cantidad, 0, Validaciones._int32Max);
            _procesoOperativoObjetoReglas.AddSL(e => e.Orden, (Int16)0, Validaciones._int16Max, false);
            _procesoOperativoObjetoReglas.AddSL(e => e.Obligatorio);
            _procesoOperativoObjetoReglas.AddSL(e => e.DiasVencimiento, (Int16)0, Validaciones._int16Max, false);
            _procesoOperativoObjetoReglas.AddSL(e => e.Activo);

            return _procesoOperativoObjetoReglas;
        }
        #endregion

        #region ProcesoOperativoEst (ProcesosOperativosEst)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new Int64 ProcesoOperativoEstInserta(EProcesoOperativoEst procesoOperativoEst)
        {
            //Validacion
            if (!ProcesoOperativoEstValida(procesoOperativoEst))
                return 0L;

            //Persistencia
            return base.ProcesoOperativoEstInserta(procesoOperativoEst);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new Boolean ProcesoOperativoEstActualiza(EProcesoOperativoEst procesoOperativoEst)
        {
            //Validacion
            if (!ProcesoOperativoEstValida(procesoOperativoEst))
                return false;

            //Persistencia
            return base.ProcesoOperativoEstActualiza(procesoOperativoEst);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new Boolean ProcesoOperativoEstElimina(EProcesoOperativoEst procesoOperativoEst)
        {
            //Validacion
            ProcesoOperativoEstReglasNeg().ValidateProperty(procesoOperativoEst, e => e.ProcesoOperativoEstId);
            if (!_mensajes.Ok)
                return false;

            //Persistencia
            return base.ProcesoOperativoEstElimina(procesoOperativoEst);
        }
        public List<MEReglaNeg> ProcesoOperativoEstReglas()
        {
            return ProcesoOperativoEstReglasNeg().Rules;
        }
        private Boolean ProcesoOperativoEstValida(EProcesoOperativoEst procesoOperativoEst)
        {
            _mensajes.Initialize();
            if (!ProcesoOperativoEstReglasNeg().Validate(procesoOperativoEst))
                return false;

            //Validaciones adicionales

            return _mensajes.Ok;
        }
        private IMReglasNeg<EProcesoOperativoEst> ProcesoOperativoEstReglasNeg()
        {
            if (_procesoOperativoEstReglas != null)
                return _procesoOperativoEstReglas;

            _procesoOperativoEstReglas = Validaciones.CreaReglasNeg<EProcesoOperativoEst>(_mensajes);
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
        public new Int64 ProcesoOperativoEstSecInserta(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            //Validacion
            if (!ProcesoOperativoEstSecValida(procesoOperativoEstSec))
                return 0L;

            //Persistencia
            return base.ProcesoOperativoEstSecInserta(procesoOperativoEstSec);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new Boolean ProcesoOperativoEstSecActualiza(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            //Validacion
            if (!ProcesoOperativoEstSecValida(procesoOperativoEstSec))
                return false;

            //Persistencia
            return base.ProcesoOperativoEstSecActualiza(procesoOperativoEstSec);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new Boolean ProcesoOperativoEstSecElimina(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            //Validacion
            ProcesoOperativoEstSecReglasNeg().ValidateProperty(procesoOperativoEstSec, e => e.ProcesoOperativoEstSecId);
            if (!_mensajes.Ok)
                return false;

            //Persistencia
            return base.ProcesoOperativoEstSecElimina(procesoOperativoEstSec);
        }
        public List<MEReglaNeg> ProcesoOperativoEstSecReglas()
        {
            return ProcesoOperativoEstSecReglasNeg().Rules;
        }
        private Boolean ProcesoOperativoEstSecValida(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            _mensajes.Initialize();
            if (!ProcesoOperativoEstSecReglasNeg().Validate(procesoOperativoEstSec))
                return false;

            //Validaciones adicionales
            if (procesoOperativoEstSec.ProcesoOperativoEstIdSig == procesoOperativoEstSec.ProcesoOperativoEstId)
            {
                _mensajes.AddError("No se puede seleccionar el estatus de origen.");
            }

            return _mensajes.Ok;
        }
        private IMReglasNeg<EProcesoOperativoEstSec> ProcesoOperativoEstSecReglasNeg()
        {
            if (_procesoOperativoEstSecReglas != null)
                return _procesoOperativoEstSecReglas;

            _procesoOperativoEstSecReglas = Validaciones.CreaReglasNeg<EProcesoOperativoEstSec>(_mensajes);
            _procesoOperativoEstSecReglas.AddSL(e => e.ProcesoOperativoEstSecId, 0L, Validaciones._int64Max, false); // Consecutivo
            _procesoOperativoEstSecReglas.AddSL(e => e.ProcesoOperativoEstIdSig, 0L, Validaciones._int64Max).MessageForRange = MMensajesXId.ValidaSeleccion;

            return _procesoOperativoEstSecReglas;
        }
        #endregion

        #endregion
    }
}
