using DSEntityNetX.Business;
using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Entidades;
using DSMetodNetX.Idioma;
using DSMetodNetX.Negocio;
using Rediin2022.AccesoDatos.PriOperacion;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriOperacion;
using System;
using System.Collections.Generic;

namespace Rediin2022.Negocio.PriOperacion
{
    public class NConExpedientes : RConExpedientes, INConExpedientes
    {
        #region Variables
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        private IMReglasNeg<EConExpediente> _conExpedienteReglas = null;
        private IMReglasNeg<EConExpedienteObjeto> _conExpedienteObjetoReglas = null;
        #endregion

        #region Constructores
        public NConExpedientes(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region ConExpProcOperativo (Enc)
        #endregion

        #region ConExpediente (Exp)
        /// <summary>
        /// Consulta paginada de la entidad ConExpediente.
        /// </summary>
        public new EConExpedientePag ConExpedientePag(EConExpedienteFiltro conExpedienteFiltro)
        {
            EConExpedientePag vConExpedientePag = base.ConExpedientePag(conExpedienteFiltro);
            if (vConExpedientePag.Pagina != null)
            {
                foreach (EConExpediente vConExpediente in vConExpedientePag.Pagina)
                {
                    //Permite Actualiza
                    vConExpediente.PermiteActualiza = ConExpedientePermiteActualiza(vConExpediente);
                    //Permite IrAExpedienteEstatu
                    vConExpediente.ControlEstatus = conExpedienteFiltro.ControlEstatus; //Adi
                    vConExpediente.PermiteIrAExpedienteEstatu = ConExpedientePermiteIrAExpedienteEstatu(vConExpediente);
                }
            }

            return vConExpedientePag;
        }
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new Int64 ConExpedienteInserta(EConExpediente conExpediente)
        {
            //Validacion
            if (!ConExpedienteValida(conExpediente))
                return 0L;

            //Persistencia
            return base.ConExpedienteInserta(conExpediente);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new Boolean ConExpedienteActualiza(EConExpediente conExpediente)
        {
            //Validacion
            if (!ConExpedienteValida(conExpediente))
                return false;

            //Persistencia
            return base.ConExpedienteActualiza(conExpediente);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new Boolean ConExpedienteElimina(EConExpediente conExpediente)
        {
            //Validacion
            ConExpedienteReglasNeg().ValidateProperty(conExpediente, e => e.ExpedienteId);
            if (!_mensajes.Ok)
                return false;

            //Persistencia
            return base.ConExpedienteElimina(conExpediente);
        }
        /// <summary>
        /// Acción personalizada CambioEstatus.
        /// </summary>
        public new Boolean ConExpedienteCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
        {
            return base.ConExpedienteCambioEstatus(conExpedienteCambioEstatus);
        }
        public List<MEReglaNeg> ConExpedienteReglas()
        {
            return ConExpedienteReglasNeg().Rules;
        }
        private Boolean ConExpedienteValida(EConExpediente conExpediente)
        {
            _mensajes.Initialize();
            if (!ConExpedienteReglasNeg().Validate(conExpediente))
                return false;

            //Validaciones adicionales

            return _mensajes.Ok;
        }
        private IMReglasNeg<EConExpediente> ConExpedienteReglasNeg()
        {
            if (_conExpedienteReglas != null)
                return _conExpedienteReglas;

            _conExpedienteReglas = Validaciones.CreaReglasNeg<EConExpediente>(_mensajes);
            _conExpedienteReglas.AddSL(e => e.ExpedienteId, 0L, Validaciones._int64Max, false); // Consecutivo

            return _conExpedienteReglas;
        }
        /// <summary>
        /// Permite indicar si se permite la acción Actualiza.
        /// </summary>
        private Boolean ConExpedientePermiteActualiza(EConExpediente conExpediente)
        {
            return conExpediente.PermiteModificar;
        }
        /// <summary>
        /// Permite indicar si se permite la acción IrAExpedienteEstatu.
        /// </summary>
        private Boolean ConExpedientePermiteIrAExpedienteEstatu(EConExpediente conExpediente)
        {
            return conExpediente.ControlEstatus;
        }
        #endregion

        #region ConExpedienteObjeto (Objs)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new Int64 ConExpedienteObjetoInserta(EConExpedienteObjeto conExpedienteObjeto)
        {
            //Validacion
            if (!ConExpedienteObjetoValida(conExpedienteObjeto))
                return 0L;

            //Persistencia
            return base.ConExpedienteObjetoInserta(conExpedienteObjeto);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new Boolean ConExpedienteObjetoElimina(EConExpedienteObjeto conExpedienteObjeto)
        {
            //Validacion
            ConExpedienteObjetoReglasNeg().ValidateProperty(conExpedienteObjeto, e => e.ExpedienteObjetoId);
            if (!_mensajes.Ok)
                return false;

            //Persistencia
            return base.ConExpedienteObjetoElimina(conExpedienteObjeto);
        }
        //Eli
        ///// <summary>
        ///// Acción personalizada Descarga.
        ///// </summary>
        //public new Boolean ConExpedienteObjetoDescarga()
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Acción personalizada SelArchivo.
        /// </summary>
        public new Boolean ConExpedienteObjetoSelArchivo(EConExpedienteObjetoSelArchivo conExpedienteObjetoSelArchivo)
        {
            //Validaciones
            Validaciones.Valida(_mensajes, conExpedienteObjetoSelArchivo.ArchivoNombre, MensajesXId.ArchivoNombre, 0, 200, false);

            if (!_mensajes.Ok)
                return false;

            return base.ConExpedienteObjetoSelArchivo(conExpedienteObjetoSelArchivo);
        }
        public List<MEReglaNeg> ConExpedienteObjetoReglas()
        {
            return ConExpedienteObjetoReglasNeg().Rules;
        }
        private Boolean ConExpedienteObjetoValida(EConExpedienteObjeto conExpedienteObjeto)
        {
            _mensajes.Initialize();
            if (!ConExpedienteObjetoReglasNeg().Validate(conExpedienteObjeto))
                return false;

            //Validaciones adicionales

            return _mensajes.Ok;
        }
        private IMReglasNeg<EConExpedienteObjeto> ConExpedienteObjetoReglasNeg()
        {
            if (_conExpedienteObjetoReglas != null)
                return _conExpedienteObjetoReglas;

            _conExpedienteObjetoReglas = Validaciones.CreaReglasNeg<EConExpedienteObjeto>(_mensajes);
            _conExpedienteObjetoReglas.AddSL(e => e.ExpedienteObjetoId, 0L, Validaciones._int64Max, false); // Consecutivo
            _conExpedienteObjetoReglas.AddSL(e => e.ProcesoOperativoObjetoId, 0L, Validaciones._int64Max).MessageForRange = MMensajesXId.ValidaSeleccion;
            _conExpedienteObjetoReglas.AddSL(e => e.ArchivoNombre, 2, 200);
            _conExpedienteObjetoReglas.AddSL(e => e.Ruta, 2, 300);
            _conExpedienteObjetoReglas.AddSL(e => e.Activo);

            return _conExpedienteObjetoReglas;
        }

        #endregion

        #region ExpedienteEstatu (ExpeEsta)
        #endregion

        #endregion
    }
}
