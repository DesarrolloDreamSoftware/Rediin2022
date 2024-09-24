using System;
using System.Collections.Generic;
using System.Text;

namespace Rediin2022.Entidades.PriClientes;

public class EMedixApiProveedor
{
	public Int64 idCliente { get; set; }
	public string numeroIdenFiscal { get; set; }
	public string nombre1 { get; set; }
	public string nombre2 { get; set; }

	public string nombre3 { get; set; }

	public string clavePais { get; set; } 
	public string poblacion { get; set; }

	public string distrito { get; set; }

	public string cp { get; set; }

	public string region { get; set; }
	public string calle { get; set; }
	public string numeroInterior { get; set; }

	public string telefono1 { get; set; }

	public string telefono2 { get; set; }

	public string correo { get; set; }

	public string nombreNotario { get; set; }

	public string numeroEscritura { get; set; }

	public string fechaEscritura { get; set; }

	public string nombreRepresentante { get; set; }

	public string idenRepresentante { get; set; }
	public string numeroIdenRepresentante { get; set; }

	public string grupoCuentasAcreedor { get; set; }
	public string tipoFiscal { get; set; }
    public string sociedad { get; set; }

	public string organizacionCompra { get; set; }

	public string conceptoBusqueda1 { get; set; }
	public string conceptoBusqueda2 { get; set; }
	public string claveCondicionPago { get; set; }
	public string numeroRegistroAnterior { get; set; }
	public string claveMoneda { get; set; }
	public string vendedorResponsable { get; set; }
	public string incoTerms1 { get; set; }
	public string incoTerms2 { get; set; }
	public string curp { get; set; }
	public string regimenFiscal { get; set; }
	public List<EMedixApiCuenta> cuentas { get; set; } = new List<EMedixApiCuenta>();
	public string numeroProveedor { get; set; }
	public string respuestaSAPCodigo { get; set; }
	public string respuestaSAPEstatus { get; set; }
	public string respuestaSAPMensaje { get; set; }
}
