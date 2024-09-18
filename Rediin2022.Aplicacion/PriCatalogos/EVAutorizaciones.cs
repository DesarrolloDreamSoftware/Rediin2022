using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;

using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Aplicacion.PriCatalogos
{
    [Serializable]
    public class EVAutorizaciones
    {
        #region Propiedades
        public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

        //Autorizacion (Autorizaciones)
        public MEVSF<EAutorizacion, EAutorizacionPag, EAutorizacionFiltro> Autorizacion { get; set; } = new();

        //public EAutorizacionPag AutorizacionPag { get; set; } = null;
        //public String AutorizacionColOrden { get; set; } = String.Empty;
        //public EAutorizacionFiltro AutorizacionFiltro { get; set; } = new EAutorizacionFiltro();
        //public Int32 AutorizacionIndice { get; set; } = 0;
        //public EAutorizacion AutorizacionSel { get; set; } = null;
        //public List<MEReglaNeg> AutorizacionReglas { get; set; } = null;

        //AutorizacionUsuario (AutorizacionesUsuarios)
        public MEVSF<EAutorizacionUsuario, EAutorizacionUsuarioPag, EAutorizacionUsuarioFiltro> AutorizacionUsuario { get; set; } = new();
        //public EAutorizacionUsuarioPag AutorizacionUsuarioPag { get; set; } = null;
        //public String AutorizacionUsuarioColOrden { get; set; } = String.Empty;
        //public EAutorizacionUsuarioFiltro AutorizacionUsuarioFiltro { get; set; } = new EAutorizacionUsuarioFiltro();
        //public Int32 AutorizacionUsuarioIndice { get; set; } = 0;
        //public EAutorizacionUsuario AutorizacionUsuarioSel { get; set; } = null;
        //public List<MEReglaNeg> AutorizacionUsuarioReglas { get; set; } = null;
        #endregion
    }
}
