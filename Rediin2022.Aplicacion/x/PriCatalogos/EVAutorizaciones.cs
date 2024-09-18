using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.Aplicacion.PriCatalogos
{
    [Serializable]
    public class EVAutorizaciones
    {
        #region Propiedades
        public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

        //Autorizacion (Autorizaciones)
        public EAutorizacionPag AutorizacionPag { get; set; } = null;
        public String AutorizacionColOrden { get; set; } = String.Empty;
        public EAutorizacionFiltro AutorizacionFiltro { get; set; } = new EAutorizacionFiltro();
        public Int32 AutorizacionIndice { get; set; } = 0;
        public EAutorizacion AutorizacionSel { get; set; } = null;
        public List<MEReglaNeg> AutorizacionReglas { get; set; } = null;

        //AutorizacionUsuario (AutorizacionesUsuarios)
        public EAutorizacionUsuarioPag AutorizacionUsuarioPag { get; set; } = null;
        public String AutorizacionUsuarioColOrden { get; set; } = String.Empty;
        public EAutorizacionUsuarioFiltro AutorizacionUsuarioFiltro { get; set; } = new EAutorizacionUsuarioFiltro();
        public Int32 AutorizacionUsuarioIndice { get; set; } = 0;
        public EAutorizacionUsuario AutorizacionUsuarioSel { get; set; } = null;
        public List<MEReglaNeg> AutorizacionUsuarioReglas { get; set; } = null;
        #endregion
    }
}
