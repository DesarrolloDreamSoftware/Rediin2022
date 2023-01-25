/* ************************************************** */
/* CREACION DEL ARBOL DE PERMISOS PARA MenuId = 1101     */
/* JERARQUIA ENCONTRADA ALAAAE                          */
/* ************************************************** */
USE [RediinSisegui2022]
GO

DECLARE @vJerarquiaAdminDoc AS VARCHAR(400);
DECLARE @vJerarquiaSelCol AS VARCHAR(400);
DECLARE @vTblPermisoId TABLE (PermisoId BIGINT);
DECLARE @vTblOpcMenuId TABLE (OpcionXMenuId BIGINT);

/* Insertamos la opción del menú principal */
INSERT INTO @vTblOpcMenuId
EXEC PTSisOpcXMenuIAE 1, 1, 1101, NULL, 'Organizaciones de compra', 'ALAAAE', 60, '/PriCatalogos/SapOrganizacionesCompra/SapOrganizacionCompraInicia', '', 'SapOrganizacionCompraInicia', 0;

DECLARE @vJerarquiaSapOrganizacionesCompra AS VARCHAR(400);
SELECT @vJerarquiaSapOrganizacionesCompra = Jerarquia
FROM PTPermisos
WHERE MenuId = 1101 AND OpcionXMenuId = (SELECT TOP 1 OpcionXMenuId FROM @vTblOpcMenuId);

    /* **************************************************************************************************** */
    /* Permisos para la entidad SapOrganizacionCompra (SapOrganizacionesCompra) */
    /* **************************************************************************************************** */
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar por id', @vJerarquiaSapOrganizacionesCompra, NULL, 'SapOrganizacionCompraXId', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Insertar', @vJerarquiaSapOrganizacionesCompra, NULL, 'SapOrganizacionCompraInserta', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Actualizar', @vJerarquiaSapOrganizacionesCompra, NULL, 'SapOrganizacionCompraActualiza', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Eliminar', @vJerarquiaSapOrganizacionesCompra, NULL, 'SapOrganizacionCompraElimina', 0, 0, 1;
    /* Exportacion a excel para la entidad SapOrganizacionCompra (SapOrganizacionesCompra) */
    EXEC PTPermEncIAE 1, 1, 1101,NULL, 'Exportar datos', @vJerarquiaSapOrganizacionesCompra, NULL, 'SapOrganizacionCompraExporta', 0, 0, 1;

/* *************************************************** */
/* Agraga los permisos para el usuario administrador   */
/* *************************************************** */
INSERT INTO PTPermisosXPerfil
SELECT  1, --PerfilId
        1101, --MenuId
        p.PermisoId,
        0, --RequiereAutorizacion
        0, --PuedeAutorizar
        3, --NivelConsultaId
        1, --TipoAudicion,
        1, --UsuarioCreador,
        GETDATE(), --FechaCreacion
        1, --UsuarioUltMod,
        GETDATE() --FechaUltMod
FROM PTPermisos p
LEFT JOIN PTPermisosXPerfil pxp
    ON pxp.PerfilId = 1
       AND pxp.MenuId = p.MenuId
       AND pxp.PermisoId = p.PermisoId
WHERE   Jerarquia LIKE @vJerarquiaSapOrganizacionesCompra + '%'
        AND ISNULL(pxp.PerfilId, 0) = 0


