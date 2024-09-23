/* ************************************************** */
/* CREACION DEL ARBOL DE PERMISOS PARA MenuId = 1101     */
/* JERARQUIA ENCONTRADA ALAAAF                          */
/* ************************************************** */
USE [Sisegui2020]
GO

DECLARE @vJerarquiaAdminDoc AS VARCHAR(400);
DECLARE @vJerarquiaSelCol AS VARCHAR(400);
DECLARE @vTblPermisoId TABLE (PermisoId BIGINT);
DECLARE @vTblOpcMenuId TABLE (OpcionXMenuId BIGINT);

/* Insertamos la opción del menú principal */
INSERT INTO @vTblOpcMenuId
EXEC PTSisOpcXMenuIAE 1, 1, 1101, NULL, 'Régimenes fiscales', 'ALAAAF', 10, '/PriCatalogos/RegimenesFiscales/RegimenFiscalInicia', '', 1, 'RegimenFiscalInicia', 0;

DECLARE @vJerarquiaRegimenesFiscales AS VARCHAR(400);
SELECT @vJerarquiaRegimenesFiscales = Jerarquia
FROM PTPermisos
WHERE MenuId = 1101 AND OpcionXMenuId = (SELECT TOP 1 OpcionXMenuId FROM @vTblOpcMenuId);

    /* **************************************************************************************************** */
    /* Permisos para la entidad RegimenFiscal (RegimenesFiscales) */
    /* **************************************************************************************************** */
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Consultar por id', @vJerarquiaRegimenesFiscales, NULL, 'RegimenFiscalXId', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Insertar', @vJerarquiaRegimenesFiscales, NULL, 'RegimenFiscalInserta', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Actualizar', @vJerarquiaRegimenesFiscales, NULL, 'RegimenFiscalActualiza', 0, 0, 1;
    EXEC PTPermEncIAE 1, 1, 1101, NULL, 'Eliminar', @vJerarquiaRegimenesFiscales, NULL, 'RegimenFiscalElimina', 0, 0, 1;

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
WHERE   Jerarquia LIKE @vJerarquiaRegimenesFiscales + '%'
        AND ISNULL(pxp.PerfilId, 0) = 0


