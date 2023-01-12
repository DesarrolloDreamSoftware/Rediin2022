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
EXEC PTSisOpcXMenuIAE 1, 1, 1101, NULL, 'Sociedades', 'ALAAAE', 70, '/PriCatalogos/SapSociedades/SapSociedadInicia', '', 'SapSociedadInicia', 0;

DECLARE @vJerarquiaSapSociedades AS VARCHAR(400);
SELECT @vJerarquiaSapSociedades = Jerarquia
FROM PTPermisos
WHERE MenuId = 1101 AND OpcionXMenuId = (SELECT TOP 1 OpcionXMenuId FROM @vTblOpcMenuId);

    /* **************************************************************************************************** */
    /* Permisos para la entidad SapSociedad (SapSociedades) */
    /* **************************************************************************************************** */
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar por id', @vJerarquiaSapSociedades, NULL, 'SapSociedadXId', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Insertar', @vJerarquiaSapSociedades, NULL, 'SapSociedadInserta', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Actualizar', @vJerarquiaSapSociedades, NULL, 'SapSociedadActualiza', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Eliminar', @vJerarquiaSapSociedades, NULL, 'SapSociedadElimina', 0, 0, 1;
    /* Exportacion a excel para la entidad SapSociedad (SapSociedades) */
    EXEC PTPermEncIAE 1, 1, 1101,NULL, 'Exportar datos', @vJerarquiaSapSociedades, NULL, 'SapSociedadExporta', 0, 0, 1;

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
WHERE   Jerarquia LIKE @vJerarquiaSapSociedades + '%'
        AND ISNULL(pxp.PerfilId, 0) = 0


USE [Rediin2022]
GO
/* ******************************************************************************** */
/* Script para la entidad SapSociedad (SapSociedades) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 16:22:21
-- Descripcion: Consulta paginada del catalogo SapSociedad (SapSociedades)
-- ==========================================================================================
CREATE PROCEDURE NCSapSociedadesCP
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL,
 @FilSapSociedadNombre AS VARCHAR(120) = NULL,
 @FilActivo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NCSapSociedades sapsoc
        WHERE   (@FilSapSociedadNombre IS NULL
                    OR sapsoc.SapSociedadNombre LIKE '%' + @FilSapSociedadNombre + '%')
                AND (@FilActivo IS NULL
                    OR sapsoc.Activo = @FilActivo)

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  sapsoc.SapSociedadId,
                sapsoc.SapSociedadNombre,
                sapsoc.Activo,
                sapsoc.UsuarioIdCreador,
                sapsoc.FechaCreacion,
                sapsoc.UsuarioIdUltMod,
                sapsoc.FechaUltMod
        FROM NCSapSociedades sapsoc
        WHERE   (@FilSapSociedadNombre IS NULL
                    OR sapsoc.SapSociedadNombre LIKE '%' + @FilSapSociedadNombre + '%')
                AND (@FilActivo IS NULL
                    OR sapsoc.Activo = @FilActivo)
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'SapSociedadId' THEN t.SapSociedadId END ASC,
        CASE WHEN @ColumnaOrden = '-SapSociedadId' THEN t.SapSociedadId END DESC,
        CASE WHEN @ColumnaOrden = 'SapSociedadNombre' THEN t.SapSociedadNombre END ASC,
        CASE WHEN @ColumnaOrden = '-SapSociedadNombre' THEN t.SapSociedadNombre END DESC,
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
-- Fecha:       10/01/2023 16:22:21
-- Descripcion: Mantenimiento del catalogo SapSociedad (SapSociedades)
-- ==========================================================================================
CREATE PROCEDURE NCSapSociedadesIAE
 @UsuarioIdSesion AS BIGINT,
 @AccionBD AS INT,
 @SapSociedadId AS VARCHAR(50),
 @SapSociedadNombre AS VARCHAR(120) = NULL,
 @Activo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();

    IF @AccionBD = 1 BEGIN

        IF EXISTS(SELECT SapSociedadId
                  FROM NCSapSociedades
                  WHERE SapSociedadId = @SapSociedadId) BEGIN
            SELECT 0; --Ya existe el registro con la llave.
            RETURN;
        END

        IF EXISTS(SELECT sapsoc.SapSociedadNombre
                  FROM NCSapSociedades sapsoc
                  WHERE sapsoc.SapSociedadNombre = @SapSociedadNombre) BEGIN
            SELECT -1; --El campo SapSociedadNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        INSERT INTO NCSapSociedades
        VALUES (@SapSociedadId,
                @SapSociedadNombre,
                @Activo,
                @UsuarioIdSesion,
                @vFecha,
                @UsuarioIdSesion,
                @vFecha);

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 2 BEGIN

        IF EXISTS(SELECT sapsoc.SapSociedadNombre
                  FROM NCSapSociedades sapsoc
                  WHERE sapsoc.SapSociedadNombre = @SapSociedadNombre
                        AND NOT (sapsoc.SapSociedadId = @SapSociedadId)) BEGIN
            SELECT -1; --El campo SapSociedadNombre esta duplicado (no es llave pero debe ser unico).
            RETURN;
        END

        UPDATE NCSapSociedades
        SET SapSociedadNombre = @SapSociedadNombre,
            Activo            = @Activo,
            UsuarioIdUltMod   = @UsuarioIdSesion,
            FechaUltMod       = @vFecha
        WHERE   SapSociedadId = @SapSociedadId;

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 3 BEGIN

        DELETE NCSapSociedades
        WHERE   SapSociedadId = @SapSociedadId;

    END

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 16:22:21
-- Descripcion: Consulta individual del catalogo SapSociedad (SapSociedades)
-- ==========================================================================================
CREATE PROCEDURE NCSapSociedadesCI
 @SapSociedadId AS VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta individual
    SELECT  sapsoc.SapSociedadId,
            sapsoc.SapSociedadNombre,
            sapsoc.Activo,
            sapsoc.UsuarioIdCreador,
            sapsoc.FechaCreacion,
            sapsoc.UsuarioIdUltMod,
            sapsoc.FechaUltMod
    FROM NCSapSociedades sapsoc
    WHERE   sapsoc.SapSociedadId = @SapSociedadId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       10/01/2023 16:22:21
-- Descripcion: Consulta de combo del catalogo SapSociedad (SapSociedades)
-- ==========================================================================================
CREATE PROCEDURE NCSapSociedadesCCmb
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta de combo
    SELECT  SapSociedadId AS Id,
            SapSociedadNombre AS Texto
    FROM NCSapSociedades
    WHERE   Activo = 1
    ORDER BY SapSociedadNombre;

END
GO