USE [Rediin2022]
GO
/* ******************************************************************************** */
/* Script para la entidad SapOrganizacionCompra (SapOrganizacionesCompra) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 16:04:18
-- Descripcion: Consulta paginada del catalogo SapOrganizacionCompra (SapOrganizacionesCompra)
-- ==========================================================================================
CREATE PROCEDURE NCSapOrganizacionesCompraCP
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL,
 @FilSapOrganizacionCompraNombre AS VARCHAR(120) = NULL,
 @FilActivo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NCSapOrganizacionesCompra saporgcom
        WHERE   (@FilSapOrganizacionCompraNombre IS NULL
                    OR saporgcom.SapOrganizacionCompraNombre LIKE '%' + @FilSapOrganizacionCompraNombre + '%')
                AND (@FilActivo IS NULL
                    OR saporgcom.Activo = @FilActivo)

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  saporgcom.SapOrganizacionCompraId,
                saporgcom.SapOrganizacionCompraNombre,
                saporgcom.Activo,
                saporgcom.UsuarioIdCreador,
                saporgcom.FechaCreacion,
                saporgcom.UsuarioIdUltMod,
                saporgcom.FechaUltMod
        FROM NCSapOrganizacionesCompra saporgcom
        WHERE   (@FilSapOrganizacionCompraNombre IS NULL
                    OR saporgcom.SapOrganizacionCompraNombre LIKE '%' + @FilSapOrganizacionCompraNombre + '%')
                AND (@FilActivo IS NULL
                    OR saporgcom.Activo = @FilActivo)
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'SapOrganizacionCompraId' THEN t.SapOrganizacionCompraId END ASC,
        CASE WHEN @ColumnaOrden = '-SapOrganizacionCompraId' THEN t.SapOrganizacionCompraId END DESC,
        CASE WHEN @ColumnaOrden = 'SapOrganizacionCompraNombre' THEN t.SapOrganizacionCompraNombre END ASC,
        CASE WHEN @ColumnaOrden = '-SapOrganizacionCompraNombre' THEN t.SapOrganizacionCompraNombre END DESC,
        CASE WHEN @ColumnaOrden = 'Activo' THEN t.Activo END ASC,
        CASE WHEN @ColumnaOrden = '-Activo' THEN t.Activo END DESC
    OFFSET @LinIni - 1 ROWS
    FETCH NEXT @TamPag ROWS ONLY

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 16:04:18
-- Descripcion: Mantenimiento del catalogo SapOrganizacionCompra (SapOrganizacionesCompra)
-- ==========================================================================================
CREATE PROCEDURE NCSapOrganizacionesCompraIAE
 @UsuarioIdSesion AS BIGINT,
 @AccionBD AS INT,
 @SapOrganizacionCompraId AS VARCHAR(50),
 @SapOrganizacionCompraNombre AS VARCHAR(120) = NULL,
 @Activo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();

    IF @AccionBD = 1 BEGIN

        IF EXISTS(SELECT SapOrganizacionCompraId
                  FROM NCSapOrganizacionesCompra
                  WHERE SapOrganizacionCompraId = @SapOrganizacionCompraId) BEGIN
            SELECT 0; --Ya existe el registro con la llave.
            RETURN;
        END

        IF EXISTS(SELECT saporgcom.SapOrganizacionCompraNombre
                  FROM NCSapOrganizacionesCompra saporgcom
                  WHERE saporgcom.SapOrganizacionCompraNombre = @SapOrganizacionCompraNombre) BEGIN
            SELECT -1; --El campo SapOrganizacionCompraNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        INSERT INTO NCSapOrganizacionesCompra
        VALUES (@SapOrganizacionCompraId,
                @SapOrganizacionCompraNombre,
                @Activo,
                @UsuarioIdSesion,
                @vFecha,
                @UsuarioIdSesion,
                @vFecha);

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 2 BEGIN

        IF EXISTS(SELECT saporgcom.SapOrganizacionCompraNombre
                  FROM NCSapOrganizacionesCompra saporgcom
                  WHERE saporgcom.SapOrganizacionCompraNombre = @SapOrganizacionCompraNombre
                        AND NOT (saporgcom.SapOrganizacionCompraId = @SapOrganizacionCompraId)) BEGIN
            SELECT -1; --El campo SapOrganizacionCompraNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        UPDATE NCSapOrganizacionesCompra
        SET SapOrganizacionCompraNombre = @SapOrganizacionCompraNombre,
            Activo                      = @Activo,
            UsuarioIdUltMod             = @UsuarioIdSesion,
            FechaUltMod                 = @vFecha
        WHERE   SapOrganizacionCompraId = @SapOrganizacionCompraId;

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 3 BEGIN

        DELETE NCSapOrganizacionesCompra
        WHERE   SapOrganizacionCompraId = @SapOrganizacionCompraId;

    END

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 16:04:18
-- Descripcion: Consulta individual del catalogo SapOrganizacionCompra (SapOrganizacionesCompra)
-- ==========================================================================================
CREATE PROCEDURE NCSapOrganizacionesCompraCI
 @SapOrganizacionCompraId AS VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta individual
    SELECT  saporgcom.SapOrganizacionCompraId,
            saporgcom.SapOrganizacionCompraNombre,
            saporgcom.Activo,
            saporgcom.UsuarioIdCreador,
            saporgcom.FechaCreacion,
            saporgcom.UsuarioIdUltMod,
            saporgcom.FechaUltMod
    FROM NCSapOrganizacionesCompra saporgcom
    WHERE   saporgcom.SapOrganizacionCompraId = @SapOrganizacionCompraId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 16:04:18
-- Descripcion: Consulta de combo del catalogo SapOrganizacionCompra (SapOrganizacionesCompra)
-- ==========================================================================================
CREATE PROCEDURE NCSapOrganizacionesCompraCCmb
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta de combo
    SELECT  SapOrganizacionCompraId AS Id,
            SapOrganizacionCompraNombre AS Texto
    FROM NCSapOrganizacionesCompra
    WHERE   Activo = 1
    ORDER BY SapOrganizacionCompraNombre;

END
GO
