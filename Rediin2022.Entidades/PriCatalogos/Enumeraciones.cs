using System.ComponentModel;

namespace Rediin2022.Entidades.PriCatalogos;

public enum TiposColumna
{
    Ninguno,
    Texto,
    Boleano,
    Entero,
    Importe,
    Fecha,
    FechaYHora,
    Hora
}
public enum EsquemaObjetos
{
    Ninguno,
    [Description("Agregar objetos")]
    AgregarObjetos,
    [Description("Listado objetos")]
    ListadoObjetos
}
public enum TipoCaptura
{
    Ninguno,
    [Description("Person Física")]
    PersonaFisica,
    [Description("Persona Moral")]
    PersonaMoral,
    [Description("Persona Extranjera")]
    PersonaExtranjera
}
public enum Combos
{
    Ninguno,
    Paises,
    Estados,
    Municipios,
    Colonias,
    Bancos,
    Identificaciones,
    [Description("SAP Sociedades")]
    SAPSociedades,
    [Description("SAP Sociedades GL")]
    SAPSociedadesGL,
    [Description("Sap Grupo de Cuentas")]
    SapGrupoCuentas,
    [Description("Sap Organizaciones Compra")]
    SapOrganizacionesCompra,
    [Description("Sap Tratamientos")]
    SapTratamientos,
    [Description("Sap Cuentas Asociadas")]
    SapCuentasAsociadas,
    [Description("Sap Grupos Tesoreria")]
    SapGruposTesoreria,
    [Description("Sap Banco")]
    SapBanco,
    [Description("Sap CondicionPago")]
    SapCondicionPago,
    [Description("Sap ViaPago")]
    SapViaPago,
    [Description("Sap GrupoTolerancia")]
    SapGrupoTolerancia,
    Incoterms,
    [Description("Regimenes fiscales")]
    RegimenesFiscales,
    Modelos, 
    Monedas
}
