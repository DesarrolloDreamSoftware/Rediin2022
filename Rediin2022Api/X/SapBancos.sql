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
EXEC PTSisOpcXMenuIAE 1, 1, 1101, NULL, 'Bancos', 'ALAAAE', 110, '/PriCatalogos/SapBancos/SapBancoInicia', '', 'SapBancoInicia', 0;

DECLARE @vJerarquiaSapBancos AS VARCHAR(400);
SELECT @vJerarquiaSapBancos = Jerarquia
FROM PTPermisos
WHERE MenuId = 1101 AND OpcionXMenuId = (SELECT TOP 1 OpcionXMenuId FROM @vTblOpcMenuId);

    /* **************************************************************************************************** */
    /* Permisos para la entidad SapBanco (SapBancos) */
    /* **************************************************************************************************** */
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar por id', @vJerarquiaSapBancos, NULL, 'SapBancoXId', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Insertar', @vJerarquiaSapBancos, NULL, 'SapBancoInserta', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Actualizar', @vJerarquiaSapBancos, NULL, 'SapBancoActualiza', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Eliminar', @vJerarquiaSapBancos, NULL, 'SapBancoElimina', 0, 0, 1;
    /* Exportacion a excel para la entidad SapBanco (SapBancos) */
    EXEC PTPermEncIAE 1, 1, 1101,NULL, 'Exportar datos', @vJerarquiaSapBancos, NULL, 'SapBancoExporta', 0, 0, 1;

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
WHERE   Jerarquia LIKE @vJerarquiaSapBancos + '%'
        AND ISNULL(pxp.PerfilId, 0) = 0


USE [Rediin2022]
GO
/* ******************************************************************************** */
/* Script para la entidad SapBanco (SapBancos) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       24/01/2023 16:48:25
-- Descripcion: Consulta paginada del catalogo SapBanco (SapBancos)
-- ==========================================================================================
CREATE PROCEDURE NCSapBancosCP
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL,
 @FilSapBancoNombre AS VARCHAR(120) = NULL,
 @FilActivo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NCSapBancos sapban
        WHERE   (@FilSapBancoNombre IS NULL
                    OR sapban.SapBancoNombre LIKE '%' + @FilSapBancoNombre + '%'
                    OR CAST(sapban.SapBancoId AS VARCHAR) LIKE '%' + @FilSapBancoNombre + '%')
                AND (@FilActivo IS NULL
                    OR sapban.Activo = @FilActivo)

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  sapban.SapBancoId,
                sapban.SapBancoNombre,
                sapban.Activo,
                sapban.UsuarioIdCreador,
                sapban.FechaCreacion,
                sapban.UsuarioIdUltMod,
                sapban.FechaUltMod
        FROM NCSapBancos sapban
        WHERE   (@FilSapBancoNombre IS NULL
                    OR sapban.SapBancoNombre LIKE '%' + @FilSapBancoNombre + '%'
                    OR CAST(sapban.SapBancoId AS VARCHAR) LIKE '%' + @FilSapBancoNombre + '%')
                AND (@FilActivo IS NULL
                    OR sapban.Activo = @FilActivo)
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'SapBancoId' THEN t.SapBancoId END ASC,
        CASE WHEN @ColumnaOrden = '-SapBancoId' THEN t.SapBancoId END DESC,
        CASE WHEN @ColumnaOrden = 'SapBancoNombre' THEN t.SapBancoNombre END ASC,
        CASE WHEN @ColumnaOrden = '-SapBancoNombre' THEN t.SapBancoNombre END DESC,
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
-- Fecha:       24/01/2023 16:48:25
-- Descripcion: Mantenimiento del catalogo SapBanco (SapBancos)
-- ==========================================================================================
CREATE PROCEDURE NCSapBancosIAE
 @UsuarioIdSesion AS BIGINT,
 @AccionBD AS INT,
 @SapBancoId AS VARCHAR(50),
 @SapBancoNombre AS VARCHAR(120) = NULL,
 @Activo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();

    IF @AccionBD = 1 BEGIN

        IF EXISTS(SELECT SapBancoId
                  FROM NCSapBancos
                  WHERE SapBancoId = @SapBancoId) BEGIN
            SELECT 0; --Ya existe el registro con la llave.
            RETURN;
        END

        IF EXISTS(SELECT sapban.SapBancoNombre
                  FROM NCSapBancos sapban
                  WHERE sapban.SapBancoNombre = @SapBancoNombre) BEGIN
            SELECT -1; --El campo SapBancoNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        INSERT INTO NCSapBancos
        VALUES (@SapBancoId,
                @SapBancoNombre,
                @Activo,
                @UsuarioIdSesion,
                @vFecha,
                @UsuarioIdSesion,
                @vFecha);

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 2 BEGIN

        IF EXISTS(SELECT sapban.SapBancoNombre
                  FROM NCSapBancos sapban
                  WHERE sapban.SapBancoNombre = @SapBancoNombre
                        AND NOT (sapban.SapBancoId = @SapBancoId)) BEGIN
            SELECT -1; --El campo SapBancoNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        UPDATE NCSapBancos
        SET SapBancoId       = @SapBancoId,
            SapBancoNombre   = @SapBancoNombre,
            Activo           = @Activo,
            UsuarioIdUltMod  = @UsuarioIdSesion,
            FechaUltMod      = @vFecha
        WHERE   ;

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 3 BEGIN

        DELETE NCSapBancos
        WHERE   SapBancoId = @SapBancoId;

    END

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       24/01/2023 16:48:25
-- Descripcion: Consulta individual del catalogo SapBanco (SapBancos)
-- ==========================================================================================
CREATE PROCEDURE NCSapBancosCI
 @SapBancoId AS VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta individual
    SELECT  sapban.SapBancoId,
            sapban.SapBancoNombre,
            sapban.Activo,
            sapban.UsuarioIdCreador,
            sapban.FechaCreacion,
            sapban.UsuarioIdUltMod,
            sapban.FechaUltMod
    FROM NCSapBancos sapban
    WHERE   sapban.SapBancoId = @SapBancoId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       24/01/2023 16:48:25
-- Descripcion: Consulta de combo del catalogo SapBanco (SapBancos)
-- ==========================================================================================
CREATE PROCEDURE NCSapBancosCCmb
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta de combo
    SELECT  SapBancoId AS Id,
            SapBancoNombre AS Texto
    FROM NCSapBancos
    WHERE   Activo = 1
    ORDER BY SapBancoNombre;

END
GO
