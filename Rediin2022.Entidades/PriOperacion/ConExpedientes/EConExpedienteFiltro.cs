using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    public class EConExpedienteFiltro : MEFiltro
    {
        #region Propiedades
        public Int64 ProcesoOperativoId { get; set; } = 0L; //[Llave padre]

        //Columnas principales
        public Int64 FilExpedienteId { get; set; } = 0L;
        public Int64 FilProcesoOperativoEstId { get; set; } = 0L;

        [XExclude]
        public Boolean ControlEstatus { get; set; } = false; //[Para visible si]

        //Adi
        /// <summary>
        /// Este campo se calcula automaticamente por la capa de acceso a datos.
        /// </summary>
        public Boolean FilBusquedaActiva { get; set; } = false;
        /// <summary>
        /// Este campo es el unico que se debe asignar para indicar que se 
        /// requiere la busqueda en cualquier campo (excepto el de expediente).
        /// </summary>
        public String FilBusquedaTexto { get; set; } = String.Empty;
        /// <summary>
        /// Este campo se calcula automaticamente por la capa de acceso a datos.
        /// </summary>
        public Boolean FilBuscaNum { get; set; } = false;
        /// <summary>
        /// Este campo se calcula automaticamente por la capa de acceso a datos.
        /// </summary>
        public Decimal FilBusquedaNumero { get; set; } = 0;
        /// <summary>
        /// Este campo se calcula automaticamente por la capa de acceso a datos.
        /// </summary>
        public Boolean FilBuscaFec { get; set; } = false;
        /// <summary>
        /// Este campo se calcula automaticamente por la capa de acceso a datos.
        /// </summary>
        public DateTime FilBusquedaFecha { get; set; } = DateTime.MinValue;

        public Int64 ColumnaId { get; set; } = 0L;
        public Boolean Ascendente { get; set; } = true;
        #endregion
    }
}
