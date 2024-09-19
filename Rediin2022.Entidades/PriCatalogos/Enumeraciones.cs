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