USE [Rediin2022]
GO
/* ******************************************************************************** */
/* Script para la entidad RegimenFiscal (RegimenesFiscales) */
/* ******************************************************************************** */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       19/09/2024 11:15:40
-- Descripcion: Consulta paginada del catalogo RegimenFiscal (RegimenesFiscales)
-- ==========================================================================================
CREATE PROCEDURE NCRegimenesFiscalesCP
 @LinIni AS BIGINT,
 @TamPag AS BIGINT,
 @ColumnaOrden AS VARCHAR(60) = NULL,
 @FilRegimenFiscalNombre AS VARCHAR(120) = NULL,
 @FilActivo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF  ISNULL(@LinIni, 0) = 0 BEGIN

        --Cuenta los registros
        SELECT  COUNT_BIG(*) AS TotalRegistros
        FROM NCRegimenesFiscales regfis
        WHERE   (@FilRegimenFiscalNombre IS NULL
                    OR regfis.RegimenFiscalNombre LIKE '%' + @FilRegimenFiscalNombre + '%')
                AND (@FilActivo IS NULL
                    OR regfis.Activo = @FilActivo)

        RETURN;
    END

    --Consulta paginada
    SELECT  t.*
    FROM (
        SELECT  regfis.RegimenFiscaId,
                regfis.RegimenFiscalClave,
                regfis.RegimenFiscalNombre,
                regfis.Activo,
                regfis.UsuarioIdCreador,
                regfis.FechaCreacion,
                regfis.UsuarioIdUltMod,
                regfis.FechaUltMod
        FROM NCRegimenesFiscales regfis
        WHERE   (@FilRegimenFiscalNombre IS NULL
                    OR regfis.RegimenFiscalNombre LIKE '%' + @FilRegimenFiscalNombre + '%')
                AND (@FilActivo IS NULL
                    OR regfis.Activo = @FilActivo)
    ) AS t
    ORDER BY
        CASE WHEN @ColumnaOrden = 'RegimenFiscaId' THEN t.RegimenFiscaId END ASC,
        CASE WHEN @ColumnaOrden = '-RegimenFiscaId' THEN t.RegimenFiscaId END DESC,
        CASE WHEN @ColumnaOrden = 'RegimenFiscalClave' THEN t.RegimenFiscalClave END ASC,
        CASE WHEN @ColumnaOrden = '-RegimenFiscalClave' THEN t.RegimenFiscalClave END DESC,
        CASE WHEN @ColumnaOrden = 'RegimenFiscalNombre' THEN t.RegimenFiscalNombre END ASC,
        CASE WHEN @ColumnaOrden = '-RegimenFiscalNombre' THEN t.RegimenFiscalNombre END DESC,
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
-- Fecha:       19/09/2024 11:15:40
-- Descripcion: Mantenimiento del catalogo RegimenFiscal (RegimenesFiscales)
-- ==========================================================================================
CREATE PROCEDURE NCRegimenesFiscalesIAE
 @UsuarioIdSesion AS BIGINT,
 @AccionBD AS INT,
 @RegimenFiscaId AS BIGINT,
 @RegimenFiscalClave AS VARCHAR(10) = NULL,
 @RegimenFiscalNombre AS VARCHAR(120) = NULL,
 @Activo AS BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @vFecha AS DATETIME = GETDATE();

    IF @AccionBD = 1 BEGIN

        IF EXISTS(SELECT RegimenFiscaId
                  FROM NCRegimenesFiscales
                  WHERE RegimenFiscaId = @RegimenFiscaId) BEGIN
            SELECT 0; --Ya existe el registro con la llave.
            RETURN;
        END

        INSERT INTO NCRegimenesFiscales
        VALUES (@RegimenFiscaId,
                @RegimenFiscalClave,
                @RegimenFiscalNombre,
                @Activo,
                @UsuarioIdSesion,
                @vFecha,
                @UsuarioIdSesion,
                @vFecha);

        SELECT 1; --Ok

    END ELSE IF @AccionBD = 2 BEGIN

        UPDATE NCRegimenesFiscales
        SET RegimenFiscalClave  = @RegimenFiscalClave,
            RegimenFiscalNombre = @RegimenFiscalNombre,
            Activo              = @Activo,
            UsuarioIdUltMod     = @UsuarioIdSesion,
            FechaUltMod         = @vFecha
        WHERE   RegimenFiscaId = @RegimenFiscaId;

    END ELSE IF @AccionBD = 3 BEGIN

        DELETE NCRegimenesFiscales
        WHERE   RegimenFiscaId = @RegimenFiscaId;

    END

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       19/09/2024 11:15:40
-- Descripcion: Consulta individual del catalogo RegimenFiscal (RegimenesFiscales)
-- ==========================================================================================
CREATE PROCEDURE NCRegimenesFiscalesCI
 @RegimenFiscaId AS BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta individual
    SELECT  regfis.RegimenFiscaId,
            regfis.RegimenFiscalClave,
            regfis.RegimenFiscalNombre,
            regfis.Activo,
            regfis.UsuarioIdCreador,
            regfis.FechaCreacion,
            regfis.UsuarioIdUltMod,
            regfis.FechaUltMod
    FROM NCRegimenesFiscales regfis
    WHERE   regfis.RegimenFiscaId = @RegimenFiscaId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Autor:       Julio Romero Delfin (Creado por CodLeSS V5)
-- Fecha:       19/09/2024 11:15:40
-- Descripcion: Consulta de combo del catalogo RegimenFiscal (RegimenesFiscales)
-- ==========================================================================================
CREATE PROCEDURE NCRegimenesFiscalesCCmb
AS
BEGIN
    SET NOCOUNT ON;

    --Consulta de combo
    SELECT  regfis.RegimenFiscalClave AS Id,
            regfis.RegimenFiscalNombre AS Texto
    FROM NCRegimenesFiscales regfis
    WHERE   regfis.Activo = 1
    ORDER BY regfis.RegimenFiscalNombre;

END
GO
