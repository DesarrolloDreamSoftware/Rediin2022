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
EXEC PTSisOpcXMenuIAE 1, 1, 1101, NULL, 'Tratamientos', 'ALAAAE', 90, '/PriCatalogos/SapTratamientos/SapTratamientoInicia', '', 'SapTratamientoInicia', 0;

DECLARE @vJerarquiaSapTratamientos AS VARCHAR(400);
SELECT @vJerarquiaSapTratamientos = Jerarquia
FROM PTPermisos
WHERE MenuId = 1101 AND OpcionXMenuId = (SELECT TOP 1 OpcionXMenuId FROM @vTblOpcMenuId);

    /* **************************************************************************************************** */
    /* Permisos para la entidad SapTratamiento (SapTratamientos) */
    /* **************************************************************************************************** */
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar por id', @vJerarquiaSapTratamientos, NULL, 'SapTratamientoXId', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Insertar', @vJerarquiaSapTratamientos, NULL, 'SapTratamientoInserta', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Actualizar', @vJerarquiaSapTratamientos, NULL, 'SapTratamientoActualiza', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Eliminar', @vJerarquiaSapTratamientos, NULL, 'SapTratamientoElimina', 0, 0, 1;
    /* Exportacion a excel para la entidad SapTratamiento (SapTratamientos) */
    EXEC PTPermEncIAE 1, 1, 1101,NULL, 'Exportar datos', @vJerarquiaSapTratamientos, NULL, 'SapTratamientoExporta', 0, 0, 1;

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
WHERE   Jerarquia LIKE @vJerarquiaSapTratamientos + '%'
        AND ISNULL(pxp.PerfilId, 0) = 0


USE [Rediin2022]
GO
/* ******************************************************************************** */
/* Script para la entidad SapTratamiento (SapTratamientos) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 16:32:45
-- Descripcion: Consulta paginada del catalogo SapTratamiento (SapTratamientos)
-- ==========================================================================================
CREATE PROCEDURE NCSapTratamientosCP
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL,
 @FilSapTratamientoNombre AS VARCHAR(120) = NULL,
 @FilActivo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NCSapTratamientos saptra
        WHERE   (@FilSapTratamientoNombre IS NULL
                    OR saptra.SapTratamientoNombre LIKE '%' + @FilSapTratamientoNombre + '%')
                AND (@FilActivo IS NULL
                    OR saptra.Activo = @FilActivo)

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  saptra.SapTratamientoId,
                saptra.SapTratamientoNombre,
                saptra.Activo,
                saptra.UsuarioIdCreador,
                saptra.FechaCreacion,
                saptra.UsuarioIdUltMod,
                saptra.FechaUltMod
        FROM NCSapTratamientos saptra
        WHERE   (@FilSapTratamientoNombre IS NULL
                    OR saptra.SapTratamientoNombre LIKE '%' + @FilSapTratamientoNombre + '%')
                AND (@FilActivo IS NULL
                    OR saptra.Activo = @FilActivo)
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'SapTratamientoId' THEN t.SapTratamientoId END ASC,
        CASE WHEN @ColumnaOrden = '-SapTratamientoId' THEN t.SapTratamientoId END DESC,
        CASE WHEN @ColumnaOrden = 'SapTratamientoNombre' THEN t.SapTratamientoNombre END ASC,
        CASE WHEN @ColumnaOrden = '-SapTratamientoNombre' THEN t.SapTratamientoNombre END DESC,
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
-- Fecha:       10/01/2023 16:32:45
-- Descripcion: Mantenimiento del catalogo SapTratamiento (SapTratamientos)
-- ==========================================================================================
CREATE PROCEDURE NCSapTratamientosIAE
 @UsuarioIdSesion AS BIGINT,
 @AccionBD AS INT,
 @SapTratamientoId AS VARCHAR(50),
 @SapTratamientoNombre AS VARCHAR(120) = NULL,
 @Activo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();

    IF @AccionBD = 1 BEGIN

        IF EXISTS(SELECT SapTratamientoId
                  FROM NCSapTratamientos
                  WHERE SapTratamientoId = @SapTratamientoId) BEGIN
            SELECT 0; --Ya existe el registro con la llave.
            RETURN;
        END

        IF EXISTS(SELECT saptra.SapTratamientoNombre
                  FROM NCSapTratamientos saptra
                  WHERE saptra.SapTratamientoNombre = @SapTratamientoNombre) BEGIN
            SELECT -1; --El campo SapTratamientoNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        INSERT INTO NCSapTratamientos
        VALUES (@SapTratamientoId,
                @SapTratamientoNombre,
                @Activo,
                @UsuarioIdSesion,
                @vFecha,
                @UsuarioIdSesion,
                @vFecha);

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 2 BEGIN

        IF EXISTS(SELECT saptra.SapTratamientoNombre
                  FROM NCSapTratamientos saptra
                  WHERE saptra.SapTratamientoNombre = @SapTratamientoNombre
                        AND NOT (saptra.SapTratamientoId = @SapTratamientoId)) BEGIN
            SELECT -1; --El campo SapTratamientoNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        UPDATE NCSapTratamientos
        SET SapTratamientoNombre = @SapTratamientoNombre,
            Activo               = @Activo,
            UsuarioIdUltMod      = @UsuarioIdSesion,
            FechaUltMod          = @vFecha
        WHERE   SapTratamientoId = @SapTratamientoId;

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 3 BEGIN

        DELETE NCSapTratamientos
        WHERE   SapTratamientoId = @SapTratamientoId;

    END

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 16:32:45
-- Descripcion: Consulta individual del catalogo SapTratamiento (SapTratamientos)
-- ==========================================================================================
CREATE PROCEDURE NCSapTratamientosCI
 @SapTratamientoId AS VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta individual
    SELECT  saptra.SapTratamientoId,
            saptra.SapTratamientoNombre,
            saptra.Activo,
            saptra.UsuarioIdCreador,
            saptra.FechaCreacion,
            saptra.UsuarioIdUltMod,
            saptra.FechaUltMod
    FROM NCSapTratamientos saptra
    WHERE   saptra.SapTratamientoId = @SapTratamientoId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 16:32:45
-- Descripcion: Consulta de combo del catalogo SapTratamiento (SapTratamientos)
-- ==========================================================================================
CREATE PROCEDURE NCSapTratamientosCCmb
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta de combo
    SELECT  SapTratamientoId AS Id,
            SapTratamientoNombre AS Texto
    FROM NCSapTratamientos
    WHERE   Activo = 1
    ORDER BY SapTratamientoNombre;

END
GO
