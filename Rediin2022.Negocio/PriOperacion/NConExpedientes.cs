using DSEntityNetX.Business;
using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Comun;
using DSMetodNetX.Entidades;
using DSMetodNetX.Entidades.Idioma;
using DSMetodNetX.Negocio;
using Rediin2022.AccesoDatos.PriOperacion;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriOperacion;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public new async Task<EConExpedientePag> ConExpedientePag(EConExpedienteFiltro conExpedienteFiltro)
        {
            EConExpedientePag vConExpedientePag = await base.ConExpedientePag(conExpedienteFiltro);
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
        public new async Task<Int64> ConExpedienteInserta(EConExpediente conExpediente)
        {
            //Validacion
            if (!ConExpedienteValida(conExpediente))
                return 0L;

            //Persistencia
            return await base.ConExpedienteInserta(conExpediente);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> ConExpedienteActualiza(EConExpediente conExpediente)
        {
            //Validacion
            if (!ConExpedienteValida(conExpediente))
                return false;

            //Persistencia
            return await base.ConExpedienteActualiza(conExpediente);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new async Task<Boolean> ConExpedienteElimina(EConExpediente conExpediente)
        {
            //Validacion
            ConExpedienteReglasNeg().ValidateProperty(conExpediente, e => e.ExpedienteId);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.ConExpedienteElimina(conExpediente);
        }
        /// <summary>
        /// Acción personalizada CambioEstatus.
        /// </summary>
        public new async Task<Boolean> ConExpedienteCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
        {
            //Validaciones
            //Eli
            //Validaciones.Valida(Mensajes, conExpedienteCambioEstatus.Comentarios, MensajesXId.Comentarios, 2, 2000);

            //if (!Mensajes.Ok)
            //    return false;

            return await base.ConExpedienteCambioEstatus(conExpedienteCambioEstatus);
        }
        public async Task<List<MEReglaNeg>> ConExpedienteReglas()
        {
            return await Task.Run(() => ConExpedienteReglasNeg().Rules);
        }
        private Boolean ConExpedienteValida(EConExpediente conExpediente)
        {
            Mensajes.Initialize();
            if (!ConExpedienteReglasNeg().Validate(conExpediente))
                return false;

            //Validaciones adicionales

            return Mensajes.Ok;
        }
        private IMReglasNeg<EConExpediente> ConExpedienteReglasNeg()
        {
            if (_conExpedienteReglas != null)
                return _conExpedienteReglas;

            _conExpedienteReglas = Validaciones.CreaReglasNeg<EConExpediente>(Mensajes);
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
        ///// <summary>
        ///// Reglas de negocio de la entidad ConExpedienteObjeto.
        ///// </summary>
        //public Byte[] ConExpedienteDescarga(String entidad, Int32 expedienteId, String nombreArchivo)
  //      {
  //          return null;
  //      }

        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new async Task<Int64> ConExpedienteObjetoInserta(EConExpedienteObjeto conExpedienteObjeto)
        {
            //Validacion
            if (!ConExpedienteObjetoValida(conExpedienteObjeto))
                return 0L;

            //Persistencia
            return await base.ConExpedienteObjetoInserta(conExpedienteObjeto);
        }
        /// <summary>
        /// Permite actualizar la entidad ConExpedienteObjeto.
        /// </summary>
        /// <param name="conExpedienteObjeto"></param>
        /// <returns></returns>
        public new async Task<Boolean> ConExpedienteObjetoActualiza(EConExpedienteObjeto conExpedienteObjeto)
        {
            //Validacion
            //if (!ConExpedienteObjetoValida(conExpedienteObjeto))
            //    return false;

            //ConExpedienteObjetoReglasNeg().ValidateProperty(conExpedienteObjeto, e => e.ExpedienteId);
            ConExpedienteObjetoReglasNeg().ValidateProperty(conExpedienteObjeto, e => e.ExpedienteObjetoId);
            //Se permitira que el nombre este vacio para cuando se elimina el objeto.
            //ConExpedienteObjetoReglasNeg().ValidateProperty(conExpedienteObjeto, e => e.ArchivoNombre);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.ConExpedienteObjetoActualiza(conExpedienteObjeto);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new async Task<Boolean> ConExpedienteObjetoElimina(EConExpedienteObjeto conExpedienteObjeto)
        {
            //Validacion
            ConExpedienteObjetoReglasNeg().ValidateProperty(conExpedienteObjeto, e => e.ExpedienteObjetoId);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.ConExpedienteObjetoElimina(conExpedienteObjeto);
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
        public new async Task<Boolean> ConExpedienteObjetoSelArchivo(EConExpedienteObjetoSelArchivo conExpedienteObjetoSelArchivo)
        {
            //Validaciones
            Validaciones.Valida(Mensajes, conExpedienteObjetoSelArchivo.ArchivoNombre, MensajesXId.ArchivoNombre, 0, 200, false);

            if (!Mensajes.Ok)
                return false;

            return await base.ConExpedienteObjetoSelArchivo(conExpedienteObjetoSelArchivo);
        }
        public async Task<List<MEReglaNeg>> ConExpedienteObjetoReglas()
        {
            return await Task.Run(() => ConExpedienteObjetoReglasNeg().Rules);
        }
        private Boolean ConExpedienteObjetoValida(EConExpedienteObjeto conExpedienteObjeto)
        {
            Mensajes.Initialize();
            if (!ConExpedienteObjetoReglasNeg().Validate(conExpedienteObjeto))
                return false;

            //Validaciones adicionales

            return Mensajes.Ok;
        }
        private IMReglasNeg<EConExpedienteObjeto> ConExpedienteObjetoReglasNeg()
        {
            if (_conExpedienteObjetoReglas != null)
                return _conExpedienteObjetoReglas;

            _conExpedienteObjetoReglas = Validaciones.CreaReglasNeg<EConExpedienteObjeto>(Mensajes);
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
