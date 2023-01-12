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
EXEC PTSisOpcXMenuIAE 1, 1, 1101, NULL, 'Sociedades GL', 'ALAAAE', 80, '/PriCatalogos/SapSociedadesGL/SapSociedadGLInicia', '', 'SapSociedadGLInicia', 0;

DECLARE @vJerarquiaSapSociedadesGL AS VARCHAR(400);
SELECT @vJerarquiaSapSociedadesGL = Jerarquia
FROM PTPermisos
WHERE MenuId = 1101 AND OpcionXMenuId = (SELECT TOP 1 OpcionXMenuId FROM @vTblOpcMenuId);

    /* **************************************************************************************************** */
    /* Permisos para la entidad SapSociedadGL (SapSociedadesGL) */
    /* **************************************************************************************************** */
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar por id', @vJerarquiaSapSociedadesGL, NULL, 'SapSociedadGLXId', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Insertar', @vJerarquiaSapSociedadesGL, NULL, 'SapSociedadGLInserta', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Actualizar', @vJerarquiaSapSociedadesGL, NULL, 'SapSociedadGLActualiza', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Eliminar', @vJerarquiaSapSociedadesGL, NULL, 'SapSociedadGLElimina', 0, 0, 1;
    /* Exportacion a excel para la entidad SapSociedadGL (SapSociedadesGL) */
    EXEC PTPermEncIAE 1, 1, 1101,NULL, 'Exportar datos', @vJerarquiaSapSociedadesGL, NULL, 'SapSociedadGLExporta', 0, 0, 1;

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
WHERE   Jerarquia LIKE @vJerarquiaSapSociedadesGL + '%'
        AND ISNULL(pxp.PerfilId, 0) = 0


USE [Rediin2022]
GO
/* ******************************************************************************** */
/* Script para la entidad SapSociedadGL (SapSociedadesGL) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 16:25:42
-- Descripcion: Consulta paginada del catalogo SapSociedadGL (SapSociedadesGL)
-- ==========================================================================================
CREATE PROCEDURE NCSapSociedadesGLCP
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL,
 @FilSapSociedadGLNombre AS VARCHAR(120) = NULL,
 @FilActivo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NCSapSociedadesGL sapsoc
        WHERE   (@FilSapSociedadGLNombre IS NULL
                    OR sapsoc.SapSociedadGLNombre LIKE '%' + @FilSapSociedadGLNombre + '%')
                AND (@FilActivo IS NULL
                    OR sapsoc.Activo = @FilActivo)

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  sapsoc.SapSociedadGLId,
                sapsoc.SapSociedadGLNombre,
                sapsoc.Activo,
                sapsoc.UsuarioIdCreador,
                sapsoc.FechaCreacion,
                sapsoc.UsuarioIdUltMod,
                sapsoc.FechaUltMod
        FROM NCSapSociedadesGL sapsoc
        WHERE   (@FilSapSociedadGLNombre IS NULL
                    OR sapsoc.SapSociedadGLNombre LIKE '%' + @FilSapSociedadGLNombre + '%')
                AND (@FilActivo IS NULL
                    OR sapsoc.Activo = @FilActivo)
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'SapSociedadGLId' THEN t.SapSociedadGLId END ASC,
        CASE WHEN @ColumnaOrden = '-SapSociedadGLId' THEN t.SapSociedadGLId END DESC,
        CASE WHEN @ColumnaOrden = 'SapSociedadGLNombre' THEN t.SapSociedadGLNombre END ASC,
        CASE WHEN @ColumnaOrden = '-SapSociedadGLNombre' THEN t.SapSociedadGLNombre END DESC,
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
-- Fecha:       10/01/2023 16:25:42
-- Descripcion: Mantenimiento del catalogo SapSociedadGL (SapSociedadesGL)
-- ==========================================================================================
CREATE PROCEDURE NCSapSociedadesGLIAE
 @UsuarioIdSesion AS BIGINT,
 @AccionBD AS INT,
 @SapSociedadGLId AS VARCHAR(50),
 @SapSociedadGLNombre AS VARCHAR(120) = NULL,
 @Activo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();

    IF @AccionBD = 1 BEGIN

        IF EXISTS(SELECT SapSociedadGLId
                  FROM NCSapSociedadesGL
                  WHERE SapSociedadGLId = @SapSociedadGLId) BEGIN
            SELECT 0; --Ya existe el registro con la llave.
            RETURN;
        END

        IF EXISTS(SELECT sapsoc.SapSociedadGLNombre
                  FROM NCSapSociedadesGL sapsoc
                  WHERE sapsoc.SapSociedadGLNombre = @SapSociedadGLNombre) BEGIN
            SELECT -1; --El campo SapSociedadGLNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        INSERT INTO NCSapSociedadesGL
        VALUES (@SapSociedadGLId,
                @SapSociedadGLNombre,
                @Activo,
                @UsuarioIdSesion,
                @vFecha,
                @UsuarioIdSesion,
                @vFecha);

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 2 BEGIN

        IF EXISTS(SELECT sapsoc.SapSociedadGLNombre
                  FROM NCSapSociedadesGL sapsoc
                  WHERE sapsoc.SapSociedadGLNombre = @SapSociedadGLNombre
                        AND NOT (sapsoc.SapSociedadGLId = @SapSociedadGLId)) BEGIN
            SELECT -1; --El campo SapSociedadGLNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        UPDATE NCSapSociedadesGL
        SET SapSociedadGLNombre = @SapSociedadGLNombre,
            Activo              = @Activo,
            UsuarioIdUltMod     = @UsuarioIdSesion,
            FechaUltMod         = @vFecha
        WHERE   SapSociedadGLId = @SapSociedadGLId;

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 3 BEGIN

        DELETE NCSapSociedadesGL
        WHERE   SapSociedadGLId = @SapSociedadGLId;

    END

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 16:25:42
-- Descripcion: Consulta individual del catalogo SapSociedadGL (SapSociedadesGL)
-- ==========================================================================================
CREATE PROCEDURE NCSapSociedadesGLCI
 @SapSociedadGLId AS VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta individual
    SELECT  sapsoc.SapSociedadGLId,
            sapsoc.SapSociedadGLNombre,
            sapsoc.Activo,
            sapsoc.UsuarioIdCreador,
            sapsoc.FechaCreacion,
            sapsoc.UsuarioIdUltMod,
            sapsoc.FechaUltMod
    FROM NCSapSociedadesGL sapsoc
    WHERE   sapsoc.SapSociedadGLId = @SapSociedadGLId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 16:25:42
-- Descripcion: Consulta de combo del catalogo SapSociedadGL (SapSociedadesGL)
-- ==========================================================================================
CREATE PROCEDURE NCSapSociedadesGLCCmb
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta de combo
    SELECT  SapSociedadGLId AS Id,
            SapSociedadGLNombre AS Texto
    FROM NCSapSociedadesGL
    WHERE   Activo = 1
    ORDER BY SapSociedadGLNombre;

END
GO
