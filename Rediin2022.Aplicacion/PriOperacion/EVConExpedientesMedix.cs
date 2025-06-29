﻿using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Aplicacion.PriOperacion;

public class EVConExpedientesMedix
{
    public Int64 ParamEstIdCaptura { get; set; }
    //public Int64 ParamEstIdAutorizado { get; set; }
    public Int64 ParamEstIdEnTesoreria { get; set; }
    public Int64 ParamEstIdRevisado { get; set; }
    public Int64 ParamColumnaIdNombre { get; set; }
    public Int64 ParamColumnaIdCorreo { get; set; }
    public Int64 ParamPerfilIdNvoUsr { get; set; }
    public Int64 ColumnaIdUsuario { get; set; }
    public Int64 ColumnaIdProveedor { get; set; }
    public Int64 ColumnaIdModelo { get; set; }
    public String ParamUrlRediinProveedores { get; set; }

    //public string ApiSapUsuario { get; set; }
    //public string ApiSapPwd { get; set; }
    //public string ApiSapUrl { get; set; }

    public List<MEReglaNeg> ReglasSAP { get; set; } = null;
}

